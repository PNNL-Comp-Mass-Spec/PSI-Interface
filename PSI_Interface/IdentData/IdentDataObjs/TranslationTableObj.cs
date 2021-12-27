using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML TranslationTableType
    /// </summary>
    /// <remarks>The table used to translate codons into nucleic acids e.g. by reference to the NCBI translation table.</remarks>
    /// <remarks>
    /// The details specifying this translation table are captured as cvParams, e.g. translation table, translation
    /// start codons and translation table description (see specification document and mapping file)
    /// <remarks>min 0, max unbounded</remarks>
    /// </remarks>
    public class TranslationTableObj : CVParamGroupObj, IIdentifiableType, IEquatable<TranslationTableObj>
    {
        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="tt"></param>
        /// <param name="idata"></param>
        public TranslationTableObj(TranslationTableType tt, IdentDataObj idata)
            : base(tt, idata)
        {
            Id = tt.id;
            Name = tt.name;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TranslationTableObj()
        {
            Id = null;
            Name = null;
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
            var o = other as TranslationTableObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(TranslationTableObj other)
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
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (CVParams?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}
