using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.X509;
using System;
using System.Security.Cryptography;
using SystemX509 = System.Security.Cryptography.X509Certificates;

namespace ExtraStandard.Tests
{
    internal static class Utils
    {
        public static X509Certificate FromX509Certificate(
            SystemX509.X509Certificate x509Cert)
        {
            return new X509CertificateParser().ReadCertificate(x509Cert.GetRawCertData());
        }

        public static AsymmetricCipherKeyPair GetRsaKeyPair(
            System.Security.Cryptography.RSA rsa)
        {
            return GetRsaKeyPair(rsa.ExportParameters(true));
        }

        public static AsymmetricCipherKeyPair GetRsaKeyPair(
            System.Security.Cryptography.RSAParameters rp)
        {
            BigInteger modulus = new BigInteger(1, rp.Modulus);
            BigInteger pubExp = new BigInteger(1, rp.Exponent);

            RsaKeyParameters pubKey = new RsaKeyParameters(
                false,
                modulus,
                pubExp);

            RsaPrivateCrtKeyParameters privKey = new RsaPrivateCrtKeyParameters(
                modulus,
                pubExp,
                new BigInteger(1, rp.D),
                new BigInteger(1, rp.P),
                new BigInteger(1, rp.Q),
                new BigInteger(1, rp.DP),
                new BigInteger(1, rp.DQ),
                new BigInteger(1, rp.InverseQ));

            return new AsymmetricCipherKeyPair(pubKey, privKey);
        }

        /// <summary>
        /// Temporäres Zertifikat für Verschlüsselung und Signatur erstellen.
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="email">Email</param>
        /// <param name="producerId">Hersteller-ID</param>
        /// <param name="producerVersion">Hersteller-Version</param>
        /// <param name="locality">Ort</param>
        /// <param name="country">Land</param>
        /// <returns>Zertifikat</returns>
        public static SystemX509.X509Certificate2 CreateTemporaryCertificate(
            string name,
            string email,
            string producerId,
            string producerVersion,
            string locality,
            string country)
        {
            var rsaCrypto = new System.Security.Cryptography.RSACryptoServiceProvider(2048);
            var keyPair = GetRsaKeyPair(rsaCrypto);

            var certGen = new Org.BouncyCastle.X509.X509V3CertificateGenerator();
            var now = DateTime.Now;
            certGen.SetNotAfter(new DateTime(now.Year + 1, 12, 31, 23, 59, 59));

            var notBefore = new DateTime(now.Year, 1, 1);
            certGen.SetNotBefore(notBefore);
            certGen.SetSerialNumber(new Org.BouncyCastle.Math.BigInteger(string.Format("{0:yyyyMMdd}", notBefore)));

            var certName = new Org.BouncyCastle.Asn1.X509.X509Name(
                string.Format("CN={0}, O={1}, OU={2}, L={3}, C={4}", name, producerId, producerVersion, locality, country));

            certGen.SetSubjectDN(certName);
            certGen.SetIssuerDN(certName);
            certGen.SetPublicKey(keyPair.Public);

            var bcCert = certGen.Generate(new Asn1SignatureFactory("SHA256WITHRSA", keyPair.Private));
            var cert = new SystemX509.X509Certificate2(bcCert.GetEncoded());

            cert = LinkPrivateKeyToCert(cert, rsaCrypto, null);

            return cert;
        }

