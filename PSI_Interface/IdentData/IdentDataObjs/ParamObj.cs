using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML ParamType
    /// </summary>
    /// <remarks>Helper type to allow either a cvParam or a userParam to be provided for an element.</remarks>
    public class ParamObj : IdentDataInternalTypeAbstract, IEquatable<ParamObj>
    {
        private ParamBaseObj _item;

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public ParamObj()
        {
            _item = null;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="p"></param>
        /// <param name="idata"></param>
        public ParamObj(ParamType p, IdentDataObj idata)
            : base(idata)
        {
            _item = null;

            switch (p.Item)
            {
                case null:
                    return;
                case CVParamType cvParam:
                    _item = new CVParamObj(cvParam, IdentData);
                    break;
                case UserParamType userParam:
                    _item = new UserParamObj(userParam, IdentData);
                    break;
            }
        }
        #endregion

        #region Properties

        /// <summary>
        /// Parameter value
        /// </summary>
        /// <remarks>min 1, max 1</remarks>
        public ParamBaseObj Item
        {
            get => _item;
            set
            {
                _item = value;
                if (_item != null)
                {
                    _item.IdentData = IdentData;
                }
            }
        }
        #endregion

        #region Object Equality
        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(ParamObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return other != null && Equals(Item, other.Item);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is ParamObj o && Equals(o);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            return Item != null ? Item.GetHashCode() : 0;
        }
        #endregion
    }
}
