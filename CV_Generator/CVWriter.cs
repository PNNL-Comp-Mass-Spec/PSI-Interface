
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using CV_Generator.OBO_Objects;

namespace CV_Generator
{
    public class CVWriter
    {
        private OBO_File _unimod;
        private OBO_File _psiMs;
        private List<OBO_File> _psiMsImports;
        private readonly List<OBO_File> _allObo = new List<OBO_File>();

        public CVWriter()
        {
            Read();
        }

        private void Read()
        {
            var unimod = new Unimod_obo_Reader();
            unimod.Read();
            _unimod = unimod.FileData;

            var psiMs = new PSI_MS_obo_Reader();
            psiMs.Read();
            _psiMs = psiMs.FileData;
            _psiMsImports = psiMs.ImportedFileData;

            _allObo.Add(_psiMs);
            _allObo.Add(_unimod);
            _allObo.AddRange(_psiMsImports);
        }

        public void WriteSingleFile(string filename)
        {
            using (var file = new StreamWriter(new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.None)))
            {
                file.NewLine = "\n";
                file.WriteLine(Header());
                file.WriteLine(UsingAndNamespace());
                // Write main class open...
                file.WriteLine(ClassOpen());
                file.WriteLine(CVInfoList("        "));
                file.WriteLine();
                PopulateTermDict();
                file.WriteLine(GenerateRelationOtherTypesEnum("        "));
                file.WriteLine();
                file.WriteLine(GenerateCVEnum("        "));
                file.WriteLine();
                file.WriteLine(GenerateTermInfoObject("        "));
                file.WriteLine();
                file.WriteLine(RelationsIsAEnum("        "));
                // Write main class close...
                file.WriteLine(ClassClose());
                file.WriteLine(NamespaceClose());
            }
        }

        public void WriteFileSet(string filenameBase)
        {
            var metadataFileName = filenameBase + "_metadata.cs";
            var enumFileName = filenameBase + "_enum.cs";
            var termDataFileName = filenameBase + "_termData.cs";
            var relationshipsFileName = filenameBase + "_relationships.cs";

            using (var file = new StreamWriter(new FileStream(metadataFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None)))
            {
                file.NewLine = "\n";
                file.WriteLine(Header());
                file.WriteLine(UsingAndNamespace(false));
                // Write main class open...
                file.WriteLine(ClassOpen());
                file.WriteLine(CVInfoList("        "));
                file.WriteLine();
                PopulateTermDict();
                file.WriteLine(GenerateRelationOtherTypesEnum("        "));
                // Write main class close...
                file.WriteLine(ClassClose());
                file.WriteLine(NamespaceClose());
            }

            using (var file = new StreamWriter(new FileStream(enumFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None)))
            {
                file.NewLine = "\n";
                file.WriteLine(Header());
                file.WriteLine(UsingAndNamespace(false));
                // Write main class open...
                file.WriteLine(ClassOpen());
                file.WriteLine(GenerateCVEnum("        "));
                // Write main class close...
                file.WriteLine(ClassClose());
                file.WriteLine(NamespaceClose());
            }

            using (var file = new StreamWriter(new FileStream(termDataFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None)))
            {
                file.NewLine = "\n";
                file.WriteLine(Header());
                file.WriteLine(UsingAndNamespace(false));
                // Write main class open...
                file.WriteLine(ClassOpen());
                PopulateTermDict();
                file.WriteLine(GenerateTermInfoObjectSplit("        "));
                // Write main class close...
                file.WriteLine(ClassClose());
                file.WriteLine(NamespaceClose());
            }

            using (var file = new StreamWriter(new FileStream(relationshipsFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None)))
            {
                file.NewLine = "\n";
                file.WriteLine(Header());
                file.WriteLine(UsingAndNamespace());
                // Write main class open...
                file.WriteLine(ClassOpen());
                PopulateTermDict();
                file.WriteLine(RelationsIsAEnumSplit("        "));
                // Write main class close...
                file.WriteLine(ClassClose());
                file.WriteLine(NamespaceClose());
            }
        }

        private string Header()
        {
            return "// DO NOT EDIT THIS FILE!\n" +
                   "// This file is autogenerated from the internet-sourced OBO files.\n" +
                   "// Any edits made will be lost when it is recreated.\n";
        }

