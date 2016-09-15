using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using ExtraStandard;

using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.BC;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace ExtraStandard.Encryption
{
    public class Pkcs7EncryptionHandler : IExtraEncryptionHandler
    {
        private readonly IEnumerable<Pkcs12Store> _allSenderCertificates;
        private readonly Pkcs12Store _senderCertificate;
        private readonly X509Certificate _receiverCertificate;

        public Pkcs7EncryptionHandler(Pkcs12Store senderCertificate, X509Certificate receiverCertificate,
            IEnumerable<Pkcs12Store> oldSenderCertificates = null)
        {
            _senderCertificate = senderCertificate;
            _receiverCertificate = receiverCertificate;
            _allSenderCertificates = (oldSenderCertificates ?? new Pkcs12Store[0]).Concat(new[] { senderCertificate }).Reverse().ToList();
        }

        public string AlgorithmId { get; } = ExtraEncryption.Pkcs7;

        public byte[] Encrypt(byte[] data, DateTime requestTimestamp)
        {
            var signedData = SignData(data, _senderCertificate, requestTimestamp);
            var encryptedData = EncryptData(signedData, _receiverCertificate);
            return encryptedData;
        }

        public byte[] Decrypt(byte[] data)
        {
            foreach (var pkcsStore in _allSenderCertificates)
            {
                var certAlias = pkcsStore.Aliases.Cast<string>().First(x => pkcsStore.IsKeyEntry(x));
                var certEntry = pkcsStore.GetCertificate(certAlias);
                var cert = certEntry.Certificate;

                var envelopedData = new CmsEnvelopedData(data);
                var recepientInfos = envelopedData.GetRecipientInfos();
                var recepientId = new RecipientID()
                {
                    Issuer = cert.IssuerDN,
                    SerialNumber = cert.SerialNumber
                };
                var recepient = recepientInfos[recepientId];
                if (recepient == null)
                    continue;

                var privKeyEntry = pkcsStore.GetKey(certAlias);
                var privKey = privKeyEntry.Key;

                var decryptedData = recepient.GetContent(privKey);
                var sig = new CmsSignedData(decryptedData);
                var sigInfos = sig.GetSignerInfos();
                var signerId = new SignerID()
                {
                    Issuer = _receiverCertificate.IssuerDN,
                    SerialNumber = _receiverCertificate.SerialNumber
                };
                var signer = sigInfos.GetFirstSigner(signerId);
                if (!signer.Verify(_receiverCertificate))
                    throw new ExtraException("Die Signatur konnte nicht überprüft werden.");

                var verifiedData = new MemoryStream();
                sig.SignedContent.Write(verifiedData);

                return verifiedData.ToArray();
            }

            throw new ExtraException("Kein Zertifikat für die Entschlüsselung gefunden.");
        }

        private static byte[] SignData(byte[] data, Pkcs12Store signCertificate, DateTime? requestTimestamp = null)
        {
            var signCertAlias = signCertificate.Aliases.Cast<string>().First(signCertificate.IsKeyEntry);
            var signCertEntry = signCertificate.GetCertificate(signCertAlias);
            var signCert = signCertEntry.Certificate;
            var signPkEntry = signCertificate.GetKey(signCertAlias);
            var signPk = signPkEntry.Key;
            string digestName;

            if (signCert.SigAlgOid == PkcsObjectIdentifiers.Sha1WithRsaEncryption.Id)
            {
                digestName = "SHA1";
            }
            else if (signCert.SigAlgOid == PkcsObjectIdentifiers.Sha256WithRsaEncryption.Id)
            {
                digestName = "SHA256";
            }
            else
            {
                throw new ExtraException($"Unsupported digest algorithm {signCert.SigAlgName}");
            }

            var digestOid = DigestUtilities.GetObjectIdentifier(digestName).Id;
            var digest = DigestUtilities.CalculateDigest(digestName, data);

            var signedAttrs = new Dictionary<object, object>()
            {
                { CmsAttributeTableParameter.Digest, digest }
            };

            if (requestTimestamp.HasValue)
            {
                var signTimestamp = new Org.BouncyCastle.Asn1.Cms.Attribute(CmsAttributes.SigningTime, new DerSet(new Time(requestTimestamp.Value.ToUniversalTime())));
                signedAttrs.Add(signTimestamp.AttrType, signTimestamp);
            }

            var signedAttrGen = new DefaultSignedAttributeTableGenerator();
            var signedAttrTable = signedAttrGen.GetAttributes(signedAttrs);

            var generator = new CmsSignedDataGenerator();
            generator.AddSigner(signPk, signCert, digestOid, new DefaultSignedAttributeTableGenerator(signedAttrTable), null);

            var signedData = generator.Generate(new CmsProcessableByteArray(data), true);
            return signedData.GetEncoded();
        }

        private static byte[] EncryptData(byte[] data, X509Certificate encryptionCertificate)
        {
            var dataGenerator = new CmsEnvelopedDataGenerator();
            dataGenerator.AddKeyTransRecipient(encryptionCertificate);
            var encryptedData = dataGenerator.Generate(new CmsProcessableByteArray(data), CmsEnvelopedGenerator.Aes256Cbc);
            return encryptedData.GetEncoded();
        }
    }
}
