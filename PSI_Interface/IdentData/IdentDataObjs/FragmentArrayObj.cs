using System;
using System.Collections.Generic;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML FragmentArrayType
    /// </summary>
    /// <remarks>
    /// An array of values for a given type of measure and for a particular ion type, in parallel to the index of ions
    /// identified.
    /// </remarks>
    public class FragmentArrayObj : IdentDataInternalTypeAbstract, IEquatable<FragmentArrayObj>
    {
        private MeasureObj _measure;
        private string _measureRef;

        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentArrayObj()
        {
            _measureRef = null;

            _measure = null;
            Values = new List<float>(1);
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="fa"></param>
        /// <param name="idata"></param>
        public FragmentArrayObj(FragmentArrayType fa, IdentDataObj idata)
            : base(idata)
        {
            MeasureRef = fa.measure_ref;

            Values = new List<float>(1);
            if (fa.values != null)
            {
                Values = new List<float>(fa.values);
            }
        }

        /// <summary>The values of this particular measure, corresponding to the index defined in ion type</summary>
        /// <remarks>Required Attribute</remarks>
        public List<float> Values { get; set; }

        /// <summary>A reference to the Measure defined in the FragmentationTable</summary>
        /// <remarks>Required Attribute</remarks>
        protected internal string MeasureRef
        {
            get
            {
                if (_measure != null)
                {
                    return _measure.Id;
                }
                return _measureRef;
            }
            internal set
            {
                _measureRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    Measure = IdentData.FindMeasure(value);
                }
            }
        }

        /// <summary>A reference to the Measure defined in the FragmentationTable</summary>
        /// <remarks>Required Attribute</remarks>
        public MeasureObj Measure
        {
            get => _measure;
            set
            {
                _measure = value;
                if (_measure != null)
                {
                    _measure.IdentData = IdentData;
                    _measureRef = _measure.Id;
                }
            }
        }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is FragmentArrayObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(FragmentArrayObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other == null)
            {
                return false;
            }

            return Equals(Measure, other.Measure) && Equals(Values, other.Values);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Measure?.GetHashCode() ?? 0;
                return (hashCode * 397) ^ (Values?.GetHashCode() ?? 0);
            }
        }

        #endregion
    }
}
