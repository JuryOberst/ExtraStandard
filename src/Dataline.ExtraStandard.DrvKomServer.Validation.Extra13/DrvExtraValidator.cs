﻿using ExtraStandard.DrvKomServer.Extra13;
using ExtraStandard.Validation;

namespace ExtraStandard.DrvKomServer.Validation.Extra13
{
    /// <summary>
    /// Standard-Implementation eines <see cref="ExtraValidator"/> für GKV eXTra-1.3-Dokumente
    /// </summary>
    public class DrvExtraValidator : ResourceExtraValidator, IDrvExtra13Validator
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="DrvExtraValidator"/> Klasse.
        /// </summary>
        /// <param name="messageType">Die Meldungsart die zu prüfen ist</param>
        /// <param name="transportDirection">Gesendete oder empfangene Meldung?</param>
        /// <param name="isError">Liegt eine Fehlermeldung vor?</param>
        public DrvExtraValidator(ExtraMessageType messageType, ExtraTransportDirection transportDirection, bool isError)
            : base(new DrvExtraValidationResources(messageType, transportDirection, isError))
        {
        }
    }
}
