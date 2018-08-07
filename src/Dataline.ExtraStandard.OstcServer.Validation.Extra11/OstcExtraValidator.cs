using ExtraStandard.OstcServer.Extra11;
using ExtraStandard.Validation;

namespace ExtraStandard.OstcServer.Validation.Extra11
{
    /// <summary>
    /// Standard-Implementation eines <see cref="ExtraValidator"/> für OSTC eXTra-1.1-Dokumente
    /// </summary>
    public class OstcExtraValidator : ResourceExtraValidator, IOstcExtra11Validator
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="OstcExtraValidator"/> Klasse.
        /// </summary>
        /// <param name="messageType">Die Meldungsart die zu prüfen ist</param>
        /// <param name="transportDirection">Gesendete oder empfangene Meldung?</param>
        /// <param name="isError">Liegt eine Fehlermeldung vor?</param>
        public OstcExtraValidator(ExtraMessageType messageType, ExtraTransportDirection transportDirection, bool isError)
            : base(new OstcExtraValidationResources(messageType, transportDirection, isError))
        {
        }
    }
}
