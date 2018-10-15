using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML PeptideEvidenceType
    /// </summary>
    /// <remarks>
    ///     PeptideEvidence links a specific Peptide element to a specific position in a DBSequence.
    ///     There must only be one PeptideEvidence item per Peptide-to-DBSequence-position.
    /// </remarks>
    /// <remarks>CVParams/UserParams: Additional parameters or descriptors for the PeptideEvidence.</remarks>
    public class PeptideEvidenceObj : ParamGroupObj, IIdentifiableType, IEquatable<PeptideEvidenceObj>
    {
        private DbSequenceObj _dBSequence;

        //private IdentDataList<CVParamType> _cvParams;
        //private IdentDataList<UserParamType> _userParams;
        private string _dBSequenceRef;
        private int _end;
        private int _frame;
        private PeptideObj _peptide;
        private string _peptideRef;
        private int _start;
        private TranslationTableObj _translationTable;
        private string _translationTableRef;

        /// <summary>
        ///     Constructor
        /// </summary>
        public PeptideEvidenceObj()
        {
            _dBSequenceRef = null;
            _peptideRef = null;
            _start = -1;
            StartSpecified = false;
            _end = -1;
            EndSpecified = false;
            Pre = null;
            Post = null;
            _translationTableRef = null;
            _frame = -1;
            FrameSpecified = false;
            IsDecoy = false;
            Id = null;
            Name = null;

            _dBSequence = null;
            _peptide = null;
            _translationTable = null;
        }

        /// <summary>
        ///     Create a peptide evidence with the specified values
        /// </summary>
        /// <param name="dbSeq">Valid <see cref="DbSequenceObj" /> object, not null</param>
        /// <param name="peptide">Valid <see cref="PeptideObj" /> object, not null</param>
        /// <param name="start">start position of the peptide in the sequence</param>
        /// <param name="end">end position of the peptide in the sequence</param>
        /// <param name="pre">prefix residue, "." if the beginning of the sequence</param>
        /// <param name="post">post/suffix residue, "." if the end of the sequence</param>
        /// <param name="isDecoy">true if the peptide is a decoy</param>
        /// <returns></returns>
        public PeptideEvidenceObj(DbSequenceObj dbSeq, PeptideObj peptide, int start, int end,
            string pre, string post, bool isDecoy = false) : this()
        {
            DBSequence = dbSeq ?? throw new ArgumentNullException(nameof(dbSeq), "Argument cannot be null.");
            Peptide = peptide ?? throw new ArgumentNullException(nameof(peptide), "Argument cannot be null.");
            Start = start;
            End = end;
            IsDecoy = isDecoy;
            Pre = pre;
            Post = post;
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="idata"></param>
        public PeptideEvidenceObj(PeptideEvidenceType pe, IdentDataObj idata)
            : base(pe, idata)
        {
            DBSequenceRef = pe.dBSequence_ref;
            PeptideRef = pe.peptide_ref;
            _start = pe.start;
            StartSpecified = pe.startSpecified;
            _end = pe.end;
            EndSpecified = pe.endSpecified;
            Pre = pe.pre;
            Post = pe.post;
            TranslationTableRef = pe.translationTable_ref;
            _frame = pe.frame;
            FrameSpecified = pe.frameSpecified;
            IsDecoy = pe.isDecoy;
            Id = pe.id;
            Name = pe.name;
        }

        /// <remarks>Set to true if the peptide is matched to a decoy sequence.</remarks>
        /// Optional Attribute
        /// boolean, default false
        public bool IsDecoy { get; set; }

        /// <remarks>
        ///     Previous flanking residue. If the peptide is N-terminal, pre="-" and not pre="".
        ///     If for any reason it is unknown (e.g. denovo), pre="?" should be used.
        /// </remarks>
        /// Optional Attribute
        /// string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"
        public string Pre { get; set; }

        /// <remarks>
        ///     Post flanking residue. If the peptide is C-terminal, post="-" and not post="". If for any reason it is unknown
        ///     (e.g. denovo), post="?" should be used.
        /// </remarks>
        /// Optional Attribute
        /// string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"
        public string Post { get; set; }

        /// <remarks>
        ///     Start position of the peptide inside the protein sequence, where the first amino acid of the
        ///     protein sequence is position 1. Must be provided unless this is a de novo search.
        /// </remarks>
        /// Optional Attribute
        /// integer
        public int Start
        {
            get => _start;
            set
            {
                _start = value;
                StartSpecified = true;
            }
        }

        /// Attribute Existence
        protected internal bool StartSpecified { get; private set; }

        /// <remarks>
        ///     The index position of the last amino acid of the peptide inside the protein sequence, where the first
        ///     amino acid of the protein sequence is position 1. Must be provided unless this is a de novo search.
        /// </remarks>
        /// Optional Attribute
        /// integer
        public int End
        {
            get => _end;
            set
            {
                _end = value;
                EndSpecified = true;
            }
        }

        /// Attribute Existence
        protected internal bool EndSpecified { get; private set; }

        /// <remarks>A reference to the translation table used if this is PeptideEvidence derived from nucleic acid sequence</remarks>
        /// Optional Attribute
        /// string
        protected internal string TranslationTableRef
        {
            get
            {
                if (_translationTable != null)
                    return _translationTable.Id;
                return _translationTableRef;
            }
            set
            {
                _translationTableRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                    TranslationTable = IdentData.FindTranslationTable(value);
            }
        }

        /// <remarks>A reference to the translation table used if this is PeptideEvidence derived from nucleic acid sequence</remarks>
        /// Optional Attribute
        /// string
        public TranslationTableObj TranslationTable
        {
            get => _translationTable;
            set
            {
                _translationTable = value;
                if (_translationTable != null)
                {
                    _translationTable.IdentData = IdentData;
                    _translationTableRef = _translationTable.Id;
                }
            }
        }

        /// <remarks>The translation frame of this sequence if this is PeptideEvidence derived from nucleic acid sequence</remarks>
        /// Optional Attribute
        /// "Allowed Frames", int: -3, -2, -1, 1, 2, 3
        public int Frame
        {
            get => _frame;
            set
            {
                _frame = value;
                FrameSpecified = true;
            }
        }

        /// Attribute Existence
        protected internal bool FrameSpecified { get; private set; }

        /// <remarks>A reference to the identified (poly)peptide sequence in the Peptide element.</remarks>
        /// Required Attribute
        /// string
        protected internal string PeptideRef
        {
            get
            {
                if (_peptide != null)
                    return _peptide.Id;
                return _peptideRef;
            }
            set
            {
                _peptideRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                    Peptide = IdentData.FindPeptide(value);
            }
        }

        /// <remarks>A reference to the identified (poly)peptide sequence in the Peptide element.</remarks>
        /// Required Attribute
        /// string
        public PeptideObj Peptide
        {
            get => _peptide;
            set
            {
                _peptide = value;
                if (_peptide != null)
                {
                    _peptide.IdentData = IdentData;
                    _peptideRef = _peptide.Id;
                }
            }
        }

        /// <remarks>A reference to the protein sequence in which the specified peptide has been linked.</remarks>
        /// Required Attribute
        /// string
        protected internal string DBSequenceRef
        {
            get
            {
                if (_dBSequence != null)
                    return _dBSequence.Id;
                return _dBSequenceRef;
            }
            set
            {
                _dBSequenceRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                    DBSequence = IdentData.FindDbSequence(value);
            }
        }

        /// <remarks>A reference to the protein sequence in which the specified peptide has been linked.</remarks>
        /// Required Attribute
        /// string
        public DbSequenceObj DBSequence
        {
            get => _dBSequence;
            set
            {
                _dBSequence = value;
                if (_dBSequence != null)
                {
                    _dBSequence.IdentData = IdentData;
                    _dBSequenceRef = _dBSequence.Id;
                }
            }
        }

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
            var o = other as PeptideEvidenceObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(PeptideEvidenceObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;
            if ((IsDecoy == other.IsDecoy) && (Start == other.Start) && (End == other.End) &&
                (Pre == other.Pre) && (Post == other.Post) && (Frame == other.Frame) && (Name == other.Name) &&
                Equals(TranslationTable, other.TranslationTable) && Equals(Peptide, other.Peptide) &&
                Equals(DBSequence, other.DBSequence) && Equals(CVParams, other.CVParams) &&
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
                hashCode = (hashCode * 397) ^ IsDecoy.GetHashCode();
                hashCode = (hashCode * 397) ^ Start;
                hashCode = (hashCode * 397) ^ End;
                hashCode = (hashCode * 397) ^ Pre.GetHashCode();
                hashCode = (hashCode * 397) ^ Post.GetHashCode();
                hashCode = (hashCode * 397) ^ Frame;
                hashCode = (hashCode * 397) ^ (TranslationTable != null ? TranslationTable.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Peptide != null ? Peptide.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DBSequence != null ? DBSequence.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}