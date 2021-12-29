using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML SpectrumIdentificationItemType
    /// </summary>
    /// <remarks>
    /// An identification of a single (poly)peptide, resulting from querying an input spectra, along with
    /// the set of confidence values for that identification. PeptideEvidence elements should be given for all
    /// mappings of the corresponding Peptide sequence within protein sequences.
    /// </remarks>
    /// <remarks>
    /// CVParams/UserParams: Scores or attributes associated with the SpectrumIdentificationItem e.g. e-value,
    /// p-value, score.
    /// </remarks>
    public class SpectrumIdentificationItemObj : ParamGroupObj, IIdentifiableType, IEquatable<SpectrumIdentificationItemObj>
    {
        private double _calculatedMassToCharge;
        private float _calculatedPI;
        private IdentDataList<IonTypeObj> _fragmentations;
        private MassTableObj _massTable;
        private string _massTableRef;
        private PeptideObj _peptide;

        private IdentDataList<PeptideEvidenceRefObj> _peptideEvidences;
        private string _peptideRef;
        private SampleObj _sample;
        private string _sampleRef;

        /// <summary>
        /// Constructor
        /// </summary>
        public SpectrumIdentificationItemObj()
        {
            Id = null;
            Name = null;
            ChargeState = 0;
            ExperimentalMassToCharge = -1;
            _calculatedMassToCharge = -1;
            CalculatedMassToChargeSpecified = false;
            _calculatedPI = -1;
            CalculatedPISpecified = false;
            _peptideRef = null;
            Rank = -1;
            PassThreshold = false;
            _massTableRef = null;
            _sampleRef = null;

            _peptide = null;
            _massTable = null;
            _sample = null;
            PeptideEvidences = new IdentDataList<PeptideEvidenceRefObj>(1);
            Fragmentations = new IdentDataList<IonTypeObj>(1);
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="sii"></param>
        /// <param name="idata"></param>
        public SpectrumIdentificationItemObj(SpectrumIdentificationItemType sii, IdentDataObj idata)
            : base(sii, idata)
        {
            Id = sii.id;
            Name = sii.name;
            ChargeState = sii.chargeState;
            ExperimentalMassToCharge = sii.experimentalMassToCharge;
            _calculatedMassToCharge = sii.calculatedMassToCharge;
            CalculatedMassToChargeSpecified = sii.calculatedMassToChargeSpecified;
            _calculatedPI = sii.calculatedPI;
            CalculatedPISpecified = sii.calculatedPISpecified;
            PeptideRef = sii.peptide_ref;
            Rank = sii.rank;
            PassThreshold = sii.passThreshold;
            MassTableRef = sii.massTable_ref;
            SampleRef = sii.sample_ref;

            PeptideEvidences = new IdentDataList<PeptideEvidenceRefObj>(1);
            Fragmentations = new IdentDataList<IonTypeObj>(1);

            if (sii.PeptideEvidenceRef?.Count > 0)
            {
                PeptideEvidences.AddRange(sii.PeptideEvidenceRef, pe => new PeptideEvidenceRefObj(pe, IdentData));
            }
            if (sii.Fragmentation?.Count > 0)
            {
                Fragmentations.AddRange(sii.Fragmentation, f => new IonTypeObj(f, IdentData));
            }
        }

        /// <summary>min 1, max unbounded // mzIdentML 1.1</summary>
        /// <remarks>min 0, max unbounded // mzIdentML 1.2 (0 only allowed if AdditionalSearchParams contains cvParam "De novo search")</remarks>
        public IdentDataList<PeptideEvidenceRefObj> PeptideEvidences
        {
            get => _peptideEvidences;
            set
            {
                _peptideEvidences = value;
                if (_peptideEvidences != null)
                    _peptideEvidences.IdentData = IdentData;
            }
        }

        /// <summary>min 0, max 1</summary>
        public IdentDataList<IonTypeObj> Fragmentations
        {
            get => _fragmentations;
            set
            {
                _fragmentations = value;
                if (_fragmentations != null)
                    _fragmentations.IdentData = IdentData;
            }
        }

        /// <summary>The charge state of the identified peptide.</summary>
        /// <remarks>Required Attribute</remarks>
        public int ChargeState { get; set; }

        /// <summary>The mass-to-charge value measured in the experiment in Daltons / charge.</summary>
        /// <remarks>Required Attribute</remarks>
        public double ExperimentalMassToCharge { get; set; }

        /// <summary>The theoretical mass-to-charge value calculated for the peptide in Daltons / charge.</summary>
        /// <remarks>Optional Attribute</remarks>
        public double CalculatedMassToCharge
        {
            get => _calculatedMassToCharge;
            set
            {
                _calculatedMassToCharge = value;
                CalculatedMassToChargeSpecified = true;
            }
        }

        /// <summary>
        /// True if Calculated mass to charge has been defined
        /// </summary>
        protected internal bool CalculatedMassToChargeSpecified { get; private set; }

        /// <summary>
        /// The calculated isoelectric point of the (poly)peptide, with relevant modifications included.
        /// Do not supply this value if the pI cannot be calculated properly.
        /// </summary>
        /// <remarks>Optional Attribute</remarks>
        public float CalculatedPI
        {
            get => _calculatedPI;
            set
            {
                _calculatedPI = value;
                CalculatedPISpecified = true;
            }
        }

        /// <summary>
        /// True if Calculated pI has been defined
        /// </summary>
        protected internal bool CalculatedPISpecified { get; private set; }

        /// <summary>A reference to the identified (poly)peptide sequence in the Peptide element.</summary>
        /// <remarks>Optional Attribute</remarks> // mzIdentML 1.1
        /// <remarks>Required Attribute</remarks> // mzIdentML 1.2
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
        /// <remarks>Optional Attribute</remarks> // mzIdentML 1.1
        /// <remarks>Required Attribute</remarks> // mzIdentML 1.2
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

        /// <summary>
        /// For an MS/MS result set, this is the rank of the identification quality as scored by the search engine.
        /// 1 is the top rank. If multiple identifications have the same top score, they should all be assigned rank =1.
        /// For PMF data, the rank attribute may be meaningless and values of rank = 0 should be given.
        /// </summary>
        /// <remarks>Required Attribute</remarks>
        public int Rank { get; set; }

        /// <summary>
        /// Set to true if the producers of the file has deemed that the identification has passed a given threshold
        /// or been validated as correct. If no such threshold has been set, value of true should be given for all results.
        /// </summary>
        /// <remarks>Required Attribute</remarks>
        public bool PassThreshold { get; set; }

        /// <summary>
        /// A reference should be given to the MassTable used to calculate the sequenceMass only if more than one
        /// MassTable has been given.
        /// </summary>
        /// <remarks>Optional Attribute</remarks>
        protected internal string MassTableRef
        {
            get
            {
                if (_massTable != null)
                    return _massTable.Id;
                return _massTableRef;
            }
            set
            {
                _massTableRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                    MassTable = IdentData.FindMassTable(value);
            }
        }

        /// <summary>
        /// A reference should be given to the MassTable used to calculate the sequenceMass only if more than one
        /// MassTable has been given.
        /// </summary>
        /// <remarks>Optional Attribute</remarks>
        public MassTableObj MassTable
        {
            get => _massTable;
            set
            {
                _massTable = value;
                if (_massTable != null)
                {
                    _massTable.IdentData = IdentData;
                    _massTableRef = _massTable.Id;
                }
            }
        }

        /// <summary>
        /// A reference should be provided to link the SpectrumIdentificationItem to a Sample
        /// if more than one sample has been described in the AnalysisSampleCollection.
        /// </summary>
        /// <remarks>Optional Attribute</remarks>
        protected internal string SampleRef
        {
            get
            {
                if (_sample != null)
                    return _sample.Id;
                return _sampleRef;
            }
            set
            {
                _sampleRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                    Sample = IdentData.FindSample(value);
            }
        }

        /// <summary>
        /// A reference should be provided to link the SpectrumIdentificationItem to a Sample
        /// if more than one sample has been described in the AnalysisSampleCollection.
        /// </summary>
        /// <remarks>Optional Attribute</remarks>
        public SampleObj Sample
        {
            get => _sample;
            set
            {
                _sample = value;
                if (_sample != null)
                {
                    _sample.IdentData = IdentData;
                    _sampleRef = _sample.Id;
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

        /// <summary>
        /// Adds a PeptideEvidence object to the PeptideEvidence Reference List
        /// </summary>
        /// <param name="pepEv"></param>
        public void AddPeptideEvidence(PeptideEvidenceObj pepEv)
        {
            PeptideEvidences.Add(new PeptideEvidenceRefObj(pepEv));
        }

        /// <summary>
        /// Get the SpecEValue of this identification
        /// </summary>
        public double GetSpecEValue()
        {
            return CVParams.GetCvParam(CV.CV.CVID.MS_MS_GF_SpecEValue, "1").ValueAs<double>();
        }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is SpectrumIdentificationItemObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(SpectrumIdentificationItemObj other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return Name == other.Name && ChargeState == other.ChargeState &&
                   ExperimentalMassToCharge.Equals(other.ExperimentalMassToCharge) &&
                   CalculatedMassToCharge.Equals(other.CalculatedMassToCharge) &&
                   CalculatedPI.Equals(other.CalculatedPI) && Rank == other.Rank &&
                   PassThreshold == other.PassThreshold && Equals(Peptide, other.Peptide) &&
                   Equals(MassTable, other.MassTable) && Equals(Sample, other.Sample) &&
                   Equals(PeptideEvidences, other.PeptideEvidences) &&
                   Equals(Fragmentations, other.Fragmentations) && ParamsEquals(other);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ ChargeState;
                hashCode = (hashCode * 397) ^ ExperimentalMassToCharge.GetHashCode();
                hashCode = (hashCode * 397) ^ CalculatedMassToCharge.GetHashCode();
                hashCode = (hashCode * 397) ^ CalculatedPI.GetHashCode();
                hashCode = (hashCode * 397) ^ Rank;
                hashCode = (hashCode * 397) ^ PassThreshold.GetHashCode();
                hashCode = (hashCode * 397) ^ (Peptide?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (MassTable?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Sample?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (PeptideEvidences?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Fragmentations?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (CVParams?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (UserParams?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}
