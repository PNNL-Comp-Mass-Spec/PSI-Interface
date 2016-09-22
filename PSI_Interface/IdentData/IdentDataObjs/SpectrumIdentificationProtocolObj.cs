using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML SpectrumIdentificationProtocolType
    /// </summary>
    /// <remarks>The parameters and settings of a SpectrumIdentification analysis.</remarks>
    public class SpectrumIdentificationProtocolObj : IdentDataInternalTypeAbstract, IIdentifiableType,
        IEquatable<SpectrumIdentificationProtocolObj>
    {
        private ParamListObj _additionalSearchParams;
        private AnalysisSoftwareObj _analysisSoftware;
        private string _analysisSoftwareRef;
        private IdentDataList<FilterInfo> _databaseFilters;
        private DatabaseTranslationObj _databaseTranslation;
        private EnzymeListObj _enzymes;
        private IdentDataList<CVParamObj> _fragmentTolerances;
        private IdentDataList<MassTableObj> _massTables;
        private IdentDataList<SearchModificationObj> _modificationParams;
        private IdentDataList<CVParamObj> _parentTolerances;

        private ParamObj _searchType;
        private ParamListObj _threshold;

        /// <summary>
        ///     Constructor
        /// </summary>
        public SpectrumIdentificationProtocolObj()
        {
            Id = null;
            Name = null;
            _analysisSoftwareRef = null;

            _analysisSoftware = null;
            _searchType = null;
            _additionalSearchParams = null;
            ModificationParams = new IdentDataList<SearchModificationObj>();
            _enzymes = null;
            MassTables = new IdentDataList<MassTableObj>();
            FragmentTolerances = new IdentDataList<CVParamObj>();
            ParentTolerances = new IdentDataList<CVParamObj>();
            _threshold = null;
            DatabaseFilters = new IdentDataList<FilterInfo>();
            _databaseTranslation = null;
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="sip"></param>
        /// <param name="idata"></param>
        public SpectrumIdentificationProtocolObj(SpectrumIdentificationProtocolType sip, IdentDataObj idata)
            : base(idata)
        {
            Id = sip.id;
            Name = sip.name;
            AnalysisSoftwareRef = sip.analysisSoftware_ref;

            _searchType = null;
            _additionalSearchParams = null;
            _modificationParams = null;
            _enzymes = null;
            _massTables = null;
            _fragmentTolerances = null;
            _parentTolerances = null;
            _threshold = null;
            _databaseFilters = null;
            _databaseTranslation = null;

            if (sip.SearchType != null)
                _searchType = new ParamObj(sip.SearchType, IdentData);
            if (sip.AdditionalSearchParams != null)
                _additionalSearchParams = new ParamListObj(sip.AdditionalSearchParams, IdentData);
            if ((sip.ModificationParams != null) && (sip.ModificationParams.Count > 0))
            {
                ModificationParams = new IdentDataList<SearchModificationObj>();
                foreach (var mp in sip.ModificationParams)
                    ModificationParams.Add(new SearchModificationObj(mp, IdentData));
            }
            if (sip.Enzymes != null)
                _enzymes = new EnzymeListObj(sip.Enzymes, IdentData);
            if ((sip.MassTable != null) && (sip.MassTable.Count > 0))
            {
                MassTables = new IdentDataList<MassTableObj>();
                foreach (var mt in sip.MassTable)
                    MassTables.Add(new MassTableObj(mt, IdentData));
            }
            if ((sip.FragmentTolerance != null) && (sip.FragmentTolerance.Count > 0))
            {
                FragmentTolerances = new IdentDataList<CVParamObj>();
                foreach (var ft in sip.FragmentTolerance)
                    FragmentTolerances.Add(new CVParamObj(ft, IdentData));
            }
            if (sip.ParentTolerance != null)
            {
                ParentTolerances = new IdentDataList<CVParamObj>();
                foreach (var pt in sip.ParentTolerance)
                    ParentTolerances.Add(new CVParamObj(pt, IdentData));
            }
            if (sip.Threshold != null)
                _threshold = new ParamListObj(sip.Threshold, IdentData);
            if ((sip.DatabaseFilters != null) && (sip.DatabaseFilters.Count > 0))
            {
                DatabaseFilters = new IdentDataList<FilterInfo>();
                foreach (var df in sip.DatabaseFilters)
                    DatabaseFilters.Add(new FilterInfo(df, IdentData));
            }
            if (sip.DatabaseTranslation != null)
                _databaseTranslation = new DatabaseTranslationObj(sip.DatabaseTranslation, IdentData);
        }

        /// <remarks>The type of search performed e.g. PMF, Tag searches, MS-MS</remarks>
        /// <remarks>min 1, max 1</remarks>
        public ParamObj SearchType
        {
            get { return _searchType; }
            set
            {
                _searchType = value;
                if (_searchType != null)
                    _searchType.IdentData = IdentData;
            }
        }

        /// <remarks>The search parameters other than the modifications searched.</remarks>
        /// <remarks>min 0, max 1</remarks>
        public ParamListObj AdditionalSearchParams
        {
            get { return _additionalSearchParams; }
            set
            {
                _additionalSearchParams = value;
                if (_additionalSearchParams != null)
                    _additionalSearchParams.IdentData = IdentData;
            }
        }

        /// <remarks>min 0, max 1</remarks>
        // Original ModificationParamsType
        public IdentDataList<SearchModificationObj> ModificationParams
        {
            get { return _modificationParams; }
            set
            {
                _modificationParams = value;
                if (_modificationParams != null)
                    _modificationParams.IdentData = IdentData;
            }
        }

        /// <remarks>min 0, max 1</remarks>
        public EnzymeListObj Enzymes
        {
            get { return _enzymes; }
            set
            {
                _enzymes = value;
                if (_enzymes != null)
                    _enzymes.IdentData = IdentData;
            }
        }

        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<MassTableObj> MassTables
        {
            get { return _massTables; }
            set
            {
                _massTables = value;
                if (_massTables != null)
                    _massTables.IdentData = IdentData;
            }
        }

        /// <remarks>min 0, max 1</remarks>
        // Original ToleranceType
        public IdentDataList<CVParamObj> FragmentTolerances
        {
            get { return _fragmentTolerances; }
            set
            {
                _fragmentTolerances = value;
                if (_fragmentTolerances != null)
                    _fragmentTolerances.IdentData = IdentData;
            }
        }

        /// <remarks>min 0, max 1</remarks>
        // Original ToleranceType
        public IdentDataList<CVParamObj> ParentTolerances
        {
            get { return _parentTolerances; }
            set
            {
                _parentTolerances = value;
                if (_parentTolerances != null)
                    _parentTolerances.IdentData = IdentData;
            }
        }

        /// <remarks>
        ///     The threshold(s) applied to determine that a result is significant. If multiple terms are used it is assumed
        ///     that all conditions are satisfied by the passing results.
        /// </remarks>
        /// <remarks>min 1, max 1</remarks>
        public ParamListObj Threshold
        {
            get { return _threshold; }
            set
            {
                _threshold = value;
                if (_threshold != null)
                    _threshold.IdentData = IdentData;
            }
        }

        /// <remarks>min 0, max 1</remarks>
        // Original DatabaseFiltersType
        public IdentDataList<FilterInfo> DatabaseFilters
        {
            get { return _databaseFilters; }
            set
            {
                _databaseFilters = value;
                if (_databaseFilters != null)
                    _databaseFilters.IdentData = IdentData;
            }
        }

        /// <remarks>min 0, max 1</remarks>
        public DatabaseTranslationObj DatabaseTranslation
        {
            get { return _databaseTranslation; }
            set
            {
                _databaseTranslation = value;
                if (_databaseTranslation != null)
                    _databaseTranslation.IdentData = IdentData;
            }
        }

        /// <remarks>The search algorithm used, given as a reference to the SoftwareCollection section.</remarks>
        /// Required Attribute
        /// string
        protected internal string AnalysisSoftwareRef
        {
            get
            {
                if (_analysisSoftware != null)
                    return _analysisSoftware.Id;
                return _analysisSoftwareRef;
            }
            set
            {
                _analysisSoftwareRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                    AnalysisSoftware = IdentData.FindAnalysisSoftware(value);
            }
        }

        /// <remarks>The search algorithm used, given as a reference to the SoftwareCollection section.</remarks>
        /// Required Attribute
        /// string
        public AnalysisSoftwareObj AnalysisSoftware
        {
            get { return _analysisSoftware; }
            set
            {
                _analysisSoftware = value;
                if (_analysisSoftware != null)
                {
                    _analysisSoftware.IdentData = IdentData;
                    _analysisSoftwareRef = _analysisSoftware.Id;
                }
            }
        }

        /// <remarks>
        ///     An identifier is an unambiguous string that is unique within the scope
        ///     (i.e. a document, a set of related documents, or a repository) of its use.
        /// </remarks>
        /// Required Attribute
        /// string
        public string Id { get; set; }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name { get; set; }

        #region Object Equality

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as SpectrumIdentificationProtocolObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(SpectrumIdentificationProtocolObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if ((Name == other.Name) && Equals(AnalysisSoftware, other.AnalysisSoftware) &&
                Equals(SearchType, other.SearchType) &&
                Equals(AdditionalSearchParams, other.AdditionalSearchParams) &&
                Equals(MassTables, other.MassTables) && Equals(ModificationParams, other.ModificationParams) &&
                Equals(Enzymes, other.Enzymes) && Equals(FragmentTolerances, other.FragmentTolerances) &&
                Equals(ParentTolerances, other.ParentTolerances) && Equals(Threshold, other.Threshold) &&
                Equals(DatabaseFilters, other.DatabaseFilters) &&
                Equals(DatabaseTranslation, other.DatabaseTranslation))
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
                hashCode = (hashCode * 397) ^ (AnalysisSoftware != null ? AnalysisSoftware.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SearchType != null ? SearchType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^
                           (AdditionalSearchParams != null ? AdditionalSearchParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (MassTables != null ? MassTables.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ModificationParams != null ? ModificationParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Enzymes != null ? Enzymes.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FragmentTolerances != null ? FragmentTolerances.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ParentTolerances != null ? ParentTolerances.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Threshold != null ? Threshold.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DatabaseFilters != null ? DatabaseFilters.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DatabaseTranslation != null ? DatabaseTranslation.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}