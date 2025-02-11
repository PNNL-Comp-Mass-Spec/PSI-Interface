using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML SearchModificationType: Container ModificationParamsType
    /// </summary>
    /// <remarks>
    /// Specification of a search modification as parameter for a spectra search. Contains the name of the
    /// modification, the mass, the specificity and whether it is a static modification.
    /// </remarks>
    /// <remarks>
    /// ModificationParamsType: The specification of static/variable modifications (e.g. Oxidation of Methionine) that
    /// are to be considered in the spectra search.
    /// </remarks>
    /// <remarks>ModificationParamsType: child element SearchModification, of type SearchModificationType, min 1, max unbounded</remarks>
    /// <remarks>
    /// CVParams: The modification is uniquely identified by references to external CVs such as UNIMOD, see
    /// specification document and mapping file for more details. min 1, max unbounded
    /// </remarks>
    public class SearchModificationObj : CVParamGroupObj, IEquatable<SearchModificationObj>
    {
        private IdentDataList<SpecificityRulesListObj> _specificityRules;

        /// <summary>
        /// Constructor
        /// </summary>
        public SearchModificationObj()
        {
            FixedMod = false;
            MassDelta = 0;
            Residues = null;

            SpecificityRules = new IdentDataList<SpecificityRulesListObj>(1);
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="sm"></param>
        /// <param name="idata"></param>
        public SearchModificationObj(SearchModificationType sm, IdentDataObj idata)
            : base(sm, idata)
        {
            FixedMod = sm.fixedMod;
            MassDelta = sm.massDelta;
            Residues = sm.residues;

            SpecificityRules = new IdentDataList<SpecificityRulesListObj>(1);

            if (sm.SpecificityRules?.Count > 0)
            {
                SpecificityRules.AddRange(sm.SpecificityRules, sr => new SpecificityRulesListObj(sr, IdentData));
            }
        }

        /// <summary>min 0, max unbounded</summary>
        public IdentDataList<SpecificityRulesListObj> SpecificityRules
        {
            get => _specificityRules;
            set
            {
                _specificityRules = value;

                if (_specificityRules != null)
                    _specificityRules.IdentData = IdentData;
            }
        }

        /// <summary>True, if the modification is static (i.e. occurs always).</summary>
        /// <remarks>Required Attribute</remarks>
        public bool FixedMod { get; set; }

        /// <summary>The mass delta of the searched modification in Daltons.</summary>
        /// <remarks>Required Attribute</remarks>
        public float MassDelta { get; set; }

        /// <summary>
        /// The residue(s) searched with the specified modification. For N or C terminal modifications that can occur
        /// on any residue, the . character should be used to specify any, otherwise the list of amino acids should be
        /// provided.
        /// </summary>
        /// <remarks>Required Attribute</remarks>
        /// <returns>RegEx: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}|."</returns>
        public string Residues { get; set; }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is SearchModificationObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(SearchModificationObj other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return FixedMod == other.FixedMod && MassDelta.Equals(other.MassDelta) &&
                   Residues == other.Residues && Equals(SpecificityRules, other.SpecificityRules) &&
                   ParamsEquals(other);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = FixedMod.GetHashCode();
                hashCode = (hashCode * 397) ^ MassDelta.GetHashCode();
                hashCode = (hashCode * 397) ^ (Residues?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (SpecificityRules?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (CVParams?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}
