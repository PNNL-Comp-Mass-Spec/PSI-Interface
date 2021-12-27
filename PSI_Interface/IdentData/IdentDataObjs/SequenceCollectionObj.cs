using System;
using System.Linq;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML SequenceCollectionType
    /// </summary>
    /// <remarks>
    /// The collection of sequences (DBSequence or Peptide) identified and their relationship between
    /// each other (PeptideEvidence) to be referenced elsewhere in the results.
    /// </remarks>
    public class SequenceCollectionObj : IdentDataInternalTypeAbstract, IEquatable<SequenceCollectionObj>
    {
        private long _dBSeqIdCounter;

        private IdentDataList<DbSequenceObj> _dBSequences;
        private long _pepEvIdCounter;
        private long _pepIdCounter;
        private IdentDataList<PeptideEvidenceObj> _peptideEvidences;
        private IdentDataList<PeptideObj> _peptides;

        /// <summary>
        /// Constructor
        /// </summary>
        public SequenceCollectionObj()
        {
            DBSequences = new IdentDataList<DbSequenceObj>(1);
            Peptides = new IdentDataList<PeptideObj>(1);
            PeptideEvidences = new IdentDataList<PeptideEvidenceObj>(1);
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="idata"></param>
        public SequenceCollectionObj(SequenceCollectionType sc, IdentDataObj idata)
            : base(idata)
        {
            DBSequences = new IdentDataList<DbSequenceObj>(1);
            Peptides = new IdentDataList<PeptideObj>(1);
            PeptideEvidences = new IdentDataList<PeptideEvidenceObj>(1);

            idata.SequenceCollection = this;

            if ((sc.DBSequence?.Count > 0))
            {
                DBSequences.AddIdMap();
                DBSequences.AddRange(sc.DBSequence, dbs => new DbSequenceObj(dbs, IdentData));
            }
            if ((sc.Peptide?.Count > 0))
            {
                Peptides.AddIdMap();
                Peptides.AddRange(sc.Peptide, p => new PeptideObj(p, IdentData));
            }
            if ((sc.PeptideEvidence?.Count > 0))
            {
                PeptideEvidences.AddIdMap();
                PeptideEvidences.AddRange(sc.PeptideEvidence, pe => new PeptideEvidenceObj(pe, IdentData));
            }
        }

        /// <summary>min 1, max unbounded (mzIdentML 1.1)</summary>
        /// <remarks>min 0, max unbounded (mzIdentML 1.2, 0 only valid if additional search params contains "de novo search" cvParam)</remarks>
        public IdentDataList<DbSequenceObj> DBSequences
        {
            get => _dBSequences;
            set
            {
                _dBSequences = value;
                if (_dBSequences != null)
                    _dBSequences.IdentData = IdentData;
            }
        }

        /// <summary>min 0, max unbounded</summary>
        public IdentDataList<PeptideObj> Peptides
        {
            get => _peptides;
            set
            {
                _peptides = value;
                if (_peptides != null)
                    _peptides.IdentData = IdentData;
            }
        }

        /// <summary>min 0, max unbounded</summary>
        public IdentDataList<PeptideEvidenceObj> PeptideEvidences
        {
            get => _peptideEvidences;
            set
            {
                _peptideEvidences = value;
                if (_peptideEvidences != null)
                    _peptideEvidences.IdentData = IdentData;
            }
        }

        internal void RebuildLists()
        {
            RebuildPeptideEvidenceList();
            RebuildPeptideList();
            RebuildDbSequenceList();
        }

        // ReSharper disable ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator

        private void RebuildPeptideEvidenceList()
        {
            _pepEvIdCounter = 0;
            _peptideEvidences.Clear();

            foreach (var sil in IdentData.DataCollection.AnalysisData.SpectrumIdentificationList)
                foreach (var sir in sil.SpectrumIdentificationResults)
                    foreach (var sii in sir.SpectrumIdentificationItems)
                        foreach (var pepEv in sii.PeptideEvidences)
                        {
                            if (_peptideEvidences.Any(item => item.Equals(pepEv.PeptideEvidence)))
                                continue;

                            pepEv.PeptideEvidence.Id = "Pep_" + _pepEvIdCounter;
                            _pepEvIdCounter++;
                            _peptideEvidences.Add(pepEv.PeptideEvidence);
                        }
        }

        private void RebuildPeptideList()
        {
            _pepIdCounter = 0;
            _peptides.Clear();

            foreach (var sil in IdentData.DataCollection.AnalysisData.SpectrumIdentificationList)
                foreach (var sir in sil.SpectrumIdentificationResults)
                    foreach (var sii in sir.SpectrumIdentificationItems)
                    {
                        if (_peptides.Any(item => item.Equals(sii.Peptide)))
                            continue;

                        sii.Peptide.Id = "Pep_" + _pepIdCounter;
                        _pepIdCounter++;
                        _peptides.Add(sii.Peptide);
                    }

            foreach (var pepEv in _peptideEvidences)
            {
                if (_peptides.Any(item => item.Equals(pepEv.Peptide)))
                    continue;

                pepEv.Peptide.Id = "Pep_" + _pepIdCounter;
                _pepIdCounter++;
                _peptides.Add(pepEv.Peptide);
            }
        }

        private void RebuildDbSequenceList()
        {
            _dBSeqIdCounter = 0;
            _dBSequences.Clear();

            foreach (var pepEv in _peptideEvidences)
            {
                if (_dBSequences.Any(item => item.Equals(pepEv.DBSequence)))
                    continue;

                pepEv.DBSequence.Id = "DBSeq_" + _dBSeqIdCounter;
                _dBSeqIdCounter++;
                _dBSequences.Add(pepEv.DBSequence);
            }
        }

        // ReSharper restore ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            var o = other as SequenceCollectionObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(SequenceCollectionObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if (Equals(DBSequences, other.DBSequences) && Equals(Peptides, other.Peptides) &&
                Equals(PeptideEvidences, other.PeptideEvidences))
                return true;
            return false;
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = DBSequences != null ? DBSequences.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Peptides != null ? Peptides.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PeptideEvidences != null ? PeptideEvidences.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}
