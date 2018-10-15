using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     Object for containing multiple CVParamObjs
    /// </summary>
    public class CVParamGroupObj : IdentDataInternalTypeAbstract, IEquatable<CVParamGroupObj>
    {
        private IdentDataList<CVParamObj> _cvParams;

        #region Constructors
        /// <summary>
        ///     Constructor
        /// </summary>
        public CVParamGroupObj()
        {
            CVParams = new IdentDataList<CVParamObj>(1);
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="cvpg"></param>
        /// <param name="idata"></param>
        public CVParamGroupObj(ICVParamGroup cvpg, IdentDataObj idata)
            : base(idata)
        {
            CVParams = new IdentDataList<CVParamObj>(1);

            if (cvpg.cvParam != null && cvpg.cvParam.Count > 0)
            {
                CVParams.AddRange(cvpg.cvParam, cvp => new CVParamObj(cvp, IdentData));
            }
        }
        #endregion

        #region Properties
        /// <summary>
        ///     List of CVParams
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
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
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

            if (Equals(CVParams, other.CVParams))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as CVParamGroupObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        /// <summary>
        ///     Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = CVParams != null ? CVParams.GetHashCode() : 0;
            return hashCode;
        }
        #endregion
    }
}