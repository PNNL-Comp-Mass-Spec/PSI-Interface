using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PSI_Interface.MSData.mzML
{
    /// <summary>
    /// Read a mzML file into a mzMLType object
    /// </summary>
    public class MzMLReader
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path">.mzML file path</param>
        /// <param name="bufferSize">Buffer size, in bytes</param>
        public MzMLReader(string path, int bufferSize = 65536)
        {
            _filePath = path;
            _bufferSize = bufferSize;
            MzMLType = MzMLSchemaType.IndexedMzML;
            ConfigureReader();
        }

        private readonly int _bufferSize;

        private Type _mzMLType;

        private readonly string _filePath;

        private Stream _reader;

        /// <summary>
        /// Schema of the file being read
        /// </summary>
        /// <remarks>
        /// The constructor initially sets this to IndexedMzML, but the call to ConfigureReader() will auto-change this to MzML if the file does not have an index
        /// </remarks>
        public MzMLSchemaType MzMLType { get; private set; }

        /// <summary>
        /// Read the file specified in the constructor, and return the object
        /// </summary>
        public mzMLType Read()
        {
            if (_reader?.CanRead != true)
            {
                throw new Exception("File has already been read!");
            }

            var xRoot = new XmlRootAttribute();

            if (MzMLType == MzMLSchemaType.MzML)
            {
                xRoot.ElementName = "mzML";
            }

            xRoot.Namespace = "http://psi.hupo.org/ms/mzml";
            xRoot.IsNullable = false;
            var serializer = new XmlSerializer(_mzMLType, xRoot);
            var mzMLData = new mzMLType();

            using (_reader)
            {
                switch (MzMLType)
                {
                    case MzMLSchemaType.IndexedMzML:
                        var imzML = (indexedmzML) serializer.Deserialize(_reader);
                        mzMLData = imzML.mzML;
                        break;
                    case MzMLSchemaType.MzML:
                        mzMLData = (mzMLType) serializer.Deserialize(_reader);
                        break;
                }
            }

            _reader.Close();
            _reader.Dispose();

            return mzMLData;
        }

        private void ConfigureReader()
        {
            var sourceFile = new FileInfo(_filePath);

            if (!sourceFile.Exists)
                throw new FileNotFoundException(".mzML file not found", _filePath);

            _reader = new FileStream(sourceFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, _bufferSize);
            // Temp reader to determine mzML schema type - indexed or not
            Stream tempReader = new FileStream(sourceFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, _bufferSize);

            if (sourceFile.Name.Trim().EndsWith(".gz"))
            {
                _reader = new GZipStream(_reader, CompressionMode.Decompress);
                tempReader = new GZipStream(tempReader, CompressionMode.Decompress);
            }

            var xSettings = new XmlReaderSettings { IgnoreWhitespace = true };

            // Note: By default, XmlReaderSettings specifies to not close the input stream, so make sure we close the stream when done.
            using (var streamReader = new StreamReader(tempReader, Encoding.UTF8, true, _bufferSize))
            using (var reader = XmlReader.Create(streamReader, xSettings))
            {
                reader.MoveToContent();
                switch (reader.Name)
                {
                    case "mzML":
                        MzMLType = MzMLSchemaType.MzML;
                        _mzMLType = typeof (mzMLType);
                        break;
                    case "indexedmzML":
                        MzMLType = MzMLSchemaType.IndexedMzML;
                        _mzMLType = typeof (indexedmzML);
                        break;
                }
            }
        }
    }
}