        private string UsingAndNamespace(bool needsGenericCollections = true)
        {
            var genColl = "// Using statements:\n" +
                          "using System.Collections.Generic;\n";
            var namespaceDecl = "// ReSharper disable InconsistentNaming\n" +
                        "\n" +
                        "namespace PSI_Interface.CV\n{";
            if (needsGenericCollections)
            {
                return genColl + namespaceDecl;
            }
            return namespaceDecl;
        }

        private string ClassOpen()
        {
            return "    public static partial class CV\n" +
                   "    {";
        }

        private string ClassClose()
        {
            return "    }";
        }

        private string NamespaceClose()
        {
            return "}";
        }

        private string CVInfoList(string indent)
        {
            var values = indent + "/// <summary>Populate the list of included Controlled Vocabularies, with descriptive information</summary>\n" +
                            indent + "public static void PopulateCVInfoList()\n" +
                            indent + "{\n";
            foreach (var cv in _allObo)
            {
                values += indent + "    CVInfoList.Add(new CVInfo(\"" + cv.Id + "\", \"" + cv.Name + "\", \"" + cv.Url + "\", \"" + cv.Version + "\"));\n";
                foreach (var ns in cv.AdditionalNamespaces)
                {
                    values += indent + "    CVInfoList.Add(new CVInfo(\"" + ns + "\", \"" + cv.Name + "\", \"" + cv.Url + "\", \"" + cv.Version + "\"));\n";
                }
            }
            values += indent + "}";
            return values;
        }

        private const string RelationsOtherTypesEnumName = "RelationsOtherTypes";

        private string GenerateRelationOtherTypesEnum(string indent)
        {
            var enumData = indent + "/// <summary>Enum listing all relationships between CV terms used in the included CVs</summary>\n";
            enumData += indent + "public enum " + RelationsOtherTypesEnumName + " : int\n" + indent + "{\n";
            var dict = new Dictionary<string, OBO_Typedef>();
            var unknownDef = new OBO_Typedef {
                Def = "Unknown term relationship"
            };
            dict.Add("Unknown", unknownDef);
            foreach (var obo in _allObo)
            {
                foreach (var typedef in obo.Typedefs.Values)
                {
                    // Remove all duplicates, and automatically create new items....
                    dict[typedef.Id] = typedef;
                }
            }
            // part_of sets are separate.
            if (dict.ContainsKey("part_of"))
            {
                dict.Remove("part_of");
            }
            foreach (var item in dict)
            {
                if (!string.IsNullOrWhiteSpace(item.Value.Def))
                {
                    enumData += indent + "    /// " + EscapeXmlEntities("summary", item.Value.DefShort) + "\n";
                }
                else
                {
                    enumData += indent + "    /// <summary>Description not provided</summary>\n";
                }
                if (!string.IsNullOrWhiteSpace(item.Value.Comment))
                {
                    enumData += indent + "    /// " + EscapeXmlEntities("remarks", item.Value.Comment) + "\n";
                }
                enumData += indent + "    " + item.Key + ",\n\n";
            }
            return enumData + indent + "}";
        }

        private readonly Dictionary<string, OBO_Term> _cvEnumData = new Dictionary<string, OBO_Term>();
        private readonly Dictionary<string, Dictionary<string, OBO_Term>> _cvMapData = new Dictionary<string, Dictionary<string, OBO_Term>>();

