using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML PeptideHypothesisType
    /// </summary>
    /// <remarks>Peptide evidence on which this ProteinHypothesis is based by reference to a PeptideEvidence element.</remarks>
    public class PeptideHypothesisObj : IdentDataInternalTypeAbstract, IEquatable<PeptideHypothesisObj>
    {
        private PeptideEvidenceObj _peptideEvidence;
        private string _peptideEvidenceRef;

        private IdentDataList<SpectrumIdentificationItemRefObj> _spectrumIdentificationItems;

        /// <summary>
        /// Constructor
        /// </summary>
        public PeptideHypothesisObj()
        {
            _peptideEvidenceRef = null;

            _peptideEvidence = null;
            SpectrumIdentificationItems = new IdentDataList<SpectrumIdentificationItemRefObj>(1);
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="ph"></param>
        /// <param name="idata"></param>
        public PeptideHypothesisObj(PeptideHypothesisType ph, IdentDataObj idata)
            : base(idata)
        {
            PeptideEvidenceRef = ph.peptideEvidence_ref;

            SpectrumIdentificationItems = new IdentDataList<SpectrumIdentificationItemRefObj>(1);

            if (ph.SpectrumIdentificationItemRef?.Count > 0)
            {
                SpectrumIdentificationItems.AddRange(ph.SpectrumIdentificationItemRef, spectrumIdItemRef => new SpectrumIdentificationItemRefObj(spectrumIdItemRef, IdentData));
            }
        }

        /// <summary>min 1, max unbounded</summary>
        public IdentDataList<SpectrumIdentificationItemRefObj> SpectrumIdentificationItems
        {
            get => _spectrumIdentificationItems;
            set
            {
                _spectrumIdentificationItems = value;

                if (_spectrumIdentificationItems != null)
                {
                    _spectrumIdentificationItems.IdentData = IdentData;
                }
            }
        }

        /// <summary>A reference to the PeptideEvidence element on which this hypothesis is based.</summary>
        /// <remarks>Required Attribute</remarks>
        protected internal string PeptideEvidenceRef
        {
            get
            {
                if (_peptideEvidence != null)
                {
                    return _peptideEvidence.Id;
                }

                return _peptideEvidenceRef;
            }
            set
            {
                _peptideEvidenceRef = value;

                if (!string.IsNullOrWhiteSpace(value))
                {
                    PeptideEvidence = IdentData.FindPeptideEvidence(value);
                }
            }
        }

        /// <summary>A reference to the PeptideEvidence element on which this hypothesis is based.</summary>
        /// <remarks>Required Attribute</remarks>
        public PeptideEvidenceObj PeptideEvidence
        {
            get => _peptideEvidence;
            set
            {
                _peptideEvidence = value;

                if (_peptideEvidence != null)
                {
                    _peptideEvidence.IdentData = IdentData;
                    _peptideEvidenceRef = _peptideEvidence.Id;
                }
            }
        }

        #region Object Equality
        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(PeptideHypothesisObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other == null)
            {
                return false;
            }

            return Equals(PeptideEvidence, other.PeptideEvidence) &&
                   Equals(SpectrumIdentificationItems, other.SpectrumIdentificationItems);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is PeptideHypothesisObj o && Equals(o);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = PeptideEvidence?.GetHashCode() ?? 0;
                return (hashCode * 397) ^
                           (SpectrumIdentificationItems?.GetHashCode() ?? 0);
            }
        }
        #endregion
    }
}
