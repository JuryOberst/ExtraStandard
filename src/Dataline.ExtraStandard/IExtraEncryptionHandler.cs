using System;

namespace ExtraStandard
{
    /// <summary>
    /// Schnittstelle für die Ver- und Entschlüsselung von Daten
    /// </summary>
    public interface IExtraEncryptionHandler
    {
        /// <summary>
        /// Holt die ID für das Verfahren
        /// </summary>
        string AlgorithmId { get; }

        /// <summary>
        /// Verschlüsselung der Daten
        /// </summary>
        /// <param name="data">Die zu verschlüsselnden Daten</param>
        /// <param name="requestTimestamp">Der Zeitstempel der Anfrage</param>
        /// <returns>Die verschlüsselten Daten</returns>
        byte[] Encrypt(byte[] data, DateTime requestTimestamp);

        /// <summary>
        /// Entschlüsselung der Daten
        /// </summary>
        /// <param name="data">Die zu entschlüsselnden Daten</param>
        /// <returns>Die entschlüsselten Daten</returns>
        byte[] Decrypt(byte[] data);
    }
}
