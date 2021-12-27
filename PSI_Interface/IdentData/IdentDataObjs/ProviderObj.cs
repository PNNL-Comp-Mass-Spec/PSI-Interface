using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML ProviderType
    /// </summary>
    /// <remarks>The provider of the document in terms of the Contact and the software the produced the document instance.</remarks>
    public class ProviderObj : IdentDataInternalTypeAbstract, IIdentifiableType, IEquatable<ProviderObj>
    {
        private AnalysisSoftwareObj _analysisSoftware;
        private string _analysisSoftwareRef;

        private ContactRoleObj _contactRole;

        /// <summary>
        /// Constructor
        /// </summary>
        public ProviderObj()
        {
            Id = null;
            Name = null;
            _analysisSoftwareRef = null;

            _analysisSoftware = null;
            _contactRole = null;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="p"></param>
        /// <param name="idata"></param>
        public ProviderObj(ProviderType p, IdentDataObj idata)
            : base(idata)
        {
            Id = p.id;
            Name = p.name;
            AnalysisSoftwareRef = p.analysisSoftware_ref;

            idata.Provider = this;

            _contactRole = null;

            if (p.ContactRole != null)
                _contactRole = new ContactRoleObj(p.ContactRole, IdentData);
        }

        /// <summary>The Contact that provided the document instance.</summary>
        /// <remarks>min 0, max 1</remarks>
        public ContactRoleObj ContactRole
        {
            get => _contactRole;
            set
            {
                _contactRole = value;
                if (_contactRole != null)
                    _contactRole.IdentData = IdentData;
            }
        }

        /// <summary>The Software that produced the document instance.</summary>
        /// <remarks>Optional Attribute</remarks>
        protected internal string AnalysisSoftwareRef
        {
            get
            {
                if (_analysisSoftware != null)
                    return _analysisSoftware.Id;
                return _analysisSoftwareRef;
            }
            set
            {
                _analysisSoftwareRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                    AnalysisSoftware = IdentData.FindAnalysisSoftware(value);
            }
        }

        /// <summary>The Software that produced the document instance.</summary>
        /// <remarks>Optional Attribute</remarks>
        public AnalysisSoftwareObj AnalysisSoftware
        {
            get => _analysisSoftware;
            set
            {
                _analysisSoftware = value;
                if (_analysisSoftware != null)
                {
                    _analysisSoftware.IdentData = IdentData;
                    _analysisSoftwareRef = _analysisSoftware.Id;
                }
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

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is ProviderObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(ProviderObj other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return Name == other.Name && Equals(ContactRole, other.ContactRole) &&
                   Equals(AnalysisSoftware, other.AnalysisSoftware);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (ContactRole?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (AnalysisSoftware?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}
