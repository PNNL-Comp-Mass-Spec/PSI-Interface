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
            this._id = null;
            this._name = null;
            this._dBSequenceRef = null;
            this._passThreshold = false;

            this._dBSequence = null;
            this.PeptideHypotheses = new IdentDataList<PeptideHypothesisObj>(1);
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="pdh"></param>
        /// <param name="idata"></param>
        public ProteinDetectionHypothesisObj(ProteinDetectionHypothesisType pdh, IdentDataObj idata)
            : base(pdh, idata)
        {
            this._id = pdh.id;
            this._name = pdh.name;
            this.DBSequenceRef = pdh.dBSequence_ref;
            this._passThreshold = pdh.passThreshold;

            this.PeptideHypotheses = new IdentDataList<PeptideHypothesisObj>(1);

            if (pdh.PeptideHypothesis != null && pdh.PeptideHypothesis.Count > 0)
            {
                this.PeptideHypotheses.AddRange(pdh.PeptideHypothesis, ph => new PeptideHypothesisObj(ph, this.IdentData));
            }
        }


        private IdentDataList<PeptideHypothesisObj> _peptideHypotheses;
        private string _dBSequenceRef;
        private DbSequenceObj _dBSequence;
        private bool _passThreshold;
        private string _id;
        private string _name;

        /// <summary>An identifier is an unambiguous string that is unique within the scope
        /// (i.e. a document, a set of related documents, or a repository) of its use.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Id
        {
            get => this._id;
            set => this._id = value;
        }

        /// <summary>The potentially ambiguous common identifier, such as a human-readable name for the instance.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Name
        {
            get => this._name;
            set => this._name = value;
        }

        /// <summary>min 1, max unbounded</summary>
        public IdentDataList<PeptideHypothesisObj> PeptideHypotheses
        {
            get => this._peptideHypotheses;
            set
            {
                this._peptideHypotheses = value;
                if (this._peptideHypotheses != null)
                {
                    this._peptideHypotheses.IdentData = this.IdentData;
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
                if (this._dBSequence != null)
                {
                    return this._dBSequence.Id;
                }
                return this._dBSequenceRef;
            }
            set
            {
                this._dBSequenceRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.DBSequence = this.IdentData.FindDbSequence(value);
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
            get => this._dBSequence;
            set
            {
                this._dBSequence = value;
                if (this._dBSequence != null)
                {
                    this._dBSequence.IdentData = this.IdentData;
                    this._dBSequenceRef = this._dBSequence.Id;
                }
            }
        }

        /// <summary>Set to true if the producers of the file has deemed that the ProteinDetectionHypothesis has passed a given
        /// threshold or been validated as correct. If no such threshold has been set, value of true should be given for all results.</summary>
        /// <remarks>Required Attribute</remarks>
        public bool PassThreshold
        {
            get => this._passThreshold;
            set => this._passThreshold = value;
        }

        #region Object Equality
        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as ProteinDetectionHypothesisObj;
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

            if (this.Name == other.Name && this.PassThreshold == other.PassThreshold &&
                Equals(this.DBSequence, other.DBSequence) && Equals(this.PeptideHypotheses, other.PeptideHypotheses) &&
                Equals(this.CVParams, other.CVParams) && Equals(this.UserParams, other.UserParams))
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
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ PassThreshold.GetHashCode();
                hashCode = (hashCode * 397) ^ (DBSequence != null ? DBSequence.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PeptideHypotheses != null ? PeptideHypotheses.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }
        #endregion
    }
}