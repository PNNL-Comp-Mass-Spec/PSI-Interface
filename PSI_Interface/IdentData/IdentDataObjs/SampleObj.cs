using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML SampleType : Containers AnalysisSampleCollectionType
    /// </summary>
    /// <remarks>
    /// A description of the sample analyzed by mass spectrometry using CVParams or UserParams.
    /// If a composite sample has been analyzed, a parent sample should be defined, which references subsamples.
    /// This represents any kind of substance used in an experimental workflow, such as whole organisms, cells,
    /// DNA, solutions, compounds and experimental substances (gels, arrays etc.).
    /// </remarks>
    /// <remarks>
    /// AnalysisSampleCollectionType: The samples analyzed can optionally be recorded using CV terms for descriptions.
    /// If a composite sample has been analyzed, the subsample association can be used to build a hierarchical description.
    /// </remarks>
    /// <remarks>AnalysisSampleCollectionType: child element Sample of type SampleType, min 1, max unbounded</remarks>
    /// <remarks>CVParams/UserParams: The characteristics of a Material.</remarks>
    public class SampleObj : ParamGroupObj, IIdentifiableType, IEquatable<SampleObj>
    {
        private IdentDataList<ContactRoleObj> _contactRoles;
        private IdentDataList<SubSampleObj> _subSamples;

        /// <summary>
        /// Constructor
        /// </summary>
        public SampleObj()
        {
            Id = null;
            Name = null;

            ContactRoles = new IdentDataList<ContactRoleObj>(1);
            SubSamples = new IdentDataList<SubSampleObj>(1);
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="s"></param>
        /// <param name="idata"></param>
        public SampleObj(SampleType s, IdentDataObj idata)
            : base(s, idata)
        {
            Id = s.id;
            Name = s.name;

            ContactRoles = new IdentDataList<ContactRoleObj>(1);
            SubSamples = new IdentDataList<SubSampleObj>(1);

            if ((s.ContactRole?.Count > 0))
            {
                ContactRoles.AddRange(s.ContactRole, cr => new ContactRoleObj(cr, IdentData));
            }
            if ((s.SubSample?.Count > 0))
            {
                SubSamples.AddRange(s.SubSample, ss => new SubSampleObj(ss, IdentData));
            }
        }

        /// <summary>
        /// Contact details for the Material. The association to ContactRole could specify, for example, the creator or
        /// provider of the Material.
        /// </summary>
        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<ContactRoleObj> ContactRoles
        {
            get => _contactRoles;
            set
            {
                _contactRoles = value;
                if (_contactRoles != null)
                    _contactRoles.IdentData = IdentData;
            }
        }

        /// <summary>min 0, max unbounded</summary>
        public IdentDataList<SubSampleObj> SubSamples
        {
            get => _subSamples;
            set
            {
                _subSamples = value;
                if (_subSamples != null)
                    _subSamples.IdentData = IdentData;
            }
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(SampleObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if ((Name == other.Name) && Equals(ContactRoles, other.ContactRoles) &&
                Equals(SubSamples, other.SubSamples) && Equals(CVParams, other.CVParams) &&
                Equals(UserParams, other.UserParams))
                return true;
            return false;
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
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public new bool Equals(object other)
        {
            var o = other as SampleObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (ContactRoles?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (SubSamples?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (CVParams?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (UserParams?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}
