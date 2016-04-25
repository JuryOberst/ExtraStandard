namespace ExtraStandard
{
    /// <summary>
    /// Identifikatoren für das Kommunikationszenario
    /// </summary>
    public static class ExtraScenario
    {
        /// <summary>
        /// Einfach versenden und keine Antwort erwarten
        /// </summary>
        public static readonly string FireAndForget = "http://www.extra-standard.de/scenario/fire-and-forget";

        /// <summary>
        /// Versenden mit Empfangsbestätigung
        /// </summary>
        public static readonly string RequestWithAcknowledgement = "http://www.extra-standard.de/scenario/request-with-acknowledgement";

        /// <summary>
        /// Versenden mit Antwort
        /// </summary>
        public static readonly string RequestWithResponse = "http://www.extra-standard.de/scenario/request-with-response";
    }
}
