using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML AmbiguousResidueType
    /// </summary>
    /// <remarks>
    /// Ambiguous residues e.g. X can be specified by the Code attribute and a set of parameters
    /// for example giving the different masses that will be used in the search.
    /// </remarks>
    /// <remarks>CVParams and UserParams: Parameters for capturing e.g. "alternate single letter codes"</remarks>
    public class AmbiguousResidueObj : ParamGroupObj, IEquatable<AmbiguousResidueObj>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AmbiguousResidueObj()
        {
            Code = null;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="ar"></param>
        /// <param name="idata"></param>
        public AmbiguousResidueObj(AmbiguousResidueType ar, IdentDataObj idata)
            : base(ar, idata)
        {
            Code = ar.code;
        }

        /// <summary>The single letter code of the ambiguous residue e.g. X.</summary>
        /// <remarks>Required Attribute</remarks>
        /// <returns>RegEx: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}"</returns>
        public string Code { get; set; }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is AmbiguousResidueObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(AmbiguousResidueObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other == null)
            {
                return false;
            }

            return Code == other.Code && Equals(CVParams, other.CVParams) &&
                   Equals(UserParams, other.UserParams);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Code?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (CVParams?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (UserParams?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}
