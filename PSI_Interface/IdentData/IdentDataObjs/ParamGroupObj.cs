using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     Object for containing multiple CVParamObjs and UserParamObjs
    /// </summary>
    public class ParamGroupObj : CVParamGroupObj, IEquatable<ParamGroupObj>
    {
        private IdentDataList<UserParamObj> _userParams;

        #region Constructors
        /// <summary>
        ///     Constructor
        /// </summary>
        public ParamGroupObj()
        {
            UserParams = new IdentDataList<UserParamObj>(1);
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="pg"></param>
        /// <param name="idata"></param>
        public ParamGroupObj(IParamGroup pg, IdentDataObj idata)
            : base(pg, idata)
        {
            UserParams = new IdentDataList<UserParamObj>(1);

            if (pg.userParam != null && pg.userParam.Count > 0)
            {
                UserParams.AddRange(pg.userParam, up => new UserParamObj(up, IdentData));
            }
        }
        #endregion

        #region Properties
        /// <summary>
        ///     List of UserParams
        /// </summary>
        public IdentDataList<UserParamObj> UserParams
        {
            get { return _userParams; }
            set
            {
                _userParams = value;
                if (_userParams != null)
                {
                    _userParams.IdentData = IdentData;
                }
            }
        }
        #endregion

        #region Object Equality
        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ParamGroupObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(CVParams, other.CVParams) && Equals(UserParams, other.UserParams))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as ParamGroupObj;
            if (o == null)
            {
                return false;
            }
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
                var hashCode = CVParams != null ? CVParams.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }
        #endregion
    }
}