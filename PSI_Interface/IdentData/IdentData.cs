using System;
using System.Collections.Generic;
using PSI_Interface.CV;

namespace PSI_Interface.IdentData
{
    /// <summary>
    /// MzIdentML MzIdentMLType
    /// </summary>
    /// <remarks>The upper-most hierarchy level of mzIdentML with sub-containers for example describing software, 
    /// protocols and search results (spectrum identifications or protein detection results).</remarks>
    public partial class IdentDataObj : IIdentifiableType
    {
        internal CVTranslator CvTranslator; // = new CVTranslator(); // Create a generic translator by default; must be re-mapped when reading a file
        private IdentDataList<CVInfo> _cvList;
        private IdentDataList<AnalysisSoftwareObj> _analysisSoftwareList;
        private ProviderObj _provider;
        private IdentDataList<AbstractContactObj> _auditCollection;
        private IdentDataList<SampleObj> _analysisSampleCollection;
        private SequenceCollectionObj _sequenceCollection;
        private AnalysisCollectionObj _analysisCollection;
        private AnalysisProtocolCollectionObj _analysisProtocolCollection;
        private DataCollectionObj _dataCollection;
        private IdentDataList<BibliographicReferenceObj> _bibliographicReferences;
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

        /// <remarks>min 1, max 1</remarks>
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

        /// <remarks>min 0, max 1</remarks>
        public IdentDataList<AnalysisSoftwareObj> AnalysisSoftwareList
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
        /// <remarks>min 0, max 1</remarks>
        public ProviderObj Provider
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

        /// <remarks>min 0, max 1</remarks>
        public IdentDataList<AbstractContactObj> AuditCollection
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

        /// <remarks>min 0, max 1</remarks>
        public IdentDataList<SampleObj> AnalysisSampleCollection
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

        /// <remarks>min 0, max 1</remarks>
        public SequenceCollectionObj SequenceCollection
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

        /// <remarks>min 1, max 1</remarks>
        public AnalysisCollectionObj AnalysisCollection
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

        /// <remarks>min 1, max 1</remarks>
        public AnalysisProtocolCollectionObj AnalysisProtocolCollection
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

