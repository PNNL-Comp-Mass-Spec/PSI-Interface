using System.Collections.Generic;
using PSI_Interface.CV;

namespace PSI_Interface.IdentData
{
    /// <summary>
    /// MzIdentML MzIdentMLType
    /// </summary>
    /// <remarks>The upper-most hierarchy level of mzIdentML with sub-containers for example describing software, 
    /// protocols and search results (spectrum identifications or protein detection results).</remarks>
    public partial class IdentData : IIdentifiableType
    {
        internal CVTranslator CvTranslator; // = new CVTranslator(); // Create a generic translator by default; must be re-mapped when reading a file
        private IdentDataList<CVInfo> _cvList;
        private IdentDataList<AnalysisSoftwareInfo> _analysisSoftwareList;
        private ProviderInfo _provider;
        private IdentDataList<AbstractContactInfo> _auditCollection;
        private IdentDataList<SampleInfo> _analysisSampleCollection;
        private SequenceCollection _sequenceCollection;
        private AnalysisCollection _analysisCollection;
        private AnalysisProtocolCollection _analysisProtocolCollection;
        private DataCollection _dataCollection;
        private IdentDataList<BibliographicReference> _bibliographicReference;
        private System.DateTime _creationDate;
        private string _version;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// min 1, max 1
        public IdentDataList<CVInfo> CVList
        {
            get { return this._cvList; }
            set
            {
                this._cvList = value;
                if (this._cvList != null)
                {
                    this._cvList.IdentData = this;
                    this.CvTranslator = new CVTranslator(this._cvList);
                }
                else
                {
                    this.CvTranslator = new CVTranslator();
                }
            }
        }

        /// min 0, max 1
        public IdentDataList<AnalysisSoftwareInfo> AnalysisSoftwareList
        {
            get { return this._analysisSoftwareList; }
            set
            {
                this._analysisSoftwareList = value;
                if (this._analysisSoftwareList != null)
                {
                    this._analysisSoftwareList.IdentData = this;
                }
            }
        }

        /// <remarks>The Provider of the mzIdentML record in terms of the contact and software.</remarks>
        /// min 0, max 1
        public ProviderInfo Provider
        {
            get { return this._provider; }
            set
            {
                this._provider = value;
                if (this._provider != null)
                {
                    this._provider.IdentData = this;
                }
            }
        }

        /// min 0, max 1
        public IdentDataList<AbstractContactInfo> AuditCollection
        {
            get { return this._auditCollection; }
            set
            {
                this._auditCollection = value;
                if (this._auditCollection != null)
                {
                    this._auditCollection.IdentData = this;
                }
            }
        }

        /// min 0, max 1
        public IdentDataList<SampleInfo> AnalysisSampleCollection
        {
            get { return this._analysisSampleCollection; }
            set
            {
                this._analysisSampleCollection = value;
                if (this._analysisSampleCollection != null)
                {
                    this._analysisSampleCollection.IdentData = this;
                }
            }
        }

        /// min 0, max 1
        public SequenceCollection SequenceCollection
        {
            get { return this._sequenceCollection; }
            set
            {
                this._sequenceCollection = value;
                if (this._sequenceCollection != null)
                {
                    this._sequenceCollection.IdentData = this;
                }
            }
        }

        /// min 1, max 1
        public AnalysisCollection AnalysisCollection
        {
            get { return this._analysisCollection; }
            set
            {
                this._analysisCollection = value;
                if (this._analysisCollection != null)
                {
                    this._analysisCollection.IdentData = this;
                }
            }
        }

        /// min 1, max 1
        public AnalysisProtocolCollection AnalysisProtocolCollection
        {
            get { return this._analysisProtocolCollection; }
            set
            {
                this._analysisProtocolCollection = value;
                if (this._analysisProtocolCollection != null)
                {
                    this._analysisProtocolCollection.IdentData = this;
                }
            }
        }

        /// min 1, max 1
        public DataCollection DataCollection
        {
            get { return this._dataCollection; }
            set
            {
                this._dataCollection = value;
                if (this._dataCollection != null)
                {
                    this._dataCollection.IdentData = this;
                }
            }
        }

        /// <remarks>Any bibliographic references associated with the file</remarks>
        /// min 0, max unbounded
        public IdentDataList<BibliographicReference> BibliographicReferences
        {
            get { return this._bibliographicReference; }
            set
            {
                this._bibliographicReference = value;
                if (this._bibliographicReference != null)
                {
                    this._bibliographicReference.IdentData = this;
                }
            }
        }

        /// <remarks>The date on which the file was produced.</remarks>
        /// Optional Attribute
        /// dataTime
        public System.DateTime CreationDate
        {
            get { return this._creationDate; }
            set
            {
                this._creationDate = value;
                this.CreationDateSpecified = true;
            }
        }

        /// Attribute Existence
        public bool CreationDateSpecified { get; private set; }

        /// <remarks>The version of the schema this instance document refers to, in the format x.y.z. 
        /// Changes to z should not affect prevent instance documents from validating.</remarks>
        /// Required Attribute
        /// string, regex: "(1\.1\.\d+)"
        public string Version
        {
            get { return this._version; }
            set { this._version = value; }
        }
    }

    /// <summary>
    /// MzIdentML cvType : Container CVListType
    /// </summary>
    /// <remarks>A source controlled vocabulary from which cvParams will be obtained.</remarks>
    /// 
    /// <remarks>CVListType: The list of controlled vocabularies used in the file.</remarks>
    /// <remarks>CVListType: child element cv of type cvType, min 1, max unbounded</remarks>
    public partial class CVInfo : IdentDataInternalTypeAbstract
    {
        /// <remarks>The full name of the CV.</remarks>
        /// Required Attribute
        /// string
        public string FullName { get; set; }

        /// <remarks>The version of the CV.</remarks>
        /// Optional Attribute
        /// string
        public string Version { get; set; }

        /// <remarks>The URI of the source CV.</remarks>
        /// Required Attribute
        /// anyURI
        public string URI { get; set; }

        /// <remarks>The unique identifier of this cv within the document to be referenced by cvParam elements.</remarks>
        /// Required Attribute
        /// string
        public string Id { get; set; }
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationItemRefType
    /// </summary>
    /// <remarks>Reference(s) to the SpectrumIdentificationItem element(s) that support the given PeptideEvidence element. 
    /// Using these references it is possible to indicate which spectra were actually accepted as evidence for this 
    /// peptide identification in the given protein.</remarks>
    public partial class SpectrumIdentificationItemRefInfo : IdentDataInternalTypeAbstract
    {
        private string _spectrumIdentificationItemRef;

        /// <remarks>A reference to the SpectrumIdentificationItem element(s).</remarks>
        /// Required Attribute
        /// string
        public string SpectrumIdentificationItemRef
        {
            get { return this._spectrumIdentificationItemRef; }
            set { _spectrumIdentificationItemRef = value; }
        }
    }

    /// <summary>
    /// MzIdentML PeptideHypothesisType
    /// </summary>
    /// <remarks>Peptide evidence on which this ProteinHypothesis is based by reference to a PeptideEvidence element.</remarks>
    public partial class PeptideHypothesis : IdentDataInternalTypeAbstract
    {
        private IdentDataList<SpectrumIdentificationItemRefInfo> _spectrumIdentificationItemRef;
        private string _peptideEvidenceRef;

