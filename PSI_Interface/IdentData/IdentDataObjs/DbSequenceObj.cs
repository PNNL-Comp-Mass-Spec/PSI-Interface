using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML DBSequenceType
    /// </summary>
    /// <remarks>
    ///     A database sequence from the specified SearchDatabase (nucleic acid or amino acid).
    ///     If the sequence is nucleic acid, the source nucleic acid sequence should be given in
    ///     the seq attribute rather than a translated sequence.
    /// </remarks>
    /// <remarks>CVParams/UserParams: Additional descriptors for the sequence, such as taxon, description line etc.</remarks>
    public class DbSequenceObj : ParamGroupObj, IIdentifiableType, IEquatable<DbSequenceObj>
    {
        private int _length;
        private SearchDatabaseInfo _searchDatabase;
        private string _searchDatabaseRef;

        /// <summary>
        ///     Constructor
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
        ///     Creates a db sequence object with the specified values
        /// </summary>
        /// <param name="searchDb">Valid <see cref="SearchDatabaseInfo" /> object, not null</param>
        /// <param name="length">length of the protein</param>
        /// <param name="accession">protein identifier</param>
        /// <param name="description">description of the protein</param>
        /// <returns></returns>
        public DbSequenceObj(SearchDatabaseInfo searchDb, int length, string accession, string description = "") : this()
        {
            Length = length;
            SearchDatabase = searchDb;
            Accession = accession;

            if (!string.IsNullOrWhiteSpace(description))
                CVParams.Add(new CVParamObj(CV.CV.CVID.MS_protein_description, description));
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
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

        /// <remarks>The actual sequence of amino acids or nucleic acid.</remarks>
        /// <remarks>min 0, max 1</remarks>
        /// string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]*"
        public string Seq { get; set; }

        /// <remarks>The unique accession of this sequence.</remarks>
        /// Required Attribute
        public string Accession { get; set; }

        /// <remarks>The source database of this sequence.</remarks>
        /// Required Attribute
        /// string
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

        /// <remarks>The source database of this sequence.</remarks>
        /// Required Attribute
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

        /// <remarks>The length of the sequence as a number of bases or residues.</remarks>
        /// Optional Attribute
        /// integer
        public int Length
        {
            get { return _length; }
            set
            {
                _length = value;
                LengthSpecified = true;
            }
        }

        /// Attribute Existence
        protected internal bool LengthSpecified { get; private set; }

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
        public new bool Equals(object other)
        {
            var o = other as DbSequenceObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(DbSequenceObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if ((Accession == other.Accession) && (Length == other.Length) && (Name == other.Name) &&
                (Seq == other.Seq) && Equals(SearchDatabase, other.SearchDatabase) &&
                Equals(CVParams, other.CVParams) && Equals(UserParams, other.UserParams))
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
                hashCode = (hashCode * 397) ^ (Accession != null ? Accession.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Length;
                hashCode = (hashCode * 397) ^ (Seq != null ? Seq.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SearchDatabase != null ? SearchDatabase.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}