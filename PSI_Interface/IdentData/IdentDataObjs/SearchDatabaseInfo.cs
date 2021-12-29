using System;
using System.IO;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML SearchDatabaseType
    /// </summary>
    /// <remarks>
    /// A database for searching mass spectra. Examples include a set of amino acid sequence entries,
    /// nucleotide databases (e.g. 6 frame translated) (mzIdentML 1.2),
    /// or annotated spectra libraries.
    /// </remarks>
    public class SearchDatabaseInfo : CVParamGroupObj, IExternalDataType, IEquatable<SearchDatabaseInfo>
    {
        // Ignore Spelling: mzIdentML
        private ParamObj _databaseName;
        private FileFormatInfo _fileFormat;
        private long _numDatabaseSequences;
        private long _numResidues;
        private DateTime _releaseDate;

        /// <summary>
        /// Constructor
        /// </summary>
        public SearchDatabaseInfo()
        {
            Id = null;
            Name = null;
            Version = null;
            _releaseDate = DateTime.Now;
            ReleaseDateSpecified = false;
            _numDatabaseSequences = -1;
            NumDatabaseSequencesSpecified = false;
            _numResidues = -1;
            NumResiduesSpecified = false;
            ExternalFormatDocumentation = null;
            Location = null;

            _databaseName = null;
            _fileFormat = null;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="sd"></param>
        /// <param name="idata"></param>
        public SearchDatabaseInfo(SearchDatabaseType sd, IdentDataObj idata)
            : base(sd, idata)
        {
            Id = sd.id;
            Name = sd.name;
            Version = sd.version;
            _releaseDate = sd.releaseDate;
            ReleaseDateSpecified = sd.releaseDateSpecified;
            _numDatabaseSequences = sd.numDatabaseSequences;
            NumDatabaseSequencesSpecified = sd.numDatabaseSequencesSpecified;
            _numResidues = sd.numResidues;
            NumResiduesSpecified = sd.numResiduesSpecified;
            ExternalFormatDocumentation = sd.ExternalFormatDocumentation;
            Location = sd.location;

            _databaseName = null;
            _fileFormat = null;

            if (sd.DatabaseName != null)
                _databaseName = new ParamObj(sd.DatabaseName, IdentData);
            if (sd.FileFormat != null)
                _fileFormat = new FileFormatInfo(sd.FileFormat, IdentData);
        }

        /// <summary>
        /// The database name may be given as a cvParam if it maps exactly to one of the release databases listed in the
        /// CV, otherwise a userParam should be used.
        /// </summary>
        /// <remarks>min 1, max 1</remarks>
        public ParamObj DatabaseName
        {
            get => _databaseName;
            set
            {
                _databaseName = value;
                if (_databaseName != null)
                    _databaseName.IdentData = IdentData;
            }
        }

        /// <summary>The version of the database.</summary>
        /// <remarks>Optional Attribute</remarks>
        public string Version { get; set; }

        /// <summary>
        /// The date and time the database was released to the public; omit this attribute when the date and time are
        /// unknown or not applicable (e.g. custom databases).
        /// </summary>
        /// <remarks>Optional Attribute</remarks>
        public DateTime ReleaseDate
        {
            get => _releaseDate;
            set
            {
                _releaseDate = value;
                ReleaseDateSpecified = true;
            }
        }

        /// <summary>
        /// True if Release Date has been defined
        /// </summary>
        protected internal bool ReleaseDateSpecified { get; private set; }

        /// <summary>The total number of sequences in the database.</summary>
        /// <remarks>Optional Attribute</remarks>
        public long NumDatabaseSequences
        {
            get => _numDatabaseSequences;
            set
            {
                _numDatabaseSequences = value;
                NumDatabaseSequencesSpecified = true;
            }
        }

        /// <summary>
        /// True if Num Database Sequences has been defined
        /// </summary>
        protected internal bool NumDatabaseSequencesSpecified { get; private set; }

        /// <summary>The number of residues in the database.</summary>
        /// <remarks>Optional Attribute</remarks>
        public long NumResidues
        {
            get => _numResidues;
            set
            {
                _numResidues = value;
                NumResiduesSpecified = true;
            }
        }

        /// <summary>
        /// True if NumResidues has been defined
        /// </summary>
        protected internal bool NumResiduesSpecified { get; private set; }

        /// <summary>
        /// An identifier is an unambiguous string that is unique within the scope
        /// (i.e. a document, a set of related documents, or a repository) of its use.
        /// </summary>
        /// <remarks>Required Attribute</remarks>
        public string Id { get; set; }

        /// <summary>The potentially ambiguous common identifier, such as a human-readable name for the instance.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Name { get; set; }

        /// <summary>
        /// A URI to access documentation and tools to interpret the external format of the ExternalData instance.
        /// For example, XML Schema or static libraries (APIs) to access binary formats.
        /// </summary>
        /// <remarks>min 0, max 1</remarks>
        public string ExternalFormatDocumentation { get; set; }

        /// <summary>min 0, max 1 (mzIdentML 1.1)</summary>
        /// <remarks>min 1, max 1 (mzIdentML 1.2)</remarks>
        public FileFormatInfo FileFormat
        {
            get => _fileFormat;
            set
            {
                _fileFormat = value;
                if (_fileFormat != null)
                    _fileFormat.IdentData = IdentData;
            }
        }

        /// <summary>The location of the data file.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Location { get; set; }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is SearchDatabaseInfo o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(SearchDatabaseInfo other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return Name == other.Name && Version == other.Version &&
                   NumDatabaseSequences == other.NumDatabaseSequences && NumResidues == other.NumResidues &&
                   ExternalFormatDocumentation == other.ExternalFormatDocumentation &&
                   Path.GetFileName(Location) == Path.GetFileName(other.Location) &&
                   Equals(DatabaseName, other.DatabaseName) && Equals(FileFormat, other.FileFormat) &&
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
                hashCode = (hashCode * 397) ^ (Version?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ NumDatabaseSequences.GetHashCode();
                hashCode = (hashCode * 397) ^ NumResidues.GetHashCode();
                hashCode = (hashCode * 397) ^ (ExternalFormatDocumentation?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Location?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (DatabaseName?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (FileFormat?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (CVParams?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}
