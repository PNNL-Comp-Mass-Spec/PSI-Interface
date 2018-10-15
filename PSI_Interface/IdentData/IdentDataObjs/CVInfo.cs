using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML cvType : Container CVListType
    /// </summary>
    /// <remarks>A source controlled vocabulary from which cvParams will be obtained.</remarks>
    /// <remarks>CVListType: The list of controlled vocabularies used in the file.</remarks>
    /// <remarks>CVListType: child element cv of type cvType, min 1, max unbounded</remarks>
    public class CVInfo : IdentDataInternalTypeAbstract, IEquatable<CVInfo>
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public CVInfo()
        {
            FullName = null;
            Version = null;
            URI = null;
            Id = null;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="cv"></param>
        /// <param name="idata"></param>
        public CVInfo(cvType cv, IdentDataObj idata)
            : base(idata)
        {
            FullName = cv.fullName;
            Version = cv.version;
            URI = cv.uri;
            Id = cv.id;
        }
        #endregion

        #region Properties
        /// <remarks>The full name of the CV.</remarks>
        /// Required Attribute
        /// string
        public string FullName { get; set; }

        /// <remarks>The version of the CV.</remarks>
        /// Optional Attribute
        /// string
        public string Version { get; set; }

        /// <remarks>The URI of the source CV.</remarks>
        /// Required Attribute
        /// anyURI
        public string URI { get; set; }

        /// <remarks>The unique identifier of this cv within the document to be referenced by cvParam elements.</remarks>
        /// Required Attribute
        /// string
        public string Id { get; set; }
        #endregion

        #region Object Equality
        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CVInfo other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (FullName == other.FullName && Version == other.Version && URI == other.URI)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as CVInfo;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = FullName != null ? FullName.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Version != null ? Version.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (URI != null ? URI.GetHashCode() : 0);
                return hashCode;
            }
        }
        #endregion
    }
}