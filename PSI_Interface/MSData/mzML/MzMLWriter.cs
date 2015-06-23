using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PSI_Interface.MSData.mzML
{
    public class MzMLWriter
    {
        public MzMLWriter(string path, int bufferSize = 65536)
        {
            _filePath = path;
            _bufferSize = bufferSize;
            MzMLType = MzMLSchemaType.MzML;
            _mzMLType = typeof (mzMLType);
        }

        private int _bufferSize = 16384;

        private XmlWriter _writer;

        private Type _mzMLType;

        private readonly string _filePath;

        public MzMLSchemaType MzMLType { get; set; }

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
            if (_filePath.EndsWith(".gz"))
            {
                writer = new GZipStream(writer, CompressionMode.Compress);
            }
            var xSettings = new XmlWriterSettings();
            xSettings.CloseOutput = true;
            xSettings.NewLineChars = "\n";
            xSettings.Indent = true;
            xSettings.Encoding = Encoding.UTF8;
            //xSettings.WriteEndDocumentOnClose = false; // This will be necessary to properly output a checksum for indexedmzML.
            _writer = XmlWriter.Create(new StreamWriter(writer, Encoding.UTF8, _bufferSize), xSettings);
        }
    }
}
