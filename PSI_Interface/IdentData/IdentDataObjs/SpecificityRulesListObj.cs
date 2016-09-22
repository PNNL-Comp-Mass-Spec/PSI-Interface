using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML SpecificityRulesType
    /// </summary>
    /// <remarks>
    ///     The specificity rules of the searched modification including for example
    ///     the probability of a modification's presence or peptide or protein termini. Standard
    ///     fixed or variable status should be provided by the attribute fixedMod.
    /// </remarks>
    /// <remarks>CVParams: list of the specificity rules. min 1, max unbounded</remarks>
    public class SpecificityRulesListObj : CVParamGroupObj
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public SpecificityRulesListObj()
        {
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="sr"></param>
        /// <param name="idata"></param>
        public SpecificityRulesListObj(SpecificityRulesType sr, IdentDataObj idata)
            : base(sr, idata)
        {
        }
    }
}