        /// min 1, max unbounded
        public IdentDataList<SpectrumIdentificationItemRefInfo> SpectrumIdentificationItemRef
        {
            get { return this._spectrumIdentificationItemRef; }
            set
            {
                this._spectrumIdentificationItemRef = value;
                if (this._spectrumIdentificationItemRef != null)
                {
                    this._spectrumIdentificationItemRef.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>A reference to the PeptideEvidence element on which this hypothesis is based.</remarks>
        /// Required Attribute
        /// string
        public string PeptideEvidenceRef
        {
            get { return this._peptideEvidenceRef; }
            set { _peptideEvidenceRef = value; }
        }
    }

    /// <summary>
    /// MzIdentML FragmentArrayType
    /// </summary>
    /// <remarks>An array of values for a given type of measure and for a particular ion type, in parallel to the index of ions identified.</remarks>
    public partial class FragmentArray : IdentDataInternalTypeAbstract
    {
        private List<float> _values;
        private string _measureRef;

        /// <remarks>The values of this particular measure, corresponding to the index defined in ion type</remarks>
        /// Required Attribute
        /// listOfFloats: string, space-separated floats
        public List<float> Values
        {
            get { return this._values; }
            set { this._values = value; }
        }

        /// <remarks>A reference to the Measure defined in the FragmentationTable</remarks>
        /// Required Attribute
        /// string
        public string MeasureRef
        {
            get { return this._measureRef; }
            set { _measureRef = value; }
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
    public partial class IonTypeInfo : IdentDataInternalTypeAbstract
    {
        private IdentDataList<FragmentArray> _fragmentArray;
        private CVParam _cvParam;
        private List<string> _index;
        private int _charge;

        /// min 0, max unbounded
        public IdentDataList<FragmentArray> FragmentArray
        {
            get { return this._fragmentArray; }
            set
            {
                this._fragmentArray = value;
                if (this._fragmentArray != null)
                {
                    this._fragmentArray.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>The type of ion identified.</remarks>
        /// min 1, max 1
        public CVParam CVParam
        {
            get { return this._cvParam; }
            set
            {
                this._cvParam = value;
                if (this._cvParam != null)
                {
                    this._cvParam.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>The index of ions identified as integers, following standard notation for a-c, x-z e.g. if b3 b5 and b6 have been identified, the index would store "3 5 6". For internal ions, the index contains pairs defining the start and end point - see specification document for examples. For immonium ions, the index is the position of the identified ion within the peptide sequence - if the peptide contains the same amino acid in multiple positions that cannot be distinguished, all positions should be given.</remarks>
        /// Optional Attribute
        /// listOfIntegers: string, space-separated integers
        public List<string> Index
        {
            get { return this._index; }
            set { this._index = value; }
        }

        /// <remarks>The charge of the identified fragmentation ions.</remarks>
        /// Required Attribute
        /// integer
        public int Charge
        {
            get { return this._charge; }
            set { this._charge = value; }
        }
    }

    /// <summary>
    /// MzIdentML CVParamType; Container types: ToleranceType
    /// </summary>
    /// <remarks>A single entry from an ontology or a controlled vocabulary.</remarks>
    /// <remarks>ToleranceType: The tolerance of the search given as a plus and minus value with units.</remarks>
    /// <remarks>ToleranceType: child element cvParam of type CVParamType, min 1, max unbounded "CV terms capturing the tolerance plus and minus values."</remarks>
    public partial class CVParam : ParamBase
    {
        private string _cvRef;
        private CV.CV.CVID _cvid;
        //private string _name;
        //private string _accession;
        private string _value;

        public CV.CV.CVID Cvid
        {
            get { return this._cvid; }
            set { this._cvid = value; }
        }

        /// <remarks>A reference to the cv element from which this term originates.</remarks>
        /// Required Attribute
        /// string
        public string CVRef
        {
            get
            {
                //return this._cvRef; 
                return this.IdentData.CvTranslator.ConvertOboCVRef(CV.CV.TermData[this.Cvid].CVRef);
            }
            set
            {
                this._cvRef = this.IdentData.CvTranslator.ConvertFileCVRef(value);
                //this._cvRef = value;
            }
        }

        /// <remarks>The accession or ID number of this CV term in the source CV.</remarks>
        /// Required Attribute
        /// string
        public string Accession
        {
            get
            {
                return CV.CV.TermData[this.Cvid].Id;
                //return this._accession; 
            }
            set
            {
                //this._accession = value; 
                if (CV.CV.TermAccessionLookup.ContainsKey(_cvRef) &&
                    CV.CV.TermAccessionLookup[_cvRef].ContainsKey(value))
                {
                    //this.Cvid = CV.CV.TermAccessionLookup[oboAcc];
                    this.Cvid = CV.CV.TermAccessionLookup[_cvRef][value];
                }
                else
                {
                    this.Cvid = CV.CV.CVID.CVID_Unknown;
                }
            }
        }

        /// <remarks>The name of the parameter.</remarks>
        /// Required Attribute
        /// string
        public override string Name
        {
            get
            {
                return CV.CV.TermData[this.Cvid].Name;
                //return this._name; 
            }
            set
            {
                /*this._name = value;*/
            } // Don't want to do anything here. public interface uses CVID
        }

        /// <remarks>The user-entered value of the parameter.</remarks>
        /// Optional Attribute
        /// string
        public override string Value
        {
            get { return this._value; }
            set { this._value = value; }
        }
    }

    /// <summary>
    /// MzIdentML AbstractParamType; Used instead of: ParamType, ParamGroup, ParamListType
    /// </summary>
    /// <remarks>Abstract entity allowing either cvParam or userParam to be referenced in other schemas.</remarks>
    /// <remarks>PramGroup: A choice of either a cvParam or userParam.</remarks>
    /// <remarks>ParamType: Helper type to allow either a cvParam or a userParam to be provided for an element.</remarks>
    /// <remarks>ParamListType: Helper type to allow multiple cvParams or userParams to be given for an element.</remarks>
    public abstract partial class ParamBase : IdentDataInternalTypeAbstract
    {
        private CV.CV.CVID _unitCvid;
        //private string _unitAccession;
        //private string _unitName;
        private string _unitCvRef;
        private bool _unitsSet = false;

        // Name and value are abstract properties, because name will be handled differently in CVParams, and value can also have restrictions based on the CVParam.

        /// <remarks>The name of the parameter.</remarks>
        /// Required Attribute
        /// string
        public abstract string Name { get; set; }

        /// <remarks>The user-entered value of the parameter.</remarks>
        /// Optional Attribute
        /// string
        public abstract string Value { get; set; }

        public CV.CV.CVID UnitCvid
        {
            get { return this._unitCvid; }
            set
            {
                this._unitCvid = value;
                this._unitsSet = true;
            }
        }

        /// <remarks>An accession number identifying the unit within the OBO foundry Unit CV.</remarks>
        /// Optional Attribute
        /// string
        public string UnitAccession
        {
            get
            {
                if (this._unitsSet)
                {
                    return CV.CV.TermData[this.UnitCvid].Id;
                }
                return null;
                //return this._unitAccession; 
            }
            set
            {
                //this._unitAccession = value; 
                if (value != null && CV.CV.TermAccessionLookup.ContainsKey(_unitCvRef) &&
                    CV.CV.TermAccessionLookup[_unitCvRef].ContainsKey(value))
                {
                    this._unitsSet = true;
                    this._unitCvid = CV.CV.TermAccessionLookup[_unitCvRef][value];
                }
                else
                {
                    this._unitCvid = CV.CV.CVID.CVID_Unknown;
                }
            }
        }

        /// <remarks>The name of the unit.</remarks>
        /// Optional Attribute
        /// string
        public string UnitName
        {
            get
            {
                if (this._unitsSet)
                {
                    return CV.CV.TermData[this.UnitCvid].Name;
                }
                return null;
                //return this._unitName; 
            }
            //set { this._unitName = value; }
        }

        /// <remarks>If a unit term is referenced, this attribute must refer to the CV 'id' attribute defined in the cvList in this file.</remarks>
        /// Optional Attribute
        /// string
        public string UnitCvRef
        {
            get
            {
                if (this._unitsSet)
                {
                    return this.IdentData.CvTranslator.ConvertOboCVRef(CV.CV.TermData[this.UnitCvid].CVRef);
                }
                return null;
                //return this._unitCvRef; 
            }
            set
            {
                this._unitCvRef = value;
                if (value != null)
                {
                    this._unitCvRef = this.IdentData.CvTranslator.ConvertFileCVRef(value);
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML UserParamType
    /// </summary>
    /// <remarks>A single user-defined parameter.</remarks>
    public partial class UserParam : ParamBase
    {
        private string _name;
        private string _value;
        private string _type;

        /// <remarks>The name of the parameter.</remarks>
        /// Required Attribute
        /// string
        public override string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <remarks>The user-entered value of the parameter.</remarks>
        /// Optional Attribute
        /// string
        public override string Value
        {
            get { return this._value; }
            set { this._value = value; }
        }

        /// <remarks>The datatype of the parameter, where appropriate (e.g.: xsd:float).</remarks>
        /// Optional Attribute
        /// string
        public string Type
        {
            get { return this._type; }
            set { this._type = value; }
        }
    }

    public partial class CVParamGroup : IdentDataInternalTypeAbstract
    {
        private IdentDataList<CVParam> _cvParams;

        public IdentDataList<CVParam> CVParams
        {
            get { return this._cvParams; }
            set
            {
                this._cvParams = value;
                if (this._cvParams != null)
                {
                    this._cvParams.IdentData = this.IdentData;
                }
            }
        }
    }

    public partial class ParamGroup : CVParamGroup
    {
        private IdentDataList<UserParam> _userParams;

        public IdentDataList<UserParam> UserParams
        {
            get { return this._userParams; }
            set
            {
                this._userParams = value;
                if (this._userParams != null)
                {
                    this._userParams.IdentData = this.IdentData;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML ParamType
    /// </summary>
    /// <remarks>Helper type to allow either a cvParam or a userParam to be provided for an element.</remarks>
    public partial class Param : IdentDataInternalTypeAbstract
    {
        private ParamBase _item;

        /// min 1, max 1
        public ParamBase Item
        {
            get { return this._item; }
            set
            {
                this._item = value;
                if (this._item != null)
                {
                    this._item.IdentData = this.IdentData;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML ParamListType
    /// </summary>
    /// <remarks>Helper type to allow multiple cvParams or userParams to be given for an element.</remarks>
    public partial class ParamList : IdentDataInternalTypeAbstract
    {
        private IdentDataList<ParamBase> _items;

        /// min 1, max unbounded
        public IdentDataList<ParamBase> Items
        {
            get { return this._items; }
            set
            {
                this._items = value;
                if (this._items != null)
                {
                    this._items.IdentData = this.IdentData;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML PeptideEvidenceRefType
    /// </summary>
    /// <remarks>Reference to the PeptideEvidence element identified. If a specific sequence can be assigned to multiple 
    /// proteins and or positions in a protein all possible PeptideEvidence elements should be referenced here.</remarks>
    public partial class PeptideEvidenceRefInfo : IdentDataInternalTypeAbstract
    {
        private string _peptideEvidenceRef;

        /// <remarks>A reference to the PeptideEvidenceItem element(s).</remarks>
        /// Required Attribute
        /// string
        public string PeptideEvidenceRef
        {
            get { return this._peptideEvidenceRef; }
            set { _peptideEvidenceRef = value; }
        }
    }

    /// <summary>
    /// MzIdentML AnalysisDataType
    /// </summary>
    /// <remarks>Data sets generated by the analyses, including peptide and protein lists.</remarks>
    public partial class AnalysisData : IdentDataInternalTypeAbstract
    {
        private IdentDataList<SpectrumIdentificationList> _spectrumIdentificationList;
        private ProteinDetectionList _proteinDetectionList;

        /// min 1, max unbounded
        public IdentDataList<SpectrumIdentificationList> SpectrumIdentificationList
        {
            get { return this._spectrumIdentificationList; }
            set
            {
                this._spectrumIdentificationList = value;
                if (this._spectrumIdentificationList != null)
                {
                    this._spectrumIdentificationList.IdentData = this.IdentData;
                }
            }
        }

        /// min 0, max 1
        public ProteinDetectionList ProteinDetectionList
        {
            get { return this._proteinDetectionList; }
            set
            {
                this._proteinDetectionList = value;
                if (this._proteinDetectionList != null)
                {
                    this._proteinDetectionList.IdentData = this.IdentData;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationListType
    /// </summary>
    /// <remarks>Represents the set of all search results from SpectrumIdentification.</remarks>
    public partial class SpectrumIdentificationList : ParamGroup, IIdentifiableType
    {
        private IdentDataList<Measure> _fragmentationTable;
        private IdentDataList<SpectrumIdentificationResult> _spectrumIdentificationResult;
        //private IdentDataList<CVParamType> _cvParams;
        //private IdentDataList<UserParamType> _userParams;
        private long _numSequencesSearched;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// min 0, max 1
        public IdentDataList<Measure> FragmentationTable
        {
            get { return this._fragmentationTable; }
            set
            {
                this._fragmentationTable = value;
                if (this._fragmentationTable != null)
                {
                    this._fragmentationTable.IdentData = this.IdentData;
                }
            }
        }

        /// min 1, max unbounded
        public IdentDataList<SpectrumIdentificationResult> SpectrumIdentificationResult
        {
            get { return this._spectrumIdentificationResult; }
            set
            {
                this._spectrumIdentificationResult = value;
                if (this._spectrumIdentificationResult != null)
                {
                    this._spectrumIdentificationResult.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>___ParamGroup___:Scores or output parameters associated with the SpectrumIdentificationList.</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams
        //{
        //    get { return this._cvParams; }
        //    set
        //    {
        //        this._cvParams = value;
        //        if (this._cvParams != null)
        //        {
        //            this._cvParams.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams
        //{
        //    get { return this._userParams; }
        //    set
        //    {
        //        this._userParams = value;
        //        if (this._userParams != null)
        //        {
        //            this._userParams.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>The number of database sequences searched against. This value should be provided unless a de novo search has been performed.</remarks>
        /// Optional Attribute
        /// long
        public long NumSequencesSearched
        {
            get { return this._numSequencesSearched; }
            set
            {
                this._numSequencesSearched = value;
                this.NumSequencesSearchedSpecified = true;
            }
        }

        /// Attribute Existence
        public bool NumSequencesSearchedSpecified { get; private set; }
    }

    /// <summary>
    /// MzIdentML MeasureType; list form is FragmentationTableType
    /// </summary>
    /// <remarks>References to CV terms defining the measures about product ions to be reported in SpectrumIdentificationItem</remarks>
    /// <remarks>FragmentationTableType: Contains the types of measures that will be reported in generic arrays 
    /// for each SpectrumIdentificationItem e.g. product ion m/z, product ion intensity, product ion m/z error</remarks>
    /// <remarks>FragmentationTableType: child element Measure of type MeasureType, min 1, max unbounded</remarks>
    public partial class Measure : CVParamGroup, IIdentifiableType
    {
        //private IdentDataList<CVParamType> _cvParams;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// min 1, max unbounded
        //public IdentDataList<CVParamType> CVParams
        //{
        //    get { return this._cvParams; }
        //    set
        //    {
        //        this._cvParams = value;
        //        if (this._cvParams != null)
        //        {
        //            this._cvParams.IdentData = this.IdentData;
        //        }
        //    }
        //}
    }

    /// <summary>
    /// MzIdentML IdentifiableType
    /// </summary>
    /// <remarks>Other classes in the model can be specified as sub-classes, inheriting from Identifiable. 
    /// Identifiable gives classes a unique identifier within the scope and a name that need not be unique.</remarks>
    public partial interface IIdentifiableType
    {
        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        string Id { get; set; }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        string Name { get; set; }
    }

    /// <summary>
    /// MzIdentML BibliographicReferenceType
    /// </summary>
    /// <remarks>Represents bibliographic references.</remarks>
    public partial class BibliographicReference : IdentDataInternalTypeAbstract, IIdentifiableType
    {
        private int _year;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <remarks>The names of the authors of the reference.</remarks>
        /// Optional Attribute
        /// string
        public string Authors { get; set; }

        /// <remarks>The name of the journal, book etc.</remarks>
        /// Optional Attribute
        /// string
        public string Publication { get; set; }

        /// <remarks>The publisher of the publication.</remarks>
        /// Optional Attribute
        /// string
        public string Publisher { get; set; }

        /// <remarks>The editor(s) of the reference.</remarks>
        /// Optional Attribute
        /// string
        public string Editor { get; set; }

        /// <remarks>The year of publication.</remarks>
        /// Optional Attribute
        /// integer
        public int Year
        {
            get { return this._year; }
            set
            {
                this._year = value;
                this.YearSpecified = true;
            }
        }

        /// Attribute Existence
        public bool YearSpecified { get; private set; }

        /// <remarks>The volume name or number.</remarks>
        /// Optional Attribute
        /// string
        public string Volume { get; set; }

        /// <remarks>The issue name or number.</remarks>
        /// Optional Attribute
        /// string
        public string Issue { get; set; }

        /// <remarks>The page numbers.</remarks>
        /// Optional Attribute
        /// string
        public string Pages { get; set; }

        /// <remarks>The title of the BibliographicReference.</remarks>
        /// Optional Attribute
        /// string
        public string Title { get; set; }

        /// <remarks>The DOI of the referenced publication.</remarks>
        /// Optional Attribute
        /// string
        public string DOI { get; set; }
    }

    /// <summary>
    /// MzIdentML ProteinDetectionHypothesisType
    /// </summary>
    /// <remarks>A single result of the ProteinDetection analysis (i.e. a protein).</remarks>
    public partial class ProteinDetectionHypothesis : ParamGroup, IIdentifiableType
    {
        private IdentDataList<PeptideHypothesis> _peptideHypothesis;
        //private IdentDataList<CVParamType> _cvParams;
        //private IdentDataList<UserParamType> _userParams;
        private string _dBSequenceRef;
        private bool _passThreshold;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// min 1, max unbounded
        public IdentDataList<PeptideHypothesis> PeptideHypothesis
        {
            get { return this._peptideHypothesis; }
            set
            {
                this._peptideHypothesis = value;
                if (this._peptideHypothesis != null)
                {
                    this._peptideHypothesis.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>___ParamGroup___:Scores or parameters associated with this ProteinDetectionHypothesis e.g. p-value</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams
        //{
        //    get { return this._cvParams; }
        //    set
        //    {
        //        this._cvParams = value;
        //        if (this._cvParams != null)
        //        {
        //            this._cvParams.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams
        //{
        //    get { return this._userParams; }
        //    set
        //    {
        //        this._userParams = value;
        //        if (this._userParams != null)
        //        {
        //            this._userParams.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>A reference to the corresponding DBSequence entry. This optional and redundant, because the PeptideEvidence 
        /// elements referenced from here also map to the DBSequence.</remarks>
        /// Optional Attribute
        /// string
        public string DBSequenceRef
        {
            get { return this._dBSequenceRef; }
            set { _dBSequenceRef = value; }
        }

        /// <remarks>Set to true if the producers of the file has deemed that the ProteinDetectionHypothesis has passed a given 
        /// threshold or been validated as correct. If no such threshold has been set, value of true should be given for all results.</remarks>
        /// Required Attribute
        /// boolean
        public bool PassThreshold
        {
            get { return this._passThreshold; }
            set { this._passThreshold = value; }
        }
    }

    /// <summary>
    /// MzIdentML ProteinAmbiguityGroupType
    /// </summary>
    /// <remarks>A set of logically related results from a protein detection, for example to represent conflicting assignments of peptides to proteins.</remarks>
    public partial class ProteinAmbiguityGroup : ParamGroup, IIdentifiableType
    {
        private IdentDataList<ProteinDetectionHypothesis> _proteinDetectionHypothesis;
        //private IdentDataList<CVParamType> _cvParams;
        //private IdentDataList<UserParamType> _userParams;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// min 1, max unbounded
        public IdentDataList<ProteinDetectionHypothesis> ProteinDetectionHypothesis
        {
            get { return this._proteinDetectionHypothesis; }
            set
            {
                this._proteinDetectionHypothesis = value;
                if (this._proteinDetectionHypothesis != null)
                {
                    this._proteinDetectionHypothesis.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>___ParamGroup___:Scores or parameters associated with the ProteinAmbiguityGroup.</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams
        //{
        //    get { return this._cvParams; }
        //    set
        //    {
        //        this._cvParams = value;
        //        if (this._cvParams != null)
        //        {
        //            this._cvParams.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams
        //{
        //    get { return this._userParams; }
        //    set
        //    {
        //        this._userParams = value;
        //        if (this._userParams != null)
        //        {
        //            this._userParams.IdentData = this.IdentData;
        //        }
        //    }
        //}
    }

    /// <summary>
    /// MzIdentML ProteinDetectionListType
    /// </summary>
    /// <remarks>The protein list resulting from a protein detection process.</remarks>
    public partial class ProteinDetectionList : ParamGroup, IIdentifiableType
    {
        private IdentDataList<ProteinAmbiguityGroup> _proteinAmbiguityGroup;
        //private IdentDataList<CVParamType> _cvParams;
        //private IdentDataList<UserParamType> _userParams;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// min 0, max unbounded
        public IdentDataList<ProteinAmbiguityGroup> ProteinAmbiguityGroup
        {
            get { return this._proteinAmbiguityGroup; }
            set
            {
                this._proteinAmbiguityGroup = value;
                if (this._proteinAmbiguityGroup != null)
                {
                    this._proteinAmbiguityGroup.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>___ParamGroup___:Scores or output parameters associated with the whole ProteinDetectionList</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams
        //{
        //    get { return this._cvParams; }
        //    set
        //    {
        //        this._cvParams = value;
        //        if (this._cvParams != null)
        //        {
        //            this._cvParams.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams
        //{
        //    get { return this._userParams; }
        //    set
        //    {
        //        this._userParams = value;
        //        if (this._userParams != null)
        //        {
        //            this._userParams.IdentData = this.IdentData;
        //        }
        //    }
        //}
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationItemType
    /// </summary>
    /// <remarks>An identification of a single (poly)peptide, resulting from querying an input spectra, along with 
    /// the set of confidence values for that identification. PeptideEvidence elements should be given for all 
    /// mappings of the corresponding Peptide sequence within protein sequences.</remarks>
    public partial class SpectrumIdentificationItem : ParamGroup, IIdentifiableType
    {
        private IdentDataList<PeptideEvidenceRefInfo> _peptideEvidenceRef;
        private IdentDataList<IonTypeInfo> _fragmentation;
        //private IdentDataList<CVParamType> _cvParam;
        //private IdentDataList<UserParamType> _userParam;
        private int _chargeState;
        private double _experimentalMassToCharge;
        private double _calculatedMassToCharge;
        private float _calculatedPI;
        private string _peptideRef;
        private int _rank;
        private bool _passThreshold;
        private string _massTableRef;
        private string _sampleRef;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// min 1, max unbounded
        public IdentDataList<PeptideEvidenceRefInfo> PeptideEvidenceRef
        {
            get { return this._peptideEvidenceRef; }
            set
            {
                this._peptideEvidenceRef = value;
                if (this._peptideEvidenceRef != null)
                {
                    this._peptideEvidenceRef.IdentData = this.IdentData;
                }
            }
        }

        /// min 0, max 1
        public IdentDataList<IonTypeInfo> Fragmentation
        {
            get { return this._fragmentation; }
            set
            {
                this._fragmentation = value;
                if (this._fragmentation != null)
                {
                    this._fragmentation.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>___ParamGroup___:Scores or attributes associated with the SpectrumIdentificationItem e.g. e-value, p-value, score.</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams
        //{
        //    get { return this._cvParam; }
        //    set
        //    {
        //        this._cvParam = value;
        //        if (this._cvParam != null)
        //        {
        //            this._cvParam.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams
        //{
        //    get { return this._userParam; }
        //    set
        //    {
        //        this._userParam = value;
        //        if (this._userParam != null)
        //        {
        //            this._userParam.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>The charge state of the identified peptide.</remarks>
        /// Required Attribute
        /// integer
        public int ChargeState
        {
            get { return this._chargeState; }
            set { this._chargeState = value; }
        }

        /// <remarks>The mass-to-charge value measured in the experiment in Daltons / charge.</remarks>
        /// Required Attribute
        /// double
        public double ExperimentalMassToCharge
        {
            get { return this._experimentalMassToCharge; }
            set { this._experimentalMassToCharge = value; }
        }

        /// <remarks>The theoretical mass-to-charge value calculated for the peptide in Daltons / charge.</remarks>
        /// Optional Attribute
        /// double
        public double CalculatedMassToCharge
        {
            get { return this._calculatedMassToCharge; }
            set
            {
                this._calculatedMassToCharge = value;
                this.CalculatedMassToChargeSpecified = true;
            }
        }

        /// Attribute Existence
        public bool CalculatedMassToChargeSpecified { get; private set; }

        /// <remarks>The calculated isoelectric point of the (poly)peptide, with relevant modifications included. 
        /// Do not supply this value if the PI cannot be calcuated properly.</remarks>
        /// Optional Attribute
        /// float
        public float CalculatedPI
        {
            get { return this._calculatedPI; }
            set
            {
                this._calculatedPI = value;
                this.CalculatedPISpecified = true;
            }
        }

        /// Attribute Existence
        public bool CalculatedPISpecified { get; private set; }

        /// <remarks>A reference to the identified (poly)peptide sequence in the Peptide element.</remarks>
        /// Optional Attribute
        /// string
        public string PeptideRef
        {
            get { return this._peptideRef; }
            set { this._peptideRef = value; }
        }

        /// <remarks>For an MS/MS result set, this is the rank of the identification quality as scored by the search engine. 
        /// 1 is the top rank. If multiple identifications have the same top score, they should all be assigned rank =1. 
        /// For PMF data, the rank attribute may be meaningless and values of rank = 0 should be given.</remarks>
        /// Required Attribute
        /// integer
        public int Rank
        {
            get { return this._rank; }
            set { this._rank = value; }
        }

        /// <remarks>Set to true if the producers of the file has deemed that the identification has passed a given threshold 
        /// or been validated as correct. If no such threshold has been set, value of true should be given for all results.</remarks>
        /// Required Attribute
        /// boolean
        public bool PassThreshold
        {
            get { return this._passThreshold; }
            set { this._passThreshold = value; }
        }

        /// <remarks>A reference should be given to the MassTable used to calculate the sequenceMass only if more than one MassTable has been given.</remarks>
        /// Optional Attribute
        /// string
        public string MassTableRef
        {
            get { return this._massTableRef; }
            set { this._massTableRef = value; }
        }

        /// <remarks>A reference should be provided to link the SpectrumIdentificationItem to a Sample 
        /// if more than one sample has been described in the AnalysisSampleCollection.</remarks>
        /// Optional Attribute
        public string SampleRef
        {
            get { return this._sampleRef; }
            set { this._sampleRef = value; }
        }
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationResultType
    /// </summary>
    /// <remarks>All identifications made from searching one spectrum. For PMF data, all peptide identifications 
    /// will be listed underneath as SpectrumIdentificationItems. For MS/MS data, there will be ranked 
    /// SpectrumIdentificationItems corresponding to possible different peptide IDs.</remarks>
    public partial class SpectrumIdentificationResult : ParamGroup, IIdentifiableType
    {
        private IdentDataList<SpectrumIdentificationItem> _spectrumIdentificationItems;
        //private IdentDataList<CVParamType> _cvParams;
        //private IdentDataList<UserParamType> _userParams;
        private string _spectrumID;
        private string _spectraDataRef;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// min 1, max unbounded
        public IdentDataList<SpectrumIdentificationItem> SpectrumIdentificationItems
        {
            get { return this._spectrumIdentificationItems; }
            set
            {
                this._spectrumIdentificationItems = value;
                if (this._spectrumIdentificationItems != null)
                {
                    this._spectrumIdentificationItems.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>___ParamGroup___: Scores or parameters associated with the SpectrumIdentificationResult 
        /// (i.e the set of SpectrumIdentificationItems derived from one spectrum) e.g. the number of peptide 
        /// sequences within the parent tolerance for this spectrum.</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams
        //{
        //    get { return this._cvParams; }
        //    set
        //    {
        //        this._cvParams = value;
        //        if (this._cvParams != null)
        //        {
        //            this._cvParams.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams
        //{
        //    get { return this._userParams; }
        //    set
        //    {
        //        this._userParams = value;
        //        if (this._userParams != null)
        //        {
        //            this._userParams.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>The locally unique id for the spectrum in the spectra data set specified by SpectraData_ref. 
        /// External guidelines are provided on the use of consistent identifiers for spectra in different external formats.</remarks>
        /// Required Attribute
        /// string
        public string SpectrumID
        {
            get { return this._spectrumID; }
            set { this._spectrumID = value; }
        }

        /// <remarks>A reference to a spectra data set (e.g. a spectra file).</remarks>
        /// Required Attribute
        /// string
        public string SpectraDataRef
        {
            get { return _spectraDataRef; }
            set { _spectraDataRef = value; }
        }
    }

    /// <summary>
    /// MzIdentML ExternalDataType
    /// </summary>
    /// <remarks>Data external to the XML instance document. The location of the data file is given in the location attribute.</remarks>
    public partial interface IExternalDataType : IIdentifiableType
    {
        /// <remarks>A URI to access documentation and tools to interpret the external format of the ExternalData instance. 
        /// For example, XML Schema or static libraries (APIs) to access binary formats.</remarks>
        /// min 0, max 1
        string ExternalFormatDocumentation { get; set; }

        /// min 0, max 1
        FileFormatInfo FileFormat { get; set; }

        /// <remarks>The location of the data file.</remarks>
        /// Required Attribute
        /// string
        string Location { get; set; }
    }

    /// <summary>
    /// MzIdentML FileFormatType
    /// </summary>
    /// <remarks>The format of the ExternalData file, for example "tiff" for image files.</remarks>
    public partial class FileFormatInfo : IdentDataInternalTypeAbstract
    {
        private CVParam _cvParam;

        /// <remarks>cvParam capturing file formats</remarks>
        /// Optional Attribute
        /// min 1, max 1
        public CVParam CVParam
        {
            get { return this._cvParam; }
            set
            {
                this._cvParam = value;
                if (this._cvParam != null)
                {
                    this._cvParam.IdentData = this.IdentData;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML SpectraDataType
    /// </summary>
    /// <remarks>A data set containing spectra data (consisting of one or more spectra).</remarks>
    public partial class SpectraData : IdentDataInternalTypeAbstract, IExternalDataType
    {
        private SpectrumIDFormat _spectrumIDFormat;
        private string _externalFormatDocumentation;
        private FileFormatInfo _fileFormat;
        private string _location;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <remarks>A URI to access documentation and tools to interpret the external format of the ExternalData instance. 
        /// For example, XML Schema or static libraries (APIs) to access binary formats.</remarks>
        /// min 0, max 1
        public string ExternalFormatDocumentation
        {
            get { return this._externalFormatDocumentation; }
            set { this._externalFormatDocumentation = value; }
        }

        /// min 0, max 1
        public FileFormatInfo FileFormat
        {
            get { return this._fileFormat; }
            set
            {
                this._fileFormat = value;
                if (this._fileFormat != null)
                {
                    this._fileFormat.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>The location of the data file.</remarks>
        /// Required Attribute
        /// string
        public string Location
        {
            get { return this._location; }
            set { this._location = value; }
        }

        /// min 1, max 1
        public SpectrumIDFormat SpectrumIDFormat
        {
            get { return this._spectrumIDFormat; }
            set
            {
                this._spectrumIDFormat = value;
                if (this._spectrumIDFormat != null)
                {
                    this._spectrumIDFormat.IdentData = this.IdentData;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML SpectrumIDFormatType
    /// </summary>
    /// <remarks>The format of the spectrum identifier within the source file</remarks>
    public partial class SpectrumIDFormat : IdentDataInternalTypeAbstract
    {
        private CVParam _cvParam;

        /// <remarks>CV term capturing the type of identifier used.</remarks>
        /// min 1, max 1
        public CVParam CVParam
        {
            get { return this._cvParam; }
            set
            {
                this._cvParam = value;
                if (this._cvParam != null)
                {
                    this._cvParam.IdentData = this.IdentData;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML SourceFileType
    /// </summary>
    /// <remarks>A file from which this mzIdentML instance was created.</remarks>
    public partial class SourceFileInfo : ParamGroup, IExternalDataType
    {
        //private IdentDataList<CVParamType> _cvParams;
        //private IdentDataList<UserParamType> _userParams;
        private string _externalFormatDocumentation;
        private FileFormatInfo _fileFormat;
        private string _location;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <remarks>A URI to access documentation and tools to interpret the external format of the ExternalData instance. 
        /// For example, XML Schema or static libraries (APIs) to access binary formats.</remarks>
        /// min 0, max 1
        public string ExternalFormatDocumentation
        {
            get { return this._externalFormatDocumentation; }
            set { this._externalFormatDocumentation = value; }
        }

        /// min 0, max 1
        public FileFormatInfo FileFormat
        {
            get { return this._fileFormat; }
            set
            {
                this._fileFormat = value;
                if (this._fileFormat != null)
                {
                    this._fileFormat.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>The location of the data file.</remarks>
        /// Required Attribute
        /// string
        public string Location
        {
            get { return this._location; }
            set { this._location = value; }
        }

        /// <remarks>___ParamGroup___:Any additional parameters description the source file.</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams
        //{
        //    get { return this._cvParams; }
        //    set
        //    {
        //        this._cvParams = value;
        //        if (this._cvParams != null)
        //        {
        //            this._cvParams.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams
        //{
        //    get { return this._userParams; }
        //    set
        //    {
        //        this._userParams = value;
        //        if (this._userParams != null)
        //        {
        //            this._userParams.IdentData = this.IdentData;
        //        }
        //    }
        //}
    }

    /// <summary>
    /// MzIdentML SearchDatabaseType
    /// </summary>
    /// <remarks>A database for searching mass spectra. Examples include a set of amino acid sequence entries, or annotated spectra libraries.</remarks>
    public partial class SearchDatabaseInfo : CVParamGroup, IExternalDataType
    {
        private Param _databaseName;
        //private IdentDataList<CVParamType> _cvParams;
        private string _version;
        private System.DateTime _releaseDate;
        private long _numDatabaseSequences;
        private long _numResidues;
        private string _externalFormatDocumentation;
        private FileFormatInfo _fileFormat;
        private string _location;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <remarks>A URI to access documentation and tools to interpret the external format of the ExternalData instance. 
        /// For example, XML Schema or static libraries (APIs) to access binary formats.</remarks>
        /// min 0, max 1
        public string ExternalFormatDocumentation
        {
            get { return this._externalFormatDocumentation; }
            set { this._externalFormatDocumentation = value; }
        }

        /// min 0, max 1
        public FileFormatInfo FileFormat
        {
            get { return this._fileFormat; }
            set
            {
                this._fileFormat = value;
                if (this._fileFormat != null)
                {
                    this._fileFormat.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>The location of the data file.</remarks>
        /// Required Attribute
        /// string
        public string Location
        {
            get { return this._location; }
            set { this._location = value; }
        }

        /// <remarks>The database name may be given as a cvParam if it maps exactly to one of the release databases listed in the CV, otherwise a userParam should be used.</remarks>
        /// min 1, max 1
        public Param DatabaseName
        {
            get { return this._databaseName; }
            set
            {
                this._databaseName = value;
                if (this._databaseName != null)
                {
                    this._databaseName.IdentData = this.IdentData;
                }
            }
        }

        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams
        //{
        //    get { return this._cvParams; }
        //    set
        //    {
        //        this._cvParams = value;
        //        if (this._cvParams != null)
        //        {
        //            this._cvParams.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>The version of the database.</remarks>
        /// Optional Attribute
        /// string
        public string Version
        {
            get { return this._version; }
            set { this._version = value; }
        }

        /// <remarks>The date and time the database was released to the public; omit this attribute when the date and time are unknown or not applicable (e.g. custom databases).</remarks>
        /// Optional Attribute
        /// dateTime
        public System.DateTime ReleaseDate
        {
            get { return this._releaseDate; }
            set
            {
                this._releaseDate = value;
                this.ReleaseDateSpecified = true;
            }
        }

        /// Attribute Existence
        public bool ReleaseDateSpecified { get; private set; }

        /// <remarks>The total number of sequences in the database.</remarks>
        /// Optional Attribute
        /// long
        public long NumDatabaseSequences
        {
            get { return this._numDatabaseSequences; }
            set
            {
                this._numDatabaseSequences = value;
                this.NumDatabaseSequencesSpecified = true;
            }
        }

        /// Attribute Existence
        public bool NumDatabaseSequencesSpecified { get; private set; }

        /// <remarks>The number of residues in the database.</remarks>
        /// Optional Attribute
        /// long
        public long NumResidues
        {
            get { return this._numResidues; }
            set
            {
                this._numResidues = value;
                this.NumResiduesSpecified = true;
            }
        }

        /// <remarks></remarks>
        /// Attribute Existence
        public bool NumResiduesSpecified { get; private set; }
    }

    /// <summary>
    /// MzIdentML ProteinDetectionProtocolType
    /// </summary>
    /// <remarks>The parameters and settings of a ProteinDetection process.</remarks>
    public partial class ProteinDetectionProtocol : IdentDataInternalTypeAbstract, IIdentifiableType
    {
        private ParamList _analysisParams;
        private ParamList _threshold;
        private string _analysisSoftwareRef;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <remarks>The parameters and settings for the protein detection given as CV terms.</remarks>
        /// min 0, max 1
        public ParamList AnalysisParams
        {
            get { return this._analysisParams; }
            set
            {
                this._analysisParams = value;
                if (this._analysisParams != null)
                {
                    this._analysisParams.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>The threshold(s) applied to determine that a result is significant. 
        /// If multiple terms are used it is assumed that all conditions are satisfied by the passing results.</remarks>
        /// min 1, max 1
        public ParamList Threshold
        {
            get { return this._threshold; }
            set
            {
                this._threshold = value;
                if (this._threshold != null)
                {
                    this._threshold.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>The protein detection software used, given as a reference to the SoftwareCollection section.</remarks>
        /// Required Attribute
        /// string
        public string AnalysisSoftwareRef
        {
            get { return this._analysisSoftwareRef; }
            set { _analysisSoftwareRef = value; }
        }
    }

    /// <summary>
    /// MzIdentML TranslationTableType
    /// </summary>
    /// <remarks>The table used to translate codons into nucleic acids e.g. by reference to the NCBI translation table.</remarks>
    public partial class TranslationTable : CVParamGroup, IIdentifiableType
    {
        //private IdentDataList<CVParamType> _cvParams;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <remarks>The details specifying this translation table are captured as cvParams, e.g. translation table, translation 
        /// start codons and translation table description (see specification document and mapping file)</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams
        //{
        //    get { return this._cvParams; }
        //    set
        //    {
        //        this._cvParams = value;
        //        if (this._cvParams != null)
        //        {
        //            this._cvParams.IdentData = this.IdentData;
        //        }
        //    }
        //}
    }

    /// <summary>
    /// MzIdentML MassTableType
    /// </summary>
    /// <remarks>The masses of residues used in the search.</remarks>
    public partial class MassTable : ParamGroup, IIdentifiableType
    {
        private IdentDataList<Residue> _residue;
        private IdentDataList<AmbiguousResidue> _ambiguousResidue;
        //private IdentDataList<CVParamType> _cvParams;
        //private IdentDataList<UserParamType> _userParams;
        private List<string> _msLevel;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <remarks>The specification of a single residue within the mass table.</remarks>
        /// min 0, max unbounded
        public IdentDataList<Residue> Residue
        {
            get { return this._residue; }
            set
            {
                this._residue = value;
                if (this._residue != null)
                {
                    this._residue.IdentData = this.IdentData;
                }
            }
        }

        /// min 0, max unbounded
        public IdentDataList<AmbiguousResidue> AmbiguousResidue
        {
            get { return this._ambiguousResidue; }
            set
            {
                this._ambiguousResidue = value;
                if (this._ambiguousResidue != null)
                {
                    this._ambiguousResidue.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>___ParamGroup___: Additional parameters or descriptors for the MassTable.</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams
        //{
        //    get { return this._cvParams; }
        //    set
        //    {
        //        this._cvParams = value;
        //        if (this._cvParams != null)
        //        {
        //            this._cvParams.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams
        //{
        //    get { return this._userParams; }
        //    set
        //    {
        //        this._userParams = value;
        //        if (this._userParams != null)
        //        {
        //            this._userParams.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>The MS spectrum that the MassTable refers to e.g. "1" for MS1 "2" for MS2 or "1 2" for MS1 or MS2.</remarks>
        /// Required Attribute
        /// integer(s), space separated
        public List<string> MsLevel
        {
            get { return this._msLevel; }
            set { this._msLevel = value; }
        }
    }

    /// <summary>
    /// MzIdentML ResidueType
    /// </summary>
    public partial class Residue : IdentDataInternalTypeAbstract
    {
        private string _code;
        private float _mass;

        /// <remarks>The single letter code for the residue.</remarks>
        /// Required Attribute
        /// chars, string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}"
        public string Code
        {
            get { return this._code; }
            set { this._code = value; }
        }

        /// <remarks>The residue mass in Daltons (not including any fixed modifications).</remarks>
        /// Required Attribute
        /// float
        public float Mass
        {
            get { return this._mass; }
            set { this._mass = value; }
        }
    }

    /// <summary>
    /// MzIdentML AmbiguousResidueType
    /// </summary>
    /// <remarks>Ambiguous residues e.g. X can be specified by the Code attribute and a set of parameters 
    /// for example giving the different masses that will be used in the search.</remarks>
    public partial class AmbiguousResidue : ParamGroup
    {
        //private IdentDataList<CVParamType> _cvParams;
        //private IdentDataList<UserParamType> _userParams;
        private string _code;

        /// <remarks>___ParamGroup___: Parameters for capturing e.g. "alternate single letter codes"</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams
        //{
        //    get { return this._cvParams; }
        //    set
        //    {
        //        this._cvParams = value;
        //        if (this._cvParams != null)
        //        {
        //            this._cvParams.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams
        //{
        //    get { return this._userParams; }
        //    set
        //    {
        //        this._userParams = value;
        //        if (this._userParams != null)
        //        {
        //            this._userParams.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>The single letter code of the ambiguous residue e.g. X.</remarks>
        /// Required Attribute
        /// chars, string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}"
        public string Code
        {
            get { return this._code; }
            set { this._code = value; }
        }
    }

    /// <summary>
    /// MzIdentML EnzymeType
    /// </summary>
    /// <remarks>The details of an individual cleavage enzyme should be provided by giving a regular expression 
    /// or a CV term if a "standard" enzyme cleavage has been performed.</remarks>
    public partial class Enzyme : IdentDataInternalTypeAbstract, IIdentifiableType
    {
        private string _siteRegexp;
        private ParamList _enzymeName;
        private string _nTermGain;
        private string _cTermGain;
        private bool _semiSpecific;
        private int _missedCleavages;
        private int _minDistance;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <remarks>Regular expression for specifying the enzyme cleavage site.</remarks>
        /// min 0, max 1
        public string SiteRegexp
        {
            get { return this._siteRegexp; }
            set { this._siteRegexp = value; }
        }

        /// <remarks>The name of the enzyme from a CV.</remarks>
        /// min 0, max 1
        public ParamList EnzymeName
        {
            get { return this._enzymeName; }
            set
            {
                this._enzymeName = value;
                if (this._enzymeName != null)
                {
                    this._enzymeName.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>Element formula gained at NTerm.</remarks>
        /// Optional Attribute
        /// string, regex: "[A-Za-z0-9 ]+"
        public string NTermGain
        {
            get { return this._nTermGain; }
            set { this._nTermGain = value; }
        }

        /// <remarks>Element formula gained at CTerm.</remarks>
        /// Optional Attribute
        /// string, regex: "[A-Za-z0-9 ]+"
        public string CTermGain
        {
            get { return this._cTermGain; }
            set { this._cTermGain = value; }
        }

        /// <remarks>Set to true if the enzyme cleaves semi-specifically (i.e. one terminus must cleave 
        /// according to the rules, the other can cleave at any residue), false if the enzyme cleavage 
        /// is assumed to be specific to both termini (accepting for any missed cleavages).</remarks>
        /// Optional Attribute
        /// boolean
        public bool SemiSpecific
        {
            get { return this._semiSpecific; }
            set
            {
                this._semiSpecific = value;
                this.SemiSpecificSpecified = true;
            }
        }

        /// Attribute Existence
        public bool SemiSpecificSpecified { get; private set; }

        /// <remarks>The number of missed cleavage sites allowed by the search. The attribute must be provided if an enzyme has been used.</remarks>
        /// Optional Attribute
        /// integer
        public int MissedCleavages
        {
            get { return this._missedCleavages; }
            set
            {
                this._missedCleavages = value;
                this.MissedCleavagesSpecified = true;
            }
        }

        /// Attribute Existence
        public bool MissedCleavagesSpecified { get; private set; }

        /// <remarks>Minimal distance for another cleavage (minimum: 1).</remarks>
        /// Optional Attribute
        /// integer >= 1
        public int MinDistance
        {
            get { return this._minDistance; }
            set
            {
                this._minDistance = value;
                this.MinDistanceSpecified = true;
            }
        }

        /// Attribute Existence
        public bool MinDistanceSpecified { get; private set; }
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationProtocolType
    /// </summary>
    /// <remarks>The parameters and settings of a SpectrumIdentification analysis.</remarks>
    public partial class SpectrumIdentificationProtocol : IdentDataInternalTypeAbstract, IIdentifiableType
    {
        private Param _searchType;
        private ParamList _additionalSearchParams;
        private IdentDataList<SearchModification> _modificationParams;
        private EnzymeList _enzymes;
        private IdentDataList<MassTable> _massTable;
        private IdentDataList<CVParam> _fragmentTolerance;
        private IdentDataList<CVParam> _parentTolerance;
        private ParamList _threshold;
        private IdentDataList<FilterInfo> _databaseFilters;
        private DatabaseTranslation _databaseTranslation;
        private string _analysisSoftwareRef;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <remarks>The type of search performed e.g. PMF, Tag searches, MS-MS</remarks>
        /// min 1, max 1
        public Param SearchType
        {
            get { return this._searchType; }
            set
            {
                this._searchType = value;
                if (this._searchType != null)
                {
                    this._searchType.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>The search parameters other than the modifications searched.</remarks>
        /// min 0, max 1
        public ParamList AdditionalSearchParams
        {
            get { return this._additionalSearchParams; }
            set
            {
                this._additionalSearchParams = value;
                if (this._additionalSearchParams != null)
                {
                    this._additionalSearchParams.IdentData = this.IdentData;
                }
            }
        }

        /// min 0, max 1 : Original ModificationParamsType
        public IdentDataList<SearchModification> ModificationParams
        {
            get { return this._modificationParams; }
            set
            {
                this._modificationParams = value;
                if (this._modificationParams != null)
                {
                    this._modificationParams.IdentData = this.IdentData;
                }
            }
        }

        /// min 0, max 1
        public EnzymeList Enzymes
        {
            get { return this._enzymes; }
            set
            {
                this._enzymes = value;
                if (this._enzymes != null)
                {
                    this._enzymes.IdentData = this.IdentData;
                }
            }
        }

        /// min 0, max unbounded
        public IdentDataList<MassTable> MassTable
        {
            get { return this._massTable; }
            set
            {
                this._massTable = value;
                if (this._massTable != null)
                {
                    this._massTable.IdentData = this.IdentData;
                }
            }
        }

        /// min 0, max 1 : Original ToleranceType
        public IdentDataList<CVParam> FragmentTolerance
        {
            get { return this._fragmentTolerance; }
            set
            {
                this._fragmentTolerance = value;
                if (this._fragmentTolerance != null)
                {
                    this._fragmentTolerance.IdentData = this.IdentData;
                }
            }
        }

        /// min 0, max 1 : Original ToleranceType
        public IdentDataList<CVParam> ParentTolerance
        {
            get { return this._parentTolerance; }
            set
            {
                this._parentTolerance = value;
                if (this._fragmentTolerance != null)
                {
                    this._fragmentTolerance.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>The threshold(s) applied to determine that a result is significant. If multiple terms are used it is assumed that all conditions are satisfied by the passing results.</remarks>
        /// min 1, max 1
        public ParamList Threshold
        {
            get { return this._threshold; }
            set
            {
                this._threshold = value;
                if (this._threshold != null)
                {
                    this._threshold.IdentData = this.IdentData;
                }
            }
        }

        /// min 0, max 1 : Original DatabaseFiltersType
        public IdentDataList<FilterInfo> DatabaseFilters
        {
            get { return this._databaseFilters; }
            set
            {
                this._databaseFilters = value;
                if (this._databaseFilters != null)
                {
                    this._databaseFilters.IdentData = this.IdentData;
                }
            }
        }

        /// min 0, max 1
        public DatabaseTranslation DatabaseTranslation
        {
            get { return this._databaseTranslation; }
            set
            {
                this._databaseTranslation = value;
                if (this._databaseTranslation != null)
                {
                    this._databaseTranslation.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>The search algorithm used, given as a reference to the SoftwareCollection section.</remarks>
        /// Required Attribute
        /// string
        public string AnalysisSoftwareRef
        {
            get { return this._analysisSoftwareRef; }
            set { _analysisSoftwareRef = value; }
        }
    }

    /// <summary>
    /// MzIdentML SpecificityRulesType
    /// </summary>
    /// <remarks>The specificity rules of the searched modification including for example 
    /// the probability of a modification's presence or peptide or protein termini. Standard 
    /// fixed or variable status should be provided by the attribute fixedMod.</remarks>
    public partial class SpecificityRulesList : CVParamGroup
    {
        //private IdentDataList<CVParamType> _cvParams;

        /// min 1, max unbounded
        //public IdentDataList<CVParamType> CVParams
        //{
        //    get { return this._cvParams; }
        //    set
        //    {
        //        this._cvParams = value;
        //        if (this._cvParams != null)
        //        {
        //            this._cvParams.IdentData = this.IdentData;
        //        }
        //    }
        //}
    }

    /// <summary>
    /// MzIdentML SearchModificationType: Container ModificationParamsType
    /// </summary>
    /// <remarks>Specification of a search modification as parameter for a spectra search. Contains the name of the 
    /// modification, the mass, the specificity and whether it is a static modification.</remarks>
    /// <remarks>ModificationParamsType: The specification of static/variable modifications (e.g. Oxidation of Methionine) that are to be considered in the spectra search.</remarks>
    /// <remarks>ModificationParamsType: child element SearchModification, of type SearchModificationType, min 1, max unbounded</remarks>
    public partial class SearchModification : CVParamGroup
    {
        private IdentDataList<SpecificityRulesList> _specificityRules;
        //private IdentDataList<CVParamType> _cvParams;
        private bool _fixedMod;
        private float _massDelta;
        private string _residues;

        /// min 0, max unbounded
        public IdentDataList<SpecificityRulesList> SpecificityRules
        {
            get { return this._specificityRules; }
            set
            {
                this._specificityRules = value;
                if (this._specificityRules != null)
                {
                    this._specificityRules.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>The modification is uniquely identified by references to external CVs such as UNIMOD, see 
        /// specification document and mapping file for more details.</remarks>
        /// min 1, max unbounded
        //public IdentDataList<CVParamType> CVParams
        //{
        //    get { return this._cvParams; }
        //    set
        //    {
        //        this._cvParams = value;
        //        if (this._cvParams != null)
        //        {
        //            this._cvParams.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>True, if the modification is static (i.e. occurs always).</remarks>
        /// Required Attribute
        /// boolean
        public bool FixedMod
        {
            get { return this._fixedMod; }
            set { this._fixedMod = value; }
        }

        /// <remarks>The mass delta of the searched modification in Daltons.</remarks>
        /// Required Attribute
        /// float
        public float MassDelta
        {
            get { return this._massDelta; }
            set { this._massDelta = value; }
        }

        /// <remarks>The residue(s) searched with the specified modification. For N or C terminal modifications that can occur 
        /// on any residue, the . character should be used to specify any, otherwise the list of amino acids should be provided.</remarks>
        /// Required Attribute
        /// listOfCharsOrAny: string, space-separated regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}|."
        public string Residues
        {
            get { return this._residues; }
            set { this._residues = value; }
        }
    }

    /// <summary>
    /// MzIdentML EnzymesType
    /// </summary>
    /// <remarks>The list of enzymes used in experiment</remarks>
    public partial class EnzymeList : IdentDataInternalTypeAbstract
    {
        private IdentDataList<Enzyme> _enzymes;
        private bool _independent;

        /// min 1, max unbounded
        public IdentDataList<Enzyme> Enzymes
        {
            get { return this._enzymes; }
            set
            {
                this._enzymes = value;
                if (this._enzymes != null)
                {
                    this._enzymes.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>If there are multiple enzymes specified, this attribute is set to true if cleavage with different enzymes is performed independently.</remarks>
        /// Optional Attribute
        /// boolean
        public bool Independent
        {
            get { return this._independent; }
            set
            {
                this._independent = value;
                this.IndependentSpecified = true;
            }
        }

        /// Attribute Existence
        public bool IndependentSpecified { get; private set; }
    }

    /// <summary>
    /// MzIdentML FilterType : Containers DatabaseFiltersType
    /// </summary>
    /// <remarks>Filters applied to the search database. The filter must include at least one of Include and Exclude. 
    /// If both are used, it is assumed that inclusion is performed first.</remarks>
    /// <remarks>DatabaseFiltersType: The specification of filters applied to the database searched.</remarks>
    /// <remarks>DatabaseFiltersType: child element Filter, of type FilterType, min 1, max unbounded</remarks>
    public partial class FilterInfo : IdentDataInternalTypeAbstract
    {
        private Param _filterType;
        private ParamList _include;
        private ParamList _exclude;

        /// <remarks>The type of filter e.g. database taxonomy filter, pi filter, mw filter</remarks>
        /// min 1, max 1
        public Param FilterType
        {
            get { return this._filterType; }
            set
            {
                _filterType = value;
                if (this._filterType != null)
                {
                    this._filterType.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>All sequences fulfilling the specifed criteria are included.</remarks>
        /// min 0, max 1
        public ParamList Include
        {
            get { return this._include; }
            set
            {
                this._include = value;
                if (this._include != null)
                {
                    this._include.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>All sequences fulfilling the specifed criteria are excluded.</remarks>
        /// min 0, max 1
        public ParamList Exclude
        {
            get { return this._exclude; }
            set
            {
                this._exclude = value;
                if (this._exclude != null)
                {
                    this._exclude.IdentData = this.IdentData;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML DatabaseTranslationType
    /// </summary>
    /// <remarks>A specification of how a nucleic acid sequence database was translated for searching.</remarks>
    public partial class DatabaseTranslation : IdentDataInternalTypeAbstract
    {
        private IdentDataList<TranslationTable> _translationTable;
        private List<int> _frames;

        /// min 1, max unbounded
        public IdentDataList<TranslationTable> TranslationTable
        {
            get { return this._translationTable; }
            set
            {
                this._translationTable = value;
                if (this._translationTable != null)
                {
                    this._translationTable.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>The frames in which the nucleic acid sequence has been translated as a space separated IdentDataList</remarks>
        /// Optional Attribute
        /// listOfAllowedFrames: space-separated string, valid values -3, -2, -1, 1, 2, 3
        public List<int> Frames
        {
            get { return this._frames; }
            set { this._frames = value; }
        }
    }

    /// <summary>
    /// MzIdentML ProtocolApplicationType
    /// </summary>
    /// <remarks>The use of a protocol with the requisite Parameters and ParameterValues. 
    /// ProtocolApplications can take Material or Data (or both) as input and produce Material or Data (or both) as output.</remarks>
    public abstract partial class ProtocolApplication : IdentDataInternalTypeAbstract, IIdentifiableType
    {
        private System.DateTime _activityDate;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <remarks>When the protocol was applied.</remarks>
        /// Optional Attribute
        /// datetime
        public System.DateTime ActivityDate
        {
            get { return this._activityDate; }
            set
            {
                this._activityDate = value;
                this.ActivityDateSpecified = true;
            }
        }

        /// Attribute Existence
        public bool ActivityDateSpecified { get; private set; }
    }

    /// <summary>
    /// MzIdentML ProteinDetectionType
    /// </summary>
    /// <remarks>An Analysis which assembles a set of peptides (e.g. from a spectra search analysis) to proteins.</remarks>
    public partial class ProteinDetection : ProtocolApplication
    {
        private IdentDataList<InputSpectrumIdentifications> _inputSpectrumIdentifications;
        private string _proteinDetectionListRef;
        private string _proteinDetectionProtocolRef;

        /// min 1, max unbounded
        public IdentDataList<InputSpectrumIdentifications> InputSpectrumIdentifications
        {
            get { return this._inputSpectrumIdentifications; }
            set
            {
                this._inputSpectrumIdentifications = value;
                if (this._inputSpectrumIdentifications != null)
                {
                    this._inputSpectrumIdentifications.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>A reference to the ProteinDetectionList in the DataCollection section.</remarks>
        /// Required Attribute
        /// string
        public string ProteinDetectionListRef
        {
            get { return this._proteinDetectionListRef; }
            set { _proteinDetectionListRef = value; }
        }

        /// <remarks>A reference to the detection protocol used for this ProteinDetection.</remarks>
        /// Required Attribute
        /// string
        public string ProteinDetectionProtocolRef
        {
            get { return this._proteinDetectionProtocolRef; }
            set { _proteinDetectionProtocolRef = value; }
        }
    }

    /// <summary>
    /// MzIdentML InputSpectrumIdentificationsType
    /// </summary>
    /// <remarks>The lists of spectrum identifications that are input to the protein detection process.</remarks>
    public partial class InputSpectrumIdentifications : IdentDataInternalTypeAbstract
    {
        private string _spectrumIdentificationListRef;

        /// <remarks>A reference to the list of spectrum identifications that were input to the process.</remarks>
        /// Required Attribute
        /// string
        public string SpectrumIdentificationListRef
        {
            get { return this._spectrumIdentificationListRef; }
            set { _spectrumIdentificationListRef = value; }
        }
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationType
    /// </summary>
    /// <remarks>An Analysis which tries to identify peptides in input spectra, referencing the database searched, 
    /// the input spectra, the output results and the protocol that is run.</remarks>
    public partial class SpectrumIdentification : ProtocolApplication
    {
        private IdentDataList<InputSpectraRef> _inputSpectra;
        private IdentDataList<SearchDatabaseRefInfo> _searchDatabaseRef;
        private string _spectrumIdentificationProtocolRef;
        private string _spectrumIdentificationListRef;

        /// <remarks>One of the spectra data sets used.</remarks>
        /// min 1, max unbounded
        public IdentDataList<InputSpectraRef> InputSpectra
        {
            get { return this._inputSpectra; }
            set
            {
                this._inputSpectra = value;
                if (this._inputSpectra != null)
                {
                    this._inputSpectra.IdentData = this.IdentData;
                }
            }
        }

        /// min 1, max unbounded
        public IdentDataList<SearchDatabaseRefInfo> SearchDatabaseRef
        {
            get { return this._searchDatabaseRef; }
            set
            {
                this._searchDatabaseRef = value;
                if (this._searchDatabaseRef != null)
                {
                    this._searchDatabaseRef.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>A reference to the search protocol used for this SpectrumIdentification.</remarks>
        /// Required Attribute
        /// string
        public string SpectrumIdentificationProtocolRef
        {
            get { return this._spectrumIdentificationProtocolRef; }
            set { _spectrumIdentificationProtocolRef = value; }
        }

        /// <remarks>A reference to the SpectrumIdentificationList produced by this analysis in the DataCollection section.</remarks>
        /// Required Attribute
        /// string
        public string SpectrumIdentificationListRef
        {
            get { return this._spectrumIdentificationListRef; }
            set { _spectrumIdentificationListRef = value; }
        }
    }

    /// <summary>
    /// MzIdentML InputSpectraType
    /// </summary>
    /// <remarks>The attribute referencing an identifier within the SpectraData section.</remarks>
    public partial class InputSpectraRef : IdentDataInternalTypeAbstract
    {
        private string _spectraDataRef;

        /// <remarks>A reference to the SpectraData element which locates the input spectra to an external file.</remarks>
        /// Optional Attribute
        /// string
        public string SpectraDataRef
        {
            get { return this._spectraDataRef; }
            set { _spectraDataRef = value; }
        }
    }

    /// <summary>
    /// MzIdentML SearchDatabaseRefType
    /// </summary>
    /// <remarks>One of the search databases used.</remarks>
    public partial class SearchDatabaseRefInfo : IdentDataInternalTypeAbstract
    {
        private string _searchDatabaseRef;

        /// <remarks>A reference to the database searched.</remarks>
        /// Optional Attribute
        /// string
        public string SearchDatabaseRef
        {
            get { return this._searchDatabaseRef; }
            set { this._searchDatabaseRef = value; }
        }
    }

    /// <summary>
    /// MzIdentML PeptideEvidenceType
    /// </summary>
    /// <remarks>PeptideEvidence links a specific Peptide element to a specific position in a DBSequence. 
    /// There must only be one PeptideEvidence item per Peptide-to-DBSequence-position.</remarks>
    public partial class PeptideEvidence : ParamGroup, IIdentifiableType
    {
        //private IdentDataList<CVParamType> _cvParams;
        //private IdentDataList<UserParamType> _userParams;
        private string _dBSequenceRef;
        private string _peptideRef;
        private int _start;
        private int _end;
        private string _pre;
        private string _post;
        private string _translationTableRef;
        private int _frame;
        private bool _isDecoy;
        private string _id;
        private string _name;

        // Taken care of elsewhere
        //public PeptideEvidence()
        //{
        //    _isDecoy = false;
        //}

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <remarks>___ParamGroup___: Additional parameters or descriptors for the PeptideEvidence.</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams
        //{
        //    get { return this._cvParams; }
        //    set
        //    {
        //        this._cvParams = value;
        //        if (this._cvParams != null)
        //        {
        //            this._cvParams.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams
        //{
        //    get { return this._userParams; }
        //    set
        //    {
        //        this._userParams = value;
        //        if (this._userParams != null)
        //        {
        //            this._userParams.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>Set to true if the peptide is matched to a decoy sequence.</remarks>
        /// Optional Attribute
        /// boolean, default false
        public bool IsDecoy
        {
            get { return this._isDecoy; }
            set { this._isDecoy = value; }
        }

        /// <remarks>Previous flanking residue. If the peptide is N-terminal, pre="-" and not pre="". 
        /// If for any reason it is unknown (e.g. denovo), pre="?" should be used.</remarks>
        /// Optional Attribute
        /// string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"
        public string Pre
        {
            get { return this._pre; }
            set { this._pre = value; }
        }

        /// <remarks>Post flanking residue. If the peptide is C-terminal, post="-" and not post="". If for any reason it is unknown (e.g. denovo), post="?" should be used.</remarks>
        /// Optional Attribute
        /// string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"
        public string Post
        {
            get { return this._post; }
            set { this._post = value; }
        }

        /// <remarks>Start position of the peptide inside the protein sequence, where the first amino acid of the 
        /// protein sequence is position 1. Must be provided unless this is a de novo search.</remarks>
        /// Optional Attribute
        /// integer
        public int Start
        {
            get { return this._start; }
            set
            {
                this._start = value;
                this.StartSpecified = true;
            }
        }

        /// Attribute Existence
        public bool StartSpecified { get; private set; }

        /// <remarks>The index position of the last amino acid of the peptide inside the protein sequence, where the first 
        /// amino acid of the protein sequence is position 1. Must be provided unless this is a de novo search.</remarks>
        /// Optional Attribute
        /// integer
        public int End
        {
            get { return this._end; }
            set
            {
                this._end = value;
                this.EndSpecified = true;
            }
        }

        /// Attribute Existence
        public bool EndSpecified { get; private set; }

        /// <remarks>A reference to the translation table used if this is PeptideEvidence derived from nucleic acid sequence</remarks>
        /// Optional Attribute
        /// string
        public string TranslationTable_ref
        {
            get { return _translationTableRef; }
            set { _translationTableRef = value; }
        }

        /// <remarks>The translation frame of this sequence if this is PeptideEvidence derived from nucleic acid sequence</remarks>
        /// Optional Attribute
        /// "Allowed Frames", int: -3, -2, -1, 1, 2, 3 
        public int Frame
        {
            get { return this._frame; }
            set
            {
                this._frame = value;
                this.FrameSpecified = true;
            }
        }

        /// Attribute Existence
        public bool FrameSpecified { get; private set; }

        /// <remarks>A reference to the identified (poly)peptide sequence in the Peptide element.</remarks>
        /// Required Attribute
        /// string
        public string PeptideRef
        {
            get { return _peptideRef; }
            set { _peptideRef = value; }
        }

        /// <remarks>A reference to the protein sequence in which the specified peptide has been linked.</remarks>
        /// Required Attribute
        /// string
        public string DBSequenceRef
        {
            get { return _dBSequenceRef; }
            set { _dBSequenceRef = value; }
        }
    }

    /// <summary>
    /// MzIdentML PeptideType
    /// </summary>
    /// <remarks>One (poly)peptide (a sequence with modifications). The combination of Peptide sequence and modifications must be unique in the file.</remarks>
    public partial class Peptide : ParamGroup, IIdentifiableType
    {
        private string _peptideSequence;
        private IdentDataList<Modification> _modification;
        private IdentDataList<SubstitutionModification> _substitutionModification;
        //private IdentDataList<CVParamType> _cvParams;
        //private IdentDataList<UserParamType> _userParams;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <remarks>The amino acid sequence of the (poly)peptide. If a substitution modification has been found, the original sequence should be reported.</remarks>
        /// min 1, max 1
        public string PeptideSequence
        {
            get { return this._peptideSequence; }
            set { this._peptideSequence = value; }
        }

        /// min 0, max unbounded
        public IdentDataList<Modification> Modification
        {
            get { return this._modification; }
            set
            {
                this._modification = value;
                if (this._modification != null)
                {
                    this._modification.IdentData = this.IdentData;
                }
            }
        }

        /// min 0, max unbounded
        public IdentDataList<SubstitutionModification> SubstitutionModification
        {
            get { return this._substitutionModification; }
            set
            {
                this._substitutionModification = value;
                if (this._substitutionModification != null)
                {
                    this._substitutionModification.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>___ParamGroup___: Additional descriptors of this peptide sequence</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams
        //{
        //    get { return this._cvParams; }
        //    set
        //    {
        //        this._cvParams = value;
        //        if (this._cvParams != null)
        //        {
        //            this._cvParams.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams
        //{
        //    get { return this._userParams; }
        //    set
        //    {
        //        this._userParams = value;
        //        if (this._userParams != null)
        //        {
        //            this._userParams.IdentData = this.IdentData;
        //        }
        //    }
        //}
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
    public partial class Modification : CVParamGroup
    {
        //private IdentDataList<CVParamType> _cvParams;
        private int _location;
        private List<string> _residues;
        private double _avgMassDelta;
        private double _monoisotopicMassDelta;

        /// <remarks>CV terms capturing the modification, sourced from an appropriate controlled vocabulary.</remarks>
        /// min 1, max unbounded
        //public IdentDataList<CVParamType> CVParams
        //{
        //    get { return this._cvParams; }
        //    set
        //    {
        //        this._cvParams = value;
        //        if (this._cvParams != null)
        //        {
        //            this._cvParams.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>Location of the modification within the peptide - position in peptide sequence, counted from 
        /// the N-terminus residue, starting at position 1. Specific modifications to the N-terminus should be 
        /// given the location 0. Modification to the C-terminus should be given as peptide length + 1. If the 
        /// modification location is unknown e.g. for PMF data, this attribute should be omitted.</remarks>
        /// Optional Attribute
        /// integer
        public int Location
        {
            get { return this._location; }
            set
            {
                this._location = value;
                this.LocationSpecified = true;
            }
        }

        /// Attribute Existence
        public bool LocationSpecified { get; private set; }

        /// <remarks>Specification of the residue (amino acid) on which the modification occurs. If multiple values 
        /// are given, it is assumed that the exact residue modified is unknown i.e. the modification is to ONE of 
        /// the residues listed. Multiple residues would usually only be specified for PMF data.</remarks>
        /// Optional Attribute
        /// listOfChars, string, space-separated regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}"
        public List<string> Residues
        {
            get { return this._residues; }
            set { this._residues = value; }
        }

        /// <remarks>Atomic mass delta considering the natural distribution of isotopes in Daltons.</remarks>
        /// Optional Attribute
        /// double
        public double AvgMassDelta
        {
            get { return this._avgMassDelta; }
            set
            {
                this._avgMassDelta = value;
                this.AvgMassDeltaSpecified = true;
            }
        }

        /// Attribute Existence
        public bool AvgMassDeltaSpecified { get; private set; }

        /// <remarks>Atomic mass delta when assuming only the most common isotope of elements in Daltons.</remarks>
        /// Optional Attribute
        /// double
        public double MonoisotopicMassDelta
        {
            get { return this._monoisotopicMassDelta; }
            set
            {
                this._monoisotopicMassDelta = value;
                this.MonoisotopicMassDeltaSpecified = true;
            }
        }

        /// Attribute Existence
        public bool MonoisotopicMassDeltaSpecified { get; private set; }
    }

    /// <summary>
    /// MzIdentML SubstitutionModificationType
    /// </summary>
    /// <remarks>A modification where one residue is substituted by another (amino acid change).</remarks>
    public partial class SubstitutionModification : IdentDataInternalTypeAbstract
    {
        private string _originalResidue;
        private string _replacementResidue;
        private int _location;
        private double _avgMassDelta;
        private double _monoisotopicMassDelta;

        /// <remarks>The original residue before replacement.</remarks>
        /// Required Attribute
        /// string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"
        public string OriginalResidue
        {
            get { return this._originalResidue; }
            set { this._originalResidue = value; }
        }

        /// <remarks>The residue that replaced the originalResidue.</remarks>
        /// Required Attribute
        /// string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"
        public string ReplacementResidue
        {
            get { return this._replacementResidue; }
            set { this._replacementResidue = value; }
        }

        /// <remarks>Location of the modification within the peptide - position in peptide sequence, counted from the N-terminus residue, starting at position 1. 
        /// Specific modifications to the N-terminus should be given the location 0. 
        /// Modification to the C-terminus should be given as peptide length + 1.</remarks>
        /// Optional Attribute
        /// integer
        public int Location
        {
            get { return this._location; }
            set
            {
                this._location = value;
                this.LocationSpecified = true;
            }
        }

        /// Attribute Existence
        public bool LocationSpecified { get; private set; }

        /// <remarks>Atomic mass delta considering the natural distribution of isotopes in Daltons. 
        /// This should only be reported if the original amino acid is known i.e. it is not "X"</remarks>
        /// Optional Attribute
        /// double
        public double AvgMassDelta
        {
            get { return this._avgMassDelta; }
            set
            {
                this._avgMassDelta = value;
                this.AvgMassDeltaSpecified = true;
            }
        }

        /// Attribute Existence
        public bool AvgMassDeltaSpecified { get; private set; }

        /// <remarks>Atomic mass delta when assuming only the most common isotope of elements in Daltons. 
        /// This should only be reported if the original amino acid is known i.e. it is not "X"</remarks>
        /// Optional Attribute
        /// double
        public double MonoisotopicMassDelta
        {
            get { return this._monoisotopicMassDelta; }
            set
            {
                this._monoisotopicMassDelta = value;
                this.MonoisotopicMassDeltaSpecified = true;
            }
        }

        /// Attribute Existence
        public bool MonoisotopicMassDeltaSpecified { get; private set; }
    }

    /// <summary>
    /// MzIdentML DBSequenceType
    /// </summary>
    /// <remarks>A database sequence from the specified SearchDatabase (nucleic acid or amino acid). 
    /// If the sequence is nucleic acid, the source nucleic acid sequence should be given in 
    /// the seq attribute rather than a translated sequence.</remarks>
    public partial class DBSequence : ParamGroup, IIdentifiableType
    {
        private string _seq;
        //private IdentDataList<CVParamType> _cvParams;
        //private IdentDataList<UserParamType> _userParams;
        private int _length;
        private string _searchDatabaseRef;
        private string _accession;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <remarks>The actual sequence of amino acids or nucleic acid.</remarks>
        /// min 0, max 1
        /// string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]*"
        public string Seq
        {
            get { return this._seq; }
            set { this._seq = value; }
        }

        /// <remarks>___ParamGroup___: Additional descriptors for the sequence, such as taxon, description line etc.</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams
        //{
        //    get { return this._cvParams; }
        //    set
        //    {
        //        this._cvParams = value;
        //        if (this._cvParams != null)
        //        {
        //            this._cvParams.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams
        //{
        //    get { return this._userParams; }
        //    set
        //    {
        //        this._userParams = value;
        //        if (this._userParams != null)
        //        {
        //            this._userParams.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>The unique accession of this sequence.</remarks>
        /// Required Attribute
        public string Accession
        {
            get { return this._accession; }
            set { this._accession = value; }
        }

        /// <remarks>The source database of this sequence.</remarks>
        /// Required Attribute
        /// string
        public string SearchDatabaseRef
        {
            get { return _searchDatabaseRef; }
            set { _searchDatabaseRef = value; }
        }

        /// <remarks>The length of the sequence as a number of bases or residues.</remarks>
        /// Optional Attribute
        /// integer
        public int Length
        {
            get { return this._length; }
            set
            {
                this._length = value;
                this.LengthSpecified = true;
            }
        }

        /// Attribute Existence
        public bool LengthSpecified { get; private set; }
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
    public partial class SampleInfo : ParamGroup, IIdentifiableType
    {
        private IdentDataList<ContactRoleInfo> _contactRoles;
        private IdentDataList<SubSample> _subSamples;
        //private IdentDataList<CVParamType> _cvParams;
        //private IdentDataList<UserParamType> _userParams;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <remarks>Contact details for the Material. The association to ContactRole could specify, for example, the creator or provider of the Material.</remarks>
        /// min 0, max unbounded
        public IdentDataList<ContactRoleInfo> ContactRoles
        {
            get { return this._contactRoles; }
            set
            {
                this._contactRoles = value;
                if (this._contactRoles != null)
                {
                    this._contactRoles.IdentData = this.IdentData;
                }
            }
        }

        /// min 0, max unbounded
        public IdentDataList<SubSample> SubSamples
        {
            get { return this._subSamples; }
            set
            {
                this._subSamples = value;
                if (this._subSamples != null)
                {
                    this._subSamples.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>___ParamGroup___: The characteristics of a Material.</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams
        //{
        //    get { return this._cvParams; }
        //    set
        //    {
        //        this._cvParams = value;
        //        if (this._cvParams != null)
        //        {
        //            this._cvParams.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams
        //{
        //    get { return this._userParams; }
        //    set
        //    {
        //        this._userParams = value;
        //        if (this._userParams != null)
        //        {
        //            this._userParams.IdentData = this.IdentData;
        //        }
        //    }
        //}
    }

    /// <summary>
    /// MzIdentML ContactRoleType
    /// </summary>
    /// <remarks>The role that a Contact plays in an organization or with respect to the associating class. 
    /// A Contact may have several Roles within scope, and as such, associations to ContactRole 
    /// allow the use of a Contact in a certain manner. Examples might include a provider, or a data analyst.</remarks>
    public partial class ContactRoleInfo : IdentDataInternalTypeAbstract
    {
        private RoleInfo _role;
        private string _contactRef;

        /// min 1, max 1
        public RoleInfo Role
        {
            get { return this._role; }
            set
            {
                this._role = value;
                if (this._role != null)
                {
                    this._role.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>When a ContactRole is used, it specifies which Contact the role is associated with.</remarks>
        /// Required Attribute
        /// string
        public string ContactRef
        {
            get { return _contactRef; }
            set { _contactRef = value; }
        }
    }

    /// <summary>
    /// MzIdentML RoleType
    /// </summary>
    /// <remarks>The roles (lab equipment sales, contractor, etc.) the Contact fills.</remarks>
    public partial class RoleInfo : IdentDataInternalTypeAbstract
    {
        private CVParam _cvParam;

        /// <remarks>CV term for contact roles, such as software provider.</remarks>
        /// min 1, max 1
        public CVParam CVParam
        {
            get { return this._cvParam; }
            set
            {
                this._cvParam = value;
                if (this._cvParam != null)
                {
                    this._cvParam.IdentData = this.IdentData;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML SubSampleType
    /// </summary>
    /// <remarks>References to the individual component samples within a mixed parent sample.</remarks>
    public partial class SubSample : IdentDataInternalTypeAbstract
    {
        private string _sampleRef;

        /// <remarks>A reference to the child sample.</remarks>
        /// Required  Attribute
        /// string
        public string SampleRef
        {
            get { return this._sampleRef; }
            set { _sampleRef = value; }
        }
    }

    /// <summary>
    /// MzIdentML AuditCollectionType; Also C# representation of AuditCollectionType
    /// </summary>
    /// <remarks>A contact is either a person or an organization.</remarks>
    /// <remarks>AuditCollectionType: The complete set of Contacts (people and organisations) for this file.</remarks>
    /// <remarks>AuditCollectionType: min 1, max unbounded, for PersonType XOR OrganizationType</remarks>
    public abstract partial class AbstractContactInfo : ParamGroup, IIdentifiableType
    {
        //private IdentDataList<CVParamType> _cvParams;
        //private IdentDataList<UserParamType> _userParams;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <remarks>___ParamGroup___: Attributes of this contact such as address, email, telephone etc.</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams
        //{
        //    get { return this._cvParams; }
        //    set
        //    {
        //        this._cvParams = value;
        //        if (this._cvParams != null)
        //        {
        //            this._cvParams.IdentData = this.IdentData;
        //        }
        //    }
        //}

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams
        //{
        //    get { return this._userParams; }
        //    set
        //    {
        //        this._userParams = value;
        //        if (this._userParams != null)
        //        {
        //            this._userParams.IdentData = this.IdentData;
        //        }
        //    }
        //}
    }

    /// <summary>
    /// MzIdentML OrganizationType
    /// </summary>
    /// <remarks>Organizations are entities like companies, universities, government agencies. 
    /// Any additional information such as the address, email etc. should be supplied either as CV parameters or as user parameters.</remarks>
    public partial class Organization : AbstractContactInfo
    {
        private ParentOrganization _parent;

        /// min 0, max 1
        public ParentOrganization Parent
        {
            get { return this._parent; }
            set
            {
                this._parent = value;
                if (this._parent != null)
                {
                    this._parent.IdentData = this.IdentData;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML ParentOrganizationType
    /// </summary>
    /// <remarks>The containing organization (the university or business which a lab belongs to, etc.)</remarks>
    public partial class ParentOrganization : IdentDataInternalTypeAbstract
    {
        private string _organizationRef;

        /// <remarks>A reference to the organization this contact belongs to.</remarks>
        /// Required Attribute
        /// string
        public string OrganizationRef
        {
            get { return this._organizationRef; }
            set { _organizationRef = value; }
        }
    }

    /// <summary>
    /// MzIdentML PersonType
    /// </summary>
    /// <remarks>A person's name and contact details. Any additional information such as the address, 
    /// contact email etc. should be supplied using CV parameters or user parameters.</remarks>
    public partial class PersonInfo : AbstractContactInfo
    {
        private IdentDataList<AffiliationInfo> _affiliation;
        private string _lastName;
        private string _firstName;
        private string _midInitials;

        /// <remarks>The organization a person belongs to.</remarks>
        /// min 0, max unbounded
        public IdentDataList<AffiliationInfo> Affiliation
        {
            get { return this._affiliation; }
            set
            {
                this._affiliation = value;
                if (this._affiliation != null)
                {
                    this._affiliation.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>The Person's last/family name.</remarks>
        /// Optional Attribute
        public string LastName
        {
            get { return this._lastName; }
            set { this._lastName = value; }
        }

        /// <remarks>The Person's first name.</remarks>
        /// Optional Attribute
        public string FirstName
        {
            get { return this._firstName; }
            set { this._firstName = value; }
        }

        /// <remarks>The Person's middle initial.</remarks>
        /// Optional Attribute
        public string MidInitials
        {
            get { return this._midInitials; }
            set { this._midInitials = value; }
        }
    }

    /// <summary>
    /// MzIdentML AffiliationType
    /// </summary>
    public partial class AffiliationInfo : IdentDataInternalTypeAbstract
    {
        private string _organizationRef;

        /// <remarks>>A reference to the organization this contact belongs to.</remarks>
        /// Required Attribute
        /// string
        public string OrganizationRef
        {
            get { return this._organizationRef; }
            set { this._organizationRef = value; }
        }
    }

    /// <summary>
    /// MzIdentML ProviderType
    /// </summary>
    /// <remarks>The provider of the document in terms of the Contact and the software the produced the document instance.</remarks>
    public partial class ProviderInfo : IdentDataInternalTypeAbstract, IIdentifiableType
    {
        private ContactRoleInfo _contactRole;
        private string _analysisSoftwareRef;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <remarks>The Contact that provided the document instance.</remarks>
        /// min 0, max 1
        public ContactRoleInfo ContactRole
        {
            get { return this._contactRole; }
            set
            {
                this._contactRole = value;
                if (this._contactRole != null)
                {
                    this._contactRole.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>The Software that produced the document instance.</remarks>
        /// Optional Attribute
        /// string
        public string AnalysisSoftwareRef
        {
            get { return this._analysisSoftwareRef; }
            set { this._analysisSoftwareRef = value; }
        }
    }

    /// <summary>
    /// MzIdentML AnalysisSoftwareType : Containers AnalysisSoftwareListType
    /// </summary>
    /// <remarks>The software used for performing the analyses.</remarks>
    /// 
    /// <remarks>AnalysisSoftwareListType: The software packages used to perform the analyses.</remarks>
    /// <remarks>AnalysisSoftwareListType: child element AnalysisSoftware of type AnalysisSoftwareType, min 1, max unbounded</remarks>
    public partial class AnalysisSoftwareInfo : IdentDataInternalTypeAbstract, IIdentifiableType
    {
        private ContactRoleInfo _contactRole;
        private Param _softwareName;
        private string _customizations;
        private string _version;
        private string _uri;
        private string _id;
        private string _name;

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <remarks>The contact details of the organisation or person that produced the software</remarks>
        /// min 0, max 1
        public ContactRoleInfo ContactRole
        {
            get { return this._contactRole; }
            set
            {
                this._contactRole = value;
                if (this._contactRole != null)
                {
                    this._contactRole.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>The name of the analysis software package, sourced from a CV if available.</remarks>
        /// min 1, max 1
        public Param SoftwareName
        {
            get { return this._softwareName; }
            set
            {
                this._softwareName = value;
                if (this._softwareName != null)
                {
                    this._softwareName.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>Any customizations to the software, such as alternative scoring mechanisms implemented, should be documented here as free text.</remarks>
        /// min 0, max 1
        public string Customizations
        {
            get { return this._customizations; }
            set { this._customizations = value; }
        }

        /// <remarks>The version of Software used.</remarks>
        /// Optional Attribute
        /// string
        public string Version
        {
            get { return this._version; }
            set { this._version = value; }
        }

        /// <remarks>URI of the analysis software e.g. manufacturer's website</remarks>
        /// Optional Attribute
        /// anyURI
        public string URI
        {
            get { return this._uri; }
            set { this._uri = value; }
        }
    }

    /// <summary>
    /// MzIdentML InputsType
    /// </summary>
    /// <remarks>The inputs to the analyses including the databases searched, the spectral data and the source file converted to mzIdentML.</remarks>
    public partial class InputsInfo : IdentDataInternalTypeAbstract
    {
        private IdentDataList<SourceFileInfo> _sourceFile;
        private IdentDataList<SearchDatabaseInfo> _searchDatabase;
        private IdentDataList<SpectraData> _spectraData;

        /// min 0, max unbounded
        public IdentDataList<SourceFileInfo> SourceFile
        {
            get { return this._sourceFile; }
            set
            {
                this._sourceFile = value;
                if (this._sourceFile != null)
                {
                    this._sourceFile.IdentData = this.IdentData;
                }
            }
        }

        /// min 0, max unbounded
        public IdentDataList<SearchDatabaseInfo> SearchDatabase
        {
            get { return this._searchDatabase; }
            set
            {
                this._searchDatabase = value;
                if (this._searchDatabase != null)
                {
                    this._searchDatabase.IdentData = this.IdentData;
                }
            }
        }

        /// min 1, max unbounde
        public IdentDataList<SpectraData> SpectraData
        {
            get { return this._spectraData; }
            set
            {
                this._spectraData = value;
                if (this._spectraData != null)
                {
                    this._spectraData.IdentData = this.IdentData;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML DataCollectionType
    /// </summary>
    /// <remarks>The collection of input and output data sets of the analyses.</remarks>
    public partial class DataCollection : IdentDataInternalTypeAbstract
    {
        private InputsInfo _inputs;
        private AnalysisData _analysisData;

        /// min 1, max 1
        public InputsInfo Inputs
        {
            get { return this._inputs; }
            set
            {
                this._inputs = value;
                if (this._inputs != null)
                {
                    this._inputs.IdentData = this.IdentData;
                }
            }
        }

        /// min 1, max 1
        public AnalysisData AnalysisData
        {
            get { return this._analysisData; }
            set
            {
                this._analysisData = value;
                if (this._analysisData != null)
                {
                    this._analysisData.IdentData = this.IdentData;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML AnalysisProtocolCollectionType
    /// </summary>
    /// <remarks>The collection of protocols which include the parameters and settings of the performed analyses.</remarks>
    public partial class AnalysisProtocolCollection : IdentDataInternalTypeAbstract
    {
        private IdentDataList<SpectrumIdentificationProtocol> _spectrumIdentificationProtocol;
        private ProteinDetectionProtocol _proteinDetectionProtocol;

        /// min 1, max unbounded
        public IdentDataList<SpectrumIdentificationProtocol> SpectrumIdentificationProtocol
        {
            get { return this._spectrumIdentificationProtocol; }
            set
            {
                this._spectrumIdentificationProtocol = value;
                if (this._spectrumIdentificationProtocol != null)
                {
                    this._spectrumIdentificationProtocol.IdentData = this.IdentData;
                }
            }
        }

        /// min 0, max 1
        public ProteinDetectionProtocol ProteinDetectionProtocol
        {
            get { return this._proteinDetectionProtocol; }
            set
            {
                this._proteinDetectionProtocol = value;
                if (this._proteinDetectionProtocol != null)
                {
                    this._proteinDetectionProtocol.IdentData = this.IdentData;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML AnalysisCollectionType
    /// </summary>
    /// <remarks>The analyses performed to get the results, which map the input and output data sets. 
    /// Analyses are for example: SpectrumIdentification (resulting in peptides) or ProteinDetection (assemble proteins from peptides).</remarks>
    public partial class AnalysisCollection : IdentDataInternalTypeAbstract
    {
        private IdentDataList<SpectrumIdentification> _spectrumIdentification;
        private ProteinDetection _proteinDetection;

        /// min 1, max unbounded
        public IdentDataList<SpectrumIdentification> SpectrumIdentification
        {
            get { return this._spectrumIdentification; }
            set
            {
                this._spectrumIdentification = value;
                if (this._spectrumIdentification != null)
                {
                    this._spectrumIdentification.IdentData = this.IdentData;
                }
            }
        }

        /// min 0, max 1
        public ProteinDetection ProteinDetection
        {
            get { return this._proteinDetection; }
            set
            {
                this._proteinDetection = value;
                if (this._proteinDetection != null)
                {
                    this._proteinDetection.IdentData = this.IdentData;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML SequenceCollectionType
    /// </summary>
    /// <remarks>The collection of sequences (DBSequence or Peptide) identified and their relationship between 
    /// each other (PeptideEvidence) to be referenced elsewhere in the results.</remarks>
    public partial class SequenceCollection : IdentDataInternalTypeAbstract
    {
        private IdentDataList<DBSequence> _dBSequences;
        private IdentDataList<Peptide> _peptides;
        private IdentDataList<PeptideEvidence> _peptideEvidences;

        /// min 1, max unbounded
        public IdentDataList<DBSequence> DBSequences
        {
            get { return this._dBSequences; }
            set
            {
                this._dBSequences = value;
                if (this._dBSequences != null)
                {
                    this._dBSequences.IdentData = this.IdentData;
                }
            }
        }

        /// min 0, max unbounded
        public IdentDataList<Peptide> Peptides
        {
            get { return this._peptides; }
            set
            {
                this._peptides = value;
                if (this._peptides != null)
                {
                    this._peptides.IdentData = this.IdentData;
                }
            }
        }

        /// min 0, max unbounded
        public IdentDataList<PeptideEvidence> PeptideEvidences
        {
            get { return this._peptideEvidences; }
            set
            {
                this._peptideEvidences = value;
                if (this._peptideEvidences != null)
                {
                    this._peptideEvidences.IdentData = this.IdentData;
                }
            }
        }
    }
}
