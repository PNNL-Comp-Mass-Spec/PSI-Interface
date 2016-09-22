using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML AuditCollectionType; Also C# representation of AuditCollectionType
    /// </summary>
    /// <remarks>A contact is either a person or an organization.</remarks>
    /// <remarks>AuditCollectionType: The complete set of Contacts (people and organisations) for this file.</remarks>
    /// <remarks>AuditCollectionType: min 1, max unbounded, for PersonType XOR OrganizationType</remarks>
    /// <remarks>CVParams, UserParams: Attributes of this contact such as address, email, telephone etc.</remarks>
    public abstract class AbstractContactObj : ParamGroupObj, IIdentifiableType, IEquatable<AbstractContactObj>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        protected AbstractContactObj()
        {
            Id = null;
            Name = null;
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="idata"></param>
        protected AbstractContactObj(AbstractContactType ac, IdentDataObj idata)
            : base(ac, idata)
        {
            Id = ac.id;
            Name = ac.name;
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

        #region Object Equality

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as AbstractContactObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(AbstractContactObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Name == other.Name && Equals(CVParams, other.CVParams) &&
                Equals(UserParams, other.UserParams))
            {
                return true;
            }
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
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}