namespace ExtraStandard
{
    /// <summary>
    /// Identifikatoren für Beschreibung von Operationen im Log
    /// </summary>
    public static class ExtraOperationType
    {
        /// <summary>
        /// Nicht definiert
        /// </summary>
        public static readonly string Undefined = "http://www.extra-standard.de/operation/UNDEFINED";

        /// <summary>
        /// Senden
        /// </summary>
        public static readonly string Send = "http://www.extra-standard.de/operation/SEND";

        /// <summary>
        /// Empfangen
        /// </summary>
        public static readonly string Receive = "http://www.extra-standard.de/operation/RECEIVE";

        /// <summary>
        /// Verschlüsseln
        /// </summary>
        public static readonly string Encrypt = "http://www.extra-standard.de/operation/ENCRYPT";

        /// <summary>
        /// Entschlüsseln
        /// </summary>
        public static readonly string Decrypt = "http://www.extra-standard.de/operation/DECRYPT";

        /// <summary>
        /// Komprimieren
        /// </summary>
        public static readonly string Compress = "http://www.extra-standard.de/operation/COMPRESS";

        /// <summary>
        /// Dekomprimieren
        /// </summary>
        public static readonly string Decompress = "http://www.extra-standard.de/operation/DECOMPRESS";

        /// <summary>
        /// Prüfen
        /// </summary>
        public static readonly string Validate = "http://www.extra-standard.de/operation/VALIDATE";

        /// <summary>
        /// Signieren
        /// </summary>
        public static readonly string Sign = "http://www.extra-standard.de/operation/SIGN";

        /// <summary>
        /// Signaturprüfung
        /// </summary>
        public static readonly string CheckSignature = "http://www.extra-standard.de/operation/CHECK-SIGNATURE";
    }
}
