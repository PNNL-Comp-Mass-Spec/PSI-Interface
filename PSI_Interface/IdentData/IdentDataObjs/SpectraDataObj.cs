using System;
using System.IO;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML SpectraDataType
    /// </summary>
    /// <remarks>A data set containing spectra data (consisting of one or more spectra).</remarks>
    public class SpectraDataObj : IdentDataInternalTypeAbstract, IExternalDataType, IEquatable<SpectraDataObj>
    {
        private FileFormatInfo _fileFormat;

        private SpectrumIDFormatObj _spectrumIDFormat;

        /// <summary>
        /// Constructor
        /// </summary>
        public SpectraDataObj()
        {
            Id = null;
            Name = null;
            ExternalFormatDocumentation = null;
            Location = null;

            _spectrumIDFormat = null;
            _fileFormat = null;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="sd"></param>
        /// <param name="idata"></param>
        public SpectraDataObj(SpectraDataType sd, IdentDataObj idata)
            : base(idata)
        {
            Id = sd.id;
            Name = sd.name;
            ExternalFormatDocumentation = sd.ExternalFormatDocumentation;
            Location = sd.location;

            _spectrumIDFormat = null;
            _fileFormat = null;

            if (sd.SpectrumIDFormat != null)
                _spectrumIDFormat = new SpectrumIDFormatObj(sd.SpectrumIDFormat, IdentData);
            if (sd.FileFormat != null)
                _fileFormat = new FileFormatInfo(sd.FileFormat, IdentData);
        }

        /// <summary>min 1, max 1</summary>
        public SpectrumIDFormatObj SpectrumIDFormat
        {
            get => _spectrumIDFormat;
            set
            {
                _spectrumIDFormat = value;
                if (_spectrumIDFormat != null)
                    _spectrumIDFormat.IdentData = IdentData;
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
            var o = other as SpectraDataObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(SpectraDataObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if ((Name == other.Name) && (ExternalFormatDocumentation == other.ExternalFormatDocumentation) &&
                (Path.GetFileName(Location) == Path.GetFileName(other.Location)) && Equals(SpectrumIDFormat, other.SpectrumIDFormat) &&
                Equals(FileFormat, other.FileFormat))
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
                hashCode = (hashCode * 397) ^ (ExternalFormatDocumentation?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Location?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (SpectrumIDFormat?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (FileFormat?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}