        /// <remarks>min 1, max 1</remarks>
        public DataCollectionObj DataCollection
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
        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<BibliographicReferenceObj> BibliographicReferences
        {
            get { return this._bibliographicReferences; }
            set
            {
                this._bibliographicReferences = value;
                if (this._bibliographicReferences != null)
                {
                    this._bibliographicReferences.IdentData = this;
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
        protected internal bool CreationDateSpecified { get; private set; }

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
    public partial class SpectrumIdentificationItemRefObj : IdentDataInternalTypeAbstract
    {
        private string _spectrumIdentificationItemRef;
        private SpectrumIdentificationItemObj _spectrumIdentificationItem;

        /// <remarks>A reference to the SpectrumIdentificationItem element(s).</remarks>
        /// Required Attribute
        /// string
        protected internal string SpectrumIdentificationItemRef
        {
            get
            {
                if (this._spectrumIdentificationItem != null)
                {
                    return this._spectrumIdentificationItem.Id;
                }
                return this._spectrumIdentificationItemRef;
            }
            set
            {
                this._spectrumIdentificationItemRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.SpectrumIdentificationItem = this.IdentData.FindSpectrumIdentificationItem(value);
                }
            }
        }

        /// <remarks>A reference to the SpectrumIdentificationItem element(s).</remarks>
        /// Required Attribute
        /// string
        public SpectrumIdentificationItemObj SpectrumIdentificationItem
        {
            get { return this._spectrumIdentificationItem; }
            set
            {
                this._spectrumIdentificationItem = value;
                if (this._spectrumIdentificationItem != null)
                {
                    this._spectrumIdentificationItem.IdentData = this.IdentData;
                    this._spectrumIdentificationItemRef = this._spectrumIdentificationItem.Id;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML PeptideHypothesisType
    /// </summary>
    /// <remarks>Peptide evidence on which this ProteinHypothesis is based by reference to a PeptideEvidence element.</remarks>
    public partial class PeptideHypothesisObj : IdentDataInternalTypeAbstract
    {
        private IdentDataList<SpectrumIdentificationItemRefObj> _spectrumIdentificationItems;
        private string _peptideEvidenceRef;
        private PeptideEvidenceObj _peptideEvidence;

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<SpectrumIdentificationItemRefObj> SpectrumIdentificationItems
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

        /// <remarks>A reference to the PeptideEvidence element on which this hypothesis is based.</remarks>
        /// Required Attribute
        /// string
        protected internal string PeptideEvidenceRef
        {
            get
            {
                if (this._peptideEvidence != null)
                {
                    return this._peptideEvidence.Id;
                }
                return this._peptideEvidenceRef;
            }
            set
            {
                this._peptideEvidenceRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.PeptideEvidence = this.IdentData.FindPeptideEvidence(value);
                }
            }
        }

        /// <remarks>A reference to the PeptideEvidence element on which this hypothesis is based.</remarks>
        /// Required Attribute
        /// string
        public PeptideEvidenceObj PeptideEvidence
        {
            get { return this._peptideEvidence; }
            set
            {
                this._peptideEvidence = value;
                if (this._peptideEvidence != null)
                {
                    this._peptideEvidence.IdentData = this.IdentData;
                    this._peptideEvidenceRef = this._peptideEvidence.Id;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML FragmentArrayType
    /// </summary>
    /// <remarks>An array of values for a given type of measure and for a particular ion type, in parallel to the index of ions identified.</remarks>
    public partial class FragmentArrayObj : IdentDataInternalTypeAbstract
    {
        private List<float> _values;
        private string _measureRef;
        private MeasureObj _measure;

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
        protected internal string MeasureRef
        {
            get
            {
                if (this._measure != null)
                {
                    return this._measure.Id;
                }
                return this._measureRef;
            }
            internal set
            {
                this._measureRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.Measure = this.IdentData.FindMeasure(value);
                }
            }
        }

        /// <remarks>A reference to the Measure defined in the FragmentationTable</remarks>
        /// Required Attribute
        /// string
        public MeasureObj Measure
        {
            get { return this._measure; }
            set
            {
                this._measure = value;
                if (this._measure != null)
                {
                    this._measure.IdentData = this.IdentData;
                    this._measureRef = this._measure.Id;
                }
            }
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
    public partial class IonTypeObj : IdentDataInternalTypeAbstract
    {
        private IdentDataList<FragmentArrayObj> _fragmentArrays;
        private CVParamObj _cvParam;
        private List<string> _index;
        private int _charge;

        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<FragmentArrayObj> FragmentArrays
        {
            get { return this._fragmentArrays; }
            set
            {
                this._fragmentArrays = value;
                if (this._fragmentArrays != null)
                {
                    this._fragmentArrays.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>The type of ion identified.</remarks>
        /// <remarks>min 1, max 1</remarks>
        public CVParamObj CVParam
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
    public partial class CVParamObj : ParamBaseObj
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
    public abstract partial class ParamBaseObj : IdentDataInternalTypeAbstract
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
    public partial class UserParamObj : ParamBaseObj
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

    public partial class CVParamGroupObj : IdentDataInternalTypeAbstract
    {
        private IdentDataList<CVParamObj> _cvParams;

        public IdentDataList<CVParamObj> CVParams
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

    public partial class ParamGroupObj : CVParamGroupObj
    {
        private IdentDataList<UserParamObj> _userParams;

        public IdentDataList<UserParamObj> UserParams
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
    public partial class ParamObj : IdentDataInternalTypeAbstract
    {
        private ParamBaseObj _item;

        /// <remarks>min 1, max 1</remarks>
        public ParamBaseObj Item
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
    public partial class ParamListObj : IdentDataInternalTypeAbstract
    {
        private IdentDataList<ParamBaseObj> _items;

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<ParamBaseObj> Items
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
    public partial class PeptideEvidenceRefObj : IdentDataInternalTypeAbstract
    {
        private string _peptideEvidenceRef;
        private PeptideEvidenceObj _peptideEvidence;

        /// <remarks>A reference to the PeptideEvidenceItem element(s).</remarks>
        /// Required Attribute
        /// string
        protected internal string PeptideEvidenceRef
        {
            get
            {
                if (this._peptideEvidence != null)
                {
                    return this._peptideEvidence.Id;
                }
                return this._peptideEvidenceRef;
            }
            set
            {
                this._peptideEvidenceRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.PeptideEvidence = this.IdentData.FindPeptideEvidence(value);
                }
            }
        }

        /// <remarks>A reference to the PeptideEvidenceItem element(s).</remarks>
        /// Required Attribute
        /// string
        public PeptideEvidenceObj PeptideEvidence
        {
            get { return this._peptideEvidence; }
            set
            {
                this._peptideEvidence = value;
                if (this._peptideEvidence != null)
                {
                    this._peptideEvidence.IdentData = this.IdentData;
                    this._peptideEvidenceRef = this._peptideEvidence.Id;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML AnalysisDataType
    /// </summary>
    /// <remarks>Data sets generated by the analyses, including peptide and protein lists.</remarks>
    public partial class AnalysisDataObj : IdentDataInternalTypeAbstract
    {
        private IdentDataList<SpectrumIdentificationListObj> _spectrumIdentificationList;
        private ProteinDetectionListObj _proteinDetectionList;

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<SpectrumIdentificationListObj> SpectrumIdentificationList
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

        /// <remarks>min 0, max 1</remarks>
        public ProteinDetectionListObj ProteinDetectionList
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
    public partial class SpectrumIdentificationListObj : ParamGroupObj, IIdentifiableType
    {
        private IdentDataList<MeasureObj> _fragmentationTables;
        private IdentDataList<SpectrumIdentificationResultObj> _spectrumIdentificationResults;
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

        /// <remarks>min 0, max 1</remarks>
        public IdentDataList<MeasureObj> FragmentationTables
        {
            get { return this._fragmentationTables; }
            set
            {
                this._fragmentationTables = value;
                if (this._fragmentationTables != null)
                {
                    this._fragmentationTables.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<SpectrumIdentificationResultObj> SpectrumIdentificationResults
        {
            get { return this._spectrumIdentificationResults; }
            set
            {
                this._spectrumIdentificationResults = value;
                if (this._spectrumIdentificationResults != null)
                {
                    this._spectrumIdentificationResults.IdentData = this.IdentData;
                }
            }
        }

        ///// <remarks>___ParamGroup___:Scores or output parameters associated with the SpectrumIdentificationList.</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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

        ///// <remarks>___ParamGroup___</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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
        protected internal bool NumSequencesSearchedSpecified { get; private set; }
    }

    /// <summary>
    /// MzIdentML MeasureType; list form is FragmentationTableType
    /// </summary>
    /// <remarks>References to CV terms defining the measures about product ions to be reported in SpectrumIdentificationItem</remarks>
    /// <remarks>FragmentationTableType: Contains the types of measures that will be reported in generic arrays 
    /// for each SpectrumIdentificationItem e.g. product ion m/z, product ion intensity, product ion m/z error</remarks>
    /// <remarks>FragmentationTableType: child element Measure of type MeasureType, min 1, max unbounded</remarks>
    public partial class MeasureObj : CVParamGroupObj, IIdentifiableType
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

        ///// <remarks>min 1, max unbounded</remarks>
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
    public partial class BibliographicReferenceObj : IdentDataInternalTypeAbstract, IIdentifiableType
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
        protected internal bool YearSpecified { get; private set; }

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
    public partial class ProteinDetectionHypothesisObj : ParamGroupObj, IIdentifiableType
    {
        private IdentDataList<PeptideHypothesisObj> _peptideHypotheses;
        //private IdentDataList<CVParamType> _cvParams;
        //private IdentDataList<UserParamType> _userParams;
        private string _dBSequenceRef;
        private DbSequenceObj _dBSequence;
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

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<PeptideHypothesisObj> PeptideHypotheses
        {
            get { return this._peptideHypotheses; }
            set
            {
                this._peptideHypotheses = value;
                if (this._peptideHypotheses != null)
                {
                    this._peptideHypotheses.IdentData = this.IdentData;
                }
            }
        }

        ///// <remarks>___ParamGroup___:Scores or parameters associated with this ProteinDetectionHypothesis e.g. p-value</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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

        ///// <remarks>___ParamGroup___</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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
        protected internal string DBSequenceRef
        {
            get
            {
                if (this._dBSequence != null)
                {
                    return this._dBSequence.Id;
                }
                return this._dBSequenceRef;
            }
            set
            {
                this._dBSequenceRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.DBSequence = this.IdentData.FindDbSequence(value);
                }
            }
        }

        /// <remarks>A reference to the corresponding DBSequence entry. This optional and redundant, because the PeptideEvidence 
        /// elements referenced from here also map to the DBSequence.</remarks>
        /// Optional Attribute
        /// string
        public DbSequenceObj DBSequence
        {
            get { return this._dBSequence; }
            set
            {
                this._dBSequence = value;
                if (this._dBSequence != null)
                {
                    this._dBSequence.IdentData = this.IdentData;
                    this._dBSequenceRef = this._dBSequence.Id;
                }
            }
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
    public partial class ProteinAmbiguityGroupObj : ParamGroupObj, IIdentifiableType
    {
        private IdentDataList<ProteinDetectionHypothesisObj> _proteinDetectionHypotheses;
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

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<ProteinDetectionHypothesisObj> ProteinDetectionHypotheses
        {
            get { return this._proteinDetectionHypotheses; }
            set
            {
                this._proteinDetectionHypotheses = value;
                if (this._proteinDetectionHypotheses != null)
                {
                    this._proteinDetectionHypotheses.IdentData = this.IdentData;
                }
            }
        }

        ///// <remarks>___ParamGroup___:Scores or parameters associated with the ProteinAmbiguityGroup.</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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

        ///// <remarks>___ParamGroup___</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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
    public partial class ProteinDetectionListObj : ParamGroupObj, IIdentifiableType
    {
        private IdentDataList<ProteinAmbiguityGroupObj> _proteinAmbiguityGroups;
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

        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<ProteinAmbiguityGroupObj> ProteinAmbiguityGroups
        {
            get { return this._proteinAmbiguityGroups; }
            set
            {
                this._proteinAmbiguityGroups = value;
                if (this._proteinAmbiguityGroups != null)
                {
                    this._proteinAmbiguityGroups.IdentData = this.IdentData;
                }
            }
        }

        ///// <remarks>___ParamGroup___:Scores or output parameters associated with the whole ProteinDetectionList</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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

        ///// <remarks>___ParamGroup___</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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
    public partial class SpectrumIdentificationItemObj : ParamGroupObj, IIdentifiableType
    {
        private IdentDataList<PeptideEvidenceRefObj> _peptideEvidences;
        private IdentDataList<IonTypeObj> _fragmentations;
        //private IdentDataList<CVParamType> _cvParam;
        //private IdentDataList<UserParamType> _userParam;
        private int _chargeState;
        private double _experimentalMassToCharge;
        private double _calculatedMassToCharge;
        private float _calculatedPI;
        private string _peptideRef;
        private PeptideObj _peptide;
        private int _rank;
        private bool _passThreshold;
        private string _massTableRef;
        private MassTableObj _massTable;
        private string _sampleRef;
        private SampleObj _sample;
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

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<PeptideEvidenceRefObj> PeptideEvidences
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

        /// <remarks>min 0, max 1</remarks>
        public IdentDataList<IonTypeObj> Fragmentations
        {
            get { return this._fragmentations; }
            set
            {
                this._fragmentations = value;
                if (this._fragmentations != null)
                {
                    this._fragmentations.IdentData = this.IdentData;
                }
            }
        }

        ///// <remarks>___ParamGroup___:Scores or attributes associated with the SpectrumIdentificationItem e.g. e-value, p-value, score.</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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

        ///// <remarks>___ParamGroup___</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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
        protected internal bool CalculatedMassToChargeSpecified { get; private set; }

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
        protected internal bool CalculatedPISpecified { get; private set; }

        /// <remarks>A reference to the identified (poly)peptide sequence in the Peptide element.</remarks>
        /// Optional Attribute
        /// string
        protected internal string PeptideRef
        {
            get
            {
                if (this._peptide != null)
                {
                    return this._peptide.Id;
                }
                return this._peptideRef;
            }
            set
            {
                this._peptideRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.Peptide = this.IdentData.FindPeptide(value);
                }
            }
        }

        /// <remarks>A reference to the identified (poly)peptide sequence in the Peptide element.</remarks>
        /// Optional Attribute
        /// string
        public PeptideObj Peptide
        {
            get { return this._peptide; }
            set
            {
                this._peptide = value;
                if (this._peptide != null)
                {
                    this._peptide.IdentData = this.IdentData;
                    this._peptideRef = this._peptide.Id;
                }
            }
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
        protected internal string MassTableRef
        {
            get
            {
                if (this._massTable != null)
                {
                    return this._massTable.Id;
                }
                return this._massTableRef;
            }
            set
            {
                this._massTableRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.MassTable = this.IdentData.FindMassTable(value);
                }
            }
        }

        /// <remarks>A reference should be given to the MassTable used to calculate the sequenceMass only if more than one MassTable has been given.</remarks>
        /// Optional Attribute
        /// string
        public MassTableObj MassTable
        {
            get { return this._massTable; }
            set
            {
                this._massTable = value;
                if (this._massTable != null)
                {
                    this._massTable.IdentData = this.IdentData;
                    this._massTableRef = this._massTable.Id;
                }
            }
        }

        /// <remarks>A reference should be provided to link the SpectrumIdentificationItem to a Sample 
        /// if more than one sample has been described in the AnalysisSampleCollection.</remarks>
        /// Optional Attribute
        protected internal string SampleRef
        {
            get
            {
                if (this._sample != null)
                {
                    return this._sample.Id;
                }
                return this._sampleRef;
            }
            set
            {
                this._sampleRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.Sample = this.IdentData.FindSample(value);
                }
            }
        }

        /// <remarks>A reference should be provided to link the SpectrumIdentificationItem to a Sample 
        /// if more than one sample has been described in the AnalysisSampleCollection.</remarks>
        /// Optional Attribute
        public SampleObj Sample
        {
            get { return this._sample; }
            set
            {
                this._sample = value;
                if (this._sample != null)
                {
                    this._sample.IdentData = this.IdentData;
                    this._sampleRef = this._sample.Id;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationResultType
    /// </summary>
    /// <remarks>All identifications made from searching one spectrum. For PMF data, all peptide identifications 
    /// will be listed underneath as SpectrumIdentificationItems. For MS/MS data, there will be ranked 
    /// SpectrumIdentificationItems corresponding to possible different peptide IDs.</remarks>
    public partial class SpectrumIdentificationResultObj : ParamGroupObj, IIdentifiableType
    {
        private IdentDataList<SpectrumIdentificationItemObj> _spectrumIdentificationItems;
        //private IdentDataList<CVParamType> _cvParams;
        //private IdentDataList<UserParamType> _userParams;
        private string _spectrumID;
        private string _spectraDataRef;
        private SpectraDataObj _spectraData;
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

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<SpectrumIdentificationItemObj> SpectrumIdentificationItems
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

        ///// <remarks>___ParamGroup___: Scores or parameters associated with the SpectrumIdentificationResult 
        ///// (i.e the set of SpectrumIdentificationItems derived from one spectrum) e.g. the number of peptide 
        ///// sequences within the parent tolerance for this spectrum.</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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

        ///// <remarks>___ParamGroup___</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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
        protected internal string SpectraDataRef
        {
            get
            {
                if (this._spectraData != null)
                {
                    return this._spectraData.Id;
                }
                return this._spectraDataRef;
            }
            set
            {
                this._spectraDataRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.SpectraData = this.IdentData.FindSpectraData(value);
                }
            }
        }

        /// <remarks>A reference to a spectra data set (e.g. a spectra file).</remarks>
        /// Required Attribute
        /// string
        public SpectraDataObj SpectraData
        {
            get { return this._spectraData; }
            set
            {
                this._spectraData = value;
                if (this._spectraData != null)
                {
                    this._spectraData.IdentData = this.IdentData;
                    this._spectraDataRef = this._spectraData.Id;
                }
            }
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
        /// <remarks>min 0, max 1</remarks>
        string ExternalFormatDocumentation { get; set; }

        /// <remarks>min 0, max 1</remarks>
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
        private CVParamObj _cvParam;

        /// <remarks>cvParam capturing file formats</remarks>
        /// <remarks>min 1, max 1</remarks>
        public CVParamObj CVParam
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
    public partial class SpectraDataObj : IdentDataInternalTypeAbstract, IExternalDataType
    {
        private SpectrumIDFormatObj _spectrumIDFormat;
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
        /// <remarks>min 0, max 1</remarks>
        public string ExternalFormatDocumentation
        {
            get { return this._externalFormatDocumentation; }
            set { this._externalFormatDocumentation = value; }
        }

        /// <remarks>min 0, max 1</remarks>
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

        /// <remarks>min 1, max 1</remarks>
        public SpectrumIDFormatObj SpectrumIDFormat
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
    public partial class SpectrumIDFormatObj : IdentDataInternalTypeAbstract
    {
        private CVParamObj _cvParam;

        /// <remarks>CV term capturing the type of identifier used.</remarks>
        /// <remarks>min 1, max 1</remarks>
        public CVParamObj CVParam
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
    public partial class SourceFileInfo : ParamGroupObj, IExternalDataType
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
        /// <remarks>min 0, max 1</remarks>
        public string ExternalFormatDocumentation
        {
            get { return this._externalFormatDocumentation; }
            set { this._externalFormatDocumentation = value; }
        }

        /// <remarks>min 0, max 1</remarks>
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

        ///// <remarks>___ParamGroup___:Any additional parameters description the source file.</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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

        ///// <remarks>___ParamGroup___</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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
    public partial class SearchDatabaseInfo : CVParamGroupObj, IExternalDataType
    {
        private ParamObj _databaseName;
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
        /// <remarks>min 0, max 1</remarks>
        public string ExternalFormatDocumentation
        {
            get { return this._externalFormatDocumentation; }
            set { this._externalFormatDocumentation = value; }
        }

        /// <remarks>min 0, max 1</remarks>
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
        /// <remarks>min 1, max 1</remarks>
        public ParamObj DatabaseName
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

        ///// <remarks>min 0, max unbounded</remarks>
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
        protected internal bool ReleaseDateSpecified { get; private set; }

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
        protected internal bool NumDatabaseSequencesSpecified { get; private set; }

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
        protected internal bool NumResiduesSpecified { get; private set; }
    }

    /// <summary>
    /// MzIdentML ProteinDetectionProtocolType
    /// </summary>
    /// <remarks>The parameters and settings of a ProteinDetection process.</remarks>
    public partial class ProteinDetectionProtocolObj : IdentDataInternalTypeAbstract, IIdentifiableType
    {
        private ParamListObj _analysisParams;
        private ParamListObj _threshold;
        private string _analysisSoftwareRef;
        private AnalysisSoftwareObj _analysisSoftware;
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
        /// <remarks>min 0, max 1</remarks>
        public ParamListObj AnalysisParams
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
        /// <remarks>min 1, max 1</remarks>
        public ParamListObj Threshold
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
        protected internal string AnalysisSoftwareRef
        {
            get
            {
                if (this._analysisSoftware != null)
                {
                    return this._analysisSoftware.Id;
                }
                return this._analysisSoftwareRef;
            }
            set
            {
                this._analysisSoftwareRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.AnalysisSoftware = this.IdentData.FindAnalysisSoftware(value);
                }
            }
        }

        /// <remarks>The protein detection software used, given as a reference to the SoftwareCollection section.</remarks>
        /// Required Attribute
        /// string
        public AnalysisSoftwareObj AnalysisSoftware
        {
            get { return this._analysisSoftware; }
            set
            {
                this._analysisSoftware = value;
                if (this._analysisSoftware != null)
                {
                    this._analysisSoftware.IdentData = this.IdentData;
                    this._analysisSoftwareRef = this._analysisSoftware.Id;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML TranslationTableType
    /// </summary>
    /// <remarks>The table used to translate codons into nucleic acids e.g. by reference to the NCBI translation table.</remarks>
    public partial class TranslationTableObj : CVParamGroupObj, IIdentifiableType
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

        ///// <remarks>The details specifying this translation table are captured as cvParams, e.g. translation table, translation 
        ///// start codons and translation table description (see specification document and mapping file)</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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
    public partial class MassTableObj : ParamGroupObj, IIdentifiableType
    {
        private IdentDataList<ResidueObj> _residues;
        private IdentDataList<AmbiguousResidueObj> _ambiguousResidues;
        //private IdentDataList<CVParamType> _cvParams;
        //private IdentDataList<UserParamType> _userParams;
        private List<string> _msLevels;
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
        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<ResidueObj> Residues
        {
            get { return this._residues; }
            set
            {
                this._residues = value;
                if (this._residues != null)
                {
                    this._residues.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<AmbiguousResidueObj> AmbiguousResidues
        {
            get { return this._ambiguousResidues; }
            set
            {
                this._ambiguousResidues = value;
                if (this._ambiguousResidues != null)
                {
                    this._ambiguousResidues.IdentData = this.IdentData;
                }
            }
        }

        ///// <remarks>___ParamGroup___: Additional parameters or descriptors for the MassTable.</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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

        ///// <remarks>___ParamGroup___</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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
        public List<string> MsLevels
        {
            get { return this._msLevels; }
            set { this._msLevels = value; }
        }
    }

    /// <summary>
    /// MzIdentML ResidueType
    /// </summary>
    public partial class ResidueObj : IdentDataInternalTypeAbstract
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
    public partial class AmbiguousResidueObj : ParamGroupObj
    {
        //private IdentDataList<CVParamType> _cvParams;
        //private IdentDataList<UserParamType> _userParams;
        private string _code;

        ///// <remarks>___ParamGroup___: Parameters for capturing e.g. "alternate single letter codes"</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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

        ///// <remarks>___ParamGroup___</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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
    public partial class EnzymeObj : IdentDataInternalTypeAbstract, IIdentifiableType
    {
        private string _siteRegexp;
        private ParamListObj _enzymeName;
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
        /// <remarks>min 0, max 1</remarks>
        public string SiteRegexp
        {
            get { return this._siteRegexp; }
            set { this._siteRegexp = value; }
        }

        /// <remarks>The name of the enzyme from a CV.</remarks>
        /// <remarks>min 0, max 1</remarks>
        public ParamListObj EnzymeName
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
        protected internal bool SemiSpecificSpecified { get; private set; }

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
        protected internal bool MissedCleavagesSpecified { get; private set; }

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
        protected internal bool MinDistanceSpecified { get; private set; }
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationProtocolType
    /// </summary>
    /// <remarks>The parameters and settings of a SpectrumIdentification analysis.</remarks>
    public partial class SpectrumIdentificationProtocolObj : IdentDataInternalTypeAbstract, IIdentifiableType
    {
        private ParamObj _searchType;
        private ParamListObj _additionalSearchParams;
        private IdentDataList<SearchModificationObj> _modificationParams;
        private EnzymeListObj _enzymes;
        private IdentDataList<MassTableObj> _massTables;
        private IdentDataList<CVParamObj> _fragmentTolerances;
        private IdentDataList<CVParamObj> _parentTolerances;
        private ParamListObj _threshold;
        private IdentDataList<FilterInfo> _databaseFilters;
        private DatabaseTranslationObj _databaseTranslation;
        private string _analysisSoftwareRef;
        private AnalysisSoftwareObj _analysisSoftware;
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
        /// <remarks>min 1, max 1</remarks>
        public ParamObj SearchType
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
        /// <remarks>min 0, max 1</remarks>
        public ParamListObj AdditionalSearchParams
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

        /// <remarks>min 0, max 1</remarks>
        // Original ModificationParamsType
        public IdentDataList<SearchModificationObj> ModificationParams
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

        /// <remarks>min 0, max 1</remarks>
        public EnzymeListObj Enzymes
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

        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<MassTableObj> MassTables
        {
            get { return this._massTables; }
            set
            {
                this._massTables = value;
                if (this._massTables != null)
                {
                    this._massTables.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>min 0, max 1</remarks>
        // Original ToleranceType
        public IdentDataList<CVParamObj> FragmentTolerances
        {
            get { return this._fragmentTolerances; }
            set
            {
                this._fragmentTolerances = value;
                if (this._fragmentTolerances != null)
                {
                    this._fragmentTolerances.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>min 0, max 1</remarks>
        // Original ToleranceType
        public IdentDataList<CVParamObj> ParentTolerances
        {
            get { return this._parentTolerances; }
            set
            {
                this._parentTolerances = value;
                if (this._parentTolerances != null)
                {
                    this._parentTolerances.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>The threshold(s) applied to determine that a result is significant. If multiple terms are used it is assumed that all conditions are satisfied by the passing results.</remarks>
        /// <remarks>min 1, max 1</remarks>
        public ParamListObj Threshold
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

        /// <remarks>min 0, max 1</remarks>
        // Original DatabaseFiltersType
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

        /// <remarks>min 0, max 1</remarks>
        public DatabaseTranslationObj DatabaseTranslation
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
        protected internal string AnalysisSoftwareRef
        {
            get
            {
                if (this._analysisSoftware != null)
                {
                    return this._analysisSoftware.Id;
                }
                return this._analysisSoftwareRef;
            }
            set
            {
                this._analysisSoftwareRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.AnalysisSoftware = this.IdentData.FindAnalysisSoftware(value);
                }
            }
        }

        /// <remarks>The search algorithm used, given as a reference to the SoftwareCollection section.</remarks>
        /// Required Attribute
        /// string
        public AnalysisSoftwareObj AnalysisSoftware
        {
            get { return this._analysisSoftware; }
            set
            {
                this._analysisSoftware = value;
                if (this._analysisSoftware != null)
                {
                    this._analysisSoftware.IdentData = this.IdentData;
                    this._analysisSoftwareRef = this._analysisSoftware.Id;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML SpecificityRulesType
    /// </summary>
    /// <remarks>The specificity rules of the searched modification including for example 
    /// the probability of a modification's presence or peptide or protein termini. Standard 
    /// fixed or variable status should be provided by the attribute fixedMod.</remarks>
    public partial class SpecificityRulesListObj : CVParamGroupObj
    {
        //private IdentDataList<CVParamType> _cvParams;

        ///// <remarks>min 1, max unbounded</remarks>
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
    public partial class SearchModificationObj : CVParamGroupObj
    {
        private IdentDataList<SpecificityRulesListObj> _specificityRules;
        //private IdentDataList<CVParamType> _cvParams;
        private bool _fixedMod;
        private float _massDelta;
        private string _residues;

        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<SpecificityRulesListObj> SpecificityRules
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

        ///// <remarks>The modification is uniquely identified by references to external CVs such as UNIMOD, see 
        ///// specification document and mapping file for more details.</remarks>
        ///// <remarks>min 1, max unbounded</remarks>
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
    public partial class EnzymeListObj : IdentDataInternalTypeAbstract
    {
        private IdentDataList<EnzymeObj> _enzymes;
        private bool _independent;

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<EnzymeObj> Enzymes
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
        protected internal bool IndependentSpecified { get; private set; }
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
        private ParamObj _filterType;
        private ParamListObj _include;
        private ParamListObj _exclude;

        /// <remarks>The type of filter e.g. database taxonomy filter, pi filter, mw filter</remarks>
        /// <remarks>min 1, max 1</remarks>
        public ParamObj FilterType
        {
            get { return this._filterType; }
            set
            {
                this._filterType = value;
                if (this._filterType != null)
                {
                    this._filterType.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>All sequences fulfilling the specifed criteria are included.</remarks>
        /// <remarks>min 0, max 1</remarks>
        public ParamListObj Include
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
        /// <remarks>min 0, max 1</remarks>
        public ParamListObj Exclude
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
    public partial class DatabaseTranslationObj : IdentDataInternalTypeAbstract
    {
        private IdentDataList<TranslationTableObj> _translationTables;
        private List<int> _frames;

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<TranslationTableObj> TranslationTables
        {
            get { return this._translationTables; }
            set
            {
                this._translationTables = value;
                if (this._translationTables != null)
                {
                    this._translationTables.IdentData = this.IdentData;
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
    public abstract partial class ProtocolApplicationObj : IdentDataInternalTypeAbstract, IIdentifiableType
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
        protected internal bool ActivityDateSpecified { get; private set; }
    }

    /// <summary>
    /// MzIdentML ProteinDetectionType
    /// </summary>
    /// <remarks>An Analysis which assembles a set of peptides (e.g. from a spectra search analysis) to proteins.</remarks>
    public partial class ProteinDetectionObj : ProtocolApplicationObj
    {
        private IdentDataList<InputSpectrumIdentificationsObj> _inputSpectrumIdentifications;
        private string _proteinDetectionListRef;
        private ProteinDetectionListObj _proteinDetectionList;
        private string _proteinDetectionProtocolRef;
        private ProteinDetectionProtocolObj _proteinDetectionProtocol;
        
        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<InputSpectrumIdentificationsObj> InputSpectrumIdentifications
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
        protected internal string ProteinDetectionListRef
        {
            get
            {
                if (this._proteinDetectionList != null)
                {
                    return this._proteinDetectionList.Id;
                }
                return this._proteinDetectionListRef;
            }
            set
            {
                this._proteinDetectionListRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.ProteinDetectionList = this.IdentData.FindProteinDetectionList(value);
                }
            }
        }

        /// <remarks>A reference to the ProteinDetectionList in the DataCollection section.</remarks>
        /// Required Attribute
        /// string
        public ProteinDetectionListObj ProteinDetectionList
        {
            get { return this._proteinDetectionList; }
            set
            {
                this._proteinDetectionList = value;
                if (this._proteinDetectionList != null)
                {
                    this._proteinDetectionList.IdentData = this.IdentData;
                    this._proteinDetectionListRef = this._proteinDetectionList.Id;
                }
            }
        }

        /// <remarks>A reference to the detection protocol used for this ProteinDetection.</remarks>
        /// Required Attribute
        /// string
        protected internal string ProteinDetectionProtocolRef
        {
            get
            {
                if (this._proteinDetectionProtocol != null)
                {
                    return this._proteinDetectionProtocol.Id;
                }
                return this._proteinDetectionProtocolRef;
            }
            set
            {
                this._proteinDetectionProtocolRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.ProteinDetectionProtocol = this.IdentData.FindProteinDetectionProtocol(value);
                }
            }
        }

        /// <remarks>A reference to the detection protocol used for this ProteinDetection.</remarks>
        /// Required Attribute
        /// string
        public ProteinDetectionProtocolObj ProteinDetectionProtocol
        {
            get { return this._proteinDetectionProtocol; }
            set
            {
                this._proteinDetectionProtocol = value;
                if (this._proteinDetectionProtocol != null)
                {
                    this._proteinDetectionProtocol.IdentData = this.IdentData;
                    this._proteinDetectionProtocolRef = this._proteinDetectionProtocol.Id;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML InputSpectrumIdentificationsType
    /// </summary>
    /// <remarks>The lists of spectrum identifications that are input to the protein detection process.</remarks>
    public partial class InputSpectrumIdentificationsObj : IdentDataInternalTypeAbstract
    {
        private string _spectrumIdentificationListRef;
        private SpectrumIdentificationListObj _spectrumIdentificationList;

        /// <remarks>A reference to the list of spectrum identifications that were input to the process.</remarks>
        /// Required Attribute
        /// string
        protected internal string SpectrumIdentificationListRef
        {
            get
            {
                if (this._spectrumIdentificationList != null)
                {
                    return this._spectrumIdentificationList.Id;
                }
                return this._spectrumIdentificationListRef;
            }
            set
            {
                this._spectrumIdentificationListRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.SpectrumIdentificationList = this.IdentData.FindSpectrumIdentificationList(value);
                }
            }
        }

        /// <remarks>A reference to the list of spectrum identifications that were input to the process.</remarks>
        /// Required Attribute
        /// string
        public SpectrumIdentificationListObj SpectrumIdentificationList
        {
            get { return this._spectrumIdentificationList; }
            set
            {
                this._spectrumIdentificationList = value;
                if (this._spectrumIdentificationList != null)
                {
                    this._spectrumIdentificationList.IdentData = this.IdentData;
                    this._spectrumIdentificationListRef = this._spectrumIdentificationList.Id;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationType
    /// </summary>
    /// <remarks>An Analysis which tries to identify peptides in input spectra, referencing the database searched, 
    /// the input spectra, the output results and the protocol that is run.</remarks>
    public partial class SpectrumIdentificationObj : ProtocolApplicationObj
    {
        private IdentDataList<InputSpectraRefObj> _inputSpectra;
        private IdentDataList<SearchDatabaseRefObj> _searchDatabases;
        private string _spectrumIdentificationProtocolRef;
        private SpectrumIdentificationProtocolObj _spectrumIdentificationProtocol;
        private string _spectrumIdentificationListRef;
        private SpectrumIdentificationListObj _spectrumIdentificationList;

        /// <remarks>One of the spectra data sets used.</remarks>
        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<InputSpectraRefObj> InputSpectra
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

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<SearchDatabaseRefObj> SearchDatabases
        {
            get { return this._searchDatabases; }
            set
            {
                this._searchDatabases = value;
                if (this._searchDatabases != null)
                {
                    this._searchDatabases.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>A reference to the search protocol used for this SpectrumIdentification.</remarks>
        /// Required Attribute
        /// string
        protected internal string SpectrumIdentificationProtocolRef
        {
            get
            {
                if (this._spectrumIdentificationProtocol != null)
                {
                    return this._spectrumIdentificationProtocol.Id;
                }
                return this._spectrumIdentificationProtocolRef;
            }
            set
            {
                this._spectrumIdentificationProtocolRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.SpectrumIdentificationProtocol = this.IdentData.FindSpectrumIdentificationProtocol(value);
                }
            }
        }

        /// <remarks>A reference to the search protocol used for this SpectrumIdentification.</remarks>
        /// Required Attribute
        /// string
        public SpectrumIdentificationProtocolObj SpectrumIdentificationProtocol
        {
            get { return this._spectrumIdentificationProtocol; }
            set
            {
                this._spectrumIdentificationProtocol = value;
                if (this._spectrumIdentificationProtocol != null)
                {
                    this._spectrumIdentificationProtocol.IdentData = this.IdentData;
                    this._spectrumIdentificationProtocolRef = this._spectrumIdentificationProtocol.Id;
                }
            }
        }

        /// <remarks>A reference to the SpectrumIdentificationList produced by this analysis in the DataCollection section.</remarks>
        /// Required Attribute
        /// string
        protected internal string SpectrumIdentificationListRef
        {
            get
            {
                if (this._spectrumIdentificationList != null)
                {
                    return this._spectrumIdentificationList.Id;
                }
                return this._spectrumIdentificationListRef;
            }
            set
            {
                this._spectrumIdentificationListRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.SpectrumIdentificationList = this.IdentData.FindSpectrumIdentificationList(value);
                }
            }
        }

        /// <remarks>A reference to the SpectrumIdentificationList produced by this analysis in the DataCollection section.</remarks>
        /// Required Attribute
        /// string
        public SpectrumIdentificationListObj SpectrumIdentificationList
        {
            get { return this._spectrumIdentificationList; }
            set
            {
                this._spectrumIdentificationList = value;
                if (this._spectrumIdentificationList != null)
                {
                    this._spectrumIdentificationList.IdentData = this.IdentData;
                    this._spectrumIdentificationListRef = this._spectrumIdentificationList.Id;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML InputSpectraType
    /// </summary>
    /// <remarks>The attribute referencing an identifier within the SpectraData section.</remarks>
    public partial class InputSpectraRefObj : IdentDataInternalTypeAbstract
    {
        private string _spectraDataRef;
        private SpectraDataObj _spectraData;

        /// <remarks>A reference to the SpectraData element which locates the input spectra to an external file.</remarks>
        /// Optional Attribute
        /// string
        protected internal string SpectraDataRef
        {
            get
            {
                if (this._spectraData != null)
                {
                    return this._spectraData.Id;
                }
                return this._spectraDataRef;
            }
            set
            {
                this._spectraDataRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.SpectraData = this.IdentData.FindSpectraData(value);
                }
            }
        }

        /// <remarks>A reference to the SpectraData element which locates the input spectra to an external file.</remarks>
        /// Optional Attribute
        /// string
        public SpectraDataObj SpectraData
        {
            get { return this._spectraData; }
            set
            {
                this._spectraData = value;
                if (this._spectraData != null)
                {
                    this._spectraData.IdentData = this.IdentData;
                    this._spectraDataRef = this._spectraData.Id;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML SearchDatabaseRefType
    /// </summary>
    /// <remarks>One of the search databases used.</remarks>
    public partial class SearchDatabaseRefObj : IdentDataInternalTypeAbstract
    {
        private string _searchDatabaseRef;
        private SearchDatabaseInfo _searchDatabase;

        /// <remarks>A reference to the database searched.</remarks>
        /// Optional Attribute
        /// string
        protected internal string SearchDatabaseRef
        {
            get
            {
                if (this._searchDatabase != null)
                {
                    return this._searchDatabase.Id;
                }
                return this._searchDatabaseRef;
            }
            set
            {
                this._searchDatabaseRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.SearchDatabase = this.IdentData.FindSearchDatabase(value);
                }
            }
        }

        /// <remarks>A reference to the database searched.</remarks>
        /// Optional Attribute
        /// string
        public SearchDatabaseInfo SearchDatabase
        {
            get { return this._searchDatabase; }
            set
            {
                this._searchDatabase = value;
                if (this._searchDatabase != null)
                {
                    this._searchDatabase.IdentData = this.IdentData;
                    this._searchDatabaseRef = this._searchDatabase.Id;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML PeptideEvidenceType
    /// </summary>
    /// <remarks>PeptideEvidence links a specific Peptide element to a specific position in a DBSequence. 
    /// There must only be one PeptideEvidence item per Peptide-to-DBSequence-position.</remarks>
    public partial class PeptideEvidenceObj : ParamGroupObj, IIdentifiableType
    {
        //private IdentDataList<CVParamType> _cvParams;
        //private IdentDataList<UserParamType> _userParams;
        private string _dBSequenceRef;
        private DbSequenceObj _dBSequence;
        private string _peptideRef;
        private PeptideObj _peptide;
        private int _start;
        private int _end;
        private string _pre;
        private string _post;
        private string _translationTableRef;
        private TranslationTableObj _translationTable;
        private int _frame;
        private bool _isDecoy;
        private string _id;
        private string _name;

        // Taken care of elsewhere
        //public PeptideEvidence()
        //{
        //    this._isDecoy = false;
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

        ///// <remarks>___ParamGroup___: Additional parameters or descriptors for the PeptideEvidence.</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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

        ///// <remarks>___ParamGroup___</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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
        protected internal bool StartSpecified { get; private set; }

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
        protected internal bool EndSpecified { get; private set; }

        /// <remarks>A reference to the translation table used if this is PeptideEvidence derived from nucleic acid sequence</remarks>
        /// Optional Attribute
        /// string
        protected internal string TranslationTableRef
        {
            get
            {
                if (this._translationTable != null)
                {
                    return this._translationTable.Id;
                }
                return this._translationTableRef;
            }
            set
            {
                this._translationTableRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.TranslationTable = this.IdentData.FindTranslationTable(value);
                }
            }
        }

        /// <remarks>A reference to the translation table used if this is PeptideEvidence derived from nucleic acid sequence</remarks>
        /// Optional Attribute
        /// string
        public TranslationTableObj TranslationTable
        {
            get { return this._translationTable; }
            set
            {
                this._translationTable = value;
                if (this._translationTable != null)
                {
                    this._translationTable.IdentData = this.IdentData;
                    this._translationTableRef = this._translationTable.Id;
                }
            }
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
        protected internal bool FrameSpecified { get; private set; }

        /// <remarks>A reference to the identified (poly)peptide sequence in the Peptide element.</remarks>
        /// Required Attribute
        /// string
        protected internal string PeptideRef
        {
            get
            {
                if (this._peptide != null)
                {
                    return this._peptide.Id;
                }
                return this._peptideRef;
            }
            set
            {
                this._peptideRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.Peptide = this.IdentData.FindPeptide(value);
                }
            }
        }

        /// <remarks>A reference to the identified (poly)peptide sequence in the Peptide element.</remarks>
        /// Required Attribute
        /// string
        public PeptideObj Peptide
        {
            get { return this._peptide; }
            set
            {
                this._peptide = value;
                if (this._peptide != null)
                {
                    this._peptide.IdentData = this.IdentData;
                    this._peptideRef = this._peptide.Id;
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
                if (this._dBSequence != null)
                {
                    return this._dBSequence.Id;
                }
                return this._dBSequenceRef;
            }
            set
            {
                this._dBSequenceRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.DBSequence = this.IdentData.FindDbSequence(value);
                }
            }
        }

        /// <remarks>A reference to the protein sequence in which the specified peptide has been linked.</remarks>
        /// Required Attribute
        /// string
        public DbSequenceObj DBSequence
        {
            get { return this._dBSequence; }
            set
            {
                this._dBSequence = value;
                if (this._dBSequence != null)
                {
                    this._dBSequence.IdentData = this.IdentData;
                    this._dBSequenceRef = this._dBSequence.Id;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML PeptideType
    /// </summary>
    /// <remarks>One (poly)peptide (a sequence with modifications). The combination of Peptide sequence and modifications must be unique in the file.</remarks>
    public partial class PeptideObj : ParamGroupObj, IIdentifiableType
    {
        private string _peptideSequence;
        private IdentDataList<ModificationObj> _modifications;
        private IdentDataList<SubstitutionModificationObj> _substitutionModifications;
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
        /// <remarks>min 1, max 1</remarks>
        public string PeptideSequence
        {
            get { return this._peptideSequence; }
            set { this._peptideSequence = value; }
        }

        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<ModificationObj> Modifications
        {
            get { return this._modifications; }
            set
            {
                this._modifications = value;
                if (this._modifications != null)
                {
                    this._modifications.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<SubstitutionModificationObj> SubstitutionModifications
        {
            get { return this._substitutionModifications; }
            set
            {
                this._substitutionModifications = value;
                if (this._substitutionModifications != null)
                {
                    this._substitutionModifications.IdentData = this.IdentData;
                }
            }
        }

        ///// <remarks>___ParamGroup___: Additional descriptors of this peptide sequence</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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

        ///// <remarks>___ParamGroup___</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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
    public partial class ModificationObj : CVParamGroupObj
    {
        //private IdentDataList<CVParamType> _cvParams;
        private int _location;
        private List<string> _residues;
        private double _avgMassDelta;
        private double _monoisotopicMassDelta;

        ///// <remarks>CV terms capturing the modification, sourced from an appropriate controlled vocabulary.</remarks>
        ///// <remarks>min 1, max unbounded</remarks>
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
        protected internal bool LocationSpecified { get; private set; }

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
        protected internal bool AvgMassDeltaSpecified { get; private set; }

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
        protected internal bool MonoisotopicMassDeltaSpecified { get; private set; }
    }

    /// <summary>
    /// MzIdentML SubstitutionModificationType
    /// </summary>
    /// <remarks>A modification where one residue is substituted by another (amino acid change).</remarks>
    public partial class SubstitutionModificationObj : IdentDataInternalTypeAbstract
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
        protected internal bool LocationSpecified { get; private set; }

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
        protected internal bool AvgMassDeltaSpecified { get; private set; }

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
        protected internal bool MonoisotopicMassDeltaSpecified { get; private set; }
    }

    /// <summary>
    /// MzIdentML DBSequenceType
    /// </summary>
    /// <remarks>A database sequence from the specified SearchDatabase (nucleic acid or amino acid). 
    /// If the sequence is nucleic acid, the source nucleic acid sequence should be given in 
    /// the seq attribute rather than a translated sequence.</remarks>
    public partial class DbSequenceObj : ParamGroupObj, IIdentifiableType
    {
        private string _seq;
        //private IdentDataList<CVParamType> _cvParams;
        //private IdentDataList<UserParamType> _userParams;
        private int _length;
        private string _searchDatabaseRef;
        private SearchDatabaseInfo _searchDatabase;
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
        /// <remarks>min 0, max 1</remarks>
        /// string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]*"
        public string Seq
        {
            get { return this._seq; }
            set { this._seq = value; }
        }

        ///// <remarks>___ParamGroup___: Additional descriptors for the sequence, such as taxon, description line etc.</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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

        ///// <remarks>___ParamGroup___</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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
        protected internal string SearchDatabaseRef
        {
            get
            {
                if (this._searchDatabase != null)
                {
                    return this._searchDatabase.Id;
                }
                return this._searchDatabaseRef;
            }
            set
            {
                this._searchDatabaseRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.SearchDatabase = this.IdentData.FindSearchDatabase(value);
                }
            }
        }

        /// <remarks>The source database of this sequence.</remarks>
        /// Required Attribute
        /// string
        public SearchDatabaseInfo SearchDatabase
        {
            get { return this._searchDatabase; }
            set
            {
                this._searchDatabase = value;
                if (this._searchDatabase != null)
                {
                    this._searchDatabase.IdentData = this.IdentData;
                    this._searchDatabaseRef = this._searchDatabase.Id;
                }
            }
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
        protected internal bool LengthSpecified { get; private set; }
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
    public partial class SampleObj : ParamGroupObj, IIdentifiableType
    {
        private IdentDataList<ContactRoleObj> _contactRoles;
        private IdentDataList<SubSampleObj> _subSamples;
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
        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<ContactRoleObj> ContactRoles
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

        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<SubSampleObj> SubSamples
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

        ///// <remarks>___ParamGroup___: The characteristics of a Material.</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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

        ///// <remarks>___ParamGroup___</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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
    public partial class ContactRoleObj : IdentDataInternalTypeAbstract
    {
        private RoleObj _role;
        private string _contactRef;
        private AbstractContactObj _contact;

        /// <remarks>min 1, max 1</remarks>
        public RoleObj Role
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
        protected internal string ContactRef
        {
            get
            {
                if (this._contact != null)
                {
                    return this._contact.Id;
                }
                return this._contactRef;
            }
            set
            {
                this._contactRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.Contact = this.IdentData.FindContact(value);
                }
            }
        }

        /// <remarks>When a ContactRole is used, it specifies which Contact the role is associated with.</remarks>
        /// Required Attribute
        /// string
        public AbstractContactObj Contact
        {
            get { return this._contact; }
            set
            {
                this._contact = value;
                if (this._contact != null)
                {
                    this._contact.IdentData = this.IdentData;
                    this._contactRef = this._contact.Id;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML RoleType
    /// </summary>
    /// <remarks>The roles (lab equipment sales, contractor, etc.) the Contact fills.</remarks>
    public partial class RoleObj : IdentDataInternalTypeAbstract
    {
        private CVParamObj _cvParam;

        /// <remarks>CV term for contact roles, such as software provider.</remarks>
        /// <remarks>min 1, max 1</remarks>
        public CVParamObj CVParam
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
    public partial class SubSampleObj : IdentDataInternalTypeAbstract
    {
        private string _sampleRef;
        private SampleObj _sample;

        /// <remarks>A reference to the child sample.</remarks>
        /// Required  Attribute
        /// string
        protected internal string SampleRef
        {
            get
            {
                if (this._sample != null)
                {
                    return this._sample.Id;
                }
                return this._sampleRef;
            }
            set
            {
                this._sampleRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.Sample = this.IdentData.FindSample(value);
                }
            }
        }

        /// <remarks>A reference to the child sample.</remarks>
        /// Required  Attribute
        /// string
        public SampleObj Sample
        {
            get { return this._sample; }
            set
            {
                this._sample = value;
                if (this._sample != null)
                {
                    this._sample.IdentData = this.IdentData;
                    this._sampleRef = this._sample.Id;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML AuditCollectionType; Also C# representation of AuditCollectionType
    /// </summary>
    /// <remarks>A contact is either a person or an organization.</remarks>
    /// <remarks>AuditCollectionType: The complete set of Contacts (people and organisations) for this file.</remarks>
    /// <remarks>AuditCollectionType: min 1, max unbounded, for PersonType XOR OrganizationType</remarks>
    public abstract partial class AbstractContactObj : ParamGroupObj, IIdentifiableType
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

        ///// <remarks>___ParamGroup___: Attributes of this contact such as address, email, telephone etc.</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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

        ///// <remarks>___ParamGroup___</remarks>
        ///// <remarks>min 0, max unbounded</remarks>
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
    public partial class OrganizationObj : AbstractContactObj
    {
        private ParentOrganizationObj _parent;

        /// <remarks>min 0, max 1</remarks>
        public ParentOrganizationObj Parent
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
    /// Base class for identical ParentOrganization and AffiliationInfo
    /// </summary>
    public partial class OrganizationRefObj : IdentDataInternalTypeAbstract
    {
        private string _organizationRef;
        private OrganizationObj _organization;

        /// <remarks>A reference to the organization this contact belongs to.</remarks>
        /// Required Attribute
        /// string
        protected internal string OrganizationRef
        {
            get
            {
                if (this._organization != null)
                {
                    return this._organization.Id;
                }
                return this._organizationRef;
            }
            set
            {
                this._organizationRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.Organization = this.IdentData.FindOrganization(value);
                }
            }
        }

        /// <remarks>A reference to the organization this contact belongs to.</remarks>
        /// Required Attribute
        /// string
        public OrganizationObj Organization
        {
            get { return this._organization; }
            set
            {
                this._organization = value;
                if (this._organization != null)
                {
                    this._organization.IdentData = this.IdentData;
                    this._organizationRef = this._organization.Id;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML ParentOrganizationType
    /// </summary>
    /// <remarks>The containing organization (the university or business which a lab belongs to, etc.)</remarks>
    public partial class ParentOrganizationObj : OrganizationRefObj
    { }

    /// <summary>
    /// MzIdentML PersonType
    /// </summary>
    /// <remarks>A person's name and contact details. Any additional information such as the address, 
    /// contact email etc. should be supplied using CV parameters or user parameters.</remarks>
    public partial class PersonObj : AbstractContactObj
    {
        private IdentDataList<AffiliationObj> _affiliations;
        private string _lastName;
        private string _firstName;
        private string _midInitials;

        /// <remarks>The organization a person belongs to.</remarks>
        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<AffiliationObj> Affiliations
        {
            get { return this._affiliations; }
            set
            {
                this._affiliations = value;
                if (this._affiliations != null)
                {
                    this._affiliations.IdentData = this.IdentData;
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
    public partial class AffiliationObj : OrganizationRefObj
    { }

    /// <summary>
    /// MzIdentML ProviderType
    /// </summary>
    /// <remarks>The provider of the document in terms of the Contact and the software the produced the document instance.</remarks>
    public partial class ProviderObj : IdentDataInternalTypeAbstract, IIdentifiableType
    {
        private ContactRoleObj _contactRole;
        private string _analysisSoftwareRef;
        private AnalysisSoftwareObj _analysisSoftware;
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
        /// <remarks>min 0, max 1</remarks>
        public ContactRoleObj ContactRole
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
        protected internal string AnalysisSoftwareRef
        {
            get
            {
                if (this._analysisSoftware != null)
                {
                    return this._analysisSoftware.Id;
                }
                return this._analysisSoftwareRef;
            }
            set
            {
                this._analysisSoftwareRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.AnalysisSoftware = this.IdentData.FindAnalysisSoftware(value);
                }
            }
        }

        /// <remarks>The Software that produced the document instance.</remarks>
        /// Optional Attribute
        /// string
        public AnalysisSoftwareObj AnalysisSoftware
        {
            get { return this._analysisSoftware; }
            set
            {
                this._analysisSoftware = value;
                if (this._analysisSoftware != null)
                {
                    this._analysisSoftware.IdentData = this.IdentData;
                    this._analysisSoftwareRef = this._analysisSoftware.Id;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML AnalysisSoftwareType : Containers AnalysisSoftwareListType
    /// </summary>
    /// <remarks>The software used for performing the analyses.</remarks>
    /// 
    /// <remarks>AnalysisSoftwareListType: The software packages used to perform the analyses.</remarks>
    /// <remarks>AnalysisSoftwareListType: child element AnalysisSoftware of type AnalysisSoftwareType, min 1, max unbounded</remarks>
    public partial class AnalysisSoftwareObj : IdentDataInternalTypeAbstract, IIdentifiableType
    {
        private ContactRoleObj _contactRole;
        private ParamObj _softwareName;
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
        /// <remarks>min 0, max 1</remarks>
        public ContactRoleObj ContactRole
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
        /// <remarks>min 1, max 1</remarks>
        public ParamObj SoftwareName
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
        /// <remarks>min 0, max 1</remarks>
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
    public partial class InputsObj : IdentDataInternalTypeAbstract
    {
        private IdentDataList<SourceFileInfo> _sourceFiles;
        private IdentDataList<SearchDatabaseInfo> _searchDatabases;
        private IdentDataList<SpectraDataObj> _spectraDataList;

        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<SourceFileInfo> SourceFiles
        {
            get { return this._sourceFiles; }
            set
            {
                this._sourceFiles = value;
                if (this._sourceFiles != null)
                {
                    this._sourceFiles.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<SearchDatabaseInfo> SearchDatabases
        {
            get { return this._searchDatabases; }
            set
            {
                this._searchDatabases = value;
                if (this._searchDatabases != null)
                {
                    this._searchDatabases.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<SpectraDataObj> SpectraDataList
        {
            get { return this._spectraDataList; }
            set
            {
                this._spectraDataList = value;
                if (this._spectraDataList != null)
                {
                    this._spectraDataList.IdentData = this.IdentData;
                }
            }
        }
    }

    /// <summary>
    /// MzIdentML DataCollectionType
    /// </summary>
    /// <remarks>The collection of input and output data sets of the analyses.</remarks>
    public partial class DataCollectionObj : IdentDataInternalTypeAbstract
    {
        private InputsObj _inputs;
        private AnalysisDataObj _analysisData;

        /// <remarks>min 1, max 1</remarks>
        public InputsObj Inputs
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

        /// <remarks>min 1, max 1</remarks>
        public AnalysisDataObj AnalysisData
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
    public partial class AnalysisProtocolCollectionObj : IdentDataInternalTypeAbstract
    {
        private IdentDataList<SpectrumIdentificationProtocolObj> _spectrumIdentificationProtocols;
        private ProteinDetectionProtocolObj _proteinDetectionProtocol;

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<SpectrumIdentificationProtocolObj> SpectrumIdentificationProtocols
        {
            get { return this._spectrumIdentificationProtocols; }
            set
            {
                this._spectrumIdentificationProtocols = value;
                if (this._spectrumIdentificationProtocols != null)
                {
                    this._spectrumIdentificationProtocols.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>min 0, max 1</remarks>
        public ProteinDetectionProtocolObj ProteinDetectionProtocol
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
    public partial class AnalysisCollectionObj : IdentDataInternalTypeAbstract
    {
        private IdentDataList<SpectrumIdentificationObj> _spectrumIdentifications;
        private ProteinDetectionObj _proteinDetection;

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<SpectrumIdentificationObj> SpectrumIdentifications
        {
            get { return this._spectrumIdentifications; }
            set
            {
                this._spectrumIdentifications = value;
                if (this._spectrumIdentifications != null)
                {
                    this._spectrumIdentifications.IdentData = this.IdentData;
                }
            }
        }

        /// <remarks>min 0, max 1</remarks>
        public ProteinDetectionObj ProteinDetection
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
    public partial class SequenceCollectionObj : IdentDataInternalTypeAbstract
    {
        private IdentDataList<DbSequenceObj> _dBSequences;
        private IdentDataList<PeptideObj> _peptides;
        private IdentDataList<PeptideEvidenceObj> _peptideEvidences;

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<DbSequenceObj> DBSequences
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

        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<PeptideObj> Peptides
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

        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<PeptideEvidenceObj> PeptideEvidences
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
