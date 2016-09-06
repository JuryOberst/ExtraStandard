using Dataline.ExtraStandard.DrvKomServer.Extra14.Dsv;

using ExtraStandard.DrvKomServer.Extra14.Dsv;
using ExtraStandard.Validation;

namespace ExtraStandard.DrvKomServer.Validation.Extra14.Dsv
{
    /// <summary>
    /// Standard-Implementation eines <see cref="ExtraValidator"/> für DRV-Versicherungsnummernabfragen (eXTra-1.4)
    /// </summary>
    public class DrvExtraValidator : ResourceExtraValidator, IDrvDsvExtra14Validator
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="DrvExtraValidator"/> Klasse.
        /// </summary>
        /// <param name="messageType">Die Meldungsart die zu prüfen ist</param>
        /// <param name="transportDirection">Gesendete oder empfangene Meldung?</param>
        /// <param name="isError">Liegt eine Fehlermeldung vor?</param>
        public DrvExtraValidator(ExtraMessageType messageType, ExtraTransportDirection transportDirection, bool isError)
            : base(new DrvDsvExtraValidationResources(messageType, transportDirection, isError))
        {
        }
    }
}
