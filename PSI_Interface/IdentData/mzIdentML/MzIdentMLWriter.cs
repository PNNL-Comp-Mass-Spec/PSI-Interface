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
	public class MzIdentMLWriter
	{
		public MzIdentMLWriter(string path, int bufferSize = 65536)
		{
			_filePath = path;
			_bufferSize = bufferSize;
		}

		private int _bufferSize = 16384;

		private XmlWriter _writer;

		private readonly string _filePath;

		public void Write(MzIdentMLType identData)
		{
			ConfigureWriter();
			var xRoot = new XmlRootAttribute();
			xRoot.ElementName = "MzIdentML";
			xRoot.Namespace = "http://psidev.info/psi/pi/mzIdentML/1.1";
			xRoot.IsNullable = false;
			var serializer = new XmlSerializer(typeof(MzIdentMLType), xRoot);
			using (_writer)
			{
				serializer.Serialize(_writer, identData);
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
			_writer = XmlWriter.Create(new StreamWriter(writer, Encoding.UTF8, _bufferSize), xSettings);
		}
	}
}
