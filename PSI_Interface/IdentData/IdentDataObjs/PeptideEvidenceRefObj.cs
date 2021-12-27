using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML PeptideEvidenceRefType
    /// </summary>
    /// <remarks>
    /// Reference to the PeptideEvidence element identified. If a specific sequence can be assigned to multiple
    /// proteins and or positions in a protein all possible PeptideEvidence elements should be referenced here.
    /// </remarks>
    public class PeptideEvidenceRefObj : IdentDataInternalTypeAbstract, IEquatable<PeptideEvidenceRefObj>
    {
        private PeptideEvidenceObj _peptideEvidence;
        private string _peptideEvidenceRef;

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public PeptideEvidenceRefObj(PeptideEvidenceObj pepEv = null)
        {
            _peptideEvidenceRef = null;

            _peptideEvidence = pepEv;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="per"></param>
        /// <param name="idata"></param>
        public PeptideEvidenceRefObj(PeptideEvidenceRefType per, IdentDataObj idata)
            : base(idata)
        {
            PeptideEvidenceRef = per.peptideEvidence_ref;
        }
        #endregion

        #region Properties
        /// <summary>A reference to the PeptideEvidenceItem element(s).</summary>
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

        /// <summary>A reference to the PeptideEvidenceItem element(s).</summary>
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
        #endregion

        #region Object Equality
        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(PeptideEvidenceRefObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return other != null && Equals(PeptideEvidence, other.PeptideEvidence);
        }


        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is PeptideEvidenceRefObj o && Equals(o);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            return PeptideEvidence?.GetHashCode() ?? 0;
        }
        #endregion
    }
}
