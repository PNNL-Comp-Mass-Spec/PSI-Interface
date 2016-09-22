using System;
using System.Linq;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML InputsType
    /// </summary>
    /// <remarks>
    ///     The inputs to the analyses including the databases searched, the spectral data and the source file converted
    ///     to mzIdentML.
    /// </remarks>
    public class InputsObj : IdentDataInternalTypeAbstract, IEquatable<InputsObj>
    {
        private IdentDataList<SearchDatabaseInfo> _searchDatabases;
        private long _searchDbIdCounter;

        private IdentDataList<SourceFileInfo> _sourceFiles;

        private long _specDataIdCounter;
        private IdentDataList<SpectraDataObj> _spectraDataList;

        /// <summary>
        ///     Constructor
        /// </summary>
        public InputsObj()
        {
            SourceFiles = new IdentDataList<SourceFileInfo>();
            SearchDatabases = new IdentDataList<SearchDatabaseInfo>();
            SpectraDataList = new IdentDataList<SpectraDataObj>();
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="i"></param>
        /// <param name="idata"></param>
        public InputsObj(InputsType i, IdentDataObj idata)
            : base(idata)
        {
            _sourceFiles = null;
            _searchDatabases = null;
            _spectraDataList = null;

            if ((i.SourceFile != null) && (i.SourceFile.Count > 0))
            {
                SourceFiles = new IdentDataList<SourceFileInfo>();
                foreach (var sf in i.SourceFile)
                    SourceFiles.Add(new SourceFileInfo(sf, IdentData));
            }
            if ((i.SearchDatabase != null) && (i.SearchDatabase.Count > 0))
            {
                SearchDatabases = new IdentDataList<SearchDatabaseInfo>();
                foreach (var sd in i.SearchDatabase)
                    SearchDatabases.Add(new SearchDatabaseInfo(sd, IdentData));
            }
            if ((i.SpectraData != null) && (i.SpectraData.Count > 0))
            {
                SpectraDataList = new IdentDataList<SpectraDataObj>();
                foreach (var sd in i.SpectraData)
                    SpectraDataList.Add(new SpectraDataObj(sd, IdentData));
            }
        }

        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<SourceFileInfo> SourceFiles
        {
            get { return _sourceFiles; }
            set
            {
                _sourceFiles = value;
                if (_sourceFiles != null)
                    _sourceFiles.IdentData = IdentData;
            }
        }

        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<SearchDatabaseInfo> SearchDatabases
        {
            get { return _searchDatabases; }
            set
            {
                _searchDatabases = value;
                if (_searchDatabases != null)
                    _searchDatabases.IdentData = IdentData;
            }
        }

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<SpectraDataObj> SpectraDataList
        {
            get { return _spectraDataList; }
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
                foreach (var dbSeq in specId.SearchDatabases)
                {
                    if (_searchDatabases.Any(item => item.Equals(dbSeq.SearchDatabase)))
                        continue;

                    dbSeq.SearchDatabase.Id = "SearchDB_" + _searchDbIdCounter;
                    _searchDbIdCounter++;
                    _searchDatabases.Add(dbSeq.SearchDatabase);
                }
        }

        private void RebuildSpectraDataList()
        {
            _specDataIdCounter = 0;
            _spectraDataList.Clear();

            foreach (var sil in IdentData.DataCollection.AnalysisData.SpectrumIdentificationList)
                foreach (var spectraData in sil.SpectrumIdentificationResults)
                {
                    if (_spectraDataList.Any(item => item.Equals(spectraData.SpectraData)))
                        continue;

                    spectraData.SpectraData.Id = "SID_" + _specDataIdCounter;
                    _specDataIdCounter++;
                    _spectraDataList.Add(spectraData.SpectraData);
                }

            foreach (var specId in IdentData.AnalysisCollection.SpectrumIdentifications)
                foreach (var spectraData in specId.InputSpectra)
                {
                    if (_spectraDataList.Any(item => item.Equals(spectraData.SpectraData)))
                        continue;

                    spectraData.SpectraData.Id = "SID_" + _specDataIdCounter;
                    _specDataIdCounter++;
                    _spectraDataList.Add(spectraData.SpectraData);
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
            var o = other as InputsObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(InputsObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if (Equals(SourceFiles, other.SourceFiles) && Equals(SearchDatabases, other.SearchDatabases) &&
                Equals(SpectraDataList, other.SpectraDataList))
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
                var hashCode = SourceFiles != null ? SourceFiles.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (SearchDatabases != null ? SearchDatabases.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SpectraDataList != null ? SpectraDataList.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}