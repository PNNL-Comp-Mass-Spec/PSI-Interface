using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML DBSequenceType
    /// </summary>
    /// <remarks>
    /// A database sequence from the specified SearchDatabase (nucleic acid or amino acid).
    /// If the sequence is nucleic acid, the source nucleic acid sequence should be given in
    /// the seq attribute rather than a translated sequence.
    /// </remarks>
    /// <remarks>CVParams/UserParams: Additional descriptors for the sequence, such as taxon, description line etc.</remarks>
    public class DbSequenceObj : ParamGroupObj, IIdentifiableType, IEquatable<DbSequenceObj>
    {
        // Ignore Spelling: MzIdentML, taxon

        private int _length;
        private SearchDatabaseInfo _searchDatabase;
        private string _searchDatabaseRef;

        /// <summary>
        /// Constructor
        /// </summary>
        public DbSequenceObj()
        {
            Id = null;
            Name = null;
            Seq = null;
            _length = -1;
            LengthSpecified = false;
            _searchDatabaseRef = null;
            Accession = null;

            _searchDatabase = null;
        }

        /// <summary>
        /// Creates a DB sequence object with the specified values
        /// </summary>
        /// <param name="searchDb">Valid <see cref="SearchDatabaseInfo" /> object, not null</param>
        /// <param name="length">length of the protein</param>
        /// <param name="accession">protein identifier</param>
        /// <param name="description">description of the protein</param>
        public DbSequenceObj(SearchDatabaseInfo searchDb, int length, string accession, string description = "") : this()
        {
            Length = length;
            SearchDatabase = searchDb;
            Accession = accession;

            if (!string.IsNullOrWhiteSpace(description))
                CVParams.Add(new CVParamObj(CV.CV.CVID.MS_protein_description, description));
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="dbs"></param>
        /// <param name="idata"></param>
        public DbSequenceObj(DBSequenceType dbs, IdentDataObj idata)
            : base(dbs, idata)
        {
            Id = dbs.id;
            Name = dbs.name;
            Seq = dbs.Seq;
            _length = dbs.length;
            LengthSpecified = dbs.lengthSpecified;
            SearchDatabaseRef = dbs.searchDatabase_ref;
            Accession = dbs.accession;
        }

        /// <summary>The actual sequence of amino acids or nucleic acid.</summary>
        /// <remarks>min 0, max 1</remarks>
        /// <returns>RegEx: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]*"</returns>
        public string Seq { get; set; }

        /// <summary>The unique accession of this sequence.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Accession { get; set; }

        /// <summary>The source database of this sequence.</summary>
        /// <remarks>Required Attribute</remarks>
        protected internal string SearchDatabaseRef
        {
            get
            {
                if (_searchDatabase != null)
                    return _searchDatabase.Id;
                return _searchDatabaseRef;
            }
            set
            {
                _searchDatabaseRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                    SearchDatabase = IdentData.FindSearchDatabase(value);
            }
        }

        /// <summary>The source database of this sequence.</summary>
        /// <remarks>Required Attribute</remarks>
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

        /// <summary>The length of the sequence as a number of bases or residues.</summary>
        /// <remarks>Optional Attribute</remarks>
        public int Length
        {
            get => _length;
            set
            {
                _length = value;
                LengthSpecified = true;
            }
        }

        /// <summary>
        /// True if Length has been defined
        /// </summary>
        protected internal bool LengthSpecified { get; private set; }

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
            return other is DbSequenceObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(DbSequenceObj other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return Accession == other.Accession && Length == other.Length && Name == other.Name &&
                   Seq == other.Seq && Equals(SearchDatabase, other.SearchDatabase) &&
                   ParamsEquals(other);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (Accession?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ Length;
                hashCode = (hashCode * 397) ^ (Seq?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (SearchDatabase?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (CVParams?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (UserParams?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}
