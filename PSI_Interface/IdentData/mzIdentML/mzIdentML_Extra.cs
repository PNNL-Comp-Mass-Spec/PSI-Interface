using System.Collections.Generic;

namespace PSI_Interface.IdentData.mzIdentML
{
    /// <summary>
    /// MzIdentML MzIdentMLType
    /// </summary>
    /// <remarks>The upper-most hierarchy level of mzIdentML with sub-containers for example describing software, 
    /// protocols and search results (spectrum identifications or protein detection results).</remarks>
    public partial class MzIdentMLType : IdentifiableType
    {
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

        /*/// min 1, max 1
        //public IdentDataList<CVInfo> CVList

        /// min 0, max 1
        //public IdentDataList<AnalysisSoftwareInfo> AnalysisSoftwareList

        /// <remarks>The Provider of the mzIdentML record in terms of the contact and software.</remarks>
        /// min 0, max 1
        //public ProviderInfo Provider

        /// min 0, max 1
        //public IdentDataList<AbstractContactType> AuditCollection

        /// min 0, max 1
        //public IdentDataList<SampleType> AnalysisSampleCollection

        /// min 0, max 1
        //public SequenceCollection SequenceCollection

        /// min 1, max 1
        //public AnalysisCollection AnalysisCollection

        /// min 1, max 1
        //public AnalysisProtocolCollection AnalysisProtocolCollection

        /// min 1, max 1
        //public DataCollection DataCollection

        /// <remarks>Any bibliographic references associated with the file</remarks>
        /// min 0, max unbounded
        //public List<BibliographicReferenceType> BibliographicReferences

        /// <remarks>The date on which the file was produced.</remarks>
        /// Optional Attribute
        /// dataTime
        //public System.DateTime CreationDate

        /// Attribute Existence
        //public bool CreationDateSpecified

        /// <remarks>The version of the schema this instance document refers to, in the format x.y.z. 
        /// Changes to z should not affect prevent instance documents from validating.</remarks>
        /// Required Attribute
        /// string, regex: "(1\.1\.\d+)"
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

        /*/// <remarks>The full name of the CV.</remarks>
        /// Required Attribute
        /// string
        //public string FullName

        /// <remarks>The version of the CV.</remarks>
        /// Optional Attribute
        /// string
        //public string Version

        /// <remarks>The URI of the source CV.</remarks>
        /// Required Attribute
        /// anyURI
        //public string URI

        /// <remarks>The unique identifier of this cv within the document to be referenced by cvParam elements.</remarks>
        /// Required Attribute
        /// string
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

        /*/// <remarks>A reference to the SpectrumIdentificationItem element(s).</remarks>
        /// Required Attribute
        /// string
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

        /*/// min 1, max unbounded
        //public List<SpectrumIdentificationItemRefType> SpectrumIdentificationItemRef

        /// <remarks>A reference to the PeptideEvidence element on which this hypothesis is based.</remarks>
        /// Required Attribute
        /// string
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

        /*/// <remarks>The values of this particular measure, corresponding to the index defined in ion type</remarks>
        /// Required Attribute
        /// listOfFloats: string, space-separated floats
        //public List<float> Values

        /// <remarks>A reference to the Measure defined in the FragmentationTable</remarks>
        /// Required Attribute
        /// string
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

        /*/// min 0, max unbounded
        //public List<FragmentArrayType> FragmentArray

        /// <remarks>The type of ion identified.</remarks>
        /// min 1, max 1
        //public CVParamType CVParam

        /// <remarks>The index of ions identified as integers, following standard notation for a-c, x-z e.g. if b3 b5 and b6 have been identified, the index would store "3 5 6". For internal ions, the index contains pairs defining the start and end point - see specification document for examples. For immonium ions, the index is the position of the identified ion within the peptide sequence - if the peptide contains the same amino acid in multiple positions that cannot be distinguished, all positions should be given.</remarks>
        /// Optional Attribute
        /// listOfIntegers: string, space-separated integers
        //public List<string> Index

        /// <remarks>The charge of the identified fragmentation ions.</remarks>
        /// Required Attribute
        /// integer
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

        /*/// <remarks>A reference to the cv element from which this term originates.</remarks>
        /// Required Attribute
        /// string
        //public string CVRef

        /// <remarks>The accession or ID number of this CV term in the source CV.</remarks>
        /// Required Attribute
        /// string
        //public string Accession

        /// <remarks>The name of the parameter.</remarks>
        /// Required Attribute
        /// string
        //public override string Name

        /// <remarks>The user-entered value of the parameter.</remarks>
        /// Optional Attribute
        /// string
        //public override string Value*/
    }

