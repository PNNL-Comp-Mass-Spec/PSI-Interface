using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace PSI_Interface.MSData
{
    /// <summary>
    /// Class for dealing with zlib compressed data
    /// </summary>
    public static class Zlib
    {
        /// <summary>
        /// Decompress zlib-compressed bytes
        /// </summary>
        /// <param name="compressedBytes"></param>
        /// <param name="expectedBytes"></param>
        public static byte[] DecompressZLib(byte[] compressedBytes, int expectedBytes)
        {
            using (var msCompressed = new MemoryStream(compressedBytes))
            {
                // We must skip the first two bytes
                // See http://george.chiramattel.com/blog/2007/09/deflatestream-block-length-does-not-match.html
                // EAT the zlib headers, the rest is usually a normal 'deflate'd stream, with the exception of the last 4 bytes
                msCompressed.ReadByte();
                msCompressed.ReadByte();

                // The last 32 bits (4 bytes) are supposed to be an Adler-32 checksum. Might need to remove them as well.
                using (var inflater = new DeflateStream(msCompressed, CompressionMode.Decompress))
                {
                    var newBytes = new byte[expectedBytes];
                    // Don't need to remove the last 4 bytes, we never attempt to decompress them
                    var bytesRead = inflater.Read(newBytes, 0, expectedBytes);
                    if (bytesRead != expectedBytes)
                    {
                        throw new InvalidDataException("Fail decompressing data...");
                    }

                    // TODO: add verification of the decompressed data via Adler32 Checksum?

                    return newBytes;
                }
            }
        }

        /// <summary>
        /// Compress bytes using zlib compression
        /// </summary>
        /// <param name="decompressedBytes"></param>
        /// <param name="compressedBytes"></param>
        public static byte[] CompressZLib(byte[] decompressedBytes, out int compressedBytes)
        {
            // Calculate the adler32 checksum of the uncompressed data, that will go at the end of the compressed data, per the zlib spec.
            var adler32 = AdlerChecksum.MakeForBuff(decompressedBytes).GetValueOrDefault(0);

            // Get the bytes of the adler32, making sure they are network order/big-endian
            var adler32Bytes = BitConverter.GetBytes(adler32);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(adler32Bytes);
            }

            using (var msCompressed = new MemoryStream())
            {
                // We must skip the first two bytes
                // See http://george.chiramattel.com/blog/2007/09/deflatestream-block-length-does-not-match.html
                // EAT the zlib headers, the rest is a normal 'deflate'd stream

                // https://stackoverflow.com/questions/9050260/what-does-a-zlib-header-look-like
                // 78 01 - No Compression / low
                // 78 9C - Default Compression
                // 78 DA - Best Compression

                var zlibCompressedBytes = new List<byte>();
                // Add the zlib header bytes to the data
                zlibCompressedBytes.Add(0x78);
                zlibCompressedBytes.Add(0x9C);

                // Compress the data using the deflate algorithm
                using (var deflater = new DeflateStream(msCompressed, CompressionMode.Compress, true))
                {
                    deflater.Write(decompressedBytes, 0, decompressedBytes.Length);
                }

                msCompressed.Seek(0, SeekOrigin.Begin);
                zlibCompressedBytes.AddRange(msCompressed.ToArray());

                // add the Adler-32 checksum bytes
                zlibCompressedBytes.AddRange(adler32Bytes);

                compressedBytes = zlibCompressedBytes.Count;
                return zlibCompressedBytes.ToArray();
            }
        }

        /// <summary>
        ///  Adler 32 check sum calculation
        ///  (From en.wikipedia.org)
        ///
        ///  Adler-32 is a checksum algorithm which was invented by Mark Adler.
        ///  It is almost as reliable as a 32-bit cyclic redundancy check for
        ///  protecting against accidental modification of data, such as distortions
        ///  occurring during a transmission.
        ///  An Adler-32 checksum is obtained by calculating two 16-bit checksums A and B and
        ///  concatenating their bits into a 32-bit integer. A is the sum of all bytes in the
        ///  string, B is the sum of the individual values of A from each step.
        ///  At the beginning of an Adler-32 run, A is initialized to 1, B to 0.
        ///  The sums are done modulo 65521 (the largest prime number smaller than 216).
        ///  The bytes are stored in network order (big endian), B occupying
        ///  the two most significant bytes.
        ///  The function may be expressed as
        ///
        ///  A = 1 + D1 + D2 + ... + DN (mod 65521)
        ///  B = (1 + D1) + (1 + D1 + D2) + ... + (1 + D1 + D2 + ... + DN) (mod 65521)
        ///    = N�D1 + (N-1)�D2 + (N-2)�D3 + ... + DN + N (mod 65521)
        ///
        ///  Adler-32(D) = B * 65536 + A
        ///
        ///  where D is the string of bytes for which the checksum is to be calculated,
        ///  and N is the length of D.
        /// Written by Youry Jukov (yjukov@hotmail.com)
        /// https://www.codeproject.com/Articles/21083/Adler-Checksum-Calculation
        /// Modified extensively for better usability in this project
        /// </summary>
        public static class AdlerChecksum
        {
            // parameters

            #region

            /// <summary>
            /// AdlerBase is Adler-32 checksum algorithm parameter.
            /// </summary>
            public const uint AdlerBase = 0xFFF1;

            /// <summary>
            /// AdlerStart is Adler-32 checksum algorithm parameter.
            /// </summary>
            public const uint AdlerStart = 0x0001;

            /// <summary>
            /// AdlerBuff is Adler-32 checksum algorithm parameter.
            /// </summary>
            public const uint AdlerBuff = 0x0400;

            #endregion

            /// <summary>
            /// Calculate Adler-32 checksum for buffer
            /// </summary>
            /// <param name="bytesBuff">Bites array for checksum calculation</param>
            /// <param name="unAdlerCheckSum">Checksum start value (default=1)</param>
            /// <returns>Returns checksum if the checksum values is successfully calculated, otherwise null</returns>
            public static uint? MakeForBuff(byte[] bytesBuff, uint unAdlerCheckSum)
            {
                if (object.Equals(bytesBuff, null))
                {
                    return null;
                }

                var nSize = bytesBuff.GetLength(0);
                if (nSize == 0)
                {
                    return null;
                }

                var unSum1 = unAdlerCheckSum & 0xFFFF;
                var unSum2 = (unAdlerCheckSum >> 16) & 0xFFFF;
                for (var i = 0; i < nSize; i++)
                {
                    unSum1 = (unSum1 + bytesBuff[i]) % AdlerBase;
                    unSum2 = (unSum1 + unSum2) % AdlerBase;
                }

                return (unSum2 << 16) + unSum1;
            }

            /// <summary>
            /// Calculate Adler-32 checksum for buffer
            /// </summary>
            /// <param name="bytesBuff">Bites array for checksum calculation</param>
            /// <returns>Returns checksum if the checksum values is successfully calculated, otherwise null</returns>
            public static uint? MakeForBuff(byte[] bytesBuff)
            {
                return MakeForBuff(bytesBuff, AdlerStart);
            }

            /// <summary>
            /// Calculate Adler-32 checksum for file
            /// </summary>
            /// <param name="sPath">Path to file for checksum calculation</param>
            /// <returns>Returns checksum if the checksum values is successfully calculated, otherwise null</returns>
            // ReSharper disable once UnusedMember.Global
            public static uint? MakeForFile(string sPath)
            {
                var checksumValue = AdlerStart;

                try
                {
                    if (!File.Exists(sPath))
                    {
                        return null;
                    }

                    var fs = new FileStream(sPath, FileMode.Open, FileAccess.Read);

                    if (fs.Length == 0)
                    {
                        return null;
                    }

                    var bytesBuff = new byte[AdlerBuff];
                    for (uint i = 0; i < fs.Length; i++)
                    {
                        var index = i % AdlerBuff;
                        bytesBuff[index] = (byte) fs.ReadByte();
                        if ((index == AdlerBuff - 1) || (i == fs.Length - 1))
                        {
                            var checksumValueX = MakeForBuff(bytesBuff, checksumValue);
                            if (checksumValueX.HasValue)
                            {
                                checksumValue = checksumValueX.Value;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
                catch
                {
                    return null;
                }

                return checksumValue;
            }
        }
    }
}
