using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PSI_Interface.MSData.mzML
{
    /// <summary>
    /// Functions for writing a mzMLType object to file
    /// </summary>
    public class MzMLWriter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path"></param>
        /// <param name="bufferSize"></param>
        public MzMLWriter(string path, int bufferSize = 65536)
        {
            _filePath = path;
            _bufferSize = bufferSize;
            MzMLType = MzMLSchemaType.MzML;
            _mzMLType = typeof (mzMLType);
        }

        private readonly int _bufferSize;

        private XmlWriter _writer;

        private readonly Type _mzMLType;

        private readonly string _filePath;

        /// <summary>
        /// Whether to write plain mzML or IndexedMzML
        /// </summary>
        public MzMLSchemaType MzMLType { get; set; }

        /// <summary>
        /// Write the supplied object to the file specified in the constructor
        /// </summary>
        /// <param name="mzMLData"></param>
        public void Write(mzMLType mzMLData)
        {
            ConfigureWriter();
            var xRoot = new XmlRootAttribute();
            if (MzMLType == MzMLSchemaType.MzML)
            {
                xRoot.ElementName = "mzML";
            }
            xRoot.Namespace = "http://psi.hupo.org/ms/mzml";
            xRoot.IsNullable = false;
            var serializer = new XmlSerializer(_mzMLType, xRoot);
            using (_writer)
            {
                switch (MzMLType)
                {
                    case MzMLSchemaType.IndexedMzML:
                        serializer.Serialize(_writer, mzMLData);
                        break;
                    case MzMLSchemaType.MzML:
                        serializer.Serialize(_writer, mzMLData);
                        break;
                }
            }
        }

        private void ConfigureWriter()
        {
            Stream writer = new FileStream(_filePath, FileMode.Create, FileAccess.Write, FileShare.None, _bufferSize);
            if (_filePath.Trim().EndsWith(".gz", StringComparison.InvariantCultureIgnoreCase))
            {
                writer = new GZipStream(writer, CompressionMode.Compress);
            }
            var xSettings = new XmlWriterSettings
            {
                CloseOutput = true,
                NewLineChars = "\n",
                Indent = true,
                Encoding = Encoding.UTF8
            };
            //xSettings.WriteEndDocumentOnClose = false; // This will be necessary to properly output a checksum for indexedmzML.
            _writer = XmlWriter.Create(new StreamWriter(writer, Encoding.UTF8, _bufferSize), xSettings);
        }
    }
}
