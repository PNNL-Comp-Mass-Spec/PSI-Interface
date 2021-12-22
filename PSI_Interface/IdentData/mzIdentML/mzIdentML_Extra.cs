using System.Collections.Generic;
using System.Xml.Serialization;

// ReSharper disable RedundantExtendsListEntry

namespace PSI_Interface.IdentData.mzIdentML
{
    /// <summary>
    /// MzIdentML MzIdentMLType
    /// </summary>
    /// <remarks>The upper-most hierarchy level of mzIdentML with sub-containers for example describing software,
    /// protocols and search results (spectrum identifications or protein detection results).</remarks>
    public partial class MzIdentMLType : IdentifiableType
    {
        // Ignore Spelling: bool, codons, cv, cvp, daltons, de novo, denovo, immonium, isoelectric
        // Ignore Spelling: poly, pre, taxon, validator, workflow, xsd

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MzIdentMLType()
        {
            this.cvList = null;
            this.AnalysisSoftwareList = null;
            this.Provider = null;
            this.AuditCollection = null;
            this.AnalysisSampleCollection = null;
            this.SequenceCollection = null;
            this.AnalysisCollection = null;
            this.AnalysisProtocolCollection = null;
            this.DataCollection = null;
            this.BibliographicReference = null;
            this.creationDate = System.DateTime.Now;
            this.creationDateSpecified = false;
            this.version = null;
        }

        /// <summary>
        /// Set the schema location for the root object
        /// </summary>
        [XmlAttribute("schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string xsiSchemaLocation = "http://psidev.info/psi/pi/mzIdentML/1.1 http://www.psidev.info/files/mzIdentML1.1.0.xsd";

        /*
        /// <remarks>min 1, max 1</remarks>
        //public IdentDataList<CVInfo> CVList

        /// <remarks>min 0, max 1</remarks>
        //public IdentDataList<AnalysisSoftwareInfo> AnalysisSoftwareList

        /// <summary>The Provider of the mzIdentML record in terms of the contact and software.</summary>
        /// <remarks>min 0, max 1</remarks>
        //public ProviderInfo Provider

        /// <remarks>min 0, max 1</remarks>
        //public IdentDataList<AbstractContactType> AuditCollection

        /// <remarks>min 0, max 1</remarks>
        //public IdentDataList<SampleType> AnalysisSampleCollection

        /// <remarks>min 0, max 1</remarks>
        //public SequenceCollection SequenceCollection

        /// <remarks>min 1, max 1</remarks>
        //public AnalysisCollection AnalysisCollection

        /// <remarks>min 1, max 1</remarks>
        //public AnalysisProtocolCollection AnalysisProtocolCollection

        /// <remarks>min 1, max 1</remarks>
        //public DataCollection DataCollection

        /// <summary>Any bibliographic references associated with the file</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<BibliographicReferenceType> BibliographicReferences

        /// <summary>The date on which the file was produced.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public System.DateTime CreationDate

        /// Attribute Existence
        //public bool CreationDateSpecified

        /// <remarks>The version of the schema this instance document refers to, in the format x.y.z.
        /// Changes to z should not affect prevent instance documents from validating.</remarks>
        /// <remarks>Required Attribute</remarks>
        /// <returns>RegEx: "(1\.1\.\d+)"</returns>
        //public string Version*/
    }

    /// <summary>
    /// MzIdentML cvType : Container CVListType
    /// </summary>
    /// <remarks>A source controlled vocabulary from which cvParams will be obtained.</remarks>
    ///
    /// <remarks>CVListType: The list of controlled vocabularies used in the file.</remarks>
    /// <remarks>CVListType: child element cv of type cvType, min 1, max unbounded</remarks>
    public partial class cvType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public cvType()
        {
            this.fullName = null;
            this.version = null;
            this.uri = null;
            this.id = null;
        }

        /*
        /// <remarks>The full name of the CV.</remarks>
        /// <remarks>Required Attribute</remarks>
        //public string FullName

        /// <summary>The version of the CV.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string Version

        /// <summary>The URI of the source CV.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string URI

        /// <summary>The unique identifier of this cv within the document to be referenced by cvParam elements.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string Id*/
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationItemRefType
    /// </summary>
    /// <remarks>Reference(s) to the SpectrumIdentificationItem element(s) that support the given PeptideEvidence element.
    /// Using these references it is possible to indicate which spectra were actually accepted as evidence for this
    /// peptide identification in the given protein.</remarks>
    public partial class SpectrumIdentificationItemRefType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SpectrumIdentificationItemRefType()
        {
            this.spectrumIdentificationItem_ref = null;
        }

        /*
        /// <remarks>A reference to the SpectrumIdentificationItem element(s).</remarks>
        /// <remarks>Required Attribute</remarks>
        //public string SpectrumIdentificationItemRef*/
    }

    /// <summary>
    /// MzIdentML PeptideHypothesisType
    /// </summary>
    /// <remarks>Peptide evidence on which this ProteinHypothesis is based by reference to a PeptideEvidence element.</remarks>
    public partial class PeptideHypothesisType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PeptideHypothesisType()
        {
            this.SpectrumIdentificationItemRef = null;
            this.peptideEvidence_ref = null;
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public List<SpectrumIdentificationItemRefType> SpectrumIdentificationItemRef

        /// <summary>A reference to the PeptideEvidence element on which this hypothesis is based.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string PeptideEvidenceRef*/
    }

    /// <summary>
    /// MzIdentML FragmentArrayType
    /// </summary>
    /// <remarks>An array of values for a given type of measure and for a particular ion type, in parallel to the index of ions identified.</remarks>
    public partial class FragmentArrayType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public FragmentArrayType()
        {
            this.values = null;
            this.measure_ref = null;
        }

        /*
        /// <summary>The values of this particular measure, corresponding to the index defined in ion type</summary>
        /// <remarks>Required Attribute</remarks>
        //public List<float> Values

        /// <summary>A reference to the Measure defined in the FragmentationTable</summary>
        /// <remarks>Required Attribute</remarks>
        //public string MeasureRef*/
    }

    /// <summary>
    /// MzIdentML IonTypeType: list form is FragmentationType
    /// </summary>
    /// <remarks>IonType defines the index of fragmentation ions being reported, importing a CV term for the type of ion e.g. b ion.
    /// Example: if b3 b7 b8 and b10 have been identified, the index attribute will contain 3 7 8 10, and the corresponding values
    /// will be reported in parallel arrays below</remarks>
    /// <remarks>FragmentationType: The product ions identified in this result.</remarks>
    /// <remarks>FragmentationType: child element IonType, of type IonTypeType, min 1, max unbounded</remarks>
    public partial class IonTypeType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public IonTypeType()
        {
            this.FragmentArray = null;
            this.cvParam = null;
            this.index = null;
            this.charge = 0;
        }

