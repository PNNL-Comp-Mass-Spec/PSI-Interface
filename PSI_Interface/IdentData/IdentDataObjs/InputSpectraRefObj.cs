using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML InputSpectraType
    /// </summary>
    /// <remarks>The attribute referencing an identifier within the SpectraData section.</remarks>
    public class InputSpectraRefObj : IdentDataInternalTypeAbstract, IEquatable<InputSpectraRefObj>
    {
        private SpectraDataObj _spectraData;
        private string _spectraDataRef;

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public InputSpectraRefObj()
        {
            _spectraDataRef = null;

            _spectraData = null;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="isr"></param>
        /// <param name="idata"></param>
        public InputSpectraRefObj(InputSpectraType isr, IdentDataObj idata)
            : base(idata)
        {
            SpectraDataRef = isr.spectraData_ref;
        }
        #endregion

        #region Properties
        /// <remarks>A reference to the SpectraData element which locates the input spectra to an external file.</remarks>
        /// Optional Attribute
        /// string
        protected internal string SpectraDataRef
        {
            get
            {
                if (_spectraData != null)
                {
                    return _spectraData.Id;
                }
                return _spectraDataRef;
            }
            set
            {
                _spectraDataRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    SpectraData = IdentData.FindSpectraData(value);
                }
            }
        }

        /// <remarks>A reference to the SpectraData element which locates the input spectra to an external file.</remarks>
        /// Optional Attribute
        /// string
        public SpectraDataObj SpectraData
        {
            get => _spectraData;
            set
            {
                _spectraData = value;
                if (_spectraData != null)
                {
                    _spectraData.IdentData = IdentData;
                    _spectraDataRef = _spectraData.Id;
                }
            }
        }
        #endregion

        #region Object Equality
        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(InputSpectraRefObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(SpectraData, other.SpectraData))
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
            var o = other as InputSpectraRefObj;
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
            var hashCode = SpectraData != null ? SpectraData.GetHashCode() : 0;
            return hashCode;
        }
        #endregion
    }
}