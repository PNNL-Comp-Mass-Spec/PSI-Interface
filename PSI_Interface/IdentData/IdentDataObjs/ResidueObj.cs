using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML ResidueType
    /// </summary>
    public class ResidueObj : IdentDataInternalTypeAbstract, IEquatable<ResidueObj>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public ResidueObj()
        {
            Code = null;
            Mass = -1;
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="r"></param>
        /// <param name="idata"></param>
        public ResidueObj(ResidueType r, IdentDataObj idata)
            : base(idata)
        {
            Code = r.code;
            Mass = r.mass;
        }

        /// <remarks>The single letter code for the residue.</remarks>
        /// Required Attribute
        /// chars, string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}"
        public string Code { get; set; }

        /// <remarks>The residue mass in Daltons (not including any fixed modifications).</remarks>
        /// Required Attribute
        /// float
        public float Mass { get; set; }

        #region Object Equality

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as ResidueObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ResidueObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if ((Code == other.Code) && Mass.Equals(other.Mass))
                return true;
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
                var hashCode = Code != null ? Code.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ Mass.GetHashCode();
                return hashCode;
            }
        }

        #endregion
    }
}