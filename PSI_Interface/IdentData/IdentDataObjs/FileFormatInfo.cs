using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML FileFormatType
    /// </summary>
    /// <remarks>The format of the ExternalData file, for example "tiff" for image files.</remarks>
    public class FileFormatInfo : IdentDataInternalTypeAbstract, IEquatable<FileFormatInfo>
    {
        private CVParamObj _cvParam;

        /// <summary>
        ///     Constructor
        /// </summary>
        public FileFormatInfo()
        {
            _cvParam = null;
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
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

        /// <remarks>cvParam capturing file formats</remarks>
        /// <remarks>min 1, max 1</remarks>
        public CVParamObj CVParam
        {
            get { return _cvParam; }
            set
            {
                _cvParam = value;
                if (_cvParam != null)
                    _cvParam.IdentData = IdentData;
            }
        }

        #region Object Equality

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as FileFormatInfo;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(FileFormatInfo other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if (Equals(CVParam, other.CVParam))
                return true;
            return false;
        }

        /// <summary>
        ///     Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = CVParam != null ? CVParam.GetHashCode() : 0;
            return hashCode;
        }

        #endregion
    }
}