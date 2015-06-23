using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI_Interface.CV
{
    public static partial class CV
    {
        public static readonly Dictionary<CVID, List<CVID>> RelationsIsA = new Dictionary<CVID, List<CVID>>();
        public static readonly Dictionary<CVID, List<CVID>> RelationsChildren = new Dictionary<CVID, List<CVID>>();
        public static readonly Dictionary<CVID, List<CVID>> RelationsPartOf = new Dictionary<CVID, List<CVID>>();
        public static readonly Dictionary<CVID, List<string>> RelationsExactSynonym = new Dictionary<CVID, List<string>>();
        public static readonly Dictionary<CVID, Dictionary<RelationsOtherTypes, List<CVID>>> RelationsOther = new Dictionary<CVID, Dictionary<RelationsOtherTypes, List<CVID>>>();
        public static readonly Dictionary<CVID, TermInfo> TermData = new Dictionary<CVID, TermInfo>();
        public static readonly List<CVInfo> CVInfoList = new List<CVInfo>();
        public static readonly Dictionary<string, Dictionary<string, CVID>> TermAccessionLookup = new Dictionary<string, Dictionary<string, CVID>>();

        public class CVInfo
        {
            public string Id { get; private set; }
            public string Name { get; private set; }
            public string URI { get; private set; }
            public string Version { get; private set; }

            public CVInfo(string pId, string pName, string pURI, string pVersion)
            {
                Id = pId;
                Name = pName;
                URI = pURI;
                Version = pVersion;
            }
        }

        public class TermInfo
        {
            public CVID Cvid { get; private set; }
            public string CVRef { get; private set; }
            public string Id { get; private set; }
            public string Name { get; private set; }
            public string Definition { get; private set; }
            public bool IsObsolete { get; private set; }

            public TermInfo(CVID pCVID, string pCVRef, string pId, string pName, string pDef, bool pIsObs)
            {
                Cvid = pCVID;
                CVRef = pCVRef;
                Id = pId;
                Name = pName;
                Definition = pDef;
                IsObsolete = pIsObs;
            }
        }

        static CV()
        {
            PopulateCVInfoList();
            PopulateTermData();
            FillRelationsIsA();
            CreateLookups();
            CreateParentRelations();
        }

        private static void CreateLookups()
        {
            if (CVInfoList.Count <= 0)
            {
                return;
            }
            foreach (var term in TermData.Values)
            {
                if (TermAccessionLookup.ContainsKey(term.CVRef))
                {
                    TermAccessionLookup[term.CVRef].Add(term.Id, term.Cvid);
                }
                else
                {
                    TermAccessionLookup.Add(term.CVRef, new Dictionary<string, CVID>() {{term.Id, term.Cvid}});
                }
            }
        }

        private static void CreateParentRelations()
        {
            foreach (var cvid in RelationsIsA)
            {
                foreach (var parent in cvid.Value)
                {
                    if (RelationsChildren.ContainsKey(parent))
                    {
                        RelationsChildren[parent].Add(cvid.Key);
                    }
                    else
                    {
                        RelationsChildren.Add(parent, new List<CVID>(){ cvid.Key });
                    }
                }
            }
        }
    }
}