        private void PopulateCVEnumData()
        {
            var invalidSymbols = @" @/[():^?*+<=!~`#$%&{}|;'.,>\"; // WARNING: '-' must be at beginning or end, in middle it must be escaped, or it is interpreted as a range
            var invalidSymbolsEscaped = System.Text.RegularExpressions.Regex.Escape(invalidSymbols);
            var invalidSymbolsRegex = @"[\]\s" + invalidSymbolsEscaped + "\\-\\\"]"; // add all whitespace matching, manually escape the ']', since above call doesn't

            // Add "CVID_Unknown" to the list first
            var unknown = new OBO_Term
            {
                Id = "??:0000000",
                Name = "CVID_Unknown",
                EnumName = "CVID_Unknown",
                Def = "CVID_Unknown [Unknown]",
                IsObsolete = false
            };
            _cvEnumData.Add("CVID_Unknown", unknown);
            _cvMapData.Add("??", new Dictionary<string, OBO_Term>() { { "CVID_Unknown", unknown } });

            const string obsolete = "_OBSOLETE";
            foreach (var obo in _allObo)
            {
                var id = obo.Id;
                var parent = new Dictionary<string, OBO_Term>();
                _cvMapData.Add(id, parent);

                foreach (var term in obo.Terms.Values)
                {
                    var name = id + "_";
                    if (!string.IsNullOrWhiteSpace(term.Id_Namespace))
                    {
                        name = term.Id_Namespace + "_";
                    }
                    //name += term.Name.Replace(' ', '_');
                    name += System.Text.RegularExpressions.Regex.Replace(term.Name, invalidSymbolsRegex, "_");
                    //name += System.Text.RegularExpressions.Regex.Replace(term.Name.Replace(' ', '_'), invalidSymbolsRegex, "_");
                    if (term.IsObsolete)
                    {
                        name += obsolete;
                    }
                    var tName = name;
                    var counter = 0;
                    while (_cvEnumData.ContainsKey(name))
                    {
                        counter++;
                        name = tName + counter;
                    }
                    _cvEnumData.Add(name, term);
                    parent.Add(name, term);
                    term.EnumName = name;
                }
            }
        }

        private string GenerateCVEnum(string indent)
        {
            if (_cvEnumData.Count == 0)
            {
                PopulateCVEnumData();
            }

            var enumData = indent + "/// <summary>\n" + indent +
                              "/// A full enumeration of the Controlled Vocabularies PSI-MS, UNIMOD, and the vocabularies they depend on\n" +
                              indent + "/// </summary>\n" +
                              indent + "public enum CVID : int\n" + indent + "{\n";

            /*foreach (var term in _cvEnumData.Values)
            {
                if (!string.IsNullOrWhiteSpace(term.Def))
                {
                    enumData += indent + "    /// " + EscapeXmlEntities("summary", term.DefShort) + "\n";
                }
                else
                {
                    enumData += indent + "    /// <summary>Description not provided</summary>\n";
                }
                var idValue = term.Id.Split(':')[1];
                if (term.EnumName.Equals("CVID_Unknown"))
                {
                    idValue = "-1";
                }
                enumData += indent + "    " + term.EnumName + " = " + idValue + ",\n\n";
            }*/
            foreach (var cv in _cvMapData)
            {
                foreach (var term in cv.Value.Values)
                {
                    var idValue = term.Id_Value;
                    if (cv.Key.Equals("??") && term.EnumName.Equals("CVID_Unknown"))
                    {
                        idValue = -1;
                    }
                    else if (cv.Key.Equals("UNIMOD"))
                    {
                        idValue += 100000000;
                    }
                    else if (cv.Key.Equals("UO"))
                    {
                        idValue += 200000000;
                    }
                    else if (cv.Key.Equals("PATO"))
                    {
                        idValue += 300000000;
                    }
                    else if (term.Id_Namespace.Equals("PEFF"))
                    {
                        idValue += 400000000;
                    }
                    if (!string.IsNullOrWhiteSpace(term.Def))
                    {
                        enumData += indent + "    /// " + EscapeXmlEntities("summary", term.DefShort) + "\n";
                    }
                    else
                    {
                        enumData += indent + "    /// <summary>Description not provided</summary>\n";
                    }
                    enumData += indent + "    " + term.EnumName + " = " + idValue + ",\n\n";
                }
            }
            return enumData + indent + "}";
        }

        private string EscapeXmlEntities(string tagName, string toEscape)
        {
            return new XElement(tagName, toEscape).ToString(SaveOptions.DisableFormatting);
        }

