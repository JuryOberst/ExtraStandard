namespace ExtraStandard.Compression
{
    /// <summary>
    /// Keine Kompression
    /// </summary>
    public class NoneCompressionHandler : IExtraCompressionHandler
    {
        /// <inheritdoc />
        public string AlgorithmId { get; } = ExtraCompression.None;

        /// <inheritdoc />
        public byte[] Compress(byte[] data, string dataName)
        {
            return data;
        }

        /// <inheritdoc />
        public byte[] Decompress(byte[] data)
        {
            return data;
        }
    }
}
