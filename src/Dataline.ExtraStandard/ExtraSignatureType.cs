namespace ExtraStandard
{
    /// <summary>
    /// Identifikatoren für Signaturverfahren
    /// </summary>
    public static class ExtraSignatureType
    {
        /// <summary>
        /// Keine Signatur
        /// </summary>
        public static readonly string None = "http://www.extra-standard.de/transforms/signature/NONE";

        /// <summary>
        /// Signatur als PKCS#7
        /// </summary>
        public static readonly string Pkcs7 = "http://www.extra-standard.de/transforms/signature/PKCS7";

        /// <summary>
        /// Signatur als PEM
        /// </summary>
        public static readonly string Pem = "http://www.extra-standard.de/transforms/signature/PEM";
    }
}
