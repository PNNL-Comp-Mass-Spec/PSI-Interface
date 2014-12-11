using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI_Interface.CV
{
	public partial class CV
	{

		public enum CVID : int
		{
			MS_Unknown
		}

		public class TermInfo
		{
			public CVID Cvid { get; private set; }
			public string Id { get; private set; }
			public string Name { get; private set; }
			public string Definition { get; private set; }
			public bool IsObsolete { get; private set; }

			public TermInfo(CVID pCVID, string pId, string pName, string pDef, bool pIsObs)
			{
				Cvid = pCVID;
				Id = pId;
				Name = pName;
				Definition = pDef;
				IsObsolete = pIsObs;
			}
		}

		public readonly Dictionary<CVID, List<CVID>> RelationsIsA = new Dictionary<CVID, List<CVID>>();
		public readonly Dictionary<CVID, List<CVID>> RelationsPartOf = new Dictionary<CVID, List<CVID>>();
		public readonly Dictionary<CVID, List<string>> RelationsExactSynonym = new Dictionary<CVID, List<string>>();
		public readonly Dictionary<CVID, Dictionary<RelationsOtherTypes, List<CVID>>> RelationsOther = new Dictionary<CVID, Dictionary<RelationsOtherTypes, List<CVID>>>();

		public enum RelationsOtherTypes : int
		{
			has_units,
			Unknown,
			has_order,
			has_domain,
			has_regexp,

		}
	}
}
