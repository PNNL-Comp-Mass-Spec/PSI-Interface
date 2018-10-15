using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML SubSampleType
    /// </summary>
    /// <remarks>References to the individual component samples within a mixed parent sample.</remarks>
    public class SubSampleObj : IdentDataInternalTypeAbstract, IEquatable<SubSampleObj>
    {
        private SampleObj _sample;

        private string _sampleRef;

        /// <summary>
        /// Constructor
        /// </summary>
        public SubSampleObj()
        {
            _sampleRef = null;

            _sample = null;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="ss"></param>
        /// <param name="idata"></param>
        public SubSampleObj(SubSampleType ss, IdentDataObj idata)
            : base(idata)
        {
            SampleRef = ss.sample_ref;
        }

        /// <remarks>A reference to the child sample.</remarks>
        /// Required  Attribute
        /// string
        protected internal string SampleRef
        {
            get
            {
                if (_sample != null)
                    return _sample.Id;
                return _sampleRef;
            }
            set
            {
                _sampleRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                    Sample = IdentData.FindSample(value);
            }
        }

        /// <remarks>A reference to the child sample.</remarks>
        /// Required  Attribute
        /// string
        public SampleObj Sample
        {
            get => _sample;
            set
            {
                _sample = value;
                if (_sample != null)
                {
                    _sample.IdentData = IdentData;
                    _sampleRef = _sample.Id;
                }
            }
        }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as SubSampleObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(SubSampleObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if (Equals(Sample, other.Sample))
                return true;
            return false;
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = Sample != null ? Sample.GetHashCode() : 0;
            return hashCode;
        }

        #endregion
    }
}