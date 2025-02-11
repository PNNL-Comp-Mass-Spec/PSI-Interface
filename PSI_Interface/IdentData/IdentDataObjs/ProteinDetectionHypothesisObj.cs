using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML ProteinDetectionHypothesisType
    /// </summary>
    /// <remarks>A single result of the ProteinDetection analysis (i.e. a protein).</remarks>
    /// <remarks>CVParams/UserParams: Scores or parameters associated with this ProteinDetectionHypothesis e.g. p-value</remarks>
    public class ProteinDetectionHypothesisObj : ParamGroupObj, IIdentifiableType, IEquatable<ProteinDetectionHypothesisObj>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ProteinDetectionHypothesisObj()
        {
            Id = null;
            Name = null;
            _dBSequenceRef = null;
            PassThreshold = false;

            _dBSequence = null;
            PeptideHypotheses = new IdentDataList<PeptideHypothesisObj>(1);
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="pdh"></param>
        /// <param name="idata"></param>
        public ProteinDetectionHypothesisObj(ProteinDetectionHypothesisType pdh, IdentDataObj idata)
            : base(pdh, idata)
        {
            Id = pdh.id;
            Name = pdh.name;
            DBSequenceRef = pdh.dBSequence_ref;
            PassThreshold = pdh.passThreshold;

            PeptideHypotheses = new IdentDataList<PeptideHypothesisObj>(1);

            if (pdh.PeptideHypothesis?.Count > 0)
            {
                PeptideHypotheses.AddRange(pdh.PeptideHypothesis, ph => new PeptideHypothesisObj(ph, IdentData));
            }
        }

        private IdentDataList<PeptideHypothesisObj> _peptideHypotheses;
        private string _dBSequenceRef;
        private DbSequenceObj _dBSequence;

        /// <summary>An identifier is an unambiguous string that is unique within the scope
        /// (i.e. a document, a set of related documents, or a repository) of its use.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Id { get; set; }

        /// <summary>The potentially ambiguous common identifier, such as a human-readable name for the instance.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Name { get; set; }

        /// <summary>min 1, max unbounded</summary>
        public IdentDataList<PeptideHypothesisObj> PeptideHypotheses
        {
            get => _peptideHypotheses;
            set
            {
                _peptideHypotheses = value;

                if (_peptideHypotheses != null)
                {
                    _peptideHypotheses.IdentData = IdentData;
                }
            }
        }

        /// <summary>A reference to the corresponding DBSequence entry.
        /// (mzIdentML 1.1) This optional and redundant, because the PeptideEvidence elements referenced from here also map to the DBSequence.
        /// (mzIdentML 1.2) Note - this attribute was optional in mzIdentML 1.1 but is now mandatory in mzIdentML 1.2. Consuming software should assume that the DBSequence entry referenced here is the definitive identifier for the protein.
        /// </summary>
        /// <remarks>
        /// Optional Attribute (mzIdentML 1.1)
        /// Required Attribute (mzIdentML 1.2)
        /// </remarks>
        protected internal string DBSequenceRef
        {
            get
            {
                if (_dBSequence != null)
                {
                    return _dBSequence.Id;
                }

                return _dBSequenceRef;
            }
            set
            {
                _dBSequenceRef = value;

                if (!string.IsNullOrWhiteSpace(value))
                {
                    DBSequence = IdentData.FindDbSequence(value);
                }
            }
        }

        /// <summary>A reference to the corresponding DBSequence entry.
        /// (mzIdentML 1.1) This optional and redundant, because the PeptideEvidence elements referenced from here also map to the DBSequence.
        /// (mzIdentML 1.2) Note - this attribute was optional in mzIdentML 1.1 but is now mandatory in mzIdentML 1.2. Consuming software should assume that the DBSequence entry referenced here is the definitive identifier for the protein.
        /// </summary>
        /// <remarks>
        /// Optional Attribute (mzIdentML 1.1)
        /// Required Attribute (mzIdentML 1.2)
        /// </remarks>
        public DbSequenceObj DBSequence
        {
            get => _dBSequence;
            set
            {
                _dBSequence = value;

                if (_dBSequence != null)
                {
                    _dBSequence.IdentData = IdentData;
                    _dBSequenceRef = _dBSequence.Id;
                }
            }
        }

        /// <summary>Set to true if the producers of the file has deemed that the ProteinDetectionHypothesis has passed a given
        /// threshold or been validated as correct. If no such threshold has been set, value of true should be given for all results.</summary>
        /// <remarks>Required Attribute</remarks>
        public bool PassThreshold { get; set; }

        #region Object Equality
        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is ProteinDetectionHypothesisObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(ProteinDetectionHypothesisObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other == null)
            {
                return false;
            }

            return Name == other.Name && PassThreshold == other.PassThreshold &&
                   Equals(DBSequence, other.DBSequence) && Equals(PeptideHypotheses, other.PeptideHypotheses) &&
                   ParamsEquals(other);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ PassThreshold.GetHashCode();
                hashCode = (hashCode * 397) ^ (DBSequence?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (PeptideHypotheses?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (CVParams?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (UserParams?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
        #endregion
    }
}
