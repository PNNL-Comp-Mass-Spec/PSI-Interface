using System;
using System.IO;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML SearchDatabaseType
    /// </summary>
    /// <remarks>
    ///     A database for searching mass spectra. Examples include a set of amino acid sequence entries, or annotated
    ///     spectra libraries.
    /// </remarks>
    public class SearchDatabaseInfo : CVParamGroupObj, IExternalDataType, IEquatable<SearchDatabaseInfo>
    {
        private ParamObj _databaseName;
        private FileFormatInfo _fileFormat;
        private long _numDatabaseSequences;
        private long _numResidues;
        private DateTime _releaseDate;

        /// <summary>
        ///     Constructor
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
        ///     Create an object using the contents of the corresponding MzIdentML object
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

        /// <remarks>
        ///     The database name may be given as a cvParam if it maps exactly to one of the release databases listed in the
        ///     CV, otherwise a userParam should be used.
        /// </remarks>
        /// <remarks>min 1, max 1</remarks>
        public ParamObj DatabaseName
        {
            get { return _databaseName; }
            set
            {
                _databaseName = value;
                if (_databaseName != null)
                    _databaseName.IdentData = IdentData;
            }
        }

        /// <remarks>The version of the database.</remarks>
        /// Optional Attribute
        /// string
        public string Version { get; set; }

        /// <remarks>
        ///     The date and time the database was released to the public; omit this attribute when the date and time are
        ///     unknown or not applicable (e.g. custom databases).
        /// </remarks>
        /// Optional Attribute
        /// dateTime
        public DateTime ReleaseDate
        {
            get { return _releaseDate; }
            set
            {
                _releaseDate = value;
                ReleaseDateSpecified = true;
            }
        }

        /// Attribute Existence
        protected internal bool ReleaseDateSpecified { get; private set; }

        /// <remarks>The total number of sequences in the database.</remarks>
        /// Optional Attribute
        /// long
        public long NumDatabaseSequences
        {
            get { return _numDatabaseSequences; }
            set
            {
                _numDatabaseSequences = value;
                NumDatabaseSequencesSpecified = true;
            }
        }

        /// Attribute Existence
        protected internal bool NumDatabaseSequencesSpecified { get; private set; }

        /// <remarks>The number of residues in the database.</remarks>
        /// Optional Attribute
        /// long
        public long NumResidues
        {
            get { return _numResidues; }
            set
            {
                _numResidues = value;
                NumResiduesSpecified = true;
            }
        }

        /// <remarks></remarks>
        /// Attribute Existence
        protected internal bool NumResiduesSpecified { get; private set; }

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

        /// <remarks>
        ///     A URI to access documentation and tools to interpret the external format of the ExternalData instance.
        ///     For example, XML Schema or static libraries (APIs) to access binary formats.
        /// </remarks>
        /// <remarks>min 0, max 1</remarks>
        public string ExternalFormatDocumentation { get; set; }

        /// <remarks>min 0, max 1</remarks>
        public FileFormatInfo FileFormat
        {
            get { return _fileFormat; }
            set
            {
                _fileFormat = value;
                if (_fileFormat != null)
                    _fileFormat.IdentData = IdentData;
            }
        }

        /// <remarks>The location of the data file.</remarks>
        /// Required Attribute
        /// string
        public string Location { get; set; }

        #region Object Equality

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as SearchDatabaseInfo;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(SearchDatabaseInfo other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if ((Name == other.Name) && (Version == other.Version) &&
                (NumDatabaseSequences == other.NumDatabaseSequences) && (NumResidues == other.NumResidues) &&
                (ExternalFormatDocumentation == other.ExternalFormatDocumentation) &&
                (Path.GetFileName(Location) == Path.GetFileName(other.Location)) &&
                Equals(DatabaseName, other.DatabaseName) && Equals(FileFormat, other.FileFormat) &&
                Equals(CVParams, other.CVParams))
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
                hashCode = (hashCode * 397) ^ (Version != null ? Version.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ NumDatabaseSequences.GetHashCode();
                hashCode = (hashCode * 397) ^ NumResidues.GetHashCode();
                hashCode = (hashCode * 397) ^ (ExternalFormatDocumentation != null ? ExternalFormatDocumentation.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Location != null ? Location.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DatabaseName != null ? DatabaseName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FileFormat != null ? FileFormat.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}