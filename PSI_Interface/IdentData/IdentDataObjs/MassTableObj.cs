using System;
using System.Collections.Generic;
using PSI_Interface.IdentData.mzIdentML;
using PSI_Interface.Utils;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML MassTableType
    /// </summary>
    /// <remarks>The masses of residues used in the search.</remarks>
    /// <remarks>CVParams/UserParams: Additional parameters or descriptors for the MassTable.</remarks>
    public class MassTableObj : ParamGroupObj, IIdentifiableType, IEquatable<MassTableObj>
    {
        private IdentDataList<AmbiguousResidueObj> _ambiguousResidues;

        private IdentDataList<ResidueObj> _residues;

        /// <summary>
        /// Constructor
        /// </summary>
        public MassTableObj()
        {
            Id = null;
            Name = null;

            Residues = new IdentDataList<ResidueObj>(1);
            AmbiguousResidues = new IdentDataList<AmbiguousResidueObj>(1);
            MsLevels = new List<string>();
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="mt"></param>
        /// <param name="idata"></param>
        public MassTableObj(MassTableType mt, IdentDataObj idata)
            : base(mt, idata)
        {
            Id = mt.id;
            Name = mt.name;

            Residues = new IdentDataList<ResidueObj>(1);
            AmbiguousResidues = new IdentDataList<AmbiguousResidueObj>(1);
            MsLevels = null;

            if ((mt.Residue?.Count > 0))
            {
                Residues.AddRange(mt.Residue, r => new ResidueObj(r, IdentData));
            }
            if ((mt.AmbiguousResidue?.Count > 0))
            {
                AmbiguousResidues.AddRange(mt.AmbiguousResidue, ar => new AmbiguousResidueObj(ar, IdentData));
            }
            if (mt.msLevel != null)
                MsLevels = new List<string>(mt.msLevel);
        }

        /// <summary>The specification of a single residue within the mass table.</summary>
        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<ResidueObj> Residues
        {
            get => _residues;
            set
            {
                _residues = value;
                if (_residues != null)
                    _residues.IdentData = IdentData;
            }
        }

        /// <summary>min 0, max unbounded</summary>
        public IdentDataList<AmbiguousResidueObj> AmbiguousResidues
        {
            get => _ambiguousResidues;
            set
            {
                _ambiguousResidues = value;
                if (_ambiguousResidues != null)
                    _ambiguousResidues.IdentData = IdentData;
            }
        }

        /// <summary>The MS spectrum that the MassTable refers to e.g. "1" for MS1 "2" for MS2 or "1 2" for MS1 or MS2.</summary>
        /// <remarks>Required Attribute</remarks>
        public List<string> MsLevels { get; set; }

        /// <summary>
        /// An identifier is an unambiguous string that is unique within the scope
        /// (i.e. a document, a set of related documents, or a repository) of its use.
        /// </summary>
        /// <remarks>Required Attribute</remarks>
        public string Id { get; set; }

        /// <summary>The potentially ambiguous common identifier, such as a human-readable name for the instance.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Name { get; set; }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is MassTableObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(MassTableObj other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return Name == other.Name && Equals(Residues, other.Residues) &&
                   Equals(AmbiguousResidues, other.AmbiguousResidues) &&
                   ListUtils.ListEqualsUnOrdered(MsLevels, other.MsLevels) && Equals(CVParams, other.CVParams) &&
                   Equals(UserParams, other.UserParams);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (Residues?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (AmbiguousResidues?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (MsLevels?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (CVParams?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (UserParams?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}
