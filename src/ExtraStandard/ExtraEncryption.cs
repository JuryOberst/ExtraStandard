namespace ExtraStandard
{
    /// <summary>
    /// Identifikatoren für Verschlüsselungsalgorithmen
    /// </summary>
    public static class ExtraEncryption
    {
        /// <summary>
        /// Keine Verschlüsselung
        /// </summary>
        public static readonly string None = "http://www.extra-standard.de/transforms/encryption/NONE";

        /// <summary>
        /// Verschlüsselung mit dem PKCS#7-Verfahren
        /// </summary>
        public static readonly string Pkcs7 = "http://www.extra-standard.de/transforms/encryption/PKCS7";

        /// <summary>
        /// Verschlüsselung über PEM (RSA-Verschlüsselung?)
        /// </summary>
        public static readonly string Pem = "http://www.extra-standard.de/transforms/encryption/PEM";
    }
}
