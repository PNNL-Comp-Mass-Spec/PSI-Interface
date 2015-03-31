using System;
using zlib;

namespace PSI_Interface.MSData
{
    public static class Base64Conversion
    {
        public static double[] DecodeString(string encoded, int bytesPerValue, int expectedLength, bool isCompressed)
        {
            if (bytesPerValue % 4 != 0)
            {
                //throw NotSupportedException("Invalid bitsPerValue");
                return null;
            }
            return DecodeBytes(Convert.FromBase64String(encoded), bytesPerValue, expectedLength, isCompressed);
        }

        public static string EncodeString(double[] data, int bytesPerValue, bool compress)
        {
            return Convert.ToBase64String(EncodeBytes(data, bytesPerValue, compress));
        }

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
            for (int i = 0; i < bytes.Length; i += bytesPerValue)
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
            for (int i = 0; i < data.Length; i++)
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
                    Array.Copy(BitConverter.GetBytes((Single)(data[i])), 0, bytes, i * bytesPerValue, bytesPerValue);
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
                int finalSize;
                bytes = Zlib.CompressZLib(bytes, out finalSize);
            }
            return bytes;
        }
    }
}
