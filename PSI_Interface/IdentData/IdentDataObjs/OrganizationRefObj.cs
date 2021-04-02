using System;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// Base class for identical ParentOrganization and AffiliationInfo
    /// </summary>
    public class OrganizationRefObj : IdentDataInternalTypeAbstract, IEquatable<OrganizationRefObj>
    {
        private OrganizationObj _organization;
        private string _organizationRef;

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public OrganizationRefObj()
        {
            _organizationRef = null;

            _organization = null;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="idata"></param>
        public OrganizationRefObj(IdentDataObj idata)
            : base(idata)
        {
        }
        #endregion

        #region Properties
        /// <summary>A reference to the organization this contact belongs to.</summary>
        /// <remarks>Required Attribute</remarks>
        protected internal string OrganizationRef
        {
            get
            {
                if (_organization != null)
                {
                    return _organization.Id;
                }
                return _organizationRef;
            }
            set
            {
                _organizationRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    Organization = IdentData.FindOrganization(value);
                }
            }
        }

        /// <summary>A reference to the organization this contact belongs to.</summary>
        /// <remarks>Required Attribute</remarks>
        public OrganizationObj Organization
        {
            get => _organization;
            set
            {
                _organization = value;
                if (_organization != null)
                {
                    _organization.IdentData = IdentData;
                    _organizationRef = _organization.Id;
                }
            }
        }
        #endregion

        #region Object Equality
        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(OrganizationRefObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(Organization, other.Organization))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as OrganizationRefObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = Organization != null ? Organization.GetHashCode() : 0;
            return hashCode;
        }
        #endregion
    }
}