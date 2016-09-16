using System;

namespace ExtraStandard
{
    /// <summary>
    /// Die Basis-Exception für eXTra-Fehler
    /// </summary>
    public class ExtraException : Exception
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="ExtraException"/> Klasse.
        /// </summary>
        public ExtraException()
        {
        }

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="ExtraException"/> Klasse.
        /// </summary>
        /// <param name="message">Die Fehlermeldung</param>
        public ExtraException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="ExtraException"/> Klasse.
        /// </summary>
        /// <param name="message">Die Fehlermeldung</param>
        /// <param name="inner">Die innere Exception</param>
        public ExtraException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
