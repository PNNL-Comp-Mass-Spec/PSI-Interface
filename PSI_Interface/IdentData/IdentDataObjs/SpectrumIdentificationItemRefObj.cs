using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML SpectrumIdentificationItemRefType
    /// </summary>
    /// <remarks>
    ///     Reference(s) to the SpectrumIdentificationItem element(s) that support the given PeptideEvidence element.
    ///     Using these references it is possible to indicate which spectra were actually accepted as evidence for this
    ///     peptide identification in the given protein.
    /// </remarks>
    public class SpectrumIdentificationItemRefObj : IdentDataInternalTypeAbstract,
        IEquatable<SpectrumIdentificationItemRefObj>
    {
        private SpectrumIdentificationItemObj _spectrumIdentificationItem;
        private string _spectrumIdentificationItemRef;

        #region Constructors
        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="siir"></param>
        /// <param name="idata"></param>
        public SpectrumIdentificationItemRefObj(SpectrumIdentificationItemRefType siir, IdentDataObj idata)
            : base(idata)
        {
            SpectrumIdentificationItemRef = siir.spectrumIdentificationItem_ref;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        public SpectrumIdentificationItemRefObj()
        {
            _spectrumIdentificationItemRef = null;

            _spectrumIdentificationItem = null;
        }
        #endregion

        #region Properties
        /// <remarks>A reference to the SpectrumIdentificationItem element(s).</remarks>
        /// Required Attribute
        /// string
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

        /// <remarks>A reference to the SpectrumIdentificationItem element(s).</remarks>
        /// Required Attribute
        /// string
        public SpectrumIdentificationItemObj SpectrumIdentificationItem
        {
            get { return _spectrumIdentificationItem; }
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
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(SpectrumIdentificationItemRefObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(SpectrumIdentificationItem, other.SpectrumIdentificationItem))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as SpectrumIdentificationItemRefObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        /// <summary>
        ///     Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = SpectrumIdentificationItem != null ? SpectrumIdentificationItem.GetHashCode() : 0;
            return hashCode;
        }
        #endregion
    }
}