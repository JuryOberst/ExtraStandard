#if !USE_BOUNCYCASTLE

using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace ExtraStandard.Encryption
{
    internal static class CertificateExtensions
    {
        private static readonly Oid _hashSha1 = new Oid("SHA1");
        private static readonly Oid _hashSha256 = new Oid("SHA256");

        public static bool IsSha256(this X509Certificate2 cert)
        {
            return cert.GetSignatureAlgorithmForCert().Value == _hashSha256.Value;
        }

        public static Oid GetSignatureAlgorithmForCert(this X509Certificate2 cert)
        {
            return GetSignatureAlgorithmForCert(cert, _hashSha1);
        }

        public static Oid GetSignatureAlgorithmForCert(this X509Certificate2 cert, Oid defaultValue)
        {
            switch (cert.SignatureAlgorithm.FriendlyName)
            {
                case "sha256RSA":
                    return _hashSha256;
                case "sha1RSA":
                    return _hashSha1;
            }
            return defaultValue;
        }
    }
}

#endif