    /*/// <summary>
    /// MzIdentML AbstractParamType; Used instead of: ParamType, ParamGroup, ParamListType
    /// </summary>
    /// <remarks>Abstract entity allowing either cvParam or userParam to be referenced in other schemas.</remarks>
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

        /// <remarks>The name of the parameter.</remarks>
        /// Required Attribute
        /// string
        //public abstract string Name

        /// <remarks>The user-entered value of the parameter.</remarks>
        /// Optional Attribute
        /// string
        //public abstract string Value

        //public CV.CV.CVID UnitCvid

        /// <remarks>An accession number identifying the unit within the OBO foundry Unit CV.</remarks>
        /// Optional Attribute
        /// string
        //public string UnitAccession

        /// <remarks>The name of the unit.</remarks>
        /// Optional Attribute
        /// string
        //public string UnitName

        /// <remarks>If a unit term is referenced, this attribute must refer to the CV 'id' attribute defined in the cvList in this file.</remarks>
        /// Optional Attribute
        /// string
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

        /*/// <remarks>The name of the parameter.</remarks>
        /// Required Attribute
        /// string
        //public override string Name

        /// <remarks>The user-entered value of the parameter.</remarks>
        /// Optional Attribute
        /// string
        //public override string Value

        /// <remarks>The datatype of the parameter, where appropriate (e.g.: xsd:float).</remarks>
        /// Optional Attribute
        /// string
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

        /*/// min 1, max 1
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

        /*/// min 1, max unbounded
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

        /*/// <remarks>A reference to the PeptideEvidenceItem element(s).</remarks>
        /// Required Attribute
        /// string
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

        /*/// min 1, max unbounded
        //public List<SpectrumIdentificationListType> SpectrumIdentificationList

        /// min 0, max 1
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

        /*/// min 0, max 1
        //public List<MeasureType> FragmentationTable

        /// min 1, max unbounded
        //public List<SpectrumIdentificationResultType> SpectrumIdentificationResult

        /// <remarks>___ParamGroup___:Scores or output parameters associated with the SpectrumIdentificationList.</remarks>
        /// min 0, max unbounded
        //public List<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public List<UserParamType> UserParams

        /// <remarks>The number of database sequences searched against. This value should be provided unless a de novo search has been performed.</remarks>
        /// Optional Attribute
        /// long
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

        /*/// min 1, max unbounded
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
        public IdentifiableType()
        {
            this.id = null;
            this.name = null;
        }

        /*/// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
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

        /*/// <remarks>The names of the authors of the reference.</remarks>
        /// Optional Attribute
        /// string
        //public string Authors

        /// <remarks>The name of the journal, book etc.</remarks>
        /// Optional Attribute
        /// string
        //public string Publication

        /// <remarks>The publisher of the publication.</remarks>
        /// Optional Attribute
        /// string
        //public string Publisher

        /// <remarks>The editor(s) of the reference.</remarks>
        /// Optional Attribute
        /// string
        //public string Editor

        /// <remarks>The year of publication.</remarks>
        /// Optional Attribute
        /// integer
        //public int Year

        /// Attribute Existence
        //public bool YearSpecified

        /// <remarks>The volume name or number.</remarks>
        /// Optional Attribute
        /// string
        //public string Volume

        /// <remarks>The issue name or number.</remarks>
        /// Optional Attribute
        /// string
        //public string Issue

        /// <remarks>The page numbers.</remarks>
        /// Optional Attribute
        /// string
        //public string Pages

        /// <remarks>The title of the BibliographicReference.</remarks>
        /// Optional Attribute
        /// string
        //public string Title

        /// <remarks>The DOI of the referenced publication.</remarks>
        /// Optional Attribute
        /// string
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

        /*/// min 1, max unbounded
        //public List<PeptideHypothesisType> PeptideHypothesis

        /// <remarks>___ParamGroup___:Scores or parameters associated with this ProteinDetectionHypothesis e.g. p-value</remarks>
        /// min 0, max unbounded
        //public List<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public List<UserParamType> UserParams

        /// <remarks>A reference to the corresponding DBSequence entry. This optional and redundant, because the PeptideEvidence 
        /// elements referenced from here also map to the DBSequence.</remarks>
        /// Optional Attribute
        /// string
        //public string DBSequenceRef

        /// <remarks>Set to true if the producers of the file has deemed that the ProteinDetectionHypothesis has passed a given 
        /// threshold or been validated as correct. If no such threshold has been set, value of true should be given for all results.</remarks>
        /// Required Attribute
        /// boolean
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

        /*/// min 1, max unbounded
        //public List<ProteinDetectionHypothesisType> ProteinDetectionHypothesis

