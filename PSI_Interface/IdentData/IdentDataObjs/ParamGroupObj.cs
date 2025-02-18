﻿using System;
using JetBrains.Annotations;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// Object for containing multiple CVParamObjs and UserParamObjs
    /// </summary>
    public class ParamGroupObj : CVParamGroupObj, IEquatable<ParamGroupObj>
    {
        private IdentDataList<UserParamObj> _userParams;

        /// <summary>
        /// Constructor
        /// </summary>
        public ParamGroupObj()
        {
            UserParams = new IdentDataList<UserParamObj>(1);
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="pg"></param>
        /// <param name="idata"></param>
        public ParamGroupObj(IParamGroup pg, IdentDataObj idata)
            : base(pg, idata)
        {
            UserParams = new IdentDataList<UserParamObj>(1);

            if (pg.userParam?.Count > 0)
            {
                UserParams.AddRange(pg.userParam, up => new UserParamObj(up, IdentData));
            }
        }

        /// <summary>
        /// List of UserParams
        /// </summary>
        public IdentDataList<UserParamObj> UserParams
        {
            get => _userParams;
            set
            {
                _userParams = value;

                if (_userParams != null)
                {
                    _userParams.IdentData = IdentData;
                }
            }
        }

        /// <summary>
        /// Object equality, with no null check
        /// </summary>
        /// <param name="other"></param>
        public bool ParamsEquals([NotNull] ParamGroupObj other)
        {
            return Equals(CVParams, other.CVParams) && Equals(UserParams, other.UserParams);
        }

        #region Object Equality
        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
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

            return ParamsEquals(other);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is ParamGroupObj o && Equals(o);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = CVParams?.GetHashCode() ?? 0;
                return (hashCode * 397) ^ (UserParams?.GetHashCode() ?? 0);
            }
        }
        #endregion
    }
}
