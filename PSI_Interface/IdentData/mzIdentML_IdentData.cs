using System.Collections.Generic;
using PSI_Interface.CV;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData
{
    /// <summary>
    /// MzIdentML MzIdentMLType
    /// </summary>
    /// <remarks>The upper-most hierarchy level of mzIdentML with sub-containers for example describing software, 
    /// protocols and search results (spectrum identifications or protein detection results).</remarks>
    public partial class IdentDataObj : IIdentifiableType
    {
        public IdentDataObj(MzIdentMLType mzid)
        {
            this._id = mzid.id;
            this._name = mzid.name;
            this._version = mzid.version;
            this._creationDate = mzid.creationDate;
            this.CreationDateSpecified = mzid.creationDateSpecified;

            this.CvTranslator = new CVTranslator();
            this._cvList = null;
            this._analysisSoftwareList = null;
            this._provider = null;
            this._auditCollection = null;
            this._analysisSampleCollection = null;
            this._sequenceCollection = null;
            this._analysisCollection = null;
            this._analysisProtocolCollection = null;
            this._dataCollection = null;
            this._bibliographicReferences = null;

            // Referenced by anything using CV/User params
            if (mzid.cvList != null && mzid.cvList.Count > 0)
            {
                this.CVList = new IdentDataList<CVInfo>();
                foreach (var cv in mzid.cvList)
                {
                    this.CVList.Add(new CVInfo(cv, this));
                }
                this.CvTranslator = new CVTranslator(this._cvList);
            }
            // Referenced by nothing
            if (mzid.BibliographicReference != null && mzid.BibliographicReference.Count > 0)
            {
                this.BibliographicReferences = new IdentDataList<BibliographicReferenceObj>();
                foreach (var br in mzid.BibliographicReference)
                {
                    this.BibliographicReferences.Add(new BibliographicReferenceObj(br, this));
                }
            }
            // Referenced by anything using organization, person, contactRoleInfo - SampleInfo, ProviderInfo, AnalysisSoftwareInfo
            if (mzid.AuditCollection != null && mzid.AuditCollection.Count > 0)
            {
                this.AuditCollection = new IdentDataList<AbstractContactObj>();
                foreach (var ac in mzid.AuditCollection)
                {
                    if (ac is PersonType)
                    {
                        this.AuditCollection.Add(new PersonObj(ac as PersonType, this));
                    }
                    else if (ac is OrganizationType)
                    {
                        this.AuditCollection.Add(new OrganizationObj(ac as OrganizationType, this));
                    }
                }
            }
            // Referenced by anything using SampleInfo: SubSample, SpectrumIdentificationItem
            if (mzid.AnalysisSampleCollection != null && mzid.AnalysisSampleCollection.Count > 0)
            {
                this.AnalysisSampleCollection = new IdentDataList<SampleObj>();
                foreach (var asc in mzid.AnalysisSampleCollection)
                {
                    this.AnalysisSampleCollection.Add(new SampleObj(asc, this));
                }
            }
            // Referenced by ProviderInfo, ProteinDetectionProtocol, SpectrumIdentificationProtocol, references AbstractContactInfo through ContactRoleInfo
            if (mzid.AnalysisSoftwareList != null && mzid.AnalysisSoftwareList.Count > 0)
            {
                this.AnalysisSoftwareList = new IdentDataList<AnalysisSoftwareObj>();
                foreach (var asl in mzid.AnalysisSoftwareList)
                {
                    this.AnalysisSoftwareList.Add(new AnalysisSoftwareObj(asl, this));
                }
            }
            // Referenced by nothing, references AnalysisSoftwareInfo
            if (mzid.Provider != null)
            {
                this._provider = new ProviderObj(mzid.Provider, this);
            }
            // Referenced by SpectrumIdentification, ProteinDetection, SpectrumIdentificationItem, PeptideEvidence, references AnalysisSoftwareInfo
            if (mzid.AnalysisProtocolCollection != null)
            {
                this._analysisProtocolCollection = new AnalysisProtocolCollectionObj(mzid.AnalysisProtocolCollection, this);
            }
            // InputsInfo referenced by DBSequence, SpectrumIdentification.SearchDatabaseRefInfo, SpectrumIdentificationResult, SpectrumIdentification.InputSpectraRef
            if (mzid.DataCollection != null)
            {
                //this._dataCollection = new DataCollection(mzid.DataCollection, this);
                this._dataCollection = new DataCollectionObj();
                this._dataCollection.Inputs = new InputsObj(mzid.DataCollection.Inputs, this);
            }
            // Referenced by SpectrumIdentificationItem, ProteinDetectionHypothesis, PeptideHypothesis
            // References InputsInfo.DBSequence, AnalysisProtocolCollection.SoftwareIdentificationProtocol.DatabaseTranslation.TranslationTable
            if (mzid.SequenceCollection != null)
            {
                this._sequenceCollection = new SequenceCollectionObj(mzid.SequenceCollection, this);
            }
            // AnalysisData referenced by SpectrumIdentification, InputSpectrumIdentifications, ProteinDetection, references Peptides, PeptideEvidence, SampleInfo, MassTable, 
            if (mzid.DataCollection != null)
            {
                this._dataCollection.AnalysisData = new AnalysisDataObj(mzid.DataCollection.AnalysisData, this);
            }
            // References SpectrumIdentificationProtocol, SpectrumIdentificationList, SpectraData, SeqrchDatabaseInfo, ProteinDetectionList, ProteinDetectionProtocol
            if (mzid.AnalysisCollection != null)
            {
                this._analysisCollection = new AnalysisCollectionObj(mzid.AnalysisCollection, this);
            }
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// min 1, max 1
        //public IdentDataList<CVInfo> CVList

        /// min 0, max 1
        //public IdentDataList<AnalysisSoftwareInfo> AnalysisSoftwareList

        /// <remarks>The Provider of the mzIdentML record in terms of the contact and software.</remarks>
        /// min 0, max 1
        //public ProviderInfo Provider

        /// min 0, max 1
        //public IdentDataList<AbstractContactInfo> AuditCollection

        /// min 0, max 1
        //public IdentDataList<SampleInfo> AnalysisSampleCollection

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
        //public IdentDataList<BibliographicReference> BibliographicReferences

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
        //public string Version
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
        public CVInfo(cvType cv, IdentDataObj idata)
            : base(idata)
        {
            this.FullName = cv.fullName;
            this.Version = cv.version;
            this.URI = cv.uri;
            this.Id = cv.id;
        }

        /// <remarks>The full name of the CV.</remarks>
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
        //public string Id
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationItemRefType
    /// </summary>
    /// <remarks>Reference(s) to the SpectrumIdentificationItem element(s) that support the given PeptideEvidence element. 
    /// Using these references it is possible to indicate which spectra were actually accepted as evidence for this 
    /// peptide identification in the given protein.</remarks>
    public partial class SpectrumIdentificationItemRefObj : IdentDataInternalTypeAbstract
    {
        public SpectrumIdentificationItemRefObj(SpectrumIdentificationItemRefType siir, IdentDataObj idata)
            : base(idata)
        {
            this.SpectrumIdentificationItemRef = siir.spectrumIdentificationItem_ref;
        }

        /// <remarks>A reference to the SpectrumIdentificationItem element(s).</remarks>
        /// Required Attribute
        /// string
        //public string SpectrumIdentificationItemRef
    }

    /// <summary>
    /// MzIdentML PeptideHypothesisType
    /// </summary>
    /// <remarks>Peptide evidence on which this ProteinHypothesis is based by reference to a PeptideEvidence element.</remarks>
    public partial class PeptideHypothesisObj : IdentDataInternalTypeAbstract
    {
        public PeptideHypothesisObj(PeptideHypothesisType ph, IdentDataObj idata)
            : base(idata)
        {
            this.PeptideEvidenceRef = ph.peptideEvidence_ref;

            this._spectrumIdentificationItems = null;

            if (ph.SpectrumIdentificationItemRef != null && ph.SpectrumIdentificationItemRef.Count > 0)
            {
                this.SpectrumIdentificationItems = new IdentDataList<SpectrumIdentificationItemRefObj>();
                foreach (var siir in ph.SpectrumIdentificationItemRef)
                {
                    this.SpectrumIdentificationItems.Add(new SpectrumIdentificationItemRefObj(siir, this.IdentData));
                }
            }
        }

        /// min 1, max unbounded
        //public IdentDataList<SpectrumIdentificationItemRefInfo> SpectrumIdentificationItemRef

        /// <remarks>A reference to the PeptideEvidence element on which this hypothesis is based.</remarks>
        /// Required Attribute
        /// string
        //public string PeptideEvidenceRef
    }

    /// <summary>
    /// MzIdentML FragmentArrayType
    /// </summary>
    /// <remarks>An array of values for a given type of measure and for a particular ion type, in parallel to the index of ions identified.</remarks>
    public partial class FragmentArrayObj : IdentDataInternalTypeAbstract
    {
        public FragmentArrayObj(FragmentArrayType fa, IdentDataObj idata)
            : base(idata)
        {
            this.MeasureRef = fa.measure_ref;

            this._values = null;
            if (fa.values != null)
            {
                this._values = new List<float>(fa.values);
            }
        }

        /// <remarks>The values of this particular measure, corresponding to the index defined in ion type</remarks>
        /// Required Attribute
        /// listOfFloats: string, space-separated floats
        //public List<float> Values

        /// <remarks>A reference to the Measure defined in the FragmentationTable</remarks>
        /// Required Attribute
        /// string
        //public string MeasureRef
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
        public IonTypeObj(IonTypeType it, IdentDataObj idata)
            : base(idata)
        {
            this._charge = it.charge;

            this._fragmentArrays = null;
            this._cvParam = null;
            this._index = null;

            if (it.FragmentArray != null && it.FragmentArray.Count > 0)
            {
                this.FragmentArrays = new IdentDataList<FragmentArrayObj>();
                foreach (var f in it.FragmentArray)
                {
                    this.FragmentArrays.Add(new FragmentArrayObj(f, this.IdentData));
                }
            }
        }

        /// min 0, max unbounded
        //public IdentDataList<FragmentArray> FragmentArray

        /// <remarks>The type of ion identified.</remarks>
        /// min 1, max 1
        //public CVParam CVParam

        /// <remarks>The index of ions identified as integers, following standard notation for a-c, x-z e.g. if b3 b5 and b6 have been identified, the index would store "3 5 6". For internal ions, the index contains pairs defining the start and end point - see specification document for examples. For immonium ions, the index is the position of the identified ion within the peptide sequence - if the peptide contains the same amino acid in multiple positions that cannot be distinguished, all positions should be given.</remarks>
        /// Optional Attribute
        /// listOfIntegers: string, space-separated integers
        //public List<string> Index

        /// <remarks>The charge of the identified fragmentation ions.</remarks>
        /// Required Attribute
        /// integer
        //public int Charge
    }

    /// <summary>
    /// MzIdentML CVParamType; Container types: ToleranceType
    /// </summary>
    /// <remarks>A single entry from an ontology or a controlled vocabulary.</remarks>
    /// <remarks>ToleranceType: The tolerance of the search given as a plus and minus value with units.</remarks>
    /// <remarks>ToleranceType: child element cvParam of type CVParamType, min 1, max unbounded "CV terms capturing the tolerance plus and minus values."</remarks>
    public partial class CVParamObj : ParamBaseObj
    {
        //public CVParam(CVParamType cvp, IdentData idata)
        //    : base(cvp, idata)
        public CVParamObj(CVParamType cvp, IdentDataObj idata)
            : base(idata)
        {
            this.CVRef = cvp.cvRef;
            //this._name = cvp.name;
            //this._accession = cvp.accession;
            this.Accession = cvp.accession;
            this._value = cvp.value;

            this.UnitCvRef = cvp.unitCvRef;
            //this._unitAccession = cvp.unitAccession;
            this.UnitAccession = cvp.unitAccession;
            //this._unitName = cvp.unitName;

            //this._cvid = CV.CV.CVID.CVID_Unknown;
        }

        //public CV.CV.CVID Cvid

        /// <remarks>A reference to the cv element from which this term originates.</remarks>
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
        //public override string Value
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
        /*public ParamBase(AbstractParamType ap, IdentData idata)
            : base(idata)
        {
            this._unitsSet = false;
            this.UnitCvRef = ap.unitCvRef;
            //this._unitAccession = ap.unitAccession;
            this.UnitAccession = ap.unitAccession;
            //this._unitName = ap.unitName;

            //this._unitCvid = CV.CV.CVID.CVID_Unknown;
        }*/

        public ParamBaseObj(IdentDataObj idata)
            : base(idata)
        {
            this._unitsSet = false;
            //this.UnitCvRef = ap.unitCvRef;
            //this._unitAccession = ap.unitAccession;
            //this.UnitAccession = ap.unitAccession;
            //this._unitName = ap.unitName;

            //this._unitCvid = CV.CV.CVID.CVID_Unknown;
        }

        // Name and value are abstract properties, because name will be handled differently in CVParams, and value can also have restrictions based on the CVParam.

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
    }

    /// <summary>
    /// MzIdentML UserParamType
    /// </summary>
    /// <remarks>A single user-defined parameter.</remarks>
    public partial class UserParamObj : ParamBaseObj
    {
        //public UserParam(UserParamType up, IdentData idata)
        //    : base(up, idata)
        public UserParamObj(UserParamType up, IdentDataObj idata)
            : base(idata)
        {
            this._name = up.name;
            this._value = up.value;
            this._type = up.type;

            this.UnitCvRef = up.unitCvRef;
            //this._unitAccession = up.unitAccession;
            this.UnitAccession = up.unitAccession;
            //this._unitName = up.unitName;
        }

        /// <remarks>The name of the parameter.</remarks>
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
        //public string Type
    }

    public partial class CVParamGroupObj : IdentDataInternalTypeAbstract
    {
        public CVParamGroupObj(ICVParamGroup cvpg, IdentDataObj idata)
            : base(idata)
        {
            this._cvParams = null;

            if (cvpg.cvParam != null && cvpg.cvParam.Count > 0)
            {
                this.CVParams = new IdentDataList<CVParamObj>();
                foreach (var cvp in cvpg.cvParam)
                {
                    this.CVParams.Add(new CVParamObj(cvp, this.IdentData));
                }
            }
        }

        //public IdentDataList<CVParam> CVParams
    }

    public partial class ParamGroupObj : CVParamGroupObj
    {
        public ParamGroupObj(IParamGroup pg, IdentDataObj idata)
            : base(pg, idata)
        {
            this._userParams = null;

            if (pg.userParam != null && pg.userParam.Count > 0)
            {
                this.UserParams = new IdentDataList<UserParamObj>();
                foreach (var up in pg.userParam)
                {
                    this.UserParams.Add(new UserParamObj(up, this.IdentData));
                }
            }
        }

        //public IdentDataList<UserParam> UserParams
    }

    /// <summary>
    /// MzIdentML ParamType
    /// </summary>
    /// <remarks>Helper type to allow either a cvParam or a userParam to be provided for an element.</remarks>
    public partial class ParamObj : IdentDataInternalTypeAbstract
    {
        public ParamObj(ParamType p, IdentDataObj idata)
            : base(idata)
        {
            this._item = null;

            if (p.Item != null)
            {
                if (p.Item is CVParamType)
                {
                    this._item = new CVParamObj(p.Item as CVParamType, this.IdentData);
                }
                else if (p.Item is UserParamType)
                {
                    this._item = new UserParamObj(p.Item as UserParamType, this.IdentData);
                }
            }
        }

        /// min 1, max 1
        //public ParamBase Item
    }

    /// <summary>
    /// MzIdentML ParamListType
    /// </summary>
    /// <remarks>Helper type to allow multiple cvParams or userParams to be given for an element.</remarks>
    public partial class ParamListObj : IdentDataInternalTypeAbstract
    {
        public ParamListObj(ParamListType pl, IdentDataObj idata)
            : base(idata)
        {
            this._items = null;

            if (pl != null && pl.Items.Count > 0)
            {
                this.Items = new IdentDataList<ParamBaseObj>();
                foreach (var p in pl.Items)
                {
                    if (p is CVParamType)
                    {
                        this.Items.Add(new CVParamObj(p as CVParamType, this.IdentData));
                    }
                    else if (p is UserParamType)
                    {
                        this.Items.Add(new UserParamObj(p as UserParamType, this.IdentData));
                    }
                }
            }
        }

        /// min 1, max unbounded
        //public IdentDataList<ParamBase> Items
    }

    /// <summary>
    /// MzIdentML PeptideEvidenceRefType
    /// </summary>
    /// <remarks>Reference to the PeptideEvidence element identified. If a specific sequence can be assigned to multiple 
    /// proteins and or positions in a protein all possible PeptideEvidence elements should be referenced here.</remarks>
    public partial class PeptideEvidenceRefObj : IdentDataInternalTypeAbstract
    {
        public PeptideEvidenceRefObj(PeptideEvidenceRefType per, IdentDataObj idata)
            : base(idata)
        {
            this.PeptideEvidenceRef = per.peptideEvidence_ref;
        }

        /// <remarks>A reference to the PeptideEvidenceItem element(s).</remarks>
        /// Required Attribute
        /// string
        //public string PeptideEvidenceRef
    }

    /// <summary>
    /// MzIdentML AnalysisDataType
    /// </summary>
    /// <remarks>Data sets generated by the analyses, including peptide and protein lists.</remarks>
    public partial class AnalysisDataObj : IdentDataInternalTypeAbstract
    {
        public AnalysisDataObj(AnalysisDataType ad, IdentDataObj idata)
            : base(idata)
        {
            this._spectrumIdentificationList = null;
            this._proteinDetectionList = null;

            if (ad.SpectrumIdentificationList != null && ad.SpectrumIdentificationList.Count > 0)
            {
                this.SpectrumIdentificationList = new IdentDataList<SpectrumIdentificationListObj>();
                foreach (var sil in ad.SpectrumIdentificationList)
                {
                    this.SpectrumIdentificationList.Add(new SpectrumIdentificationListObj(sil, this.IdentData));
                }
            }
            if (ad.ProteinDetectionList != null)
            {
                this._proteinDetectionList = new ProteinDetectionListObj(ad.ProteinDetectionList, this.IdentData);
            }
        }

        /// min 1, max unbounded
        //public IdentDataList<SpectrumIdentificationList> SpectrumIdentificationList

        /// min 0, max 1
        //public ProteinDetectionList ProteinDetectionList
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationListType
    /// </summary>
    /// <remarks>Represents the set of all search results from SpectrumIdentification.</remarks>
    public partial class SpectrumIdentificationListObj : ParamGroupObj, IIdentifiableType
    {
        public SpectrumIdentificationListObj(SpectrumIdentificationListType sil, IdentDataObj idata)
            : base(sil, idata)
        {
            this._id = sil.id;
            this._name = sil.name;
            this._numSequencesSearched = sil.numSequencesSearched;
            this.NumSequencesSearchedSpecified = sil.numSequencesSearchedSpecified;

            this._fragmentationTables = null;
            this._spectrumIdentificationResults = null;

            if (sil.FragmentationTable != null && sil.FragmentationTable.Count > 0)
            {
                this.FragmentationTables = new IdentDataList<MeasureObj>();
                foreach (var f in sil.FragmentationTable)
                {
                    this.FragmentationTables.Add(new MeasureObj(f, this.IdentData));
                }
            }
            if (sil.SpectrumIdentificationResult != null && sil.SpectrumIdentificationResult.Count > 0)
            {
                this.SpectrumIdentificationResults = new IdentDataList<SpectrumIdentificationResultObj>();
                foreach (var sir in sil.SpectrumIdentificationResult)
                {
                    this.SpectrumIdentificationResults.Add(new SpectrumIdentificationResultObj(sir, this.IdentData));
                }
            }
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// min 0, max 1
        //public IdentDataList<Measure> FragmentationTable

        /// min 1, max unbounded
        //public IdentDataList<SpectrumIdentificationResult> SpectrumIdentificationResult

        /// <remarks>___ParamGroup___:Scores or output parameters associated with the SpectrumIdentificationList.</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams

        /// <remarks>The number of database sequences searched against. This value should be provided unless a de novo search has been performed.</remarks>
        /// Optional Attribute
        /// long
        //public long NumSequencesSearched

        /// Attribute Existence
        //public bool NumSequencesSearchedSpecified
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
        public MeasureObj(MeasureType m, IdentDataObj idata)
            : base(m, idata)
        {
            this._id = m.id;
            this._name = m.name;
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// min 1, max unbounded
        //public IdentDataList<CVParamType> CVParams
    }

    /// <summary>
    /// MzIdentML IdentifiableType
    /// </summary>
    /// <remarks>Other classes in the model can be specified as sub-classes, inheriting from Identifiable. 
    /// Identifiable gives classes a unique identifier within the scope and a name that need not be unique.</remarks>
    /*public partial interface IIdentifiableType
    {
        public IIdentifiableType()
        {
            this.Id = null;
            this.Name = null;
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //string Id 

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //string Name 
    }*/

    /// <summary>
    /// MzIdentML BibliographicReferenceType
    /// </summary>
    /// <remarks>Represents bibliographic references.</remarks>
    public partial class BibliographicReferenceObj : IdentDataInternalTypeAbstract, IIdentifiableType
    {
        public BibliographicReferenceObj(BibliographicReferenceType br, IdentDataObj idata)
            : base(idata)
        {
            this._year = br.year;
            this.YearSpecified = br.yearSpecified;
            this._id = br.id;
            this._name = br.name;
            this.Authors = br.authors;
            this.Publication = br.publication;
            this.Publisher = br.publisher;
            this.Editor = br.editor;
            this.Volume = br.volume;
            this.Issue = br.issue;
            this.Pages = br.pages;
            this.Title = br.title;
            this.DOI = br.doi;
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// <remarks>The names of the authors of the reference.</remarks>
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
        //public string DOI
    }

    /// <summary>
    /// MzIdentML ProteinDetectionHypothesisType
    /// </summary>
    /// <remarks>A single result of the ProteinDetection analysis (i.e. a protein).</remarks>
    public partial class ProteinDetectionHypothesisObj : ParamGroupObj, IIdentifiableType
    {
        public ProteinDetectionHypothesisObj(ProteinDetectionHypothesisType pdh, IdentDataObj idata)
            : base(pdh, idata)
        {
            this._id = pdh.id;
            this._name = pdh.name;
            this.DBSequenceRef = pdh.dBSequence_ref;
            this._passThreshold = pdh.passThreshold;

            this._peptideHypotheses = null;

            if (pdh.PeptideHypothesis != null && pdh.PeptideHypothesis.Count > 0)
            {
                this.PeptideHypotheses = new IdentDataList<PeptideHypothesisObj>();
                foreach (var ph in pdh.PeptideHypothesis)
                {
                    this.PeptideHypotheses.Add(new PeptideHypothesisObj(ph, this.IdentData));
                }
            }
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// min 1, max unbounded
        //public IdentDataList<PeptideHypothesis> PeptideHypothesis

        /// <remarks>___ParamGroup___:Scores or parameters associated with this ProteinDetectionHypothesis e.g. p-value</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams

        /// <remarks>A reference to the corresponding DBSequence entry. This optional and redundant, because the PeptideEvidence 
        /// elements referenced from here also map to the DBSequence.</remarks>
        /// Optional Attribute
        /// string
        //public string DBSequenceRef

        /// <remarks>Set to true if the producers of the file has deemed that the ProteinDetectionHypothesis has passed a given 
        /// threshold or been validated as correct. If no such threshold has been set, value of true should be given for all results.</remarks>
        /// Required Attribute
        /// boolean
        //public bool PassThreshold
    }

    /// <summary>
    /// MzIdentML ProteinAmbiguityGroupType
    /// </summary>
    /// <remarks>A set of logically related results from a protein detection, for example to represent conflicting assignments of peptides to proteins.</remarks>
    public partial class ProteinAmbiguityGroupObj : ParamGroupObj, IIdentifiableType
    {
        public ProteinAmbiguityGroupObj(ProteinAmbiguityGroupType pag, IdentDataObj idata)
            : base(pag, idata)
        {
            this._id = pag.id;
            this._name = pag.name;

            this._proteinDetectionHypotheses = null;

            if (pag.ProteinDetectionHypothesis != null && pag.ProteinDetectionHypothesis.Count > 0)
            {
                this.ProteinDetectionHypotheses = new IdentDataList<ProteinDetectionHypothesisObj>();
                foreach (var pdh in pag.ProteinDetectionHypothesis)
                {
                    this.ProteinDetectionHypotheses.Add(new ProteinDetectionHypothesisObj(pdh, this.IdentData));
                }
            }
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// min 1, max unbounded
        //public IdentDataList<ProteinDetectionHypothesis> ProteinDetectionHypothesis

        /// <remarks>___ParamGroup___:Scores or parameters associated with the ProteinAmbiguityGroup.</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams
    }

    /// <summary>
    /// MzIdentML ProteinDetectionListType
    /// </summary>
    /// <remarks>The protein list resulting from a protein detection process.</remarks>
    public partial class ProteinDetectionListObj : ParamGroupObj, IIdentifiableType
    {
        public ProteinDetectionListObj(ProteinDetectionListType pdl, IdentDataObj idata)
            : base(pdl, idata)
        {
            this._id = pdl.id;
            this._name = pdl.name;

            this._proteinAmbiguityGroups = null;

            if (pdl.ProteinAmbiguityGroup != null && pdl.ProteinAmbiguityGroup.Count > 0)
            {
                this.ProteinAmbiguityGroups = new IdentDataList<ProteinAmbiguityGroupObj>();
                foreach (var pag in pdl.ProteinAmbiguityGroup)
                {
                    this.ProteinAmbiguityGroups.Add(new ProteinAmbiguityGroupObj(pag, this.IdentData));
                }
            }
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// min 0, max unbounded
        //public IdentDataList<ProteinAmbiguityGroup> ProteinAmbiguityGroup

        /// <remarks>___ParamGroup___:Scores or output parameters associated with the whole ProteinDetectionList</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationItemType
    /// </summary>
    /// <remarks>An identification of a single (poly)peptide, resulting from querying an input spectra, along with 
    /// the set of confidence values for that identification. PeptideEvidence elements should be given for all 
    /// mappings of the corresponding Peptide sequence within protein sequences.</remarks>
    public partial class SpectrumIdentificationItemObj : ParamGroupObj, IIdentifiableType
    {
        public SpectrumIdentificationItemObj(SpectrumIdentificationItemType sii, IdentDataObj idata)
            : base(sii, idata)
        {
            this._id = sii.id;
            this._name = sii.name;
            this._chargeState = sii.chargeState;
            this._experimentalMassToCharge = sii.experimentalMassToCharge;
            this._calculatedMassToCharge = sii.calculatedMassToCharge;
            this.CalculatedMassToChargeSpecified = sii.calculatedMassToChargeSpecified;
            this._calculatedPI = sii.calculatedPI;
            this.CalculatedPISpecified = sii.calculatedPISpecified;
            this.PeptideRef = sii.peptide_ref;
            this._rank = sii.rank;
            this._passThreshold = sii.passThreshold;
            this.MassTableRef = sii.massTable_ref;
            this.SampleRef = sii.sample_ref;

            this._peptideEvidences = null;
            this._fragmentations = null;

            if (sii.PeptideEvidenceRef != null && sii.PeptideEvidenceRef.Count > 0)
            {
                this.PeptideEvidences = new IdentDataList<PeptideEvidenceRefObj>();
                foreach (var pe in sii.PeptideEvidenceRef)
                {
                    this.PeptideEvidences.Add(new PeptideEvidenceRefObj(pe, this.IdentData));
                }
            }
            if (sii.Fragmentation != null && sii.Fragmentation.Count > 0)
            {
                this.Fragmentations = new IdentDataList<IonTypeObj>();
                foreach (var f in sii.Fragmentation)
                {
                    this.Fragmentations.Add(new IonTypeObj(f, this.IdentData));
                }
            }
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// min 1, max unbounded
        //public IdentDataList<PeptideEvidenceRefInfo> PeptideEvidenceRef

        /// min 0, max 1
        //public IdentDataList<IonTypeInfo> Fragmentation

        /// <remarks>___ParamGroup___:Scores or attributes associated with the SpectrumIdentificationItem e.g. e-value, p-value, score.</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams

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
        //public string SampleRef
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationResultType
    /// </summary>
    /// <remarks>All identifications made from searching one spectrum. For PMF data, all peptide identifications 
    /// will be listed underneath as SpectrumIdentificationItems. For MS/MS data, there will be ranked 
    /// SpectrumIdentificationItems corresponding to possible different peptide IDs.</remarks>
    public partial class SpectrumIdentificationResultObj : ParamGroupObj, IIdentifiableType
    {
        public SpectrumIdentificationResultObj(SpectrumIdentificationResultType sir, IdentDataObj idata)
            : base(sir, idata)
        {
            this._id = sir.id;
            this._name = sir.name;
            this._spectrumID = sir.spectrumID;
            this.SpectraDataRef = sir.spectraData_ref;

            this._spectrumIdentificationItems = null;

            if (sir.SpectrumIdentificationItem != null && sir.SpectrumIdentificationItem.Count > 0)
            {
                this.SpectrumIdentificationItems = new IdentDataList<SpectrumIdentificationItemObj>();
                foreach (var sii in sir.SpectrumIdentificationItem)
                {
                    this.SpectrumIdentificationItems.Add(new SpectrumIdentificationItemObj(sii, this.IdentData));
                }
            }
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// min 1, max unbounded
        //public IdentDataList<SpectrumIdentificationItem> SpectrumIdentificationItems

        /// <remarks>___ParamGroup___: Scores or parameters associated with the SpectrumIdentificationResult 
        /// (i.e the set of SpectrumIdentificationItems derived from one spectrum) e.g. the number of peptide 
        /// sequences within the parent tolerance for this spectrum.</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams

        /// <remarks>The locally unique id for the spectrum in the spectra data set specified by SpectraData_ref. 
        /// External guidelines are provided on the use of consistent identifiers for spectra in different external formats.</remarks>
        /// Required Attribute
        /// string
        //public string SpectrumID

        /// <remarks>A reference to a spectra data set (e.g. a spectra file).</remarks>
        /// Required Attribute
        /// string
        //public string SpectraDataRef
    }

    /// <summary>
    /// MzIdentML ExternalDataType
    /// </summary>
    /// <remarks>Data external to the XML instance document. The location of the data file is given in the location attribute.</remarks>
    /*public partial interface IExternalDataType : IIdentifiableType
    {
        public IExternalDataType()
        {
            this.ExternalFormatDocumentation = null;
            this.Location = null;
            this.FileFormat = null;
        }

        /// <remarks>A URI to access documentation and tools to interpret the external format of the ExternalData instance. 
        /// For example, XML Schema or static libraries (APIs) to access binary formats.</remarks>
        /// min 0, max 1
        string ExternalFormatDocumentation 

        /// min 0, max 1
        FileFormatInfo FileFormat 

        /// <remarks>The location of the data file.</remarks>
        /// Required Attribute
        /// string
        string Location 
    }*/

    /// <summary>
    /// MzIdentML FileFormatType
    /// </summary>
    /// <remarks>The format of the ExternalData file, for example "tiff" for image files.</remarks>
    public partial class FileFormatInfo : IdentDataInternalTypeAbstract
    {
        public FileFormatInfo(FileFormatType ff, IdentDataObj idata)
            : base(idata)
        {
            this._cvParam = null;

            if (ff.cvParam != null)
            {
                this._cvParam = new CVParamObj(ff.cvParam, this.IdentData);
            }
        }

        /// <remarks>cvParam capturing file formats</remarks>
        /// Optional Attribute
        /// min 1, max 1
        //public CVParam CVParam
    }

    /// <summary>
    /// MzIdentML SpectraDataType
    /// </summary>
    /// <remarks>A data set containing spectra data (consisting of one or more spectra).</remarks>
    public partial class SpectraDataObj : IdentDataInternalTypeAbstract, IExternalDataType
    {
        public SpectraDataObj(SpectraDataType sd, IdentDataObj idata)
            : base(idata)
        {
            this._id = sd.id;
            this._name = sd.name;
            this._externalFormatDocumentation = sd.ExternalFormatDocumentation;
            this._location = sd.location;

            this._spectrumIDFormat = null;
            this._fileFormat = null;

            if (sd.SpectrumIDFormat != null)
            {
                this._spectrumIDFormat = new SpectrumIDFormatObj(sd.SpectrumIDFormat, this.IdentData);
            }
            if (sd.FileFormat != null)
            {
                this._fileFormat = new FileFormatInfo(sd.FileFormat, this.IdentData);
            }
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// <remarks>A URI to access documentation and tools to interpret the external format of the ExternalData instance. 
        /// For example, XML Schema or static libraries (APIs) to access binary formats.</remarks>
        /// min 0, max 1
        //public string ExternalFormatDocumentation

        /// min 0, max 1
        //public FileFormatInfo FileFormat

        /// <remarks>The location of the data file.</remarks>
        /// Required Attribute
        /// string
        //public string Location

        /// min 1, max 1
        //public SpectrumIDFormat SpectrumIDFormat
    }

    /// <summary>
    /// MzIdentML SpectrumIDFormatType
    /// </summary>
    /// <remarks>The format of the spectrum identifier within the source file</remarks>
    public partial class SpectrumIDFormatObj : IdentDataInternalTypeAbstract
    {
        public SpectrumIDFormatObj(SpectrumIDFormatType sidf, IdentDataObj idata)
            : base(idata)
        {
            this._cvParam = null;

            if (sidf.cvParam != null)
            {
                this._cvParam = new CVParamObj(sidf.cvParam, this.IdentData);
            }
        }

        /// <remarks>CV term capturing the type of identifier used.</remarks>
        /// min 1, max 1
        //public CVParam CVParam
    }

    /// <summary>
    /// MzIdentML SourceFileType
    /// </summary>
    /// <remarks>A file from which this mzIdentML instance was created.</remarks>
    public partial class SourceFileInfo : ParamGroupObj, IExternalDataType
    {
        public SourceFileInfo(SourceFileType sf, IdentDataObj idata)
            : base(sf, idata)
        {
            this._id = sf.id;
            this._name = sf.name;
            this._externalFormatDocumentation = sf.ExternalFormatDocumentation;
            this._location = sf.location;

            this._fileFormat = null;

            if (sf.FileFormat != null)
            {
                this._fileFormat = new FileFormatInfo(sf.FileFormat, this.IdentData);
            }
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// <remarks>A URI to access documentation and tools to interpret the external format of the ExternalData instance. 
        /// For example, XML Schema or static libraries (APIs) to access binary formats.</remarks>
        /// min 0, max 1
        //public string ExternalFormatDocumentation

        /// min 0, max 1
        //public FileFormatInfo FileFormat

        /// <remarks>The location of the data file.</remarks>
        /// Required Attribute
        /// string
        //public string Location

        /// <remarks>___ParamGroup___:Any additional parameters description the source file.</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams
    }

    /// <summary>
    /// MzIdentML SearchDatabaseType
    /// </summary>
    /// <remarks>A database for searching mass spectra. Examples include a set of amino acid sequence entries, or annotated spectra libraries.</remarks>
    public partial class SearchDatabaseInfo : CVParamGroupObj, IExternalDataType
    {
        public SearchDatabaseInfo(SearchDatabaseType sd, IdentDataObj idata)
            : base(sd, idata)
        {
            this._id = sd.id;
            this._name = sd.name;
            this._version = sd.version;
            this._releaseDate = sd.releaseDate;
            this.ReleaseDateSpecified = sd.releaseDateSpecified;
            this._numDatabaseSequences = sd.numDatabaseSequences;
            this.NumDatabaseSequencesSpecified = sd.numDatabaseSequencesSpecified;
            this._numResidues = sd.numResidues;
            this.NumResiduesSpecified = sd.numResiduesSpecified;
            this._externalFormatDocumentation = sd.ExternalFormatDocumentation;
            this._location = sd.location;

            this._databaseName = null;
            this._fileFormat = null;

            if (sd.DatabaseName != null)
            {
                this._databaseName = new ParamObj(sd.DatabaseName, this.IdentData);
            }
            if (sd.FileFormat != null)
            {
                this._fileFormat = new FileFormatInfo(sd.FileFormat, this.IdentData);
            }
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// <remarks>A URI to access documentation and tools to interpret the external format of the ExternalData instance. 
        /// For example, XML Schema or static libraries (APIs) to access binary formats.</remarks>
        /// min 0, max 1
        //public string ExternalFormatDocumentation

        /// min 0, max 1
        //public FileFormatInfo FileFormat

        /// <remarks>The location of the data file.</remarks>
        /// Required Attribute
        /// string
        //public string Location

        /// <remarks>The database name may be given as a cvParam if it maps exactly to one of the release databases listed in the CV, otherwise a userParam should be used.</remarks>
        /// min 1, max 1
        //public Param DatabaseName

        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams

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
        //public bool NumResiduesSpecified
    }

    /// <summary>
    /// MzIdentML ProteinDetectionProtocolType
    /// </summary>
    /// <remarks>The parameters and settings of a ProteinDetection process.</remarks>
    public partial class ProteinDetectionProtocolObj : IdentDataInternalTypeAbstract, IIdentifiableType
    {
        public ProteinDetectionProtocolObj(ProteinDetectionProtocolType pdp, IdentDataObj idata)
            : base(idata)
        {
            this._id = pdp.id;
            this._name = pdp.name;
            this.AnalysisSoftwareRef = pdp.analysisSoftware_ref;

            this._analysisParams = null;
            this._threshold = null;

            if (pdp.AnalysisParams != null)
            {
                this._analysisParams = new ParamListObj(pdp.AnalysisParams, this.IdentData);
            }
            if (pdp.Threshold != null)
            {
                this._threshold = new ParamListObj(pdp.Threshold, this.IdentData);
            }
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// <remarks>The parameters and settings for the protein detection given as CV terms.</remarks>
        /// min 0, max 1
        //public ParamList AnalysisParams

        /// <remarks>The threshold(s) applied to determine that a result is significant. 
        /// If multiple terms are used it is assumed that all conditions are satisfied by the passing results.</remarks>
        /// min 1, max 1
        //public ParamList Threshold

        /// <remarks>The protein detection software used, given as a reference to the SoftwareCollection section.</remarks>
        /// Required Attribute
        /// string
        //public string AnalysisSoftwareRef
    }

    /// <summary>
    /// MzIdentML TranslationTableType
    /// </summary>
    /// <remarks>The table used to translate codons into nucleic acids e.g. by reference to the NCBI translation table.</remarks>
    public partial class TranslationTableObj : CVParamGroupObj, IIdentifiableType
    {
        public TranslationTableObj(TranslationTableType tt, IdentDataObj idata)
            : base(tt, idata)
        {
            this._id = tt.id;
            this._name = tt.name;
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// <remarks>The details specifying this translation table are captured as cvParams, e.g. translation table, translation 
        /// start codons and translation table description (see specification document and mapping file)</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams
    }

    /// <summary>
    /// MzIdentML MassTableType
    /// </summary>
    /// <remarks>The masses of residues used in the search.</remarks>
    public partial class MassTableObj : ParamGroupObj, IIdentifiableType
    {
        public MassTableObj(MassTableType mt, IdentDataObj idata)
            : base(mt, idata)
        {
            this._id = mt.id;
            this._name = mt.name;

            this._residues = null;
            this._ambiguousResidues = null;
            this._msLevels = null;

            if (mt.Residue != null && mt.Residue.Count > 0)
            {
                this.Residues = new IdentDataList<ResidueObj>();
                foreach (var r in mt.Residue)
                {
                    this.Residues.Add(new ResidueObj(r, this.IdentData));
                }
            }
            if (mt.AmbiguousResidue != null && mt.AmbiguousResidue.Count > 0)
            {
                this.AmbiguousResidues = new IdentDataList<AmbiguousResidueObj>();
                foreach (var ar in mt.AmbiguousResidue)
                {
                    this.AmbiguousResidues.Add(new AmbiguousResidueObj(ar, this.IdentData));
                }
            }
            if (mt.msLevel != null)
            {
                this._msLevels = new List<string>(mt.msLevel);
            }
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// <remarks>The specification of a single residue within the mass table.</remarks>
        /// min 0, max unbounded
        //public IdentDataList<Residue> Residue

        /// min 0, max unbounded
        //public IdentDataList<AmbiguousResidue> AmbiguousResidue

        /// <remarks>___ParamGroup___: Additional parameters or descriptors for the MassTable.</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams

        /// <remarks>The MS spectrum that the MassTable refers to e.g. "1" for MS1 "2" for MS2 or "1 2" for MS1 or MS2.</remarks>
        /// Required Attribute
        /// integer(s), space separated
        //public List<string> MsLevel
    }

    /// <summary>
    /// MzIdentML ResidueType
    /// </summary>
    public partial class ResidueObj : IdentDataInternalTypeAbstract
    {
        public ResidueObj(ResidueType r, IdentDataObj idata)
            : base(idata)
        {
            this._code = r.code;
            this._mass = r.mass;
        }

        /// <remarks>The single letter code for the residue.</remarks>
        /// Required Attribute
        /// chars, string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}"
        //public string Code

        /// <remarks>The residue mass in Daltons (not including any fixed modifications).</remarks>
        /// Required Attribute
        /// float
        //public float Mass
    }

    /// <summary>
    /// MzIdentML AmbiguousResidueType
    /// </summary>
    /// <remarks>Ambiguous residues e.g. X can be specified by the Code attribute and a set of parameters 
    /// for example giving the different masses that will be used in the search.</remarks>
    public partial class AmbiguousResidueObj : ParamGroupObj
    {
        public AmbiguousResidueObj(AmbiguousResidueType ar, IdentDataObj idata)
            : base(ar, idata)
        {
            this._code = ar.code;
        }

        /// <remarks>___ParamGroup___: Parameters for capturing e.g. "alternate single letter codes"</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams

        /// <remarks>The single letter code of the ambiguous residue e.g. X.</remarks>
        /// Required Attribute
        /// chars, string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}"
        //public string Code
    }

    /// <summary>
    /// MzIdentML EnzymeType
    /// </summary>
    /// <remarks>The details of an individual cleavage enzyme should be provided by giving a regular expression 
    /// or a CV term if a "standard" enzyme cleavage has been performed.</remarks>
    public partial class EnzymeObj : IdentDataInternalTypeAbstract, IIdentifiableType
    {
        public EnzymeObj(EnzymeType e, IdentDataObj idata)
            : base(idata)
        {
            this._id = e.id;
            this._name = e.name;
            this._siteRegexp = e.SiteRegexp;
            this._nTermGain = e.nTermGain;
            this._cTermGain = e.cTermGain;
            this._semiSpecific = e.semiSpecific;
            this.SemiSpecificSpecified = e.semiSpecificSpecified;
            this._missedCleavages = e.missedCleavages;
            this.MissedCleavagesSpecified = e.missedCleavagesSpecified;
            this._minDistance = e.minDistance;
            this.MinDistanceSpecified = e.minDistanceSpecified;

            this._enzymeName = null;

            if (e.EnzymeName != null)
            {
                this._enzymeName = new ParamListObj(e.EnzymeName, this.IdentData);
            }
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// <remarks>Regular expression for specifying the enzyme cleavage site.</remarks>
        /// min 0, max 1
        //public string SiteRegexp

        /// <remarks>The name of the enzyme from a CV.</remarks>
        /// min 0, max 1
        //public ParamList EnzymeName

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
        //public bool MinDistanceSpecified
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationProtocolType
    /// </summary>
    /// <remarks>The parameters and settings of a SpectrumIdentification analysis.</remarks>
    public partial class SpectrumIdentificationProtocolObj : IdentDataInternalTypeAbstract, IIdentifiableType
    {
        public SpectrumIdentificationProtocolObj(SpectrumIdentificationProtocolType sip, IdentDataObj idata)
            : base(idata)
        {
            this._id = sip.id;
            this._name = sip.name;
            this.AnalysisSoftwareRef = sip.analysisSoftware_ref;

            this._searchType = null;
            this._additionalSearchParams = null;
            this._modificationParams = null;
            this._enzymes = null;
            this._massTables = null;
            this._fragmentTolerances = null;
            this._parentTolerances = null;
            this._threshold = null;
            this._databaseFilters = null;
            this._databaseTranslation = null;

            if (sip.SearchType != null)
            {
                this._searchType = new ParamObj(sip.SearchType, this.IdentData);
            }
            if (sip.AdditionalSearchParams != null)
            {
                this._additionalSearchParams = new ParamListObj(sip.AdditionalSearchParams, this.IdentData);
            }
            if (sip.ModificationParams != null && sip.ModificationParams.Count > 0)
            {
                this.ModificationParams = new IdentDataList<SearchModificationObj>();
                foreach (var mp in sip.ModificationParams)
                {
                    this.ModificationParams.Add(new SearchModificationObj(mp, this.IdentData));
                }
            }
            if (sip.Enzymes != null)
            {
                this._enzymes = new EnzymeListObj(sip.Enzymes, this.IdentData);
            }
            if (sip.MassTable != null && sip.MassTable.Count > 0)
            {
                this.MassTables = new IdentDataList<MassTableObj>();
                foreach (var mt in sip.MassTable)
                {
                    this.MassTables.Add(new MassTableObj(mt, this.IdentData));
                }
            }
            if (sip.FragmentTolerance != null && sip.FragmentTolerance.Count > 0)
            {
                this.FragmentTolerances = new IdentDataList<CVParamObj>();
                foreach (var ft in sip.FragmentTolerance)
                {
                    this.FragmentTolerances.Add(new CVParamObj(ft, this.IdentData));
                }
            }
            if (sip.ParentTolerance != null)
            {
                this.ParentTolerances = new IdentDataList<CVParamObj>();
                foreach (var pt in sip.ParentTolerance)
                {
                    this.ParentTolerances.Add(new CVParamObj(pt, this.IdentData));
                }
            }
            if (sip.Threshold != null)
            {
                this._threshold = new ParamListObj(sip.Threshold, this.IdentData);
            }
            if (sip.DatabaseFilters != null && sip.DatabaseFilters.Count > 0)
            {
                this.DatabaseFilters = new IdentDataList<FilterInfo>();
                foreach (var df in sip.DatabaseFilters)
                {
                    this.DatabaseFilters.Add(new FilterInfo(df, this.IdentData));
                }
            }
            if (sip.DatabaseTranslation != null)
            {
                this._databaseTranslation = new DatabaseTranslationObj(sip.DatabaseTranslation, this.IdentData);
            }
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// <remarks>The type of search performed e.g. PMF, Tag searches, MS-MS</remarks>
        /// min 1, max 1
        //public Param SearchType

        /// <remarks>The search parameters other than the modifications searched.</remarks>
        /// min 0, max 1
        //public ParamList AdditionalSearchParams

        /// min 0, max 1 : Original ModificationParamsType
        //public IdentDataList<SearchModification> ModificationParams

        /// min 0, max 1
        //public EnzymeList Enzymes

        /// min 0, max unbounded
        //public IdentDataList<MassTable> MassTable

        /// min 0, max 1 : Original ToleranceType
        //public IdentDataList<CVParam> FragmentTolerance

        /// min 0, max 1 : Original ToleranceType
        //public IdentDataList<CVParam> ParentTolerance

        /// <remarks>The threshold(s) applied to determine that a result is significant. If multiple terms are used it is assumed that all conditions are satisfied by the passing results.</remarks>
        /// min 1, max 1
        //public ParamList Threshold

        /// min 0, max 1 : Original DatabaseFiltersType
        //public IdentDataList<FilterInfo> DatabaseFilters

        /// min 0, max 1
        //public DatabaseTranslation DatabaseTranslation

        /// <remarks>The search algorithm used, given as a reference to the SoftwareCollection section.</remarks>
        /// Required Attribute
        /// string
        //public string AnalysisSoftwareRef
    }

    /// <summary>
    /// MzIdentML SpecificityRulesType
    /// </summary>
    /// <remarks>The specificity rules of the searched modification including for example 
    /// the probability of a modification's presence or peptide or protein termini. Standard 
    /// fixed or variable status should be provided by the attribute fixedMod.</remarks>
    public partial class SpecificityRulesListObj : CVParamGroupObj
    {
        public SpecificityRulesListObj(SpecificityRulesType sr, IdentDataObj idata)
            : base(sr, idata)
        {
        }

        /// min 1, max unbounded
        //public IdentDataList<CVParamType> CVParams
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
        public SearchModificationObj(SearchModificationType sm, IdentDataObj idata)
            : base(sm, idata)
        {
            this._fixedMod = sm.fixedMod;
            this._massDelta = sm.massDelta;
            this._residues = sm.residues;

            this._specificityRules = null;

            if (sm.SpecificityRules != null && sm.SpecificityRules.Count > 0)
            {
                this.SpecificityRules = new IdentDataList<SpecificityRulesListObj>();
                foreach (var sr in sm.SpecificityRules)
                {
                    this.SpecificityRules.Add(new SpecificityRulesListObj(sr, this.IdentData));
                }
            }
        }

        /// min 0, max unbounded
        //public IdentDataList<SpecificityRulesList> SpecificityRules

        /// <remarks>The modification is uniquely identified by references to external CVs such as UNIMOD, see 
        /// specification document and mapping file for more details.</remarks>
        /// min 1, max unbounded
        //public IdentDataList<CVParamType> CVParams

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
        //public string Residues
    }

    /// <summary>
    /// MzIdentML EnzymesType
    /// </summary>
    /// <remarks>The list of enzymes used in experiment</remarks>
    public partial class EnzymeListObj : IdentDataInternalTypeAbstract
    {
        public EnzymeListObj(EnzymesType el, IdentDataObj idata)
            : base(idata)
        {
            this._independent = el.independent;
            this.IndependentSpecified = el.independentSpecified;

            this._enzymes = null;

            if (el.Enzyme != null && el.Enzyme.Count > 0)
            {
                this.Enzymes = new IdentDataList<EnzymeObj>();
                foreach (var e in el.Enzyme)
                {
                    this.Enzymes.Add(new EnzymeObj(e, this.IdentData));
                }
            }
        }

        /// min 1, max unbounded
        //public IdentDataList<Enzyme> Enzymes

        /// <remarks>If there are multiple enzymes specified, this attribute is set to true if cleavage with different enzymes is performed independently.</remarks>
        /// Optional Attribute
        /// boolean
        //public bool Independent

        /// Attribute Existence
        //public bool IndependentSpecified
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
        public FilterInfo(FilterType f, IdentDataObj idata)
            : base(idata)
        {
            this._filterType = null;
            this._include = null;
            this._exclude = null;

            if (f.FilterType1 != null)
            {
                this._filterType = new ParamObj(f.FilterType1, this.IdentData);
            }
            if (f.Include != null)
            {
                this._include = new ParamListObj(f.Include, this.IdentData);
            }
            if (f.Exclude != null)
            {
                this._exclude = new ParamListObj(f.Exclude, this.IdentData);
            }
        }

        /// <remarks>The type of filter e.g. database taxonomy filter, pi filter, mw filter</remarks>
        /// min 1, max 1
        //public Param FilterType

        /// <remarks>All sequences fulfilling the specifed criteria are included.</remarks>
        /// min 0, max 1
        //public ParamList Include

        /// <remarks>All sequences fulfilling the specifed criteria are excluded.</remarks>
        /// min 0, max 1
        //public ParamList Exclude
    }

    /// <summary>
    /// MzIdentML DatabaseTranslationType
    /// </summary>
    /// <remarks>A specification of how a nucleic acid sequence database was translated for searching.</remarks>
    public partial class DatabaseTranslationObj : IdentDataInternalTypeAbstract
    {
        public DatabaseTranslationObj(DatabaseTranslationType dt, IdentDataObj idata)
            : base(idata)
        {
            this._translationTables = null;
            this._frames = null;

            if (dt.TranslationTable != null && dt.TranslationTable.Count > 0)
            {
                this.TranslationTables = new IdentDataList<TranslationTableObj>();
                foreach (var t in dt.TranslationTable)
                {
                    this.TranslationTables.Add(new TranslationTableObj(t, this.IdentData));
                }
            }
            if (dt.frames != null)
            {
                this._frames = new List<int>(dt.frames);
            }
        }

        /// min 1, max unbounded
        //public IdentDataList<TranslationTable> TranslationTable

        /// <remarks>The frames in which the nucleic acid sequence has been translated as a space separated IdentDataList</remarks>
        /// Optional Attribute
        /// listOfAllowedFrames: space-separated string, valid values -3, -2, -1, 1, 2, 3
        //public List<int> Frames
    }

    /// <summary>
    /// MzIdentML ProtocolApplicationType
    /// </summary>
    /// <remarks>The use of a protocol with the requisite Parameters and ParameterValues. 
    /// ProtocolApplications can take Material or Data (or both) as input and produce Material or Data (or both) as output.</remarks>
    public abstract partial class ProtocolApplicationObj : IdentDataInternalTypeAbstract, IIdentifiableType
    {
        public ProtocolApplicationObj(ProtocolApplicationType pa, IdentDataObj idata)
            : base(idata)
        {
            this._id = pa.id;
            this._name = pa.name;
            this._activityDate = pa.activityDate;
            this.ActivityDateSpecified = pa.activityDateSpecified;
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// <remarks>When the protocol was applied.</remarks>
        /// Optional Attribute
        /// datetime
        //public System.DateTime ActivityDate

        /// Attribute Existence
        //public bool ActivityDateSpecified
    }

    /// <summary>
    /// MzIdentML ProteinDetectionType
    /// </summary>
    /// <remarks>An Analysis which assembles a set of peptides (e.g. from a spectra search analysis) to proteins.</remarks>
    public partial class ProteinDetectionObj : ProtocolApplicationObj
    {
        public ProteinDetectionObj(ProteinDetectionType pd, IdentDataObj idata)
            : base(pd, idata)
        {
            this.ProteinDetectionListRef = pd.proteinDetectionList_ref;
            this.ProteinDetectionProtocolRef = pd.proteinDetectionProtocol_ref;

            this._inputSpectrumIdentifications = null;

            if (pd.InputSpectrumIdentifications != null && pd.InputSpectrumIdentifications.Count > 0)
            {
                this.InputSpectrumIdentifications = new IdentDataList<InputSpectrumIdentificationsObj>();
                foreach (var isi in pd.InputSpectrumIdentifications)
                {
                    this.InputSpectrumIdentifications.Add(new InputSpectrumIdentificationsObj(isi, this.IdentData));
                }
            }
        }

        /// min 1, max unbounded
        //public IdentDataList<InputSpectrumIdentifications> InputSpectrumIdentifications

        /// <remarks>A reference to the ProteinDetectionList in the DataCollection section.</remarks>
        /// Required Attribute
        /// string
        //public string ProteinDetectionListRef

        /// <remarks>A reference to the detection protocol used for this ProteinDetection.</remarks>
        /// Required Attribute
        /// string
        //public string ProteinDetectionProtocolRef
    }

    /// <summary>
    /// MzIdentML InputSpectrumIdentificationsType
    /// </summary>
    /// <remarks>The lists of spectrum identifications that are input to the protein detection process.</remarks>
    public partial class InputSpectrumIdentificationsObj : IdentDataInternalTypeAbstract
    {
        public InputSpectrumIdentificationsObj(InputSpectrumIdentificationsType isi, IdentDataObj idata)
            : base(idata)
        {
            this.SpectrumIdentificationListRef = isi.spectrumIdentificationList_ref;
        }

        /// <remarks>A reference to the list of spectrum identifications that were input to the process.</remarks>
        /// Required Attribute
        /// string
        //public string SpectrumIdentificationListRef
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationType
    /// </summary>
    /// <remarks>An Analysis which tries to identify peptides in input spectra, referencing the database searched, 
    /// the input spectra, the output results and the protocol that is run.</remarks>
    public partial class SpectrumIdentificationObj : ProtocolApplicationObj
    {
        public SpectrumIdentificationObj(SpectrumIdentificationType si, IdentDataObj idata)
            : base(si, idata)
        {
            this.SpectrumIdentificationProtocolRef = si.spectrumIdentificationProtocol_ref;
            this.SpectrumIdentificationListRef = si.spectrumIdentificationList_ref;

            this._inputSpectra = null;
            this._searchDatabases = null;

            if (si.InputSpectra != null && si.InputSpectra.Count > 0)
            {
                this.InputSpectra = new IdentDataList<InputSpectraRefObj>();
                foreach (var ispec in si.InputSpectra)
                {
                    this.InputSpectra.Add(new InputSpectraRefObj(ispec, this.IdentData));
                }
            }
            if (si.SearchDatabaseRef != null && si.SearchDatabaseRef.Count > 0)
            {
                this.SearchDatabases = new IdentDataList<SearchDatabaseRefObj>();
                foreach (var sd in si.SearchDatabaseRef)
                {
                    this.SearchDatabases.Add(new SearchDatabaseRefObj(sd, this.IdentData));
                }
            }
        }

        /// <remarks>One of the spectra data sets used.</remarks>
        /// min 1, max unbounded
        //public IdentDataList<InputSpectraRef> InputSpectra

        /// min 1, max unbounded
        //public IdentDataList<SearchDatabaseRefInfo> SearchDatabaseRef

        /// <remarks>A reference to the search protocol used for this SpectrumIdentification.</remarks>
        /// Required Attribute
        /// string
        //public string SpectrumIdentificationProtocolRef

        /// <remarks>A reference to the SpectrumIdentificationList produced by this analysis in the DataCollection section.</remarks>
        /// Required Attribute
        /// string
        //public string SpectrumIdentificationListRef
    }

    /// <summary>
    /// MzIdentML InputSpectraType
    /// </summary>
    /// <remarks>The attribute referencing an identifier within the SpectraData section.</remarks>
    public partial class InputSpectraRefObj : IdentDataInternalTypeAbstract
    {
        public InputSpectraRefObj(InputSpectraType isr, IdentDataObj idata)
            : base(idata)
        {
            this.SpectraDataRef = isr.spectraData_ref;
        }

        /// <remarks>A reference to the SpectraData element which locates the input spectra to an external file.</remarks>
        /// Optional Attribute
        /// string
        //public string SpectraDataRef
    }

    /// <summary>
    /// MzIdentML SearchDatabaseRefType
    /// </summary>
    /// <remarks>One of the search databases used.</remarks>
    public partial class SearchDatabaseRefObj : IdentDataInternalTypeAbstract
    {
        public SearchDatabaseRefObj(SearchDatabaseRefType sdr, IdentDataObj idata)
            : base(idata)
        {
            this.SearchDatabaseRef = sdr.searchDatabase_ref;
        }

        /// <remarks>A reference to the database searched.</remarks>
        /// Optional Attribute
        /// string
        //public string SearchDatabaseRef
    }

    /// <summary>
    /// MzIdentML PeptideEvidenceType
    /// </summary>
    /// <remarks>PeptideEvidence links a specific Peptide element to a specific position in a DBSequence. 
    /// There must only be one PeptideEvidence item per Peptide-to-DBSequence-position.</remarks>
    public partial class PeptideEvidenceObj : ParamGroupObj, IIdentifiableType
    {
        public PeptideEvidenceObj(PeptideEvidenceType pe, IdentDataObj idata)
            : base(pe, idata)
        {
            this.DBSequenceRef = pe.dBSequence_ref;
            this.PeptideRef = pe.peptide_ref;
            this._start = pe.start;
            this.StartSpecified = pe.startSpecified;
            this._end = pe.end;
            this.EndSpecified = pe.endSpecified;
            this._pre = pe.pre;
            this._post = pe.post;
            this.TranslationTableRef = pe.translationTable_ref;
            this._frame = pe.frame;
            this.FrameSpecified = pe.frameSpecified;
            this._isDecoy = pe.isDecoy;
            this._id = pe.id;
            this._name = pe.name;
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        //public PeptideEvidence()

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
        //public string DBSequenceRef
    }

    /// <summary>
    /// MzIdentML PeptideType
    /// </summary>
    /// <remarks>One (poly)peptide (a sequence with modifications). The combination of Peptide sequence and modifications must be unique in the file.</remarks>
    public partial class PeptideObj : ParamGroupObj, IIdentifiableType
    {
        public PeptideObj(PeptideType p, IdentDataObj idata)
            : base(p, idata)
        {
            this._id = p.id;
            this._name = p.name;
            this._peptideSequence = p.PeptideSequence;

            this._modifications = null;
            this._substitutionModifications = null;
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// <remarks>The amino acid sequence of the (poly)peptide. If a substitution modification has been found, the original sequence should be reported.</remarks>
        /// min 1, max 1
        //public string PeptideSequence

        /// min 0, max unbounded
        //public IdentDataList<Modification> Modification

        /// min 0, max unbounded
        //public IdentDataList<SubstitutionModification> SubstitutionModification

        /// <remarks>___ParamGroup___: Additional descriptors of this peptide sequence</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams
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
        public ModificationObj(ModificationType m, IdentDataObj idata)
            : base(m, idata)
        {
            this._location = m.location;
            this.LocationSpecified = m.locationSpecified;
            this._avgMassDelta = m.avgMassDelta;
            this.AvgMassDeltaSpecified = m.avgMassDeltaSpecified;
            this._monoisotopicMassDelta = m.monoisotopicMassDelta;
            this.MonoisotopicMassDeltaSpecified = m.monoisotopicMassDeltaSpecified;

            this._residues = null;

            if (m.residues != null)
            {
                this._residues = new List<string>(m.residues);
            }
        }

        /// <remarks>CV terms capturing the modification, sourced from an appropriate controlled vocabulary.</remarks>
        /// min 1, max unbounded
        //public IdentDataList<CVParamType> CVParams

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
        //public bool MonoisotopicMassDeltaSpecified
    }

    /// <summary>
    /// MzIdentML SubstitutionModificationType
    /// </summary>
    /// <remarks>A modification where one residue is substituted by another (amino acid change).</remarks>
    public partial class SubstitutionModificationObj : IdentDataInternalTypeAbstract
    {
        public SubstitutionModificationObj(SubstitutionModificationType sm, IdentDataObj idata)
            : base(idata)
        {
            this._originalResidue = sm.originalResidue;
            this._replacementResidue = sm.replacementResidue;
            this._location = sm.location;
            this.LocationSpecified = sm.locationSpecified;
            this._avgMassDelta = sm.avgMassDelta;
            this.AvgMassDeltaSpecified = sm.avgMassDeltaSpecified;
            this._monoisotopicMassDelta = sm.monoisotopicMassDelta;
            this.MonoisotopicMassDeltaSpecified = sm.monoisotopicMassDeltaSpecified;
        }

        /// <remarks>The original residue before replacement.</remarks>
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
        //public bool MonoisotopicMassDeltaSpecified
    }

    /// <summary>
    /// MzIdentML DBSequenceType
    /// </summary>
    /// <remarks>A database sequence from the specified SearchDatabase (nucleic acid or amino acid). 
    /// If the sequence is nucleic acid, the source nucleic acid sequence should be given in 
    /// the seq attribute rather than a translated sequence.</remarks>
    public partial class DbSequenceObj : ParamGroupObj, IIdentifiableType
    {
        public DbSequenceObj(DBSequenceType dbs, IdentDataObj idata)
            : base(dbs, idata)
        {
            this._id = dbs.id;
            this._name = dbs.name;
            this._seq = dbs.Seq;
            this._length = dbs.length;
            this.LengthSpecified = dbs.lengthSpecified;
            this.SearchDatabaseRef = dbs.searchDatabase_ref;
            this._accession = dbs.accession;
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// <remarks>The actual sequence of amino acids or nucleic acid.</remarks>
        /// min 0, max 1
        /// string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]*"
        //public string Seq

        /// <remarks>___ParamGroup___: Additional descriptors for the sequence, such as taxon, description line etc.</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams

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
        //public bool LengthSpecified
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
        public SampleObj(SampleType s, IdentDataObj idata)
            : base(s, idata)
        {
            this._id = s.id;
            this._name = s.name;

            this._contactRoles = null;
            this._subSamples = null;

            if (s.ContactRole != null && s.ContactRole.Count > 0)
            {
                this.ContactRoles = new IdentDataList<ContactRoleObj>();
                foreach (var cr in s.ContactRole)
                {
                    this.ContactRoles.Add(new ContactRoleObj(cr, this.IdentData));
                }
            }
            if (s.SubSample != null && s.SubSample.Count > 0)
            {
                this.SubSamples = new IdentDataList<SubSampleObj>();
                foreach (var ss in s.SubSample)
                {
                    this.SubSamples.Add(new SubSampleObj(ss, this.IdentData));
                }
            }
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// <remarks>Contact details for the Material. The association to ContactRole could specify, for example, the creator or provider of the Material.</remarks>
        /// min 0, max unbounded
        //public IdentDataList<ContactRoleInfo> ContactRoles

        /// min 0, max unbounded
        //public IdentDataList<SubSample> SubSamples

        /// <remarks>___ParamGroup___: The characteristics of a Material.</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams
    }

    /// <summary>
    /// MzIdentML ContactRoleType
    /// </summary>
    /// <remarks>The role that a Contact plays in an organization or with respect to the associating class. 
    /// A Contact may have several Roles within scope, and as such, associations to ContactRole 
    /// allow the use of a Contact in a certain manner. Examples might include a provider, or a data analyst.</remarks>
    public partial class ContactRoleObj : IdentDataInternalTypeAbstract
    {
        public ContactRoleObj(ContactRoleType cr, IdentDataObj idata)
            : base(idata)
        {
            this.ContactRef = cr.contact_ref;

            this._role = null;

            if (cr.Role != null)
            {
                this._role = new RoleObj(cr.Role, this.IdentData);
            }
        }

        /// min 1, max 1
        //public RoleInfo Role

        /// <remarks>When a ContactRole is used, it specifies which Contact the role is associated with.</remarks>
        /// Required Attribute
        /// string
        //public string ContactRef
    }

    /// <summary>
    /// MzIdentML RoleType
    /// </summary>
    /// <remarks>The roles (lab equipment sales, contractor, etc.) the Contact fills.</remarks>
    public partial class RoleObj : IdentDataInternalTypeAbstract
    {
        public RoleObj(RoleType r, IdentDataObj idata)
            : base(idata)
        {
            this._cvParam = null;

            if (r.cvParam != null)
            {
                this._cvParam = new CVParamObj(r.cvParam, this.IdentData);
            }
        }

        /// <remarks>CV term for contact roles, such as software provider.</remarks>
        /// min 1, max 1
        //public CVParam CVParam
    }

    /// <summary>
    /// MzIdentML SubSampleType
    /// </summary>
    /// <remarks>References to the individual component samples within a mixed parent sample.</remarks>
    public partial class SubSampleObj : IdentDataInternalTypeAbstract
    {
        public SubSampleObj(SubSampleType ss, IdentDataObj idata)
            : base(idata)
        {
            this.SampleRef = ss.sample_ref;
        }

        /// <remarks>A reference to the child sample.</remarks>
        /// Required  Attribute
        /// string
        //public string SampleRef
    }

    /// <summary>
    /// MzIdentML AuditCollectionType; Also C# representation of AuditCollectionType
    /// </summary>
    /// <remarks>A contact is either a person or an organization.</remarks>
    /// <remarks>AuditCollectionType: The complete set of Contacts (people and organisations) for this file.</remarks>
    /// <remarks>AuditCollectionType: min 1, max unbounded, for PersonType XOR OrganizationType</remarks>
    public abstract partial class AbstractContactObj : ParamGroupObj, IIdentifiableType
    {
        public AbstractContactObj(AbstractContactType ac, IdentDataObj idata)
            : base(ac, idata)
        {
            this._id = ac.id;
            this._name = ac.name;
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// <remarks>___ParamGroup___: Attributes of this contact such as address, email, telephone etc.</remarks>
        /// min 0, max unbounded
        //public IdentDataList<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public IdentDataList<UserParamType> UserParams
    }

    /// <summary>
    /// MzIdentML OrganizationType
    /// </summary>
    /// <remarks>Organizations are entities like companies, universities, government agencies. 
    /// Any additional information such as the address, email etc. should be supplied either as CV parameters or as user parameters.</remarks>
    public partial class OrganizationObj : AbstractContactObj
    {
        public OrganizationObj(OrganizationType o, IdentDataObj idata)
            : base(o, idata)
        {
            this._parent = null;

            if (o.Parent != null)
            {
                this._parent = new ParentOrganizationObj(o.Parent, this.IdentData);
            }
        }

        /// min 0, max 1
        //public ParentOrganization Parent
    }

    /// <summary>
    /// Base class for identical ParentOrganization and AffiliationInfo
    /// </summary>
    public partial class OrganizationRefObj : IdentDataInternalTypeAbstract
    {
        public OrganizationRefObj(IdentDataObj idata)
            : base(idata)
        { }

        /// <remarks>A reference to the organization this contact belongs to.</remarks>
        /// Required Attribute
        /// string
        //public string OrganizationRef
    }

    /// <summary>
    /// MzIdentML ParentOrganizationType
    /// </summary>
    /// <remarks>The containing organization (the university or business which a lab belongs to, etc.)</remarks>
    public partial class ParentOrganizationObj : OrganizationRefObj
    {
        public ParentOrganizationObj(ParentOrganizationType po, IdentDataObj idata)
            : base(idata)
        {
            this.OrganizationRef = po.organization_ref;
        }

        /// <remarks>A reference to the organization this contact belongs to.</remarks>
        /// Required Attribute
        /// string
        //public string OrganizationRef
    }

    /// <summary>
    /// MzIdentML PersonType
    /// </summary>
    /// <remarks>A person's name and contact details. Any additional information such as the address, 
    /// contact email etc. should be supplied using CV parameters or user parameters.</remarks>
    public partial class PersonObj : AbstractContactObj
    {
        public PersonObj(PersonType p, IdentDataObj idata)
            : base(p, idata)
        {
            this._lastName = p.lastName;
            this._firstName = p.firstName;
            this._midInitials = p.midInitials;

            this._affiliations = null;

            if (p.Affiliation != null && p.Affiliation.Count > 0)
            {
                this.Affiliations = new IdentDataList<AffiliationObj>();
                foreach (var a in p.Affiliation)
                {
                    this.Affiliations.Add(new AffiliationObj(a, this.IdentData));
                }
            }
        }

        /// <remarks>The organization a person belongs to.</remarks>
        /// min 0, max unbounded
        //public IdentDataList<AffiliationInfo> Affiliation

        /// <remarks>The Person's last/family name.</remarks>
        /// Optional Attribute
        //public string LastName

        /// <remarks>The Person's first name.</remarks>
        /// Optional Attribute
        //public string FirstName

        /// <remarks>The Person's middle initial.</remarks>
        /// Optional Attribute
        //public string MidInitials
    }

    /// <summary>
    /// MzIdentML AffiliationType
    /// </summary>
    public partial class AffiliationObj : OrganizationRefObj
    {
        public AffiliationObj(AffiliationType a, IdentDataObj idata)
            : base(idata)
        {
            this.OrganizationRef = a.organization_ref;
        }

        /// <remarks>>A reference to the organization this contact belongs to.</remarks>
        /// Required Attribute
        /// string
        //public string OrganizationRef
    }

    /// <summary>
    /// MzIdentML ProviderType
    /// </summary>
    /// <remarks>The provider of the document in terms of the Contact and the software the produced the document instance.</remarks>
    public partial class ProviderObj : IdentDataInternalTypeAbstract, IIdentifiableType
    {
        public ProviderObj(ProviderType p, IdentDataObj idata)
            : base(idata)
        {
            this._id = p.id;
            this._name = p.name;
            this.AnalysisSoftwareRef = p.analysisSoftware_ref;

            idata.Provider = this;

            this._contactRole = null;

            if (p.ContactRole != null)
            {
                this._contactRole = new ContactRoleObj(p.ContactRole, this.IdentData);
            }
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// <remarks>The Contact that provided the document instance.</remarks>
        /// min 0, max 1
        //public ContactRoleInfo ContactRole

        /// <remarks>The Software that produced the document instance.</remarks>
        /// Optional Attribute
        /// string
        //public string AnalysisSoftwareRef
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
        public AnalysisSoftwareObj(AnalysisSoftwareType asi, IdentDataObj idata)
            : base(idata)
        {
            this._id = asi.id;
            this._name = asi.name;
            this._customizations = asi.Customizations;
            this._version = asi.version;
            this._uri = asi.uri;

            this._contactRole = null;
            this._softwareName = null;

            if (asi.ContactRole != null)
            {
                this._contactRole = new ContactRoleObj(asi.ContactRole, this.IdentData);
            }
            if (asi.SoftwareName != null)
            {
                this._softwareName = new ParamObj(asi.SoftwareName, this.IdentData);
            }
        }

        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        //public string Id

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        //public string Name

        /// <remarks>The contact details of the organisation or person that produced the software</remarks>
        /// min 0, max 1
        //public ContactRoleInfo ContactRole

        /// <remarks>The name of the analysis software package, sourced from a CV if available.</remarks>
        /// min 1, max 1
        //public Param SoftwareName

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
        //public string URI
    }

    /// <summary>
    /// MzIdentML InputsType
    /// </summary>
    /// <remarks>The inputs to the analyses including the databases searched, the spectral data and the source file converted to mzIdentML.</remarks>
    public partial class InputsObj : IdentDataInternalTypeAbstract
    {
        public InputsObj(InputsType i, IdentDataObj idata)
            : base(idata)
        {
            this._sourceFiles = null;
            this._searchDatabases = null;
            this._spectraDataList = null;

            if (i.SourceFile != null && i.SourceFile.Count > 0)
            {
                this.SourceFiles = new IdentDataList<SourceFileInfo>();
                foreach (var sf in i.SourceFile)
                {
                    this.SourceFiles.Add(new SourceFileInfo(sf, this.IdentData));
                }
            }
            if (i.SearchDatabase != null && i.SearchDatabase.Count > 0)
            {
                this.SearchDatabases = new IdentDataList<SearchDatabaseInfo>();
                foreach (var sd in i.SearchDatabase)
                {
                    this.SearchDatabases.Add(new SearchDatabaseInfo(sd, this.IdentData));
                }
            }
            if (i.SpectraData != null && i.SpectraData.Count > 0)
            {
                this.SpectraDataList = new IdentDataList<SpectraDataObj>();
                foreach (var sd in i.SpectraData)
                {
                    this.SpectraDataList.Add(new SpectraDataObj(sd, this.IdentData));
                }
            }
        }

        /// min 0, max unbounded
        //public IdentDataList<SourceFileInfo> SourceFile

        /// min 0, max unbounded
        //public IdentDataList<SearchDatabaseInfo> SearchDatabase

        /// min 1, max unbounde
        //public IdentDataList<SpectraData> SpectraData
    }

    /// <summary>
    /// MzIdentML DataCollectionType
    /// </summary>
    /// <remarks>The collection of input and output data sets of the analyses.</remarks>
    public partial class DataCollectionObj : IdentDataInternalTypeAbstract
    {
        public DataCollectionObj(DataCollectionType dc, IdentDataObj idata)
            : base(idata)
        {
            this._inputs = null;
            this._analysisData = null;

            idata.DataCollection = this;

            if (dc.Inputs != null)
            {
                this._inputs = new InputsObj(dc.Inputs, this.IdentData);
            }
            if (dc.AnalysisData != null)
            {
                this._analysisData = new AnalysisDataObj(dc.AnalysisData, this.IdentData);
            }
        }

        /// min 1, max 1
        //public InputsInfo Inputs

        /// min 1, max 1
        //public AnalysisData AnalysisData
    }

    /// <summary>
    /// MzIdentML AnalysisProtocolCollectionType
    /// </summary>
    /// <remarks>The collection of protocols which include the parameters and settings of the performed analyses.</remarks>
    public partial class AnalysisProtocolCollectionObj : IdentDataInternalTypeAbstract
    {
        public AnalysisProtocolCollectionObj(AnalysisProtocolCollectionType apc, IdentDataObj idata)
            : base(idata)
        {
            this._spectrumIdentificationProtocols = null;
            this._proteinDetectionProtocol = null;

            idata.AnalysisProtocolCollection = this;

            if (apc.SpectrumIdentificationProtocol != null && apc.SpectrumIdentificationProtocol.Count > 0)
            {
                this.SpectrumIdentificationProtocols = new IdentDataList<SpectrumIdentificationProtocolObj>();
                foreach (var sip in apc.SpectrumIdentificationProtocol)
                {
                    this.SpectrumIdentificationProtocols.Add(new SpectrumIdentificationProtocolObj(sip, this.IdentData));
                }
            }
            if (apc.ProteinDetectionProtocol != null)
            {
                this._proteinDetectionProtocol = new ProteinDetectionProtocolObj(apc.ProteinDetectionProtocol, this.IdentData);
            }
        }

        /// min 1, max unbounded
        //public IdentDataList<SpectrumIdentificationProtocol> SpectrumIdentificationProtocol

        /// min 0, max 1
        //public ProteinDetectionProtocol ProteinDetectionProtocol
    }

    /// <summary>
    /// MzIdentML AnalysisCollectionType
    /// </summary>
    /// <remarks>The analyses performed to get the results, which map the input and output data sets. 
    /// Analyses are for example: SpectrumIdentification (resulting in peptides) or ProteinDetection (assemble proteins from peptides).</remarks>
    public partial class AnalysisCollectionObj : IdentDataInternalTypeAbstract
    {
        public AnalysisCollectionObj(AnalysisCollectionType ac, IdentDataObj idata)
            : base(idata)
        {
            this._spectrumIdentifications = null;
            this._proteinDetection = null;

            idata.AnalysisCollection = this;

            if (ac.SpectrumIdentification != null && ac.SpectrumIdentification.Count > 0)
            {
                this.SpectrumIdentifications = new IdentDataList<SpectrumIdentificationObj>();
                foreach (var si in ac.SpectrumIdentification)
                {
                    this.SpectrumIdentifications.Add(new SpectrumIdentificationObj(si, this.IdentData));
                }
            }
            if (ac.ProteinDetection != null)
            {
                this.ProteinDetection = new ProteinDetectionObj(ac.ProteinDetection, this.IdentData);
            }
        }

        /// min 1, max unbounded
        //public IdentDataList<SpectrumIdentification> SpectrumIdentification

        /// min 0, max 1
        //public ProteinDetection ProteinDetection
    }

    /// <summary>
    /// MzIdentML SequenceCollectionType
    /// </summary>
    /// <remarks>The collection of sequences (DBSequence or Peptide) identified and their relationship between 
    /// each other (PeptideEvidence) to be referenced elsewhere in the results.</remarks>
    public partial class SequenceCollectionObj : IdentDataInternalTypeAbstract
    {
        public SequenceCollectionObj(SequenceCollectionType sc, IdentDataObj idata)
            : base(idata)
        {
            this._dBSequences = null;
            this._peptides = null;
            this._peptideEvidences = null;

            idata.SequenceCollection = this;

            if (sc.DBSequence != null && sc.DBSequence.Count > 0)
            {
                this.DBSequences = new IdentDataList<DbSequenceObj>();
                foreach (var dbs in sc.DBSequence)
                {
                    this.DBSequences.Add(new DbSequenceObj(dbs, this.IdentData));
                }
            }
            if (sc.Peptide != null && sc.Peptide.Count > 0)
            {
                this.Peptides = new IdentDataList<PeptideObj>();
                foreach (var p in sc.Peptide)
                {
                    this.Peptides.Add(new PeptideObj(p, this.IdentData));
                }
            }
            if (sc.PeptideEvidence != null && sc.PeptideEvidence.Count > 0)
            {
                this.PeptideEvidences = new IdentDataList<PeptideEvidenceObj>();
                foreach (var pe in sc.PeptideEvidence)
                {
                    this.PeptideEvidences.Add(new PeptideEvidenceObj(pe, this.IdentData));
                }
            }
        }

        /// min 1, max unbounded
        //public IdentDataList<DBSequence> DBSequences

        /// min 0, max unbounded
        //public IdentDataList<Peptide> Peptides

        /// min 0, max unbounded
        //public IdentDataList<PeptideEvidence> PeptideEvidences
    }
}
