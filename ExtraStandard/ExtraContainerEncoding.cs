using System.Diagnostics.CodeAnalysis;

namespace ExtraStandard
{
    /// <summary>
    /// Kodierung für die übertragenen Daten
    /// </summary>
    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Typische Web-Kodierungsnamen.")]
    public static class ExtraContainerEncoding
    {
        /// <summary>
        /// Kodierung als ISO-8859-1
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static readonly string Iso8859_1 = "I1";

        /// <summary>
        /// ISO 8-Bit, Code gemäß DIN 66303:2000-06
        /// </summary>
        public static readonly string Iso8 = "I8";

        /// <summary>
        /// ASCII (7-Bit)?
        /// </summary>
        public static readonly string Din60003 = "I7";

        /// <summary>
        /// Kodierung als UTF-8
        /// </summary>
        public static readonly string Utf8 = "U8";
    }
}
