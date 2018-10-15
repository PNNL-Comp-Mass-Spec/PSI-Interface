using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML UserParamType
    /// </summary>
    /// <remarks>A single user-defined parameter.</remarks>
    public class UserParamObj : ParamBaseObj, IEquatable<UserParamObj>
    {
        private string _name;
        private string _value;

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public UserParamObj()
        {
            _name = null;
            _value = null;
            Type = null;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="up"></param>
        /// <param name="idata"></param>
        public UserParamObj(UserParamType up, IdentDataObj idata)
            : base(idata)
        {
            _name = up.name;
            _value = up.value;
            Type = up.type;

            UnitCvRef = up.unitCvRef;
            //this._unitAccession = up.unitAccession;
            UnitAccession = up.unitAccession;
            //this._unitName = up.unitName;
        }

        #endregion

        #region Properties

        /// <remarks>The name of the parameter.</remarks>
        /// Required Attribute
        /// string
        public override string Name
        {
            get => _name;
            set => _name = value;
        }

        /// <remarks>The user-entered value of the parameter.</remarks>
        /// Optional Attribute
        /// string
        public override string Value
        {
            get => _value;
            set => _value = value;
        }

        /// <remarks>The data type of the parameter, where appropriate (e.g.: xsd:float).</remarks>
        /// Optional Attribute
        /// string
        public string Type { get; set; }

        #endregion

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(UserParamObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Name == other.Name && Value == other.Value && Type == other.Type &&
                UnitCvid == other.UnitCvid)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as UserParamObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
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
                hashCode = (hashCode * 397) ^ (Value != null ? Value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Type != null ? Type.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ UnitCvid.GetHashCode();
                return hashCode;
            }
        }

        #endregion
    }
}