        /// <summary>
        /// Zertifikat mit privatem Schlüssel verknüpfen.
        /// </summary>
        /// <param name="cert">Zertifikat</param>
        /// <param name="rsa">Privater Schlüssel</param>
        /// <param name="keyPwd">Passwort mit dem das Zertifikat verschlüsselt werden soll.</param>
        /// <returns>Neues Zertifikat</returns>
        private static SystemX509.X509Certificate2 LinkPrivateKeyToCert(
            SystemX509.X509Certificate2 cert,
            System.Security.Cryptography.RSACryptoServiceProvider rsa,
            System.Security.SecureString keyPwd)
        {
            var pkcs12store = new Org.BouncyCastle.Pkcs.Pkcs12StoreBuilder().Build();
            var rsaKeyPair = GetRsaKeyPair(rsa);
            var bcCert = FromX509Certificate(cert);
            var pkcs12data = LinkPrivateKeyToCert(bcCert, rsaKeyPair, keyPwd);
            var testCert = new SystemX509.X509Certificate2(pkcs12data, keyPwd, System.Security.Cryptography.X509Certificates.X509KeyStorageFlags.Exportable | System.Security.Cryptography.X509Certificates.X509KeyStorageFlags.PersistKeySet);
            return testCert;
        }

        /// <summary>
        /// Zertifikat mit privatem Schlüssel verknüpfen. (BouncyCastle-Teil)
        /// </summary>
        /// <param name="bcCert">Zertifikat</param>
        /// <param name="rsaKeyPair">Private Schlüssel</param>
        /// <param name="keyPwd">Passwort mit dem das Zertifikat verschlüsselt werden soll.</param>
        /// <returns></returns>
        private static byte[] LinkPrivateKeyToCert(
            Org.BouncyCastle.X509.X509Certificate bcCert,
            Org.BouncyCastle.Crypto.AsymmetricCipherKeyPair rsaKeyPair,
            System.Security.SecureString keyPwd)
        {
            var pkcs12store = new Org.BouncyCastle.Pkcs.Pkcs12StoreBuilder().Build();
            pkcs12store.SetKeyEntry(string.Empty,
                new Org.BouncyCastle.Pkcs.AsymmetricKeyEntry(rsaKeyPair.Private),
                new[] { new Org.BouncyCastle.Pkcs.X509CertificateEntry(bcCert) });
            var pkcs12data = new System.IO.MemoryStream();
            pkcs12store.Save(pkcs12data, keyPwd.ToUnencryptedString().ToCharArray(), new Org.BouncyCastle.Security.SecureRandom(new CryptoApiRandomGenerator()));
            return pkcs12data.ToArray();
        }

        /// <summary>
        /// SecureString in String umwandeln. Passwort bleibt entschlüsselt.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string ToUnencryptedString(this System.Security.SecureString value)
        {
            if (value == null)
                return string.Empty;

            var bStr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(value);
            try
            {
                return System.Runtime.InteropServices.Marshal.PtrToStringBSTR(bStr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(bStr);
            }
        }

        /// <summary>
        /// Uses RandomNumberGenerator.Create() to get randomness generator
        /// </summary>
        private class CryptoApiRandomGenerator : IRandomGenerator
        {
            private readonly RandomNumberGenerator rndProv;

            public CryptoApiRandomGenerator()
                : this(RandomNumberGenerator.Create())
            {
            }

            public CryptoApiRandomGenerator(RandomNumberGenerator rng)
            {
                this.rndProv = rng;
            }

            #region IRandomGenerator Members

            public virtual void AddSeedMaterial(byte[] seed)
            {
                // We don't care about the seed
            }

            public virtual void AddSeedMaterial(long seed)
            {
                // We don't care about the seed
            }

            public virtual void NextBytes(byte[] bytes)
            {
                rndProv.GetBytes(bytes);
            }

            public virtual void NextBytes(byte[] bytes, int start, int len)
            {
                if (start < 0)
                    throw new ArgumentException("Start offset cannot be negative", "start");
                if (bytes.Length < (start + len))
                    throw new ArgumentException("Byte array too small for requested offset and length");

                if (bytes.Length == len && start == 0)
                {
                    NextBytes(bytes);
                }
                else
                {
                    byte[] tmpBuf = new byte[len];
                    NextBytes(tmpBuf);
                    Array.Copy(tmpBuf, 0, bytes, start, len);
                }
            }

            #endregion
        }
    }
}