        /// <remarks>___ParamGroup___:Scores or parameters associated with the ProteinAmbiguityGroup.</remarks>
        /// min 0, max unbounded
        //public List<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
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

        /*/// min 0, max unbounded
        //public List<ProteinAmbiguityGroupType> ProteinAmbiguityGroup

        /// <remarks>___ParamGroup___:Scores or output parameters associated with the whole ProteinDetectionList</remarks>
        /// min 0, max unbounded
        //public List<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
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

        /*/// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// min 1, max unbounded
        //public List<PeptideEvidenceRefType> PeptideEvidenceRef

        /// min 0, max 1
        //public List<IonTypeType> Fragmentation

        /// <remarks>___ParamGroup___:Scores or attributes associated with the SpectrumIdentificationItem e.g. e-value, p-value, score.</remarks>
        /// min 0, max unbounded
        //public List<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public List<UserParamType> UserParams

        /// <remarks>The charge state of the identified peptide.</remarks>
        /// Required Attribute
        /// integer
        //public int ChargeState

        /// <remarks>The mass-to-charge value measured in the experiment in Daltons / charge.</remarks>
        /// Required Attribute
        /// double
        //public double ExperimentalMassToCharge

        /// <remarks>The theoretical mass-to-charge value calculated for the peptide in Daltons / charge.</remarks>
        /// Optional Attribute
        /// double
        //public double CalculatedMassToCharge

        /// Attribute Existence
        //public bool CalculatedMassToChargeSpecified

        /// <remarks>The calculated isoelectric point of the (poly)peptide, with relevant modifications included. 
        /// Do not supply this value if the PI cannot be calcuated properly.</remarks>
        /// Optional Attribute
        /// float
        //public float CalculatedPI

        /// Attribute Existence
        //public bool CalculatedPISpecified

        /// <remarks>A reference to the identified (poly)peptide sequence in the Peptide element.</remarks>
        /// Optional Attribute
        /// string
        //public string PeptideRef

        /// <remarks>For an MS/MS result set, this is the rank of the identification quality as scored by the search engine. 
        /// 1 is the top rank. If multiple identifications have the same top score, they should all be assigned rank =1. 
        /// For PMF data, the rank attribute may be meaningless and values of rank = 0 should be given.</remarks>
        /// Required Attribute
        /// integer
        //public int Rank

        /// <remarks>Set to true if the producers of the file has deemed that the identification has passed a given threshold 
        /// or been validated as correct. If no such threshold has been set, value of true should be given for all results.</remarks>
        /// Required Attribute
        /// boolean
        //public bool PassThreshold

        /// <remarks>A reference should be given to the MassTable used to calculate the sequenceMass only if more than one MassTable has been given.</remarks>
        /// Optional Attribute
        /// string
        //public string MassTableRef

        /// <remarks>A reference should be provided to link the SpectrumIdentificationItem to a Sample 
        /// if more than one sample has been described in the AnalysisSampleCollection.</remarks>
        /// Optional Attribute
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

        /*/// min 1, max unbounded
        //public List<SpectrumIdentificationItemType> SpectrumIdentificationItems

        /// <remarks>___ParamGroup___: Scores or parameters associated with the SpectrumIdentificationResult 
        /// (i.e the set of SpectrumIdentificationItems derived from one spectrum) e.g. the number of peptide 
        /// sequences within the parent tolerance for this spectrum.</remarks>
        /// min 0, max unbounded
        //public List<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public List<UserParamType> UserParams

        /// <remarks>The locally unique id for the spectrum in the spectra data set specified by SpectraData_ref. 
        /// External guidelines are provided on the use of consistent identifiers for spectra in different external formats.</remarks>
        /// Required Attribute
        /// string
        //public string SpectrumID

        /// <remarks>A reference to a spectra data set (e.g. a spectra file).</remarks>
        /// Required Attribute
        /// string
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

        /*/// <remarks>A URI to access documentation and tools to interpret the external format of the ExternalData instance. 
        /// For example, XML Schema or static libraries (APIs) to access binary formats.</remarks>
        /// min 0, max 1
        //public string ExternalFormatDocumentation

        /// min 0, max 1
        //public FileFormatType FileFormat

        /// <remarks>The location of the data file.</remarks>
        /// Required Attribute
        /// string
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

