using System;
using System.Collections.Generic;

namespace CV_Generator.OBO_Objects
{
    public class OBO_Term
    {
        public OBO_Term(List<KeyValuePair<string, string>> data = null)
        {
            Id_Value = int.MinValue;

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
                    cleaned = cleaned.Substring(0, Def.IndexOf("[", StringComparison.Ordinal)).Trim();
                }
                cleaned = cleaned.Replace('"', ' ').Trim();
                return cleaned;
            }
        }

        // Derived from required
        public string Id_Namespace { get; private set; }
        public int Id_Value { get; private set; }

        public void ParseId()
        {
            if (string.IsNullOrWhiteSpace(Id) || Id.Equals("??:0000000"))
            {
                Id_Namespace = "";
                Id_Value = -1;
                return;
            }
            var split = Id.Split(':');
            Id_Namespace = split[0];
            Id_Value = int.Parse(split[1].Trim('_',' '));
        }

        private string id;

        // Required
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                ParseId();
            }
        }
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
        // Tags that are not allowed with IsObsolete == true:
        public readonly List<string> IsA = new List<string>();
        public readonly List<string> IntersectionOf = new List<string>();
        public readonly List<string> UnionOf = new List<string>();
        public readonly List<string> DisjointFrom = new List<string>();
        public readonly List<string> Relationship = new List<string>();
        // public string InverseOf; only allowed in Typedef
        public bool IsObsolete;
        // Tags that are only allowed with IsObsolete == true:
        public readonly List<string> ReplacedBy = new List<string>();
        public readonly List<string> Consider = new List<string>();
        // UseTerm deprecated
        public string CreatedBy;
        public string CreationDate;
    }
}
