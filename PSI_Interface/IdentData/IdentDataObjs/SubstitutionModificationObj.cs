using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML SubstitutionModificationType
    /// </summary>
    /// <remarks>A modification where one residue is substituted by another (amino acid change).</remarks>
    public class SubstitutionModificationObj : IdentDataInternalTypeAbstract, IEquatable<SubstitutionModificationObj>
    {
        private double _avgMassDelta;
        private int _location;
        private double _monoisotopicMassDelta;

        /// <summary>
        ///     Constructor
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
        ///     Create an object using the contents of the corresponding MzIdentML object
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

        /// <remarks>The original residue before replacement.</remarks>
        /// Required Attribute
        /// string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"
        public string OriginalResidue { get; set; }

        /// <remarks>The residue that replaced the originalResidue.</remarks>
        /// Required Attribute
        /// string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"
        public string ReplacementResidue { get; set; }

        /// <remarks>
        ///     Location of the modification within the peptide - position in peptide sequence, counted from the N-terminus
        ///     residue, starting at position 1.
        ///     Specific modifications to the N-terminus should be given the location 0.
        ///     Modification to the C-terminus should be given as peptide length + 1.
        /// </remarks>
        /// Optional Attribute
        /// integer
        public int Location
        {
            get => _location;
            set
            {
                _location = value;
                LocationSpecified = true;
            }
        }

        /// Attribute Existence
        protected internal bool LocationSpecified { get; private set; }

        /// <remarks>
        ///     Atomic mass delta considering the natural distribution of isotopes in Daltons.
        ///     This should only be reported if the original amino acid is known i.e. it is not "X"
        /// </remarks>
        /// Optional Attribute
        /// double
        public double AvgMassDelta
        {
            get => _avgMassDelta;
            set
            {
                _avgMassDelta = value;
                AvgMassDeltaSpecified = true;
            }
        }

        /// Attribute Existence
        protected internal bool AvgMassDeltaSpecified { get; private set; }

        /// <remarks>
        ///     Atomic mass delta when assuming only the most common isotope of elements in Daltons.
        ///     This should only be reported if the original amino acid is known i.e. it is not "X"
        /// </remarks>
        /// Optional Attribute
        /// double
        public double MonoisotopicMassDelta
        {
            get => _monoisotopicMassDelta;
            set
            {
                _monoisotopicMassDelta = value;
                MonoisotopicMassDeltaSpecified = true;
            }
        }

        /// Attribute Existence
        protected internal bool MonoisotopicMassDeltaSpecified { get; private set; }

        #region Object Equality

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as SubstitutionModificationObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(SubstitutionModificationObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if (AvgMassDelta.Equals(other.AvgMassDelta) && (Location == other.Location) &&
                MonoisotopicMassDelta.Equals(other.MonoisotopicMassDelta) &&
                (OriginalResidue == other.OriginalResidue) && (ReplacementResidue == other.ReplacementResidue))
                return true;
            return false;
        }

        /// <summary>
        ///     Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = AvgMassDelta.GetHashCode();
                hashCode = (hashCode * 397) ^ Location;
                hashCode = (hashCode * 397) ^ MonoisotopicMassDelta.GetHashCode();
                hashCode = (hashCode * 397) ^ (OriginalResidue != null ? OriginalResidue.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ReplacementResidue != null ? ReplacementResidue.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}