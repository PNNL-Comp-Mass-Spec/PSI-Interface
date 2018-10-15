using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML BibliographicReferenceType
    /// </summary>
    /// <remarks>Represents bibliographic references.</remarks>
    public class BibliographicReferenceObj : IdentDataInternalTypeAbstract, IIdentifiableType, IEquatable<BibliographicReferenceObj>
    {
        private int _year;

        /// <summary>
        ///     Constructor
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
        ///     Create an object using the contents of the corresponding MzIdentML object
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

        /// <remarks>The names of the authors of the reference.</remarks>
        /// Optional Attribute
        /// string
        public string Authors { get; set; }

        /// <remarks>The name of the journal, book etc.</remarks>
        /// Optional Attribute
        /// string
        public string Publication { get; set; }

        /// <remarks>The publisher of the publication.</remarks>
        /// Optional Attribute
        /// string
        public string Publisher { get; set; }

        /// <remarks>The editor(s) of the reference.</remarks>
        /// Optional Attribute
        /// string
        public string Editor { get; set; }

        /// <remarks>The year of publication.</remarks>
        /// Optional Attribute
        /// integer
        public int Year
        {
            get => _year;
            set
            {
                _year = value;
                YearSpecified = true;
            }
        }

        /// Attribute Existence
        protected internal bool YearSpecified { get; private set; }

        /// <remarks>The volume name or number.</remarks>
        /// Optional Attribute
        /// string
        public string Volume { get; set; }

        /// <remarks>The issue name or number.</remarks>
        /// Optional Attribute
        /// string
        public string Issue { get; set; }

        /// <remarks>The page numbers.</remarks>
        /// Optional Attribute
        /// string
        public string Pages { get; set; }

        /// <remarks>The title of the BibliographicReference.</remarks>
        /// Optional Attribute
        /// string
        public string Title { get; set; }

        /// <remarks>The DOI of the referenced publication.</remarks>
        /// Optional Attribute
        /// string
        public string DOI { get; set; }

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
        public override bool Equals(object other)
        {
            var o = other as BibliographicReferenceObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
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
        ///     Object hash code
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