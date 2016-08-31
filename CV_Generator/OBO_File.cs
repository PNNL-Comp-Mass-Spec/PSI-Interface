using System;
using System.Collections.Generic;

namespace CV_Generator
{
    public class OBO_File
    {
        private OBO_Header _header;

        public OBO_Header Header
        {
            get { return _header; }
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

        public string Url { get; private set; }
        public string Name { get; private set; }
        private string _id;

        public string Id
        {
            get { return _id; }
            set
            {
                if (IsGeneratedId)
                {
                    _id = GetAvailableId(value);
                    IsGeneratedId = false;
                }
            }
        }

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
            string filename = Url.Substring(Url.LastIndexOf("/") + 1);
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
                case "unimod.obo":
                    Name = "UNIMOD";
                    _id = GetAvailableId("UNIMOD");
                    break;
                default:
                    Name = filename.Substring(0, filename.LastIndexOf("."));
                    _id = GetAvailableId(Name.ToUpper());
                    IsGeneratedId = true;
                    break;
            }
        }

        private string GetAvailableId(string desiredId)
        {
            string finalId = desiredId;
            // Id safety mechanism: If the Id already exists, add a number and continue.
            while (Ids.ContainsKey(finalId))
            {
                Ids[desiredId]++;
                finalId = finalId + Ids[desiredId];
            }
            Ids.Add(finalId, 0); // Add it to the dictionary
            return finalId;
        }

        public class OBO_Header
        {
            public OBO_Header(List<KeyValuePair<string, string>> data = null)
            {
                if (data != null)
                {
                    foreach (var datum in data)
                    {
                        switch (datum.Key.ToLower())
                        {
                            case "format-version":
                                FormatVersion = datum.Value;
                                break;
                            case "data-version":
                                DataVersion = datum.Value;
                                break;
                            case "version":
                                Version = datum.Value;
                                break;
                            case "date":
                                Date = datum.Value;
                                break;
                            case "saved-by":
                                SavedBy = datum.Value;
                                break;
                            case "auto-generated-by":
                                AutoGeneratedBy = datum.Value;
                                break;
                            case "import":
                                Import.Add(datum.Value);
                                break;
                            case "subsetdef":
                                SubsetDef.Add(datum.Value);
                                break;
                            case "synonymtypedef":
                                SynonymTypeDef.Add(datum.Value);
                                break;
                            case "default-namespace":
                                DefaultNamespace = datum.Value;
                                break;
                            case "idspace":
                                IdSpace.Add(datum.Value);
                                break;
                            case "default-relationship-id-prefix":
                                DefaultRelationshipIdPrefix.Add(datum.Value);
                                break;
                            case "id-mapping":
                                IdMapping.Add(datum.Value);
                                break;
                            case "remark":
                                Remark.Add(datum.Value);
                                break;
                            default:
                                Other.Add(datum);
                                break;
                        }
                    }
                }
            }

            // Required
            public string FormatVersion;

            // Optional
            public string DataVersion;
            public string Version { get { return DataVersion; } set { DataVersion = value; } }
            public string Date;
            public string SavedBy;
            public string AutoGeneratedBy;

            public readonly List<string> Import = new List<string>();
            public readonly List<string> SubsetDef = new List<string>();
            public readonly List<string> SynonymTypeDef = new List<string>();
            public string DefaultNamespace;
            public readonly List<string> Remark = new List<string>();
            public readonly List<string> IdSpace = new List<string>();
            public readonly List<string> DefaultRelationshipIdPrefix = new List<string>();
            public readonly List<string> IdMapping = new List<string>();
            public readonly List<KeyValuePair<string, string>> Other = new List<KeyValuePair<string, string>>(); 
        }

