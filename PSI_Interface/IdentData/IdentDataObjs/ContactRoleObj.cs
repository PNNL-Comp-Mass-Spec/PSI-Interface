using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML ContactRoleType
    /// </summary>
    /// <remarks>
    /// The role that a Contact plays in an organization or with respect to the associating class.
    /// A Contact may have several Roles within scope, and as such, associations to ContactRole
    /// allow the use of a Contact in a certain manner. Examples might include a provider, or a data analyst.
    /// </remarks>
    public class ContactRoleObj : IdentDataInternalTypeAbstract, IEquatable<ContactRoleObj>
    {
        private AbstractContactObj _contact;
        private string _contactRef;

        private RoleObj _role;

        /// <summary>
        /// Constructor
        /// </summary>
        public ContactRoleObj()
        {
            _contactRef = null;

            _contact = null;
            _role = null;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="cr"></param>
        /// <param name="idata"></param>
        public ContactRoleObj(ContactRoleType cr, IdentDataObj idata)
            : base(idata)
        {
            ContactRef = cr.contact_ref;

            _role = null;

            if (cr.Role != null)
                _role = new RoleObj(cr.Role, IdentData);
        }

        /// <summary>min 1, max 1</summary>
        public RoleObj Role
        {
            get => _role;
            set
            {
                _role = value;
                if (_role != null)
                    _role.IdentData = IdentData;
            }
        }

        /// <summary>When a ContactRole is used, it specifies which Contact the role is associated with.</summary>
        /// <remarks>Required Attribute</remarks>
        protected internal string ContactRef
        {
            get
            {
                if (_contact != null)
                    return _contact.Id;
                return _contactRef;
            }
            set
            {
                _contactRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                    Contact = IdentData.FindContact(value);
            }
        }

        /// <summary>When a ContactRole is used, it specifies which Contact the role is associated with.</summary>
        /// <remarks>Required Attribute</remarks>
        public AbstractContactObj Contact
        {
            get => _contact;
            set
            {
                _contact = value;
                if (_contact != null)
                {
                    _contact.IdentData = IdentData;
                    _contactRef = _contact.Id;
                }
            }
        }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as ContactRoleObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ContactRoleObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if (Equals(Contact, other.Contact) && Equals(Role, other.Role))
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
                var hashCode = Contact != null ? Contact.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Role != null ? Role.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}