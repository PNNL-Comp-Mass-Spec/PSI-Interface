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
	public class MzMLReader
	{
		public MzMLReader(string path, int bufferSize = 65536)
		{
			_filePath = path;
			_bufferSize = bufferSize;
			MzMLType = MzMLSchemaType.IndexedMzML;
			ConfigureReader();
		}

		private int _bufferSize = 16384;

		private Type _mzMLType;

		private readonly string _filePath;

		private bool _hasRead = false;

		private Stream _reader;

		public MzMLSchemaType MzMLType { get; private set; }

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
			mzMLType mzMLData = new mzMLType();
			using (_reader)
			{
				switch (MzMLType)
				{
					case MzMLSchemaType.IndexedMzML:
						indexedmzML imzML = (indexedmzML) serializer.Deserialize(_reader);
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
			_reader = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read, _bufferSize);
			// Temp reader to determine mzML schema type - indexed or not
			Stream tempReader = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read, _bufferSize);
			if (_filePath.EndsWith(".gz"))
			{
				_reader = new GZipStream(_reader, CompressionMode.Decompress);
				tempReader = new GZipStream(tempReader, CompressionMode.Decompress);
			}
			var xSettings = new XmlReaderSettings { IgnoreWhitespace = true };
			XmlReader reader = XmlReader.Create(new StreamReader(tempReader, System.Text.Encoding.UTF8, true, _bufferSize), xSettings);
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
