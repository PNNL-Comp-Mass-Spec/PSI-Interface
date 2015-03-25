using System;
using System.IO;
using System.IO.Compression;

namespace PSI_Interface.MSData
{
    public static class Zlib
    {
        public static byte[] DecompressZLib(byte[] compressedBytes, int expectedBytes)
        {
            var msCompressed = new MemoryStream(compressedBytes);
            // We must skip the first two bytes
            // See http://george.chiramattel.com/blog/2007/09/deflatestream-block-length-does-not-match.html
            // EAT the zlib headers, the rest is a normal 'deflate'd stream
            msCompressed.ReadByte();
            msCompressed.ReadByte();
            //var msInflated = new MemoryStream((int)(msCompressed.Length * 2));
            //var newBytes = new byte[msCompressed.Length * 2];
            byte[] newBytes = new byte[expectedBytes];
            // The last 32 bits (4 bytes) are supposed to be an Adler-32 checksum. Might need to remove them as well.
            using (var inflater = new DeflateStream(msCompressed, CompressionMode.Decompress))
            {
                var bytesRead = inflater.Read(newBytes, 0, expectedBytes);
                if (bytesRead != expectedBytes)
                {
                    throw new InvalidDataException("Fail decompressing data...");
                }
                //while (inflater.CanRead)
                //{
                //  var readBytes = new byte[4095];
                //  // Should be able to change to just this.
                //  var bytesRead = inflater.Read(readBytes, 0, readBytes.Length);
                //  if (bytesRead != 0)
                //  {
                //      msInflated.Write(readBytes, 0, bytesRead);
                //  }
                //}
            }
            //newBytes = new byte[msInflated.Length];
            //msInflated.Read(newBytes, 0, (int)msInflated.Length);
            return newBytes;
        }

        public static byte[] CompressZLib(byte[] decompressedBytes, out int compressedBytes)
        {
            var msDecompressed = new MemoryStream(decompressedBytes);
            // We must skip the first two bytes
            // See http://george.chiramattel.com/blog/2007/09/deflatestream-block-length-does-not-match.html
            // EAT the zlib headers, the rest is a normal 'deflate'd stream
            //msDecompressed.ReadByte();
            //msDecompressed.ReadByte();

            //var msInflated = new MemoryStream((int)(msCompressed.Length * 2));
            //var newBytes = new byte[msCompressed.Length * 2];
            byte[] newBytes = new byte[decompressedBytes.Length];

            // TODO: Add the zlib headers to the data....
            // TODO: Also need the Adler32 Checksum at the end...

            var deflate2 = new Ionic.Zlib.DeflateStream(msDecompressed, Ionic.Zlib.CompressionMode.Compress);
            compressedBytes = deflate2.Read(newBytes, 0, decompressedBytes.Length);

            // The last 32 bits (4 bytes) are supposed to be an Adler-32 checksum. Might need to remove them as well.
            using (var deflater = new DeflateStream(msDecompressed, CompressionMode.Compress))
            {
                compressedBytes = deflater.Read(newBytes, 0, decompressedBytes.Length) + 2;
                
                //while (inflater.CanRead)
                //{
                //  var readBytes = new byte[4095];
                //  // Should be able to change to just this.
                //  var bytesRead = inflater.Read(readBytes, 0, readBytes.Length);
                //  if (bytesRead != 0)
                //  {
                //      msInflated.Write(readBytes, 0, bytesRead);
                //  }
                //}
            }
            //newBytes = new byte[msInflated.Length];
            //msInflated.Read(newBytes, 0, (int)msInflated.Length);
            return newBytes;
        }
    }
}
