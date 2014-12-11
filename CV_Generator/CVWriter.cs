﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CV_Generator
{
	public class CVWriter
	{
		private OBO_File _unimod;
		private OBO_File _psiMs;
		private List<OBO_File> _psiMsImports;
		private List<OBO_File> _allObo = new List<OBO_File>();

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

		private string Header()
		{
			return "// DO NOT EDIT THIS FILE!\n" +
				"// This file is autogenerated from the internet-sourced OBO files.\n" +
				"// Any edits made will be lost when it is recreated.\n";
		}

		private string UsingAndNamespace()
		{
			return "// Using statements:\n" +
				"\n" +
				"namespace PSI_Interface.CV\n{\n";
		}

		private string TermInfo()
		{
			return "\t\tpublic class TermInfo\n" +
				   "\t\t{\n" +
				   "\t\t\tpublic CVID Cvid { get; private set; }\n" +
				   "\t\t\tpublic string Id { get; private set; }\n" +
			       "\t\t\tpublic string Name { get; private set; }\n" +
				   "\t\t\tpublic string Definition { get; private set; }\n" +
				   "\t\t\tpublic bool isObsolete { get; private set; }\n" +
				   "\t\t\t\n" +
				   "\t\t\tpublic TermInfo(CVID pCVID, string pId, string pName, string pDef, bool pIsObs)\n" +
				   "\t\t\t{\n" +
				   "\t\t\t\tCvid = pCVID;\n" +
				   "\t\t\t\tId = pId;\n" +
				   "\t\t\t\tName = pName;\n" +
				   "\t\t\t\tDefinition = pDef;\n" +
				   "\t\t\t\tIsObsolete = pIsObs;\n" +
				   "\t\t\t}\n" +
				   "\t\t}\n";
		}

		private string Relations()
		{
			return
				"\t\tpublic readonly Dictionary<CVID, List<CVID>> RelationsIsA = new Dictionary<CVID, List<CVID>>();\n" +
				"\t\tpublic readonly Dictionary<CVID, List<CVID>> RelationsPartOf = new Dictionary<CVID, List<CVID>>();\n" +
				"\t\tpublic readonly Dictionary<CVID, List<string>> RelationsExactSynonym = new Dictionary<CVID, List<string>>();\n" +
				"\t\tpublic readonly Dictionary<CVID, Dictionary<" + RelationsOtherTypesEnumName + ", List<CVID>>> RelationsOther = new Dictionary<CVID, Dictionary<" + RelationsOtherTypesEnumName + ", List<CVID>>>();\n";
		}

		private const string RelationsOtherTypesEnumName = "RelationsOtherTypes";

		//public enum RelationsOtherTypes : int
		//{
		//	has_units,
		//	Unknown,
		//	has_order,
		//	has_domain,
		//	has_regexp,
		//
		//}

		private string GenerateRelationOtherTypesEnum()
		{
			string enumData = "\t\tpublic enum " + RelationsOtherTypesEnumName + " : int\n" + "\t\t{\n";
			var dict = new Dictionary<string, int>();
			dict.Add("Unknown", 0);
			foreach (var obo in _allObo)
			{
				foreach (var typedef in obo.Typedefs)
				{
					// Remove all duplicates, and automatically create new items....
					dict[typedef.Id] = 0;
				}
			}
			// part_of sets are separate.
			if (dict.ContainsKey("part_of"))
			{
				dict.Remove("part_of");
			}
			foreach (var key in dict.Keys)
			{
				enumData += "\t\t\t" + key + ",\n";
			}
			return enumData + "\t\t}\n";
		}

		private string GenerateCVEnum()
		{
			var names = new Dictionary<string, OBO_File.OBO_Term>();
			const string obsol = "_OBSOLETE";
			foreach (var obo in _allObo)
			{
				if (obo.IsGeneratedId && obo.Terms.Count > 0)
				{
					string tempId = obo.Terms[0].Id;
					tempId = tempId.Split(':')[0];
					obo.Id = tempId;
				}
				var id = obo.Id;

				foreach (var term in obo.Terms)
				{
					string name = id + "_";
					name += term.Name.Replace(' ', '_');
					if (term.IsObsolete)
					{
						name += obsol;
					}
					string tName = name;
					int counter = 0;
					while (names.ContainsKey(name))
					{
						counter++;
						name = tName + counter;
					}
					names.Add(name, term);
					term.EnumName = name;
				}
			}

			string enumData = "\t\tpublic enum CVID : int\n" + "\t\t{\n";
			enumData += "\t\t\tCVID_Unknown,\n\n";
			foreach (var term in names.Values)
			{
				if (!string.IsNullOrWhiteSpace(term.Def))
				{
					enumData += "\t\t\t// " + term.DefShort + "\n";
				}
				enumData += "\t\t\t" + term.EnumName + ",\n\n";
			}
			return enumData + "\t\t}\n";
		}
	}
}