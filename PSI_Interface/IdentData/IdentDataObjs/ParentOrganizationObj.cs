using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML ParentOrganizationType
    /// </summary>
    /// <remarks>The containing organization (the university or business which a lab belongs to, etc.)</remarks>
    public class ParentOrganizationObj : OrganizationRefObj
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ParentOrganizationObj()
        {
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="po"></param>
        /// <param name="idata"></param>
        public ParentOrganizationObj(ParentOrganizationType po, IdentDataObj idata)
            : base(idata)
        {
            OrganizationRef = po.organization_ref;
        }
    }
}