﻿using System;

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
        public bool Equals(OrganizationRefObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return other != null && Equals(Organization, other.Organization);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is OrganizationRefObj o && Equals(o);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            return Organization?.GetHashCode() ?? 0;
        }
        #endregion
    }
}
