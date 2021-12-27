using System;
using System.Linq;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML InputsType
    /// </summary>
    /// <remarks>
    /// The inputs to the analyses including the databases searched, the spectral data and the source file converted
    /// to mzIdentML.
    /// </remarks>
    public class InputsObj : IdentDataInternalTypeAbstract, IEquatable<InputsObj>
    {
        private IdentDataList<SearchDatabaseInfo> _searchDatabases;
        private long _searchDbIdCounter;

        private IdentDataList<SourceFileInfo> _sourceFiles;

        private long _specDataIdCounter;
        private IdentDataList<SpectraDataObj> _spectraDataList;

        /// <summary>
        /// Constructor
        /// </summary>
        public InputsObj()
        {
            SourceFiles = new IdentDataList<SourceFileInfo>(1);
            SearchDatabases = new IdentDataList<SearchDatabaseInfo>(1);
            SpectraDataList = new IdentDataList<SpectraDataObj>(1);
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="i"></param>
        /// <param name="idata"></param>
        public InputsObj(InputsType i, IdentDataObj idata)
            : base(idata)
        {
            SourceFiles = new IdentDataList<SourceFileInfo>(1);
            SearchDatabases = new IdentDataList<SearchDatabaseInfo>(1);
            SpectraDataList = new IdentDataList<SpectraDataObj>(1);
            if ((i.SourceFile?.Count > 0))
            {
                SourceFiles.AddRange(i.SourceFile, sf => new SourceFileInfo(sf, IdentData));
            }
            if ((i.SearchDatabase?.Count > 0))
            {
                SearchDatabases.AddRange(i.SearchDatabase, sd => new SearchDatabaseInfo(sd, IdentData));
            }
            if ((i.SpectraData?.Count > 0))
            {
                SpectraDataList.AddRange(i.SpectraData, sd => new SpectraDataObj(sd, IdentData));
            }
        }

        /// <summary>min 0, max unbounded</summary>
        public IdentDataList<SourceFileInfo> SourceFiles
        {
            get => _sourceFiles;
            set
            {
                _sourceFiles = value;
                if (_sourceFiles != null)
                    _sourceFiles.IdentData = IdentData;
            }
        }

        /// <summary>min 0, max unbounded</summary>
        public IdentDataList<SearchDatabaseInfo> SearchDatabases
        {
            get => _searchDatabases;
            set
            {
                _searchDatabases = value;
                if (_searchDatabases != null)
                    _searchDatabases.IdentData = IdentData;
            }
        }

        /// <summary>min 1, max unbounded</summary>
        public IdentDataList<SpectraDataObj> SpectraDataList
        {
            get => _spectraDataList;
            set
            {
                _spectraDataList = value;
                if (_spectraDataList != null)
                    _spectraDataList.IdentData = IdentData;
            }
        }

        internal void RebuildLists()
        {
            RebuildSearchDatabaseList();
            RebuildSpectraDataList();
        }

        // ReSharper disable ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator

        private void RebuildSearchDatabaseList()
        {
            _searchDbIdCounter = 0;
            _searchDatabases.Clear();

            foreach (var dbSeq in IdentData.SequenceCollection.DBSequences)
            {
                if (_searchDatabases.Any(item => item.Equals(dbSeq.SearchDatabase)))
                    continue;

                dbSeq.SearchDatabase.Id = "SearchDB_" + _searchDbIdCounter;
                _searchDbIdCounter++;
                _searchDatabases.Add(dbSeq.SearchDatabase);
            }

            foreach (var specId in IdentData.AnalysisCollection.SpectrumIdentifications)
            {
                foreach (var dbSeq in specId.SearchDatabases)
                {
                    if (_searchDatabases.Any(item => item.Equals(dbSeq.SearchDatabase)))
                        continue;

                    dbSeq.SearchDatabase.Id = "SearchDB_" + _searchDbIdCounter;
                    _searchDbIdCounter++;
                    _searchDatabases.Add(dbSeq.SearchDatabase);
                }
            }
        }

        private void RebuildSpectraDataList()
        {
            _specDataIdCounter = 0;
            _spectraDataList.Clear();

            foreach (var sil in IdentData.DataCollection.AnalysisData.SpectrumIdentificationList)
            {
                foreach (var spectraData in sil.SpectrumIdentificationResults)
                {
                    if (_spectraDataList.Any(item => item.Equals(spectraData.SpectraData)))
                        continue;

                    spectraData.SpectraData.Id = "SID_" + _specDataIdCounter;
                    _specDataIdCounter++;
                    _spectraDataList.Add(spectraData.SpectraData);
                }
            }

            foreach (var specId in IdentData.AnalysisCollection.SpectrumIdentifications)
            {
                foreach (var spectraData in specId.InputSpectra)
                {
                    if (_spectraDataList.Any(item => item.Equals(spectraData.SpectraData)))
                        continue;

                    spectraData.SpectraData.Id = "SID_" + _specDataIdCounter;
                    _specDataIdCounter++;
                    _spectraDataList.Add(spectraData.SpectraData);
                }
            }
        }

        // ReSharper restore ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is InputsObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(InputsObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            return Equals(SourceFiles, other.SourceFiles) && Equals(SearchDatabases, other.SearchDatabases) &&
                   Equals(SpectraDataList, other.SpectraDataList);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = SourceFiles?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (SearchDatabases?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (SpectraDataList?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}
