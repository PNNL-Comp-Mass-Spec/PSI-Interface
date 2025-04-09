using System;
using System.Collections.Generic;

namespace CV_Generator.OBO_Objects
{
    public class OBO_File
    {
        // Ignore Spelling: Namespaces, OBO, Proteomics

        private OBO_Header _header;

        public OBO_Header Header
        {
            get => _header;
            set
            {
                _header = value;

                if (!string.IsNullOrWhiteSpace(_header.DataVersion))
                {
                    Version = _header.DataVersion;
                }
                else if (!string.IsNullOrWhiteSpace(_header.Date))
                {
                    Version = _header.Date;
                }
                else
                {
                    Version = DateTime.Now.ToLongDateString();
                }
            }
        }

        public readonly Dictionary<string, OBO_Term> Terms;
        public readonly Dictionary<string, OBO_Typedef> Typedefs;
        public readonly Dictionary<string, OBO_Instance> Instances;
        private static readonly Dictionary<string, int> Ids = new Dictionary<string, int>();

        public string Url { get; }
        public string Name { get; private set; }
        private string _id;

        public string Id
        {
            get => _id;
            set
            {
                if (IsGeneratedId)
                {
                    _id = GetAvailableId(value);
                    IsGeneratedId = false;
                }
            }
        }

        public List<string> AdditionalNamespaces { get; } = new List<string>();

        public string Version { get; private set; }
        public bool IsGeneratedId { get; private set; }

        public OBO_File(string url)
        {
            Terms = new Dictionary<string, OBO_Term>();
            Typedefs = new Dictionary<string, OBO_Typedef>();
            Instances = new Dictionary<string, OBO_Instance>();
            Url = url;
            IsGeneratedId = false;
            SetNameAndId();
        }

        private void SetNameAndId()
        {
            var filename = Url.Substring(Url.LastIndexOf("/", StringComparison.Ordinal) + 1);
            switch (filename.ToLower())
            {
                case "psi-ms.obo":
                    Name = "Proteomics Standards Initiative Mass Spectrometry Ontology";
                    _id = GetAvailableId("MS");
                    break;
                case "unit.obo":
                    Name = "Unit Ontology";
                    _id = GetAvailableId("UO");
                    break;
                case "uo.obo":
                    Name = "Unit Ontology";
                    _id = GetAvailableId("UO");
                    break;
                case "quality.obo": // Old PATO obo file
                    Name = "Quality Ontology";
                    _id = GetAvailableId("PATO");
                    break;
                case "pato.obo":
                    Name = "Quality Ontology";
                    _id = GetAvailableId("PATO");
                    break;
                case "stato.owl":
                    Name = "STATO: the statistical methods ontology";
                    _id = GetAvailableId("STATO");
                    break;
                case "unimod.obo":
                    Name = "UNIMOD";
                    _id = GetAvailableId("UNIMOD");
                    break;
                default:
                    Name = filename.Substring(0, filename.LastIndexOf(".", StringComparison.Ordinal));
                    _id = GetAvailableId(Name.ToUpper());
                    IsGeneratedId = true;
                    break;
            }
        }

        private string GetAvailableId(string desiredId)
        {
            var finalId = desiredId;

            // Id safety mechanism: If the Id already exists, add a number and continue.
            while (Ids.ContainsKey(finalId))
            {
                Ids[desiredId]++;
                finalId += Ids[desiredId];
            }

            Ids.Add(finalId, 0); // Add it to the dictionary

            return finalId;
        }
    }
}
