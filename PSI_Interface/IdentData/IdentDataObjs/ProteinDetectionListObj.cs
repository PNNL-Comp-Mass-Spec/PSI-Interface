using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML ProteinDetectionListType
    /// </summary>
    /// <remarks>The protein list resulting from a protein detection process.</remarks>
    /// <remarks>CVParams/UserParams: Scores or output parameters associated with the whole ProteinDetectionList</remarks>
    public class ProteinDetectionListObj : ParamGroupObj, IIdentifiableType, IEquatable<ProteinDetectionListObj>
    {
        private IdentDataList<ProteinAmbiguityGroupObj> _proteinAmbiguityGroups;

        /// <summary>
        ///     Constructor
        /// </summary>
        public ProteinDetectionListObj()
        {
            Id = null;
            Name = null;

            ProteinAmbiguityGroups = new IdentDataList<ProteinAmbiguityGroupObj>(1);
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="pdl"></param>
        /// <param name="idata"></param>
        public ProteinDetectionListObj(ProteinDetectionListType pdl, IdentDataObj idata)
            : base(pdl, idata)
        {
            Id = pdl.id;
            Name = pdl.name;

            ProteinAmbiguityGroups = new IdentDataList<ProteinAmbiguityGroupObj>(1);

            if ((pdl.ProteinAmbiguityGroup != null) && (pdl.ProteinAmbiguityGroup.Count > 0))
            {
                ProteinAmbiguityGroups.AddRange(pdl.ProteinAmbiguityGroup, pag => new ProteinAmbiguityGroupObj(pag, IdentData));
            }
        }

        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<ProteinAmbiguityGroupObj> ProteinAmbiguityGroups
        {
            get { return _proteinAmbiguityGroups; }
            set
            {
                _proteinAmbiguityGroups = value;
                if (_proteinAmbiguityGroups != null)
                    _proteinAmbiguityGroups.IdentData = IdentData;
            }
        }

        /// <remarks>
        ///     An identifier is an unambiguous string that is unique within the scope
        ///     (i.e. a document, a set of related documents, or a repository) of its use.
        /// </remarks>
        /// Required Attribute
        /// string
        public string Id { get; set; }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name { get; set; }

        #region Object Equality

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as ProteinDetectionListObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ProteinDetectionListObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if ((Name == other.Name) && Equals(ProteinAmbiguityGroups, other.ProteinAmbiguityGroups) &&
                Equals(CVParams, other.CVParams) && Equals(UserParams, other.UserParams))
                return true;
            return false;
        }

        /// <summary>
        ///     Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name != null ? Name.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (ProteinAmbiguityGroups != null ? ProteinAmbiguityGroups.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}