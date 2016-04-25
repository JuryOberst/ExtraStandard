namespace ExtraStandard
{
    /// <summary>
    /// Identifikatoren für Erkennung und Verarbeitung von Testfällen
    /// </summary>
    public static class ExtraTestIndicator
    {
        /// <summary>
        /// Kein Test (Echtdaten)
        /// </summary>
        public static readonly string NoTest = "http://www.extra-standard.de/test/NONE";

        /// <summary>
        /// Test für Empfang?
        /// </summary>
        public static readonly string Receive = "http://www.extra-standard.de/test/RECEIVE";

        /// <summary>
        /// Test für Akzeptieren der Meldung?
        /// </summary>
        public static readonly string Accept = "http://www.extra-standard.de/test/ACCEPT";

        /// <summary>
        /// Test der Verarbeitung
        /// </summary>
        public static readonly string Process = "http://www.extra-standard.de/test/PROCESS";
    }
}
