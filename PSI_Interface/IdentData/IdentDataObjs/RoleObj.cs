using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML RoleType
    /// </summary>
    /// <remarks>The roles (lab equipment sales, contractor, etc.) the Contact fills.</remarks>
    public class RoleObj : IdentDataInternalTypeAbstract, IEquatable<RoleObj>
    {
        private CVParamObj _cvParam;

        /// <summary>
        /// Constructor
        /// </summary>
        public RoleObj()
        {
            _cvParam = null;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="r"></param>
        /// <param name="idata"></param>
        public RoleObj(RoleType r, IdentDataObj idata)
            : base(idata)
        {
            _cvParam = null;

            if (r.cvParam != null)
                _cvParam = new CVParamObj(r.cvParam, IdentData);
        }

        /// <summary>CV term for contact roles, such as software provider.</summary>
        /// <remarks>min 1, max 1</remarks>
        public CVParamObj CVParam
        {
            get => _cvParam;
            set
            {
                _cvParam = value;
                if (_cvParam != null)
                    _cvParam.IdentData = IdentData;
            }
        }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            var o = other as RoleObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(RoleObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if (Equals(CVParam, other.CVParam))
                return true;
            return false;
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            return CVParam != null ? CVParam.GetHashCode() : 0;
        }

        #endregion
    }
}
