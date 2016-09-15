using System.IO;
using System.IO.Compression;
using System.Linq;

namespace ExtraStandard.Compression
{
    /// <summary>
    /// ZIP-Kompression
    /// </summary>
    public class ZipCompressionHandler : IExtraCompressionHandler
    {
        public string AlgorithmId { get; } = ExtraCompression.Zip;

        public byte[] Compress(byte[] data, string dataName)
        {
            var fileName = string.IsNullOrEmpty(dataName) ? "data.bin" : dataName;
            var output = new MemoryStream();
            using (var archive = new ZipArchive(output, ZipArchiveMode.Create))
            {
                var entry = archive.CreateEntry(fileName, CompressionLevel.Optimal);
                using (var stream = entry.Open())
                    stream.Write(data, 0, data.Length);
            }
            return output.ToArray();
        }

        public byte[] Decompress(byte[] data)
        {
            var output = new MemoryStream();
            using (var archive = new ZipArchive(new MemoryStream(data), ZipArchiveMode.Read))
            {
                var entry = archive.Entries.Single(x => x.Length != 0);
                using (var stream = entry.Open())
                    stream.CopyTo(output);
            }
            return output.ToArray();
        }
    }
}
