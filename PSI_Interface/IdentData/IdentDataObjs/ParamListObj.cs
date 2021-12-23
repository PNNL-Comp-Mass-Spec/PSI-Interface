using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML ParamListType
    /// </summary>
    /// <remarks>Helper type to allow multiple cvParams or userParams to be given for an element.</remarks>
    public class ParamListObj : IdentDataInternalTypeAbstract, IEquatable<ParamListObj>
    {
        private IdentDataList<ParamBaseObj> _items;

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public ParamListObj()
        {
            Items = new IdentDataList<ParamBaseObj>(1);
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="pl"></param>
        /// <param name="idata"></param>
        public ParamListObj(ParamListType pl, IdentDataObj idata)
            : base(idata)
        {
            Items = new IdentDataList<ParamBaseObj>(1);

            if (pl?.Items.Count > 0)
            {
                Items.AddRange(pl.Items, p =>
                {
                    if (p is CVParamType cvp)
                    {
                        return new CVParamObj(cvp, IdentData);
                    }

                    if (p is UserParamType up)
                    {
                        return new UserParamObj(up, IdentData);
                    }

                    return null;
                });
            }
        }
        #endregion

        #region Properties

        /// <summary>
        /// List of items
        /// </summary>
        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<ParamBaseObj> Items
        {
            get => _items;
            set
            {
                _items = value;
                if (_items != null)
                {
                    _items.IdentData = IdentData;
                }
            }
        }
        #endregion

        #region Object Equality
        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(ParamListObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(Items, other.Items))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            var o = other as ParamListObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            var hashCode = Items != null ? Items.GetHashCode() : 0;
            return hashCode;
        }
        #endregion
    }
}
