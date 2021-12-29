using System;
using JetBrains.Annotations;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// Object for containing multiple CVParamObjs
    /// </summary>
    public class CVParamGroupObj : IdentDataInternalTypeAbstract, IEquatable<CVParamGroupObj>
    {
        private IdentDataList<CVParamObj> _cvParams;

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

        /// <summary>
        /// Object equality, with no null check
        /// </summary>
        /// <param name="other"></param>
        public bool ParamsEquals([NotNull] CVParamGroupObj other)
        {
            return Equals(CVParams, other.CVParams);
        }

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

            return ParamsEquals(other);
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
            return CVParams?.GetHashCode() ?? 0;
        }
        #endregion
    }
}
