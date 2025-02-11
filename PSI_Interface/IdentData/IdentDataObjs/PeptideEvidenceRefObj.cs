using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML PeptideEvidenceRefType
    /// </summary>
    /// <remarks>
    /// Reference to the PeptideEvidence element identified. If a specific sequence can be assigned to multiple
    /// proteins and or positions in a protein all possible PeptideEvidence elements should be referenced here.
    /// </remarks>
    public class PeptideEvidenceRefObj : IdentDataInternalTypeAbstract, IEquatable<PeptideEvidenceRefObj>
    {
        /// <summary>
        /// IComparer class to sort lists of <see cref="PeptideEvidenceRefObj"/> by database name, then by start index of the peptide in the database file
        /// </summary>
        public static IComparer<PeptideEvidenceRefObj> SortByDbNameAndStartIndexComparer { get; } = new SortByDbAndStartIndex();

        private PeptideEvidenceObj _peptideEvidence;
        private string _peptideEvidenceRef;

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public PeptideEvidenceRefObj(PeptideEvidenceObj pepEv = null)
        {
            _peptideEvidenceRef = null;

            _peptideEvidence = pepEv;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="per"></param>
        /// <param name="idata"></param>
        public PeptideEvidenceRefObj(PeptideEvidenceRefType per, IdentDataObj idata)
            : base(idata)
        {
            PeptideEvidenceRef = per.peptideEvidence_ref;
        }
        #endregion

        #region Properties
        /// <summary>A reference to the PeptideEvidenceItem element(s).</summary>
        /// <remarks>Required Attribute</remarks>
        protected internal string PeptideEvidenceRef
        {
            get
            {
                if (_peptideEvidence != null)
                {
                    return _peptideEvidence.Id;
                }
                return _peptideEvidenceRef;
            }
            set
            {
                _peptideEvidenceRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    PeptideEvidence = IdentData.FindPeptideEvidence(value);
                }
            }
        }

        /// <summary>A reference to the PeptideEvidenceItem element(s).</summary>
        /// <remarks>Required Attribute</remarks>
        public PeptideEvidenceObj PeptideEvidence
        {
            get => _peptideEvidence;
            set
            {
                _peptideEvidence = value;
                if (_peptideEvidence != null)
                {
                    _peptideEvidence.IdentData = IdentData;
                    _peptideEvidenceRef = _peptideEvidence.Id;
                }
            }
        }
        #endregion

        #region Object Equality
        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(PeptideEvidenceRefObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return other != null && Equals(PeptideEvidence, other.PeptideEvidence);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is PeptideEvidenceRefObj o && Equals(o);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            return PeptideEvidence?.GetHashCode() ?? 0;
        }
        #endregion

        /// <summary>
        /// Show the peptide evidence ID
        /// </summary>
        public override string ToString()
        {
            return PeptideEvidenceRef;
        }

        /// <summary>
        /// IComparer class to sort lists of <see cref="PeptideEvidenceRefObj"/> by database name, then by start index of the peptide in the database file
        /// </summary>
        public class SortByDbAndStartIndex : IComparer<PeptideEvidenceRefObj>
        {
            private readonly Regex _msgfPlusPepEvFastaIndexRegex = new Regex(@"^PepEv_(?<index>\d+)_", RegexOptions.Compiled);

            /// <summary>
            /// Use a regex to extract the index MS-GF+ uses in peptide evidence IDs, if possible
            /// </summary>
            /// <param name="obj"></param>
            /// <param name="index"></param>
            /// <returns></returns>
            private bool TryGetMsgfPlusFastaIndex(PeptideEvidenceRefObj obj, out int index)
            {
                index = -1;
                var match = _msgfPlusPepEvFastaIndexRegex.Match(obj.PeptideEvidenceRef);
                if (!match.Success)
                {
                    return false;
                }

                index = int.Parse(match.Groups["index"].Value);
                return true;
            }

            /// <summary>
            /// IComparer implementation
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public int Compare(PeptideEvidenceRefObj x, PeptideEvidenceRefObj y)
            {
                if (ReferenceEquals(x, y)) return 0;
                if (y is null) return 1;
                if (x is null) return -1;

                if (x.PeptideEvidence?.DBSequence?.SearchDatabase?.Location is null ||
                    y.PeptideEvidence?.DBSequence?.SearchDatabase?.Location is null)
                {
                    // For MS-GF+, avoid using string comparison directly on PeptideEvidenceRef because the first sortable text is a number
                    if (TryGetMsgfPlusFastaIndex(x, out var xI) && TryGetMsgfPlusFastaIndex(y, out var yI))
                    {
                        return xI.CompareTo(yI);
                    }

                    return string.Compare(x.PeptideEvidenceRef, y.PeptideEvidenceRef, StringComparison.Ordinal);
                }

                var compare1 = string.Compare(x.PeptideEvidence.DBSequence.SearchDatabase.Location, y.PeptideEvidence.DBSequence.SearchDatabase.Location, StringComparison.Ordinal);
                if (x.PeptideEvidence.DBSequence.SearchDatabase.DatabaseName?.Item?.Name != null &&
                    y.PeptideEvidence.DBSequence.SearchDatabase.DatabaseName?.Item?.Name != null)
                {
                    compare1 = string.Compare(x.PeptideEvidence.DBSequence.SearchDatabase.DatabaseName.Item.Name, y.PeptideEvidence.DBSequence.SearchDatabase.DatabaseName.Item.Name, StringComparison.Ordinal);
                }

                if (compare1 != 0)
                {
                    return compare1;
                }

                // For MS-GF+, avoid using string comparison directly on PeptideEvidenceRef because the first sortable text is a number
                if (TryGetMsgfPlusFastaIndex(x, out var xIndex) && TryGetMsgfPlusFastaIndex(y, out var yIndex))
                {
                    return xIndex.CompareTo(yIndex);
                }

                return string.Compare(x.PeptideEvidenceRef, y.PeptideEvidenceRef, StringComparison.Ordinal);
            }
        }
    }
}