        private string GenerateTermInfoObject(string indent)
        {
            if (_cvEnumData.Count == 0)
            {
                PopulateCVEnumData();
            }

            var dictData = indent + "/// <summary>Populate the CV Term data objects</summary>\n" +
                              indent + "private static void PopulateTermData()\n" + indent + "{\n";
            /*foreach (var term in _cvEnumData.Values)
            {
                dictData += indent + "    TermData.Add(" + "CVID." + term.EnumName + ", new TermInfo(" + "CVID." +
                            term.EnumName + ", @\"" + term.Id + "\", @\"" + term.Name + "\", @\"" + term.DefShort + "\", " + term.IsObsolete.ToString().ToLower() + "));\n";
                //TermData as list
                //dictData += indent + "    TermData.Add(new TermInfo(" + "CVID." + term.EnumName + ", @\"" + term.Id +
                //            "\", @\"" + term.Name + "\", @\"" + term.DefShort + "\", " + term.IsObsolete.ToString().ToLower() + "));\n";
            }*/
            foreach (var cv in _cvMapData)
            {
                foreach (var term in cv.Value.Values)
                {
                    dictData += indent + "    TermData.Add(" + "CVID." + term.EnumName + ", new TermInfo(" + "CVID." +
                                term.EnumName + ", @\"" + cv.Key + "\", @\"" + term.Id + "\", @\"" + term.Name + "\", @\"" + term.DefShort + "\", " + term.IsObsolete.ToString().ToLower() + "));\n";
                    //TermData as list
                    //dictData += indent + "    TermData.Add(new TermInfo(" + "CVID." + term.EnumName + ", @\"" + term.Id +
                    //            "\", @\"" + term.Name + "\", @\"" + term.DefShort + "\", " + term.IsObsolete.ToString().ToLower() + "));\n";
                }
            }
            return dictData + indent + "}";
        }

        private string GenerateTermInfoObjectSplit(string indent)
        {
            if (_cvEnumData.Count == 0)
            {
                PopulateCVEnumData();
            }

            var functionName = "PopulateTermData";
            var functionPartName = functionName + "Part";
            var commentStart = indent + "/// <summary>Populate the CV Term data objects";
            var commentEnd = "</summary>\n";
            var functionPartCommentInsert = ", breakdown part ";
            var functionStartFirst = indent + "private static void ";
            var functionStartRest = "()\n" + indent + "{\n";
            var functionEnd = indent + "}";
            var subFunctions = new List<string>();
            var functionCounter = 1;

            var dictData = commentStart + functionPartCommentInsert + functionCounter + commentEnd +
                functionStartFirst + functionPartName + functionCounter + functionStartRest;
            subFunctions.Add(functionPartName + functionCounter);
            functionCounter++;
            /*foreach (var term in _cvEnumData.Values)
            {
                dictData += indent + "    TermData.Add(" + "CVID." + term.EnumName + ", new TermInfo(" + "CVID." +
                            term.EnumName + ", @\"" + term.Id + "\", @\"" + term.Name + "\", @\"" + term.DefShort + "\", " + term.IsObsolete.ToString().ToLower() + "));\n";
                //TermData as list
                //dictData += indent + "    TermData.Add(new TermInfo(" + "CVID." + term.EnumName + ", @\"" + term.Id +
                //            "\", @\"" + term.Name + "\", @\"" + term.DefShort + "\", " + term.IsObsolete.ToString().ToLower() + "));\n";
            }*/
            var counter = 0;
            var skipBreak = true;
            foreach (var cv in _cvMapData)
            {
                foreach (var term in cv.Value.Values)
                {
                    if (counter % 1000 == 0 && !skipBreak)
                    {
                        dictData += functionEnd + "\n\n";
                        dictData += commentStart + functionPartCommentInsert + functionCounter + commentEnd +
                            functionStartFirst + functionPartName + functionCounter + functionStartRest;
                        subFunctions.Add(functionPartName + functionCounter);
                        functionCounter++;
                    }
                    skipBreak = false;
                    counter++;

                    dictData += indent + "    TermData.Add(" + "CVID." + term.EnumName + ", new TermInfo(" + "CVID." +
                                term.EnumName + ", @\"" + cv.Key + "\", @\"" + term.Id + "\", @\"" + term.Name + "\", @\"" +
                                term.DefShort + "\", " + term.IsObsolete.ToString().ToLower() + "));\n";
                    //TermData as list
                    //dictData += indent + "    TermData.Add(new TermInfo(" + "CVID." + term.EnumName + ", @\"" + term.Id +
                    //            "\", @\"" + term.Name + "\", @\"" + term.DefShort + "\", " + term.IsObsolete.ToString().ToLower() + "));\n";
                }
            }

            dictData += functionEnd + "\n\n";
            dictData += commentStart + commentEnd + functionStartFirst + functionName + functionStartRest;

            foreach (var part in subFunctions)
            {
                dictData += indent + "    " + part + "();\n";
            }

            dictData += functionEnd;
            return dictData;
        }

        private readonly Dictionary<string, OBO_Term> _bigTermDict = new Dictionary<string, OBO_Term>();
        private bool _bigTermDictPopulated;

