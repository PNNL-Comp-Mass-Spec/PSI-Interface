using System;
using System.Collections.Generic;
using PSI_Interface.IdentData.mzIdentML;
using PSI_Interface.Utils;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML MassTableType
    /// </summary>
    /// <remarks>The masses of residues used in the search.</remarks>
    /// <remarks>CVParams/UserParams: Additional parameters or descriptors for the MassTable.</remarks>
    public class MassTableObj : ParamGroupObj, IIdentifiableType, IEquatable<MassTableObj>
    {
        private IdentDataList<AmbiguousResidueObj> _ambiguousResidues;

        private IdentDataList<ResidueObj> _residues;

        /// <summary>
        ///     Constructor
        /// </summary>
        public MassTableObj()
        {
            Id = null;
            Name = null;

            Residues = new IdentDataList<ResidueObj>();
            AmbiguousResidues = new IdentDataList<AmbiguousResidueObj>();
            MsLevels = new List<string>();
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="mt"></param>
        /// <param name="idata"></param>
        public MassTableObj(MassTableType mt, IdentDataObj idata)
            : base(mt, idata)
        {
            Id = mt.id;
            Name = mt.name;

            _residues = null;
            _ambiguousResidues = null;
            MsLevels = null;

            if ((mt.Residue != null) && (mt.Residue.Count > 0))
            {
                Residues = new IdentDataList<ResidueObj>();
                foreach (var r in mt.Residue)
                    Residues.Add(new ResidueObj(r, IdentData));
            }
            if ((mt.AmbiguousResidue != null) && (mt.AmbiguousResidue.Count > 0))
            {
                AmbiguousResidues = new IdentDataList<AmbiguousResidueObj>();
                foreach (var ar in mt.AmbiguousResidue)
                    AmbiguousResidues.Add(new AmbiguousResidueObj(ar, IdentData));
            }
            if (mt.msLevel != null)
                MsLevels = new List<string>(mt.msLevel);
        }

        /// <remarks>The specification of a single residue within the mass table.</remarks>
        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<ResidueObj> Residues
        {
            get { return _residues; }
            set
            {
                _residues = value;
                if (_residues != null)
                    _residues.IdentData = IdentData;
            }
        }

        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<AmbiguousResidueObj> AmbiguousResidues
        {
            get { return _ambiguousResidues; }
            set
            {
                _ambiguousResidues = value;
                if (_ambiguousResidues != null)
                    _ambiguousResidues.IdentData = IdentData;
            }
        }

        /// <remarks>The MS spectrum that the MassTable refers to e.g. "1" for MS1 "2" for MS2 or "1 2" for MS1 or MS2.</remarks>
        /// Required Attribute
        /// integer(s), space separated
        public List<string> MsLevels { get; set; }

        /// <remarks>
        ///     An identifier is an unambiguous string that is unique within the scope
        ///     (i.e. a document, a set of related documents, or a repository) of its use.
        /// </remarks>
        /// Required Attribute
        /// string
        public string Id { get; set; }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name { get; set; }

        #region Object Equality

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as MassTableObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(MassTableObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if ((Name == other.Name) && Equals(Residues, other.Residues) &&
                Equals(AmbiguousResidues, other.AmbiguousResidues) &&
                ListUtils.ListEqualsUnOrdered(MsLevels, other.MsLevels) && Equals(CVParams, other.CVParams) &&
                Equals(UserParams, other.UserParams))
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
                var hashCode = Name != null ? Name.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Residues != null ? Residues.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AmbiguousResidues != null ? AmbiguousResidues.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (MsLevels != null ? MsLevels.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}