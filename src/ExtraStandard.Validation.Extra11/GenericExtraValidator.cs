﻿using ExtraStandard.Extra11;

namespace ExtraStandard.Validation.Extra11
{
    /// <summary>
    /// Standard-Implementation eines <see cref="ExtraValidator"/> für eXTra-1.1-Dokumente
    /// </summary>
    public class GenericExtraValidator : ResourceExtraValidator
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="GenericExtraValidator"/> Klasse.
        /// </summary>
        /// <param name="messageType">Die Meldungsart die zu prüfen ist</param>
        /// <param name="transportDirection">Gesendete oder empfangene Meldung?</param>
        public GenericExtraValidator(ExtraMessageType messageType, ExtraTransportDirection transportDirection)
            : base(new GenericExtraValidationResources(messageType, transportDirection))
        {
        }
    }
}