        /*/// <remarks>cvParam capturing file formats</remarks>
        /// Optional Attribute
        /// min 1, max 1
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

        /*/// <remarks>A URI to access documentation and tools to interpret the external format of the ExternalData instance. 
        /// For example, XML Schema or static libraries (APIs) to access binary formats.</remarks>
        /// min 0, max 1
        //public string ExternalFormatDocumentation

        /// min 0, max 1
        //public FileFormatType FileFormat

        /// <remarks>The location of the data file.</remarks>
        /// Required Attribute
        /// string
        //public string Location

        /// min 1, max 1
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

        /*/// <remarks>CV term capturing the type of identifier used.</remarks>
        /// min 1, max 1
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

        /*/// <remarks>A URI to access documentation and tools to interpret the external format of the ExternalData instance. 
        /// For example, XML Schema or static libraries (APIs) to access binary formats.</remarks>
        /// min 0, max 1
        //public string ExternalFormatDocumentation

        /// min 0, max 1
        //public FileFormatType FileFormat

        /// <remarks>The location of the data file.</remarks>
        /// Required Attribute
        /// string
        //public string Location

        /// <remarks>___ParamGroup___:Any additional parameters description the source file.</remarks>
        /// min 0, max unbounded
        //public List<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
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

        /*/// <remarks>A URI to access documentation and tools to interpret the external format of the ExternalData instance. 
        /// For example, XML Schema or static libraries (APIs) to access binary formats.</remarks>
        /// min 0, max 1
        //public string ExternalFormatDocumentation

        /// min 0, max 1
        //public FileFormatType FileFormat

        /// <remarks>The location of the data file.</remarks>
        /// Required Attribute
        /// string
        //public string Location

        /// <remarks>The database name may be given as a cvParam if it maps exactly to one of the release databases listed in the CV, otherwise a userParam should be used.</remarks>
        /// min 1, max 1
        //public ParamType DatabaseName

        /// min 0, max unbounded
        //public List<CVParamType> CVParams

        /// <remarks>The version of the database.</remarks>
        /// Optional Attribute
        /// string
        //public string Version

        /// <remarks>The date and time the database was released to the public; omit this attribute when the date and time are unknown or not applicable (e.g. custom databases).</remarks>
        /// Optional Attribute
        /// dateTime
        //public System.DateTime ReleaseDate

        /// Attribute Existence
        //public bool ReleaseDateSpecified

        /// <remarks>The total number of sequences in the database.</remarks>
        /// Optional Attribute
        /// long
        //public long NumDatabaseSequences

        /// Attribute Existence
        //public bool NumDatabaseSequencesSpecified

        /// <remarks>The number of residues in the database.</remarks>
        /// Optional Attribute
        /// long
        //public long NumResidues

        /// <remarks></remarks>
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

        /*/// <remarks>The parameters and settings for the protein detection given as CV terms.</remarks>
        /// min 0, max 1
        //public ParamListType AnalysisParams

        /// <remarks>The threshold(s) applied to determine that a result is significant. 
        /// If multiple terms are used it is assumed that all conditions are satisfied by the passing results.</remarks>
        /// min 1, max 1
        //public ParamListType Threshold

        /// <remarks>The protein detection software used, given as a reference to the SoftwareCollection section.</remarks>
        /// Required Attribute
        /// string
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

        /*/// <remarks>The details specifying this translation table are captured as cvParams, e.g. translation table, translation 
        /// start codons and translation table description (see specification document and mapping file)</remarks>
        /// min 0, max unbounded
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

        /*/// <remarks>The specification of a single residue within the mass table.</remarks>
        /// min 0, max unbounded
        //public List<ResidueType> Residue

        /// min 0, max unbounded
        //public List<AmbiguousResidueType> AmbiguousResidue

        /// <remarks>___ParamGroup___: Additional parameters or descriptors for the MassTable.</remarks>
        /// min 0, max unbounded
        //public List<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public List<UserParamType> UserParams

        /// <remarks>The MS spectrum that the MassTable refers to e.g. "1" for MS1 "2" for MS2 or "1 2" for MS1 or MS2.</remarks>
        /// Required Attribute
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

        /*/// <remarks>The single letter code for the residue.</remarks>
        /// Required Attribute
        /// chars, string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}"
        //public string Code

        /// <remarks>The residue mass in Daltons (not including any fixed modifications).</remarks>
        /// Required Attribute
        /// float
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

        /*/// <remarks>___ParamGroup___: Parameters for capturing e.g. "alternate single letter codes"</remarks>
        /// min 0, max unbounded
        //public List<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public List<UserParamType> UserParams

