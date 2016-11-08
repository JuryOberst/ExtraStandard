using System;

namespace ExtraStandard.Encryption
{
    /// <summary>
    /// Handler für "Keine Verschlüsselung/Signatur"
    /// </summary>
    public class NoneEncryptionHandler : IExtraEncryptionHandler
    {
        /// <inheritdoc />
        public string AlgorithmId { get; } = ExtraEncryption.None;

        /// <inheritdoc />
        public byte[] Encrypt(byte[] data, DateTime requestTimestamp)
        {
            return data;
        }

        /// <inheritdoc />
        public byte[] Decrypt(byte[] data)
        {
            return data;
        }
    }
}
