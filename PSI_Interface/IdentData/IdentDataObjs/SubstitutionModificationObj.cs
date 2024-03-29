﻿using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML SubstitutionModificationType
    /// </summary>
    /// <remarks>A modification where one residue is substituted by another (amino acid change).</remarks>
    public class SubstitutionModificationObj : IdentDataInternalTypeAbstract, IEquatable<SubstitutionModificationObj>
    {
        private double _avgMassDelta;
        private int _location;
        private double _monoisotopicMassDelta;

        /// <summary>
        /// Constructor
        /// </summary>
        public SubstitutionModificationObj()
        {
            OriginalResidue = null;
            ReplacementResidue = null;
            _location = -1;
            LocationSpecified = false;
            _avgMassDelta = 0;
            AvgMassDeltaSpecified = false;
            _monoisotopicMassDelta = 0;
            MonoisotopicMassDeltaSpecified = false;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="sm"></param>
        /// <param name="idata"></param>
        public SubstitutionModificationObj(SubstitutionModificationType sm, IdentDataObj idata)
            : base(idata)
        {
            OriginalResidue = sm.originalResidue;
            ReplacementResidue = sm.replacementResidue;
            _location = sm.location;
            LocationSpecified = sm.locationSpecified;
            _avgMassDelta = sm.avgMassDelta;
            AvgMassDeltaSpecified = sm.avgMassDeltaSpecified;
            _monoisotopicMassDelta = sm.monoisotopicMassDelta;
            MonoisotopicMassDeltaSpecified = sm.monoisotopicMassDeltaSpecified;
        }

        /// <summary>The original residue before replacement.</summary>
        /// <remarks>Required Attribute</remarks>
        /// <returns>RegEx: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"</returns>
        public string OriginalResidue { get; set; }

        /// <summary>The residue that replaced the originalResidue.</summary>
        /// <remarks>Required Attribute</remarks>
        /// <returns>RegEx: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"</returns>
        public string ReplacementResidue { get; set; }

        /// <summary>
        /// Location of the modification within the peptide - position in peptide sequence, counted from the N-terminus
        /// residue, starting at position 1.
        /// Specific modifications to the N-terminus should be given the location 0.
        /// Modification to the C-terminus should be given as peptide length + 1.
        /// </summary>
        /// <remarks>Optional Attribute</remarks>
        public int Location
        {
            get => _location;
            set
            {
                _location = value;
                LocationSpecified = true;
            }
        }

        /// <summary>
        /// True if Location has been defined
        /// </summary>
        protected internal bool LocationSpecified { get; private set; }

        /// <summary>
        /// Atomic mass delta considering the natural distribution of isotopes in Daltons.
        /// This should only be reported if the original amino acid is known i.e. it is not "X"
        /// </summary>
        /// <remarks>Optional Attribute</remarks>
        public double AvgMassDelta
        {
            get => _avgMassDelta;
            set
            {
                _avgMassDelta = value;
                AvgMassDeltaSpecified = true;
            }
        }

        /// <summary>
        /// True if average mass delta has been defined
        /// </summary>
        protected internal bool AvgMassDeltaSpecified { get; private set; }

        /// <summary>
        /// Atomic mass delta when assuming only the most common isotope of elements in Daltons.
        /// This should only be reported if the original amino acid is known i.e. it is not "X"
        /// </summary>
        /// <remarks>Optional Attribute</remarks>
        public double MonoisotopicMassDelta
        {
            get => _monoisotopicMassDelta;
            set
            {
                _monoisotopicMassDelta = value;
                MonoisotopicMassDeltaSpecified = true;
            }
        }

        /// <summary>
        /// True if monoisotopic mass delta has been defined
        /// </summary>
        protected internal bool MonoisotopicMassDeltaSpecified { get; private set; }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is SubstitutionModificationObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(SubstitutionModificationObj other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return AvgMassDelta.Equals(other.AvgMassDelta) && Location == other.Location &&
                   MonoisotopicMassDelta.Equals(other.MonoisotopicMassDelta) &&
                   OriginalResidue == other.OriginalResidue && ReplacementResidue == other.ReplacementResidue;
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = AvgMassDelta.GetHashCode();
                hashCode = (hashCode * 397) ^ Location;
                hashCode = (hashCode * 397) ^ MonoisotopicMassDelta.GetHashCode();
                hashCode = (hashCode * 397) ^ (OriginalResidue?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (ReplacementResidue?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}
