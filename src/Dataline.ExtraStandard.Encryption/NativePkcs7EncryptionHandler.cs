using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;

using X509Certificate = Org.BouncyCastle.X509.X509Certificate;

namespace ExtraStandard.Encryption
{
    /// <summary>
    /// Signatur und Verschlüsselung über PKCS#7 v1.5
    /// </summary>
    public class NativePkcs7EncryptionHandler : IExtraEncryptionHandler
    {
        private static readonly Oid _hashSha1 = new Oid("SHA1");
        private static readonly Oid _hashSha256 = new Oid("SHA256");

        private readonly X509Certificate2Collection _allSenderCertificates;
        private readonly X509Certificate2 _senderCertificate;
        private readonly Lazy<X509Certificate> _encryptionCertificate;

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="NativePkcs7EncryptionHandler"/> Klasse.
        /// </summary>
        /// <param name="senderCertificate">Das X509-Zertifikat des Absenders (PKCS#12)</param>
        /// <param name="receiverCertificate">Das X509-Zertifikat des Empfängers</param>
        /// <param name="oldSenderCertificates">Die abgelaufenen X509-Zertifikate des Absenders</param>
        public NativePkcs7EncryptionHandler(X509Certificate2 senderCertificate, X509Certificate2 receiverCertificate, IEnumerable<X509Certificate2> oldSenderCertificates = null)
        {
            _senderCertificate = senderCertificate;
            _allSenderCertificates = new X509Certificate2Collection((oldSenderCertificates ?? new X509Certificate2[0]).Concat(new[] { senderCertificate }).Reverse().ToArray());
            _encryptionCertificate = new Lazy<X509Certificate>(() => new Org.BouncyCastle.X509.X509CertificateParser().ReadCertificate(receiverCertificate.RawData));
        }

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="NativePkcs7EncryptionHandler"/> Klasse.
        /// </summary>
        /// <param name="senderCertificate">Das X509-Zertifikat des Absenders (PKCS#12)</param>
        /// <param name="receiverCertificate">Das X509-Zertifikat des Empfängers</param>
        /// <param name="oldSenderCertificates">Die abgelaufenen X509-Zertifikate des Absenders</param>
        public NativePkcs7EncryptionHandler(Pkcs12Store senderCertificate, X509Certificate receiverCertificate, IEnumerable<Pkcs12Store> oldSenderCertificates = null)
            : this(ConvertToCertificate2(senderCertificate), ConvertToCertificate2(receiverCertificate), oldSenderCertificates?.Select(ConvertToCertificate2))
        {
        }

        /// <inheritdoc />
        public string AlgorithmId { get; } = ExtraEncryption.Pkcs7;

        /// <inheritdoc />
        public byte[] Encrypt(byte[] data, DateTime requestTimestamp)
        {
            var signedData = SignData(data, _senderCertificate, requestTimestamp);
            var encryptedData = EncryptData(signedData, _encryptionCertificate.Value);
            return encryptedData;
        }

        /// <inheritdoc />
        public byte[] Decrypt(byte[] data)
        {
            try
            {
                var env = new System.Security.Cryptography.Pkcs.EnvelopedCms();
                env.Decode(data);
                env.Decrypt(_allSenderCertificates);

                var decryptedData = env.ContentInfo.Content;
                var sig = new System.Security.Cryptography.Pkcs.SignedCms();
                sig.Decode(decryptedData);
                sig.CheckSignature(true);

                var verifiedData = sig.ContentInfo.Content;

                return verifiedData;
            }
            catch (Exception ex)
            {
                throw new ExtraEncryptionException("No certificate for decryption found.", ex);
            }
        }

        private static byte[] SignData(byte[] data, X509Certificate2 signCertificate, DateTime? requestTimestamp = null)
        {
            var contentInfo = new System.Security.Cryptography.Pkcs.ContentInfo(data);
            var signedCms = new System.Security.Cryptography.Pkcs.SignedCms(contentInfo);
            var signer = new System.Security.Cryptography.Pkcs.CmsSigner(signCertificate)
            {
                DigestAlgorithm = GetSignatureAlgorithmForCert(signCertificate),
                IncludeOption = X509IncludeOption.EndCertOnly
            };
            if (requestTimestamp.HasValue)
                signer.SignedAttributes.Add(new System.Security.Cryptography.Pkcs.Pkcs9SigningTime(requestTimestamp.Value));
            signedCms.ComputeSignature(signer);
            return signedCms.Encode();
        }

        private static byte[] EncryptData(byte[] data, X509Certificate encryptionCertificate)
        {
            var dataGenerator = new CmsEnvelopedDataGenerator();
            dataGenerator.AddKeyTransRecipient(encryptionCertificate);
            var encryptedData = dataGenerator.Generate(new CmsProcessableByteArray(data), CmsEnvelopedGenerator.Aes256Cbc);
            return encryptedData.GetEncoded();
        }

        private static Oid GetSignatureAlgorithmForCert(X509Certificate2 cert)
        {
            switch (cert.SignatureAlgorithm.FriendlyName)
            {
                case "sha256RSA":
                    return _hashSha256;
                case "sha1RSA":
                    return _hashSha1;
            }
            throw new ExtraException($"Unsupported signature algorithm {cert.SignatureAlgorithm.FriendlyName}");
        }

        private static X509Certificate2 ConvertToCertificate2(Pkcs12Store store)
        {
            foreach (string alias in store.Aliases)
            {
                if (store.IsCertificateEntry(alias))
                {
                    var entry = store.GetCertificate(alias);
                }
                if (store.IsKeyEntry(alias))
                {
                    var entry = store.GetKey(alias);
                }
            }

            var senderCertStream = new MemoryStream();
            store.Save(senderCertStream, new char[0], new SecureRandom());

            var coll = new X509Certificate2Collection();
            coll.Import(senderCertStream.ToArray());

            return coll.Cast<X509Certificate2>().First(x => x.HasPrivateKey);
        }

        private static X509Certificate2 ConvertToCertificate2(X509Certificate cert)
        {
            return new X509Certificate2(cert.GetEncoded());
        }
    }
}