        public class OBO_Term
        {
            public OBO_Term(List<KeyValuePair<string, string>> data = null)
            {
                if (data != null)
                {
                    foreach (var datum in data)
                    {
                        switch (datum.Key.ToLower())
                        {
                            case "id":
                                Id = datum.Value;
                                break;
                            case "is_anonymous":
                                IsAnonymous = Convert.ToBoolean(datum.Value);
                                break;
                            case "name":
                                Name = datum.Value;
                                break;
                            case "namespace":
                                Namespace = datum.Value;
                                break;
                            case "alt_id":
                                AltId.Add(datum.Value);
                                break;
                            case "def":
                                Def = datum.Value;
                                break;
                            case "comment":
                                Comment = datum.Value;
                                break;
                            case "subset":
                                Subset.Add(datum.Value);
                                break;
                            case "synonym":
                                Synonym.Add(datum.Value);
                                break;
                            case "xref":
                                XRef.Add(datum.Value);
                                break;
                            case "is_a":
                                IsA.Add(datum.Value);
                                break;
                            case "intersection_of":
                                IntersectionOf.Add(datum.Value);
                                break;
                            case "union_of":
                                UnionOf.Add(datum.Value);
                                break;
                            case "disjoint_from":
                                DisjointFrom.Add(datum.Value);
                                break;
                            case "relationship":
                                Relationship.Add(datum.Value);
                                break;
                            case "is_obsolete":
                                IsObsolete = Convert.ToBoolean(datum.Value);
                                break;
                            case "replaced_by":
                                ReplacedBy.Add(datum.Value);
                                break;
                            case "consider":
                                Consider.Add(datum.Value);
                                break;
                            case "created_by":
                                CreatedBy = datum.Value;
                                break;
                            case "creation_date":
                                CreationDate = datum.Value;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            // Added for ease of use
            public string EnumName;
            public string DefShort
            {
                get
                {
                    if (string.IsNullOrWhiteSpace(Def))
                    {
                        return string.Empty;
                    }
                    var cleaned = Def;
                    if (cleaned.Contains("["))
                    {
                        cleaned = cleaned.Substring(0, Def.IndexOf("[")).Trim();
                    }
                    cleaned = cleaned.Replace('"', ' ').Trim();
                    return cleaned;
                }
            }

            // Required
            public string Id;
            public string Name;

            // Optional
            public bool IsAnonymous = false;
            public string Namespace;
            public readonly List<string> AltId = new List<string>();
            public string Def; // zero or one
            public string Comment; // zero or one
            public readonly List<string> Subset = new List<string>();
            public readonly List<string> Synonym = new List<string>();
            // ExactSynonym deprecated
            // NarrowSynonym deprecated
            // BroadSynonym deprecated
            public readonly List<string>  XRef = new List<string>();
            // XRefAnalog deprecated
            // XRefUnknown deprecated
            // Tags that are not allowed with IsObsolete == true:
            public readonly List<string> IsA = new List<string>();
            public readonly List<string> IntersectionOf = new List<string>();
            public readonly List<string> UnionOf = new List<string>();
            public readonly List<string> DisjointFrom = new List<string>();
            public readonly List<string> Relationship = new List<string>();
            // public string InverseOf; only allowed in Typedef
            public bool IsObsolete = false;
            // Tags that are only allowed with IsObsolete == true:
            public readonly List<string> ReplacedBy = new List<string>();
            public readonly List<string> Consider = new List<string>();
            // UseTerm deprecated
            public string CreatedBy;
            public string CreationDate;
        }

        public class OBO_Typedef
        {
            public OBO_Typedef(List<KeyValuePair<string, string>> data = null)
            {
                if (data != null)
                {
                    foreach (var datum in data)
                    {
                        switch (datum.Key.ToLower())
                        {
                            case "id":
                                Id = datum.Value;
                                break;
                            case "is_anonymous":
                                IsAnonymous = Convert.ToBoolean(datum.Value);
                                break;
                            case "name":
                                Name = datum.Value;
                                break;
                            case "namespace":
                                Namespace = datum.Value;
                                break;
                            case "alt_id":
                                AltId.Add(datum.Value);
                                break;
                            case "def":
                                Def = datum.Value;
                                break;
                            case "comment":
                                Comment = datum.Value;
                                break;
                            case "subset":
                                Subset.Add(datum.Value);
                                break;
                            case "synonym":
                                Synonym.Add(datum.Value);
                                break;
                            case "xref":
                                XRef.Add(datum.Value);
                                break;
                            case "domain":
                                Domain = datum.Value;
                                break;
                            case "range":
                                Range = datum.Value;
                                break;
                            case "is_anti_symmetric":
                                IsAntiSymmetric = Convert.ToBoolean(datum.Value);
                                break;
                            case "is_cyclic":
                                IsCyclic = Convert.ToBoolean(datum.Value);
                                break;
                            case "is_reflexive":
                                IsReflexive = Convert.ToBoolean(datum.Value);
                                break;
                            case "is_symmetric":
                                IsSymmetric = Convert.ToBoolean(datum.Value);
                                break;
                            case "is_transitive":
                                IsTransitive = Convert.ToBoolean(datum.Value);
                                break;
                            case "is_a":
                                IsA.Add(datum.Value);
                                break;
                            case "inverse_of":
                                InverseOf = datum.Value;
                                break;
                            case "transitive_over":
                                TransitiveOver.Add(datum.Value);
                                break;
                            case "relationship":
                                Relationship.Add(datum.Value);
                                break;
                            case "is_obsolete":
                                IsObsolete = Convert.ToBoolean(datum.Value);
                                break;
                            case "replaced_by":
                                ReplacedBy.Add(datum.Value);
                                break;
                            case "consider":
                                Consider.Add(datum.Value);
                                break;
                            case "created_by":
                                CreatedBy = datum.Value;
                                break;
                            case "creation_date":
                                CreationDate = datum.Value;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            // Added for ease of use
            public string EnumName;
            public string DefShort
            {
                get
                {
                    if (string.IsNullOrWhiteSpace(Def))
                    {
                        return string.Empty;
                    }
                    var pos = Def.IndexOf("[");
                    if (pos >= 0)
                    {
                        return Def.Substring(0, pos).Trim();
                    }
                    return Def.Trim();
                }
            }

            // Required
            public string Id;
            public string Name;

            // Optional
            public bool IsAnonymous;
            public string Namespace;
            public readonly List<string> AltId = new List<string>();
            public string Def; // zero or one
            public string Comment; // zero or one
            public readonly List<string> Subset = new List<string>();
            public readonly List<string> Synonym = new List<string>();
            // ExactSynonym deprecated
            // NarrowSynonym deprecated
            // BroadSynonym deprecated
            public readonly List<string> XRef = new List<string>();
            // XRefAnalog deprecated
            // XRefUnknown deprecated
            public string Domain;
            public string Range;
            public bool IsAntiSymmetric = false;
            public bool IsCyclic = false;
            public bool IsReflexive = false;
            public bool IsSymmetric = false;
            public bool IsTransitive = false;
            // Tags that are not allowed with IsObsolete == true:
            public readonly List<string> IsA = new List<string>();
            public string InverseOf; // zero or one; // only allowed in Typedef
            public readonly List<string> TransitiveOver = new List<string>();
            public readonly List<string> Relationship = new List<string>();
            public bool IsObsolete = false;
            // Tags that are only allowed with IsObsolete == true:
            public readonly List<string> ReplacedBy = new List<string>();
            public readonly List<string> Consider = new List<string>();
            // UseTerm deprecated
            public string CreatedBy;
            public string CreationDate;

            public bool IsMetadataTag = false;
        }

        public class OBO_Instance
        {
            public OBO_Instance(List<KeyValuePair<string, string>> data = null)
            {
                if (data != null)
                {
                    foreach (var datum in data)
                    {
                        switch (datum.Key.ToLower())
                        {
                            case "id":
                                Id = datum.Value;
                                break;
                            case "is_anonymous":
                                IsAnonymous = Convert.ToBoolean(datum.Value);
                                break;
                            case "name":
                                Name = datum.Value;
                                break;
                            case "namespace":
                                Namespace = datum.Value;
                                break;
                            case "alt_id":
                                AltId.Add(datum.Value);
                                break;
                            case "comment":
                                Comment = datum.Value;
                                break;
                            case "synonym":
                                Synonym.Add(datum.Value);
                                break;
                            case "xref":
                                XRef.Add(datum.Value);
                                break;
                            case "instance_of":
                                InstanceOf = datum.Value;
                                break;
                            case "property_value":
                                Property_Value.Add(datum.Value);
                                break;
                            case "is_obsolete":
                                IsObsolete = Convert.ToBoolean(datum.Value);
                                break;
                            case "replaced_by":
                                ReplacedBy.Add(datum.Value);
                                break;
                            case "consider":
                                Consider.Add(datum.Value);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            // Required
            public string Id;
            public string Name;
            public string InstanceOf;

            // Optional
            public bool IsAnonymous;
            public string Namespace;
            public readonly List<string> AltId = new List<string>();
            public string Comment; // zero or one
            public readonly List<string> Synonym = new List<string>();
            // ExactSynonym deprecated
            // NarrowSynonym deprecated
            // BroadSynonym deprecated
            public readonly List<string> XRef = new List<string>();
            // XRefAnalog deprecated
            // XRefUnknown deprecated
            public readonly List<string> Property_Value = new List<string>();
            public bool IsObsolete = false;
            // Tags that are not allowed with IsObsolete == true:
            // Tags that are only allowed with IsObsolete == true:
            public readonly List<string> ReplacedBy = new List<string>();
            public readonly List<string> Consider = new List<string>();
            // UseTerm deprecated
        }
    }
}
