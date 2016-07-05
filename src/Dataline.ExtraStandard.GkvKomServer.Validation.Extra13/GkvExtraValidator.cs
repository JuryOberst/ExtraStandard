using ExtraStandard.GkvKomServer.Extra13;
using ExtraStandard.Validation;

namespace ExtraStandard.GkvKomServer.Validation.Extra13
{
    /// <summary>
    /// Standard-Implementation eines <see cref="ExtraValidator"/> für DSRV eXTra-1.3-Dokumente
    /// </summary>
    public class GkvExtraValidator : ResourceExtraValidator
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="GkvExtraValidator"/> Klasse.
        /// </summary>
        /// <param name="messageType">Die Meldungsart die zu prüfen ist</param>
        /// <param name="transportDirection">Gesendete oder empfangene Meldung?</param>
        public GkvExtraValidator(ExtraMessageType messageType, ExtraTransportDirection transportDirection)
            : base(new GkvExtraValidationResources(messageType, transportDirection))
        {
        }
    }
}
