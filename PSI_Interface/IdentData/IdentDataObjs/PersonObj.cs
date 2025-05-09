﻿using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML PersonType
    /// </summary>
    /// <remarks>
    /// A person's name and contact details. Any additional information such as the address,
    /// contact email etc. should be supplied using CV parameters or user parameters.
    /// </remarks>
    public class PersonObj : AbstractContactObj, IEquatable<PersonObj>
    {
        private IdentDataList<AffiliationObj> _affiliations;

        /// <summary>
        /// Constructor
        /// </summary>
        public PersonObj()
        {
            LastName = null;
            FirstName = null;
            MidInitials = null;

            Affiliations = new IdentDataList<AffiliationObj>(1);
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="p"></param>
        /// <param name="idata"></param>
        public PersonObj(PersonType p, IdentDataObj idata)
            : base(p, idata)
        {
            LastName = p.lastName;
            FirstName = p.firstName;
            MidInitials = p.midInitials;

            Affiliations = new IdentDataList<AffiliationObj>(1);

            if (p.Affiliation?.Count > 0)
            {
                Affiliations.AddRange(p.Affiliation, a => new AffiliationObj(a, IdentData));
            }
        }

        /// <summary>The organization a person belongs to.</summary>
        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<AffiliationObj> Affiliations
        {
            get => _affiliations;
            set
            {
                _affiliations = value;

                if (_affiliations != null)
                    _affiliations.IdentData = IdentData;
            }
        }

        /// <summary>The Person's last/family name.</summary>
        /// <remarks>Optional Attribute</remarks>
        public string LastName { get; set; }

        /// <summary>The Person's first name.</summary>
        /// <remarks>Optional Attribute</remarks>
        public string FirstName { get; set; }

        /// <summary>The Person's middle initial.</summary>
        /// <remarks>Optional Attribute</remarks>
        public string MidInitials { get; set; }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is PersonObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(PersonObj other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return Name == other.Name && LastName == other.LastName && FirstName == other.FirstName &&
                   MidInitials == other.MidInitials && Equals(Affiliations, other.Affiliations) &&
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
                hashCode = (hashCode * 397) ^ (LastName?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (FirstName?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (MidInitials?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Affiliations?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (CVParams?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (UserParams?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}
