using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using CV_Generator.OBO_Objects;

namespace CV_Generator
{
    public class OBO_Reader
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ignoredOntologyImports">imported ontology file names to ignore/exclude</param>
        /// <param name="ignoredTermNamespaces">term namespaces that should be excluded from the import (e.g., because they are copied from another ontology that is separately included)</param>
        public OBO_Reader(IEnumerable<string> ignoredOntologyImports = null, IEnumerable<string> ignoredTermNamespaces = null)
        {
            _ignoredOntologyImports = new SortedSet<string>(ignoredOntologyImports ?? Enumerable.Empty<string>());
            _ignoredTermNamespaces = new SortedSet<string>(ignoredTermNamespaces ?? Enumerable.Empty<string>());
        }

        public OBO_File FileData { get; private set; }

        /// <summary>
        /// Ignored ontology imports (because they are either never used, or because there is a problem with importing their terms)
        /// </summary>
        private readonly SortedSet<string> _ignoredOntologyImports = new SortedSet<string> { "pato.obo", "stato.owl" };

        /// <summary>
        /// Ignored term namespaces (because they are either never used, or because the terms are copied from another ontology that is separately included)
        /// </summary>
        private readonly SortedSet<string> _ignoredTermNamespaces;

        public readonly List<OBO_File> ImportedFileData = new List<OBO_File>();

        public void Read(string url)
        {
            Console.WriteLine("Downloading data from " + url);

            var fileData = (new WebClient()).DownloadString(url);
            FileData = new OBO_File(url);
            using (var reader = new StringReader(fileData))
            {
                var line = reader.ReadLine();
                while (string.IsNullOrWhiteSpace(line) && line != null)
                {
                    line = reader.ReadLine();
                }

                var type = "header";
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
                            FileData.Header = new OBO_Header(data);
                            break;
                        case "[term]":
                            var term = new OBO_Term(data);
                            if (FileData.Terms.ContainsKey(term.Id))
                            {
                                Console.WriteLine("Warning: Duplicate term id found");
                                Console.WriteLine("\tFirst term: \t\"" + FileData.Terms[term.Id].Id + "\": \"" +
                                                  FileData.Terms[term.Id].Def + "\".");
                                Console.WriteLine("\tConflict term: \t\"" + term.Id + "\": \"" + term.Def + "\".");
                                Console.WriteLine("\tChanging conflict id to \"" + term.Id + "_\"");
                                term.Id += "_";
                            }

                            if (!_ignoredTermNamespaces.Contains(term.Id_Namespace))
                            {
                                FileData.Terms.Add(term.Id, term);
                            }

                            break;
                        case "[typedef]":
                            var typeDef = new OBO_Typedef(data);
                            FileData.Typedefs.Add(typeDef.Id, typeDef);
                            break;
                        case "[instance]":
                            var instance = new OBO_Instance(data);
                            FileData.Instances.Add(instance.Id, instance);
                            break;
                    }
                }
            }

            if (FileData.Terms.Count > 0)
            {
                var namespaces = FileData.Terms.Values.Select(x => x.Id_Namespace).Distinct();
                foreach (var ns in namespaces)
                {
                    if (FileData.IsGeneratedId)
                    {
                        FileData.Id = ns;
                    }
                    else if (!FileData.Id.Equals(ns))
                    {
                        FileData.AdditionalNamespaces.Add(ns);
                    }
                }
            }

            foreach (var import in FileData.Header.Import)
            {
                var urlParts = import.Split('/');

                if (_ignoredOntologyImports.Contains(urlParts.LastOrDefault()))
                {
                    Console.WriteLine("Ignoring ontology " + import);
                    continue;
                }

                var reader = new OBO_Reader();
                reader.Read(import);
                ImportedFileData.Add(reader.FileData);
                ImportedFileData.AddRange(reader.ImportedFileData);
            }
        }
    }
}
