using System;
using System.Collections.Generic;

namespace CV_Generator.OBO_Objects
{
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
                var pos = Def.IndexOf("[", StringComparison.Ordinal);
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
}
