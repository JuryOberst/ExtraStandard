namespace ExtraStandard.Validation
{
    /// <summary>
    /// Schnittstelle einer Factory für einen <see cref="IExtraValidator"/> für eXTra-1.1-Dokumente
    /// </summary>
    /// <typeparam name="T">Eine Schnittstelle die von <see cref="IExtraValidator"/> abgeleitet ist</typeparam>
    public interface IExtraValidatorFactory<out T> where T : IExtraValidator
    {
        /// <summary>
        /// Erstellung eines Validators
        /// </summary>
        /// <param name="messageType">Meldungstyp (Datenlieferung, Abholung und Bestätigung)</param>
        /// <param name="transportDirection">Versand oder Rückmeldung</param>
        /// <param name="isError">Ist das zu prüfende Dokument ein Fehler-Dokument?</param>
        /// <returns></returns>
        T Create(ExtraMessageType messageType, ExtraTransportDirection transportDirection, bool isError);
    }
}
