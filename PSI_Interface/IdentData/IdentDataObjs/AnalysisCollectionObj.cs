using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML AnalysisCollectionType
    /// </summary>
    /// <remarks>
    /// The analyses performed to get the results, which map the input and output data sets.
    /// Analyses are for example: SpectrumIdentification (resulting in peptides) or ProteinDetection (assemble proteins
    /// from peptides).
    /// </remarks>
    public class AnalysisCollectionObj : IdentDataInternalTypeAbstract, IEquatable<AnalysisCollectionObj>
    {
        private ProteinDetectionObj _proteinDetection;

        private IdentDataList<SpectrumIdentificationObj> _spectrumIdentifications;

        /// <summary>
        /// Constructor
        /// </summary>
        public AnalysisCollectionObj()
        {
            SpectrumIdentifications = new IdentDataList<SpectrumIdentificationObj>(1);
            _proteinDetection = null;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="idata"></param>
        public AnalysisCollectionObj(AnalysisCollectionType ac, IdentDataObj idata)
            : base(idata)
        {
            SpectrumIdentifications = new IdentDataList<SpectrumIdentificationObj>(1);
            _proteinDetection = null;

            idata.AnalysisCollection = this;

            if (ac.SpectrumIdentification != null && ac.SpectrumIdentification.Count > 0)
            {
                SpectrumIdentifications.AddRange(ac.SpectrumIdentification, si => new SpectrumIdentificationObj(si, IdentData));
            }
            if (ac.ProteinDetection != null)
            {
                ProteinDetection = new ProteinDetectionObj(ac.ProteinDetection, IdentData);
            }
        }

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<SpectrumIdentificationObj> SpectrumIdentifications
        {
            get => _spectrumIdentifications;
            set
            {
                _spectrumIdentifications = value;
                if (_spectrumIdentifications != null)
                {
                    _spectrumIdentifications.IdentData = IdentData;
                }
            }
        }

        /// <remarks>min 0, max 1</remarks>
        public ProteinDetectionObj ProteinDetection
        {
            get => _proteinDetection;
            set
            {
                _proteinDetection = value;
                if (_proteinDetection != null)
                {
                    _proteinDetection.IdentData = IdentData;
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
            var o = other as AnalysisCollectionObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(AnalysisCollectionObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(ProteinDetection, other.ProteinDetection) &&
                Equals(SpectrumIdentifications, other.SpectrumIdentifications))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = ProteinDetection != null ? ProteinDetection.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^
                           (SpectrumIdentifications != null ? SpectrumIdentifications.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}