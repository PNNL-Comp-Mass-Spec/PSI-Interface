using System;
using System.Collections.Generic;

namespace CV_Generator.OBO_Objects
{
    public class OBO_Instance
    {
        // Ignore Spelling: OBO

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
        public bool IsObsolete;
        // Tags that are not allowed with IsObsolete == true:
        // Tags that are only allowed with IsObsolete == true:
        public readonly List<string> ReplacedBy = new List<string>();
        public readonly List<string> Consider = new List<string>();
        // UseTerm deprecated
    }
}
