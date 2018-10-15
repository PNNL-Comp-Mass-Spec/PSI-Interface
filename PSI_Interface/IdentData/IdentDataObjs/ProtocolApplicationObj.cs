using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML ProtocolApplicationType
    /// </summary>
    /// <remarks>
    ///     The use of a protocol with the requisite Parameters and ParameterValues.
    ///     ProtocolApplications can take Material or Data (or both) as input and produce Material or Data (or both) as output.
    /// </remarks>
    public abstract class ProtocolApplicationObj : IdentDataInternalTypeAbstract, IIdentifiableType, IEquatable<ProtocolApplicationObj>
    {
        private DateTime _activityDate;

        /// <summary>
        ///     Constructor
        /// </summary>
        protected ProtocolApplicationObj()
        {
            Id = null;
            Name = null;
            _activityDate = DateTime.Now;
            ActivityDateSpecified = false;
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
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

        /// <remarks>When the protocol was applied.</remarks>
        /// Optional Attribute
        /// datetime
        public DateTime ActivityDate
        {
            get => _activityDate;
            set
            {
                _activityDate = value;
                ActivityDateSpecified = true;
            }
        }

        /// Attribute Existence
        protected internal bool ActivityDateSpecified { get; private set; }

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
            var o = other as ProtocolApplicationObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ProtocolApplicationObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if (Name == other.Name)
                return true;
            return false;
        }

        /// <summary>
        ///     Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = Name != null ? Name.GetHashCode() : 0;
            return hashCode;
        }

        #endregion
    }
}