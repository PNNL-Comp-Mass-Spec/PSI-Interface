using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData
{
    /// <summary>
    ///     MzIdentML InputSpectraType
    /// </summary>
    /// <remarks>The attribute referencing an identifier within the SpectraData section.</remarks>
    public class InputSpectraRefObj : IdentDataInternalTypeAbstract, IEquatable<InputSpectraRefObj>
    {
        private SpectraDataObj _spectraData;
        private string _spectraDataRef;

        #region Constructors
        /// <summary>
        ///     Constructor
        /// </summary>
        public InputSpectraRefObj()
        {
            _spectraDataRef = null;

            _spectraData = null;
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
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
            get { return _spectraData; }
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
        ///     Object equality
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
        ///     Object equality
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
        ///     Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = SpectraData != null ? SpectraData.GetHashCode() : 0;
            return hashCode;
        }
        #endregion
    }

    /// <summary>
    ///     Base class for identical ParentOrganization and AffiliationInfo
    /// </summary>
    public class OrganizationRefObj : IdentDataInternalTypeAbstract, IEquatable<OrganizationRefObj>
    {
        private OrganizationObj _organization;
        private string _organizationRef;

        #region Constructors
        /// <summary>
        ///     Constructor
        /// </summary>
        public OrganizationRefObj()
        {
            _organizationRef = null;

            _organization = null;
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="idata"></param>
        public OrganizationRefObj(IdentDataObj idata)
            : base(idata)
        {
        }
        #endregion

        #region Properties
        /// <remarks>A reference to the organization this contact belongs to.</remarks>
        /// Required Attribute
        /// string
        protected internal string OrganizationRef
        {
            get
            {
                if (_organization != null)
                {
                    return _organization.Id;
                }
                return _organizationRef;
            }
            set
            {
                _organizationRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    Organization = IdentData.FindOrganization(value);
                }
            }
        }

        /// <remarks>A reference to the organization this contact belongs to.</remarks>
        /// Required Attribute
        /// string
        public OrganizationObj Organization
        {
            get { return _organization; }
            set
            {
                _organization = value;
                if (_organization != null)
                {
                    _organization.IdentData = IdentData;
                    _organizationRef = _organization.Id;
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
        public bool Equals(OrganizationRefObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(Organization, other.Organization))
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
            var o = other as OrganizationRefObj;
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
            var hashCode = Organization != null ? Organization.GetHashCode() : 0;
            return hashCode;
        }
        #endregion
    }

    /// <summary>
    ///     MzIdentML PeptideEvidenceRefType
    /// </summary>
    /// <remarks>
    ///     Reference to the PeptideEvidence element identified. If a specific sequence can be assigned to multiple
    ///     proteins and or positions in a protein all possible PeptideEvidence elements should be referenced here.
    /// </remarks>
    public class PeptideEvidenceRefObj : IdentDataInternalTypeAbstract, IEquatable<PeptideEvidenceRefObj>
    {
        private PeptideEvidenceObj _peptideEvidence;
        private string _peptideEvidenceRef;

        #region Constructors
        /// <summary>
        ///     Constructor
        /// </summary>
        public PeptideEvidenceRefObj(PeptideEvidenceObj pepEv = null)
        {
            _peptideEvidenceRef = null;

            _peptideEvidence = pepEv;
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="per"></param>
        /// <param name="idata"></param>
        public PeptideEvidenceRefObj(PeptideEvidenceRefType per, IdentDataObj idata)
            : base(idata)
        {
            PeptideEvidenceRef = per.peptideEvidence_ref;
        }
        #endregion

        #region Properties
        /// <remarks>A reference to the PeptideEvidenceItem element(s).</remarks>
        /// Required Attribute
        /// string
        protected internal string PeptideEvidenceRef
        {
            get
            {
                if (_peptideEvidence != null)
                {
                    return _peptideEvidence.Id;
                }
                return _peptideEvidenceRef;
            }
            set
            {
                _peptideEvidenceRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    PeptideEvidence = IdentData.FindPeptideEvidence(value);
                }
            }
        }

        /// <remarks>A reference to the PeptideEvidenceItem element(s).</remarks>
        /// Required Attribute
        /// string
        public PeptideEvidenceObj PeptideEvidence
        {
            get { return _peptideEvidence; }
            set
            {
                _peptideEvidence = value;
                if (_peptideEvidence != null)
                {
                    _peptideEvidence.IdentData = IdentData;
                    _peptideEvidenceRef = _peptideEvidence.Id;
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
        public bool Equals(PeptideEvidenceRefObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(PeptideEvidence, other.PeptideEvidence))
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
            var o = other as PeptideEvidenceRefObj;
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
            var hashCode = PeptideEvidence != null ? PeptideEvidence.GetHashCode() : 0;
            return hashCode;
        }
        #endregion
    }

    /// <summary>
    ///     MzIdentML SearchDatabaseRefType
    /// </summary>
    /// <remarks>One of the search databases used.</remarks>
    public class SearchDatabaseRefObj : IdentDataInternalTypeAbstract, IEquatable<SearchDatabaseRefObj>
    {
        private SearchDatabaseInfo _searchDatabase;
        private string _searchDatabaseRef;

        #region Constructors
        /// <summary>
        ///     Constructor
        /// </summary>
        public SearchDatabaseRefObj()
        {
            _searchDatabaseRef = null;

            _searchDatabase = null;
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="sdr"></param>
        /// <param name="idata"></param>
        public SearchDatabaseRefObj(SearchDatabaseRefType sdr, IdentDataObj idata)
            : base(idata)
        {
            SearchDatabaseRef = sdr.searchDatabase_ref;
        }
        #endregion

        #region Properties
        /// <remarks>A reference to the database searched.</remarks>
        /// Optional Attribute
        /// string
        protected internal string SearchDatabaseRef
        {
            get
            {
                if (_searchDatabase != null)
                {
                    return _searchDatabase.Id;
                }
                return _searchDatabaseRef;
            }
            set
            {
                _searchDatabaseRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    SearchDatabase = IdentData.FindSearchDatabase(value);
                }
            }
        }

        /// <remarks>A reference to the database searched.</remarks>
        /// Optional Attribute
        /// string
        public SearchDatabaseInfo SearchDatabase
        {
            get { return _searchDatabase; }
            set
            {
                _searchDatabase = value;
                if (_searchDatabase != null)
                {
                    _searchDatabase.IdentData = IdentData;
                    _searchDatabaseRef = _searchDatabase.Id;
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
        public bool Equals(SearchDatabaseRefObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(SearchDatabase, other.SearchDatabase))
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
            var o = other as SearchDatabaseRefObj;
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
            var hashCode = SearchDatabase != null ? SearchDatabase.GetHashCode() : 0;
            return hashCode;
        }
        #endregion
    }

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