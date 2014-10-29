using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PSI_Interface.IdentData.mzIdentML
{
	public class MzIdentMLReader
	{
		public MzIdentMLReader(string path, int bufferSize = 65536)
		{
			_filePath = path;
			_bufferSize = bufferSize;
			ConfigureReader();
		}

		private int _bufferSize = 16384;

		private readonly string _filePath;

		private bool _hasRead = false;

		private Stream _reader;

		public MzIdentMLType Read()
		{
			if (_hasRead)
			{
				throw new Exception("File has already been read!");
			}
			var xRoot = new XmlRootAttribute();
			xRoot.ElementName = "MzIdentML";
			xRoot.Namespace = "http://psidev.info/psi/pi/mzIdentML/1.1";
			xRoot.IsNullable = false;
			var serializer = new XmlSerializer(typeof(MzIdentMLType), xRoot);
			MzIdentMLType identData = (MzIdentMLType) serializer.Deserialize(_reader);
			return identData;
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
			//var xSettings = new XmlReaderSettings { IgnoreWhitespace = true };
			//XmlReader reader = XmlReader.Create(new StreamReader(tempReader, System.Text.Encoding.UTF8, true, _bufferSize), xSettings);
			//using (reader)
			//{
			//	reader.MoveToContent();
			//	switch (reader.Name)
			//	{
			//		case "mzML":
			//			MzMLType = MzMLSchemaType.MzML;
			//			_mzMLType = typeof (mzMLType);
			//			break;
			//		case "indexedmzML":
			//			MzMLType = MzMLSchemaType.IndexedMzML;
			//			_mzMLType = typeof (indexedmzML);
			//			break;
			//	}
			//}
		}
	}
}
