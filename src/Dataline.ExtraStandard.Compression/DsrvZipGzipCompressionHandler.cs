using System.IO;
using System.IO.Compression;

namespace ExtraStandard.Compression
{
    /// <summary>
    /// GZIP-Kompression für die DSRV mit ZIP als Verfahrenskennung
    /// </summary>
    public class DsrvZipGzipCompressionHandler : IExtraCompressionHandler
    {
        private readonly GzipCompressionHandler _gzipMethod = new GzipCompressionHandler();

        private readonly ZipCompressionHandler _zipMethod = new ZipCompressionHandler();

        /// <inheritdoc />
        public string AlgorithmId => _zipMethod.AlgorithmId;

        /// <inheritdoc />
        public byte[] Compress(byte[] data, string dataName)
        {
            return _gzipMethod.Compress(data, dataName);
        }

        /// <inheritdoc />
        public byte[] Decompress(byte[] data)
        {
            if (data.Length > 2 && data[0] == 0x50 && data[1] == 0x4B)
            {
                // ZIP-Kennung "PK"
                return _zipMethod.Decompress(data);
            }

            return _gzipMethod.Decompress(data);
        }
    }
}
