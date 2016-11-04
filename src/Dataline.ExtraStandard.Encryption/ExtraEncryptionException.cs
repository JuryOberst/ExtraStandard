using System;

namespace ExtraStandard.Encryption
{
    /// <summary>
    /// Fehler bei der Ver- oder Entschlüsselung
    /// </summary>
    public class ExtraEncryptionException : ExtraException
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="ExtraEncryptionException"/> Klasse.
        /// </summary>
        public ExtraEncryptionException()
        {
        }

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="ExtraEncryptionException"/> Klasse.
        /// </summary>
        /// <param name="message">Die Fehlermeldung</param>
        public ExtraEncryptionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="ExtraEncryptionException"/> Klasse.
        /// </summary>
        /// <param name="message">Die Fehlermeldung</param>
        /// <param name="innerException">Die innere <see cref="Exception"/></param>
        public ExtraEncryptionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
