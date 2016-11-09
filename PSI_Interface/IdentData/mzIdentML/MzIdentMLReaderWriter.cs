using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PSI_Interface.IdentData.mzIdentML
{
    /// <summary>
    /// Class for reading an MzIdentML file into MzIdentMLType group objects
    /// </summary>
    public static class MzIdentMlReaderWriter
    {
        /// <summary>
        /// Read the mzid file into MzIdentMLType objects
        /// </summary>
        /// <returns></returns>
        public static MzIdentMLType Read(string filePath, int bufferSize = 65536)
        {
            var xRoot = new XmlRootAttribute()
            {
                ElementName = "MzIdentML",
                Namespace = "http://psidev.info/psi/pi/mzIdentML/1.1",
                IsNullable = false,
            };
            var serializer = new XmlSerializer(typeof(MzIdentMLType), xRoot);
            using (var reader = CreateReader(filePath, bufferSize))
            {
                return (MzIdentMLType)serializer.Deserialize(reader);
            }
        }

        private static Stream CreateReader(string filePath, int bufferSize)
        {
            var sourceFile = new FileInfo(filePath);
            if (!sourceFile.Exists)
                throw new FileNotFoundException(".mzID file not found", filePath);

            Stream reader = new FileStream(sourceFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, bufferSize);
            if (sourceFile.Name.Trim().EndsWith(".gz", StringComparison.InvariantCultureIgnoreCase))
            {
                reader = new GZipStream(reader, CompressionMode.Decompress);
            }
            return reader;
        }
        /// <summary>
        /// Write the provided data to the file
        /// </summary>
        /// <param name="identData"></param>
        /// <param name="filePath">Path to file to be written, with extension of .mzid[.gz]</param>
        /// <param name="bufferSize">File stream buffer size</param>
        public static void Write(MzIdentMLType identData, string filePath, int bufferSize = 65536)
        {
            var xRoot = new XmlRootAttribute()
            {
                ElementName = "MzIdentML",
                Namespace = "http://psidev.info/psi/pi/mzIdentML/1.1",
                IsNullable = false,
            };
            var serializer = new XmlSerializer(typeof(MzIdentMLType), xRoot);
            using (var writer = CreateWriter(filePath, bufferSize))
            {
                serializer.Serialize(writer, identData);
            }
        }

        private static XmlWriter CreateWriter(string filePath, int bufferSize)
        {
            Stream writer = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite, bufferSize);
            if (filePath.Trim().EndsWith(".gz", StringComparison.InvariantCultureIgnoreCase))
            {
                writer = new GZipStream(writer, CompressionMode.Compress);
            }
            var utf8EncodingNoMark = new UTF8Encoding(false); // DO NOT ADD THE BYTE ORDER MARK!!!
            var xSettings = new XmlWriterSettings()
            {
                CloseOutput = true,
                NewLineChars = "\n",
                Indent = true,
                Encoding = utf8EncodingNoMark,
            };
            return XmlWriter.Create(new StreamWriter(writer, utf8EncodingNoMark, bufferSize), xSettings);
        }
    }
}
