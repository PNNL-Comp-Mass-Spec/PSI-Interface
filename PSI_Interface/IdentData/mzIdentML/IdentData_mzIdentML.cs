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
        public MzIdentMLType(IdentDataObj identData) : base(identData)
        {
            this.creationDate = System.DateTime.Now;
            this.creationDateSpecified = false;
            if (identData.CreationDateSpecified)
            {
                this.creationDate = identData.CreationDate;
                this.creationDateSpecified = true;
            }
            this.version = identData.Version;

            // Default values
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

            if (identData.CVList != null && identData.CVList.Count > 0)
            {
                this.cvList = new List<cvType>();
                foreach (var cv in identData.CVList)
                {
                    this.cvList.Add(new cvType(cv));
                }
            }
            if (identData.AnalysisSoftwareList != null && identData.AnalysisSoftwareList.Count > 0)
            {
                this.AnalysisSoftwareList = new List<AnalysisSoftwareType>();
                foreach (var asl in identData.AnalysisSoftwareList)
                {
                    this.AnalysisSoftwareList.Add(new AnalysisSoftwareType(asl));
                }
            }
            if (identData.Provider != null)
            {
                this.Provider = new ProviderType(identData.Provider);
            }
            if (identData.AuditCollection != null && identData.AuditCollection.Count > 0)
            {
                this.AuditCollection = new List<AbstractContactType>();
                foreach (var a in identData.AuditCollection)
                {
                    if (a is OrganizationObj)
                    {
                        this.AuditCollection.Add(new OrganizationType(a as OrganizationObj));
                    }
                    else if (a is PersonObj)
                    {
                        this.AuditCollection.Add(new PersonType(a as PersonObj));
                    }
                }
            }
            if (identData.AnalysisSampleCollection != null && identData.AnalysisSampleCollection.Count > 0)
            {
                this.AnalysisSampleCollection = new List<SampleType>();
                foreach (var s in identData.AnalysisSampleCollection)
                {
                    this.AnalysisSampleCollection.Add(new SampleType(s));
                }
            }
            if (identData.SequenceCollection != null)
            {
                this.SequenceCollection = new SequenceCollectionType(identData.SequenceCollection);
            }
            if (identData.AnalysisCollection != null)
            {
                this.AnalysisCollection = new AnalysisCollectionType(identData.AnalysisCollection);
            }
            if (identData.AnalysisProtocolCollection != null)
            {
                this.AnalysisProtocolCollection = new AnalysisProtocolCollectionType(identData.AnalysisProtocolCollection);
            }
            if (identData.DataCollection != null)
            {
                this.DataCollection = new DataCollectionType(identData.DataCollection);
            }
            if (identData.BibliographicReferences != null && identData.BibliographicReferences.Count > 0)
            {
                this.BibliographicReference = new List<BibliographicReferenceType>();
                foreach (var br in identData.BibliographicReferences)
                {
                    this.BibliographicReference.Add(new BibliographicReferenceType(br));
                }
            }
        }

        /// min 1, max 1
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
        //public string Version
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
        public cvType(CVInfo cvi)
        {
            this.fullName = cvi.FullName;
            this.version = cvi.Version;
            this.uri = cvi.URI;
            this.id = cvi.Id;
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
    public partial class SpectrumIdentificationItemRefType
    {
        public SpectrumIdentificationItemRefType(SpectrumIdentificationItemRefObj siiri)
        {
            this.spectrumIdentificationItem_ref = siiri.SpectrumIdentificationItemRef;
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
    public partial class PeptideHypothesisType
    {
        public PeptideHypothesisType(PeptideHypothesisObj ph)
        {
            this.peptideEvidence_ref = ph.PeptideEvidenceRef;

            this.SpectrumIdentificationItemRef = null;
            if (ph.SpectrumIdentificationItems != null && ph.SpectrumIdentificationItems.Count > 0)
            {
                this.SpectrumIdentificationItemRef = new List<SpectrumIdentificationItemRefType>();
                foreach (var siir in ph.SpectrumIdentificationItems)
                {
                    this.SpectrumIdentificationItemRef.Add(new SpectrumIdentificationItemRefType(siir));
                }
            }
        }

        /// min 1, max unbounded
        //public List<SpectrumIdentificationItemRefType> SpectrumIdentificationItemRef

        /// <remarks>A reference to the PeptideEvidence element on which this hypothesis is based.</remarks>
        /// Required Attribute
        /// string
        //public string PeptideEvidenceRef
    }

    /// <summary>
    /// MzIdentML FragmentArrayType
    /// </summary>
    /// <remarks>An array of values for a given type of measure and for a particular ion type, in parallel to the index of ions identified.</remarks>
    public partial class FragmentArrayType
    {
        public FragmentArrayType(FragmentArrayObj fa)
        {
            this.measure_ref = fa.MeasureRef;

            this.values = null;
            if (fa.Values != null && fa.Values.Count > 0)
            {
                this.values = new List<float>(fa.Values);
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
    public partial class IonTypeType
    {
        public IonTypeType(IonTypeObj iti)
        {
            this.charge = iti.Charge;

            this.cvParam = null;
            this.FragmentArray = null;
            this.index = null;

            if (iti.CVParam != null)
            {
                this.cvParam = new CVParamType(iti.CVParam);
            }
            if (iti.FragmentArrays != null && iti.FragmentArrays.Count > 0)
            {
                this.FragmentArray = new List<FragmentArrayType>();
                foreach (var fa in iti.FragmentArrays)
                {
                    this.FragmentArray.Add(new FragmentArrayType(fa));
                }
            }
            if (iti.Index != null && iti.Index.Count > 0)
            {
                this.index = new List<string>(iti.Index);
            }
        }

        /// min 0, max unbounded
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
        //public int Charge
    }

    /// <summary>
    /// MzIdentML CVParamType; Container types: ToleranceType
    /// </summary>
    /// <remarks>A single entry from an ontology or a controlled vocabulary.</remarks>
    /// <remarks>ToleranceType: The tolerance of the search given as a plus and minus value with units.</remarks>
    /// <remarks>ToleranceType: child element cvParam of type CVParamType, min 1, max unbounded "CV terms capturing the tolerance plus and minus values."</remarks>
    public partial class CVParamType/* : AbstractParamType*/
    {
        //public CVParamType(CVParam cvp) : base(cvp)
        public CVParamType(CVParamObj cvp)
        {
            this.cvRef = cvp.CVRef;
            this.accession = cvp.Accession;
            this.name = cvp.Name;
            this.value = cvp.Value;
            this.unitCvRef = cvp.UnitCvRef;
            this.unitAccession = cvp.UnitAccession;
            this.unitName = cvp.UnitName;
        }

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

    /*/// <summary>
    /// MzIdentML AbstractParamType; Used instead of: ParamType, ParamGroup, ParamListType
    /// </summary>
    /// <remarks>Abstract entity allowing either cvParam or userParam to be referenced in other schemas.</remarks>
    /// <remarks>PramGroup: A choice of either a cvParam or userParam.</remarks>
    /// <remarks>ParamType: Helper type to allow either a cvParam or a userParam to be provided for an element.</remarks>
    /// <remarks>ParamListType: Helper type to allow multiple cvParams or userParams to be given for an element.</remarks>
    public abstract partial class AbstractParamType
    {
        public AbstractParamType(ParamBase pb)
        {
            this.name = pb.Name;
            this.value = pb.Value;
            this.unitAccession = pb.UnitAccession;
            this.unitName = pb.UnitName;
            this.unitCvRef = pb.UnitCvRef;
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
        //public UserParamType(UserParam up) : base(up)
        public UserParamType(UserParamObj up)
        {
            this.name = up.Name;
            this.value = up.Value;
            this.unitCvRef = up.UnitCvRef;
            this.unitAccession = up.UnitAccession;
            this.unitName = up.UnitName;

            this.type = null;
            if (up.Type != null)
            {
                this.type = up.Type;
            }
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

    /// <summary>
    /// MzIdentML ParamType
    /// </summary>
    /// <remarks>Helper type to allow either a cvParam or a userParam to be provided for an element.</remarks>
    public partial class ParamType
    {
        public ParamType(ParamObj p)
        {
            this.Item = null;

            if (p.Item != null && p.Item is CVParamObj)
            {
                this.Item = new CVParamType(p.Item as CVParamObj);
            }
            else if (p.Item != null && p.Item is UserParamObj)
            {
                this.Item = new UserParamType(p.Item as UserParamObj);
            }
        }

        /// min 1, max 1
        //public ParamBase Item
    }

    /// <summary>
    /// MzIdentML ParamListType
    /// </summary>
    /// <remarks>Helper type to allow multiple cvParams or userParams to be given for an element.</remarks>
    public partial class ParamListType
    {
        public ParamListType(ParamListObj pl)
        {
            this.Items = null;

            if (pl.Items != null && pl.Items.Count > 0)
            {
                this.Items = new List<AbstractParamType>();
                foreach (var p in pl.Items)
                {
                    if (p != null && p is CVParamObj)
                    {
                        this.Items.Add(new CVParamType(p as CVParamObj));
                    }
                    else if (p != null && p is UserParamObj)
                    {
                        this.Items.Add(new UserParamType(p as UserParamObj));
                    }
                }
            }
        }

        /// min 1, max unbounded
        //public List<ParamBase> Items
    }

    /// <summary>
    /// MzIdentML PeptideEvidenceRefType
    /// </summary>
    /// <remarks>Reference to the PeptideEvidence element identified. If a specific sequence can be assigned to multiple 
    /// proteins and or positions in a protein all possible PeptideEvidence elements should be referenced here.</remarks>
    public partial class PeptideEvidenceRefType
    {
        public PeptideEvidenceRefType(PeptideEvidenceRefObj peri)
        {
            this.peptideEvidence_ref = peri.PeptideEvidenceRef;
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
    public partial class AnalysisDataType
    {
        public AnalysisDataType(AnalysisDataObj ad)
        {
            // Default values
            this.SpectrumIdentificationList = null;
            this.ProteinDetectionList = null;

            if (ad.SpectrumIdentificationList != null && ad.SpectrumIdentificationList.Count > 0)
            {
                this.SpectrumIdentificationList = new List<SpectrumIdentificationListType>();
                foreach (var sil in ad.SpectrumIdentificationList)
                {
                    this.SpectrumIdentificationList.Add(new SpectrumIdentificationListType(sil));
                }
            }
            if (ad.ProteinDetectionList != null)
            {
                this.ProteinDetectionList = new ProteinDetectionListType(ad.ProteinDetectionList);
            }
        }

        /// min 1, max unbounded
        //public List<SpectrumIdentificationListType> SpectrumIdentificationList

        /// min 0, max 1
        //public ProteinDetectionListType ProteinDetectionList
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationListType
    /// </summary>
    /// <remarks>Represents the set of all search results from SpectrumIdentification.</remarks>
    public partial class SpectrumIdentificationListType : IdentifiableType, IParamGroup
    {
        public SpectrumIdentificationListType(SpectrumIdentificationListObj sil) : base(sil)
        {
            this.numSequencesSearched = sil.NumSequencesSearched;
            this.numSequencesSearchedSpecified = sil.NumSequencesSearchedSpecified;

            this.cvParam = null;
            this.userParam = null;
            ParamGroupFunctions.CopyParamGroup(this, sil);

            this.FragmentationTable = null;
            this.SpectrumIdentificationResult = null;
            if (sil.FragmentationTables != null && sil.FragmentationTables.Count > 0)
            {
                this.FragmentationTable = new List<MeasureType>();
                {
                    foreach (var f in sil.FragmentationTables)
                    {
                        this.FragmentationTable.Add(new MeasureType(f));
                    }
                }
            }
            if (sil.SpectrumIdentificationResults != null && sil.SpectrumIdentificationResults.Count > 0)
            {
                this.SpectrumIdentificationResult = new List<SpectrumIdentificationResultType>();
                foreach (var sir in sil.SpectrumIdentificationResults)
                {
                    this.SpectrumIdentificationResult.Add(new SpectrumIdentificationResultType(sir));
                }
            }
        }

        /// min 0, max 1
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
        //public bool NumSequencesSearchedSpecified
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
        public MeasureType(MeasureObj m) : base(m)
        {
            this.cvParam = null;
            ParamGroupFunctions.CopyCVParamGroup(this, m);
        }

        /// min 1, max unbounded
        //public List<CVParamType> CVParams
    }

    /// <summary>
    /// MzIdentML IdentifiableType
    /// </summary>
    /// <remarks>Other classes in the model can be specified as sub-classes, inheriting from Identifiable. 
    /// Identifiable gives classes a unique identifier within the scope and a name that need not be unique.</remarks>
    public abstract partial class IdentifiableType
    {
        public IdentifiableType(IIdentifiableType idId)
        {
            this.id = idId.Id;
            this.name = idId.Name;
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
    }

    /// <summary>
    /// MzIdentML BibliographicReferenceType
    /// </summary>
    /// <remarks>Represents bibliographic references.</remarks>
    public partial class BibliographicReferenceType : IdentifiableType
    {
        public BibliographicReferenceType(BibliographicReferenceObj br) : base(br)
        {
            this.authors = br.Authors;
            this.publication = br.Publication;
            this.publisher = br.Publisher;
            this.editor = br.Editor;
            this.year = br.Year;
            this.yearSpecified = br.YearSpecified; // No special test needed
            this.volume = br.Volume;
            this.issue = br.Issue;
            this.pages = br.Pages;
            this.title = br.Title;
            this.doi = br.DOI;
        }

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
    public partial class ProteinDetectionHypothesisType : IdentifiableType, IParamGroup
    {
        public ProteinDetectionHypothesisType(ProteinDetectionHypothesisObj pdh) : base(pdh)
        {
            this.dBSequence_ref = pdh.DBSequenceRef;
            this.passThreshold = pdh.PassThreshold;
            this.cvParam = null;
            this.userParam = null;
            ParamGroupFunctions.CopyParamGroup(this, pdh);

            // Default value
            this.PeptideHypothesis = null;

            if (pdh.PeptideHypotheses != null && pdh.PeptideHypotheses.Count > 0)
            {
                this.PeptideHypothesis = new List<PeptideHypothesisType>();
                foreach (var ph in pdh.PeptideHypotheses)
                {
                    this.PeptideHypothesis.Add(new PeptideHypothesisType(ph));
                }
            }
        }

        /// min 1, max unbounded
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
        //public bool PassThreshold
    }

    /// <summary>
    /// MzIdentML ProteinAmbiguityGroupType
    /// </summary>
    /// <remarks>A set of logically related results from a protein detection, for example to represent conflicting assignments of peptides to proteins.</remarks>
    public partial class ProteinAmbiguityGroupType : IdentifiableType, IParamGroup
    {
        public ProteinAmbiguityGroupType(ProteinAmbiguityGroupObj pag) : base(pag)
        {
            this.cvParam = null;
            this.userParam = null;
            ParamGroupFunctions.CopyParamGroup(this, pag);

            this.ProteinDetectionHypothesis = null;
            if (pag.ProteinDetectionHypotheses != null && pag.ProteinDetectionHypotheses.Count > 0)
            {
                this.ProteinDetectionHypothesis = new List<ProteinDetectionHypothesisType>();
                foreach (var pdh in pag.ProteinDetectionHypotheses)
                {
                    this.ProteinDetectionHypothesis.Add(new ProteinDetectionHypothesisType(pdh));
                }
            }
        }

        /// min 1, max unbounded
        //public List<ProteinDetectionHypothesisType> ProteinDetectionHypothesis

        /// <remarks>___ParamGroup___:Scores or parameters associated with the ProteinAmbiguityGroup.</remarks>
        /// min 0, max unbounded
        //public List<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public List<UserParamType> UserParams
    }

    /// <summary>
    /// MzIdentML ProteinDetectionListType
    /// </summary>
    /// <remarks>The protein list resulting from a protein detection process.</remarks>
    public partial class ProteinDetectionListType : IdentifiableType, IParamGroup
    {
        public ProteinDetectionListType(ProteinDetectionListObj pdl) : base(pdl)
        {
            this.cvParam = null;
            this.userParam = null;
            ParamGroupFunctions.CopyParamGroup(this, pdl);

            this.ProteinAmbiguityGroup = null;
            if (pdl.ProteinAmbiguityGroups != null && pdl.ProteinAmbiguityGroups.Count > 0)
            {
                this.ProteinAmbiguityGroup = new List<ProteinAmbiguityGroupType>();
                foreach (var pag in pdl.ProteinAmbiguityGroups)
                {
                    this.ProteinAmbiguityGroup.Add(new ProteinAmbiguityGroupType(pag));
                }
            }
        }

        /// min 0, max unbounded
        //public List<ProteinAmbiguityGroupType> ProteinAmbiguityGroup

        /// <remarks>___ParamGroup___:Scores or output parameters associated with the whole ProteinDetectionList</remarks>
        /// min 0, max unbounded
        //public List<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public List<UserParamType> UserParams
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationItemType
    /// </summary>
    /// <remarks>An identification of a single (poly)peptide, resulting from querying an input spectra, along with 
    /// the set of confidence values for that identification. PeptideEvidence elements should be given for all 
    /// mappings of the corresponding Peptide sequence within protein sequences.</remarks>
    public partial class SpectrumIdentificationItemType : IdentifiableType, IParamGroup
    {
        public SpectrumIdentificationItemType(SpectrumIdentificationItemObj sii) : base(sii)
        {
            this.chargeState = sii.ChargeState;
            this.experimentalMassToCharge = sii.ExperimentalMassToCharge;
            this.calculatedMassToCharge = sii.CalculatedMassToCharge;
            this.calculatedMassToChargeSpecified = sii.CalculatedMassToChargeSpecified;
            this.calculatedPI = sii.CalculatedPI;
            this.calculatedPISpecified = sii.CalculatedPISpecified;
            this.peptide_ref = sii.PeptideRef;
            this.rank = sii.Rank;
            this.passThreshold = sii.PassThreshold;
            this.massTable_ref = sii.MassTableRef;
            this.sample_ref = sii.SampleRef;

            this.cvParam = null;
            this.userParam = null;
            ParamGroupFunctions.CopyParamGroup(this, sii);

            // Default values
            this.PeptideEvidenceRef = null;
            this.Fragmentation = null;
            if (sii.PeptideEvidences != null && sii.PeptideEvidences.Count > 0)
            {
                this.PeptideEvidenceRef = new List<PeptideEvidenceRefType>();
                foreach (var per in sii.PeptideEvidences)
                {
                    this.PeptideEvidenceRef.Add(new PeptideEvidenceRefType(per));
                }
            }
            if (sii.Fragmentations != null && sii.Fragmentations.Count > 0)
            {
                this.Fragmentation = new List<IonTypeType>();
                foreach (var f in sii.Fragmentations)
                {
                    this.Fragmentation.Add(new IonTypeType(f));
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
        //public string SampleRef
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationResultType
    /// </summary>
    /// <remarks>All identifications made from searching one spectrum. For PMF data, all peptide identifications 
    /// will be listed underneath as SpectrumIdentificationItems. For MS/MS data, there will be ranked 
    /// SpectrumIdentificationItems corresponding to possible different peptide IDs.</remarks>
    public partial class SpectrumIdentificationResultType : IdentifiableType, IParamGroup
    {
        public SpectrumIdentificationResultType(SpectrumIdentificationResultObj sir) : base(sir)
        {
            this.spectrumID = sir.SpectrumID;
            this.spectraData_ref = sir.SpectraDataRef;

            this.cvParam = null;
            this.userParam = null;
            ParamGroupFunctions.CopyParamGroup(this, sir);

            this.SpectrumIdentificationItem = null;
            if (sir.SpectrumIdentificationItems != null && sir.SpectrumIdentificationItems.Count > 0)
            {
                this.SpectrumIdentificationItem = new List<SpectrumIdentificationItemType>();
                foreach (var sii in sir.SpectrumIdentificationItems)
                {
                    this.SpectrumIdentificationItem.Add(new SpectrumIdentificationItemType(sii));
                }
            }
        }

        /// min 1, max unbounded
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
        //public string SpectraDataRef
    }

    /// <summary>
    /// MzIdentML ExternalDataType
    /// </summary>
    /// <remarks>Data external to the XML instance document. The location of the data file is given in the location attribute.</remarks>
    public partial class ExternalDataType : IdentifiableType
    {
        public ExternalDataType(IExternalDataType ed) : base(ed)
        {
            this.ExternalFormatDocumentation = ed.ExternalFormatDocumentation;
            this.location = ed.Location;

            this.FileFormat = null;
            if (ed.FileFormat != null)
            {
                this.FileFormat = new FileFormatType(ed.FileFormat);
            }
        }
        
        /// <remarks>A URI to access documentation and tools to interpret the external format of the ExternalData instance. 
        /// For example, XML Schema or static libraries (APIs) to access binary formats.</remarks>
        /// min 0, max 1
        //public string ExternalFormatDocumentation

        /// min 0, max 1
        //public FileFormatType FileFormat

        /// <remarks>The location of the data file.</remarks>
        /// Required Attribute
        /// string
        //public string Location
    }

    /// <summary>
    /// MzIdentML FileFormatType
    /// </summary>
    /// <remarks>The format of the ExternalData file, for example "tiff" for image files.</remarks>
    public partial class FileFormatType
    {
        public FileFormatType(FileFormatInfo ffi)
        {
            this.cvParam = null;
            if (ffi.CVParam != null)
            {
                this.cvParam = new CVParamType(ffi.CVParam);
            }
        }

        /// <remarks>cvParam capturing file formats</remarks>
        /// Optional Attribute
        /// min 1, max 1
        //public CVParamType CVParam
    }

    /// <summary>
    /// MzIdentML SpectraDataType
    /// </summary>
    /// <remarks>A data set containing spectra data (consisting of one or more spectra).</remarks>
    public partial class SpectraDataType : ExternalDataType
    {
        public SpectraDataType(SpectraDataObj sd) : base(sd)
        {
            this.SpectrumIDFormat = null;

            if (sd.SpectrumIDFormat != null)
            {
                this.SpectrumIDFormat = new SpectrumIDFormatType(sd.SpectrumIDFormat);
            }
        }

        /// <remarks>A URI to access documentation and tools to interpret the external format of the ExternalData instance. 
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
        //public SpectrumIDFormatType SpectrumIDFormat
    }

    /// <summary>
    /// MzIdentML SpectrumIDFormatType
    /// </summary>
    /// <remarks>The format of the spectrum identifier within the source file</remarks>
    public partial class SpectrumIDFormatType
    {
        public SpectrumIDFormatType(SpectrumIDFormatObj sidf)
        {
            this.cvParam = null;

            if (sidf.CVParam != null)
            {
                this.cvParam = new CVParamType(sidf.CVParam);
            }
        }

        /// <remarks>CV term capturing the type of identifier used.</remarks>
        /// min 1, max 1
        //public CVParamType CVParams
    }

    /// <summary>
    /// MzIdentML SourceFileType
    /// </summary>
    /// <remarks>A file from which this mzIdentML instance was created.</remarks>
    public partial class SourceFileType : ExternalDataType, IParamGroup
    {
        public SourceFileType(SourceFileInfo sfi) : base(sfi)
        {
            this.cvParam = null;
            this.userParam = null;
            ParamGroupFunctions.CopyParamGroup(this, sfi);
        }

        /// <remarks>A URI to access documentation and tools to interpret the external format of the ExternalData instance. 
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
        //public List<UserParamType> UserParams
    }

    /// <summary>
    /// MzIdentML SearchDatabaseType
    /// </summary>
    /// <remarks>A database for searching mass spectra. Examples include a set of amino acid sequence entries, or annotated spectra libraries.</remarks>
    public partial class SearchDatabaseType : ExternalDataType, ICVParamGroup
    {
        public SearchDatabaseType(SearchDatabaseInfo sdi) : base(sdi)
        {
            this.version = sdi.Version;
            this.releaseDate = sdi.ReleaseDate;
            this.releaseDateSpecified = sdi.ReleaseDateSpecified;
            this.numDatabaseSequences = sdi.NumDatabaseSequences;
            this.numDatabaseSequencesSpecified = sdi.NumDatabaseSequencesSpecified;
            this.numResidues = sdi.NumResidues;
            this.numResiduesSpecified = sdi.NumResiduesSpecified;

            this.cvParam = null;
            ParamGroupFunctions.CopyCVParamGroup(this, sdi);

            this.DatabaseName = null;
            if (sdi.DatabaseName != null)
            {
                this.DatabaseName = new ParamType(sdi.DatabaseName);
            }
        }

        /// <remarks>A URI to access documentation and tools to interpret the external format of the ExternalData instance. 
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
        //public bool NumResiduesSpecified
    }

    /// <summary>
    /// MzIdentML ProteinDetectionProtocolType
    /// </summary>
    /// <remarks>The parameters and settings of a ProteinDetection process.</remarks>
    public partial class ProteinDetectionProtocolType : IdentifiableType
    {
        public ProteinDetectionProtocolType(ProteinDetectionProtocolObj pdp) : base(pdp)
        {
            this.analysisSoftware_ref = pdp.AnalysisSoftwareRef;

            this.AnalysisParams = null;
            this.Threshold = null;

            if (pdp.AnalysisParams != null)
            {
                this.AnalysisParams = new ParamListType(pdp.AnalysisParams);
            }
            if (pdp.Threshold != null)
            {
                this.Threshold = new ParamListType(pdp.Threshold);
            }
        }

        /// <remarks>The parameters and settings for the protein detection given as CV terms.</remarks>
        /// min 0, max 1
        //public ParamListType AnalysisParams

        /// <remarks>The threshold(s) applied to determine that a result is significant. 
        /// If multiple terms are used it is assumed that all conditions are satisfied by the passing results.</remarks>
        /// min 1, max 1
        //public ParamListType Threshold

        /// <remarks>The protein detection software used, given as a reference to the SoftwareCollection section.</remarks>
        /// Required Attribute
        /// string
        //public string AnalysisSoftwareRef
    }

    /// <summary>
    /// MzIdentML TranslationTableType
    /// </summary>
    /// <remarks>The table used to translate codons into nucleic acids e.g. by reference to the NCBI translation table.</remarks>
    public partial class TranslationTableType : IdentifiableType, ICVParamGroup
    {
        public TranslationTableType(TranslationTableObj tt) : base(tt)
        {
            this.cvParam = null;
            ParamGroupFunctions.CopyCVParamGroup(this, tt);
        }

        /// <remarks>The details specifying this translation table are captured as cvParams, e.g. translation table, translation 
        /// start codons and translation table description (see specification document and mapping file)</remarks>
        /// min 0, max unbounded
        //public List<CVParamType> CVParams
    }

    /// <summary>
    /// MzIdentML MassTableType
    /// </summary>
    /// <remarks>The masses of residues used in the search.</remarks>
    public partial class MassTableType : IdentifiableType, IParamGroup
    {
        public MassTableType(MassTableObj mt) : base(mt)
        {
            this.cvParam = null;
            this.userParam = null;
            ParamGroupFunctions.CopyParamGroup(this, mt);

            // Default values
            this.Residue = null;
            this.AmbiguousResidue = null;
            this.msLevel = null;

            if (mt.Residues != null && mt.Residues.Count > 0)
            {
                this.Residue = new List<ResidueType>();
                foreach (var r in mt.Residues)
                {
                    this.Residue.Add(new ResidueType(r));
                }
            }
            if (mt.AmbiguousResidues != null && mt.AmbiguousResidues.Count > 0)
            {
                this.AmbiguousResidue = new List<AmbiguousResidueType>();
                foreach (var ar in mt.AmbiguousResidues)
                {
                    this.AmbiguousResidue.Add(new AmbiguousResidueType(ar));
                }
            }
            if (mt.MsLevels != null && mt.MsLevels.Count > 0)
            {
                this.msLevel = new List<string>(mt.MsLevels);
            }
        }

        /// <remarks>The specification of a single residue within the mass table.</remarks>
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
        //public List<string> MsLevel
    }

    /// <summary>
    /// MzIdentML ResidueType
    /// </summary>
    public partial class ResidueType
    {
        public ResidueType(ResidueObj r)
        {
            this.code = r.Code;
            this.mass = r.Mass;
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
    public partial class AmbiguousResidueType : IParamGroup
    {
        public AmbiguousResidueType(AmbiguousResidueObj ar)
        {
            this.code = ar.Code;

            this.cvParam = null;
            this.userParam = null;
            ParamGroupFunctions.CopyParamGroup(this, ar);
        }

        /// <remarks>___ParamGroup___: Parameters for capturing e.g. "alternate single letter codes"</remarks>
        /// min 0, max unbounded
        //public List<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public List<UserParamType> UserParams

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
    public partial class EnzymeType : IdentifiableType
    {
        public EnzymeType(EnzymeObj e) : base(e)
        {
            this.SiteRegexp = e.SiteRegexp;
            this.nTermGain = e.NTermGain;
            this.cTermGain = e.CTermGain;
            this.semiSpecific = e.SemiSpecific;
            this.semiSpecificSpecified = e.SemiSpecificSpecified;
            this.missedCleavages = e.MissedCleavages;
            this.missedCleavagesSpecified = e.MissedCleavagesSpecified;
            this.minDistance = e.MinDistance;
            this.minDistanceSpecified = e.MinDistanceSpecified;

            this.EnzymeName = null;
            if (e.EnzymeName != null)
            {
                this.EnzymeName = new ParamListType(e.EnzymeName);
            }
        }

        /// <remarks>Regular expression for specifying the enzyme cleavage site.</remarks>
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
        //public bool MinDistanceSpecified
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationProtocolType
    /// </summary>
    /// <remarks>The parameters and settings of a SpectrumIdentification analysis.</remarks>
    public partial class SpectrumIdentificationProtocolType : IdentifiableType
    {
        public SpectrumIdentificationProtocolType(SpectrumIdentificationProtocolObj sip) : base(sip)
        {
            this.analysisSoftware_ref = sip.AnalysisSoftwareRef;

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

            if (sip.SearchType != null)
            {
                this.SearchType = new ParamType(sip.SearchType);
            }
            if (sip.AdditionalSearchParams != null)
            {
                this.AdditionalSearchParams = new ParamListType(sip.AdditionalSearchParams);
            }
            if (sip.ModificationParams != null && sip.ModificationParams.Count > 0)
            {
                this.ModificationParams = new List<SearchModificationType>();
                foreach (var mp in sip.ModificationParams)
                {
                    this.ModificationParams.Add(new SearchModificationType(mp));
                }
            }
            if (sip.Enzymes != null)
            {
                this.Enzymes = new EnzymesType(sip.Enzymes);
            }
            if (sip.MassTables != null && sip.MassTables.Count > 0)
            {
                this.MassTable = new List<MassTableType>();
                foreach (var mt in sip.MassTables)
                {
                    this.MassTable.Add(new MassTableType(mt));
                }
            }
            if (sip.FragmentTolerances != null && sip.FragmentTolerances.Count > 0)
            {
                this.FragmentTolerance = new List<CVParamType>();
                foreach (var ft in sip.FragmentTolerances)
                {
                    this.FragmentTolerance.Add(new CVParamType(ft));
                }
            }
            if (sip.ParentTolerances != null && sip.ParentTolerances.Count > 0)
            {
                this.ParentTolerance = new List<CVParamType>();
                foreach (var pt in sip.ParentTolerances)
                {
                    this.ParentTolerance.Add(new CVParamType(pt));
                }
            }
            if (sip.Threshold != null)
            {
                this.Threshold = new ParamListType(sip.Threshold);
            }
            if (sip.DatabaseFilters != null && sip.DatabaseFilters.Count > 0)
            {
                this.DatabaseFilters = new List<FilterType>();
                foreach (var df in sip.DatabaseFilters)
                {
                    this.DatabaseFilters.Add(new FilterType(df));
                }
            }
            if (sip.DatabaseTranslation != null)
            {
                this.DatabaseTranslation = new DatabaseTranslationType(sip.DatabaseTranslation);
            }
        }

        /// <remarks>The type of search performed e.g. PMF, Tag searches, MS-MS</remarks>
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
        //public string AnalysisSoftwareRef
    }

    /// <summary>
    /// MzIdentML SpecificityRulesType
    /// </summary>
    /// <remarks>The specificity rules of the searched modification including for example 
    /// the probability of a modification's presence or peptide or protein termini. Standard 
    /// fixed or variable status should be provided by the attribute fixedMod.</remarks>
    public partial class SpecificityRulesType : ICVParamGroup
    {
        public SpecificityRulesType(SpecificityRulesListObj srl)
        {
            this.cvParam = null;
            ParamGroupFunctions.CopyCVParamGroup(this, srl);
        }

        /// min 1, max unbounded
        //public List<CVParamType> CVParams
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
        public SearchModificationType(SearchModificationObj sm)
        {
            this.fixedMod = false;
            this.massDelta = 0;
            this.residues = null;

            this.cvParam = null;
            ParamGroupFunctions.CopyCVParamGroup(this, sm);

            this.SpecificityRules = null;
            if (sm.SpecificityRules != null && sm.SpecificityRules.Count > 0)
            {
                this.SpecificityRules = new List<SpecificityRulesType>();
                foreach (var sr in sm.SpecificityRules)
                {
                    this.SpecificityRules.Add(new SpecificityRulesType(sr));
                }
            }
        }

        /// min 0, max unbounded
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
        //public string Residues
    }

    /// <summary>
    /// MzIdentML EnzymesType
    /// </summary>
    /// <remarks>The list of enzymes used in experiment</remarks>
    public partial class EnzymesType
    {
        public EnzymesType(EnzymeListObj el)
        {
            this.independent = el.Independent;
            this.independentSpecified = el.IndependentSpecified;

            this.Enzyme = null;
            if (el.Enzymes != null && el.Enzymes.Count > 0)
            {
                this.Enzyme = new List<EnzymeType>();
                foreach (var e in el.Enzymes)
                {
                    this.Enzyme.Add(new EnzymeType(e));
                }
            }
        }

        /// min 1, max unbounded
        //public List<EnzymeType> Enzyme

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
    public partial class FilterType
    {
        public FilterType(FilterInfo fi)
        {
            // Default values
            this.FilterType1 = null;
            this.Include = null;
            this.Exclude = null;

            if (fi.FilterType != null)
            {
                this.FilterType1 = new ParamType(fi.FilterType);
            }
            if (fi.Include != null)
            {
                this.Include = new ParamListType(fi.Include);
            }
            if (fi.Exclude != null)
            {
                this.Exclude = new ParamListType(fi.Exclude);
            }
        }

        /// <remarks>The type of filter e.g. database taxonomy filter, pi filter, mw filter</remarks>
        /// min 1, max 1
        //public ParamType FilterType

        /// <remarks>All sequences fulfilling the specifed criteria are included.</remarks>
        /// min 0, max 1
        //public ParamListType Include

        /// <remarks>All sequences fulfilling the specifed criteria are excluded.</remarks>
        /// min 0, max 1
        //public ParamListType Exclude
    }

    /// <summary>
    /// MzIdentML DatabaseTranslationType
    /// </summary>
    /// <remarks>A specification of how a nucleic acid sequence database was translated for searching.</remarks>
    public partial class DatabaseTranslationType
    {
        public DatabaseTranslationType(DatabaseTranslationObj dt)
        {
            // Default values
            this.TranslationTable = null;
            this.frames = null;

            if (dt.TranslationTables != null && dt.TranslationTables.Count > 0)
            {
                this.TranslationTable = new List<TranslationTableType>();
                foreach (var t in dt.TranslationTables)
                {
                    this.TranslationTable.Add(new TranslationTableType(t));
                }
            }
            if (dt.Frames != null && dt.Frames.Count > 0)
            {
                this.frames = new List<int>(dt.Frames);
            }
        }

        /// min 1, max unbounded
        //public List<TranslationTableType> TranslationTable

        /// <remarks>The frames in which the nucleic acid sequence has been translated as a space separated List</remarks>
        /// Optional Attribute
        /// listOfAllowedFrames: space-separated string, valid values -3, -2, -1, 1, 2, 3
        //public List<int> Frames
    }

    /// <summary>
    /// MzIdentML ProtocolApplicationType
    /// </summary>
    /// <remarks>The use of a protocol with the requisite Parameters and ParameterValues. 
    /// ProtocolApplications can take Material or Data (or both) as input and produce Material or Data (or both) as output.</remarks>
    public abstract partial class ProtocolApplicationType : IdentifiableType
    {
        public ProtocolApplicationType(ProtocolApplicationObj pa) : base(pa)
        {
            this.activityDate = pa.ActivityDate;
            this.activityDateSpecified = pa.ActivityDateSpecified;
        }

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
    public partial class ProteinDetectionType : ProtocolApplicationType
    {
        public ProteinDetectionType(ProteinDetectionObj pd) : base(pd)
        {
            this.proteinDetectionList_ref = pd.ProteinDetectionListRef;
            this.proteinDetectionProtocol_ref = proteinDetectionProtocol_ref;

            this.InputSpectrumIdentifications = null;
            if (pd.InputSpectrumIdentifications != null && pd.InputSpectrumIdentifications.Count > 0)
            {
                this.InputSpectrumIdentifications = new List<InputSpectrumIdentificationsType>();
                foreach (var isi in pd.InputSpectrumIdentifications)
                {
                    this.InputSpectrumIdentifications.Add(new InputSpectrumIdentificationsType(isi));
                }
            }
        }

        /// min 1, max unbounded
        //public List<InputSpectrumIdentificationsType> InputSpectrumIdentifications

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
    public partial class InputSpectrumIdentificationsType
    {
        public InputSpectrumIdentificationsType(InputSpectrumIdentificationsObj isi)
        {
            this.spectrumIdentificationList_ref = isi.SpectrumIdentificationListRef;
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
    public partial class SpectrumIdentificationType : ProtocolApplicationType
    {
        public SpectrumIdentificationType(SpectrumIdentificationObj si) : base(si)
        {
            this.spectrumIdentificationProtocol_ref = si.SpectrumIdentificationProtocolRef;
            this.spectrumIdentificationList_ref = si.SpectrumIdentificationListRef;

            this.InputSpectra = null;
            this.SearchDatabaseRef = null;
            if (si.InputSpectra != null && si.InputSpectra.Count > 0)
            {
                this.InputSpectra = new List<InputSpectraType>();
                foreach (var ispec in si.InputSpectra)
                {
                    this.InputSpectra.Add(new InputSpectraType(ispec));
                }
            }
            if (si.SearchDatabases != null && si.SearchDatabases.Count > 0)
            {
                this.SearchDatabaseRef = new List<SearchDatabaseRefType>();
                foreach (var sdr in si.SearchDatabases)
                {
                    this.SearchDatabaseRef.Add(new SearchDatabaseRefType(sdr));
                }
            }
        }

        /// <remarks>One of the spectra data sets used.</remarks>
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
        //public string SpectrumIdentificationListRef
    }

    /// <summary>
    /// MzIdentML InputSpectraType
    /// </summary>
    /// <remarks>The attribute referencing an identifier within the SpectraData section.</remarks>
    public partial class InputSpectraType
    {
        public InputSpectraType(InputSpectraRefObj isr)
        {
            this.spectraData_ref = isr.SpectraDataRef;
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
    public partial class SearchDatabaseRefType
    {
        public SearchDatabaseRefType(SearchDatabaseRefObj sdri)
        {
            this.searchDatabase_ref = sdri.SearchDatabaseRef;
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
    public partial class PeptideEvidenceType : IdentifiableType, IParamGroup
    {
        public PeptideEvidenceType(PeptideEvidenceObj pe) : base(pe)
        {
            this.isDecoy = pe.IsDecoy;
            this.pre = pe.Pre;
            this.post = pe.Post;
            this.start = pe.Start;
            this.startSpecified = pe.StartSpecified;
            this.end = pe.End;
            this.endSpecified = pe.EndSpecified;
            this.translationTable_ref = pe.TranslationTableRef;
            this.frame = pe.Frame;
            this.frameSpecified = pe.FrameSpecified;
            this.peptide_ref = pe.PeptideRef;
            this.dBSequence_ref = pe.DBSequenceRef;

            this.cvParam = null;
            this.userParam = null;
            ParamGroupFunctions.CopyParamGroup(this, pe);
        }

        //public PeptideEvidenceType()

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
        //public string DBSequenceRef
    }

    /// <summary>
    /// MzIdentML PeptideType
    /// </summary>
    /// <remarks>One (poly)peptide (a sequence with modifications). The combination of Peptide sequence and modifications must be unique in the file.</remarks>
    public partial class PeptideType : IdentifiableType, IParamGroup
    {
        public PeptideType(PeptideObj p) : base(p)
        {
            this.PeptideSequence = p.PeptideSequence;

            this.cvParam = null;
            this.userParam = null;
            ParamGroupFunctions.CopyParamGroup(this, p);

            // Default values
            this.Modification = null;
            this.SubstitutionModification = null;

            if (p.Modifications != null && p.Modifications.Count > 0)
            {
                this.Modification = new List<ModificationType>();
                foreach (var m in p.Modifications)
                {
                    this.Modification.Add(new ModificationType(m));
                }
            }
            if (p.SubstitutionModifications != null && p.SubstitutionModifications.Count > 0)
            {
                this.SubstitutionModification = new List<SubstitutionModificationType>();
                foreach (var sm in p.SubstitutionModifications)
                {
                    this.SubstitutionModification.Add(new SubstitutionModificationType(sm));
                }
            }
        }

        /// <remarks>The amino acid sequence of the (poly)peptide. If a substitution modification has been found, the original sequence should be reported.</remarks>
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
        //public List<UserParamType> UserParams
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
        public ModificationType(ModificationObj m)
        {
            this.location = m.Location;
            this.locationSpecified = m.LocationSpecified;
            this.avgMassDelta = m.AvgMassDelta;
            this.avgMassDeltaSpecified = m.AvgMassDeltaSpecified;
            this.monoisotopicMassDelta = m.MonoisotopicMassDelta;
            this.monoisotopicMassDeltaSpecified = m.MonoisotopicMassDeltaSpecified;

            this.cvParam = null;
            ParamGroupFunctions.CopyCVParamGroup(this, m);

            this.residues = null;
            if (m.Residues != null && m.Residues.Count > 0)
            {
                this.residues = new List<string>(m.Residues);
            }
        }

        /// <remarks>CV terms capturing the modification, sourced from an appropriate controlled vocabulary.</remarks>
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
        //public bool MonoisotopicMassDeltaSpecified
    }

    /// <summary>
    /// MzIdentML SubstitutionModificationType
    /// </summary>
    /// <remarks>A modification where one residue is substituted by another (amino acid change).</remarks>
    public partial class SubstitutionModificationType
    {
        public SubstitutionModificationType(SubstitutionModificationObj sm)
        {
            this.originalResidue = sm.OriginalResidue;
            this.replacementResidue = sm.ReplacementResidue;
            this.location = sm.Location;
            this.locationSpecified = sm.LocationSpecified;
            this.avgMassDelta = sm.AvgMassDelta;
            this.avgMassDeltaSpecified = sm.AvgMassDeltaSpecified;
            this.monoisotopicMassDelta = sm.MonoisotopicMassDelta;
            this.monoisotopicMassDeltaSpecified = sm.AvgMassDeltaSpecified;
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
    public partial class DBSequenceType : IdentifiableType, IParamGroup
    {
        public DBSequenceType(DbSequenceObj dbs) : base(dbs)
        {
            this.Seq = dbs.Seq;
            this.accession = dbs.Accession;
            this.searchDatabase_ref = dbs.SearchDatabaseRef;
            this.length = dbs.Length;
            this.lengthSpecified = dbs.LengthSpecified;

            this.cvParam = null;
            this.userParam = null;
            ParamGroupFunctions.CopyParamGroup(this, dbs);
        }

        /// <remarks>The actual sequence of amino acids or nucleic acid.</remarks>
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
    public partial class SampleType : IdentifiableType, IParamGroup
    {
        public SampleType(SampleObj si) : base(si)
        {
            this.cvParam = null;
            this.userParam = null;
            ParamGroupFunctions.CopyParamGroup(this, si);

            this.ContactRole = null;
            this.SubSample = null;
            if (si.ContactRoles != null && si.ContactRoles.Count > 0)
            {
                this.ContactRole = new List<ContactRoleType>();
                foreach (var cr in si.ContactRoles)
                {
                    this.ContactRole.Add(new ContactRoleType(cr));
                }
            }
            if (si.SubSamples != null && si.SubSamples.Count > 0)
            {
                this.SubSample = new List<SubSampleType>();
                foreach (var ss in si.SubSamples)
                {
                    this.SubSample.Add(new SubSampleType(ss));
                }
            }
        }

        /// <remarks>Contact details for the Material. The association to ContactRole could specify, for example, the creator or provider of the Material.</remarks>
        /// min 0, max unbounded
        //public List<ContactRoleType> ContactRoles

        /// min 0, max unbounded
        //public List<SubSampleType> SubSamples

        /// <remarks>___ParamGroup___: The characteristics of a Material.</remarks>
        /// min 0, max unbounded
        //public List<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public List<UserParamType> UserParams
    }

    /// <summary>
    /// MzIdentML ContactRoleType
    /// </summary>
    /// <remarks>The role that a Contact plays in an organization or with respect to the associating class. 
    /// A Contact may have several Roles within scope, and as such, associations to ContactRole 
    /// allow the use of a Contact in a certain manner. Examples might include a provider, or a data analyst.</remarks>
    public partial class ContactRoleType
    {
        public ContactRoleType(ContactRoleObj cri)
        {
            this.contact_ref = cri.ContactRef;

            this.Role = null;
            if (cri.Role != null)
            {
                this.Role = new RoleType(cri.Role);
            }
        }

        // min 1, max 1
        //public RoleType Role

        /// <remarks>When a ContactRole is used, it specifies which Contact the role is associated with.</remarks>
        /// Required Attribute
        /// string
        //public string contact_ref
    }

    /// <summary>
    /// MzIdentML RoleType
    /// </summary>
    /// <remarks>The roles (lab equipment sales, contractor, etc.) the Contact fills.</remarks>
    public partial class RoleType
    {
        public RoleType(RoleObj ri)
        {
            this.cvParam = null;

            if (ri.CVParam != null)
            {
                this.cvParam = new CVParamType(ri.CVParam);
            }
        }

        /// <remarks>CV term for contact roles, such as software provider.</remarks>
        /// min 1, max 1
        //public CVParamType CVParam
    }

    /// <summary>
    /// MzIdentML SubSampleType
    /// </summary>
    /// <remarks>References to the individual component samples within a mixed parent sample.</remarks>
    public partial class SubSampleType
    {
        public SubSampleType(SubSampleObj ss)
        {
            this.sample_ref = ss.SampleRef;
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
    public abstract partial class AbstractContactType : IdentifiableType, IParamGroup
    {
        public AbstractContactType(AbstractContactObj aci) : base(aci)
        {
            this.cvParam = null;
            this.userParam = null;
            ParamGroupFunctions.CopyParamGroup(this, aci);
        }

        /// <remarks>___ParamGroup___: Attributes of this contact such as address, email, telephone etc.</remarks>
        /// min 0, max unbounded
        //public List<CVParamType> CVParams

        /// <remarks>___ParamGroup___</remarks>
        /// min 0, max unbounded
        //public List<UserParamType> UserParams
    }

    /// <summary>
    /// MzIdentML OrganizationType
    /// </summary>
    /// <remarks>Organizations are entities like companies, universities, government agencies. 
    /// Any additional information such as the address, email etc. should be supplied either as CV parameters or as user parameters.</remarks>
    public partial class OrganizationType : AbstractContactType
    {
        public OrganizationType(OrganizationObj o) : base(o)
        {
            this.Parent = null;
            if (o.Parent != null)
            {
                this.Parent = new ParentOrganizationType(o.Parent);
            }
        }

        /// min 0, max 1
        //public ParentOrganizationType Parent
    }

    /// <summary>
    /// MzIdentML ParentOrganizationType
    /// </summary>
    /// <remarks>The containing organization (the university or business which a lab belongs to, etc.)</remarks>
    public partial class ParentOrganizationType
    {
        public ParentOrganizationType(ParentOrganizationObj po)
        {
            this.organization_ref = po.OrganizationRef;
        }

        /// <remarks>A reference to the organization this contact belongs to.</remarks>
        /// Required Attribute
        /// string
        //public string organizationRef
    }

    /// <summary>
    /// MzIdentML PersonType
    /// </summary>
    /// <remarks>A person's name and contact details. Any additional information such as the address, 
    /// contact email etc. should be supplied using CV parameters or user parameters.</remarks>
    public partial class PersonType : AbstractContactType
    {
        public PersonType(PersonObj pi) : base(pi)
        {
            this.lastName = pi.LastName;
            this.firstName = pi.FirstName;
            this.midInitials = pi.MidInitials;

            this.Affiliation = null;
            if (pi.Affiliations != null && pi.Affiliations.Count > 0)
            {
                this.Affiliation = new List<AffiliationType>();
                foreach (var a in pi.Affiliations)
                {
                    this.Affiliation.Add(new AffiliationType(a));
                }
            }
        }

        /// <remarks>The organization a person belongs to.</remarks>
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
        //public string MidInitials
    }

    /// <summary>
    /// MzIdentML AffiliationType
    /// </summary>
    public partial class AffiliationType
    {
        public AffiliationType(AffiliationObj ai)
        {
            this.organization_ref = ai.OrganizationRef;
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
    public partial class ProviderType : IdentifiableType
    {
        public ProviderType(ProviderObj pi) : base(pi)
        {
            this.analysisSoftware_ref = pi.AnalysisSoftwareRef;

            this.ContactRole = null;
            if (pi.ContactRole != null)
            {
                this.ContactRole = new ContactRoleType(pi.ContactRole);
            }
        }

        /// <remarks>The Contact that provided the document instance.</remarks>
        /// min 0, max 1
        //public ContactRoleType ContactRole

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
    public partial class AnalysisSoftwareType : IdentifiableType
    {
        public AnalysisSoftwareType(AnalysisSoftwareObj asi) : base(asi)
        {
            this.Customizations = asi.Customizations;
            this.version = asi.Version;
            this.uri = asi.URI;

            // Default values
            this.ContactRole = null;
            this.SoftwareName = null;

            if (asi.ContactRole != null)
            {
                this.ContactRole = new ContactRoleType(asi.ContactRole);
            }
            if (asi.SoftwareName != null)
            {
                this.SoftwareName = new ParamType(asi.SoftwareName);
            }
        }

        /// <remarks>The contact details of the organisation or person that produced the software</remarks>
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
        //public string URI
    }

    /// <summary>
    /// MzIdentML InputsType
    /// </summary>
    /// <remarks>The inputs to the analyses including the databases searched, the spectral data and the source file converted to mzIdentML.</remarks>
    public partial class InputsType
    {
        public InputsType(InputsObj ii)
        {
            // Default values
            this.SourceFile = null;
            this.SearchDatabase = null;
            this.SpectraData = null;

            if (ii.SourceFiles != null && ii.SourceFiles.Count > 0)
            {
                this.SourceFile = new List<SourceFileType>();
                foreach (var sf in ii.SourceFiles)
                {
                    this.SourceFile.Add(new SourceFileType(sf));
                }
            }
            if (ii.SearchDatabases != null && ii.SearchDatabases.Count > 0)
            {
                this.SearchDatabase = new List<SearchDatabaseType>();
                foreach (var sd in ii.SearchDatabases)
                {
                    this.SearchDatabase.Add(new SearchDatabaseType(sd));
                }
            }
            if (ii.SpectraDataList != null && ii.SpectraDataList.Count > 0)
            {
                this.SpectraData = new List<SpectraDataType>();
                foreach (var sd in ii.SpectraDataList)
                {
                    this.SpectraData.Add(new SpectraDataType(sd));
                }
            }
        }

        /// min 0, max unbounded
        //public List<SourceFileType> SourceFile

        /// min 0, max unbounded
        //public List<SearchDatabaseType> SearchDatabase

        /// min 1, max unbounde
        //public List<SpectraDataType> SpectraData
    }

    /// <summary>
    /// MzIdentML DataCollectionType
    /// </summary>
    /// <remarks>The collection of input and output data sets of the analyses.</remarks>
    public partial class DataCollectionType
    {
        public DataCollectionType(DataCollectionObj dc)
        {
            // Default values
            this.Inputs = null;
            this.AnalysisData = null;

            if (dc.Inputs != null)
            {
                this.Inputs = new InputsType(dc.Inputs);
            }
            if (dc.AnalysisData != null)
            {
                this.AnalysisData = new AnalysisDataType(dc.AnalysisData);
            }
        }

        /// min 1, max 1
        //public InputsInfo Inputs

        /// min 1, max 1
        //public AnalysisDataType AnalysisData
    }

    /// <summary>
    /// MzIdentML AnalysisProtocolCollectionType
    /// </summary>
    /// <remarks>The collection of protocols which include the parameters and settings of the performed analyses.</remarks>
    public partial class AnalysisProtocolCollectionType
    {
        public AnalysisProtocolCollectionType(AnalysisProtocolCollectionObj apc)
        {
            // Default values
            this.SpectrumIdentificationProtocol = null;
            this.ProteinDetectionProtocol = null;

            if (apc.SpectrumIdentificationProtocols != null && apc.SpectrumIdentificationProtocols.Count > 0)
            {
                this.SpectrumIdentificationProtocol = new List<SpectrumIdentificationProtocolType>();
                foreach (var sip in apc.SpectrumIdentificationProtocols)
                {
                    this.SpectrumIdentificationProtocol.Add(new SpectrumIdentificationProtocolType(sip));
                }
            }
            if (apc.ProteinDetectionProtocol != null)
            {
                this.ProteinDetectionProtocol = new ProteinDetectionProtocolType(apc.ProteinDetectionProtocol);
            }
        }

        /// min 1, max unbounded
        //public List<SpectrumIdentificationProtocolType> SpectrumIdentificationProtocol

        /// min 0, max 1
        //public ProteinDetectionProtocolType ProteinDetectionProtocol
    }

    /// <summary>
    /// MzIdentML AnalysisCollectionType
    /// </summary>
    /// <remarks>The analyses performed to get the results, which map the input and output data sets. 
    /// Analyses are for example: SpectrumIdentification (resulting in peptides) or ProteinDetection (assemble proteins from peptides).</remarks>
    public partial class AnalysisCollectionType
    {
        public AnalysisCollectionType(AnalysisCollectionObj ac)
        {
            // Default values
            this.SpectrumIdentification = null;
            this.ProteinDetection = null;

            if (ac.SpectrumIdentifications != null && ac.SpectrumIdentifications.Count > 0)
            {
                this.SpectrumIdentification = new List<SpectrumIdentificationType>();
                foreach (var si in ac.SpectrumIdentifications)
                {
                    this.SpectrumIdentification.Add(new SpectrumIdentificationType(si));
                }
            }
            if (ac.ProteinDetection != null)
            {
                this.ProteinDetection = new ProteinDetectionType(ac.ProteinDetection);
            }
        }

        /// min 1, max unbounded
        //public List<SpectrumIdentificationType> SpectrumIdentification

        /// min 0, max 1
        //public ProteinDetectionType ProteinDetection
    }

    /// <summary>
    /// MzIdentML SequenceCollectionType
    /// </summary>
    /// <remarks>The collection of sequences (DBSequence or Peptide) identified and their relationship between 
    /// each other (PeptideEvidence) to be referenced elsewhere in the results.</remarks>
    public partial class SequenceCollectionType
    {
        public SequenceCollectionType(SequenceCollectionObj sc)
        {
            // Default values
            this.DBSequence = null;
            this.Peptide = null;
            this.PeptideEvidence = null;

            if (sc.DBSequences != null && sc.DBSequences.Count > 0)
            {
                this.DBSequence = new List<DBSequenceType>();
                foreach (var dbs in sc.DBSequences)
                {
                    this.DBSequence.Add(new DBSequenceType(dbs));
                }
            }
            if (sc.Peptides != null && sc.Peptides.Count > 0)
            {
                this.Peptide = new List<PeptideType>();
                foreach (var p in sc.Peptides)
                {
                    this.Peptide.Add(new PeptideType(p));
                }
            }
            if (sc.PeptideEvidences != null && sc.PeptideEvidences.Count > 0)
            {
                this.PeptideEvidence = new List<PeptideEvidenceType>();
                foreach (var pe in sc.PeptideEvidences)
                {
                    this.PeptideEvidence.Add(new PeptideEvidenceType(pe));
                }
            }
        }

        /// min 1, max unbounded
        //public List<DBSequenceType> DBSequences

        /// min 0, max unbounded
        //public List<PeptideType> Peptides

        /// min 0, max unbounded
        //public List<PeptideEvidenceType> PeptideEvidences
    }
}
