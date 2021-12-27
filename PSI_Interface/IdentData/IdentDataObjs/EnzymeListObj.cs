using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML EnzymesType
    /// </summary>
    /// <remarks>The list of enzymes used in experiment</remarks>
    public class EnzymeListObj : IdentDataInternalTypeAbstract, IEquatable<EnzymeListObj>
    {
        private IdentDataList<EnzymeObj> _enzymes;
        private bool _independent;

        /// <summary>
        /// Constructor
        /// </summary>
        public EnzymeListObj()
        {
            _independent = false;
            IndependentSpecified = false;

            Enzymes = new IdentDataList<EnzymeObj>(1);
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="el"></param>
        /// <param name="idata"></param>
        public EnzymeListObj(EnzymesType el, IdentDataObj idata)
            : base(idata)
        {
            _independent = el.independent;
            IndependentSpecified = el.independentSpecified;

            Enzymes = new IdentDataList<EnzymeObj>(1);

            if ((el.Enzyme?.Count > 0))
            {
                Enzymes.AddRange(el.Enzyme, e => new EnzymeObj(e, IdentData));
            }
        }

        /// <summary>min 1, max unbounded</summary>
        public IdentDataList<EnzymeObj> Enzymes
        {
            get => _enzymes;
            set
            {
                _enzymes = value;
                if (_enzymes != null)
                    _enzymes.IdentData = IdentData;
            }
        }

        /// <summary>
        /// If there are multiple enzymes specified, this attribute is set to true if cleavage with different enzymes is
        /// performed independently.
        /// </summary>
        /// <remarks>Optional Attribute</remarks>
        public bool Independent
        {
            get => _independent;
            set
            {
                _independent = value;
                IndependentSpecified = true;
            }
        }

        /// <summary>
        /// True if Independent has been defined
        /// </summary>
        protected internal bool IndependentSpecified { get; private set; }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is EnzymeListObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(EnzymeListObj other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return Independent == other.Independent && Equals(Enzymes, other.Enzymes);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Independent.GetHashCode();
                hashCode = (hashCode * 397) ^ (Enzymes != null ? Enzymes.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}
