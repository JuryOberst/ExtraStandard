﻿using ExtraStandard.DrvKomServer.Extra14;
using ExtraStandard.Validation;

namespace ExtraStandard.DrvKomServer.Validation.Extra14
{
    /// <summary>
    /// Standard-Implementation eines <see cref="ExtraValidator"/> für GKV eXTra-1.4-Dokumente
    /// </summary>
    public class DrvExtraValidator : ResourceExtraValidator, IDrvExtra14Validator
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
