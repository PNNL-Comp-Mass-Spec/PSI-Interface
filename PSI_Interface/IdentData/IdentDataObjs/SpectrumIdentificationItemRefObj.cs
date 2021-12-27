using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML SpectrumIdentificationItemRefType
    /// </summary>
    /// <remarks>
    /// Reference(s) to the SpectrumIdentificationItem element(s) that support the given PeptideEvidence element.
    /// Using these references it is possible to indicate which spectra were actually accepted as evidence for this
    /// peptide identification in the given protein.
    /// </remarks>
    public class SpectrumIdentificationItemRefObj : IdentDataInternalTypeAbstract,
        IEquatable<SpectrumIdentificationItemRefObj>
    {
        private SpectrumIdentificationItemObj _spectrumIdentificationItem;
        private string _spectrumIdentificationItemRef;

        #region Constructors
        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="spectrumIdItemRef"></param>
        /// <param name="idata"></param>
        public SpectrumIdentificationItemRefObj(SpectrumIdentificationItemRefType spectrumIdItemRef, IdentDataObj idata)
            : base(idata)
        {
            SpectrumIdentificationItemRef = spectrumIdItemRef.spectrumIdentificationItem_ref;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SpectrumIdentificationItemRefObj()
        {
            _spectrumIdentificationItemRef = null;

            _spectrumIdentificationItem = null;
        }
        #endregion

        #region Properties
        /// <summary>A reference to the SpectrumIdentificationItem element(s).</summary>
        /// <remarks>Required Attribute</remarks>
        protected internal string SpectrumIdentificationItemRef
        {
            get
            {
                if (_spectrumIdentificationItem != null)
                {
                    return _spectrumIdentificationItem.Id;
                }
                return _spectrumIdentificationItemRef;
            }
            set
            {
                _spectrumIdentificationItemRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    SpectrumIdentificationItem = IdentData.FindSpectrumIdentificationItem(value);
                }
            }
        }

        /// <summary>A reference to the SpectrumIdentificationItem element(s).</summary>
        /// <remarks>Required Attribute</remarks>
        public SpectrumIdentificationItemObj SpectrumIdentificationItem
        {
            get => _spectrumIdentificationItem;
            set
            {
                _spectrumIdentificationItem = value;
                if (_spectrumIdentificationItem != null)
                {
                    _spectrumIdentificationItem.IdentData = IdentData;
                    _spectrumIdentificationItemRef = _spectrumIdentificationItem.Id;
                }
            }
        }
        #endregion

        #region Object Equality
        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(SpectrumIdentificationItemRefObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return other != null && Equals(SpectrumIdentificationItem, other.SpectrumIdentificationItem);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is SpectrumIdentificationItemRefObj o && Equals(o);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            return SpectrumIdentificationItem?.GetHashCode() ?? 0;
        }
        #endregion
    }
}
