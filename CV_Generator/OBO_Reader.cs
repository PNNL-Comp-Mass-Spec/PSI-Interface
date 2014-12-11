using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CV_Generator
{
	public class OBO_Reader
	{
		public OBO_File FileData;
		public List<OBO_File> ImportedFileData = new List<OBO_File>();

		public void Read(string url)
		{
			var fileData = (new WebClient()).DownloadString(url);
			FileData = new OBO_File(url);
			using (var reader = new StringReader(fileData))
			{
				string line = reader.ReadLine();
				while (string.IsNullOrWhiteSpace(line) && line != null)
				{
					line = reader.ReadLine();
				}

				string type = "header";
				var data = new List<KeyValuePair<string, string>>();
				while (reader.Peek() != -1 && !string.IsNullOrWhiteSpace(line))
				{
					if (line.StartsWith("["))
					{
						type = line;

						line = reader.ReadLine();
						while (string.IsNullOrWhiteSpace(line) && line != null)
						{
							line = reader.ReadLine();
						}
					}
					data.Clear();
					while (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("["))
					{
						var keyStop = line.IndexOf(':');
						var valueStart = line[keyStop + 1] == ' ' ? keyStop + 2 : keyStop + 1;
						data.Add(new KeyValuePair<string, string>(line.Substring(0, keyStop), line.Substring(valueStart)));

						line = reader.ReadLine();
						while (string.IsNullOrWhiteSpace(line) && line != null)
						{
							line = reader.ReadLine();
						}
					}

					switch (type.ToLower())
					{
						case "header":
							FileData.Header = new OBO_File.OBO_Header(data);
							break;
						case "[term]":
							FileData.Terms.Add(new OBO_File.OBO_Term(data));
							break;
						case "[typedef]":
							FileData.Typedefs.Add(new OBO_File.OBO_Typedef(data));
							break;
						case "[instance]":
							FileData.Instances.Add(new OBO_File.OBO_Instance(data));
							break;
					}
				}
			}

			foreach (var import in FileData.Header.Import)
			{
				var reader = new OBO_Reader();
				reader.Read(import);
				ImportedFileData.Add(reader.FileData);
				ImportedFileData.AddRange(reader.ImportedFileData);
			}
		}
	}
}
