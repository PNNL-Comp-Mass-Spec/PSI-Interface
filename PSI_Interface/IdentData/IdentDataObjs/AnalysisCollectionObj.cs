﻿using System;
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

            if (ac.SpectrumIdentification?.Count > 0)
            {
                SpectrumIdentifications.AddRange(ac.SpectrumIdentification, si => new SpectrumIdentificationObj(si, IdentData));
            }

            if (ac.ProteinDetection != null)
            {
                ProteinDetection = new ProteinDetectionObj(ac.ProteinDetection, IdentData);
            }
        }

        /// <summary>min 1, max unbounded</summary>
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

        /// <summary>min 0, max 1</summary>
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
        public override bool Equals(object other)
        {
            return other is AnalysisCollectionObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
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

            return Equals(ProteinDetection, other.ProteinDetection) &&
                   Equals(SpectrumIdentifications, other.SpectrumIdentifications);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = ProteinDetection?.GetHashCode() ?? 0;
                return (hashCode * 397) ^
                           (SpectrumIdentifications?.GetHashCode() ?? 0);
            }
        }

        #endregion
    }
}
