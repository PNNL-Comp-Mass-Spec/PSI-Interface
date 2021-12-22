using System;
using System.IO;
using System.IO.Compression;
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
        /// <param name="path"></param>
        /// <param name="bufferSize"></param>
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

        private readonly bool _hasRead = false;

        private Stream _reader;

        /// <summary>
        /// Schema of the file being read
        /// </summary>
        public MzMLSchemaType MzMLType { get; private set; }

        /// <summary>
        /// Read the file specified in the constructor, and return the object
        /// </summary>
        public mzMLType Read()
        {
            if (_hasRead)
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
            var reader = XmlReader.Create(new StreamReader(tempReader, System.Text.Encoding.UTF8, true, _bufferSize), xSettings);
            using (reader)
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
