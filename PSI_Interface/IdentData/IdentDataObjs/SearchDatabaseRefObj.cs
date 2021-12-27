using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML SearchDatabaseRefType
    /// </summary>
    /// <remarks>One of the search databases used.</remarks>
    public class SearchDatabaseRefObj : IdentDataInternalTypeAbstract, IEquatable<SearchDatabaseRefObj>
    {
        private SearchDatabaseInfo _searchDatabase;
        private string _searchDatabaseRef;

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public SearchDatabaseRefObj()
        {
            _searchDatabaseRef = null;

            _searchDatabase = null;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
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
        /// <summary>A reference to the database searched.</summary>
        /// <remarks>Optional Attribute</remarks>
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

        /// <summary>A reference to the database searched.</summary>
        /// <remarks>Optional Attribute</remarks>
        public SearchDatabaseInfo SearchDatabase
        {
            get => _searchDatabase;
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
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(SearchDatabaseRefObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return other != null && Equals(SearchDatabase, other.SearchDatabase);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is SearchDatabaseRefObj o && Equals(o);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            return SearchDatabase?.GetHashCode() ?? 0;
        }
        #endregion
    }
}
