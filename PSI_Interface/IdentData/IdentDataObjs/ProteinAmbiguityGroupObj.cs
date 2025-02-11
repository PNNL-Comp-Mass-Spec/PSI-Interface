using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML ProteinAmbiguityGroupType
    /// </summary>
    /// <remarks>
    /// A set of logically related results from a protein detection, for example to represent conflicting assignments
    /// of peptides to proteins.
    /// </remarks>
    /// <remarks>CVParams/UserParams: Scores or parameters associated with the ProteinAmbiguityGroup.</remarks>
    public class ProteinAmbiguityGroupObj : ParamGroupObj, IIdentifiableType, IEquatable<ProteinAmbiguityGroupObj>
    {
        private IdentDataList<ProteinDetectionHypothesisObj> _proteinDetectionHypotheses;

        /// <summary>
        /// Constructor
        /// </summary>
        public ProteinAmbiguityGroupObj()
        {
            Id = null;
            Name = null;

            ProteinDetectionHypotheses = new IdentDataList<ProteinDetectionHypothesisObj>(1);
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="pag"></param>
        /// <param name="idata"></param>
        public ProteinAmbiguityGroupObj(ProteinAmbiguityGroupType pag, IdentDataObj idata)
            : base(pag, idata)
        {
            Id = pag.id;
            Name = pag.name;

            ProteinDetectionHypotheses = new IdentDataList<ProteinDetectionHypothesisObj>(1);

            if (pag.ProteinDetectionHypothesis?.Count > 0)
            {
                ProteinDetectionHypotheses.AddRange(pag.ProteinDetectionHypothesis, pdh => new ProteinDetectionHypothesisObj(pdh, IdentData));
            }
        }

        /// <summary>min 1, max unbounded</summary>
        public IdentDataList<ProteinDetectionHypothesisObj> ProteinDetectionHypotheses
        {
            get => _proteinDetectionHypotheses;
            set
            {
                _proteinDetectionHypotheses = value;

                if (_proteinDetectionHypotheses != null)
                    _proteinDetectionHypotheses.IdentData = IdentData;
            }
        }

        /// <summary>
        /// An identifier is an unambiguous string that is unique within the scope
        /// (i.e. a document, a set of related documents, or a repository) of its use.
        /// </summary>
        /// <remarks>Required Attribute</remarks>
        public string Id { get; set; }

        /// <summary>The potentially ambiguous common identifier, such as a human-readable name for the instance.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Name { get; set; }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is ProteinAmbiguityGroupObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(ProteinAmbiguityGroupObj other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return Name == other.Name && Equals(ProteinDetectionHypotheses, other.ProteinDetectionHypotheses) &&
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
                hashCode = (hashCode * 397) ^ (ProteinDetectionHypotheses?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (CVParams?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (UserParams?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}
