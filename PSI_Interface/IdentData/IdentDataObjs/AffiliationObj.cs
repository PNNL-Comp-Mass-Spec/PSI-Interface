using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML AffiliationType
    /// </summary>
    public class AffiliationObj : OrganizationRefObj
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AffiliationObj() : base()
        { }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="a"></param>
        /// <param name="idata"></param>
        public AffiliationObj(AffiliationType a, IdentDataObj idata)
            : base(idata)
        {
            OrganizationRef = a.organization_ref;
        }
    }
}