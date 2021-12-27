using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML ProtocolApplicationType
    /// </summary>
    /// <remarks>
    /// The use of a protocol with the requisite Parameters and ParameterValues.
    /// ProtocolApplications can take Material or Data (or both) as input and produce Material or Data (or both) as output.
    /// </remarks>
    public abstract class ProtocolApplicationObj : IdentDataInternalTypeAbstract, IIdentifiableType, IEquatable<ProtocolApplicationObj>
    {
        private DateTime _activityDate;

        /// <summary>
        /// Constructor
        /// </summary>
        protected ProtocolApplicationObj()
        {
            Id = null;
            Name = null;
            _activityDate = DateTime.Now;
            ActivityDateSpecified = false;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="pa"></param>
        /// <param name="idata"></param>
        protected ProtocolApplicationObj(ProtocolApplicationType pa, IdentDataObj idata)
            : base(idata)
        {
            Id = pa.id;
            Name = pa.name;
            _activityDate = pa.activityDate;
            ActivityDateSpecified = pa.activityDateSpecified;
        }

        /// <summary>When the protocol was applied.</summary>
        /// <remarks>Optional Attribute</remarks>
        public DateTime ActivityDate
        {
            get => _activityDate;
            set
            {
                _activityDate = value;
                ActivityDateSpecified = true;
            }
        }

        /// <summary>
        /// True if Activity Date has been defined
        /// </summary>
        protected internal bool ActivityDateSpecified { get; private set; }

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
            return other is ProtocolApplicationObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(ProtocolApplicationObj other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return Name == other.Name;
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            return Name?.GetHashCode() ?? 0;
        }

        #endregion
    }
}
