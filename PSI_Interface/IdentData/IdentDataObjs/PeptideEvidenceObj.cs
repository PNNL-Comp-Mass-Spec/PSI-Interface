using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML PeptideEvidenceType
    /// </summary>
    /// <remarks>
    /// PeptideEvidence links a specific Peptide element to a specific position in a DBSequence.
    /// There must only be one PeptideEvidence item per Peptide-to-DBSequence-position.
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
        /// Constructor
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
        /// Create a peptide evidence with the specified values
        /// </summary>
        /// <param name="dbSeq">Valid <see cref="DbSequenceObj" /> object, not null</param>
        /// <param name="peptide">Valid <see cref="PeptideObj" /> object, not null</param>
        /// <param name="start">start position of the peptide in the sequence</param>
        /// <param name="end">end position of the peptide in the sequence</param>
        /// <param name="pre">prefix residue, "." if the beginning of the sequence</param>
        /// <param name="post">post/suffix residue, "." if the end of the sequence</param>
        /// <param name="isDecoy">true if the peptide is a decoy</param>
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
        /// Create an object using the contents of the corresponding MzIdentML object
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

        /// <summary>Set to true if the peptide is matched to a decoy sequence.</summary>
        /// <remarks>Optional Attribute, defaults to false</remarks>
        public bool IsDecoy { get; set; }

        /// <summary>
        /// Previous flanking residue. If the peptide is N-terminal, pre="-" and not pre="".
        /// If for any reason it is unknown (e.g. DeNovo), pre="?" should be used.
        /// </summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>RegEx: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"</returns>
        public string Pre { get; set; }

        /// <summary>
        /// Post flanking residue. If the peptide is C-terminal, post="-" and not post="". If for any reason it is unknown
        /// (e.g. DeNovo), post="?" should be used.
        /// </summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>RegEx: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"</returns>
        public string Post { get; set; }

        /// <summary>
        /// Start position of the peptide inside the protein sequence, where the first amino acid of the
        /// protein sequence is position 1. Must be provided unless this is a de novo search.
        /// </summary>
        /// <remarks>Optional Attribute</remarks>
        public int Start
        {
            get => _start;
            set
            {
                _start = value;
                StartSpecified = true;
            }
        }

        /// <summary>
        /// True if Start has been defined
        /// </summary>
        protected internal bool StartSpecified { get; private set; }

        /// <summary>
        /// The index position of the last amino acid of the peptide inside the protein sequence, where the first
        /// amino acid of the protein sequence is position 1. Must be provided unless this is a de novo search.
        /// </summary>
        /// <remarks>Optional Attribute</remarks>
        public int End
        {
            get => _end;
            set
            {
                _end = value;
                EndSpecified = true;
            }
        }

        /// <summary>
        /// True if End has been defined
        /// </summary>
        protected internal bool EndSpecified { get; private set; }

        /// <summary>A reference to the translation table used if this is PeptideEvidence derived from nucleic acid sequence</summary>
        /// <remarks>Optional Attribute</remarks>
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

        /// <summary>A reference to the translation table used if this is PeptideEvidence derived from nucleic acid sequence</summary>
        /// <remarks>Optional Attribute</remarks>
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

        /// <summary>The translation frame of this sequence if this is PeptideEvidence derived from nucleic acid sequence</summary>
        /// <remarks>Optional Attribute</remarks>
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

        /// <summary>
        /// True if Frame has been defined
        /// </summary>
        protected internal bool FrameSpecified { get; private set; }

        /// <summary>A reference to the identified (poly)peptide sequence in the Peptide element.</summary>
        /// <remarks>Required Attribute</remarks>
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

        /// <summary>A reference to the identified (poly)peptide sequence in the Peptide element.</summary>
        /// <remarks>Required Attribute</remarks>
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

        /// <summary>A reference to the protein sequence in which the specified peptide has been linked.</summary>
        /// <remarks>Required Attribute</remarks>
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

        /// <summary>A reference to the protein sequence in which the specified peptide has been linked.</summary>
        /// <remarks>Required Attribute</remarks>
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
            return other is PeptideEvidenceObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(PeptideEvidenceObj other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return IsDecoy == other.IsDecoy && Start == other.Start && End == other.End &&
                   Pre == other.Pre && Post == other.Post && Frame == other.Frame && Name == other.Name &&
                   Equals(TranslationTable, other.TranslationTable) && Equals(Peptide, other.Peptide) &&
                   Equals(DBSequence, other.DBSequence) && ParamsEquals(other);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ IsDecoy.GetHashCode();
                hashCode = (hashCode * 397) ^ Start;
                hashCode = (hashCode * 397) ^ End;
                hashCode = (hashCode * 397) ^ Pre.GetHashCode();
                hashCode = (hashCode * 397) ^ Post.GetHashCode();
                hashCode = (hashCode * 397) ^ Frame;
                hashCode = (hashCode * 397) ^ (TranslationTable?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Peptide?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (DBSequence?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (CVParams?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (UserParams?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}
