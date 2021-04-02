using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML BibliographicReferenceType
    /// </summary>
    /// <remarks>Represents bibliographic references.</remarks>
    public class BibliographicReferenceObj : IdentDataInternalTypeAbstract, IIdentifiableType, IEquatable<BibliographicReferenceObj>
    {
        private int _year;

        /// <summary>
        /// Constructor
        /// </summary>
        public BibliographicReferenceObj()
        {
            _year = 1990;
            YearSpecified = false;
            Id = null;
            Name = null;
            Authors = null;
            Publication = null;
            Publisher = null;
            Editor = null;
            Volume = null;
            Issue = null;
            Pages = null;
            Title = null;
            DOI = null;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="br"></param>
        /// <param name="idata"></param>
        public BibliographicReferenceObj(BibliographicReferenceType br, IdentDataObj idata)
            : base(idata)
        {
            _year = br.year;
            YearSpecified = br.yearSpecified;
            Id = br.id;
            Name = br.name;
            Authors = br.authors;
            Publication = br.publication;
            Publisher = br.publisher;
            Editor = br.editor;
            Volume = br.volume;
            Issue = br.issue;
            Pages = br.pages;
            Title = br.title;
            DOI = br.doi;
        }

        /// <summary>The names of the authors of the reference.</summary>
        /// <remarks>Optional Attribute</remarks>
        public string Authors { get; set; }

        /// <summary>The name of the journal, book etc.</summary>
        /// <remarks>Optional Attribute</remarks>
        public string Publication { get; set; }

        /// <summary>The publisher of the publication.</summary>
        /// <remarks>Optional Attribute</remarks>
        public string Publisher { get; set; }

        /// <summary>The editor(s) of the reference.</summary>
        /// <remarks>Optional Attribute</remarks>
        public string Editor { get; set; }

        /// <summary>The year of publication.</summary>
        /// <remarks>Optional Attribute</remarks>
        public int Year
        {
            get => _year;
            set
            {
                _year = value;
                YearSpecified = true;
            }
        }

        /// <summary>
        /// True if Year has been defined
        /// </summary>
        protected internal bool YearSpecified { get; private set; }

        /// <summary>The volume name or number.</summary>
        /// <remarks>Optional Attribute</remarks>
        public string Volume { get; set; }

        /// <summary>The issue name or number.</summary>
        /// <remarks>Optional Attribute</remarks>
        public string Issue { get; set; }

        /// <summary>The page numbers.</summary>
        /// <remarks>Optional Attribute</remarks>
        public string Pages { get; set; }

        /// <summary>The title of the BibliographicReference.</summary>
        /// <remarks>Optional Attribute</remarks>
        public string Title { get; set; }

        /// <summary>The DOI of the referenced publication.</summary>
        /// <remarks>Optional Attribute</remarks>
        public string DOI { get; set; }

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
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as BibliographicReferenceObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(BibliographicReferenceObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if ((Name == other.Name) && (Authors == other.Authors) && (DOI == other.DOI) &&
                (Year == other.Year) && (Publication == other.Publication) && (Publisher == other.Publisher) &&
                (Editor == other.Editor) && (Pages == other.Pages) && (Title == other.Title))
                return true;
            return false;
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name != null ? Name.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Authors != null ? Authors.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DOI != null ? DOI.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Year;
                hashCode = (hashCode * 397) ^ (Publication != null ? Publication.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Publisher != null ? Publisher.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Editor != null ? Editor.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Pages != null ? Pages.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Title != null ? Title.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}