        /// <remarks>The single letter code of the ambiguous residue e.g. X.</remarks>
        /// Required Attribute
        /// chars, string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}"
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

        /*/// <remarks>Regular expression for specifying the enzyme cleavage site.</remarks>
        /// min 0, max 1
        //public string SiteRegexp

        /// <remarks>The name of the enzyme from a CV.</remarks>
        /// min 0, max 1
        //public ParamListType EnzymeName

        /// <remarks>Element formula gained at NTerm.</remarks>
        /// Optional Attribute
        /// string, regex: "[A-Za-z0-9 ]+"
        //public string NTermGain

        /// <remarks>Element formula gained at CTerm.</remarks>
        /// Optional Attribute
        /// string, regex: "[A-Za-z0-9 ]+"
        //public string CTermGain

        /// <remarks>Set to true if the enzyme cleaves semi-specifically (i.e. one terminus must cleave 
        /// according to the rules, the other can cleave at any residue), false if the enzyme cleavage 
        /// is assumed to be specific to both termini (accepting for any missed cleavages).</remarks>
        /// Optional Attribute
        /// boolean
        //public bool SemiSpecific

        /// Attribute Existence
        //public bool SemiSpecificSpecified

        /// <remarks>The number of missed cleavage sites allowed by the search. The attribute must be provided if an enzyme has been used.</remarks>
        /// Optional Attribute
        /// integer
        //public int MissedCleavages

        /// Attribute Existence
        //public bool MissedCleavagesSpecified

        /// <remarks>Minimal distance for another cleavage (minimum: 1).</remarks>
        /// Optional Attribute
        /// integer >= 1
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

        /*/// <remarks>The type of search performed e.g. PMF, Tag searches, MS-MS</remarks>
        /// min 1, max 1
        //public ParamType SearchType

        /// <remarks>The search parameters other than the modifications searched.</remarks>
        /// min 0, max 1
        //public ParamListType AdditionalSearchParams

        /// min 0, max 1 : Original ModificationParamsType
        //public List<SearchModificationType> ModificationParams

        /// min 0, max 1
        //public EnzymesType Enzymes

        /// min 0, max unbounded
        //public List<MassTableType> MassTable

        /// min 0, max 1 : Original ToleranceType
        //public List<CVParamType> FragmentTolerance

        /// min 0, max 1 : Original ToleranceType
        //public List<CVParamType> ParentTolerance

        /// <remarks>The threshold(s) applied to determine that a result is significant. If multiple terms are used it is assumed that all conditions are satisfied by the passing results.</remarks>
        /// min 1, max 1
        //public ParamListType Threshold

        /// min 0, max 1 : Original DatabaseFiltersType
        //public List<FilterInfo> DatabaseFilters

        /// min 0, max 1
        //public DatabaseTranslationType DatabaseTranslation

        /// <remarks>The search algorithm used, given as a reference to the SoftwareCollection section.</remarks>
        /// Required Attribute
        /// string
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

        /*/// min 1, max unbounded
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

        /*/// min 0, max unbounded
        //public List<SpecificityRulesType> SpecificityRules

        /// <remarks>The modification is uniquely identified by references to external CVs such as UNIMOD, see 
        /// specification document and mapping file for more details.</remarks>
        /// min 1, max unbounded
        //public List<CVParamType> CVParams

        /// <remarks>True, if the modification is static (i.e. occurs always).</remarks>
        /// Required Attribute
        /// boolean
        //public bool FixedMod

        /// <remarks>The mass delta of the searched modification in Daltons.</remarks>
        /// Required Attribute
        /// float
        //public float MassDelta

        /// <remarks>The residue(s) searched with the specified modification. For N or C terminal modifications that can occur 
        /// on any residue, the . character should be used to specify any, otherwise the list of amino acids should be provided.</remarks>
        /// Required Attribute
        /// listOfCharsOrAny: string, space-separated regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}|."
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

        /*/// min 1, max unbounded
        //public List<EnzymeType> Enzyme

        /// <remarks>If there are multiple enzymes specified, this attribute is set to true if cleavage with different enzymes is performed independently.</remarks>
        /// Optional Attribute
        /// boolean
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

        /*/// <remarks>The type of filter e.g. database taxonomy filter, pi filter, mw filter</remarks>
        /// min 1, max 1
        //public ParamType FilterType

        /// <remarks>All sequences fulfilling the specifed criteria are included.</remarks>
        /// min 0, max 1
        //public ParamListType Include

        /// <remarks>All sequences fulfilling the specifed criteria are excluded.</remarks>
        /// min 0, max 1
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

