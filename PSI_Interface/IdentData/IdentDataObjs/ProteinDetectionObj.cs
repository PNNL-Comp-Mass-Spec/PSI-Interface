using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML ProteinDetectionType
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
        /// Constructor
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
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="pd"></param>
        /// <param name="idata"></param>
        public ProteinDetectionObj(ProteinDetectionType pd, IdentDataObj idata)
            : base(pd, idata)
        {
            ProteinDetectionListRef = pd.proteinDetectionList_ref;
            ProteinDetectionProtocolRef = pd.proteinDetectionProtocol_ref;

            InputSpectrumIdentifications = new IdentDataList<InputSpectrumIdentificationsObj>(1);

            if (pd.InputSpectrumIdentifications?.Count > 0)
            {
                InputSpectrumIdentifications.AddRange(pd.InputSpectrumIdentifications, isi => new InputSpectrumIdentificationsObj(isi, IdentData));
            }
        }

        /// <summary>min 1, max unbounded</summary>
        public IdentDataList<InputSpectrumIdentificationsObj> InputSpectrumIdentifications
        {
            get => _inputSpectrumIdentifications;
            set
            {
                _inputSpectrumIdentifications = value;

                if (_inputSpectrumIdentifications != null)
                    _inputSpectrumIdentifications.IdentData = IdentData;
            }
        }

        /// <summary>A reference to the ProteinDetectionList in the DataCollection section.</summary>
        /// <remarks>Required Attribute</remarks>
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

        /// <summary>A reference to the ProteinDetectionList in the DataCollection section.</summary>
        /// <remarks>Required Attribute</remarks>
        public ProteinDetectionListObj ProteinDetectionList
        {
            get => _proteinDetectionList;
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

        /// <summary>A reference to the detection protocol used for this ProteinDetection.</summary>
        /// <remarks>Required Attribute</remarks>
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

        /// <summary>A reference to the detection protocol used for this ProteinDetection.</summary>
        /// <remarks>Required Attribute</remarks>
        public ProteinDetectionProtocolObj ProteinDetectionProtocol
        {
            get => _proteinDetectionProtocol;
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
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is ProteinDetectionObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(ProteinDetectionObj other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return Name == other.Name && Equals(InputSpectrumIdentifications, other.InputSpectrumIdentifications) &&
                   Equals(ProteinDetectionList, other.ProteinDetectionList) &&
                   Equals(ProteinDetectionProtocol, other.ProteinDetectionProtocol);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (InputSpectrumIdentifications?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (ProteinDetectionList?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (ProteinDetectionProtocol?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}
