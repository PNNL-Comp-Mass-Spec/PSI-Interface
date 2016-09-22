using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML InputSpectrumIdentificationsType
    /// </summary>
    /// <remarks>The lists of spectrum identifications that are input to the protein detection process.</remarks>
    public class InputSpectrumIdentificationsObj : IdentDataInternalTypeAbstract, IEquatable<InputSpectrumIdentificationsObj>
    {
        private SpectrumIdentificationListObj _spectrumIdentificationList;

        private string _spectrumIdentificationListRef;

        /// <summary>
        ///     Constructor
        /// </summary>
        public InputSpectrumIdentificationsObj()
        {
            _spectrumIdentificationListRef = null;

            _spectrumIdentificationList = null;
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="isi"></param>
        /// <param name="idata"></param>
        public InputSpectrumIdentificationsObj(InputSpectrumIdentificationsType isi, IdentDataObj idata)
            : base(idata)
        {
            SpectrumIdentificationListRef = isi.spectrumIdentificationList_ref;
        }

        /// <remarks>A reference to the list of spectrum identifications that were input to the process.</remarks>
        /// Required Attribute
        /// string
        protected internal string SpectrumIdentificationListRef
        {
            get
            {
                if (_spectrumIdentificationList != null)
                    return _spectrumIdentificationList.Id;
                return _spectrumIdentificationListRef;
            }
            set
            {
                _spectrumIdentificationListRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                    SpectrumIdentificationList = IdentData.FindSpectrumIdentificationList(value);
            }
        }

        /// <remarks>A reference to the list of spectrum identifications that were input to the process.</remarks>
        /// Required Attribute
        /// string
        public SpectrumIdentificationListObj SpectrumIdentificationList
        {
            get { return _spectrumIdentificationList; }
            set
            {
                _spectrumIdentificationList = value;
                if (_spectrumIdentificationList != null)
                {
                    _spectrumIdentificationList.IdentData = IdentData;
                    _spectrumIdentificationListRef = _spectrumIdentificationList.Id;
                }
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
            var o = other as InputSpectrumIdentificationsObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(InputSpectrumIdentificationsObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if (Equals(SpectrumIdentificationList, other.SpectrumIdentificationList))
                return true;
            return false;
        }

        /// <summary>
        ///     Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = SpectrumIdentificationList != null ? SpectrumIdentificationList.GetHashCode() : 0;
            return hashCode;
        }

        #endregion
    }
}