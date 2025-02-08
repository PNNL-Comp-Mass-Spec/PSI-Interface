using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML PeptideType
    /// </summary>
    /// <remarks>
    /// One (poly)peptide (a sequence with modifications). The combination of Peptide sequence and modifications must
    /// be unique in the file.
    /// </remarks>
    /// <remarks>CVParams/UserParams: Additional descriptors of this peptide sequence</remarks>
    public class PeptideObj : ParamGroupObj, IIdentifiableType, IEquatable<PeptideObj>
    {
        private IdentDataList<ModificationObj> _modifications;
        private IdentDataList<SubstitutionModificationObj> _substitutionModifications;

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="p"></param>
        /// <param name="idata"></param>
        public PeptideObj(PeptideType p, IdentDataObj idata)
            : base(p, idata)
        {
            Id = p.id;
            Name = p.name;
            PeptideSequence = p.PeptideSequence;

            Modifications = new IdentDataList<ModificationObj>(1);
            SubstitutionModifications = new IdentDataList<SubstitutionModificationObj>(1);

            if (p.Modification != null)
            {
                Modifications.AddRange(p.Modification, m => new ModificationObj(m, idata));
            }

            if (p.SubstitutionModification != null)
            {
                SubstitutionModifications.AddRange(p.SubstitutionModification, sm => new SubstitutionModificationObj(sm, idata));
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public PeptideObj()
        {
            Id = null;
            Name = null;
            PeptideSequence = null;

            Modifications = new IdentDataList<ModificationObj>(1);
            SubstitutionModifications = new IdentDataList<SubstitutionModificationObj>(1);
        }

        /// <summary>
        /// Create a peptide with the specified sequence
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns>A plain peptide with not modifications (must be added)</returns>
        public PeptideObj(string sequence) : this()
        {
            PeptideSequence = sequence;
        }

        /// <summary>
        /// The amino acid sequence of the (poly)peptide. If a substitution modification has been found, the original
        /// sequence should be reported.
        /// </summary>
        /// <remarks>min 1, max 1</remarks>
        public string PeptideSequence { get; set; }

        /// <summary>min 0, max unbounded</summary>
        public IdentDataList<ModificationObj> Modifications
        {
            get => _modifications;
            set
            {
                _modifications = value;
                if (_modifications != null)
                    _modifications.IdentData = IdentData;
            }
        }

        /// <summary>min 0, max unbounded</summary>
        public IdentDataList<SubstitutionModificationObj> SubstitutionModifications
        {
            get => _substitutionModifications;
            set
            {
                _substitutionModifications = value;
                if (_substitutionModifications != null)
                    _substitutionModifications.IdentData = IdentData;
            }
        }

        /// <summary>
        /// An identifier is an unambiguous string that is unique within the scope
        /// (i.e. a document, a set of related documents, or a repository) of its use.
        /// </summary>
        /// <remarks>Required Attribute</remarks>
        public string Id { get; set; }

        /// <summary>The potentially ambiguous common identifier, such as a human-readable name for the instance.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Name { get; set; }

        /// <summary>
        /// Show the peptide sequence and the modification count (if non-zero)
        /// </summary>
        public override string ToString()
        {
            var modCount = Modifications?.Count ?? 0;

            if (modCount == 0)
            {
                return string.Format("{0}", PeptideSequence ?? string.Empty);
            }

            return string.Format("{0} ({1} {2})",
                PeptideSequence ?? string.Empty,
                modCount,
                modCount == 1 ? "mod" : "mods");
        }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is PeptideObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(PeptideObj other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return Name == other.Name &&
                   PeptideSequence == other.PeptideSequence &&
                   Equals(Modifications, other.Modifications) &&
                   Equals(SubstitutionModifications, other.SubstitutionModifications) &&
                   ParamsEquals(other);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (PeptideSequence?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Modifications?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (SubstitutionModifications?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (CVParams?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (UserParams?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}