        private void PopulateTermDict()
        {
            if (_bigTermDictPopulated)
            {
                return;
            }
            foreach (var obo in _allObo)
            {
                foreach (var term in obo.Terms)
                {
                    _bigTermDict.Add(term.Key, term.Value);
                }
            }
            _bigTermDictPopulated = true;
        }

        private string RelationsIsAEnum(string indent)
        {
            var items = new Dictionary<string, List<string>>();
            foreach (var obo in _allObo)
            {
                foreach (var term in obo.Terms.Values)
                {
                    if (term.IsA.Count > 0)
                    {
                        items.Add(term.EnumName, new List<string>());
                        foreach (var rel in term.IsA)
                        {
                            var rel2 = rel.Trim().Split(' ')[0];
                            if (_bigTermDict.ContainsKey(rel2))
                            {
                                items[term.EnumName].Add(_bigTermDict[rel2].EnumName);
                            }
                        }
                        if (items[term.EnumName].Count <= 0)
                        {
                            items.Remove(term.EnumName);
                        }
                    }
                }
            }

            var fillData = indent + "/// <summary>Populate the relationships between CV terms</summary>\n" +
                              indent + "private static void FillRelationsIsA()\n" + indent + "{\n";
            foreach (var item in items)
            {
                //RelationsIsA.Add("name", new List<string> { "ref", "ref2", });
                fillData += indent + "    RelationsIsA.Add(" + "CVID." + item.Key + ", new List<CVID> { ";
                foreach (var map in item.Value)
                {
                    fillData += "CVID." + map + ", ";
                }
                fillData += "});\n";
            }
            return fillData + indent + "}";
        }

        private string RelationsIsAEnumSplit(string indent)
        {
            var items = new Dictionary<string, List<string>>();
            foreach (var obo in _allObo)
            {
                foreach (var term in obo.Terms.Values)
                {
                    if (term.IsA.Count > 0)
                    {
                        items.Add(term.EnumName, new List<string>());
                        foreach (var rel in term.IsA)
                        {
                            var rel2 = rel.Trim().Split(' ')[0];
                            if (_bigTermDict.ContainsKey(rel2))
                            {
                                items[term.EnumName].Add(_bigTermDict[rel2].EnumName);
                            }
                        }
                        if (items[term.EnumName].Count <= 0)
                        {
                            items.Remove(term.EnumName);
                        }
                    }
                }
            }

            var functionName = "FillRelationsIsA";
            var functionPartName = functionName + "Part";
            var commentStart = indent + "/// <summary>Populate the relationships between CV terms";
            var commentEnd = "</summary>\n";
            var functionPartCommentInsert = ", breakdown part ";
            var functionStartFirst = indent + "private static void ";
            var functionStartRest = "()\n" + indent + "{\n";
            var functionEnd = indent + "}";
            var subFunctions = new List<string>();
            var functionCounter = 1;

            var fillData = commentStart + functionPartCommentInsert + functionCounter + commentEnd +
                functionStartFirst + functionPartName + functionCounter + functionStartRest;
            subFunctions.Add(functionPartName + functionCounter);
            functionCounter++;

            var counter = 0;
            var skipBreak = true;
            foreach (var item in items)
            {
                if (counter % 1000 == 0 && !skipBreak)
                {
                    fillData += functionEnd + "\n\n";
                    fillData += commentStart + functionPartCommentInsert + functionCounter + commentEnd +
                        functionStartFirst + functionPartName + functionCounter + functionStartRest;
                    subFunctions.Add(functionPartName + functionCounter);
                    functionCounter++;
                }
                skipBreak = false;
                counter++;

                //RelationsIsA.Add("name", new List<string> { "ref", "ref2", });
                fillData += indent + "    RelationsIsA.Add(" + "CVID." + item.Key + ", new List<CVID> { ";
                foreach (var map in item.Value)
                {
                    fillData += "CVID." + map + ", ";
                }
                fillData += "});\n";
            }

            fillData += functionEnd + "\n\n";
            fillData += commentStart + commentEnd + functionStartFirst + functionName + functionStartRest;

            foreach (var part in subFunctions)
            {
                fillData += indent + "    " + part + "();\n";
            }

            fillData += functionEnd;
            return fillData;
        }
    }
}
