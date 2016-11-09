using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI_Interface.CV
{
    /// <summary>
    /// Class containing a large amount of information about the needed controlled vocabularies
    /// </summary>
    public static partial class CV
    {
        /// <summary>
        /// CV term is-a relationships
        /// </summary>
        public static readonly Dictionary<CVID, List<CVID>> RelationsIsA = new Dictionary<CVID, List<CVID>>();

        /// <summary>
        /// CV term children relationships
        /// </summary>
        public static readonly Dictionary<CVID, List<CVID>> RelationsChildren = new Dictionary<CVID, List<CVID>>();
        
        /// <summary>
        /// CV term part-of relationships
        /// </summary>
        public static readonly Dictionary<CVID, List<CVID>> RelationsPartOf = new Dictionary<CVID, List<CVID>>();

        /// <summary>
        /// CV term exact synonym relations
        /// </summary>
        public static readonly Dictionary<CVID, List<string>> RelationsExactSynonym = new Dictionary<CVID, List<string>>();

        /// <summary>
        /// CV term other relations
        /// </summary>
        public static readonly Dictionary<CVID, Dictionary<RelationsOtherTypes, List<CVID>>> RelationsOther = new Dictionary<CVID, Dictionary<RelationsOtherTypes, List<CVID>>>();

        /// <summary>
        /// CV term extra data
        /// </summary>
        public static readonly Dictionary<CVID, TermInfo> TermData = new Dictionary<CVID, TermInfo>();

        /// <summary>
        /// CV descriptive information
        /// </summary>
        public static readonly List<CVInfo> CVInfoList = new List<CVInfo>();

        /// <summary>
        /// Mapping from CV to CV term accession to CV term enum
        /// </summary>
        public static readonly Dictionary<string, Dictionary<string, CVID>> TermAccessionLookup = new Dictionary<string, Dictionary<string, CVID>>();

        /// <summary>
        /// Returns true if child has an IsA relationship with parent
        /// </summary>
        /// <param name="child"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static bool CvidIsA(CVID child, CVID parent)
        {
            if (!RelationsIsA.ContainsKey(child))
            {
                return false;
            }
            var relList = RelationsIsA[child];
            if (relList.Contains(parent))
            {
                return true;
            }
            // Dig deeper - check grandparents, etc.
            foreach (var ancestor in RelationsIsA[child])
            {
                if (CvidIsA(ancestor, parent))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Primary identifying information about a particular CV
        /// </summary>
        public class CVInfo
        {
            /// <summary>
            /// CV identifier
            /// </summary>
            public string Id { get; private set; }

            /// <summary>
            /// CV name
            /// </summary>
            public string Name { get; private set; }

            /// <summary>
            /// CV URI
            /// </summary>
            public string URI { get; private set; }

            /// <summary>
            /// CV Version
            /// </summary>
            public string Version { get; private set; }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="pId">CV Identifier</param>
            /// <param name="pName">CV Name</param>
            /// <param name="pURI">CV URI</param>
            /// <param name="pVersion">CV Version</param>
            public CVInfo(string pId, string pName, string pURI, string pVersion)
            {
                Id = pId;
                Name = pName;
                URI = pURI;
                Version = pVersion;
            }
        }

        /// <summary>
        /// Information about a particular CV Term
        /// </summary>
        public class TermInfo
        {
            /// <summary>
            /// Term enum identifier
            /// </summary>
            public CVID Cvid { get; private set; }

            /// <summary>
            /// Term parent CV
            /// </summary>
            public string CVRef { get; private set; }

            /// <summary>
            /// Term identifier
            /// </summary>
            public string Id { get; private set; }

            /// <summary>
            /// Term name
            /// </summary>
            public string Name { get; private set; }

            /// <summary>
            /// Term definition
            /// </summary>
            public string Definition { get; private set; }

            /// <summary>
            /// If the term is marked obsolete
            /// </summary>
            public bool IsObsolete { get; private set; }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="pCVID">Term enum identifier</param>
            /// <param name="pCVRef">term parent CV</param>
            /// <param name="pId">term identifier</param>
            /// <param name="pName">term name</param>
            /// <param name="pDef">term definition</param>
            /// <param name="pIsObs">if the term is marked obsolete</param>
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

        /// <summary>
        /// Constructor: Populates the relationship dictionaries
        /// </summary>
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