        /*
        /// <remarks>min 0, max unbounded</remarks>
        //public List<FragmentArrayType> FragmentArray

        /// <summary>The type of ion identified.</summary>
        /// <remarks>min 1, max 1</remarks>
        //public CVParamType CVParam

        /// <summary>The index of ions identified as integers, following standard notation for a-c, x-z e.g. if b3 b5 and b6 have been identified, the index would store "3 5 6". For internal ions, the index contains pairs defining the start and end point - see specification document for examples. For immonium ions, the index is the position of the identified ion within the peptide sequence - if the peptide contains the same amino acid in multiple positions that cannot be distinguished, all positions should be given.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public List<string> Index

        /// <summary>The charge of the identified fragmentation ions.</summary>
        /// <remarks>Required Attribute</remarks>
        //public int Charge*/
    }

    /// <summary>
    /// MzIdentML CVParamType; Container types: ToleranceType
    /// </summary>
    /// <remarks>A single entry from an ontology or a controlled vocabulary.</remarks>
    /// <remarks>ToleranceType: The tolerance of the search given as a plus and minus value with units.</remarks>
    /// <remarks>ToleranceType: child element cvParam of type CVParamType, min 1, max unbounded "CV terms capturing the tolerance plus and minus values."</remarks>
    public partial class CVParamType/* : AbstractParamType*/
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public CVParamType()
        {
            this.accession = null;
            this.cvRef = null;
            this.name = null;
            this.value = null;
            this.unitCvRef = null;
            this.unitAccession = null;
            this.unitName = null;
        }

        /*
        /// <remarks>A reference to the cv element from which this term originates.</remarks>
        /// <remarks>Required Attribute</remarks>
        //public string CVRef

        /// <summary>The accession or ID number of this CV term in the source CV.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string Accession

        /// <summary>The name of the parameter.</summary>
        /// <remarks>Required Attribute</remarks>
        //public override string Name

        /// <summary>The user-entered value of the parameter.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public override string Value*/
    }

    /*/// <summary>
    /// MzIdentML AbstractParamType; Used instead of: ParamType, ParamGroup, ParamListType
    /// </summary>
    /// <remarks>Abstract entity allowing either cvParam or userParam to be referenced in other schema.</remarks>
    /// <remarks>PramGroup: A choice of either a cvParam or userParam.</remarks>
    /// <remarks>ParamType: Helper type to allow either a cvParam or a userParam to be provided for an element.</remarks>
    /// <remarks>ParamListType: Helper type to allow multiple cvParams or userParams to be given for an element.</remarks>
    public abstract partial class AbstractParamType
    {
        public AbstractParamType()
        {
            this.name = null;
            this.value = null;
            this.unitAccession = null;
            this.unitName = null;
            this.unitCvRef = null;
        }

        /// <summary>The name of the parameter.</summary>
        /// <remarks>Required Attribute</remarks>
        //public abstract string Name

        /// <summary>The user-entered value of the parameter.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public abstract string Value

        //public CV.CV.CVID UnitCvid

        /// <summary>An accession number identifying the unit within the OBO foundry Unit CV.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string UnitAccession

        /// <summary>The name of the unit.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string UnitName

        /// <summary>If a unit term is referenced, this attribute must refer to the CV 'id' attribute defined in the cvList in this file.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string UnitCvRef
    }*/

    /// <summary>
    /// MzIdentML UserParamType
    /// </summary>
    /// <remarks>A single user-defined parameter.</remarks>
    public partial class UserParamType/* : AbstractParamType*/
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public UserParamType()
        {
            this.name = null;
            this.value = null;
            this.unitAccession = null;
            this.unitName = null;
            this.unitCvRef = null;
            this.type = null;
        }

        /*
        /// <remarks>The name of the parameter.</remarks>
        /// <remarks>Required Attribute</remarks>
        //public override string Name

        /// <summary>The user-entered value of the parameter.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public override string Value

        /// <summary>The data type of the parameter, where appropriate (e.g.: xsd:float).</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string Type*/
    }

    /// <summary>
    /// MzIdentML ParamType
    /// </summary>
    /// <remarks>Helper type to allow either a cvParam or a userParam to be provided for an element.</remarks>
    public partial class ParamType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ParamType()
        {
            this.Item = null;
        }

        /*
        /// <remarks>min 1, max 1</remarks>
        //public ParamBase Item*/
    }

    /// <summary>
    /// MzIdentML ParamListType
    /// </summary>
    /// <remarks>Helper type to allow multiple cvParams or userParams to be given for an element.</remarks>
    public partial class ParamListType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ParamListType()
        {
            this.Items = null;
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public List<ParamBase> Items*/
    }

    /// <summary>
    /// MzIdentML PeptideEvidenceRefType
    /// </summary>
    /// <remarks>Reference to the PeptideEvidence element identified. If a specific sequence can be assigned to multiple
    /// proteins and or positions in a protein all possible PeptideEvidence elements should be referenced here.</remarks>
    public partial class PeptideEvidenceRefType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PeptideEvidenceRefType()
        {
            this.peptideEvidence_ref = null;
        }

        /*
        /// <remarks>A reference to the PeptideEvidenceItem element(s).</remarks>
        /// <remarks>Required Attribute</remarks>
        //public string PeptideEvidenceRef*/
    }

    /// <summary>
    /// MzIdentML AnalysisDataType
    /// </summary>
    /// <remarks>Data sets generated by the analyses, including peptide and protein lists.</remarks>
    public partial class AnalysisDataType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public AnalysisDataType()
        {
            this.SpectrumIdentificationList = null;
            this.ProteinDetectionList = null;
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public List<SpectrumIdentificationListType> SpectrumIdentificationList

        /// <remarks>min 0, max 1</remarks>
        //public ProteinDetectionListType ProteinDetectionList*/
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationListType
    /// </summary>
    /// <remarks>Represents the set of all search results from SpectrumIdentification.</remarks>
    public partial class SpectrumIdentificationListType : IdentifiableType, IParamGroup
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SpectrumIdentificationListType()
        {
            this.FragmentationTable = null;
            this.SpectrumIdentificationResult = null;
            this.cvParam = null;
            this.userParam = null;
            this.numSequencesSearched = -1;
            this.numSequencesSearchedSpecified = false;
        }

        /*
        /// <remarks>min 0, max 1</remarks>
        //public List<MeasureType> FragmentationTable

        /// <remarks>min 1, max unbounded</remarks>
        //public List<SpectrumIdentificationResultType> SpectrumIdentificationResult

        /// <summary>___ParamGroup___:Scores or output parameters associated with the SpectrumIdentificationList.</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<CVParamType> CVParams

        /// <summary>___ParamGroup___</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<UserParamType> UserParams

        /// <summary>The number of database sequences searched against. This value should be provided unless a de novo search has been performed.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public long NumSequencesSearched

        /// Attribute Existence
        //public bool NumSequencesSearchedSpecified*/
    }

    /// <summary>
    /// MzIdentML MeasureType; list form is FragmentationTableType
    /// </summary>
    /// <remarks>References to CV terms defining the measures about product ions to be reported in SpectrumIdentificationItem</remarks>
    /// <remarks>FragmentationTableType: Contains the types of measures that will be reported in generic arrays
    /// for each SpectrumIdentificationItem e.g. product ion m/z, product ion intensity, product ion m/z error</remarks>
    /// <remarks>FragmentationTableType: child element Measure of type MeasureType, min 1, max unbounded</remarks>
    public partial class MeasureType : IdentifiableType, ICVParamGroup
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MeasureType()
        {
            this.cvParam = null;
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public List<CVParamType> CVParams*/
    }

    /// <summary>
    /// MzIdentML IdentifiableType
    /// </summary>
    /// <remarks>Other classes in the model can be specified as sub-classes, inheriting from Identifiable.
    /// Identifiable gives classes a unique identifier within the scope and a name that need not be unique.</remarks>
    public abstract partial class IdentifiableType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        // ReSharper disable once PublicConstructorInAbstractClass
        public IdentifiableType()
        {
            this.id = null;
            this.name = null;
        }

        /*
        /// <remarks>An identifier is an unambiguous string that is unique within the scope
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// <remarks>Required Attribute</remarks>
        //public string Id

        /// <summary>The potentially ambiguous common identifier, such as a human-readable name for the instance.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string Name*/
    }

    /// <summary>
    /// MzIdentML BibliographicReferenceType
    /// </summary>
    /// <remarks>Represents bibliographic references.</remarks>
    public partial class BibliographicReferenceType : IdentifiableType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BibliographicReferenceType()
        {
            this.authors = null;
            this.publication = null;
            this.publisher = null;
            this.editor = null;
            this.year = -1;
            this.yearSpecified = false;
            this.volume = null;
            this.issue = null;
            this.pages = null;
            this.title = null;
            this.doi = null;
        }

        /*
        /// <remarks>The names of the authors of the reference.</remarks>
        /// <remarks>Optional Attribute</remarks>
        //public string Authors

        /// <summary>The name of the journal, book etc.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string Publication

        /// <summary>The publisher of the publication.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string Publisher

        /// <summary>The editor(s) of the reference.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string Editor

        /// <summary>The year of publication.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public int Year

        /// Attribute Existence
        //public bool YearSpecified

        /// <summary>The volume name or number.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string Volume

        /// <summary>The issue name or number.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string Issue

        /// <summary>The page numbers.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string Pages

        /// <summary>The title of the BibliographicReference.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string Title

        /// <summary>The DOI of the referenced publication.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string DOI*/
    }

    /// <summary>
    /// MzIdentML ProteinDetectionHypothesisType
    /// </summary>
    /// <remarks>A single result of the ProteinDetection analysis (i.e. a protein).</remarks>
    public partial class ProteinDetectionHypothesisType : IdentifiableType, IParamGroup
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ProteinDetectionHypothesisType()
        {
            this.PeptideHypothesis = null;
            this.cvParam = null;
            this.userParam = null;
            this.dBSequence_ref = null;
            this.passThreshold = false;
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public List<PeptideHypothesisType> PeptideHypothesis

        /// <summary>___ParamGroup___:Scores or parameters associated with this ProteinDetectionHypothesis e.g. p-value</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<CVParamType> CVParams

        /// <summary>___ParamGroup___</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<UserParamType> UserParams

        /// <summary>A reference to the corresponding DBSequence entry. This optional and redundant, because the PeptideEvidence
        /// elements referenced from here also map to the DBSequence.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string DBSequenceRef

        /// <summary>Set to true if the producers of the file has deemed that the ProteinDetectionHypothesis has passed a given
        /// threshold or been validated as correct. If no such threshold has been set, value of true should be given for all results.</summary>
        /// <remarks>Required Attribute</remarks>
        //public bool PassThreshold*/
    }

    /// <summary>
    /// MzIdentML ProteinAmbiguityGroupType
    /// </summary>
    /// <remarks>A set of logically related results from a protein detection, for example to represent conflicting assignments of peptides to proteins.</remarks>
    public partial class ProteinAmbiguityGroupType : IdentifiableType, IParamGroup
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ProteinAmbiguityGroupType()
        {
            this.ProteinDetectionHypothesis = null;
            this.cvParam = null;
            this.userParam = null;
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public List<ProteinDetectionHypothesisType> ProteinDetectionHypothesis

        /// <summary>___ParamGroup___:Scores or parameters associated with the ProteinAmbiguityGroup.</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<CVParamType> CVParams

        /// <summary>___ParamGroup___</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<UserParamType> UserParams*/
    }

    /// <summary>
    /// MzIdentML ProteinDetectionListType
    /// </summary>
    /// <remarks>The protein list resulting from a protein detection process.</remarks>
    public partial class ProteinDetectionListType : IdentifiableType, IParamGroup
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ProteinDetectionListType()
        {
            this.ProteinAmbiguityGroup = null;
            this.cvParam = null;
            this.userParam = null;
        }

        /*
        /// <remarks>min 0, max unbounded</remarks>
        //public List<ProteinAmbiguityGroupType> ProteinAmbiguityGroup

        /// <summary>___ParamGroup___:Scores or output parameters associated with the whole ProteinDetectionList</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<CVParamType> CVParams

        /// <summary>___ParamGroup___</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<UserParamType> UserParams*/
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationItemType
    /// </summary>
    /// <remarks>An identification of a single (poly)peptide, resulting from querying an input spectra, along with
    /// the set of confidence values for that identification. PeptideEvidence elements should be given for all
    /// mappings of the corresponding Peptide sequence within protein sequences.</remarks>
    public partial class SpectrumIdentificationItemType : IdentifiableType, IParamGroup
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SpectrumIdentificationItemType()
        {
            this.PeptideEvidenceRef = null;
            this.Fragmentation = null;
            this.cvParam = null;
            this.userParam = null;
            this.chargeState = 0;
            this.experimentalMassToCharge = -1;
            this.calculatedMassToCharge = -1;
            this.calculatedMassToChargeSpecified = false;
            this.calculatedPI = 0;
            this.calculatedPISpecified = false;
            this.peptide_ref = null;
            this.rank = -1;
            this.passThreshold = false;
            this.massTable_ref = null;
            this.sample_ref = null;
        }

        /*
        /// <remarks>An identifier is an unambiguous string that is unique within the scope
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// <remarks>Required Attribute</remarks>
        //public string Id

        /// <summary>The potentially ambiguous common identifier, such as a human-readable name for the instance.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string Name

        /// <remarks>min 1, max unbounded</remarks>
        //public List<PeptideEvidenceRefType> PeptideEvidenceRef

        /// <remarks>min 0, max 1</remarks>
        //public List<IonTypeType> Fragmentation

        /// <summary>___ParamGroup___:Scores or attributes associated with the SpectrumIdentificationItem e.g. e-value, p-value, score.</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<CVParamType> CVParams

        /// <summary>___ParamGroup___</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<UserParamType> UserParams

        /// <summary>The charge state of the identified peptide.</summary>
        /// <remarks>Required Attribute</remarks>
        //public int ChargeState

        /// <summary>The mass-to-charge value measured in the experiment in Daltons / charge.</summary>
        /// <remarks>Required Attribute</remarks>
        //public double ExperimentalMassToCharge

        /// <summary>The theoretical mass-to-charge value calculated for the peptide in Daltons / charge.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public double CalculatedMassToCharge

        /// Attribute Existence
        //public bool CalculatedMassToChargeSpecified

        /// <remarks>The calculated isoelectric point of the (poly)peptide, with relevant modifications included.
        /// Do not supply this value if the PI cannot be calculated properly.</remarks>
        /// <remarks>Optional Attribute</remarks>
        //public float CalculatedPI

        /// Attribute Existence
        //public bool CalculatedPISpecified

        /// <summary>A reference to the identified (poly)peptide sequence in the Peptide element.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string PeptideRef

        /// <summary>For an MS/MS result set, this is the rank of the identification quality as scored by the search engine.
        /// 1 is the top rank. If multiple identifications have the same top score, they should all be assigned rank =1.
        /// For PMF data, the rank attribute may be meaningless and values of rank = 0 should be given.</summary>
        /// <remarks>Required Attribute</remarks>
        //public int Rank

        /// <summary>Set to true if the producers of the file has deemed that the identification has passed a given threshold
        /// or been validated as correct. If no such threshold has been set, value of true should be given for all results.</summary>
        /// <remarks>Required Attribute</remarks>
        //public bool PassThreshold

        /// <summary>A reference should be given to the MassTable used to calculate the sequenceMass only if more than one MassTable has been given.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string MassTableRef

        /// <summary>A reference should be provided to link the SpectrumIdentificationItem to a Sample
        /// if more than one sample has been described in the AnalysisSampleCollection.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string SampleRef*/
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationResultType
    /// </summary>
    /// <remarks>All identifications made from searching one spectrum. For PMF data, all peptide identifications
    /// will be listed underneath as SpectrumIdentificationItems. For MS/MS data, there will be ranked
    /// SpectrumIdentificationItems corresponding to possible different peptide IDs.</remarks>
    public partial class SpectrumIdentificationResultType : IdentifiableType, IParamGroup
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SpectrumIdentificationResultType()
        {
            this.SpectrumIdentificationItem = null;
            this.cvParam = null;
            this.userParam = null;
            this.spectrumID = null;
            this.spectraData_ref = null;
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public List<SpectrumIdentificationItemType> SpectrumIdentificationItems

        /// <summary>___ParamGroup___: Scores or parameters associated with the SpectrumIdentificationResult
        /// (i.e the set of SpectrumIdentificationItems derived from one spectrum) e.g. the number of peptide
        /// sequences within the parent tolerance for this spectrum.</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<CVParamType> CVParams

        /// <summary>___ParamGroup___</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<UserParamType> UserParams

        /// <summary>The locally unique id for the spectrum in the spectra data set specified by SpectraData_ref.
        /// External guidelines are provided on the use of consistent identifiers for spectra in different external formats.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string SpectrumID

        /// <summary>A reference to a spectra data set (e.g. a spectra file).</summary>
        /// <remarks>Required Attribute</remarks>
        //public string SpectraDataRef*/
    }

    /// <summary>
    /// MzIdentML ExternalDataType
    /// </summary>
    /// <remarks>Data external to the XML instance document. The location of the data file is given in the location attribute.</remarks>
    public partial class ExternalDataType : IdentifiableType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ExternalDataType()
        {
            this.ExternalFormatDocumentation = null;
            this.FileFormat = null;
            this.location = null;
        }

        /*
        /// <remarks>A URI to access documentation and tools to interpret the external format of the ExternalData instance.
        /// For example, XML Schema or static libraries (APIs) to access binary formats.</remarks>
        /// <remarks>min 0, max 1</remarks>
        //public string ExternalFormatDocumentation

        /// <remarks>min 0, max 1</remarks>
        //public FileFormatType FileFormat

        /// <summary>The location of the data file.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string Location*/
    }

    /// <summary>
    /// MzIdentML FileFormatType
    /// </summary>
    /// <remarks>The format of the ExternalData file, for example "tiff" for image files.</remarks>
    public partial class FileFormatType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public FileFormatType()
        {
            this.cvParam = null;
        }

        /*
        /// <remarks>cvParam capturing file formats</remarks>
        /// <remarks>Optional Attribute</remarks>
        /// <remarks>min 1, max 1</remarks>
        //public CVParamType CVParam*/
    }

    /// <summary>
    /// MzIdentML SpectraDataType
    /// </summary>
    /// <remarks>A data set containing spectra data (consisting of one or more spectra).</remarks>
    public partial class SpectraDataType : ExternalDataType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SpectraDataType()
        {
            this.SpectrumIDFormat = null;
        }

        /*
        /// <remarks>A URI to access documentation and tools to interpret the external format of the ExternalData instance.
        /// For example, XML Schema or static libraries (APIs) to access binary formats.</remarks>
        /// <remarks>min 0, max 1</remarks>
        //public string ExternalFormatDocumentation

        /// <remarks>min 0, max 1</remarks>
        //public FileFormatType FileFormat

        /// <summary>The location of the data file.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string Location

        /// <remarks>min 1, max 1</remarks>
        //public SpectrumIDFormatType SpectrumIDFormat*/
    }

    /// <summary>
    /// MzIdentML SpectrumIDFormatType
    /// </summary>
    /// <remarks>The format of the spectrum identifier within the source file</remarks>
    public partial class SpectrumIDFormatType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SpectrumIDFormatType()
        {
            this.cvParam = null;
        }

        /*
        /// <remarks>CV term capturing the type of identifier used.</remarks>
        /// <remarks>min 1, max 1</remarks>
        //public CVParamType CVParams*/
    }

    /// <summary>
    /// MzIdentML SourceFileType
    /// </summary>
    /// <remarks>A file from which this mzIdentML instance was created.</remarks>
    public partial class SourceFileType : ExternalDataType, IParamGroup
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SourceFileType()
        {
            this.cvParam = null;
            this.userParam = null;
        }

        /*
        /// <remarks>A URI to access documentation and tools to interpret the external format of the ExternalData instance.
        /// For example, XML Schema or static libraries (APIs) to access binary formats.</remarks>
        /// <remarks>min 0, max 1</remarks>
        //public string ExternalFormatDocumentation

        /// <remarks>min 0, max 1</remarks>
        //public FileFormatType FileFormat

        /// <summary>The location of the data file.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string Location

        /// <summary>___ParamGroup___:Any additional parameters description the source file.</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<CVParamType> CVParams

        /// <summary>___ParamGroup___</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<UserParamType> UserParams*/
    }

    /// <summary>
    /// MzIdentML SearchDatabaseType
    /// </summary>
    /// <remarks>A database for searching mass spectra. Examples include a set of amino acid sequence entries, or annotated spectra libraries.</remarks>
    public partial class SearchDatabaseType : ExternalDataType, ICVParamGroup
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SearchDatabaseType()
        {
            this.DatabaseName = null;
            this.cvParam = null;
            this.version = null;
            this.releaseDate = System.DateTime.Now;
            this.releaseDateSpecified = false;
            this.numDatabaseSequences = -1;
            this.numDatabaseSequencesSpecified = false;
            this.numResidues = -1;
            this.numResiduesSpecified = false;
        }

        /*
        /// <remarks>A URI to access documentation and tools to interpret the external format of the ExternalData instance.
        /// For example, XML Schema or static libraries (APIs) to access binary formats.</remarks>
        /// <remarks>min 0, max 1</remarks>
        //public string ExternalFormatDocumentation

        /// <remarks>min 0, max 1</remarks>
        //public FileFormatType FileFormat

        /// <summary>The location of the data file.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string Location

        /// <summary>The database name may be given as a cvParam if it maps exactly to one of the release databases listed in the CV, otherwise a userParam should be used.</summary>
        /// <remarks>min 1, max 1</remarks>
        //public ParamType DatabaseName

        /// <remarks>min 0, max unbounded</remarks>
        //public List<CVParamType> CVParams

        /// <summary>The version of the database.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string Version

        /// <summary>The date and time the database was released to the public; omit this attribute when the date and time are unknown or not applicable (e.g. custom databases).</summary>
        /// <remarks>Optional Attribute</remarks>
        //public System.DateTime ReleaseDate

        /// Attribute Existence
        //public bool ReleaseDateSpecified

        /// <summary>The total number of sequences in the database.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public long NumDatabaseSequences

        /// Attribute Existence
        //public bool NumDatabaseSequencesSpecified

        /// <summary>The number of residues in the database.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public long NumResidues

        /// Attribute Existence
        //public bool NumResiduesSpecified*/
    }

    /// <summary>
    /// MzIdentML ProteinDetectionProtocolType
    /// </summary>
    /// <remarks>The parameters and settings of a ProteinDetection process.</remarks>
    public partial class ProteinDetectionProtocolType : IdentifiableType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ProteinDetectionProtocolType()
        {
            this.AnalysisParams = null;
            this.Threshold = null;
            this.analysisSoftware_ref = null;
        }

        /*
        /// <remarks>The parameters and settings for the protein detection given as CV terms.</remarks>
        /// <remarks>min 0, max 1</remarks>
        //public ParamListType AnalysisParams

        /// <remarks>The threshold(s) applied to determine that a result is significant.
        /// If multiple terms are used it is assumed that all conditions are satisfied by the passing results.</remarks>
        /// <remarks>min 1, max 1</remarks>
        //public ParamListType Threshold

        /// <summary>The protein detection software used, given as a reference to the SoftwareCollection section.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string AnalysisSoftwareRef*/
    }

    /// <summary>
    /// MzIdentML TranslationTableType
    /// </summary>
    /// <remarks>The table used to translate codons into nucleic acids e.g. by reference to the NCBI translation table.</remarks>
    public partial class TranslationTableType : IdentifiableType, ICVParamGroup
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public TranslationTableType()
        {
            this.cvParam = null;
        }

        /*
        /// <summary>The details specifying this translation table are captured as cvParams, e.g. translation table, translation
        /// start codons and translation table description (see specification document and mapping file)</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<CVParamType> CVParams*/
    }

    /// <summary>
    /// MzIdentML MassTableType
    /// </summary>
    /// <remarks>The masses of residues used in the search.</remarks>
    public partial class MassTableType : IdentifiableType, IParamGroup
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MassTableType()
        {
            this.Residue = null;
            this.AmbiguousResidue = null;
            this.cvParam = null;
            this.userParam = null;
            this.msLevel = null;
        }

        /*
        /// <remarks>The specification of a single residue within the mass table.</remarks>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<ResidueType> Residue

        /// <remarks>min 0, max unbounded</remarks>
        //public List<AmbiguousResidueType> AmbiguousResidue

        /// <summary>___ParamGroup___: Additional parameters or descriptors for the MassTable.</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<CVParamType> CVParams

        /// <summary>___ParamGroup___</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<UserParamType> UserParams

        /// <summary>The MS spectrum that the MassTable refers to e.g. "1" for MS1 "2" for MS2 or "1 2" for MS1 or MS2.</summary>
        /// <remarks>Required Attribute</remarks>
        /// integer(s), space separated
        //public List<string> MsLevel*/
    }

    /// <summary>
    /// MzIdentML ResidueType
    /// </summary>
    public partial class ResidueType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ResidueType()
        {
            this.code = null;
            this.mass = -1;
        }

        /*
        /// <summary>The single letter code for the residue.</summary>
        /// <remarks>Required Attribute</remarks>
        /// <returns>RegEx: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}"</returns>
        //public string Code

        /// <summary>The residue mass in Daltons (not including any fixed modifications).</summary>
        /// <remarks>Required Attribute</remarks>
        //public float Mass*/
    }

    /// <summary>
    /// MzIdentML AmbiguousResidueType
    /// </summary>
    /// <remarks>Ambiguous residues e.g. X can be specified by the Code attribute and a set of parameters
    /// for example giving the different masses that will be used in the search.</remarks>
    public partial class AmbiguousResidueType : IParamGroup
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public AmbiguousResidueType()
        {
            this.cvParam = null;
            this.userParam = null;
            this.code = null;
        }

        /*
        /// <summary>___ParamGroup___: Parameters for capturing e.g. "alternate single letter codes"</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<CVParamType> CVParams

        /// <summary>___ParamGroup___</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<UserParamType> UserParams

        /// <summary>The single letter code of the ambiguous residue e.g. X.</summary>
        /// <remarks>Required Attribute</remarks>
        /// <returns>RegEx: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}"</returns>
        //public string Code*/
    }

    /// <summary>
    /// MzIdentML EnzymeType
    /// </summary>
    /// <remarks>The details of an individual cleavage enzyme should be provided by giving a regular expression
    /// or a CV term if a "standard" enzyme cleavage has been performed.</remarks>
    public partial class EnzymeType : IdentifiableType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public EnzymeType()
        {
            this.SiteRegexp = null;
            this.EnzymeName = null;
            this.nTermGain = null;
            this.cTermGain = null;
            this.semiSpecific = false;
            this.semiSpecificSpecified = false;
            this.missedCleavages = -1;
            this.missedCleavagesSpecified = false;
            this.minDistance = -1;
            this.minDistanceSpecified = false;
        }

        /*
        /// <remarks>Regular expression for specifying the enzyme cleavage site.</remarks>
        /// <remarks>min 0, max 1</remarks>
        //public string SiteRegexp

        /// <summary>The name of the enzyme from a CV.</summary>
        /// <remarks>min 0, max 1</remarks>
        //public ParamListType EnzymeName

        /// <summary>Element formula gained at NTerm.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>RegEx: "[A-Za-z0-9 ]+"</returns>
        //public string NTermGain

        /// <summary>Element formula gained at CTerm.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>RegEx: "[A-Za-z0-9 ]+"</returns>
        //public string CTermGain

        /// <summary>Set to true if the enzyme cleaves semi-specifically (i.e. one terminus must cleave
        /// according to the rules, the other can cleave at any residue), false if the enzyme cleavage
        /// is assumed to be specific to both termini (accepting for any missed cleavages).</summary>
        /// <remarks>Optional Attribute</remarks>
        //public bool SemiSpecific

        /// Attribute Existence
        //public bool SemiSpecificSpecified

        /// <summary>The number of missed cleavage sites allowed by the search. The attribute must be provided if an enzyme has been used.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public int MissedCleavages

        /// Attribute Existence
        //public bool MissedCleavagesSpecified

        /// <summary>Minimal distance for another cleavage (minimum: 1).</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>integer, greater than or equal to 1</returns>
        //public int MinDistance

        /// Attribute Existence
        //public bool MinDistanceSpecified*/
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationProtocolType
    /// </summary>
    /// <remarks>The parameters and settings of a SpectrumIdentification analysis.</remarks>
    public partial class SpectrumIdentificationProtocolType : IdentifiableType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SpectrumIdentificationProtocolType()
        {
            this.SearchType = null;
            this.AdditionalSearchParams = null;
            this.ModificationParams = null;
            this.Enzymes = null;
            this.MassTable = null;
            this.FragmentTolerance = null;
            this.ParentTolerance = null;
            this.Threshold = null;
            this.DatabaseFilters = null;
            this.DatabaseTranslation = null;
            this.analysisSoftware_ref = null;
        }

        /*
        /// <remarks>The type of search performed e.g. PMF, Tag searches, MS-MS</remarks>
        /// <remarks>min 1, max 1</remarks>
        //public ParamType SearchType

        /// <summary>The search parameters other than the modifications searched.</summary>
        /// <remarks>min 0, max 1</remarks>
        //public ParamListType AdditionalSearchParams

        /// <remarks>min 0, max 1 : Original ModificationParamsType</remarks>
        //public List<SearchModificationType> ModificationParams

        /// <remarks>min 0, max 1</remarks>
        //public EnzymesType Enzymes

        /// <remarks>min 0, max unbounded</remarks>
        //public List<MassTableType> MassTable

        /// <remarks>min 0, max 1 : Original ToleranceType</remarks>
        //public List<CVParamType> FragmentTolerance

        /// <remarks>min 0, max 1 : Original ToleranceType</remarks>
        //public List<CVParamType> ParentTolerance

        /// <summary>The threshold(s) applied to determine that a result is significant. If multiple terms are used it is assumed that all conditions are satisfied by the passing results.</summary>
        /// <remarks>min 1, max 1</remarks>
        //public ParamListType Threshold

        /// <remarks>min 0, max 1 : Original DatabaseFiltersType</remarks>
        //public List<FilterInfo> DatabaseFilters

        /// <remarks>min 0, max 1</remarks>
        //public DatabaseTranslationType DatabaseTranslation

        /// <summary>The search algorithm used, given as a reference to the SoftwareCollection section.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string AnalysisSoftwareRef*/
    }

    /// <summary>
    /// MzIdentML SpecificityRulesType
    /// </summary>
    /// <remarks>The specificity rules of the searched modification including for example
    /// the probability of a modification's presence or peptide or protein termini. Standard
    /// fixed or variable status should be provided by the attribute fixedMod.</remarks>
    public partial class SpecificityRulesType : ICVParamGroup
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SpecificityRulesType()
        {
            this.cvParam = null;
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public List<CVParamType> CVParams*/
    }

    /// <summary>
    /// MzIdentML SearchModificationType: Container ModificationParamsType
    /// </summary>
    /// <remarks>Specification of a search modification as parameter for a spectra search. Contains the name of the
    /// modification, the mass, the specificity and whether it is a static modification.</remarks>
    /// <remarks>ModificationParamsType: The specification of static/variable modifications (e.g. Oxidation of Methionine) that are to be considered in the spectra search.</remarks>
    /// <remarks>ModificationParamsType: child element SearchModification, of type SearchModificationType, min 1, max unbounded</remarks>
    public partial class SearchModificationType : ICVParamGroup
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SearchModificationType()
        {
            this.SpecificityRules = null;
            this.cvParam = null;
            this.fixedMod = false;
            this.massDelta = 0;
            this.residues = null;
        }

        /*
        /// <remarks>min 0, max unbounded</remarks>
        //public List<SpecificityRulesType> SpecificityRules

        /// <summary>The modification is uniquely identified by references to external CVs such as UNIMOD, see
        /// specification document and mapping file for more details.</summary>
        /// <remarks>min 1, max unbounded</remarks>
        //public List<CVParamType> CVParams

        /// <summary>True, if the modification is static (i.e. occurs always).</summary>
        /// <remarks>Required Attribute</remarks>
        //public bool FixedMod

        /// <summary>The mass delta of the searched modification in Daltons.</summary>
        /// <remarks>Required Attribute</remarks>
        //public float MassDelta

        /// <summary>The residue(s) searched with the specified modification. For N or C terminal modifications that can occur
        /// on any residue, the . character should be used to specify any, otherwise the list of amino acids should be provided.</summary>
        /// <remarks>Required Attribute</remarks>
        /// <returns>RegEx: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}|."</returns>
        //public string Residues*/
    }

    /// <summary>
    /// MzIdentML EnzymesType
    /// </summary>
    /// <remarks>The list of enzymes used in experiment</remarks>
    public partial class EnzymesType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public EnzymesType()
        {
            this.Enzyme = null;
            this.independent = false;
            this.independentSpecified = false;
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public List<EnzymeType> Enzyme

        /// <summary>If there are multiple enzymes specified, this attribute is set to true if cleavage with different enzymes is performed independently.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public bool Independent

        /// Attribute Existence
        //public bool IndependentSpecified*/
    }

    /// <summary>
    /// MzIdentML FilterType : Containers DatabaseFiltersType
    /// </summary>
    /// <remarks>Filters applied to the search database. The filter must include at least one of Include and Exclude.
    /// If both are used, it is assumed that inclusion is performed first.</remarks>
    /// <remarks>DatabaseFiltersType: The specification of filters applied to the database searched.</remarks>
    /// <remarks>DatabaseFiltersType: child element Filter, of type FilterType, min 1, max unbounded</remarks>
    public partial class FilterType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public FilterType()
        {
            this.FilterType1 = null;
            this.Include = null;
            this.Exclude = null;
        }

        /*
        /// <remarks>The type of filter e.g. database taxonomy filter, pI filter, MW filter</remarks>
        /// <remarks>min 1, max 1</remarks>
        //public ParamType FilterType

        /// <summary>All sequences fulfilling the specified criteria are included.</summary>
        /// <remarks>min 0, max 1</remarks>
        //public ParamListType Include

        /// <summary>All sequences fulfilling the specified criteria are excluded.</summary>
        /// <remarks>min 0, max 1</remarks>
        //public ParamListType Exclude*/
    }

    /// <summary>
    /// MzIdentML DatabaseTranslationType
    /// </summary>
    /// <remarks>A specification of how a nucleic acid sequence database was translated for searching.</remarks>
    public partial class DatabaseTranslationType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public DatabaseTranslationType()
        {
            this.TranslationTable = null;
            this.frames = null;
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public List<TranslationTableType> TranslationTable

        /// <summary>The frames in which the nucleic acid sequence has been translated as a space separated List</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>List of allowed frames: -3, -2, -1, 1, 2, 3</returns>
        //public List<int> Frames*/
    }

    /// <summary>
    /// MzIdentML ProtocolApplicationType
    /// </summary>
    /// <remarks>The use of a protocol with the requisite Parameters and ParameterValues.
    /// ProtocolApplications can take Material or Data (or both) as input and produce Material or Data (or both) as output.</remarks>
    public abstract partial class ProtocolApplicationType : IdentifiableType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        // ReSharper disable once PublicConstructorInAbstractClass
        public ProtocolApplicationType()
        {
            this.activityDate = System.DateTime.Now;
            this.activityDateSpecified = false;
        }

        /*
        /// <remarks>When the protocol was applied.</remarks>
        /// <remarks>Optional Attribute</remarks>
        //public System.DateTime ActivityDate

        /// Attribute Existence
        //public bool ActivityDateSpecified*/
    }

    /// <summary>
    /// MzIdentML ProteinDetectionType
    /// </summary>
    /// <remarks>An Analysis which assembles a set of peptides (e.g. from a spectra search analysis) to proteins.</remarks>
    public partial class ProteinDetectionType : ProtocolApplicationType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ProteinDetectionType()
        {
            this.InputSpectrumIdentifications = null;
            this.proteinDetectionList_ref = null;
            this.proteinDetectionProtocol_ref = null;
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public List<InputSpectrumIdentificationsType> InputSpectrumIdentifications

        /// <summary>A reference to the ProteinDetectionList in the DataCollection section.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string ProteinDetectionListRef

        /// <summary>A reference to the detection protocol used for this ProteinDetection.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string ProteinDetectionProtocolRef*/
    }

    /// <summary>
    /// MzIdentML InputSpectrumIdentificationsType
    /// </summary>
    /// <remarks>The lists of spectrum identifications that are input to the protein detection process.</remarks>
    public partial class InputSpectrumIdentificationsType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public InputSpectrumIdentificationsType()
        {
            this.spectrumIdentificationList_ref = null;
        }

        /*
        /// <remarks>A reference to the list of spectrum identifications that were input to the process.</remarks>
        /// <remarks>Required Attribute</remarks>
        //public string SpectrumIdentificationListRef*/
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationType
    /// </summary>
    /// <remarks>An Analysis which tries to identify peptides in input spectra, referencing the database searched,
    /// the input spectra, the output results and the protocol that is run.</remarks>
    public partial class SpectrumIdentificationType : ProtocolApplicationType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SpectrumIdentificationType()
        {
            this.InputSpectra = null;
            this.SearchDatabaseRef = null;
            this.spectrumIdentificationProtocol_ref = null;
            this.spectrumIdentificationList_ref = null;
        }

        /*
        /// <remarks>One of the spectra data sets used.</remarks>
        /// <remarks>min 1, max unbounded</remarks>
        //public List<InputSpectraType> InputSpectra

        /// <remarks>min 1, max unbounded</remarks>
        //public List<SearchDatabaseRefType> SearchDatabaseRef

        /// <summary>A reference to the search protocol used for this SpectrumIdentification.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string SpectrumIdentificationProtocolRef

        /// <summary>A reference to the SpectrumIdentificationList produced by this analysis in the DataCollection section.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string SpectrumIdentificationListRef*/
    }

    /// <summary>
    /// MzIdentML InputSpectraType
    /// </summary>
    /// <remarks>The attribute referencing an identifier within the SpectraData section.</remarks>
    public partial class InputSpectraType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public InputSpectraType()
        {
            this.spectraData_ref = null;
        }

        /*
        /// <remarks>A reference to the SpectraData element which locates the input spectra to an external file.</remarks>
        /// <remarks>Optional Attribute</remarks>
        //public string SpectraDataRef*/
    }

    /// <summary>
    /// MzIdentML SearchDatabaseRefType
    /// </summary>
    /// <remarks>One of the search databases used.</remarks>
    public partial class SearchDatabaseRefType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SearchDatabaseRefType()
        {
            this.searchDatabase_ref = null;
        }

        /*
        /// <remarks>A reference to the database searched.</remarks>
        /// <remarks>Optional Attribute</remarks>
        //public string SearchDatabaseRef*/
    }

    /// <summary>
    /// MzIdentML PeptideEvidenceType
    /// </summary>
    /// <remarks>PeptideEvidence links a specific Peptide element to a specific position in a DBSequence.
    /// There must only be one PeptideEvidence item per Peptide-to-DBSequence-position.</remarks>
    public partial class PeptideEvidenceType : IdentifiableType, IParamGroup
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PeptideEvidenceType()
        {
            this.cvParam = null;
            this.userParam = null;
            this.isDecoy = false;
            this.pre = null;
            this.post = null;
            this.start = 0;
            this.startSpecified = false;
            this.end = 0;
            this.endSpecified = false;
            this.translationTable_ref = null;
            this.frame = 0;
            this.frameSpecified = false;
            this.peptide_ref = null;
            this.dBSequence_ref = null;
        }

        /*
        //public PeptideEvidenceType()

        /// <summary>___ParamGroup___: Additional parameters or descriptors for the PeptideEvidence.</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<CVParamType> CVParams

        /// <summary>___ParamGroup___</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<UserParamType> UserParams

        /// <summary>Set to true if the peptide is matched to a decoy sequence.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public bool IsDecoy

        /// <remarks>Previous flanking residue. If the peptide is N-terminal, pre="-" and not pre="".
        /// If for any reason it is unknown (e.g. denovo), pre="?" should be used.</remarks>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>RegEx: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"
        //public string Pre

        /// <summary>Post flanking residue. If the peptide is C-terminal, post="-" and not post="". If for any reason it is unknown (e.g. denovo), post="?" should be used.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>RegEx: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"</returns>
        //public string Post

        /// <remarks>Start position of the peptide inside the protein sequence, where the first amino acid of the
        /// protein sequence is position 1. Must be provided unless this is a de novo search.</remarks>
        /// <remarks>Optional Attribute</remarks>
        //public int Start

        /// Attribute Existence
        //public bool StartSpecified

        /// <remarks>The index position of the last amino acid of the peptide inside the protein sequence, where the first
        /// amino acid of the protein sequence is position 1. Must be provided unless this is a de novo search.</remarks>
        /// <remarks>Optional Attribute</remarks>
        //public int End

        /// Attribute Existence
        //public bool EndSpecified

        /// <summary>A reference to the translation table used if this is PeptideEvidence derived from nucleic acid sequence</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string TranslationTable_ref

        /// <summary>The translation frame of this sequence if this is PeptideEvidence derived from nucleic acid sequence</summary>
        /// <remarks>Optional Attribute</remarks>
        /// "Allowed Frames", int: -3, -2, -1, 1, 2, 3
        //public int Frame

        /// Attribute Existence
        //public bool FrameSpecified

        /// <summary>A reference to the identified (poly)peptide sequence in the Peptide element.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string PeptideRef

        /// <summary>A reference to the protein sequence in which the specified peptide has been linked.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string DBSequenceRef*/
    }

    /// <summary>
    /// MzIdentML PeptideType
    /// </summary>
    /// <remarks>One (poly)peptide (a sequence with modifications). The combination of Peptide sequence and modifications must be unique in the file.</remarks>
    public partial class PeptideType : IdentifiableType, IParamGroup
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PeptideType()
        {
            this.PeptideSequence = null;
            this.Modification = null;
            this.SubstitutionModification = null;
            this.cvParam = null;
            this.userParam = null;
        }

        /*
        /// <remarks>The amino acid sequence of the (poly)peptide. If a substitution modification has been found, the original sequence should be reported.</remarks>
        /// <remarks>min 1, max 1</remarks>
        //public string PeptideSequence

        /// <remarks>min 0, max unbounded</remarks>
        //public List<ModificationType> Modification

        /// <remarks>min 0, max unbounded</remarks>
        //public List<SubstitutionModificationType> SubstitutionModification

        /// <summary>___ParamGroup___: Additional descriptors of this peptide sequence</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<CVParamType> CVParams

        /// <summary>___ParamGroup___</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<UserParamType> UserParams*/
    }

    /// <summary>
    /// MzIdentML ModificationType
    /// </summary>
    /// <remarks>A molecule modification specification. If n modifications have been found on a peptide, there should
    /// be n instances of Modification. If multiple modifications are provided as cvParams, it is assumed that the
    /// modification is ambiguous i.e. one modification or another. A cvParam must be provided with the identification
    /// of the modification sourced from a suitable CV e.g. UNIMOD. If the modification is not present in the CV (and
    /// this will be checked by the semantic validator within a given tolerance window), there is a unknown
    /// modification CV term that must be used instead. A neutral loss should be defined as an additional CVParam
    /// within Modification. If more complex information should be given about neutral losses (such as presence/absence
    /// on particular product ions), this can additionally be encoded within the FragmentationArray.</remarks>
    public partial class ModificationType : ICVParamGroup
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ModificationType()
        {
            this.cvParam = null;
            this.location = -1;
            this.locationSpecified = false;
            this.residues = null;
            this.avgMassDelta = 0;
            this.avgMassDeltaSpecified = false;
            this.monoisotopicMassDelta = 0;
            this.monoisotopicMassDeltaSpecified = false;
        }

        /*
        /// <remarks>CV terms capturing the modification, sourced from an appropriate controlled vocabulary.</remarks>
        /// <remarks>min 1, max unbounded</remarks>
        //public List<CVParamType> CVParams

        /// <remarks>Location of the modification within the peptide - position in peptide sequence, counted from
        /// the N-terminus residue, starting at position 1. Specific modifications to the N-terminus should be
        /// given the location 0. Modification to the C-terminus should be given as peptide length + 1. If the
        /// modification location is unknown e.g. for PMF data, this attribute should be omitted.</remarks>
        /// <remarks>Optional Attribute</remarks>
        //public int Location

        /// Attribute Existence
        //public bool LocationSpecified

        /// <remarks>Specification of the residue (amino acid) on which the modification occurs. If multiple values
        /// are given, it is assumed that the exact residue modified is unknown i.e. the modification is to ONE of
        /// the residues listed. Multiple residues would usually only be specified for PMF data.</remarks>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>RegEx: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}"</returns>
        //public List<string> Residues

        /// <summary>Atomic mass delta considering the natural distribution of isotopes in Daltons.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public double AvgMassDelta

        /// Attribute Existence
        //public bool AvgMassDeltaSpecified

        /// <summary>Atomic mass delta when assuming only the most common isotope of elements in Daltons.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public double MonoisotopicMassDelta

        /// Attribute Existence
        //public bool MonoisotopicMassDeltaSpecified*/
    }

    /// <summary>
    /// MzIdentML SubstitutionModificationType
    /// </summary>
    /// <remarks>A modification where one residue is substituted by another (amino acid change).</remarks>
    public partial class SubstitutionModificationType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SubstitutionModificationType()
        {
            this.originalResidue = null;
            this.replacementResidue = null;
            this.location = -1;
            this.locationSpecified = false;
            this.avgMassDelta = 0;
            this.avgMassDeltaSpecified = false;
            this.monoisotopicMassDelta = 0;
            this.monoisotopicMassDeltaSpecified = false;
        }

        /*
        /// <remarks>The original residue before replacement.</remarks>
        /// <remarks>Required Attribute</remarks>
        /// <returns>RegEx: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"</returns>
        //public string OriginalResidue

        /// <summary>The residue that replaced the originalResidue.</summary>
        /// <remarks>Required Attribute</remarks>
        /// <returns>RegEx: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"</returns>
        //public string ReplacementResidue

        /// <remarks>Location of the modification within the peptide - position in peptide sequence, counted from the N-terminus residue, starting at position 1.
        /// Specific modifications to the N-terminus should be given the location 0.
        /// Modification to the C-terminus should be given as peptide length + 1.</remarks>
        /// <remarks>Optional Attribute</remarks>
        //public int Location

        /// Attribute Existence
        //public bool LocationSpecified

        /// <remarks>Atomic mass delta considering the natural distribution of isotopes in Daltons.
        /// This should only be reported if the original amino acid is known i.e. it is not "X"</remarks>
        /// <remarks>Optional Attribute</remarks>
        //public double AvgMassDelta

        /// Attribute Existence
        //public bool AvgMassDeltaSpecified

        /// <remarks>Atomic mass delta when assuming only the most common isotope of elements in Daltons.
        /// This should only be reported if the original amino acid is known i.e. it is not "X"</remarks>
        /// <remarks>Optional Attribute</remarks>
        //public double MonoisotopicMassDelta

        /// Attribute Existence
        //public bool MonoisotopicMassDeltaSpecified*/
    }

    /// <summary>
    /// MzIdentML DBSequenceType
    /// </summary>
    /// <remarks>A database sequence from the specified SearchDatabase (nucleic acid or amino acid).
    /// If the sequence is nucleic acid, the source nucleic acid sequence should be given in
    /// the seq attribute rather than a translated sequence.</remarks>
    public partial class DBSequenceType : IdentifiableType, IParamGroup
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public DBSequenceType()
        {
            this.Seq = null;
            this.cvParam = null;
            this.userParam = null;
            this.accession = null;
            this.searchDatabase_ref = null;
            this.length = -1;
            this.lengthSpecified = false;
        }

        /*
        /// <remarks>The actual sequence of amino acids or nucleic acid.</remarks>
        /// <remarks>min 0, max 1</remarks>
        /// <returns>RegEx: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]*"</returns>
        //public string Seq

        /// <summary>___ParamGroup___: Additional descriptors for the sequence, such as taxon, description line etc.</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<CVParamType> CVParams

        /// <summary>___ParamGroup___</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<UserParamType> UserParams

        /// <summary>The unique accession of this sequence.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string Accession

        /// <summary>The source database of this sequence.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string SearchDatabase_ref

        /// <summary>The length of the sequence as a number of bases or residues.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public int Length

        /// Attribute Existence
        //public bool LengthSpecified*/
    }

    /// <summary>
    /// MzIdentML SampleType : Containers AnalysisSampleCollectionType
    /// </summary>
    /// <remarks>A description of the sample analyzed by mass spectrometry using CVParams or UserParams.
    /// If a composite sample has been analyzed, a parent sample should be defined, which references subsamples.
    /// This represents any kind of substance used in an experimental workflow, such as whole organisms, cells,
    /// DNA, solutions, compounds and experimental substances (gels, arrays etc.).</remarks>
    ///
    /// <remarks>AnalysisSampleCollectionType: The samples analyzed can optionally be recorded using CV terms for descriptions.
    /// If a composite sample has been analyzed, the subsample association can be used to build a hierarchical description.</remarks>
    /// <remarks>AnalysisSampleCollectionType: child element Sample of type SampleType, min 1, max unbounded</remarks>
    public partial class SampleType : IdentifiableType, IParamGroup
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SampleType()
        {
            this.ContactRole = null;
            this.SubSample = null;
            this.cvParam = null;
            this.userParam = null;
        }

        /*
        /// <remarks>Contact details for the Material. The association to ContactRole could specify, for example, the creator or provider of the Material.</remarks>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<ContactRoleType> ContactRoles

        /// <remarks>min 0, max unbounded</remarks>
        //public List<SubSampleType> SubSamples

        /// <summary>___ParamGroup___: The characteristics of a Material.</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<CVParamType> CVParams

        /// <summary>___ParamGroup___</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<UserParamType> UserParams*/
    }

    /// <summary>
    /// MzIdentML ContactRoleType
    /// </summary>
    /// <remarks>The role that a Contact plays in an organization or with respect to the associating class.
    /// A Contact may have several Roles within scope, and as such, associations to ContactRole
    /// allow the use of a Contact in a certain manner. Examples might include a provider, or a data analyst.</remarks>
    public partial class ContactRoleType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ContactRoleType()
        {
            this.Role = null;
            this.contact_ref = null;
        }

        // min 1, max 1
        //public RoleType Role

        /*
        /// <remarks>When a ContactRole is used, it specifies which Contact the role is associated with.</remarks>
        /// <remarks>Required Attribute</remarks>
        //public string contact_ref*/
    }

    /// <summary>
    /// MzIdentML RoleType
    /// </summary>
    /// <remarks>The roles (lab equipment sales, contractor, etc.) the Contact fills.</remarks>
    public partial class RoleType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public RoleType()
        {
            this.cvParam = null;
        }

        /*
        /// <remarks>CV term for contact roles, such as software provider.</remarks>
        /// <remarks>min 1, max 1</remarks>
        //public CVParamType CVParam*/
    }

    /// <summary>
    /// MzIdentML SubSampleType
    /// </summary>
    /// <remarks>References to the individual component samples within a mixed parent sample.</remarks>
    public partial class SubSampleType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SubSampleType()
        {
            this.sample_ref = null;
        }

        /*
        /// <remarks>A reference to the child sample.</remarks>
        /// <remarks>Required Attribute</remarks>
        //public string SampleRef*/
    }

    /// <summary>
    /// MzIdentML AuditCollectionType; Also C# representation of AuditCollectionType
    /// </summary>
    /// <remarks>A contact is either a person or an organization.</remarks>
    /// <remarks>AuditCollectionType: The complete set of Contacts (people and organizations) for this file.</remarks>
    /// <remarks>AuditCollectionType: min 1, max unbounded, for PersonType XOR OrganizationType</remarks>
    public abstract partial class AbstractContactType : IdentifiableType, IParamGroup
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        protected AbstractContactType()
        {
            this.cvParam = null;
            this.userParam = null;
        }

        /*
        /// <remarks>___ParamGroup___: Attributes of this contact such as address, email, telephone etc.</remarks>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<CVParamType> CVParams

        /// <summary>___ParamGroup___</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<UserParamType> UserParams*/
    }

    /// <summary>
    /// MzIdentML OrganizationType
    /// </summary>
    /// <remarks>Organizations are entities like companies, universities, government agencies.
    /// Any additional information such as the address, email etc. should be supplied either as CV parameters or as user parameters.</remarks>
    public partial class OrganizationType : AbstractContactType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public OrganizationType()
        {
            this.Parent = null;
        }

        /*
        /// <remarks>min 0, max 1</remarks>
        //public ParentOrganizationType Parent*/
    }

    /// <summary>
    /// MzIdentML ParentOrganizationType
    /// </summary>
    /// <remarks>The containing organization (the university or business which a lab belongs to, etc.)</remarks>
    public partial class ParentOrganizationType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ParentOrganizationType()
        {
            this.organization_ref = null;
        }

        /*
        /// <remarks>A reference to the organization this contact belongs to.</remarks>
        /// <remarks>Required Attribute</remarks>
        //public string organizationRef*/
    }

    /// <summary>
    /// MzIdentML PersonType
    /// </summary>
    /// <remarks>A person's name and contact details. Any additional information such as the address,
    /// contact email etc. should be supplied using CV parameters or user parameters.</remarks>
    public partial class PersonType : AbstractContactType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PersonType()
        {
            this.Affiliation = null;
            this.lastName = null;
            this.firstName = null;
            this.midInitials = null;
        }

        /*
        /// <remarks>The organization a person belongs to.</remarks>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<AffiliationInfo> Affiliation

        /// <summary>The Person's last/family name.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string LastName

        /// <summary>The Person's first name.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string FirstName

        /// <summary>The Person's middle initial.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string MidInitials*/
    }

    /// <summary>
    /// MzIdentML AffiliationType
    /// </summary>
    public partial class AffiliationType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public AffiliationType()
        {
            this.organization_ref = null;
        }

        /*
        /// <remarks>>A reference to the organization this contact belongs to.</remarks>
        /// <remarks>Required Attribute</remarks>
        //public string OrganizationRef*/
    }

    /// <summary>
    /// MzIdentML ProviderType
    /// </summary>
    /// <remarks>The provider of the document in terms of the Contact and the software the produced the document instance.</remarks>
    public partial class ProviderType : IdentifiableType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ProviderType()
        {
            this.ContactRole = null;
            this.analysisSoftware_ref = null;
        }

        /*
        /// <remarks>The Contact that provided the document instance.</remarks>
        /// <remarks>min 0, max 1</remarks>
        //public ContactRoleType ContactRole

        /// <summary>The Software that produced the document instance.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string AnalysisSoftwareRef*/
    }

    /// <summary>
    /// MzIdentML AnalysisSoftwareType : Containers AnalysisSoftwareListType
    /// </summary>
    /// <remarks>The software used for performing the analyses.</remarks>
    ///
    /// <remarks>AnalysisSoftwareListType: The software packages used to perform the analyses.</remarks>
    /// <remarks>AnalysisSoftwareListType: child element AnalysisSoftware of type AnalysisSoftwareType, min 1, max unbounded</remarks>
    public partial class AnalysisSoftwareType : IdentifiableType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public AnalysisSoftwareType()
        {
            this.ContactRole = null;
            this.SoftwareName = null;
            this.Customizations = null;
            this.version = null;
            this.uri = null;
        }

        /*
        /// <remarks>The contact details of the organization or person that produced the software</remarks>
        /// <remarks>min 0, max 1</remarks>
        //public ContactRoleType ContactRole

        /// <summary>The name of the analysis software package, sourced from a CV if available.</summary>
        /// <remarks>min 1, max 1</remarks>
        //public ParamType SoftwareName

        /// <summary>Any customizations to the software, such as alternative scoring mechanisms implemented, should be documented here as free text.</summary>
        /// <remarks>min 0, max 1</remarks>
        //public string Customizations

        /// <summary>The version of Software used.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string Version

        /// <summary>URI of the analysis software e.g. manufacturer's website</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string URI*/
    }

    /// <summary>
    /// MzIdentML InputsType
    /// </summary>
    /// <remarks>The inputs to the analyses including the databases searched, the spectral data and the source file converted to mzIdentML.</remarks>
    public partial class InputsType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public InputsType()
        {
            this.SourceFile = null;
            this.SearchDatabase = null;
            this.SpectraData = null;
        }

        /*
        /// <remarks>min 0, max unbounded</remarks>
        //public List<SourceFileType> SourceFile

        /// <remarks>min 0, max unbounded</remarks>
        //public List<SearchDatabaseType> SearchDatabase

        /// <remarks>min 1, max unbounded</remarks>
        //public List<SpectraDataType> SpectraData*/
    }

    /// <summary>
    /// MzIdentML DataCollectionType
    /// </summary>
    /// <remarks>The collection of input and output data sets of the analyses.</remarks>
    public partial class DataCollectionType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public DataCollectionType()
        {
            this.Inputs = null;
            this.AnalysisData = null;
        }

        /*
        /// <remarks>min 1, max 1</remarks>
        //public InputsInfo Inputs

        /// <remarks>min 1, max 1</remarks>
        //public AnalysisDataType AnalysisData*/
    }

    /// <summary>
    /// MzIdentML AnalysisProtocolCollectionType
    /// </summary>
    /// <remarks>The collection of protocols which include the parameters and settings of the performed analyses.</remarks>
    public partial class AnalysisProtocolCollectionType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public AnalysisProtocolCollectionType()
        {
            this.SpectrumIdentificationProtocol = null;
            this.ProteinDetectionProtocol = null;
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public List<SpectrumIdentificationProtocolType> SpectrumIdentificationProtocol

        /// <remarks>min 0, max 1</remarks>
        //public ProteinDetectionProtocolType ProteinDetectionProtocol*/
    }

    /// <summary>
    /// MzIdentML AnalysisCollectionType
    /// </summary>
    /// <remarks>The analyses performed to get the results, which map the input and output data sets.
    /// Analyses are for example: SpectrumIdentification (resulting in peptides) or ProteinDetection (assemble proteins from peptides).</remarks>
    public partial class AnalysisCollectionType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public AnalysisCollectionType()
        {
            this.SpectrumIdentification = null;
            this.ProteinDetection = null;
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public List<SpectrumIdentificationType> SpectrumIdentification

        /// <remarks>min 0, max 1</remarks>
        //public ProteinDetectionType ProteinDetection*/
    }

    /// <summary>
    /// MzIdentML SequenceCollectionType
    /// </summary>
    /// <remarks>The collection of sequences (DBSequence or Peptide) identified and their relationship between
    /// each other (PeptideEvidence) to be referenced elsewhere in the results.</remarks>
    public partial class SequenceCollectionType
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SequenceCollectionType()
        {
            this.DBSequence = null;
            this.Peptide = null;
            this.PeptideEvidence = null;
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public List<DBSequenceType> DBSequences

        /// <remarks>min 0, max unbounded</remarks>
        //public List<PeptideType> Peptides

        /// <remarks>min 0, max unbounded</remarks>
        //public List<PeptideEvidenceType> PeptideEvidences*/
    }
}
