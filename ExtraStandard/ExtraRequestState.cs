namespace ExtraStandard
{
    /// <summary>
    /// Status der Anforderung
    /// </summary>
    public static class ExtraRequestState
    {
        /// <summary>
        /// Kein Status
        /// </summary>
        public static readonly string ExtraNone = "http://www.extra-standard.de/requestState/NONE";

        /// <summary>
        /// Verfügbar
        /// </summary>
        public static readonly string ExtraAvailable = "http://www.extra-standard.de/requestState/AVAILABLE";

        /// <summary>
        /// Abgeholt
        /// </summary>
        public static readonly string ExtraFetched = "http://www.extra-standard.de/requestState/FETCHED";

        /// <summary>
        /// Bestätigt
        /// </summary>
        public static readonly string ExtraConfirmed = "http://www.extra-standard.de/requestState/CONFIRMED";
    }
}
