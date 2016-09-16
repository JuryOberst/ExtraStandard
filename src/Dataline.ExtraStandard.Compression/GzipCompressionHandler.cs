using System.IO;
using System.IO.Compression;

namespace ExtraStandard.Compression
{
    /// <summary>
    /// GZIP-Kompression
    /// </summary>
    public class GzipCompressionHandler : IExtraCompressionHandler
    {
        /// <inheritdoc />
        public string AlgorithmId { get; } = ExtraCompression.GZip;

        /// <inheritdoc />
        public byte[] Compress(byte[] data, string dataName)
        {
            var output = new MemoryStream();
            using (var stream = new GZipStream(output, CompressionLevel.Optimal))
                stream.Write(data, 0, data.Length);
            return output.ToArray();
        }

        /// <inheritdoc />
        public byte[] Decompress(byte[] data)
        {
            var output = new MemoryStream();
            using (var stream = new GZipStream(new MemoryStream(data), CompressionMode.Decompress))
                stream.CopyTo(output);
            return output.ToArray();
        }
    }
}
