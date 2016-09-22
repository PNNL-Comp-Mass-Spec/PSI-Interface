using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML EnzymeType
    /// </summary>
    /// <remarks>
    ///     The details of an individual cleavage enzyme should be provided by giving a regular expression
    ///     or a CV term if a "standard" enzyme cleavage has been performed.
    /// </remarks>
    public class EnzymeObj : IdentDataInternalTypeAbstract, IIdentifiableType, IEquatable<EnzymeObj>
    {
        private ParamListObj _enzymeName;
        private int _minDistance;
        private int _missedCleavages;
        private bool _semiSpecific;

        /// <summary>
        ///     Constructor
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
        ///     Create an object using the contents of the corresponding MzIdentML object
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

        /// <remarks>Regular expression for specifying the enzyme cleavage site.</remarks>
        /// <remarks>min 0, max 1</remarks>
        public string SiteRegexp { get; set; }

        /// <remarks>The name of the enzyme from a CV.</remarks>
        /// <remarks>min 0, max 1</remarks>
        public ParamListObj EnzymeName
        {
            get { return _enzymeName; }
            set
            {
                _enzymeName = value;
                if (_enzymeName != null)
                    _enzymeName.IdentData = IdentData;
            }
        }

        /// <remarks>Element formula gained at NTerm.</remarks>
        /// Optional Attribute
        /// string, regex: "[A-Za-z0-9 ]+"
        public string NTermGain { get; set; }

        /// <remarks>Element formula gained at CTerm.</remarks>
        /// Optional Attribute
        /// string, regex: "[A-Za-z0-9 ]+"
        public string CTermGain { get; set; }

        /// <remarks>
        ///     Set to true if the enzyme cleaves semi-specifically (i.e. one terminus must cleave
        ///     according to the rules, the other can cleave at any residue), false if the enzyme cleavage
        ///     is assumed to be specific to both termini (accepting for any missed cleavages).
        /// </remarks>
        /// Optional Attribute
        /// boolean
        public bool SemiSpecific
        {
            get { return _semiSpecific; }
            set
            {
                _semiSpecific = value;
                SemiSpecificSpecified = true;
            }
        }

        /// Attribute Existence
        protected internal bool SemiSpecificSpecified { get; private set; }

        /// <remarks>
        ///     The number of missed cleavage sites allowed by the search. The attribute must be provided if an enzyme has
        ///     been used.
        /// </remarks>
        /// Optional Attribute
        /// integer
        public int MissedCleavages
        {
            get { return _missedCleavages; }
            set
            {
                _missedCleavages = value;
                MissedCleavagesSpecified = true;
            }
        }

        /// Attribute Existence
        protected internal bool MissedCleavagesSpecified { get; private set; }

        /// <remarks>Minimal distance for another cleavage (minimum: 1).</remarks>
        /// Optional Attribute
        /// integer >= 1
        public int MinDistance
        {
            get { return _minDistance; }
            set
            {
                _minDistance = value;
                MinDistanceSpecified = true;
            }
        }

        /// Attribute Existence
        protected internal bool MinDistanceSpecified { get; private set; }

        /// <remarks>
        ///     An identifier is an unambiguous string that is unique within the scope
        ///     (i.e. a document, a set of related documents, or a repository) of its use.
        /// </remarks>
        /// Required Attribute
        /// string
        public string Id { get; set; }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name { get; set; }

        #region Object Equality

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as EnzymeObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(EnzymeObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if ((Name == other.Name) && (SiteRegexp == other.SiteRegexp) && (NTermGain == other.NTermGain) &&
                (CTermGain == other.CTermGain) && (SemiSpecific == other.SemiSpecific) &&
                (MissedCleavages == other.MissedCleavages) && (MinDistance == other.MinDistance) &&
                Equals(EnzymeName, other.EnzymeName))
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