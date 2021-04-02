using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML OrganizationType
    /// </summary>
    /// <remarks>
    /// Organizations are entities like companies, universities, government agencies.
    /// Any additional information such as the address, email etc. should be supplied either as CV parameters or as user
    /// parameters.
    /// </remarks>
    public class OrganizationObj : AbstractContactObj, IEquatable<OrganizationObj>
    {
        private ParentOrganizationObj _parent;

        /// <summary>
        /// Constructor
        /// </summary>
        public OrganizationObj()
        {
            _parent = null;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="o"></param>
        /// <param name="idata"></param>
        public OrganizationObj(OrganizationType o, IdentDataObj idata)
            : base(o, idata)
        {
            _parent = null;

            if (o.Parent != null)
                _parent = new ParentOrganizationObj(o.Parent, IdentData);
        }

        /// <summary>min 0, max 1</summary>
        public ParentOrganizationObj Parent
        {
            get => _parent;
            set
            {
                _parent = value;
                if (_parent != null)
                    _parent.IdentData = IdentData;
            }
        }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as OrganizationObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(OrganizationObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if ((Name == other.Name) && Equals(Parent, other.Parent) &&
                Equals(CVParams, other.CVParams) && Equals(UserParams, other.UserParams))
                return true;
            return false;
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name != null ? Name.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Parent != null ? Parent.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}