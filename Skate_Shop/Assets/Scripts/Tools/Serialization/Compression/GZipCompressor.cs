using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace UnityTools.Serialization.Compression
{
    public class GZipCompressor
    {
        private Encoding _stringEncoder = Encoding.UTF8;

        public byte[] Compress(string str)
        {
            var bytes = _stringEncoder.GetBytes(str);
            return Compress(bytes);
        }

        public byte[] Compress(byte[] decompressedBytes)
        {
            using var decompressedStream = new MemoryStream(decompressedBytes);
            using var resultStream = new MemoryStream();
            using (var compressedStream = new GZipStream(resultStream, CompressionMode.Compress))
            {
                decompressedStream.CopyTo(compressedStream);
            }
            return resultStream.ToArray();
        }

        public string Decompress(byte[] compressedBytes)
        {
            using var compressedStream = new MemoryStream(compressedBytes);
            using var resultStream = new MemoryStream();
            using (var decompressedStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            {
                decompressedStream.CopyTo(resultStream);
            }
            return _stringEncoder.GetString(resultStream.ToArray());
        }

        public async Task<byte[]> CompressAsync(string str)
        {
            var bytes = _stringEncoder.GetBytes(str);
            return await CompressAsync(bytes);
        }

        public async Task<byte[]> CompressAsync(byte[] decompressedBytes)
        {
            using var decompressedStream = new MemoryStream(decompressedBytes);
            using var resultStream = new MemoryStream();
            using (var compressedStream = new GZipStream(resultStream, CompressionMode.Compress))
            {
                await decompressedStream.CopyToAsync(compressedStream);
            }
            return resultStream.ToArray();
        }

        public async Task<string> DecompressAsync(byte[] compressedBytes)
        {
            using var compressedStream = new MemoryStream(compressedBytes);
            using var resultStream = new MemoryStream();
            using (var decompressedStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            {
                await decompressedStream.CopyToAsync(resultStream);
            }
            return _stringEncoder.GetString(resultStream.ToArray());
        }
    }

}