using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML ProteinDetectionType
    /// </summary>
    /// <remarks>An Analysis which assembles a set of peptides (e.g. from a spectra search analysis) to proteins.</remarks>
    public class ProteinDetectionObj : ProtocolApplicationObj, IEquatable<ProteinDetectionObj>
    {
        private IdentDataList<InputSpectrumIdentificationsObj> _inputSpectrumIdentifications;
        private ProteinDetectionListObj _proteinDetectionList;
        private string _proteinDetectionListRef;
        private ProteinDetectionProtocolObj _proteinDetectionProtocol;
        private string _proteinDetectionProtocolRef;

        /// <summary>
        ///     Constructor
        /// </summary>
        public ProteinDetectionObj()
        {
            _proteinDetectionListRef = null;
            _proteinDetectionProtocolRef = null;

            _proteinDetectionList = null;
            _proteinDetectionProtocol = null;
            InputSpectrumIdentifications = new IdentDataList<InputSpectrumIdentificationsObj>(1);
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="pd"></param>
        /// <param name="idata"></param>
        public ProteinDetectionObj(ProteinDetectionType pd, IdentDataObj idata)
            : base(pd, idata)
        {
            ProteinDetectionListRef = pd.proteinDetectionList_ref;
            ProteinDetectionProtocolRef = pd.proteinDetectionProtocol_ref;

            InputSpectrumIdentifications = new IdentDataList<InputSpectrumIdentificationsObj>(1);

            if ((pd.InputSpectrumIdentifications != null) && (pd.InputSpectrumIdentifications.Count > 0))
            {
                InputSpectrumIdentifications.AddRange(pd.InputSpectrumIdentifications, isi => new InputSpectrumIdentificationsObj(isi, IdentData));
            }
        }

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<InputSpectrumIdentificationsObj> InputSpectrumIdentifications
        {
            get { return _inputSpectrumIdentifications; }
            set
            {
                _inputSpectrumIdentifications = value;
                if (_inputSpectrumIdentifications != null)
                    _inputSpectrumIdentifications.IdentData = IdentData;
            }
        }

        /// <remarks>A reference to the ProteinDetectionList in the DataCollection section.</remarks>
        /// Required Attribute
        /// string
        protected internal string ProteinDetectionListRef
        {
            get
            {
                if (_proteinDetectionList != null)
                    return _proteinDetectionList.Id;
                return _proteinDetectionListRef;
            }
            set
            {
                _proteinDetectionListRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                    ProteinDetectionList = IdentData.FindProteinDetectionList(value);
            }
        }

        /// <remarks>A reference to the ProteinDetectionList in the DataCollection section.</remarks>
        /// Required Attribute
        /// string
        public ProteinDetectionListObj ProteinDetectionList
        {
            get { return _proteinDetectionList; }
            set
            {
                _proteinDetectionList = value;
                if (_proteinDetectionList != null)
                {
                    _proteinDetectionList.IdentData = IdentData;
                    _proteinDetectionListRef = _proteinDetectionList.Id;
                }
            }
        }

        /// <remarks>A reference to the detection protocol used for this ProteinDetection.</remarks>
        /// Required Attribute
        /// string
        protected internal string ProteinDetectionProtocolRef
        {
            get
            {
                if (_proteinDetectionProtocol != null)
                    return _proteinDetectionProtocol.Id;
                return _proteinDetectionProtocolRef;
            }
            set
            {
                _proteinDetectionProtocolRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                    ProteinDetectionProtocol = IdentData.FindProteinDetectionProtocol(value);
            }
        }

        /// <remarks>A reference to the detection protocol used for this ProteinDetection.</remarks>
        /// Required Attribute
        /// string
        public ProteinDetectionProtocolObj ProteinDetectionProtocol
        {
            get { return _proteinDetectionProtocol; }
            set
            {
                _proteinDetectionProtocol = value;
                if (_proteinDetectionProtocol != null)
                {
                    _proteinDetectionProtocol.IdentData = IdentData;
                    _proteinDetectionProtocolRef = _proteinDetectionProtocol.Id;
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
            var o = other as ProteinDetectionObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ProteinDetectionObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if ((Name == other.Name) && Equals(InputSpectrumIdentifications, other.InputSpectrumIdentifications) &&
                Equals(ProteinDetectionList, other.ProteinDetectionList) &&
                Equals(ProteinDetectionProtocol, other.ProteinDetectionProtocol))
                return true;
            return false;
        }

        /// <summary>
        ///     Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name != null ? Name.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (InputSpectrumIdentifications != null ? InputSpectrumIdentifications.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ProteinDetectionList != null ? ProteinDetectionList.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ProteinDetectionProtocol != null ? ProteinDetectionProtocol.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}