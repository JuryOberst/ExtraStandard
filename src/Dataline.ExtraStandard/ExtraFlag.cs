namespace ExtraStandard
{
    /// <summary>
    /// Allgemeine eXTra-Flags-Klasse, die die Daten aus den unterschiedlichsten eXTra-Versionen zusammenfasst.
    /// </summary>
    public partial class ExtraFlag
    {

        /// <summary>
        /// Holt oder setzt den Fehlercode
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Holt oder setzt die Gewichtung
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// Holt oder setzt den Fehlertext
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Holt oder setzt den Ursprung des Flags
        /// </summary>
        public string Originator { get; set; }

        /// <summary>
        /// Holt oder setzt den Stack
        /// </summary>
        public string Stack { get; set; }
    }
}
