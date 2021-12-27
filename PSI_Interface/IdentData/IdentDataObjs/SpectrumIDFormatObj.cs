using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML SpectrumIDFormatType
    /// </summary>
    /// <remarks>The format of the spectrum identifier within the source file</remarks>
    public class SpectrumIDFormatObj : IdentDataInternalTypeAbstract, IEquatable<SpectrumIDFormatObj>
    {
        private CVParamObj _cvParam;

        /// <summary>
        /// Constructor
        /// </summary>
        public SpectrumIDFormatObj()
        {
            _cvParam = null;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="formatType"></param>
        /// <param name="idata"></param>
        public SpectrumIDFormatObj(SpectrumIDFormatType formatType, IdentDataObj idata)
            : base(idata)
        {
            _cvParam = null;

            if (formatType.cvParam != null)
                _cvParam = new CVParamObj(formatType.cvParam, IdentData);
        }

        /// <summary>CV term capturing the type of identifier used.</summary>
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
            return other is SpectrumIDFormatObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(SpectrumIDFormatObj other)
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
            return CVParam?.GetHashCode() ?? 0;
        }

        #endregion
    }
}
