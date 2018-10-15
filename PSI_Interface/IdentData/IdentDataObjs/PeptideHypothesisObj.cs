using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML PeptideHypothesisType
    /// </summary>
    /// <remarks>Peptide evidence on which this ProteinHypothesis is based by reference to a PeptideEvidence element.</remarks>
    public class PeptideHypothesisObj : IdentDataInternalTypeAbstract, IEquatable<PeptideHypothesisObj>
    {
        private PeptideEvidenceObj _peptideEvidence;
        private string _peptideEvidenceRef;

        private IdentDataList<SpectrumIdentificationItemRefObj> _spectrumIdentificationItems;

        /// <summary>
        ///     Constructor
        /// </summary>
        public PeptideHypothesisObj()
        {
            _peptideEvidenceRef = null;

            _peptideEvidence = null;
            SpectrumIdentificationItems = new IdentDataList<SpectrumIdentificationItemRefObj>(1);
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="ph"></param>
        /// <param name="idata"></param>
        public PeptideHypothesisObj(PeptideHypothesisType ph, IdentDataObj idata)
            : base(idata)
        {
            PeptideEvidenceRef = ph.peptideEvidence_ref;

            SpectrumIdentificationItems = new IdentDataList<SpectrumIdentificationItemRefObj>(1);

            if (ph.SpectrumIdentificationItemRef != null && ph.SpectrumIdentificationItemRef.Count > 0)
            {
                SpectrumIdentificationItems.AddRange(ph.SpectrumIdentificationItemRef, spectrumIdItemRef => new SpectrumIdentificationItemRefObj(spectrumIdItemRef, IdentData));
            }
        }

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<SpectrumIdentificationItemRefObj> SpectrumIdentificationItems
        {
            get { return _spectrumIdentificationItems; }
            set
            {
                _spectrumIdentificationItems = value;
                if (_spectrumIdentificationItems != null)
                {
                    _spectrumIdentificationItems.IdentData = IdentData;
                }
            }
        }

        /// <remarks>A reference to the PeptideEvidence element on which this hypothesis is based.</remarks>
        /// Required Attribute
        /// string
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

        /// <remarks>A reference to the PeptideEvidence element on which this hypothesis is based.</remarks>
        /// Required Attribute
        /// string
        public PeptideEvidenceObj PeptideEvidence
        {
            get { return _peptideEvidence; }
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
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
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

            if (Equals(PeptideEvidence, other.PeptideEvidence) &&
                Equals(SpectrumIdentificationItems, other.SpectrumIdentificationItems))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as PeptideHypothesisObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        /// <summary>
        ///     Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = PeptideEvidence != null ? PeptideEvidence.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^
                           (SpectrumIdentificationItems != null ? SpectrumIdentificationItems.GetHashCode() : 0);
                return hashCode;
            }
        }
        #endregion
    }
}