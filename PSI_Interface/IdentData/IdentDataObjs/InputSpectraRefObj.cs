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
        /// <summary>A reference to the SpectraData element which locates the input spectra to an external file.</summary>
        /// <remarks>Optional Attribute</remarks>
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

        /// <summary>A reference to the SpectraData element which locates the input spectra to an external file.</summary>
        /// <remarks>Optional Attribute</remarks>
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
        public bool Equals(InputSpectraRefObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return other != null && Equals(SpectraData, other.SpectraData);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is InputSpectraRefObj o && Equals(o);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            return SpectraData?.GetHashCode() ?? 0;
        }
        #endregion
    }
}
