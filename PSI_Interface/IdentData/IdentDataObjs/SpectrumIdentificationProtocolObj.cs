using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML SpectrumIdentificationProtocolType
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
        /// Constructor
        /// </summary>
        public SpectrumIdentificationProtocolObj()
        {
            Id = null;
            Name = null;
            _analysisSoftwareRef = null;

            _analysisSoftware = null;
            _searchType = null;
            _additionalSearchParams = null;
            ModificationParams = new IdentDataList<SearchModificationObj>(1);
            _enzymes = null;
            MassTables = new IdentDataList<MassTableObj>(1);
            FragmentTolerances = new IdentDataList<CVParamObj>(1);
            ParentTolerances = new IdentDataList<CVParamObj>(1);
            _threshold = null;
            DatabaseFilters = new IdentDataList<FilterInfo>(1);
            _databaseTranslation = null;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
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
            _enzymes = null;
            _threshold = null;
            _databaseTranslation = null;

            ModificationParams = new IdentDataList<SearchModificationObj>(1);
            MassTables = new IdentDataList<MassTableObj>(1);
            FragmentTolerances = new IdentDataList<CVParamObj>(1);
            ParentTolerances = new IdentDataList<CVParamObj>(1);
            DatabaseFilters = new IdentDataList<FilterInfo>(1);

            if (sip.SearchType != null)
                _searchType = new ParamObj(sip.SearchType, IdentData);
            if (sip.AdditionalSearchParams != null)
                _additionalSearchParams = new ParamListObj(sip.AdditionalSearchParams, IdentData);
            if (sip.ModificationParams?.Count > 0)
            {
                ModificationParams.AddRange(sip.ModificationParams, mp => new SearchModificationObj(mp, IdentData));
            }
            if (sip.Enzymes != null)
                _enzymes = new EnzymeListObj(sip.Enzymes, IdentData);
            if (sip.MassTable?.Count > 0)
            {
                MassTables.AddRange(sip.MassTable, mt => new MassTableObj(mt, IdentData));
            }
            if (sip.FragmentTolerance?.Count > 0)
            {
                FragmentTolerances.AddRange(sip.FragmentTolerance, ft => new CVParamObj(ft, IdentData));
            }
            if (sip.ParentTolerance?.Count > 0)
            {
                ParentTolerances.AddRange(sip.ParentTolerance, pt => new CVParamObj(pt, IdentData));
            }
            if (sip.Threshold != null)
                _threshold = new ParamListObj(sip.Threshold, IdentData);
            if (sip.DatabaseFilters?.Count > 0)
            {
                DatabaseFilters.AddRange(sip.DatabaseFilters, df => new FilterInfo(df, IdentData));
            }
            if (sip.DatabaseTranslation != null)
                _databaseTranslation = new DatabaseTranslationObj(sip.DatabaseTranslation, IdentData);
        }

        /// <summary>The type of search performed e.g. PMF, Tag searches, MS-MS</summary>
        /// <remarks>min 1, max 1</remarks>
        public ParamObj SearchType
        {
            get => _searchType;
            set
            {
                _searchType = value;
                if (_searchType != null)
                    _searchType.IdentData = IdentData;
            }
        }

        /// <summary>The search parameters other than the modifications searched.</summary>
        /// <remarks>min 0, max 1</remarks>
        public ParamListObj AdditionalSearchParams
        {
            get => _additionalSearchParams;
            set
            {
                _additionalSearchParams = value;
                if (_additionalSearchParams != null)
                    _additionalSearchParams.IdentData = IdentData;
            }
        }

        /// <summary>min 0, max 1</summary>
        // Original ModificationParamsType
        public IdentDataList<SearchModificationObj> ModificationParams
        {
            get => _modificationParams;
            set
            {
                _modificationParams = value;
                if (_modificationParams != null)
                    _modificationParams.IdentData = IdentData;
            }
        }

        /// <summary>min 0, max 1</summary>
        public EnzymeListObj Enzymes
        {
            get => _enzymes;
            set
            {
                _enzymes = value;
                if (_enzymes != null)
                    _enzymes.IdentData = IdentData;
            }
        }

        /// <summary>min 0, max unbounded</summary>
        public IdentDataList<MassTableObj> MassTables
        {
            get => _massTables;
            set
            {
                _massTables = value;
                if (_massTables != null)
                    _massTables.IdentData = IdentData;
            }
        }

        /// <summary>min 0, max 1</summary>
        // Original ToleranceType
        public IdentDataList<CVParamObj> FragmentTolerances
        {
            get => _fragmentTolerances;
            set
            {
                _fragmentTolerances = value;
                if (_fragmentTolerances != null)
                    _fragmentTolerances.IdentData = IdentData;
            }
        }

        /// <summary>min 0, max 1</summary>
        // Original ToleranceType
        public IdentDataList<CVParamObj> ParentTolerances
        {
            get => _parentTolerances;
            set
            {
                _parentTolerances = value;
                if (_parentTolerances != null)
                    _parentTolerances.IdentData = IdentData;
            }
        }

        /// <summary>
        /// The threshold(s) applied to determine that a result is significant. If multiple terms are used it is assumed
        /// that all conditions are satisfied by the passing results.
        /// </summary>
        /// <remarks>min 1, max 1</remarks>
        public ParamListObj Threshold
        {
            get => _threshold;
            set
            {
                _threshold = value;
                if (_threshold != null)
                    _threshold.IdentData = IdentData;
            }
        }

        /// <summary>min 0, max 1</summary>
        // Original DatabaseFiltersType
        public IdentDataList<FilterInfo> DatabaseFilters
        {
            get => _databaseFilters;
            set
            {
                _databaseFilters = value;
                if (_databaseFilters != null)
                    _databaseFilters.IdentData = IdentData;
            }
        }

        /// <summary>min 0, max 1</summary>
        public DatabaseTranslationObj DatabaseTranslation
        {
            get => _databaseTranslation;
            set
            {
                _databaseTranslation = value;
                if (_databaseTranslation != null)
                    _databaseTranslation.IdentData = IdentData;
            }
        }

        /// <summary>The search algorithm used, given as a reference to the SoftwareCollection section.</summary>
        /// <remarks>Required Attribute</remarks>
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

        /// <summary>The search algorithm used, given as a reference to the SoftwareCollection section.</summary>
        /// <remarks>Required Attribute</remarks>
        public AnalysisSoftwareObj AnalysisSoftware
        {
            get => _analysisSoftware;
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

        /// <summary>
        /// An identifier is an unambiguous string that is unique within the scope
        /// (i.e. a document, a set of related documents, or a repository) of its use.
        /// </summary>
        /// <remarks>Required Attribute</remarks>
        public string Id { get; set; }

        /// <summary>The potentially ambiguous common identifier, such as a human-readable name for the instance.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Name { get; set; }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            var o = other as SpectrumIdentificationProtocolObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(SpectrumIdentificationProtocolObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if (Name == other.Name && Equals(AnalysisSoftware, other.AnalysisSoftware) &&
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
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (AnalysisSoftware?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (SearchType?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^
                           (AdditionalSearchParams?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (MassTables?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (ModificationParams?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Enzymes?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (FragmentTolerances?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (ParentTolerances?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Threshold?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (DatabaseFilters?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (DatabaseTranslation?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}
