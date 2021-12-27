using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML ResidueType
    /// </summary>
    public class ResidueObj : IdentDataInternalTypeAbstract, IEquatable<ResidueObj>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ResidueObj()
        {
            Code = null;
            Mass = -1;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="r"></param>
        /// <param name="idata"></param>
        public ResidueObj(ResidueType r, IdentDataObj idata)
            : base(idata)
        {
            Code = r.code;
            Mass = r.mass;
        }

        /// <summary>The single letter code for the residue.</summary>
        /// <remarks>Required Attribute</remarks>
        /// <returns>RegEx: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}"</returns>
        public string Code { get; set; }

        /// <summary>The residue mass in Daltons (not including any fixed modifications).</summary>
        /// <remarks>Required Attribute</remarks>
        public float Mass { get; set; }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is ResidueObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(ResidueObj other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return Code == other.Code && Mass.Equals(other.Mass);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Code?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ Mass.GetHashCode();
                return hashCode;
            }
        }

        #endregion
    }
}
