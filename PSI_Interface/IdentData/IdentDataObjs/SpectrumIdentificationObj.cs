using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML SpectrumIdentificationType
    /// </summary>
    /// <remarks>
    ///     An Analysis which tries to identify peptides in input spectra, referencing the database searched,
    ///     the input spectra, the output results and the protocol that is run.
    /// </remarks>
    public class SpectrumIdentificationObj : ProtocolApplicationObj, IEquatable<SpectrumIdentificationObj>
    {
        private IdentDataList<InputSpectraRefObj> _inputSpectra;
        private IdentDataList<SearchDatabaseRefObj> _searchDatabases;
        private SpectrumIdentificationListObj _spectrumIdentificationList;
        private string _spectrumIdentificationListRef;
        private SpectrumIdentificationProtocolObj _spectrumIdentificationProtocol;
        private string _spectrumIdentificationProtocolRef;

        /// <summary>
        ///     Constructor
        /// </summary>
        public SpectrumIdentificationObj()
        {
            _spectrumIdentificationProtocolRef = null;
            _spectrumIdentificationListRef = null;

            _spectrumIdentificationProtocol = null;
            _spectrumIdentificationList = null;
            InputSpectra = new IdentDataList<InputSpectraRefObj>(1);
            SearchDatabases = new IdentDataList<SearchDatabaseRefObj>(1);
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="si"></param>
        /// <param name="idata"></param>
        public SpectrumIdentificationObj(SpectrumIdentificationType si, IdentDataObj idata)
            : base(si, idata)
        {
            SpectrumIdentificationProtocolRef = si.spectrumIdentificationProtocol_ref;
            SpectrumIdentificationListRef = si.spectrumIdentificationList_ref;

            InputSpectra = new IdentDataList<InputSpectraRefObj>(1);
            SearchDatabases = new IdentDataList<SearchDatabaseRefObj>(1);

            if ((si.InputSpectra != null) && (si.InputSpectra.Count > 0))
            {
                InputSpectra.AddRange(si.InputSpectra, ispec => new InputSpectraRefObj(ispec, IdentData));
            }
            if ((si.SearchDatabaseRef != null) && (si.SearchDatabaseRef.Count > 0))
            {
                SearchDatabases.AddRange(si.SearchDatabaseRef, sd => new SearchDatabaseRefObj(sd, IdentData));
            }
        }

        /// <remarks>One of the spectra data sets used.</remarks>
        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<InputSpectraRefObj> InputSpectra
        {
            get { return _inputSpectra; }
            set
            {
                _inputSpectra = value;
                if (_inputSpectra != null)
                    _inputSpectra.IdentData = IdentData;
            }
        }

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<SearchDatabaseRefObj> SearchDatabases
        {
            get { return _searchDatabases; }
            set
            {
                _searchDatabases = value;
                if (_searchDatabases != null)
                    _searchDatabases.IdentData = IdentData;
            }
        }

        /// <remarks>A reference to the search protocol used for this SpectrumIdentification.</remarks>
        /// Required Attribute
        /// string
        protected internal string SpectrumIdentificationProtocolRef
        {
            get
            {
                if (_spectrumIdentificationProtocol != null)
                    return _spectrumIdentificationProtocol.Id;
                return _spectrumIdentificationProtocolRef;
            }
            set
            {
                _spectrumIdentificationProtocolRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                    SpectrumIdentificationProtocol = IdentData.FindSpectrumIdentificationProtocol(value);
            }
        }

        /// <remarks>A reference to the search protocol used for this SpectrumIdentification.</remarks>
        /// Required Attribute
        /// string
        public SpectrumIdentificationProtocolObj SpectrumIdentificationProtocol
        {
            get { return _spectrumIdentificationProtocol; }
            set
            {
                _spectrumIdentificationProtocol = value;
                if (_spectrumIdentificationProtocol != null)
                {
                    _spectrumIdentificationProtocol.IdentData = IdentData;
                    _spectrumIdentificationProtocolRef = _spectrumIdentificationProtocol.Id;
                }
            }
        }

        /// <remarks>A reference to the SpectrumIdentificationList produced by this analysis in the DataCollection section.</remarks>
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

        /// <remarks>A reference to the SpectrumIdentificationList produced by this analysis in the DataCollection section.</remarks>
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
            var o = other as SpectrumIdentificationObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(SpectrumIdentificationObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if ((Name == other.Name) && Equals(InputSpectra, other.InputSpectra) &&
                Equals(SpectrumIdentificationList, other.SpectrumIdentificationList) &&
                Equals(SpectrumIdentificationProtocol, other.SpectrumIdentificationProtocol))
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
                hashCode = (hashCode * 397) ^ (InputSpectra != null ? InputSpectra.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SpectrumIdentificationList != null ? SpectrumIdentificationList.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SpectrumIdentificationProtocol != null ? SpectrumIdentificationProtocol.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}