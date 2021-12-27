using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// Object for containing multiple CVParamObjs
    /// </summary>
    public class CVParamGroupObj : IdentDataInternalTypeAbstract, IEquatable<CVParamGroupObj>
    {
        private IdentDataList<CVParamObj> _cvParams;

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public CVParamGroupObj()
        {
            CVParams = new IdentDataList<CVParamObj>(1);
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="cvpg"></param>
        /// <param name="idata"></param>
        public CVParamGroupObj(ICVParamGroup cvpg, IdentDataObj idata)
            : base(idata)
        {
            CVParams = new IdentDataList<CVParamObj>(1);

            if (cvpg.cvParam?.Count > 0)
            {
                CVParams.AddRange(cvpg.cvParam, cvp => new CVParamObj(cvp, IdentData));
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// List of CVParams
        /// </summary>
        public IdentDataList<CVParamObj> CVParams
        {
            get => _cvParams;
            set
            {
                _cvParams = value;
                if (_cvParams != null)
                {
                    _cvParams.IdentData = IdentData;
                }
            }
        }
        #endregion

        #region Object Equality
        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(CVParamGroupObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other == null)
            {
                return false;
            }

            return Equals(CVParams, other.CVParams);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is CVParamGroupObj o && Equals(o);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            return CVParams != null ? CVParams.GetHashCode() : 0;
        }
        #endregion
    }
}
