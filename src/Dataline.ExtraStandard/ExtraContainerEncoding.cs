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
        public const string Iso8859_1 = "I1";

        /// <summary>
        /// DIN 66003:2000-06
        /// </summary>
        public const string Din66003 = "I8";

        /// <summary>
        /// DIN 66303:1986-11 DRV8
        /// </summary>
        public const string Din66303 = "I7";

        /// <summary>
        /// Kodierung als UTF-8
        /// </summary>
        public const string Utf8 = "U8";
    }
}
