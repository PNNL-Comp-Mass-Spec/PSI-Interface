using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML SampleType : Containers AnalysisSampleCollectionType
    /// </summary>
    /// <remarks>
    ///     A description of the sample analysed by mass spectrometry using CVParams or UserParams.
    ///     If a composite sample has been analysed, a parent sample should be defined, which references subsamples.
    ///     This represents any kind of substance used in an experimental workflow, such as whole organisms, cells,
    ///     DNA, solutions, compounds and experimental substances (gels, arrays etc.).
    /// </remarks>
    /// <remarks>
    ///     AnalysisSampleCollectionType: The samples analysed can optionally be recorded using CV terms for descriptions.
    ///     If a composite sample has been analysed, the subsample association can be used to build a hierarchical description.
    /// </remarks>
    /// <remarks>AnalysisSampleCollectionType: child element Sample of type SampleType, min 1, max unbounded</remarks>
    /// <remarks>CVParams/UserParams: The characteristics of a Material.</remarks>
    public class SampleObj : ParamGroupObj, IIdentifiableType, IEquatable<SampleObj>
    {
        private IdentDataList<ContactRoleObj> _contactRoles;
        private IdentDataList<SubSampleObj> _subSamples;

        /// <summary>
        ///     Constructor
        /// </summary>
        public SampleObj()
        {
            Id = null;
            Name = null;

            ContactRoles = new IdentDataList<ContactRoleObj>();
            SubSamples = new IdentDataList<SubSampleObj>();
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="s"></param>
        /// <param name="idata"></param>
        public SampleObj(SampleType s, IdentDataObj idata)
            : base(s, idata)
        {
            Id = s.id;
            Name = s.name;

            _contactRoles = null;
            _subSamples = null;

            if ((s.ContactRole != null) && (s.ContactRole.Count > 0))
            {
                ContactRoles = new IdentDataList<ContactRoleObj>();
                foreach (var cr in s.ContactRole)
                    ContactRoles.Add(new ContactRoleObj(cr, IdentData));
            }
            if ((s.SubSample != null) && (s.SubSample.Count > 0))
            {
                SubSamples = new IdentDataList<SubSampleObj>();
                foreach (var ss in s.SubSample)
                    SubSamples.Add(new SubSampleObj(ss, IdentData));
            }
        }

        /// <remarks>
        ///     Contact details for the Material. The association to ContactRole could specify, for example, the creator or
        ///     provider of the Material.
        /// </remarks>
        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<ContactRoleObj> ContactRoles
        {
            get { return _contactRoles; }
            set
            {
                _contactRoles = value;
                if (_contactRoles != null)
                    _contactRoles.IdentData = IdentData;
            }
        }

        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<SubSampleObj> SubSamples
        {
            get { return _subSamples; }
            set
            {
                _subSamples = value;
                if (_subSamples != null)
                    _subSamples.IdentData = IdentData;
            }
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
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

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public new bool Equals(object other)
        {
            var o = other as SampleObj;
            if (o == null)
                return false;
            return Equals(o);
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
                hashCode = (hashCode * 397) ^ (ContactRoles != null ? ContactRoles.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SubSamples != null ? SubSamples.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}