using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PSI_Interface.IdentData
{
	/// <summary>
	/// MzIdentML MzIdentMLType
	/// </summary>
	/// <remarks>The upper-most hierarchy level of mzIdentML with sub-containers for example describing software, 
	/// protocols and search results (spectrum identifications or protein detection results).</remarks>
	public class MzIdentMLType : IdentifiableType
	{
		private List<cvType> cvListField;
		private List<AnalysisSoftwareType> analysisSoftwareListField;
		private ProviderType providerField;
		private List<AbstractContactType> auditCollectionField;
		private List<SampleType> analysisSampleCollectionField;
		private SequenceCollectionType sequenceCollectionField;
		private AnalysisCollectionType analysisCollectionField;
		private AnalysisProtocolCollectionType analysisProtocolCollectionField;
		private DataCollectionType dataCollectionField;
		private List<BibliographicReferenceType> bibliographicReferenceField;
		private System.DateTime creationDateField;
		private bool creationDateFieldSpecified;
		private string versionField;

		/// min 1, max 1
		public cvType[] cvList
		{
			get { return cvListField.ToArray(); }
			set { cvListField = value.ToList(); }
		}

		/// min 0, max 1
		public AnalysisSoftwareType[] AnalysisSoftwareList
		{
			get { return analysisSoftwareListField.ToArray(); }
			set { analysisSoftwareListField = value.ToList(); }
		}

		/// <remarks>The Provider of the mzIdentML record in terms of the contact and software.</remarks>
		/// min 0, max 1
		public ProviderType Provider
		{
			get { return providerField; }
			set { providerField = value; }
		}

		/// min 0, max 1
		public AbstractContactType[] AuditCollection
		{
			get { return auditCollectionField.ToArray(); }
			set { auditCollectionField = value.ToList(); }
		}

		/// min 0, max 1
		public SampleType[] AnalysisSampleCollection
		{
			get { return analysisSampleCollectionField.ToArray(); }
			set { analysisSampleCollectionField = value.ToList(); }
		}

		/// min 0, max 1
		public SequenceCollectionType SequenceCollection
		{
			get { return sequenceCollectionField; }
			set { sequenceCollectionField = value; }
		}

		/// min 1, max 1
		public AnalysisCollectionType AnalysisCollection
		{
			get { return analysisCollectionField; }
			set { analysisCollectionField = value; }
		}

		/// min 1, max 1
		public AnalysisProtocolCollectionType AnalysisProtocolCollection
		{
			get { return analysisProtocolCollectionField; }
			set { analysisProtocolCollectionField = value; }
		}

		/// min 1, max 1
		public DataCollectionType DataCollection
		{
			get { return dataCollectionField; }
			set { dataCollectionField = value; }
		}

		/// <remarks>Any bibliographic references associated with the file</remarks>
		/// min 0, max unbounded
		public BibliographicReferenceType[] BibliographicReference
		{
			get { return bibliographicReferenceField.ToArray(); }
			set { bibliographicReferenceField = value.ToList(); }
		}

		/// <remarks>The date on which the file was produced.</remarks>
		/// Optional Attribute
		/// dataTime
		public System.DateTime creationDate
		{
			get { return creationDateField; }
			set { creationDateField = value; }
		}

		/// Attribute Existence
		public bool creationDateSpecified
		{
			get { return creationDateFieldSpecified; }
			set { creationDateFieldSpecified = value; }
		}

		/// <remarks>The version of the schema this instance document refers to, in the format x.y.z. 
		/// Changes to z should not affect prevent instance documents from validating.</remarks>
		/// Required Attribute
		/// string, regex: "(1\.1\.\d+)"
		public string version
		{
			get { return versionField; }
			set { versionField = value; }
		}
	}

	/// <summary>
	/// MzIdentML cvType : Container CVListType
	/// </summary>
	/// <remarks>A source controlled vocabulary from which cvParams will be obtained.</remarks>
	/// 
	/// <remarks>CVListType: The list of controlled vocabularies used in the file.</remarks>
	/// <remarks>CVListType: child element cv of type cvType, min 1, max unbounded</remarks>
	public class cvType
	{
		private string fullNameField;
		private string versionField;
		private string uriField;
		private string idField;

		/// <remarks>The full name of the CV.</remarks>
		/// Required Attribute
		/// string
		public string fullName
		{
			get { return fullNameField; }
			set { fullNameField = value; }
		}

		/// <remarks>The version of the CV.</remarks>
		/// Optional Attribute
		/// string
		public string version
		{
			get { return versionField; }
			set { versionField = value; }
		}

		/// <remarks>The URI of the source CV.</remarks>
		/// Required Attribute
		/// anyURI
		public string uri
		{
			get { return uriField; }
			set { uriField = value; }
		}

		/// <remarks>The unique identifier of this cv within the document to be referenced by cvParam elements.</remarks>
		/// Required Attribute
		/// string
		public string id
		{
			get { return idField; }
			set { idField = value; }
		}
	}

	/// <summary>
	/// MzIdentML SpectrumIdentificationItemRefType
	/// </summary>
	/// <remarks>Reference(s) to the SpectrumIdentificationItem element(s) that support the given PeptideEvidence element. 
	/// Using these references it is possible to indicate which spectra were actually accepted as evidence for this 
	/// peptide identification in the given protein.</remarks>
	public class SpectrumIdentificationItemRefType
	{
		private string spectrumIdentificationItem_refField;

		/// <remarks>A reference to the SpectrumIdentificationItem element(s).</remarks>
		/// Required Attribute
		/// string
		public string spectrumIdentificationItem_ref
		{
			get { return spectrumIdentificationItem_refField; }
			set { spectrumIdentificationItem_refField = value; }
		}
	}

	/// <summary>
	/// MzIdentML PeptideHypothesisType
	/// </summary>
	/// <remarks>Peptide evidence on which this ProteinHypothesis is based by reference to a PeptideEvidence element.</remarks>
	public class PeptideHypothesisType
	{
		private List<SpectrumIdentificationItemRefType> spectrumIdentificationItemRefField;
		private string peptideEvidence_refField;

		/// min 1, max unbounded
		public SpectrumIdentificationItemRefType[] SpectrumIdentificationItemRef
		{
			get { return spectrumIdentificationItemRefField.ToArray(); }
			set { spectrumIdentificationItemRefField = value.ToList(); }
		}

		/// <remarks>A reference to the PeptideEvidence element on which this hypothesis is based.</remarks>
		/// Required Attribute
		/// string
		public string peptideEvidence_ref
		{
			get { return peptideEvidence_refField; }
			set { peptideEvidence_refField = value; }
		}
	}

	/// <summary>
	/// MzIdentML FragmentArrayType
	/// </summary>
	/// <remarks>An array of values for a given type of measure and for a particular ion type, in parallel to the index of ions identified.</remarks>
	public class FragmentArrayType
	{
		private List<float> valuesField;
		private string measure_refField;

		/// <remarks>The values of this particular measure, corresponding to the index defined in ion type</remarks>
		/// Required Attribute
		/// listOfFloats: string, space-separated floats
		public float[] values
		{
			get { return valuesField.ToArray(); }
			set { valuesField = value.ToList(); }
		}

		/// <remarks>A reference to the Measure defined in the FragmentationTable</remarks>
		/// Required Attribute
		/// string
		public string measure_ref
		{
			get { return measure_refField; }
			set { measure_refField = value; }
		}
	}

	/// <summary>
	/// MzIdentML IonTypeType: list form is FragmentationType
	/// </summary>
	/// <remarks>IonType defines the index of fragmentation ions being reported, importing a CV term for the type of ion e.g. b ion. 
	/// Example: if b3 b7 b8 and b10 have been identified, the index attribute will contain 3 7 8 10, and the corresponding values 
	/// will be reported in parallel arrays below</remarks>
	/// <remarks>FragmentationType: The product ions identified in this result.</remarks>
	/// <remarks>FragmentationType: child element IonType, of type IonTypeType, min 1, max unbounded</remarks>
	public class IonTypeType
	{
		private List<FragmentArrayType> fragmentArrayField;
		private CVParamType cvParamField;
		private List<string> indexField;
		private int chargeField;

		/// min 0, max unbounded
		public FragmentArrayType[] FragmentArray
		{
			get { return fragmentArrayField.ToArray(); }
			set { fragmentArrayField = value.ToList(); }
		}

		/// <remarks>The type of ion identified.</remarks>
		/// min 1, max 1
		public CVParamType cvParam
		{
			get { return cvParamField; }
			set { cvParamField = value; }
		}

		/// <remarks>The index of ions identified as integers, following standard notation for a-c, x-z e.g. if b3 b5 and b6 have been identified, the index would store "3 5 6". For internal ions, the index contains pairs defining the start and end point - see specification document for examples. For immonium ions, the index is the position of the identified ion within the peptide sequence - if the peptide contains the same amino acid in multiple positions that cannot be distinguished, all positions should be given.</remarks>
		/// Optional Attribute
		/// listOfIntegers: string, space-separated integers
		public string[] index
		{
			get { return indexField.ToArray(); }
			set { indexField = value.ToList(); }
		}

		/// <remarks>The charge of the identified fragmentation ions.</remarks>
		/// Required Attribute
		/// integer
		public int charge
		{
			get { return chargeField; }
			set { chargeField = value; }
		}
	}

	/// <summary>
	/// MzIdentML CVParamType; Container types: ToleranceType
	/// </summary>
	/// <remarks>A single entry from an ontology or a controlled vocabulary.</remarks>
	/// <remarks>ToleranceType: The tolerance of the search given as a plus and minus value with units.</remarks>
	/// <remarks>ToleranceType: child element cvParam of type CVParamType, min 1, max unbounded "CV terms capturing the tolerance plus and minus values."</remarks>
	public class CVParamType : AbstractParamType
	{
		private string cvRefField;
		private string accessionField;

		/// <remarks>A reference to the cv element from which this term originates.</remarks>
		/// Required Attribute
		/// string
		public string cvRef
		{
			get { return cvRefField; }
			set { cvRefField = value; }
		}

		/// <remarks>The accession or ID number of this CV term in the source CV.</remarks>
		/// Required Attribute
		/// string
		public string accession
		{
			get { return accessionField; }
			set { accessionField = value; }
		}
	}

	/// <summary>
	/// MzIdentML AbstractParamType; Used instead of: ParamType, ParamGroup, ParamListType
	/// </summary>
	/// <remarks>Abstract entity allowing either cvParam or userParam to be referenced in other schemas.</remarks>
	/// <remarks>PramGroup: A choice of either a cvParam or userParam.</remarks>
	/// <remarks>ParamType: Helper type to allow either a cvParam or a userParam to be provided for an element.</remarks>
	/// <remarks>ParamListType: Helper type to allow multiple cvParams or userParams to be given for an element.</remarks>
	public abstract class AbstractParamType
	{
		private string nameField;
		private string valueField;
		private string unitAccessionField;
		private string unitNameField;
		private string unitCvRefField;

		/// <remarks>The name of the parameter.</remarks>
		/// Required Attribute
		/// string
		public string name
		{
			get { return nameField; }
			set { nameField = value; }
		}

		/// <remarks>The user-entered value of the parameter.</remarks>
		/// Optional Attribute
		/// string
		public string value
		{
			get { return valueField; }
			set { valueField = value; }
		}

		/// <remarks>An accession number identifying the unit within the OBO foundry Unit CV.</remarks>
		/// Optional Attribute
		/// string
		public string unitAccession
		{
			get { return unitAccessionField; }
			set { unitAccessionField = value; }
		}

		/// <remarks>The name of the unit.</remarks>
		/// Optional Attribute
		/// string
		public string unitName
		{
			get { return unitNameField; }
			set { unitNameField = value; }
		}

		/// <remarks>If a unit term is referenced, this attribute must refer to the CV 'id' attribute defined in the cvList in this file.</remarks>
		/// Optional Attribute
		/// string
		public string unitCvRef
		{
			get { return unitCvRefField; }
			set { unitCvRefField = value; }
		}
	}

	/// <summary>
	/// MzIdentML UserParamType
	/// </summary>
	/// <remarks>A single user-defined parameter.</remarks>
	public class UserParamType : AbstractParamType
	{
		private string typeField;

		/// <remarks>The datatype of the parameter, where appropriate (e.g.: xsd:float).</remarks>
		/// Optional Attribute
		/// string
		public string type
		{
			get { return typeField; }
			set { typeField = value; }
		}
	}

	/// <summary>
	/// MzIdentML PeptideEvidenceRefType
	/// </summary>
	/// <remarks>Reference to the PeptideEvidence element identified. If a specific sequence can be assigned to multiple 
	/// proteins and or positions in a protein all possible PeptideEvidence elements should be referenced here.</remarks>
	public class PeptideEvidenceRefType
	{
		private string peptideEvidence_refField;

		/// <remarks>A reference to the PeptideEvidenceItem element(s).</remarks>
		/// Required Attribute
		/// string
		public string peptideEvidence_ref
		{
			get { return peptideEvidence_refField; }
			set { peptideEvidence_refField = value; }
		}
	}

	/// <summary>
	/// MzIdentML AnalysisDataType
	/// </summary>
	/// <remarks>Data sets generated by the analyses, including peptide and protein lists.</remarks>
	public class AnalysisDataType
	{
		private List<SpectrumIdentificationListType> spectrumIdentificationListField;
		private ProteinDetectionListType proteinDetectionListField;

		/// min 1, max unbounded
		public SpectrumIdentificationListType[] SpectrumIdentificationList
		{
			get { return spectrumIdentificationListField.ToArray(); }
			set { spectrumIdentificationListField = value.ToList(); }
		}

		/// min 0, max 1
		public ProteinDetectionListType ProteinDetectionList
		{
			get { return proteinDetectionListField; }
			set { proteinDetectionListField = value; }
		}
	}

	/// <summary>
	/// MzIdentML SpectrumIdentificationListType
	/// </summary>
	/// <remarks>Represents the set of all search results from SpectrumIdentification.</remarks>
	public class SpectrumIdentificationListType : IdentifiableType
	{
		private List<MeasureType> fragmentationTableField;
		private List<SpectrumIdentificationResultType> spectrumIdentificationResultField;
		private List<CVParamType> cvParamField;
		private List<UserParamType> userParamField;
		private long numSequencesSearchedField;
		private bool numSequencesSearchedFieldSpecified;

		/// min 0, max 1
		public MeasureType[] FragmentationTable
		{
			get { return fragmentationTableField.ToArray(); }
			set { fragmentationTableField = value.ToList(); }
		}

		/// min 1, max unbounded
		public SpectrumIdentificationResultType[] SpectrumIdentificationResult
		{
			get { return spectrumIdentificationResultField.ToArray(); }
			set { spectrumIdentificationResultField = value.ToList(); }
		}

		/// <remarks>___ParamGroup___:Scores or output parameters associated with the SpectrumIdentificationList.</remarks>
		/// min 0, max unbounded
		public CVParamType[] cvParam
		{
			get { return cvParamField.ToArray(); }
			set { cvParamField = value.ToList(); }
		}

		/// <remarks>___ParamGroup___</remarks>
		/// min 0, max unbounded
		public UserParamType[] userParam
		{
			get { return userParamField.ToArray(); }
			set { userParamField = value.ToList(); }
		}

		/// <remarks>The number of database sequences searched against. This value should be provided unless a de novo search has been performed.</remarks>
		/// Optional Attribute
		/// long
		public long numSequencesSearched
		{
			get { return numSequencesSearchedField; }
			set { numSequencesSearchedField = value; }
		}

		/// Attribute Existence
		public bool numSequencesSearchedSpecified
		{
			get { return numSequencesSearchedFieldSpecified; }
			set { numSequencesSearchedFieldSpecified = value; }
		}
	}

	/// <summary>
	/// MzIdentML MeasureType; list form is FragmentationTableType
	/// </summary>
	/// <remarks>References to CV terms defining the measures about product ions to be reported in SpectrumIdentificationItem</remarks>
	/// <remarks>FragmentationTableType: Contains the types of measures that will be reported in generic arrays 
	/// for each SpectrumIdentificationItem e.g. product ion m/z, product ion intensity, product ion m/z error</remarks>
	/// <remarks>FragmentationTableType: child element Measure of type MeasureType, min 1, max unbounded</remarks>
	public class MeasureType : IdentifiableType
	{
		private List<CVParamType> cvParamField;

		/// min 1, max unbounded
		public CVParamType[] cvParam
		{
			get { return cvParamField.ToArray(); }
			set { cvParamField = value.ToList(); }
		}
	}

	/// <summary>
	/// MzIdentML IdentifiableType
	/// </summary>
	/// <remarks>Other classes in the model can be specified as sub-classes, inheriting from Identifiable. 
	/// Identifiable gives classes a unique identifier within the scope and a name that need not be unique.</remarks>
	public abstract class IdentifiableType
	{
		private string idField;
		private string nameField;

		/// <remarks>An identifier is an unambiguous string that is unique within the scope 
		/// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
		/// Required Attribute
		/// string
		public string id
		{
			get { return idField; }
			set { idField = value; }
		}

		/// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
		/// Required Attribute
		/// string
		public string name
		{
			get { return nameField; }
			set { nameField = value; }
		}
	}

	/// <summary>
	/// MzIdentML BibliographicReferenceType
	/// </summary>
	/// <remarks>Represents bibliographic references.</remarks>
	public class BibliographicReferenceType : IdentifiableType
	{
		private string authorsField;
		private string publicationField;
		private string publisherField;
		private string editorField;
		private int yearField;
		private bool yearFieldSpecified;
		private string volumeField;
		private string issueField;
		private string pagesField;
		private string titleField;
		private string doiField;

		/// <remarks>The names of the authors of the reference.</remarks>
		/// Optional Attribute
		/// string
		public string authors
		{
			get { return authorsField; }
			set { authorsField = value; }
		}

		/// <remarks>The name of the journal, book etc.</remarks>
		/// Optional Attribute
		/// string
		public string publication
		{
			get { return publicationField; }
			set { publicationField = value; }
		}

		/// <remarks>The publisher of the publication.</remarks>
		/// Optional Attribute
		/// string
		public string publisher
		{
			get { return publisherField; }
			set { publisherField = value; }
		}

		/// <remarks>The editor(s) of the reference.</remarks>
		/// Optional Attribute
		/// string
		public string editor
		{
			get { return editorField; }
			set { editorField = value; }
		}

		/// <remarks>The year of publication.</remarks>
		/// Optional Attribute
		/// integer
		public int year
		{
			get { return yearField; }
			set { yearField = value; }
		}

		/// Attribute Existence
		public bool yearSpecified
		{
			get { return yearFieldSpecified; }
			set { yearFieldSpecified = value; }
		}

		/// <remarks>The volume name or number.</remarks>
		/// Optional Attribute
		/// string
		public string volume
		{
			get { return volumeField; }
			set { volumeField = value; }
		}

		/// <remarks>The issue name or number.</remarks>
		/// Optional Attribute
		/// string
		public string issue
		{
			get { return issueField; }
			set { issueField = value; }
		}

		/// <remarks>The page numbers.</remarks>
		/// Optional Attribute
		/// string
		public string pages
		{
			get { return pagesField; }
			set { pagesField = value; }
		}

		/// <remarks>The title of the BibliographicReference.</remarks>
		/// Optional Attribute
		/// string
		public string title
		{
			get { return titleField; }
			set { titleField = value; }
		}

		/// <remarks>The DOI of the referenced publication.</remarks>
		/// Optional Attribute
		/// string
		public string doi
		{
			get { return doiField; }
			set { doiField = value; }
		}
	}

	/// <summary>
	/// MzIdentML ProteinDetectionHypothesisType
	/// </summary>
	/// <remarks>A single result of the ProteinDetection analysis (i.e. a protein).</remarks>
	public class ProteinDetectionHypothesisType : IdentifiableType
	{
		private List<PeptideHypothesisType> peptideHypothesisField;
		private List<CVParamType> cvParamField;
		private List<UserParamType> userParamField;
		private string dBSequence_refField;
		private bool passThresholdField;

		/// min 1, max unbounded
		public PeptideHypothesisType[] PeptideHypothesis
		{
			get { return peptideHypothesisField.ToArray(); }
			set { peptideHypothesisField = value.ToList(); }
		}

		/// <remarks>___ParamGroup___:Scores or parameters associated with this ProteinDetectionHypothesis e.g. p-value</remarks>
		/// min 0, max unbounded
		public CVParamType[] cvParam
		{
			get { return cvParamField.ToArray(); }
			set { cvParamField = value.ToList(); }
		}

		/// <remarks>___ParamGroup___</remarks>
		/// min 0, max unbounded
		public UserParamType[] userParam
		{
			get { return userParamField.ToArray(); }
			set { userParamField = value.ToList(); }
		}

		/// <remarks>A reference to the corresponding DBSequence entry. This optional and redundant, because the PeptideEvidence 
		/// elements referenced from here also map to the DBSequence.</remarks>
		/// Optional Attribute
		/// string
		public string dBSequence_ref
		{
			get { return dBSequence_refField; }
			set { dBSequence_refField = value; }
		}

		/// <remarks>Set to true if the producers of the file has deemed that the ProteinDetectionHypothesis has passed a given 
		/// threshold or been validated as correct. If no such threshold has been set, value of true should be given for all results.</remarks>
		/// Required Attribute
		/// boolean
		public bool passThreshold
		{
			get { return passThresholdField; }
			set { passThresholdField = value; }
		}
	}

	/// <summary>
	/// MzIdentML ProteinAmbiguityGroupType
	/// </summary>
	/// <remarks>A set of logically related results from a protein detection, for example to represent conflicting assignments of peptides to proteins.</remarks>
	public class ProteinAmbiguityGroupType : IdentifiableType
	{
		private List<ProteinDetectionHypothesisType> proteinDetectionHypothesisField;
		private List<CVParamType> cvParamField;
		private List<UserParamType> userParamField;

		/// min 1, max unbounded
		public ProteinDetectionHypothesisType[] ProteinDetectionHypothesis
		{
			get { return proteinDetectionHypothesisField.ToArray(); }
			set { proteinDetectionHypothesisField = value.ToList(); }
		}

		/// <remarks>___ParamGroup___:Scores or parameters associated with the ProteinAmbiguityGroup.</remarks>
		/// min 0, max unbounded
		public CVParamType[] cvParam
		{
			get { return cvParamField.ToArray(); }
			set { cvParamField = value.ToList(); }
		}

		/// <remarks>___ParamGroup___</remarks>
		/// min 0, max unbounded
		public UserParamType[] userParam
		{
			get { return userParamField.ToArray(); }
			set { userParamField = value.ToList(); }
		}
	}

	/// <summary>
	/// MzIdentML ProteinDetectionListType
	/// </summary>
	/// <remarks>The protein list resulting from a protein detection process.</remarks>
	public class ProteinDetectionListType : IdentifiableType
	{
		private List<ProteinAmbiguityGroupType> proteinAmbiguityGroupField;
		private List<CVParamType> cvParamField;
		private List<UserParamType> userParamField;

		/// min 0, max unbounded
		public ProteinAmbiguityGroupType[] ProteinAmbiguityGroup
		{
			get { return proteinAmbiguityGroupField.ToArray(); }
			set { proteinAmbiguityGroupField = value.ToList(); }
		}

		/// <remarks>___ParamGroup___:Scores or output parameters associated with the whole ProteinDetectionList</remarks>
		/// min 0, max unbounded
		public CVParamType[] cvParam
		{
			get { return cvParamField.ToArray(); }
			set { cvParamField = value.ToList(); }
		}

		/// <remarks>___ParamGroup___</remarks>
		/// min 0, max unbounded
		public UserParamType[] userParam
		{
			get { return userParamField.ToArray(); }
			set { userParamField = value.ToList(); }
		}
	}

	/// <summary>
	/// MzIdentML SpectrumIdentificationItemType
	/// </summary>
	/// <remarks>An identification of a single (poly)peptide, resulting from querying an input spectra, along with 
	/// the set of confidence values for that identification. PeptideEvidence elements should be given for all 
	/// mappings of the corresponding Peptide sequence within protein sequences.</remarks>
	public class SpectrumIdentificationItemType : IdentifiableType
	{
		private List<PeptideEvidenceRefType> peptideEvidenceRefField;
		private List<IonTypeType> fragmentationField;
		private List<CVParamType> cvParamField;
		private List<UserParamType> userParamField;
		private int chargeStateField;
		private double experimentalMassToChargeField;
		private double calculatedMassToChargeField;
		private bool calculatedMassToChargeFieldSpecified;
		private float calculatedPIField;
		private bool calculatedPIFieldSpecified;
		private string peptide_refField;
		private int rankField;
		private bool passThresholdField;
		private string massTable_refField;
		private string sample_refField;

		/// min 1, max unbounded
		public PeptideEvidenceRefType[] PeptideEvidenceRef
		{
			get { return peptideEvidenceRefField.ToArray(); }
			set { peptideEvidenceRefField = value.ToList(); }
		}

		/// min 0, max 1
		public IonTypeType[] Fragmentation
		{
			get { return fragmentationField.ToArray(); }
			set { fragmentationField = value.ToList(); }
		}

		/// <remarks>___ParamGroup___:Scores or attributes associated with the SpectrumIdentificationItem e.g. e-value, p-value, score.</remarks>
		/// min 0, max unbounded
		public CVParamType[] cvParam
		{
			get { return cvParamField.ToArray(); }
			set { cvParamField = value.ToList(); }
		}

		/// <remarks>___ParamGroup___</remarks>
		/// min 0, max unbounded
		public UserParamType[] userParam
		{
			get { return userParamField.ToArray(); }
			set { userParamField = value.ToList(); }
		}

		/// <remarks>The charge state of the identified peptide.</remarks>
		/// Required Attribute
		/// integer
		public int chargeState
		{
			get { return chargeStateField; }
			set { chargeStateField = value; }
		}

		/// <remarks>The mass-to-charge value measured in the experiment in Daltons / charge.</remarks>
		/// Required Attribute
		/// double
		public double experimentalMassToCharge
		{
			get { return experimentalMassToChargeField; }
			set { experimentalMassToChargeField = value; }
		}

		/// <remarks>The theoretical mass-to-charge value calculated for the peptide in Daltons / charge.</remarks>
		/// Optional Attribute
		/// double
		public double calculatedMassToCharge
		{
			get { return calculatedMassToChargeField; }
			set { calculatedMassToChargeField = value; }
		}

		/// Attribute Existence
		public bool calculatedMassToChargeSpecified
		{
			get { return calculatedMassToChargeFieldSpecified; }
			set { calculatedMassToChargeFieldSpecified = value; }
		}

		/// <remarks>The calculated isoelectric point of the (poly)peptide, with relevant modifications included. 
		/// Do not supply this value if the PI cannot be calcuated properly.</remarks>
		/// Optional Attribute
		/// float
		public float calculatedPI
		{
			get { return calculatedPIField; }
			set { calculatedPIField = value; }
		}

		/// Attribute Existence
		public bool calculatedPISpecified
		{
			get { return calculatedPIFieldSpecified; }
			set { calculatedPIFieldSpecified = value; }
		}

		/// <remarks>A reference to the identified (poly)peptide sequence in the Peptide element.</remarks>
		/// Optional Attribute
		/// string
		public string peptide_ref
		{
			get { return peptide_refField; }
			set { peptide_refField = value; }
		}

		/// <remarks>For an MS/MS result set, this is the rank of the identification quality as scored by the search engine. 
		/// 1 is the top rank. If multiple identifications have the same top score, they should all be assigned rank =1. 
		/// For PMF data, the rank attribute may be meaningless and values of rank = 0 should be given.</remarks>
		/// Required Attribute
		/// integer
		public int rank
		{
			get { return rankField; }
			set { rankField = value; }
		}

		/// <remarks>Set to true if the producers of the file has deemed that the identification has passed a given threshold 
		/// or been validated as correct. If no such threshold has been set, value of true should be given for all results.</remarks>
		/// Required Attribute
		/// boolean
		public bool passThreshold
		{
			get { return passThresholdField; }
			set { passThresholdField = value; }
		}

		/// <remarks>A reference should be given to the MassTable used to calculate the sequenceMass only if more than one MassTable has been given.</remarks>
		/// Optional Attribute
		/// string
		public string massTable_ref
		{
			get { return massTable_refField; }
			set { massTable_refField = value; }
		}

		/// <remarks>A reference should be provided to link the SpectrumIdentificationItem to a Sample 
		/// if more than one sample has been described in the AnalysisSampleCollection.</remarks>
		/// Optional Attribute
		public string sample_ref
		{
			get { return sample_refField; }
			set { sample_refField = value; }
		}
	}

	/// <summary>
	/// MzIdentML SpectrumIdentificationResultType
	/// </summary>
	/// <remarks>All identifications made from searching one spectrum. For PMF data, all peptide identifications 
	/// will be listed underneath as SpectrumIdentificationItems. For MS/MS data, there will be ranked 
	/// SpectrumIdentificationItems corresponding to possible different peptide IDs.</remarks>
	public class SpectrumIdentificationResultType : IdentifiableType
	{
		private List<SpectrumIdentificationItemType> spectrumIdentificationItemField;
		private List<CVParamType> cvParamField;
		private List<UserParamType> userParamField;
		private string spectrumIDField;
		private string spectraData_refField;

		/// min 1, max unbounded
		public SpectrumIdentificationItemType[] SpectrumIdentificationItem
		{
			get { return spectrumIdentificationItemField.ToArray(); }
			set { spectrumIdentificationItemField = value.ToList(); }
		}

		/// <remarks>___ParamGroup___: Scores or parameters associated with the SpectrumIdentificationResult 
		/// (i.e the set of SpectrumIdentificationItems derived from one spectrum) e.g. the number of peptide 
		/// sequences within the parent tolerance for this spectrum.</remarks>
		/// min 0, max unbounded
		public CVParamType[] cvParam
		{
			get { return cvParamField.ToArray(); }
			set { cvParamField = value.ToList(); }
		}

		/// <remarks>___ParamGroup___</remarks>
		/// min 0, max unbounded
		public UserParamType[] userParam
		{
			get { return userParamField.ToArray(); }
			set { userParamField = value.ToList(); }
		}

		/// <remarks>The locally unique id for the spectrum in the spectra data set specified by SpectraData_ref. 
		/// External guidelines are provided on the use of consistent identifiers for spectra in different external formats.</remarks>
		/// Required Attribute
		/// string
		public string spectrumID
		{
			get { return spectrumIDField; }
			set { spectrumIDField = value; }
		}

		/// <remarks>A reference to a spectra data set (e.g. a spectra file).</remarks>
		/// Required Attribute
		/// string
		public string spectraData_ref
		{
			get { return spectraData_refField; }
			set { spectraData_refField = value; }
		}
	}

	/// <summary>
	/// MzIdentML ExternalDataType
	/// </summary>
	/// <remarks>Data external to the XML instance document. The location of the data file is given in the location attribute.</remarks>
	public class ExternalDataType : IdentifiableType
	{
		private string externalFormatDocumentationField;
		private FileFormatType fileFormatField;
		private string locationField;

		/// <remarks>A URI to access documentation and tools to interpret the external format of the ExternalData instance. 
		/// For example, XML Schema or static libraries (APIs) to access binary formats.</remarks>
		/// min 0, max 1
		public string ExternalFormatDocumentation
		{
			get { return externalFormatDocumentationField; }
			set { externalFormatDocumentationField = value; }
		}

		/// min 0, max 1
		public FileFormatType FileFormat
		{
			get { return fileFormatField; }
			set { fileFormatField = value; }
		}

		/// <remarks>The location of the data file.</remarks>
		/// Required Attribute
		/// string
		public string location
		{
			get { return locationField; }
			set { locationField = value; }
		}
	}

	/// <summary>
	/// MzIdentML FileFormatType
	/// </summary>
	/// <remarks>The format of the ExternalData file, for example "tiff" for image files.</remarks>
	public class FileFormatType
	{
		private CVParamType cvParamField;

		/// <remarks>cvParam capturing file formats</remarks>
		/// Optional Attribute
		/// min 1, max 1
		public CVParamType cvParam
		{
			get { return cvParamField; }
			set { cvParamField = value; }
		}
	}

	/// <summary>
	/// MzIdentML SpectraDataType
	/// </summary>
	/// <remarks>A data set containing spectra data (consisting of one or more spectra).</remarks>
	public class SpectraDataType : ExternalDataType
	{
		private SpectrumIDFormatType spectrumIDFormatField;

		/// min 1, max 1
		public SpectrumIDFormatType SpectrumIDFormat
		{
			get { return spectrumIDFormatField; }
			set { spectrumIDFormatField = value; }
		}
	}

	/// <summary>
	/// MzIdentML SpectrumIDFormatType
	/// </summary>
	/// <remarks>The format of the spectrum identifier within the source file</remarks>
	public class SpectrumIDFormatType
	{
		private CVParamType cvParamField;

		/// <remarks>CV term capturing the type of identifier used.</remarks>
		/// min 1, max 1
		public CVParamType cvParam
		{
			get { return cvParamField; }
			set { cvParamField = value; }
		}
	}

	/// <summary>
	/// MzIdentML SourceFileType
	/// </summary>
	/// <remarks>A file from which this mzIdentML instance was created.</remarks>
	public class SourceFileType : ExternalDataType
	{
		private List<CVParamType> cvParamField;
		private List<UserParamType> userParamField;

		/// <remarks>___ParamGroup___:Any additional parameters description the source file.</remarks>
		/// min 0, max unbounded
		public CVParamType[] cvParam
		{
			get { return cvParamField.ToArray(); }
			set { cvParamField = value.ToList(); }
		}

		/// <remarks>___ParamGroup___</remarks>
		/// min 0, max unbounded
		public UserParamType[] userParam
		{
			get { return userParamField.ToArray(); }
			set { userParamField = value.ToList(); }
		}
	}

	/// <summary>
	/// MzIdentML SearchDatabaseType
	/// </summary>
	/// <remarks>A database for searching mass spectra. Examples include a set of amino acid sequence entries, or annotated spectra libraries.</remarks>
	public class SearchDatabaseType : ExternalDataType
	{
		private ParamType databaseNameField;
		private List<CVParamType> cvParamField;
		private string versionField;
		private System.DateTime releaseDateField;
		private bool releaseDateFieldSpecified;
		private long numDatabaseSequencesField;
		private bool numDatabaseSequencesFieldSpecified;
		private long numResiduesField;
		private bool numResiduesFieldSpecified;

		/// <remarks>The database name may be given as a cvParam if it maps exactly to one of the release databases listed in the CV, otherwise a userParam should be used.</remarks>
		/// min 1, max 1
		public ParamType DatabaseName
		{
			get { return databaseNameField; }
			set { databaseNameField = value; }
		}

		/// min 0, max unbounded
		public CVParamType[] cvParam
		{
			get { return cvParamField.ToArray(); }
			set { cvParamField = value.ToList(); }
		}

		/// <remarks>The version of the database.</remarks>
		/// Optional Attribute
		/// string
		public string version
		{
			get { return versionField; }
			set { versionField = value; }
		}

		/// <remarks>The date and time the database was released to the public; omit this attribute when the date and time are unknown or not applicable (e.g. custom databases).</remarks>
		/// Optional Attribute
		/// dateTime
		public System.DateTime releaseDate
		{
			get { return releaseDateField; }
			set { releaseDateField = value; }
		}

		/// Attribute Existence
		public bool releaseDateSpecified
		{
			get { return releaseDateFieldSpecified; }
			set { releaseDateFieldSpecified = value; }
		}

		/// <remarks>The total number of sequences in the database.</remarks>
		/// Optional Attribute
		/// long
		public long numDatabaseSequences
		{
			get { return numDatabaseSequencesField; }
			set { numDatabaseSequencesField = value; }
		}

		/// Attribute Existence
		public bool numDatabaseSequencesSpecified
		{
			get { return numDatabaseSequencesFieldSpecified; }
			set { numDatabaseSequencesFieldSpecified = value; }
		}

		/// <remarks>The number of residues in the database.</remarks>
		/// Optional Attribute
		/// long
		public long numResidues
		{
			get { return numResiduesField; }
			set { numResiduesField = value; }
		}

		/// <remarks></remarks>
		/// Attribute Existence
		public bool numResiduesSpecified
		{
			get { return numResiduesFieldSpecified; }
			set { numResiduesFieldSpecified = value; }
		}
	}

	/// <summary>
	/// MzIdentML ParamType
	/// </summary>
	/// <remarks>Helper type to allow either a cvParam or a userParam to be provided for an element.</remarks>
	public class ParamType
	{
		private AbstractParamType itemField;

		/// min 1, max 1
		public AbstractParamType Item
		{
			get { return itemField; }
			set { itemField = value; }
		}
	}

	/// <summary>
	/// MzIdentML ProteinDetectionProtocolType
	/// </summary>
	/// <remarks>The parameters and settings of a ProteinDetection process.</remarks>
	public class ProteinDetectionProtocolType : IdentifiableType
	{
		private ParamListType analysisParamsField;
		private ParamListType thresholdField;
		private string analysisSoftware_refField;

		/// <remarks>The parameters and settings for the protein detection given as CV terms.</remarks>
		/// min 0, max 1
		public ParamListType AnalysisParams
		{
			get { return analysisParamsField; }
			set { analysisParamsField = value; }
		}

		/// <remarks>The threshold(s) applied to determine that a result is significant. 
		/// If multiple terms are used it is assumed that all conditions are satisfied by the passing results.</remarks>
		/// min 1, max 1
		public ParamListType Threshold
		{
			get { return thresholdField; }
			set { thresholdField = value; }
		}

		/// <remarks>The protein detection software used, given as a reference to the SoftwareCollection section.</remarks>
		/// Required Attribute
		/// string
		public string analysisSoftware_ref
		{
			get { return analysisSoftware_refField; }
			set { analysisSoftware_refField = value; }
		}
	}

	/// <summary>
	/// MzIdentML ParamListType
	/// </summary>
	/// <remarks>Helper type to allow multiple cvParams or userParams to be given for an element.</remarks>
	public class ParamListType
	{
		private List<AbstractParamType> itemsField;

		/// min 1, max unbounded
		public AbstractParamType[] Items
		{
			get { return itemsField.ToArray(); }
			set { itemsField = value.ToList(); }
		}
	}

	/// <summary>
	/// MzIdentML TranslationTableType
	/// </summary>
	/// <remarks>The table used to translate codons into nucleic acids e.g. by reference to the NCBI translation table.</remarks>
	public class TranslationTableType : IdentifiableType
	{
		private List<CVParamType> cvParamField;

		/// <remarks>The details specifying this translation table are captured as cvParams, e.g. translation table, translation 
		/// start codons and translation table description (see specification document and mapping file)</remarks>
		/// min 0, max unbounded
		public CVParamType[] cvParam
		{
			get { return cvParamField.ToArray(); }
			set { cvParamField = value.ToList(); }
		}
	}

	/// <summary>
	/// MzIdentML MassTableType
	/// </summary>
	/// <remarks>The masses of residues used in the search.</remarks>
	public class MassTableType : IdentifiableType
	{
		private List<ResidueType> residueField;
		private List<AmbiguousResidueType> ambiguousResidueField;
		private List<CVParamType> cvParamField;
		private List<UserParamType> userParamField;
		private List<string> msLevelField;

		/// <remarks>The specification of a single residue within the mass table.</remarks>
		/// min 0, max unbounded
		public ResidueType[] Residue
		{
			get { return residueField.ToArray(); }
			set { residueField = value.ToList(); }
		}

		/// min 0, max unbounded
		public AmbiguousResidueType[] AmbiguousResidue
		{
			get { return ambiguousResidueField.ToArray(); }
			set { ambiguousResidueField = value.ToList(); }
		}

		/// <remarks>___ParamGroup___: Additional parameters or descriptors for the MassTable.</remarks>
		/// min 0, max unbounded
		public CVParamType[] cvParam
		{
			get { return cvParamField.ToArray(); }
			set { cvParamField = value.ToList(); }
		}

		/// <remarks>___ParamGroup___</remarks>
		/// min 0, max unbounded
		public UserParamType[] userParam
		{
			get { return userParamField.ToArray(); }
			set { userParamField = value.ToList(); }
		}

		/// <remarks>The MS spectrum that the MassTable refers to e.g. "1" for MS1 "2" for MS2 or "1 2" for MS1 or MS2.</remarks>
		/// Required Attribute
		/// integer(s), space separated
		public string[] msLevel
		{
			get { return msLevelField.ToArray(); }
			set { msLevelField = value.ToList(); }
		}
	}

	/// <summary>
	/// MzIdentML ResidueType
	/// </summary>
	public class ResidueType
	{
		private string codeField;
		private float massField;

		/// <remarks>The single letter code for the residue.</remarks>
		/// Required Attribute
		/// chars, string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}"
		public string code
		{
			get { return codeField; }
			set { codeField = value; }
		}

		/// <remarks>The residue mass in Daltons (not including any fixed modifications).</remarks>
		/// Required Attribute
		/// float
		public float mass
		{
			get { return massField; }
			set { massField = value; }
		}
	}

	/// <summary>
	/// MzIdentML AmbiguousResidueType
	/// </summary>
	/// <remarks>Ambiguous residues e.g. X can be specified by the Code attribute and a set of parameters 
	/// for example giving the different masses that will be used in the search.</remarks>
	public class AmbiguousResidueType
	{
		private List<CVParamType> cvParamField;
		private List<UserParamType> userParamField;
		private string codeField;

		/// <remarks>___ParamGroup___: Parameters for capturing e.g. "alternate single letter codes"</remarks>
		/// min 0, max unbounded
		public CVParamType[] cvParam
		{
			get { return cvParamField.ToArray(); }
			set { cvParamField = value.ToList(); }
		}

		/// <remarks>___ParamGroup___</remarks>
		/// min 0, max unbounded
		public UserParamType[] userParam
		{
			get { return userParamField.ToArray(); }
			set { userParamField = value.ToList(); }
		}

		/// <remarks>The single letter code of the ambiguous residue e.g. X.</remarks>
		/// Required Attribute
		/// chars, string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}"
		public string code
		{
			get { return codeField; }
			set { codeField = value; }
		}
	}

	/// <summary>
	/// MzIdentML EnzymeType
	/// </summary>
	/// <remarks>The details of an individual cleavage enzyme should be provided by giving a regular expression 
	/// or a CV term if a "standard" enzyme cleavage has been performed.</remarks>
	public class EnzymeType : IdentifiableType
	{
		private string siteRegexpField;
		private ParamListType enzymeNameField;
		private string nTermGainField;
		private string cTermGainField;
		private bool semiSpecificField;
		private bool semiSpecificFieldSpecified;
		private int missedCleavagesField;
		private bool missedCleavagesFieldSpecified;
		private int minDistanceField;
		private bool minDistanceFieldSpecified;

		/// <remarks>Regular expression for specifying the enzyme cleavage site.</remarks>
		/// min 0, max 1
		public string SiteRegexp
		{
			get { return siteRegexpField; }
			set { siteRegexpField = value; }
		}

		/// <remarks>The name of the enzyme from a CV.</remarks>
		/// min 0, max 1
		public ParamListType EnzymeName
		{
			get { return enzymeNameField; }
			set { enzymeNameField = value; }
		}

		/// <remarks>Element formula gained at NTerm.</remarks>
		/// Optional Attribute
		/// string, regex: "[A-Za-z0-9 ]+"
		public string nTermGain
		{
			get { return nTermGainField; }
			set { nTermGainField = value; }
		}

		/// <remarks>Element formula gained at CTerm.</remarks>
		/// Optional Attribute
		/// string, regex: "[A-Za-z0-9 ]+"
		public string cTermGain
		{
			get { return cTermGainField; }
			set { cTermGainField = value; }
		}

		/// <remarks>Set to true if the enzyme cleaves semi-specifically (i.e. one terminus must cleave 
		/// according to the rules, the other can cleave at any residue), false if the enzyme cleavage 
		/// is assumed to be specific to both termini (accepting for any missed cleavages).</remarks>
		/// Optional Attribute
		/// boolean
		public bool semiSpecific
		{
			get { return semiSpecificField; }
			set { semiSpecificField = value; }
		}

		/// Attribute Existence
		public bool semiSpecificSpecified
		{
			get { return semiSpecificFieldSpecified; }
			set { semiSpecificFieldSpecified = value; }
		}

		/// <remarks>The number of missed cleavage sites allowed by the search. The attribute must be provided if an enzyme has been used.</remarks>
		/// Optional Attribute
		/// integer
		public int missedCleavages
		{
			get { return missedCleavagesField; }
			set { missedCleavagesField = value; }
		}

		/// Attribute Existence
		public bool missedCleavagesSpecified
		{
			get { return missedCleavagesFieldSpecified; }
			set { missedCleavagesFieldSpecified = value; }
		}

		/// <remarks>Minimal distance for another cleavage (minimum: 1).</remarks>
		/// Optional Attribute
		/// integer >= 1
		public int minDistance
		{
			get { return minDistanceField; }
			set { minDistanceField = value; }
		}

		/// Attribute Existence
		public bool minDistanceSpecified
		{
			get { return minDistanceFieldSpecified; }
			set { minDistanceFieldSpecified = value; }
		}
	}

	/// <summary>
	/// MzIdentML SpectrumIdentificationProtocolType
	/// </summary>
	/// <remarks>The parameters and settings of a SpectrumIdentification analysis.</remarks>
	public class SpectrumIdentificationProtocolType : IdentifiableType
	{
		private ParamType searchTypeField;
		private ParamListType additionalSearchParamsField;
		private List<SearchModificationType> modificationParamsField;
		private EnzymesType enzymesField;
		private List<MassTableType> massTableField;
		private List<CVParamType> fragmentToleranceField;
		private List<CVParamType> parentToleranceField;
		private ParamListType thresholdField;
		private List<FilterType> databaseFiltersField;
		private DatabaseTranslationType databaseTranslationField;
		private string analysisSoftware_refField;

		/// <remarks>The type of search performed e.g. PMF, Tag searches, MS-MS</remarks>
		/// min 1, max 1
		public ParamType SearchType
		{
			get { return searchTypeField; }
			set { searchTypeField = value; }
		}

		/// <remarks>The search parameters other than the modifications searched.</remarks>
		/// min 0, max 1
		public ParamListType AdditionalSearchParams
		{
			get { return additionalSearchParamsField; }
			set { additionalSearchParamsField = value; }
		}

		/// min 0, max 1 : Original ModificationParamsType
		public SearchModificationType[] ModificationParams
		{
			get { return modificationParamsField.ToArray(); }
			set { modificationParamsField = value.ToList(); }
		}

		/// min 0, max 1
		public EnzymesType Enzymes
		{
			get { return enzymesField; }
			set { enzymesField = value; }
		}

		/// min 0, max unbounded
		public MassTableType[] MassTable
		{
			get { return massTableField.ToArray(); }
			set { massTableField = value.ToList(); }
		}

		/// min 0, max 1 : Original ToleranceType
		public CVParamType[] FragmentTolerance
		{
			get { return fragmentToleranceField.ToArray(); }
			set { fragmentToleranceField = value.ToList(); }
		}

		/// min 0, max 1 : Original ToleranceType
		public CVParamType[] ParentTolerance
		{
			get { return parentToleranceField.ToArray(); }
			set { parentToleranceField = value.ToList(); }
		}

		/// <remarks>The threshold(s) applied to determine that a result is significant. If multiple terms are used it is assumed that all conditions are satisfied by the passing results.</remarks>
		/// min 1, max 1
		public ParamListType Threshold
		{
			get { return thresholdField; }
			set { thresholdField = value; }
		}

		/// min 0, max 1 : Original DatabaseFiltersType
		public FilterType[] DatabaseFilters
		{
			get { return databaseFiltersField.ToArray(); }
			set { databaseFiltersField = value.ToList(); }
		}

		/// min 0, max 1
		public DatabaseTranslationType DatabaseTranslation
		{
			get { return databaseTranslationField; }
			set { databaseTranslationField = value; }
		}

		/// <remarks>The search algorithm used, given as a reference to the SoftwareCollection section.</remarks>
		/// Required Attribute
		/// string
		public string analysisSoftware_ref
		{
			get { return analysisSoftware_refField; }
			set { analysisSoftware_refField = value; }
		}
	}

	/// <summary>
	/// MzIdentML SpecificityRulesType
	/// </summary>
	/// <remarks>The specificity rules of the searched modification including for example 
	/// the probability of a modification's presence or peptide or protein termini. Standard 
	/// fixed or variable status should be provided by the attribute fixedMod.</remarks>
	public class SpecificityRulesType
	{
		private List<CVParamType> cvParamField;

		/// min 1, max unbounded
		public CVParamType[] cvParam
		{
			get { return cvParamField.ToArray(); }
			set { cvParamField = value.ToList(); }
		}
	}

	/// <summary>
	/// MzIdentML SearchModificationType: Container ModificationParamsType
	/// </summary>
	/// <remarks>Specification of a search modification as parameter for a spectra search. Contains the name of the 
	/// modification, the mass, the specificity and whether it is a static modification.</remarks>
	/// <remarks>ModificationParamsType: The specification of static/variable modifications (e.g. Oxidation of Methionine) that are to be considered in the spectra search.</remarks>
	/// <remarks>ModificationParamsType: child element SearchModification, of type SearchModificationType, min 1, max unbounded</remarks>
	public class SearchModificationType
	{
		private List<SpecificityRulesType> specificityRulesField;
		private List<CVParamType> cvParamField;
		private bool fixedModField;
		private float massDeltaField;
		private string residuesField;

		/// min 0, max unbounded
		public SpecificityRulesType[] SpecificityRules
		{
			get { return specificityRulesField.ToArray(); }
			set { specificityRulesField = value.ToList(); }
		}

		/// <remarks>The modification is uniquely identified by references to external CVs such as UNIMOD, see 
		/// specification document and mapping file for more details.</remarks>
		/// min 1, max unbounded
		public CVParamType[] cvParam
		{
			get { return cvParamField.ToArray(); }
			set { cvParamField = value.ToList(); }
		}

		/// <remarks>True, if the modification is static (i.e. occurs always).</remarks>
		/// Required Attribute
		/// boolean
		public bool fixedMod
		{
			get { return fixedModField; }
			set { fixedModField = value; }
		}

		/// <remarks>The mass delta of the searched modification in Daltons.</remarks>
		/// Required Attribute
		/// float
		public float massDelta
		{
			get { return massDeltaField; }
			set { massDeltaField = value; }
		}

		/// <remarks>The residue(s) searched with the specified modification. For N or C terminal modifications that can occur 
		/// on any residue, the . character should be used to specify any, otherwise the list of amino acids should be provided.</remarks>
		/// Required Attribute
		/// listOfCharsOrAny: string, space-separated regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}|."
		public string residues
		{
			get { return residuesField; }
			set { residuesField = value; }
		}
	}

	/// <summary>
	/// MzIdentML EnzymesType
	/// </summary>
	/// <remarks>The list of enzymes used in experiment</remarks>
	public class EnzymesType
	{
		private List<EnzymeType> enzymeField;
		private bool independentField;
		private bool independentFieldSpecified;

		/// min 1, max unbounded
		public EnzymeType[] Enzyme
		{
			get { return enzymeField.ToArray(); }
			set { enzymeField = value.ToList(); }
		}

		/// <remarks>If there are multiple enzymes specified, this attribute is set to true if cleavage with different enzymes is performed independently.</remarks>
		/// Optional Attribute
		/// boolean
		public bool independent
		{
			get { return independentField; }
			set { independentField = value; }
		}

		/// Attribute Existence
		public bool independentSpecified
		{
			get { return independentFieldSpecified; }
			set { independentFieldSpecified = value; }
		}
	}

	/// <summary>
	/// MzIdentML FilterType : Containers DatabaseFiltersType
	/// </summary>
	/// <remarks>Filters applied to the search database. The filter must include at least one of Include and Exclude. 
	/// If both are used, it is assumed that inclusion is performed first.</remarks>
	/// <remarks>DatabaseFiltersType: The specification of filters applied to the database searched.</remarks>
	/// <remarks>DatabaseFiltersType: child element Filter, of type FilterType, min 1, max unbounded</remarks>
	public class FilterType
	{
		private ParamType filterType1Field;
		private ParamListType includeField;
		private ParamListType excludeField;

		/// <remarks>The type of filter e.g. database taxonomy filter, pi filter, mw filter</remarks>
		/// min 1, max 1
		public ParamType FilterType1
		{
			get { return filterType1Field; }
			set { filterType1Field = value; }
		}

		/// <remarks>All sequences fulfilling the specifed criteria are included.</remarks>
		/// min 0, max 1
		public ParamListType Include
		{
			get { return includeField; }
			set { includeField = value; }
		}

		/// <remarks>All sequences fulfilling the specifed criteria are excluded.</remarks>
		/// min 0, max 1
		public ParamListType Exclude
		{
			get { return excludeField; }
			set { excludeField = value; }
		}
	}

	/// <summary>
	/// MzIdentML DatabaseTranslationType
	/// </summary>
	/// <remarks>A specification of how a nucleic acid sequence database was translated for searching.</remarks>
	public class DatabaseTranslationType
	{
		private List<TranslationTableType> translationTableField;
		private List<int> framesField;

		/// min 1, max unbounded
		public TranslationTableType[] TranslationTable
		{
			get { return translationTableField.ToArray(); }
			set { translationTableField = value.ToList(); }
		}

		/// <remarks>The frames in which the nucleic acid sequence has been translated as a space separated list</remarks>
		/// Optional Attribute
		/// listOfAllowedFrames: space-separated string, valid values -3, -2, -1, 1, 2, 3
		public int[] frames
		{
			get { return framesField.ToArray(); }
			set { framesField = value.ToList(); }
		}
	}

	/// <summary>
	/// MzIdentML ProtocolApplicationType
	/// </summary>
	/// <remarks>The use of a protocol with the requisite Parameters and ParameterValues. 
	/// ProtocolApplications can take Material or Data (or both) as input and produce Material or Data (or both) as output.</remarks>
	public abstract class ProtocolApplicationType : IdentifiableType
	{
		private System.DateTime activityDateField;
		private bool activityDateFieldSpecified;

		/// <remarks>When the protocol was applied.</remarks>
		/// Optional Attribute
		/// datetime
		public System.DateTime activityDate
		{
			get { return activityDateField; }
			set { activityDateField = value; }
		}

		/// Attribute Existence
		public bool activityDateSpecified
		{
			get { return activityDateFieldSpecified; }
			set { activityDateFieldSpecified = value; }
		}
	}

	/// <summary>
	/// MzIdentML ProteinDetectionType
	/// </summary>
	/// <remarks>An Analysis which assembles a set of peptides (e.g. from a spectra search analysis) to proteins.</remarks>
	public class ProteinDetectionType : ProtocolApplicationType
	{
		private List<InputSpectrumIdentificationsType> inputSpectrumIdentificationsField;
		private string proteinDetectionList_refField;
		private string proteinDetectionProtocol_refField;

		/// min 1, max unbounded
		public InputSpectrumIdentificationsType[] InputSpectrumIdentifications
		{
			get { return inputSpectrumIdentificationsField.ToArray(); }
			set { inputSpectrumIdentificationsField = value.ToList(); }
		}

		/// <remarks>A reference to the ProteinDetectionList in the DataCollection section.</remarks>
		/// Required Attribute
		/// string
		public string proteinDetectionList_ref
		{
			get { return proteinDetectionList_refField; }
			set { proteinDetectionList_refField = value; }
		}

		/// <remarks>A reference to the detection protocol used for this ProteinDetection.</remarks>
		/// Required Attribute
		/// string
		public string proteinDetectionProtocol_ref
		{
			get { return proteinDetectionProtocol_refField; }
			set { proteinDetectionProtocol_refField = value; }
		}
	}

	/// <summary>
	/// MzIdentML InputSpectrumIdentificationsType
	/// </summary>
	/// <remarks>The lists of spectrum identifications that are input to the protein detection process.</remarks>
	public class InputSpectrumIdentificationsType
	{
		private string spectrumIdentificationList_refField;

		/// <remarks>A reference to the list of spectrum identifications that were input to the process.</remarks>
		/// Required Attribute
		/// string
		public string spectrumIdentificationList_ref
		{
			get { return spectrumIdentificationList_refField; }
			set { spectrumIdentificationList_refField = value; }
		}
	}

	/// <summary>
	/// MzIdentML SpectrumIdentificationType
	/// </summary>
	/// <remarks>An Analysis which tries to identify peptides in input spectra, referencing the database searched, 
	/// the input spectra, the output results and the protocol that is run.</remarks>
	public class SpectrumIdentificationType : ProtocolApplicationType
	{
		private List<InputSpectraType> inputSpectraField;
		private List<SearchDatabaseRefType> searchDatabaseRefField;
		private string spectrumIdentificationProtocol_refField;
		private string spectrumIdentificationList_refField;

		/// <remarks>One of the spectra data sets used.</remarks>
		/// min 1, max unbounded
		public InputSpectraType[] InputSpectra
		{
			get { return inputSpectraField.ToArray(); }
			set { inputSpectraField = value.ToList(); }
		}

		/// min 1, max unbounded
		public SearchDatabaseRefType[] SearchDatabaseRef
		{
			get { return searchDatabaseRefField.ToArray(); }
			set { searchDatabaseRefField = value.ToList(); }
		}

		/// <remarks>A reference to the search protocol used for this SpectrumIdentification.</remarks>
		/// Required Attribute
		/// string
		public string spectrumIdentificationProtocol_ref
		{
			get { return spectrumIdentificationProtocol_refField; }
			set { spectrumIdentificationProtocol_refField = value; }
		}

		/// <remarks>A reference to the SpectrumIdentificationList produced by this analysis in the DataCollection section.</remarks>
		/// Required Attribute
		/// string
		public string spectrumIdentificationList_ref
		{
			get { return spectrumIdentificationList_refField; }
			set { spectrumIdentificationList_refField = value; }
		}
	}

	/// <summary>
	/// MzIdentML InputSpectraType
	/// </summary>
	/// <remarks>The attribute referencing an identifier within the SpectraData section.</remarks>
	public class InputSpectraType
	{
		private string spectraData_refField;

		/// <remarks>A reference to the SpectraData element which locates the input spectra to an external file.</remarks>
		/// Optional Attribute
		/// string
		public string spectraData_ref
		{
			get { return spectraData_refField; }
			set { spectraData_refField = value; }
		}
	}

	/// <summary>
	/// MzIdentML SearchDatabaseRefType
	/// </summary>
	/// <remarks>One of the search databases used.</remarks>
	public class SearchDatabaseRefType
	{
		private string searchDatabase_refField;

		/// <remarks>A reference to the database searched.</remarks>
		/// Optional Attribute
		/// string
		public string searchDatabase_ref
		{
			get { return searchDatabase_refField; }
			set { searchDatabase_refField = value; }
		}
	}

	/// <summary>
	/// MzIdentML PeptideEvidenceType
	/// </summary>
	/// <remarks>PeptideEvidence links a specific Peptide element to a specific position in a DBSequence. 
	/// There must only be one PeptideEvidence item per Peptide-to-DBSequence-position.</remarks>
	public class PeptideEvidenceType : IdentifiableType
	{
		private List<CVParamType> cvParamField;
		private List<UserParamType> userParamField;
		private string dBSequence_refField;
		private string peptide_refField;
		private int startField;
		private bool startFieldSpecified;
		private int endField;
		private bool endFieldSpecified;
		private string preField;
		private string postField;
		private string translationTable_refField;
		private int frameField;
		private bool frameFieldSpecified;
		private bool isDecoyField;

		public PeptideEvidenceType()
		{
			isDecoyField = false;
		}

		/// <remarks>___ParamGroup___: Additional parameters or descriptors for the PeptideEvidence.</remarks>
		/// min 0, max unbounded
		public CVParamType[] cvParam
		{
			get { return cvParamField.ToArray(); }
			set { cvParamField = value.ToList(); }
		}

		/// <remarks>___ParamGroup___</remarks>
		/// min 0, max unbounded
		public UserParamType[] userParam
		{
			get { return userParamField.ToArray(); }
			set { userParamField = value.ToList(); }
		}

		/// <remarks>Set to true if the peptide is matched to a decoy sequence.</remarks>
		/// Optional Attribute
		/// boolean, default false
		public bool isDecoy
		{
			get { return isDecoyField; }
			set { isDecoyField = value; }
		}

		/// <remarks>Previous flanking residue. If the peptide is N-terminal, pre="-" and not pre="". 
		/// If for any reason it is unknown (e.g. denovo), pre="?" should be used.</remarks>
		/// Optional Attribute
		/// string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"
		public string pre
		{
			get { return preField; }
			set { preField = value; }
		}

		/// <remarks>Post flanking residue. If the peptide is C-terminal, post="-" and not post="". If for any reason it is unknown (e.g. denovo), post="?" should be used.</remarks>
		/// Optional Attribute
		/// string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"
		public string post
		{
			get { return postField; }
			set { postField = value; }
		}

		/// <remarks>Start position of the peptide inside the protein sequence, where the first amino acid of the 
		/// protein sequence is position 1. Must be provided unless this is a de novo search.</remarks>
		/// Optional Attribute
		/// integer
		public int start
		{
			get { return startField; }
			set { startField = value; }
		}

		/// Attribute Existence
		public bool startSpecified
		{
			get { return startFieldSpecified; }
			set { startFieldSpecified = value; }
		}

		/// <remarks>The index position of the last amino acid of the peptide inside the protein sequence, where the first 
		/// amino acid of the protein sequence is position 1. Must be provided unless this is a de novo search.</remarks>
		/// Optional Attribute
		/// integer
		public int end
		{
			get { return endField; }
			set { endField = value; }
		}

		/// Attribute Existence
		public bool endSpecified
		{
			get { return endFieldSpecified; }
			set { endFieldSpecified = value; }
		}

		/// <remarks>A reference to the translation table used if this is PeptideEvidence derived from nucleic acid sequence</remarks>
		/// Optional Attribute
		/// string
		public string translationTable_ref
		{
			get { return translationTable_refField; }
			set { translationTable_refField = value; }
		}

		/// <remarks>The translation frame of this sequence if this is PeptideEvidence derived from nucleic acid sequence</remarks>
		/// Optional Attribute
		/// "Allowed Frames", int: -3, -2, -1, 1, 2, 3 
		public int frame
		{
			get { return frameField; }
			set { frameField = value; }
		}

		/// Attribute Existence
		public bool frameSpecified
		{
			get { return frameFieldSpecified; }
			set { frameFieldSpecified = value; }
		}

		/// <remarks>A reference to the identified (poly)peptide sequence in the Peptide element.</remarks>
		/// Required Attribute
		/// string
		public string peptide_ref
		{
			get { return peptide_refField; }
			set { peptide_refField = value; }
		}

		/// <remarks>A reference to the protein sequence in which the specified peptide has been linked.</remarks>
		/// Required Attribute
		/// string
		public string dBSequence_ref
		{
			get { return dBSequence_refField; }
			set { dBSequence_refField = value; }
		}
	}

	/// <summary>
	/// MzIdentML PeptideType
	/// </summary>
	/// <remarks>One (poly)peptide (a sequence with modifications). The combination of Peptide sequence and modifications must be unique in the file.</remarks>
	public class PeptideType : IdentifiableType
	{
		private string peptideSequenceField;
		private List<ModificationType> modificationField;
		private List<SubstitutionModificationType> substitutionModificationField;
		private List<CVParamType> cvParamField;
		private List<UserParamType> userParamField;

		/// <remarks>The amino acid sequence of the (poly)peptide. If a substitution modification has been found, the original sequence should be reported.</remarks>
		/// min 1, max 1
		public string PeptideSequence
		{
			get { return peptideSequenceField; }
			set { peptideSequenceField = value; }
		}

		/// min 0, max unbounded
		public ModificationType[] Modification
		{
			get { return modificationField.ToArray(); }
			set { modificationField = value.ToList(); }
		}

		/// min 0, max unbounded
		public SubstitutionModificationType[] SubstitutionModification
		{
			get { return substitutionModificationField.ToArray(); }
			set { substitutionModificationField = value.ToList(); }
		}

		/// <remarks>___ParamGroup___: Additional descriptors of this peptide sequence</remarks>
		/// min 0, max unbounded
		public CVParamType[] cvParam
		{
			get { return cvParamField.ToArray(); }
			set { cvParamField = value.ToList(); }
		}

		/// <remarks>___ParamGroup___</remarks>
		/// min 0, max unbounded
		public UserParamType[] userParam
		{
			get { return userParamField.ToArray(); }
			set { userParamField = value.ToList(); }
		}
	}

	/// <summary>
	/// MzIdentML ModificationType
	/// </summary>
	/// <remarks>A molecule modification specification. If n modifications have been found on a peptide, there should 
	/// be n instances of Modification. If multiple modifications are provided as cvParams, it is assumed that the 
	/// modification is ambiguous i.e. one modification or another. A cvParam must be provided with the identification 
	/// of the modification sourced from a suitable CV e.g. UNIMOD. If the modification is not present in the CV (and 
	/// this will be checked by the semantic validator within a given tolerance window), there is a â€œunknown 
	/// modificationâ€ CV term that must be used instead. A neutral loss should be defined as an additional CVParam 
	/// within Modification. If more complex information should be given about neutral losses (such as presence/absence 
	/// on particular product ions), this can additionally be encoded within the FragmentationArray.</remarks>
	public class ModificationType
	{
		private List<CVParamType> cvParamField;
		private int locationField;
		private bool locationFieldSpecified;
		private List<string> residuesField;
		private double avgMassDeltaField;
		private bool avgMassDeltaFieldSpecified;
		private double monoisotopicMassDeltaField;
		private bool monoisotopicMassDeltaFieldSpecified;

		/// <remarks>CV terms capturing the modification, sourced from an appropriate controlled vocabulary.</remarks>
		/// min 1, max unbounded
		public CVParamType[] cvParam
		{
			get { return cvParamField.ToArray(); }
			set { cvParamField = value.ToList(); }
		}

		/// <remarks>Location of the modification within the peptide - position in peptide sequence, counted from 
		/// the N-terminus residue, starting at position 1. Specific modifications to the N-terminus should be 
		/// given the location 0. Modification to the C-terminus should be given as peptide length + 1. If the 
		/// modification location is unknown e.g. for PMF data, this attribute should be omitted.</remarks>
		/// Optional Attribute
		/// integer
		public int location
		{
			get { return locationField; }
			set { locationField = value; }
		}

		/// Attribute Existence
		public bool locationSpecified
		{
			get { return locationFieldSpecified; }
			set { locationFieldSpecified = value; }
		}

		/// <remarks>Specification of the residue (amino acid) on which the modification occurs. If multiple values 
		/// are given, it is assumed that the exact residue modified is unknown i.e. the modification is to ONE of 
		/// the residues listed. Multiple residues would usually only be specified for PMF data.</remarks>
		/// Optional Attribute
		/// listOfChars, string, space-separated regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}"
		public string[] residues
		{
			get { return residuesField.ToArray(); }
			set { residuesField = value.ToList(); }
		}

		/// <remarks>Atomic mass delta considering the natural distribution of isotopes in Daltons.</remarks>
		/// Optional Attribute
		/// double
		public double avgMassDelta
		{
			get { return avgMassDeltaField; }
			set { avgMassDeltaField = value; }
		}

		/// Attribute Existence
		public bool avgMassDeltaSpecified
		{
			get { return avgMassDeltaFieldSpecified; }
			set { avgMassDeltaFieldSpecified = value; }
		}

		/// <remarks>Atomic mass delta when assuming only the most common isotope of elements in Daltons.</remarks>
		/// Optional Attribute
		/// double
		public double monoisotopicMassDelta
		{
			get { return monoisotopicMassDeltaField; }
			set { monoisotopicMassDeltaField = value; }
		}

		/// Attribute Existence
		public bool monoisotopicMassDeltaSpecified
		{
			get { return monoisotopicMassDeltaFieldSpecified; }
			set { monoisotopicMassDeltaFieldSpecified = value; }
		}
	}

	/// <summary>
	/// MzIdentML SubstitutionModificationType
	/// </summary>
	/// <remarks>A modification where one residue is substituted by another (amino acid change).</remarks>
	public class SubstitutionModificationType
	{
		private string originalResidueField;
		private string replacementResidueField;
		private int locationField;
		private bool locationFieldSpecified;
		private double avgMassDeltaField;
		private bool avgMassDeltaFieldSpecified;
		private double monoisotopicMassDeltaField;
		private bool monoisotopicMassDeltaFieldSpecified;

		/// <remarks>The original residue before replacement.</remarks>
		/// Required Attribute
		/// string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"
		public string originalResidue
		{
			get { return originalResidueField; }
			set { originalResidueField = value; }
		}

		/// <remarks>The residue that replaced the originalResidue.</remarks>
		/// Required Attribute
		/// string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"
		public string replacementResidue
		{
			get { return replacementResidueField; }
			set { replacementResidueField = value; }
		}

		/// <remarks>Location of the modification within the peptide - position in peptide sequence, counted from the N-terminus residue, starting at position 1. 
		/// Specific modifications to the N-terminus should be given the location 0. 
		/// Modification to the C-terminus should be given as peptide length + 1.</remarks>
		/// Optional Attribute
		/// integer
		public int location
		{
			get { return locationField; }
			set { locationField = value; }
		}

		/// Attribute Existence
		public bool locationSpecified
		{
			get { return locationFieldSpecified; }
			set { locationFieldSpecified = value; }
		}

		/// <remarks>Atomic mass delta considering the natural distribution of isotopes in Daltons. 
		/// This should only be reported if the original amino acid is known i.e. it is not "X"</remarks>
		/// Optional Attribute
		/// double
		public double avgMassDelta
		{
			get { return avgMassDeltaField; }
			set { avgMassDeltaField = value; }
		}

		/// Attribute Existence
		public bool avgMassDeltaSpecified
		{
			get { return avgMassDeltaFieldSpecified; }
			set { avgMassDeltaFieldSpecified = value; }
		}

		/// <remarks>Atomic mass delta when assuming only the most common isotope of elements in Daltons. 
		/// This should only be reported if the original amino acid is known i.e. it is not "X"</remarks>
		/// Optional Attribute
		/// double
		public double monoisotopicMassDelta
		{
			get { return monoisotopicMassDeltaField; }
			set { monoisotopicMassDeltaField = value; }
		}

		/// Attribute Existence
		public bool monoisotopicMassDeltaSpecified
		{
			get { return monoisotopicMassDeltaFieldSpecified; }
			set { monoisotopicMassDeltaFieldSpecified = value; }
		}
	}

	/// <summary>
	/// MzIdentML DBSequenceType
	/// </summary>
	/// <remarks>A database sequence from the specified SearchDatabase (nucleic acid or amino acid). 
	/// If the sequence is nucleic acid, the source nucleic acid sequence should be given in 
	/// the seq attribute rather than a translated sequence.</remarks>
	public class DBSequenceType : IdentifiableType
	{
		private string seqField;
		private List<CVParamType> cvParamField;
		private List<UserParamType> userParamField;
		private int lengthField;
		private bool lengthFieldSpecified;
		private string searchDatabase_refField;
		private string accessionField;

		/// <remarks>The actual sequence of amino acids or nucleic acid.</remarks>
		/// min 0, max 1
		/// string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]*"
		public string Seq
		{
			get { return seqField; }
			set { seqField = value; }
		}

		/// <remarks>___ParamGroup___: Additional descriptors for the sequence, such as taxon, description line etc.</remarks>
		/// min 0, max unbounded
		public CVParamType[] cvParam
		{
			get { return cvParamField.ToArray(); }
			set { cvParamField = value.ToList(); }
		}

		/// <remarks>___ParamGroup___</remarks>
		/// min 0, max unbounded
		public UserParamType[] userParam
		{
			get { return userParamField.ToArray(); }
			set { userParamField = value.ToList(); }
		}

		/// <remarks>The unique accession of this sequence.</remarks>
		/// Required Attribute
		public string accession
		{
			get { return accessionField; }
			set { accessionField = value; }
		}

		/// <remarks>The source database of this sequence.</remarks>
		/// Required Attribute
		/// string
		public string searchDatabase_ref
		{
			get { return searchDatabase_refField; }
			set { searchDatabase_refField = value; }
		}

		/// <remarks>The length of the sequence as a number of bases or residues.</remarks>
		/// Optional Attribute
		/// integer
		public int length
		{
			get { return lengthField; }
			set { lengthField = value; }
		}

		/// Attribute Existence
		public bool lengthSpecified
		{
			get { return lengthFieldSpecified; }
			set { lengthFieldSpecified = value; }
		}
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
	public class SampleType : IdentifiableType
	{
		private List<ContactRoleType> contactRoleField;
		private List<SubSampleType> subSampleField;
		private List<CVParamType> cvParamField;
		private List<UserParamType> userParamField;

		/// <remarks>Contact details for the Material. The association to ContactRole could specify, for example, the creator or provider of the Material.</remarks>
		/// min 0, max unbounded
		public ContactRoleType[] ContactRole
		{
			get { return contactRoleField.ToArray(); }
			set { contactRoleField = value.ToList(); }
		}

		/// min 0, max unbounded
		public SubSampleType[] SubSample
		{
			get { return subSampleField.ToArray(); }
			set { subSampleField = value.ToList(); }
		}

		/// <remarks>___ParamGroup___: The characteristics of a Material.</remarks>
		/// min 0, max unbounded
		public CVParamType[] cvParam
		{
			get { return cvParamField.ToArray(); }
			set { cvParamField = value.ToList(); }
		}

		/// <remarks>___ParamGroup___</remarks>
		/// min 0, max unbounded
		public UserParamType[] userParam
		{
			get { return userParamField.ToArray(); }
			set { userParamField = value.ToList(); }
		}
	}

	/// <summary>
	/// MzIdentML ContactRoleType
	/// </summary>
	/// <remarks>The role that a Contact plays in an organization or with respect to the associating class. 
	/// A Contact may have several Roles within scope, and as such, associations to ContactRole 
	/// allow the use of a Contact in a certain manner. Examples might include a provider, or a data analyst.</remarks>
	public class ContactRoleType
	{
		private RoleType roleField;
		private string contact_refField;

		// min 1, max 1
		public RoleType Role
		{
			get { return roleField; }
			set { roleField = value; }
		}

		/// <remarks>When a ContactRole is used, it specifies which Contact the role is associated with.</remarks>
		/// Required Attribute
		/// string
		public string contact_ref
		{
			get { return contact_refField; }
			set { contact_refField = value; }
		}
	}

	/// <summary>
	/// MzIdentML RoleType
	/// </summary>
	/// <remarks>The roles (lab equipment sales, contractor, etc.) the Contact fills.</remarks>
	public class RoleType
	{
		private CVParamType cvParamField;

		/// <remarks>CV term for contact roles, such as software provider.</remarks>
		/// min 1, max 1
		public CVParamType cvParam
		{
			get { return cvParamField; }
			set { cvParamField = value; }
		}
	}

	/// <summary>
	/// MzIdentML SubSampleType
	/// </summary>
	/// <remarks>References to the individual component samples within a mixed parent sample.</remarks>
	public class SubSampleType
	{
		private string sample_refField;

		/// <remarks>A reference to the child sample.</remarks>
		/// Required  Attribute
		/// string
		public string sample_ref
		{
			get { return sample_refField; }
			set { sample_refField = value; }
		}
	}

	/// <summary>
	/// MzIdentML AuditCollectionType; Also C# representation of AuditCollectionType
	/// </summary>
	/// <remarks>A contact is either a person or an organization.</remarks>
	/// <remarks>AuditCollectionType: The complete set of Contacts (people and organisations) for this file.</remarks>
	/// <remarks>AuditCollectionType: min 1, max unbounded, for PersonType XOR OrganizationType</remarks>
	public abstract class AbstractContactType : IdentifiableType
	{
		private List<CVParamType> cvParamField;
		private List<UserParamType> userParamField;

		/// <remarks>___ParamGroup___: Attributes of this contact such as address, email, telephone etc.</remarks>
		/// min 0, max unbounded
		public CVParamType[] cvParam
		{
			get { return cvParamField.ToArray(); }
			set { cvParamField = value.ToList(); }
		}

		/// <remarks>___ParamGroup___</remarks>
		/// min 0, max unbounded
		public UserParamType[] userParam
		{
			get { return userParamField.ToArray(); }
			set { userParamField = value.ToList(); }
		}
	}

	/// <summary>
	/// MzIdentML OrganizationType
	/// </summary>
	/// <remarks>Organizations are entities like companies, universities, government agencies. 
	/// Any additional information such as the address, email etc. should be supplied either as CV parameters or as user parameters.</remarks>
	public class OrganizationType : AbstractContactType
	{
		private ParentOrganizationType parentField;

		/// min 0, max 1
		public ParentOrganizationType Parent
		{
			get { return parentField; }
			set { parentField = value; }
		}
	}

	/// <summary>
	/// MzIdentML ParentOrganizationType
	/// </summary>
	/// <remarks>The containing organization (the university or business which a lab belongs to, etc.)</remarks>
	public class ParentOrganizationType
	{
		private string organization_refField;

		/// <remarks>A reference to the organization this contact belongs to.</remarks>
		/// Required Attribute
		/// string
		public string organization_ref
		{
			get { return organization_refField; }
			set { organization_refField = value; }
		}
	}

	/// <summary>
	/// MzIdentML PersonType
	/// </summary>
	/// <remarks>A person's name and contact details. Any additional information such as the address, 
	/// contact email etc. should be supplied using CV parameters or user parameters.</remarks>
	public class PersonType : AbstractContactType
	{
		private List<AffiliationType> affiliationField;
		private string lastNameField;
		private string firstNameField;
		private string midInitialsField;

		/// <remarks>The organization a person belongs to.</remarks>
		/// min 0, max unbounded
		public AffiliationType[] Affiliation
		{
			get { return affiliationField.ToArray(); }
			set { affiliationField = value.ToList(); }
		}

		/// <remarks>The Person's last/family name.</remarks>
		/// Optional Attribute
		public string lastName
		{
			get { return lastNameField; }
			set { lastNameField = value; }
		}

		/// <remarks>The Person's first name.</remarks>
		/// Optional Attribute
		public string firstName
		{
			get { return firstNameField; }
			set { firstNameField = value; }
		}

		/// <remarks>The Person's middle initial.</remarks>
		/// Optional Attribute
		public string midInitials
		{
			get { return midInitialsField; }
			set { midInitialsField = value; }
		}
	}

	/// <summary>
	/// MzIdentML AffiliationType
	/// </summary>
	public class AffiliationType
	{
		private string organization_refField;

		/// <remarks>>A reference to the organization this contact belongs to.</remarks>
		/// Required Attribute
		/// string
		public string organization_ref
		{
			get { return organization_refField; }
			set { organization_refField = value; }
		}
	}

	/// <summary>
	/// MzIdentML ProviderType
	/// </summary>
	/// <remarks>The provider of the document in terms of the Contact and the software the produced the document instance.</remarks>
	public class ProviderType : IdentifiableType
	{
		private ContactRoleType contactRoleField;
		private string analysisSoftware_refField;

		/// <remarks>The Contact that provided the document instance.</remarks>
		/// min 0, max 1
		public ContactRoleType ContactRole
		{
			get { return contactRoleField; }
			set { contactRoleField = value; }
		}

		/// <remarks>The Software that produced the document instance.</remarks>
		/// Optional Attribute
		/// string
		public string analysisSoftware_ref
		{
			get { return analysisSoftware_refField; }
			set { analysisSoftware_refField = value; }
		}
	}

	/// <summary>
	/// MzIdentML AnalysisSoftwareType : Containers AnalysisSoftwareListType
	/// </summary>
	/// <remarks>The software used for performing the analyses.</remarks>
	/// 
	/// <remarks>AnalysisSoftwareListType: The software packages used to perform the analyses.</remarks>
	/// <remarks>AnalysisSoftwareListType: child element AnalysisSoftware of type AnalysisSoftwareType, min 1, max unbounded</remarks>
	public class AnalysisSoftwareType : IdentifiableType
	{
		private ContactRoleType contactRoleField;
		private ParamType softwareNameField;
		private string customizationsField;
		private string versionField;
		private string uriField;

		/// <remarks>The contact details of the organisation or person that produced the software</remarks>
		/// min 0, max 1
		public ContactRoleType ContactRole
		{
			get { return contactRoleField; }
			set { contactRoleField = value; }
		}

		/// <remarks>The name of the analysis software package, sourced from a CV if available.</remarks>
		/// min 1, max 1
		public ParamType SoftwareName
		{
			get { return softwareNameField; }
			set { softwareNameField = value; }
		}

		/// <remarks>Any customizations to the software, such as alternative scoring mechanisms implemented, should be documented here as free text.</remarks>
		/// min 0, max 1
		public string Customizations
		{
			get { return customizationsField; }
			set { customizationsField = value; }
		}

		/// <remarks>The version of Software used.</remarks>
		/// Optional Attribute
		/// string
		public string version
		{
			get { return versionField; }
			set { versionField = value; }
		}

		/// <remarks>URI of the analysis software e.g. manufacturer's website</remarks>
		/// Optional Attribute
		/// anyURI
		public string uri
		{
			get { return uriField; }
			set { uriField = value; }
		}
	}

	/// <summary>
	/// MzIdentML InputsType
	/// </summary>
	/// <remarks>The inputs to the analyses including the databases searched, the spectral data and the source file converted to mzIdentML.</remarks>
	public class InputsType
	{
		private List<SourceFileType> sourceFileField;
		private List<SearchDatabaseType> searchDatabaseField;
		private List<SpectraDataType> spectraDataField;

		/// min 0, max unbounded
		public SourceFileType[] SourceFile
		{
			get { return sourceFileField.ToArray(); }
			set { sourceFileField = value.ToList(); }
		}

		/// min 0, max unbounded
		public SearchDatabaseType[] SearchDatabase
		{
			get { return searchDatabaseField.ToArray(); }
			set { searchDatabaseField = value.ToList(); }
		}

		/// min 1, max unbounde
		public SpectraDataType[] SpectraData
		{
			get { return spectraDataField.ToArray(); }
			set { spectraDataField = value.ToList(); }
		}
	}

	/// <summary>
	/// MzIdentML DataCollectionType
	/// </summary>
	/// <remarks>The collection of input and output data sets of the analyses.</remarks>
	public class DataCollectionType
	{
		private InputsType inputsField;
		private AnalysisDataType analysisDataField;

		/// min 1, max 1
		public InputsType Inputs
		{
			get { return inputsField; }
			set { inputsField = value; }
		}

		/// min 1, max 1
		public AnalysisDataType AnalysisData
		{
			get { return analysisDataField; }
			set { analysisDataField = value; }
		}
	}

	/// <summary>
	/// MzIdentML AnalysisProtocolCollectionType
	/// </summary>
	/// <remarks>The collection of protocols which include the parameters and settings of the performed analyses.</remarks>
	public class AnalysisProtocolCollectionType
	{
		private List<SpectrumIdentificationProtocolType> spectrumIdentificationProtocolField;
		private ProteinDetectionProtocolType proteinDetectionProtocolField;

		/// min 1, max unbounded
		public SpectrumIdentificationProtocolType[] SpectrumIdentificationProtocol
		{
			get { return spectrumIdentificationProtocolField.ToArray(); }
			set { spectrumIdentificationProtocolField = value.ToList(); }
		}

		/// min 0, max 1
		public ProteinDetectionProtocolType ProteinDetectionProtocol
		{
			get { return proteinDetectionProtocolField; }
			set { proteinDetectionProtocolField = value; }
		}
	}

	/// <summary>
	/// MzIdentML AnalysisCollectionType
	/// </summary>
	/// <remarks>The analyses performed to get the results, which map the input and output data sets. 
	/// Analyses are for example: SpectrumIdentification (resulting in peptides) or ProteinDetection (assemble proteins from peptides).</remarks>
	public class AnalysisCollectionType
	{
		private List<SpectrumIdentificationType> spectrumIdentificationField;
		private ProteinDetectionType proteinDetectionField;

		/// min 1, max unbounded
		public SpectrumIdentificationType[] SpectrumIdentification
		{
			get { return spectrumIdentificationField.ToArray(); }
			set { spectrumIdentificationField = value.ToList(); }
		}

		/// min 0, max 1
		public ProteinDetectionType ProteinDetection
		{
			get { return proteinDetectionField; }
			set { proteinDetectionField = value; }
		}
	}

	/// <summary>
	/// MzIdentML SequenceCollectionType
	/// </summary>
	/// <remarks>The collection of sequences (DBSequence or Peptide) identified and their relationship between 
	/// each other (PeptideEvidence) to be referenced elsewhere in the results.</remarks>
	public class SequenceCollectionType
	{
		private List<DBSequenceType> dBSequenceField;
		private List<PeptideType> peptideField;
		private List<PeptideEvidenceType> peptideEvidenceField;

		/// min 1, max unbounded
		public DBSequenceType[] DBSequence
		{
			get { return dBSequenceField.ToArray(); }
			set { dBSequenceField = value.ToList(); }
		}

		/// min 0, max unbounded
		public PeptideType[] Peptide
		{
			get { return peptideField.ToArray(); }
			set { peptideField = value.ToList(); }
		}

		/// min 0, max unbounded
		public PeptideEvidenceType[] PeptideEvidence
		{
			get { return peptideEvidenceField.ToArray(); }
			set { peptideEvidenceField = value.ToList(); }
		}
	}
}