        /*/// min 1, max unbounded
        //public List<TranslationTableType> TranslationTable

        /// <remarks>The frames in which the nucleic acid sequence has been translated as a space separated List</remarks>
        /// Optional Attribute
        /// listOfAllowedFrames: space-separated string, valid values -3, -2, -1, 1, 2, 3
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
        public ProtocolApplicationType()
        {
            this.activityDate = System.DateTime.Now;
            this.activityDateSpecified = false;
        }

        /*/// <remarks>When the protocol was applied.</remarks>
        /// Optional Attribute
        /// datetime
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

        /*/// min 1, max unbounded
        //public List<InputSpectrumIdentificationsType> InputSpectrumIdentifications

        /// <remarks>A reference to the ProteinDetectionList in the DataCollection section.</remarks>
        /// Required Attribute
        /// string
        //public string ProteinDetectionListRef

        /// <remarks>A reference to the detection protocol used for this ProteinDetection.</remarks>
        /// Required Attribute
        /// string
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

        /*/// <remarks>A reference to the list of spectrum identifications that were input to the process.</remarks>
        /// Required Attribute
        /// string
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

        /*/// <remarks>One of the spectra data sets used.</remarks>
        /// min 1, max unbounded
        //public List<InputSpectraType> InputSpectra

        /// min 1, max unbounded
        //public List<SearchDatabaseRefType> SearchDatabaseRef

        /// <remarks>A reference to the search protocol used for this SpectrumIdentification.</remarks>
        /// Required Attribute
        /// string
        //public string SpectrumIdentificationProtocolRef

        /// <remarks>A reference to the SpectrumIdentificationList produced by this analysis in the DataCollection section.</remarks>
        /// Required Attribute
        /// string
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

        /*/// <remarks>A reference to the SpectraData element which locates the input spectra to an external file.</remarks>
        /// Optional Attribute
        /// string
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

        /*/// <remarks>A reference to the database searched.</remarks>
        /// Optional Attribute
        /// string
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

        /*//public PeptideEvidenceType()

        /// <remarks>___ParamGroup___: Additional parameters or descriptors for the PeptideEvidence.</remarks>
        /// min 0, max unbounded
        //public List<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public List<UserParamType> UserParams

        /// <remarks>Set to true if the peptide is matched to a decoy sequence.</remarks>
        /// Optional Attribute
        /// boolean, default false
        //public bool IsDecoy

        /// <remarks>Previous flanking residue. If the peptide is N-terminal, pre="-" and not pre="". 
        /// If for any reason it is unknown (e.g. denovo), pre="?" should be used.</remarks>
        /// Optional Attribute
        /// string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"
        //public string Pre

        /// <remarks>Post flanking residue. If the peptide is C-terminal, post="-" and not post="". If for any reason it is unknown (e.g. denovo), post="?" should be used.</remarks>
        /// Optional Attribute
        /// string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"
        //public string Post

        /// <remarks>Start position of the peptide inside the protein sequence, where the first amino acid of the 
        /// protein sequence is position 1. Must be provided unless this is a de novo search.</remarks>
        /// Optional Attribute
        /// integer
        //public int Start

        /// Attribute Existence
        //public bool StartSpecified

        /// <remarks>The index position of the last amino acid of the peptide inside the protein sequence, where the first 
        /// amino acid of the protein sequence is position 1. Must be provided unless this is a de novo search.</remarks>
        /// Optional Attribute
        /// integer
        //public int End

        /// Attribute Existence
        //public bool EndSpecified

        /// <remarks>A reference to the translation table used if this is PeptideEvidence derived from nucleic acid sequence</remarks>
        /// Optional Attribute
        /// string
        //public string TranslationTable_ref

        /// <remarks>The translation frame of this sequence if this is PeptideEvidence derived from nucleic acid sequence</remarks>
        /// Optional Attribute
        /// "Allowed Frames", int: -3, -2, -1, 1, 2, 3 
        //public int Frame

        /// Attribute Existence
        //public bool FrameSpecified

        /// <remarks>A reference to the identified (poly)peptide sequence in the Peptide element.</remarks>
        /// Required Attribute
        /// string
        //public string PeptideRef

        /// <remarks>A reference to the protein sequence in which the specified peptide has been linked.</remarks>
        /// Required Attribute
        /// string
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

