using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML EnzymeType
    /// </summary>
    /// <remarks>
    /// The details of an individual cleavage enzyme should be provided by giving a regular expression
    /// or a CV term if a "standard" enzyme cleavage has been performed.
    /// </remarks>
    public class EnzymeObj : IdentDataInternalTypeAbstract, IIdentifiableType, IEquatable<EnzymeObj>
    {
        private ParamListObj _enzymeName;
        private int _minDistance;
        private int _missedCleavages;
        private bool _semiSpecific;

        /// <summary>
        /// Constructor
        /// </summary>
        public EnzymeObj()
        {
            Id = null;
            Name = null;
            SiteRegexp = null;
            NTermGain = null;
            CTermGain = null;
            _semiSpecific = false;
            SemiSpecificSpecified = false;
            _missedCleavages = -1;
            MissedCleavagesSpecified = false;
            _minDistance = -1;
            MinDistanceSpecified = false;

            _enzymeName = null;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="e"></param>
        /// <param name="idata"></param>
        public EnzymeObj(EnzymeType e, IdentDataObj idata)
            : base(idata)
        {
            Id = e.id;
            Name = e.name;
            SiteRegexp = e.SiteRegexp;
            NTermGain = e.nTermGain;
            CTermGain = e.cTermGain;
            _semiSpecific = e.semiSpecific;
            SemiSpecificSpecified = e.semiSpecificSpecified;
            _missedCleavages = e.missedCleavages;
            MissedCleavagesSpecified = e.missedCleavagesSpecified;
            _minDistance = e.minDistance;
            MinDistanceSpecified = e.minDistanceSpecified;

            _enzymeName = null;

            if (e.EnzymeName != null)
                _enzymeName = new ParamListObj(e.EnzymeName, IdentData);
        }

        /// <summary>Regular expression for specifying the enzyme cleavage site.</summary>
        /// <remarks>min 0, max 1</remarks>
        public string SiteRegexp { get; set; }

        /// <summary>The name of the enzyme from a CV.</summary>
        /// <remarks>min 0, max 1</remarks>
        public ParamListObj EnzymeName
        {
            get => _enzymeName;
            set
            {
                _enzymeName = value;
                if (_enzymeName != null)
                    _enzymeName.IdentData = IdentData;
            }
        }

        /// <summary>Element formula gained at NTerm.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>RegEx: "[A-Za-z0-9 ]+"</returns>
        public string NTermGain { get; set; }

        /// <summary>Element formula gained at CTerm.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>RegEx: "[A-Za-z0-9 ]+"</returns>
        public string CTermGain { get; set; }

        /// <summary>
        /// Set to true if the enzyme cleaves semi-specifically (i.e. one terminus must cleave
        /// according to the rules, the other can cleave at any residue), false if the enzyme cleavage
        /// is assumed to be specific to both termini (accepting for any missed cleavages).
        /// </summary>
        /// <remarks>Optional Attribute</remarks>
        public bool SemiSpecific
        {
            get => _semiSpecific;
            set
            {
                _semiSpecific = value;
                SemiSpecificSpecified = true;
            }
        }

        /// <summary>
        /// True if Semi-specific has been defined
        /// </summary>
        protected internal bool SemiSpecificSpecified { get; private set; }

        /// <summary>
        /// The number of missed cleavage sites allowed by the search. The attribute must be provided if an enzyme has
        /// been used.
        /// </summary>
        /// <remarks>Optional Attribute</remarks>
        public int MissedCleavages
        {
            get => _missedCleavages;
            set
            {
                _missedCleavages = value;
                MissedCleavagesSpecified = true;
            }
        }

        /// <summary>
        /// True if Missed Cleavages has been defined
        /// </summary>
        protected internal bool MissedCleavagesSpecified { get; private set; }

        /// <summary>Minimal distance for another cleavage (minimum: 1).</summary>
        /// <remarks>Optional Attribute</remarks>
        public int MinDistance
        {
            get => _minDistance;
            set
            {
                _minDistance = value;
                MinDistanceSpecified = true;
            }
        }

        /// <summary>
        /// True if Minimal distance has been defined
        /// </summary>
        protected internal bool MinDistanceSpecified { get; private set; }

        /// <summary>
        /// An identifier is an unambiguous string that is unique within the scope
        /// (i.e. a document, a set of related documents, or a repository) of its use.
        /// </summary>
        /// <remarks>Required Attribute</remarks>
        public string Id { get; set; }

        /// <summary>The potentially ambiguous common identifier, such as a human-readable name for the instance.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Name { get; set; }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is EnzymeObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(EnzymeObj other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return Name == other.Name && SiteRegexp == other.SiteRegexp && NTermGain == other.NTermGain &&
                   CTermGain == other.CTermGain && SemiSpecific == other.SemiSpecific &&
                   MissedCleavages == other.MissedCleavages && MinDistance == other.MinDistance &&
                   Equals(EnzymeName, other.EnzymeName);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name != null ? Name.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (SiteRegexp != null ? SiteRegexp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (NTermGain != null ? NTermGain.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CTermGain != null ? CTermGain.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ SemiSpecific.GetHashCode();
                hashCode = (hashCode * 397) ^ MissedCleavages.GetHashCode();
                hashCode = (hashCode * 397) ^ MinDistance.GetHashCode();
                hashCode = (hashCode * 397) ^ (EnzymeName != null ? EnzymeName.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}
