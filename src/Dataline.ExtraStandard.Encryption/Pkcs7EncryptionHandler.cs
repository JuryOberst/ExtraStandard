using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

#if !SUPPORTS_PKCS_SIGNING
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Cms;
#endif
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;

using X509Certificate = Org.BouncyCastle.X509.X509Certificate;

namespace ExtraStandard.Encryption
{
    /// <summary>
    /// Signatur und Verschlüsselung über PKCS#7 v1.5
    /// </summary>
    public class Pkcs7EncryptionHandler : IExtraEncryptionHandler
    {
        private readonly IExtraEncryptionHandler _encryptionHandler;

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="Pkcs7EncryptionHandler"/> Klasse.
        /// </summary>
        /// <param name="senderCertificate">Das X509-Zertifikat des Absenders (PKCS#12)</param>
        /// <param name="receiverCertificate">Das X509-Zertifikat des Empfängers</param>
        /// <param name="oldSenderCertificates">Die abgelaufenen X509-Zertifikate des Absenders</param>
        public Pkcs7EncryptionHandler(Pkcs12Store senderCertificate, X509Certificate receiverCertificate, IEnumerable<Pkcs12Store> oldSenderCertificates = null)
        {
#if SUPPORTS_PKCS_SIGNING
            _encryptionHandler = new NativePkcs7EncryptionHandler(senderCertificate, receiverCertificate, oldSenderCertificates);
#else
            _encryptionHandler = new BouncyCastlePkcs7EncryptionHandler(senderCertificate, receiverCertificate, oldSenderCertificates);
#endif
        }

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="Pkcs7EncryptionHandler"/> Klasse.
        /// </summary>
        /// <param name="senderCertificate">Das X509-Zertifikat des Absenders (PKCS#12)</param>
        /// <param name="receiverCertificate">Das X509-Zertifikat des Empfängers</param>
        /// <param name="oldSenderCertificates">Die abgelaufenen X509-Zertifikate des Absenders</param>
        public Pkcs7EncryptionHandler(X509Certificate2 senderCertificate, X509Certificate2 receiverCertificate, IEnumerable<X509Certificate2> oldSenderCertificates = null)
        {
#if SUPPORTS_PKCS_SIGNING
            _encryptionHandler = new NativePkcs7EncryptionHandler(senderCertificate, receiverCertificate, oldSenderCertificates);
#else
            _encryptionHandler = new BouncyCastlePkcs7EncryptionHandler(senderCertificate, receiverCertificate, oldSenderCertificates);
#endif
        }

        /// <summary>
        /// Wird native Signatur und Entschlüsselung unterstützt?
        /// </summary>
#if SUPPORTS_PKCS_SIGNING
        public static bool IsNativeSupported { get; } = true;
#else
        public static bool IsNativeSupported { get; } = false;
#endif

        /// <inheritdoc />
        public string AlgorithmId { get; } = ExtraEncryption.Pkcs7;

        /// <inheritdoc />
        public byte[] Encrypt(byte[] data, DateTime requestTimestamp)
        {
            return _encryptionHandler.Encrypt(data, requestTimestamp);
        }

        /// <inheritdoc />
        public byte[] Decrypt(byte[] data)
        {
            return _encryptionHandler.Decrypt(data);
        }
    }
}
