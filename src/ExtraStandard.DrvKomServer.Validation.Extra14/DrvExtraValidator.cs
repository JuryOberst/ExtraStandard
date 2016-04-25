using ExtraStandard.DrvKomServer.Extra14;
using ExtraStandard.Validation;

namespace ExtraStandard.DrvKomServer.Validation.Extra14
{
    /// <summary>
    /// Standard-Implementation eines <see cref="ExtraValidator"/> für GKV eXTra-1.4-Dokumente
    /// </summary>
    public class DrvExtraValidator : ResourceExtraValidator
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="DrvExtraValidator"/> Klasse.
        /// </summary>
        /// <param name="messageType">Die Meldungsart die zu prüfen ist</param>
        /// <param name="transportDirection">Gesendete oder empfangene Meldung?</param>
        public DrvExtraValidator(ExtraMessageType messageType, ExtraTransportDirection transportDirection)
            : base(new DrvExtraValidationResources(messageType, transportDirection))
        {
        }
    }
}
