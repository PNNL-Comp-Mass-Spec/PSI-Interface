using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML FileFormatType
    /// </summary>
    /// <remarks>The format of the ExternalData file, for example "tiff" for image files.</remarks>
    public class FileFormatInfo : IdentDataInternalTypeAbstract, IEquatable<FileFormatInfo>
    {
        private CVParamObj _cvParam;

        /// <summary>
        /// Constructor
        /// </summary>
        public FileFormatInfo()
        {
            _cvParam = null;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="ff"></param>
        /// <param name="idata"></param>
        public FileFormatInfo(FileFormatType ff, IdentDataObj idata)
            : base(idata)
        {
            _cvParam = null;

            if (ff.cvParam != null)
                _cvParam = new CVParamObj(ff.cvParam, IdentData);
        }

        /// <summary>cvParam capturing file formats</summary>
        /// <remarks>min 1, max 1</remarks>
        public CVParamObj CVParam
        {
            get => _cvParam;
            set
            {
                _cvParam = value;
                if (_cvParam != null)
                    _cvParam.IdentData = IdentData;
            }
        }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is FileFormatInfo o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(FileFormatInfo other)
        {
            if (ReferenceEquals(this, other))
                return true;

            return other != null && Equals(CVParam, other.CVParam);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            return CVParam != null ? CVParam.GetHashCode() : 0;
        }

        #endregion
    }
}
