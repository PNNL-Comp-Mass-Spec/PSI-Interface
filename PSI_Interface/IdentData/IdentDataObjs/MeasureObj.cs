using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML MeasureType; list form is FragmentationTableType
    /// </summary>
    /// <remarks>References to CV terms defining the measures about product ions to be reported in SpectrumIdentificationItem</remarks>
    /// <remarks>
    ///     FragmentationTableType: Contains the types of measures that will be reported in generic arrays
    ///     for each SpectrumIdentificationItem e.g. product ion m/z, product ion intensity, product ion m/z error
    /// </remarks>
    /// <remarks>FragmentationTableType: child element Measure of type MeasureType, min 1, max unbounded</remarks>
    /// <remarks>CVParams: min 1, max unbounded</remarks>
    public class MeasureObj : CVParamGroupObj, IIdentifiableType, IEquatable<MeasureObj>
    {
        //private IdentDataList<CVParamType> _cvParams;

        /// <summary>
        ///     Constructor
        /// </summary>
        public MeasureObj()
        {
            Id = null;
            Name = null;
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="m"></param>
        /// <param name="idata"></param>
        public MeasureObj(MeasureType m, IdentDataObj idata)
            : base(m, idata)
        {
            Id = m.id;
            Name = m.name;
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
            var o = other as MeasureObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(MeasureObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if ((Name == other.Name) && Equals(CVParams, other.CVParams))
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
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}