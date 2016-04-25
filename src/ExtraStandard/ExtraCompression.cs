namespace ExtraStandard
{
    /// <summary>
    /// Kennungen für die Kompressionsverfahren die vom eXTra-Standard unterstützt werden.
    /// </summary>
    public static class ExtraCompression
    {
        /// <summary>
        /// Keine Kompression
        /// </summary>
        public static readonly string None = "http://www.extra-standard.de/transforms/compression/NONE";

        /// <summary>
        /// Kompression mit dem ZIP-Verfahren (Deflate?)
        /// </summary>
        public static readonly string Zip = "http://www.extra-standard.de/transforms/compression/ZIP";

        /// <summary>
        /// Kompression mit dem GZIP-Verfahren
        /// </summary>
        public static readonly string GZip = "http://www.extra-standard.de/transforms/compression/GZIP";

        /// <summary>
        /// Kompression mit dem JET-Verfahren
        /// </summary>
        public static readonly string Jet = "http://www.extra-standard.de/transforms/compression/JET";
    }
}
