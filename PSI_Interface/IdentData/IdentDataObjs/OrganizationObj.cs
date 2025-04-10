﻿using System;
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
        public override bool Equals(object other)
        {
            return other is OrganizationObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(OrganizationObj other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return Name == other.Name && Equals(Parent, other.Parent) &&
                   Equals(CVParams, other.CVParams) && Equals(UserParams, other.UserParams);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (Parent?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (CVParams?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (UserParams?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}
