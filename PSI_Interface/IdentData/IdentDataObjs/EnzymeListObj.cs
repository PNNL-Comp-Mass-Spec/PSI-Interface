using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML EnzymesType
    /// </summary>
    /// <remarks>The list of enzymes used in experiment</remarks>
    public class EnzymeListObj : IdentDataInternalTypeAbstract, IEquatable<EnzymeListObj>
    {
        private IdentDataList<EnzymeObj> _enzymes;
        private bool _independent;

        /// <summary>
        ///     Constructor
        /// </summary>
        public EnzymeListObj()
        {
            _independent = false;
            IndependentSpecified = false;

            Enzymes = new IdentDataList<EnzymeObj>();
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="el"></param>
        /// <param name="idata"></param>
        public EnzymeListObj(EnzymesType el, IdentDataObj idata)
            : base(idata)
        {
            _independent = el.independent;
            IndependentSpecified = el.independentSpecified;

            _enzymes = null;

            if ((el.Enzyme != null) && (el.Enzyme.Count > 0))
            {
                Enzymes = new IdentDataList<EnzymeObj>();
                foreach (var e in el.Enzyme)
                    Enzymes.Add(new EnzymeObj(e, IdentData));
            }
        }

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<EnzymeObj> Enzymes
        {
            get { return _enzymes; }
            set
            {
                _enzymes = value;
                if (_enzymes != null)
                    _enzymes.IdentData = IdentData;
            }
        }

        /// <remarks>
        ///     If there are multiple enzymes specified, this attribute is set to true if cleavage with different enzymes is
        ///     performed independently.
        /// </remarks>
        /// Optional Attribute
        /// boolean
        public bool Independent
        {
            get { return _independent; }
            set
            {
                _independent = value;
                IndependentSpecified = true;
            }
        }

        /// Attribute Existence
        protected internal bool IndependentSpecified { get; private set; }

        #region Object Equality

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as EnzymeListObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(EnzymeListObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if ((Independent == other.Independent) && Equals(Enzymes, other.Enzymes))
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
                var hashCode = Independent.GetHashCode();
                hashCode = (hashCode * 397) ^ (Enzymes != null ? Enzymes.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}