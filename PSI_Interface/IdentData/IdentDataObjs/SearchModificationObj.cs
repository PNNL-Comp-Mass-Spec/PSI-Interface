using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML SearchModificationType: Container ModificationParamsType
    /// </summary>
    /// <remarks>
    ///     Specification of a search modification as parameter for a spectra search. Contains the name of the
    ///     modification, the mass, the specificity and whether it is a static modification.
    /// </remarks>
    /// <remarks>
    ///     ModificationParamsType: The specification of static/variable modifications (e.g. Oxidation of Methionine) that
    ///     are to be considered in the spectra search.
    /// </remarks>
    /// <remarks>ModificationParamsType: child element SearchModification, of type SearchModificationType, min 1, max unbounded</remarks>
    /// <remarks>
    ///     CVParams: The modification is uniquely identified by references to external CVs such as UNIMOD, see
    ///     specification document and mapping file for more details. min 1, max unbounded
    /// </remarks>
    public class SearchModificationObj : CVParamGroupObj, IEquatable<SearchModificationObj>
    {
        private IdentDataList<SpecificityRulesListObj> _specificityRules;

        /// <summary>
        ///     Constructor
        /// </summary>
        public SearchModificationObj()
        {
            FixedMod = false;
            MassDelta = 0;
            Residues = null;

            SpecificityRules = new IdentDataList<SpecificityRulesListObj>(1);
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
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

            if ((sm.SpecificityRules != null) && (sm.SpecificityRules.Count > 0))
            {
                SpecificityRules.AddRange(sm.SpecificityRules, sr => new SpecificityRulesListObj(sr, IdentData));
            }
        }

        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<SpecificityRulesListObj> SpecificityRules
        {
            get { return _specificityRules; }
            set
            {
                _specificityRules = value;
                if (_specificityRules != null)
                    _specificityRules.IdentData = IdentData;
            }
        }

        /// <remarks>True, if the modification is static (i.e. occurs always).</remarks>
        /// Required Attribute
        /// boolean
        public bool FixedMod { get; set; }

        /// <remarks>The mass delta of the searched modification in Daltons.</remarks>
        /// Required Attribute
        /// float
        public float MassDelta { get; set; }

        /// <remarks>
        ///     The residue(s) searched with the specified modification. For N or C terminal modifications that can occur
        ///     on any residue, the . character should be used to specify any, otherwise the list of amino acids should be
        ///     provided.
        /// </remarks>
        /// Required Attribute
        /// listOfCharsOrAny: string, space-separated regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}|."
        public string Residues { get; set; }

        #region Object Equality

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as SearchModificationObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(SearchModificationObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if ((FixedMod == other.FixedMod) && MassDelta.Equals(other.MassDelta) &&
                (Residues == other.Residues) && Equals(SpecificityRules, other.SpecificityRules) &&
                Equals(CVParams, other.CVParams))
                return true;
            return false;
        }

        /// <summary>
        ///     Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = FixedMod.GetHashCode();
                hashCode = (hashCode * 397) ^ MassDelta.GetHashCode();
                hashCode = (hashCode * 397) ^ (Residues != null ? Residues.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SpecificityRules != null ? SpecificityRules.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}