        /*/// <remarks>The amino acid sequence of the (poly)peptide. If a substitution modification has been found, the original sequence should be reported.</remarks>
        /// min 1, max 1
        //public string PeptideSequence

        /// min 0, max unbounded
        //public List<ModificationType> Modification

        /// min 0, max unbounded
        //public List<SubstitutionModificationType> SubstitutionModification

        /// <remarks>___ParamGroup___: Additional descriptors of this peptide sequence</remarks>
        /// min 0, max unbounded
        //public List<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public List<UserParamType> UserParams*/
    }

    /// <summary>
    /// MzIdentML ModificationType
    /// </summary>
    /// <remarks>A molecule modification specification. If n modifications have been found on a peptide, there should 
    /// be n instances of Modification. If multiple modifications are provided as cvParams, it is assumed that the 
    /// modification is ambiguous i.e. one modification or another. A cvParam must be provided with the identification 
    /// of the modification sourced from a suitable CV e.g. UNIMOD. If the modification is not present in the CV (and 
    /// this will be checked by the semantic validator within a given tolerance window), there is a â€œunknown 
    /// modificationâ€? CV term that must be used instead. A neutral loss should be defined as an additional CVParam 
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

        /*/// <remarks>CV terms capturing the modification, sourced from an appropriate controlled vocabulary.</remarks>
        /// min 1, max unbounded
        //public List<CVParamType> CVParams

        /// <remarks>Location of the modification within the peptide - position in peptide sequence, counted from 
        /// the N-terminus residue, starting at position 1. Specific modifications to the N-terminus should be 
        /// given the location 0. Modification to the C-terminus should be given as peptide length + 1. If the 
        /// modification location is unknown e.g. for PMF data, this attribute should be omitted.</remarks>
        /// Optional Attribute
        /// integer
        //public int Location

        /// Attribute Existence
        //public bool LocationSpecified

        /// <remarks>Specification of the residue (amino acid) on which the modification occurs. If multiple values 
        /// are given, it is assumed that the exact residue modified is unknown i.e. the modification is to ONE of 
        /// the residues listed. Multiple residues would usually only be specified for PMF data.</remarks>
        /// Optional Attribute
        /// listOfChars, string, space-separated regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}"
        //public List<string> Residues

        /// <remarks>Atomic mass delta considering the natural distribution of isotopes in Daltons.</remarks>
        /// Optional Attribute
        /// double
        //public double AvgMassDelta

        /// Attribute Existence
        //public bool AvgMassDeltaSpecified

        /// <remarks>Atomic mass delta when assuming only the most common isotope of elements in Daltons.</remarks>
        /// Optional Attribute
        /// double
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

        /*/// <remarks>The original residue before replacement.</remarks>
        /// Required Attribute
        /// string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"
        //public string OriginalResidue

        /// <remarks>The residue that replaced the originalResidue.</remarks>
        /// Required Attribute
        /// string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"
        //public string ReplacementResidue

        /// <remarks>Location of the modification within the peptide - position in peptide sequence, counted from the N-terminus residue, starting at position 1. 
        /// Specific modifications to the N-terminus should be given the location 0. 
        /// Modification to the C-terminus should be given as peptide length + 1.</remarks>
        /// Optional Attribute
        /// integer
        //public int Location

        /// Attribute Existence
        //public bool LocationSpecified

        /// <remarks>Atomic mass delta considering the natural distribution of isotopes in Daltons. 
        /// This should only be reported if the original amino acid is known i.e. it is not "X"</remarks>
        /// Optional Attribute
        /// double
        //public double AvgMassDelta

        /// Attribute Existence
        //public bool AvgMassDeltaSpecified

        /// <remarks>Atomic mass delta when assuming only the most common isotope of elements in Daltons. 
        /// This should only be reported if the original amino acid is known i.e. it is not "X"</remarks>
        /// Optional Attribute
        /// double
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

        /*/// <remarks>The actual sequence of amino acids or nucleic acid.</remarks>
        /// min 0, max 1
        /// string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]*"
        //public string Seq

        /// <remarks>___ParamGroup___: Additional descriptors for the sequence, such as taxon, description line etc.</remarks>
        /// min 0, max unbounded
        //public List<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public List<UserParamType> UserParams

        /// <remarks>The unique accession of this sequence.</remarks>
        /// Required Attribute
        //public string Accession

        /// <remarks>The source database of this sequence.</remarks>
        /// Required Attribute
        /// string
        //public string SearchDatabase_ref

        /// <remarks>The length of the sequence as a number of bases or residues.</remarks>
        /// Optional Attribute
        /// integer
        //public int Length

        /// Attribute Existence
        //public bool LengthSpecified*/
    }

    /// <summary>
    /// MzIdentML SampleType : Containers AnalysisSampleCollectionType
    /// </summary>
    /// <remarks>A description of the sample analysed by mass spectrometry using CVParams or UserParams. 
    /// If a composite sample has been analysed, a parent sample should be defined, which references subsamples. 
    /// This represents any kind of substance used in an experimental workflow, such as whole organisms, cells, 
    /// DNA, solutions, compounds and experimental substances (gels, arrays etc.).</remarks>
    /// 
    /// <remarks>AnalysisSampleCollectionType: The samples analysed can optionally be recorded using CV terms for descriptions. 
    /// If a composite sample has been analysed, the subsample association can be used to build a hierarchical description.</remarks>
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

        /*/// <remarks>Contact details for the Material. The association to ContactRole could specify, for example, the creator or provider of the Material.</remarks>
        /// min 0, max unbounded
        //public List<ContactRoleType> ContactRoles

        /// min 0, max unbounded
        //public List<SubSampleType> SubSamples

        /// <remarks>___ParamGroup___: The characteristics of a Material.</remarks>
        /// min 0, max unbounded
        //public List<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
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

        /*/// <remarks>When a ContactRole is used, it specifies which Contact the role is associated with.</remarks>
        /// Required Attribute
        /// string
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

        /*/// <remarks>CV term for contact roles, such as software provider.</remarks>
        /// min 1, max 1
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

        /*/// <remarks>A reference to the child sample.</remarks>
        /// Required  Attribute
        /// string
        //public string SampleRef*/
    }

    /// <summary>
    /// MzIdentML AuditCollectionType; Also C# representation of AuditCollectionType
    /// </summary>
    /// <remarks>A contact is either a person or an organization.</remarks>
    /// <remarks>AuditCollectionType: The complete set of Contacts (people and organisations) for this file.</remarks>
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

        /*/// <remarks>___ParamGroup___: Attributes of this contact such as address, email, telephone etc.</remarks>
        /// min 0, max unbounded
        //public List<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
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

        /*/// min 0, max 1
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

        /*/// <remarks>A reference to the organization this contact belongs to.</remarks>
        /// Required Attribute
        /// string
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

        /*/// <remarks>The organization a person belongs to.</remarks>
        /// min 0, max unbounded
        //public List<AffiliationInfo> Affiliation

        /// <remarks>The Person's last/family name.</remarks>
        /// Optional Attribute
        //public string LastName

        /// <remarks>The Person's first name.</remarks>
        /// Optional Attribute
        //public string FirstName

        /// <remarks>The Person's middle initial.</remarks>
        /// Optional Attribute
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

        /*/// <remarks>>A reference to the organization this contact belongs to.</remarks>
        /// Required Attribute
        /// string
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

        /*/// <remarks>The Contact that provided the document instance.</remarks>
        /// min 0, max 1
        //public ContactRoleType ContactRole

        /// <remarks>The Software that produced the document instance.</remarks>
        /// Optional Attribute
        /// string
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

        /*/// <remarks>The contact details of the organisation or person that produced the software</remarks>
        /// min 0, max 1
        //public ContactRoleType ContactRole

        /// <remarks>The name of the analysis software package, sourced from a CV if available.</remarks>
        /// min 1, max 1
        //public ParamType SoftwareName

        /// <remarks>Any customizations to the software, such as alternative scoring mechanisms implemented, should be documented here as free text.</remarks>
        /// min 0, max 1
        //public string Customizations

        /// <remarks>The version of Software used.</remarks>
        /// Optional Attribute
        /// string
        //public string Version

        /// <remarks>URI of the analysis software e.g. manufacturer's website</remarks>
        /// Optional Attribute
        /// anyURI
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

        /*/// min 0, max unbounded
        //public List<SourceFileType> SourceFile

        /// min 0, max unbounded
        //public List<SearchDatabaseType> SearchDatabase

        /// min 1, max unbounde
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

        /*/// min 1, max 1
        //public InputsInfo Inputs

        /// min 1, max 1
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

        /*/// min 1, max unbounded
        //public List<SpectrumIdentificationProtocolType> SpectrumIdentificationProtocol

        /// min 0, max 1
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

        /*/// min 1, max unbounded
        //public List<SpectrumIdentificationType> SpectrumIdentification

        /// min 0, max 1
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

        /*/// min 1, max unbounded
        //public List<DBSequenceType> DBSequences

        /// min 0, max unbounded
        //public List<PeptideType> Peptides

        /// min 0, max unbounded
        //public List<PeptideEvidenceType> PeptideEvidences*/
    }
}
