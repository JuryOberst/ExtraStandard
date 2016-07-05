namespace ExtraStandard.Validation
{
    /// <summary>
    /// Art der Meldung die validiert werden soll
    /// </summary>
    public enum ExtraMessageType
    {
        /// <summary>
        /// Versand einer Meldung
        /// </summary>
        SupplyData,

        /// <summary>
        /// Abfrage für die Abholung eines Verarbeitungsergebnisses
        /// </summary>
        GetProcessingResultQuery,

        /// <summary>
        /// Abholung eines Verarbeitungsergebnisses
        /// </summary>
        GetProcessingResult,

        /// <summary>
        /// Abfrage für die Bestätigung der Abholung eines Verarbeitungsergebnisses
        /// </summary>
        AcknowledgeProcessingResultQuery,

        /// <summary>
        /// Bestätigung der Abholung eines Verarbeitungsergebnisses
        /// </summary>
        AcknowledgeProcessingResult,
    }
}
