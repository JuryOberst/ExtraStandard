﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

using X509Certificate = Org.BouncyCastle.X509.X509Certificate;

namespace ExtraStandard.Encryption
{
    /// <summary>
    /// Signatur und Verschlüsselung über PKCS#7 v1.5
    /// </summary>
    public class BouncyCastlePkcs7EncryptionHandler : IExtraEncryptionHandler
    {
        private readonly IEnumerable<Pkcs12Store> _allSenderCertificates;
        private readonly Pkcs12Store _senderCertificate;
        private readonly X509Certificate _receiverCertificate;

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="BouncyCastlePkcs7EncryptionHandler"/> Klasse.
        /// </summary>
        /// <param name="senderCertificate">Das X509-Zertifikat des Absenders (PKCS#12)</param>
        /// <param name="receiverCertificate">Das X509-Zertifikat des Empfängers</param>
        /// <param name="oldSenderCertificates">Die abgelaufenen X509-Zertifikate des Absenders</param>
        public BouncyCastlePkcs7EncryptionHandler(Pkcs12Store senderCertificate, X509Certificate receiverCertificate,
            IEnumerable<Pkcs12Store> oldSenderCertificates = null)
        {
            _senderCertificate = senderCertificate;
            _receiverCertificate = receiverCertificate;
            _allSenderCertificates = (oldSenderCertificates ?? new Pkcs12Store[0]).Concat(new[] { senderCertificate }).Reverse().ToList();
        }

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="BouncyCastlePkcs7EncryptionHandler"/> Klasse.
        /// </summary>
        /// <param name="senderCertificate">Das X509-Zertifikat des Absenders (PKCS#12)</param>
        /// <param name="receiverCertificate">Das X509-Zertifikat des Empfängers</param>
        /// <param name="oldSenderCertificates">Die abgelaufenen X509-Zertifikate des Absenders</param>
        public BouncyCastlePkcs7EncryptionHandler(X509Certificate2 senderCertificate, X509Certificate2 receiverCertificate, IEnumerable<X509Certificate2> oldSenderCertificates = null)
            : this(
                  new Pkcs12Store(new MemoryStream(senderCertificate.Export(X509ContentType.Pkcs12)), new char[0]), 
                  new X509CertificateParser().ReadCertificate(receiverCertificate.Export(X509ContentType.Cert)),
                  oldSenderCertificates?.Select(cert => new Pkcs12Store(new MemoryStream(cert.Export(X509ContentType.Pkcs12)), new char[0])))
        {
        }

        /// <inheritdoc />
        public string AlgorithmId { get; } = ExtraEncryption.Pkcs7;

        /// <inheritdoc />
        public byte[] Encrypt(byte[] data, DateTime requestTimestamp)
        {
            var signedData = SignData(data, _senderCertificate, requestTimestamp);
            var encryptedData = EncryptData(signedData, _receiverCertificate);
            return encryptedData;
        }

        /// <inheritdoc />
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
                    throw new ExtraEncryptionException("Failed to verify the signature.");

                var verifiedData = new MemoryStream();
                sig.SignedContent.Write(verifiedData);

                return verifiedData.ToArray();
            }

            throw new ExtraEncryptionException("No certificate for decryption found.");
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
