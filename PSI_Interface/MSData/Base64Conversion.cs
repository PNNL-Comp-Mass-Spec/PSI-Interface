using System;

namespace PSI_Interface.MSData
{
    /// <summary>
    /// Collection of functions to convert between Base64 and arrays of doubles or bytes
    /// </summary>
    public static class Base64Conversion
    {
        /// <summary>
        /// Decode a base-64 string to an array of doubles
        /// </summary>
        /// <param name="encoded">base-64 string</param>
        /// <param name="bytesPerValue">number of bytes per value, generally 4 (float) or 8 (double)</param>
        /// <param name="expectedLength">expected number of doubles in the array</param>
        /// <param name="isCompressed">Whether the base64 data is compressed</param>
        /// <returns>decoded base-64 data</returns>
        public static double[] DecodeString(string encoded, int bytesPerValue, int expectedLength, bool isCompressed)
        {
            if (bytesPerValue % 4 != 0)
            {
                //throw new NotSupportedException("Invalid bitsPerValue");
                return null;
            }
            return DecodeBytes(Convert.FromBase64String(encoded), bytesPerValue, expectedLength, isCompressed);
        }

        /// <summary>
        /// Encode an array of doubles as a base-64 string
        /// </summary>
        /// <param name="data">the array to encode as base-64</param>
        /// <param name="bytesPerValue">number of bytes per value, generally 4 (float) or 8 (double)</param>
        /// <param name="compress">if the data should be compressed</param>
        /// <returns>base-64 encoded data</returns>
        public static string EncodeString(double[] data, int bytesPerValue, bool compress)
        {
            return Convert.ToBase64String(EncodeBytes(data, bytesPerValue, compress));
        }

        /// <summary>
        /// Converts an array of bytes, possibly compressed, to an array of doubles
        /// </summary>
        /// <param name="bytes">the array of bytes to convert to an array of doubles</param>
        /// <param name="bytesPerValue">number of bytes per value, generally 4 (float) or 8 (double)</param>
        /// <param name="expectedLength">expected number of doubles in the array</param>
        /// <param name="isCompressed">Whether the byte array data is compressed</param>
        /// <returns>decoded byte data</returns>
        public static double[] DecodeBytes(byte[] bytes, int bytesPerValue, int expectedLength, bool isCompressed)
        {
            if (isCompressed)
            {
                bytes = Zlib.DecompressZLib(bytes, expectedLength * bytesPerValue);
            }
            if (bytes.Length % bytesPerValue != 0 || bytes.Length / bytesPerValue != expectedLength)
            {
                // We need to fail out.
            }
            //byte[] oneNumber = new byte[dataSize];
            //bool swapBytes = true;
            var data = new double[expectedLength];
            for (var i = 0; i < bytes.Length; i += bytesPerValue)
            {
                // mzML binary data should always be Little Endian. Some other data formats may use Big Endian, which would require a byte swap
                //Array.Copy(bytes, i, oneNumber, 0, dataSize);
                //if (swapBytes)
                //{
                //  Array.Reverse(oneNumber);
                //}
                if (bytesPerValue == 4)
                {
                    //bda.Data[i / dataSize] = BitConverter.ToSingle(oneNumber, 0);
                    data[i / bytesPerValue] = BitConverter.ToSingle(bytes, i);
                }
                else if (bytesPerValue == 8)
                {
                    //bda.Data[i / dataSize] = BitConverter.ToDouble(oneNumber, 0);
                    data[i / bytesPerValue] = BitConverter.ToDouble(bytes, i);
                }
            }
            return data;
        }

        /// <summary>
        /// Converts an array of bytes (possibly compressed) to an array of doubles
        /// </summary>
        /// <param name="data">the array to convert to an array of bytes (possibly compressed)</param>
        /// <param name="bytesPerValue">number of bytes per value, generally 4 (float) or 8 (double)</param>
        /// <param name="compress">if the data should be compressed</param>
        /// <returns>byte encoded data</returns>
        public static byte[] EncodeBytes(double[] data, int bytesPerValue, bool compress)
        {
            //if (bytesPerValue % 4 != 0)
            if (bytesPerValue != 4 && bytesPerValue != 8)
            {
                //throw NotSupportedException("Invalid bitsPerValue");
                return null;
            }
            //byte[] oneNumber = new byte[dataSize];
            //bool swapBytes = true;
            var bytes = new byte[data.Length * bytesPerValue];
            for (var i = 0; i < data.Length; i++)
            {
                // mzML binary data should always be Little Endian. Some other data formats may use Big Endian, which would require a byte swap
                //Array.Copy(bytes, i, oneNumber, 0, dataSize);
                //if (swapBytes)
                //{
                //  Array.Reverse(oneNumber);
                //}
                if (bytesPerValue == 4)
                {
                    //bda.Data[i / dataSize] = BitConverter.ToSingle(oneNumber, 0);
                    //data[i / bytesPerValue] = BitConverter.ToSingle(bytes, i);
                    Array.Copy(BitConverter.GetBytes((Single)data[i]), 0, bytes, i * bytesPerValue, bytesPerValue);
                }
                else if (bytesPerValue == 8)
                {
                    //bda.Data[i / dataSize] = BitConverter.ToDouble(oneNumber, 0);
                    //data[i / bytesPerValue] = BitConverter.ToDouble(bytes, i);
                    Array.Copy(BitConverter.GetBytes(data[i]), 0, bytes, i * bytesPerValue, bytesPerValue);
                }
            }
            //byte[] bytes = Convert.FromBase64String(encoded);
            if (compress)
            {
                bytes = Zlib.CompressZLib(bytes, out _);
            }
            return bytes;
        }
    }
}
