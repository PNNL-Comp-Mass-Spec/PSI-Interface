using System.Collections.Generic;
using PSI_Interface.IdentData.IdentDataObjs;

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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="identData"></param>
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
                this.cvList = new List<cvType>(identData.CVList.Count);
                this.cvList.AddRange(identData.CVList, cv => new cvType(cv));
            }
            if (identData.AnalysisSoftwareList != null && identData.AnalysisSoftwareList.Count > 0)
            {
                this.AnalysisSoftwareList = new List<AnalysisSoftwareType>(identData.AnalysisSoftwareList.Count);
                this.AnalysisSoftwareList.AddRange(identData.AnalysisSoftwareList, asl => new AnalysisSoftwareType(asl));
            }
            if (identData.Provider != null)
            {
                this.Provider = new ProviderType(identData.Provider);
            }
            if (identData.AuditCollection != null && identData.AuditCollection.Count > 0)
            {
                this.AuditCollection = new List<AbstractContactType>(identData.AuditCollection.Count);
                this.AuditCollection.AddRange(identData.AuditCollection, a =>
                {
                    if (a is OrganizationObj o)
                    {
                        return new OrganizationType(o);
                    }
                    else if (a is PersonObj p)
                    {
                        return new PersonType(p);
                    }

                    return null; // If this is hit, something is very wrong...
                });
            }
            if (identData.AnalysisSampleCollection != null && identData.AnalysisSampleCollection.Count > 0)
            {
                this.AnalysisSampleCollection = new List<SampleType>(identData.AnalysisSampleCollection.Count);
                this.AnalysisSampleCollection.AddRange(identData.AnalysisSampleCollection, s => new SampleType(s));
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
                this.BibliographicReference = new List<BibliographicReferenceType>(identData.BibliographicReferences.Count);
                this.BibliographicReference.AddRange(identData.BibliographicReferences, br => new BibliographicReferenceType(br));
            }
        }

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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="cvi"></param>
        public cvType(CVInfo cvi)
        {
            this.fullName = cvi.FullName;
            this.version = cvi.Version;
            this.uri = cvi.URI;
            this.id = cvi.Id;
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="spectrumIdItemRef"></param>
        public SpectrumIdentificationItemRefType(SpectrumIdentificationItemRefObj spectrumIdItemRef)
        {
            this.spectrumIdentificationItem_ref = spectrumIdItemRef.SpectrumIdentificationItemRef;
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="ph"></param>
        public PeptideHypothesisType(PeptideHypothesisObj ph)
        {
            this.peptideEvidence_ref = ph.PeptideEvidenceRef;

            this.SpectrumIdentificationItemRef = null;
            if (ph.SpectrumIdentificationItems != null && ph.SpectrumIdentificationItems.Count > 0)
            {
                this.SpectrumIdentificationItemRef = new List<SpectrumIdentificationItemRefType>(ph.SpectrumIdentificationItems.Count);
                this.SpectrumIdentificationItemRef.AddRange(ph.SpectrumIdentificationItems, spectrumIdItemRef => new SpectrumIdentificationItemRefType(spectrumIdItemRef));
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="fa"></param>
        public FragmentArrayType(FragmentArrayObj fa)
        {
            this.measure_ref = fa.MeasureRef;

            this.values = null;
            if (fa.Values != null && fa.Values.Count > 0)
            {
                this.values = new List<float>(fa.Values);
            }
        }

        /*
        /// <remarks>The values of this particular measure, corresponding to the index defined in ion type</remarks>
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="iti"></param>
        public IonTypeType(IonTypeObj iti)
        {
            this.charge = iti.Charge;

            this.cvParam = null;
            this.userParam = null;
            this.FragmentArray = null;
            this.index = null;

            if (iti.CVParams != null && iti.CVParams.Count > 0)
            {
                this.cvParam = new List<CVParamType>(iti.CVParams.Count);
                this.cvParam.AddRange(iti.CVParams, cvp => new CVParamType(cvp));
            }

            if (iti.UserParams != null && iti.UserParams.Count > 0)
            {
                this.userParam = new List<UserParamType>(iti.UserParams.Count);
                this.userParam.AddRange(iti.UserParams, up => new UserParamType(up));
            }

            if (iti.IdentData.Version.Equals("1.1"))
            {
                if (this.cvParam != null && this.cvParam.Count > 1)
                {
                    this.cvParam.RemoveRange(1, this.cvParam.Count - 1);
                }
                this.userParam = null;
            }

            if (iti.FragmentArrays != null && iti.FragmentArrays.Count > 0)
            {
                this.FragmentArray = new List<FragmentArrayType>(iti.FragmentArrays.Count);
                this.FragmentArray.AddRange(iti.FragmentArrays, fa => new FragmentArrayType(fa));
            }
            if (iti.Index != null && iti.Index.Count > 0)
            {
                this.index = new List<string>(iti.Index);
            }
        }

        /*
        /// <remarks>min 0, max unbounded</remarks>
        //public List<FragmentArrayType> FragmentArray

        /// <remarks>
        /// In case more information about the ions annotation has to be conveyed, that has no fit in FragmentArray.
        /// Note: It is suggested that the value attribute takes the form of a list of the same size as FragmentArray values.
        /// However, there is no formal encoding and it cannot be expected that other software will process or impart that information properly.
        /// </remarks>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<UserParamType> userParam // mzIdentML 1.2

        /// <summary>The type of ion identified.</summary>
        /// <remarks>(mzIdentML 1.2 add) In the case of neutral losses, one term should report the ion type, a second term should report the neutral loss
        /// Note: this is a change in practice from mzIdentML 1.1.</remarks>
        /// <remarks>min 1, max 1</remarks>
        //public CVParamType CVParam // mzIdentML 1.1
        //public List<CVParamType> CVParam // mzIdentML 1.2

        /// <summary>The index of ions identified as integers, following standard notation for a-c, x-z e.g. if b3 b5 and b6 have been identified, the index would store "3 5 6". For internal ions, the index contains pairs defining the start and end point - see specification document for examples. For immonium ions, the index is the position of the identified ion within the peptide sequence - if the peptide contains the same amino acid in multiple positions that cannot be distinguished, all positions should be given.</summary>
        /// <remarks>(mzIdentML 1.2 add) For precursor ions, including neutral losses, the index value MUST be 0. For any other ions not related to the position within the peptide sequence e.g. quantification reporter ions, the index value MUST be 0.</remarks>
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="cvp"></param>
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
        public AbstractParamType(ParamBase pb)
        {
            this.name = pb.Name;
            this.value = pb.Value;
            this.unitAccession = pb.UnitAccession;
            this.unitName = pb.UnitName;
            this.unitCvRef = pb.UnitCvRef;
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="up"></param>
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="p"></param>
        public ParamType(ParamObj p)
        {
            this.Item = null;

            if (p.Item is CVParamObj cvp)
            {
                this.Item = new CVParamType(cvp);
            }
            else if (p.Item is UserParamObj up)
            {
                this.Item = new UserParamType(up);
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="pl"></param>
        public ParamListType(ParamListObj pl)
        {
            this.Items = null;

            if (pl.Items != null && pl.Items.Count > 0)
            {
                this.Items = new List<AbstractParamType>(pl.Items.Count);
                this.Items.AddRange(pl.Items, p =>
                {
                    if (p is CVParamObj cvp)
                    {
                        return new CVParamType(cvp);
                    }
                    else if (p is UserParamObj up)
                    {
                        return new UserParamType(up);
                    }

                    return null;
                });
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="peri"></param>
        public PeptideEvidenceRefType(PeptideEvidenceRefObj peri)
        {
            this.peptideEvidence_ref = peri.PeptideEvidenceRef;
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="ad"></param>
        public AnalysisDataType(AnalysisDataObj ad)
        {
            // Default values
            this.SpectrumIdentificationList = null;
            this.ProteinDetectionList = null;

            if (ad.SpectrumIdentificationList != null && ad.SpectrumIdentificationList.Count > 0)
            {
                this.SpectrumIdentificationList = new List<SpectrumIdentificationListType>(ad.SpectrumIdentificationList.Count);
                this.SpectrumIdentificationList.AddRange(ad.SpectrumIdentificationList, sil => new SpectrumIdentificationListType(sil));
            }
            if (ad.ProteinDetectionList != null)
            {
                this.ProteinDetectionList = new ProteinDetectionListType(ad.ProteinDetectionList);
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="sil"></param>
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
                this.FragmentationTable = new List<MeasureType>(sil.FragmentationTables.Count);
                this.FragmentationTable.AddRange(sil.FragmentationTables, f => new MeasureType(f));
            }
            if (sil.SpectrumIdentificationResults != null && sil.SpectrumIdentificationResults.Count > 0)
            {
                this.SpectrumIdentificationResult = new List<SpectrumIdentificationResultType>(sil.SpectrumIdentificationResults.Count);
                this.SpectrumIdentificationResult.AddRange(sil.SpectrumIdentificationResults, sir => new SpectrumIdentificationResultType(sir));
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="m"></param>
        public MeasureType(MeasureObj m) : base(m)
        {
            this.cvParam = null;
            ParamGroupFunctions.CopyCVParamGroup(this, m);
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="idId"></param>
        public IdentifiableType(IIdentifiableType idId)
        {
            this.id = idId.Id;
            this.name = idId.Name;
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="br"></param>
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="pdh"></param>
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
                this.PeptideHypothesis = new List<PeptideHypothesisType>(pdh.PeptideHypotheses.Count);
                this.PeptideHypothesis.AddRange(pdh.PeptideHypotheses, ph => new PeptideHypothesisType(ph));
            }
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

        /// <remarks>A reference to the corresponding DBSequence entry.
        /// (mzIdentML 1.1) This optional and redundant, because the PeptideEvidence elements referenced from here also map to the DBSequence.
        /// (mzIdentML 1.2) Note - this attribute was optional in mzIdentML 1.1 but is now mandatory in mzIdentML 1.2. Consuming software should assume that the DBSequence entry referenced here is the definitive identifier for the protein.
        /// </remarks>
        /// <remarks>
        /// Optional Attribute (mzIdentML 1.1)
        /// Required Attribute (mzIdentML 1.2)
        /// </remarks>
        //public string DBSequenceRef

        /// <remarks>Set to true if the producers of the file has deemed that the ProteinDetectionHypothesis has passed a given
        /// threshold or been validated as correct. If no such threshold has been set, value of true should be given for all results.</remarks>
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="pag"></param>
        public ProteinAmbiguityGroupType(ProteinAmbiguityGroupObj pag) : base(pag)
        {
            this.cvParam = null;
            this.userParam = null;
            ParamGroupFunctions.CopyParamGroup(this, pag);

            this.ProteinDetectionHypothesis = null;
            if (pag.ProteinDetectionHypotheses != null && pag.ProteinDetectionHypotheses.Count > 0)
            {
                this.ProteinDetectionHypothesis = new List<ProteinDetectionHypothesisType>(pag.ProteinDetectionHypotheses.Count);
                this.ProteinDetectionHypothesis.AddRange(pag.ProteinDetectionHypotheses, pdh => new ProteinDetectionHypothesisType(pdh));
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="pdl"></param>
        public ProteinDetectionListType(ProteinDetectionListObj pdl) : base(pdl)
        {
            this.cvParam = null;
            this.userParam = null;
            ParamGroupFunctions.CopyParamGroup(this, pdl);

            this.ProteinAmbiguityGroup = null;
            if (pdl.ProteinAmbiguityGroups != null && pdl.ProteinAmbiguityGroups.Count > 0)
            {
                this.ProteinAmbiguityGroup = new List<ProteinAmbiguityGroupType>(pdl.ProteinAmbiguityGroups.Count);
                this.ProteinAmbiguityGroup.AddRange(pdl.ProteinAmbiguityGroups, pag => new ProteinAmbiguityGroupType(pag));
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="sii"></param>
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
                this.PeptideEvidenceRef = new List<PeptideEvidenceRefType>(sii.PeptideEvidences.Count);
                this.PeptideEvidenceRef.AddRange(sii.PeptideEvidences, per => new PeptideEvidenceRefType(per));
            }
            if (sii.Fragmentations != null && sii.Fragmentations.Count > 0)
            {
                this.Fragmentation = new List<IonTypeType>(sii.Fragmentations.Count);
                this.Fragmentation.AddRange(sii.Fragmentations, f => new IonTypeType(f));
            }
        }

        /*
        /// <remarks>An identifier is an unambiguous string that is unique within the scope
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// <remarks>Required Attribute</remarks>
        //public string Id

        /// <summary>The potentially ambiguous common identifier, such as a human-readable name for the instance.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string Name

        /// <remarks>min 1, max unbounded // mzIdentML 1.1</remarks>
        /// <remarks>min 0, max unbounded // mzIdentML 1.2 (0 only allowed if AdditionalSearchParams contains cvParam "De novo search")</remarks>
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
        /// <remarks>Optional Attribute</remarks> // mzIdentML 1.1
        /// <remarks>Required Attribute</remarks> // mzIdentML 1.2
        //public string PeptideRef

        /// <remarks>For an MS/MS result set, this is the rank of the identification quality as scored by the search engine.
        /// 1 is the top rank. If multiple identifications have the same top score, they should all be assigned rank =1.
        /// For PMF data, the rank attribute may be meaningless and values of rank = 0 should be given.</remarks>
        /// <remarks>Required Attribute</remarks>
        //public int Rank

        /// <remarks>Set to true if the producers of the file has deemed that the identification has passed a given threshold
        /// or been validated as correct. If no such threshold has been set, value of true should be given for all results.</remarks>
        /// <remarks>Required Attribute</remarks>
        //public bool PassThreshold

        /// <summary>A reference should be given to the MassTable used to calculate the sequenceMass only if more than one MassTable has been given.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string MassTableRef

        /// <remarks>A reference should be provided to link the SpectrumIdentificationItem to a Sample
        /// if more than one sample has been described in the AnalysisSampleCollection.</remarks>
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="sir"></param>
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
                this.SpectrumIdentificationItem = new List<SpectrumIdentificationItemType>(sir.SpectrumIdentificationItems.Count);
                this.SpectrumIdentificationItem.AddRange(sir.SpectrumIdentificationItems, sii => new SpectrumIdentificationItemType(sii));
            }
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public List<SpectrumIdentificationItemType> SpectrumIdentificationItems

        /// <remarks>___ParamGroup___: Scores or parameters associated with the SpectrumIdentificationResult
        /// (i.e the set of SpectrumIdentificationItems derived from one spectrum) e.g. the number of peptide
        /// sequences within the parent tolerance for this spectrum.</remarks>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<CVParamType> CVParams

        /// <summary>___ParamGroup___</summary>
        /// <remarks>min 0, max unbounded</remarks>
        //public List<UserParamType> UserParams

        /// <remarks>The locally unique id for the spectrum in the spectra data set specified by SpectraData_ref.
        /// External guidelines are provided on the use of consistent identifiers for spectra in different external formats.</remarks>
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="ed"></param>
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

        /*
        /// <remarks>A URI to access documentation and tools to interpret the external format of the ExternalData instance.
        /// For example, XML Schema or static libraries (APIs) to access binary formats.</remarks>
        /// <remarks>min 0, max 1</remarks>
        //public string ExternalFormatDocumentation

        /// <remarks>min 0, max 1 (mzIdentML 1.1)</remarks>
        /// <remarks>min 1, max 1 (mzIdentML 1.2)</remarks>
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="ffi"></param>
        public FileFormatType(FileFormatInfo ffi)
        {
            this.cvParam = null;
            if (ffi.CVParam != null)
            {
                this.cvParam = new CVParamType(ffi.CVParam);
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="sd"></param>
        public SpectraDataType(SpectraDataObj sd) : base(sd)
        {
            this.SpectrumIDFormat = null;

            if (sd.SpectrumIDFormat != null)
            {
                this.SpectrumIDFormat = new SpectrumIDFormatType(sd.SpectrumIDFormat);
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="idFormatObject"></param>
        public SpectrumIDFormatType(SpectrumIDFormatObj idFormatObject)
        {
            this.cvParam = null;

            if (idFormatObject.CVParam != null)
            {
                this.cvParam = new CVParamType(idFormatObject.CVParam);
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="sfi"></param>
        public SourceFileType(SourceFileInfo sfi) : base(sfi)
        {
            this.cvParam = null;
            this.userParam = null;
            ParamGroupFunctions.CopyParamGroup(this, sfi);
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
    /// <remarks>A database for searching mass spectra. Examples include a set of amino acid sequence entries, nucleotide databases (e.g. 6 frame translated) (mzIdentML 1.2), or annotated spectra libraries.</remarks>
    public partial class SearchDatabaseType : ExternalDataType, ICVParamGroup
    {
        /// <summary>
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="sdi"></param>
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="pdp"></param>
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="tt"></param>
        public TranslationTableType(TranslationTableObj tt) : base(tt)
        {
            this.cvParam = null;
            ParamGroupFunctions.CopyCVParamGroup(this, tt);
        }

        /*
        /// <remarks>The details specifying this translation table are captured as cvParams, e.g. translation table, translation
        /// start codons and translation table description (see specification document and mapping file)</remarks>
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="mt"></param>
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
                this.Residue = new List<ResidueType>(mt.Residues.Count);
                this.Residue.AddRange(mt.Residues, r => new ResidueType(r));
            }
            if (mt.AmbiguousResidues != null && mt.AmbiguousResidues.Count > 0)
            {
                this.AmbiguousResidue = new List<AmbiguousResidueType>(mt.AmbiguousResidues.Count);
                this.AmbiguousResidue.AddRange(mt.AmbiguousResidues, ar => new AmbiguousResidueType(ar));
            }
            if (mt.MsLevels != null && mt.MsLevels.Count > 0)
            {
                this.msLevel = new List<string>(mt.MsLevels);
            }
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
        //public List<string> MsLevel*/
    }

    /// <summary>
    /// MzIdentML ResidueType
    /// </summary>
    public partial class ResidueType
    {
        /// <summary>
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="r"></param>
        public ResidueType(ResidueObj r)
        {
            this.code = r.Code;
            this.mass = r.Mass;
        }

        /*
        /// <remarks>The single letter code for the residue.</remarks>
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="ar"></param>
        public AmbiguousResidueType(AmbiguousResidueObj ar)
        {
            this.code = ar.Code;

            this.cvParam = null;
            this.userParam = null;
            ParamGroupFunctions.CopyParamGroup(this, ar);
        }

        /*
        /// <remarks>___ParamGroup___: Parameters for capturing e.g. "alternate single letter codes"</remarks>
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
    /// or a CV term if a "standard" enzyme cleavage has been performed.
    /// </remarks>
    public partial class EnzymeType : IdentifiableType
    {
        /// <summary>
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="e"></param>
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

        /// <remarks>Set to true if the enzyme cleaves semi-specifically (i.e. one terminus must cleave
        /// according to the rules, the other can cleave at any residue), false if the enzyme cleavage
        /// is assumed to be specific to both termini (accepting for any missed cleavages).</remarks>
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
        /// <returns>integer >= 1</returns>
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="sip"></param>
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
                this.ModificationParams = new List<SearchModificationType>(sip.ModificationParams.Count);
                this.ModificationParams.AddRange(sip.ModificationParams, mp => new SearchModificationType(mp));
            }
            if (sip.Enzymes != null && sip.Enzymes.Enzymes.Count > 0)
            {
                this.Enzymes = new EnzymesType(sip.Enzymes);
            }
            if (sip.MassTables != null && sip.MassTables.Count > 0)
            {
                this.MassTable = new List<MassTableType>(sip.MassTables.Count);
                this.MassTable.AddRange(sip.MassTables, mt => new MassTableType(mt));
            }
            if (sip.FragmentTolerances != null && sip.FragmentTolerances.Count > 0)
            {
                this.FragmentTolerance = new List<CVParamType>(sip.FragmentTolerances.Count);
                this.FragmentTolerance.AddRange(sip.FragmentTolerances, ft => new CVParamType(ft));
            }
            if (sip.ParentTolerances != null && sip.ParentTolerances.Count > 0)
            {
                this.ParentTolerance = new List<CVParamType>(sip.ParentTolerances.Count);
                this.ParentTolerance.AddRange(sip.ParentTolerances, pt => new CVParamType(pt));
            }
            if (sip.Threshold != null)
            {
                this.Threshold = new ParamListType(sip.Threshold);
            }
            if (sip.DatabaseFilters != null && sip.DatabaseFilters.Count > 0)
            {
                this.DatabaseFilters = new List<FilterType>(sip.DatabaseFilters.Count);
                this.DatabaseFilters.AddRange(sip.DatabaseFilters, df => new FilterType(df));
            }
            if (sip.DatabaseTranslation != null)
            {
                this.DatabaseTranslation = new DatabaseTranslationType(sip.DatabaseTranslation);
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="srl"></param>
        public SpecificityRulesType(SpecificityRulesListObj srl)
        {
            this.cvParam = null;
            ParamGroupFunctions.CopyCVParamGroup(this, srl);
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="sm"></param>
        public SearchModificationType(SearchModificationObj sm)
        {
            this.fixedMod = sm.FixedMod;
            this.massDelta = sm.MassDelta;
            this.residues = sm.Residues;

            this.cvParam = null;
            ParamGroupFunctions.CopyCVParamGroup(this, sm);

            this.SpecificityRules = null;
            if (sm.SpecificityRules != null && sm.SpecificityRules.Count > 0)
            {
                this.SpecificityRules = new List<SpecificityRulesType>(sm.SpecificityRules.Count);
                this.SpecificityRules.AddRange(sm.SpecificityRules, sr => new SpecificityRulesType(sr));
            }
        }

        /*
        /// <remarks>min 0, max unbounded</remarks>
        //public List<SpecificityRulesType> SpecificityRules

        /// <summary>
        /// The modification is uniquely identified by references to external CVs such as UNIMOD, see
        /// specification document and mapping file for more details.
        /// </summary>
        /// <remarks>min 1, max unbounded</remarks>
        //public List<CVParamType> CVParams

        /// <summary>True, if the modification is static (i.e. occurs always).</summary>
        /// <remarks>Required Attribute</remarks>
        //public bool FixedMod

        /// <summary>The mass delta of the searched modification in Daltons.</summary>
        /// <remarks>Required Attribute</remarks>
        //public float MassDelta

        /// <remarks>The residue(s) searched with the specified modification. For N or C terminal modifications that can occur
        /// on any residue, the . character should be used to specify any, otherwise the list of amino acids should be provided.</remarks>
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="el"></param>
        public EnzymesType(EnzymeListObj el)
        {
            this.independent = el.Independent;
            this.independentSpecified = el.IndependentSpecified;

            this.Enzyme = null;
            if (el.Enzymes != null && el.Enzymes.Count > 0)
            {
                this.Enzyme = new List<EnzymeType>(el.Enzymes.Count);
                this.Enzyme.AddRange(el.Enzymes, e => new EnzymeType(e));
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="fi"></param>
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="dt"></param>
        public DatabaseTranslationType(DatabaseTranslationObj dt)
        {
            // Default values
            this.TranslationTable = null;
            this.frames = null;

            if (dt.TranslationTables != null && dt.TranslationTables.Count > 0)
            {
                this.TranslationTable = new List<TranslationTableType>(dt.TranslationTables.Count);
                this.TranslationTable.AddRange(dt.TranslationTables, t => new TranslationTableType(t));
            }
            if (dt.Frames != null && dt.Frames.Count > 0)
            {
                this.frames = new List<int>(dt.Frames);
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="pa"></param>
        public ProtocolApplicationType(ProtocolApplicationObj pa) : base(pa)
        {
            this.activityDate = pa.ActivityDate;
            this.activityDateSpecified = pa.ActivityDateSpecified;
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="pd"></param>
        public ProteinDetectionType(ProteinDetectionObj pd) : base(pd)
        {
            this.proteinDetectionList_ref = pd.ProteinDetectionListRef;
            this.proteinDetectionProtocol_ref = proteinDetectionProtocol_ref;

            this.InputSpectrumIdentifications = null;
            if (pd.InputSpectrumIdentifications != null && pd.InputSpectrumIdentifications.Count > 0)
            {
                this.InputSpectrumIdentifications = new List<InputSpectrumIdentificationsType>(pd.InputSpectrumIdentifications.Count);
                this.InputSpectrumIdentifications.AddRange(pd.InputSpectrumIdentifications, isi => new InputSpectrumIdentificationsType(isi));
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="isi"></param>
        public InputSpectrumIdentificationsType(InputSpectrumIdentificationsObj isi)
        {
            this.spectrumIdentificationList_ref = isi.SpectrumIdentificationListRef;
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="si"></param>
        public SpectrumIdentificationType(SpectrumIdentificationObj si) : base(si)
        {
            this.spectrumIdentificationProtocol_ref = si.SpectrumIdentificationProtocolRef;
            this.spectrumIdentificationList_ref = si.SpectrumIdentificationListRef;

            this.InputSpectra = null;
            this.SearchDatabaseRef = null;
            if (si.InputSpectra != null && si.InputSpectra.Count > 0)
            {
                this.InputSpectra = new List<InputSpectraType>(si.InputSpectra.Count);
                this.InputSpectra.AddRange(si.InputSpectra, ispec => new InputSpectraType(ispec));
            }
            if (si.SearchDatabases != null && si.SearchDatabases.Count > 0)
            {
                this.SearchDatabaseRef = new List<SearchDatabaseRefType>(si.SearchDatabases.Count);
                this.SearchDatabaseRef.AddRange(si.SearchDatabases, sdr => new SearchDatabaseRefType(sdr));
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="isr"></param>
        public InputSpectraType(InputSpectraRefObj isr)
        {
            this.spectraData_ref = isr.SpectraDataRef;
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="sdri"></param>
        public SearchDatabaseRefType(SearchDatabaseRefObj sdri)
        {
            this.searchDatabase_ref = sdri.SearchDatabaseRef;
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="pe"></param>
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

        /*//public PeptideEvidenceType()

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
        /// <returns>RegEx: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ?\-]{1}"</returns>
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="p"></param>
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
                this.Modification = new List<ModificationType>(p.Modifications.Count);
                this.Modification.AddRange(p.Modifications, m => new ModificationType(m));
            }
            if (p.SubstitutionModifications != null && p.SubstitutionModifications.Count > 0)
            {
                this.SubstitutionModification = new List<SubstitutionModificationType>(p.SubstitutionModifications.Count);
                this.SubstitutionModification.AddRange(p.SubstitutionModifications, sm => new SubstitutionModificationType(sm));
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="m"></param>
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="sm"></param>
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="dbs"></param>
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="si"></param>
        public SampleType(SampleObj si) : base(si)
        {
            this.cvParam = null;
            this.userParam = null;
            ParamGroupFunctions.CopyParamGroup(this, si);

            this.ContactRole = null;
            this.SubSample = null;
            if (si.ContactRoles != null && si.ContactRoles.Count > 0)
            {
                this.ContactRole = new List<ContactRoleType>(si.ContactRoles.Count);
                this.ContactRole.AddRange(si.ContactRoles, cr => new ContactRoleType(cr));
            }
            if (si.SubSamples != null && si.SubSamples.Count > 0)
            {
                this.SubSample = new List<SubSampleType>(si.SubSamples.Count);
                this.SubSample.AddRange(si.SubSamples, ss => new SubSampleType(ss));
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="cri"></param>
        public ContactRoleType(ContactRoleObj cri)
        {
            this.contact_ref = cri.ContactRef;

            this.Role = null;
            if (cri.Role != null)
            {
                this.Role = new RoleType(cri.Role);
            }
        }

        /*// min 1, max 1
        //public RoleType Role

        /// <summary>When a ContactRole is used, it specifies which Contact the role is associated with.</summary>
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="ri"></param>
        public RoleType(RoleObj ri)
        {
            this.cvParam = null;

            if (ri.CVParam != null)
            {
                this.cvParam = new CVParamType(ri.CVParam);
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="ss"></param>
        public SubSampleType(SubSampleObj ss)
        {
            this.sample_ref = ss.SampleRef;
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="aci"></param>
        protected AbstractContactType(AbstractContactObj aci) : base(aci)
        {
            this.cvParam = null;
            this.userParam = null;
            ParamGroupFunctions.CopyParamGroup(this, aci);
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="o"></param>
        public OrganizationType(OrganizationObj o) : base(o)
        {
            this.Parent = null;
            if (o.Parent != null)
            {
                this.Parent = new ParentOrganizationType(o.Parent);
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="po"></param>
        public ParentOrganizationType(ParentOrganizationObj po)
        {
            this.organization_ref = po.OrganizationRef;
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="pi"></param>
        public PersonType(PersonObj pi) : base(pi)
        {
            this.lastName = pi.LastName;
            this.firstName = pi.FirstName;
            this.midInitials = pi.MidInitials;

            this.Affiliation = null;
            if (pi.Affiliations != null && pi.Affiliations.Count > 0)
            {
                this.Affiliation = new List<AffiliationType>(pi.Affiliations.Count);
                this.Affiliation.AddRange(pi.Affiliations, a => new AffiliationType(a));
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="ai"></param>
        public AffiliationType(AffiliationObj ai)
        {
            this.organization_ref = ai.OrganizationRef;
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="pi"></param>
        public ProviderType(ProviderObj pi) : base(pi)
        {
            this.analysisSoftware_ref = pi.AnalysisSoftwareRef;

            this.ContactRole = null;
            if (pi.ContactRole != null)
            {
                this.ContactRole = new ContactRoleType(pi.ContactRole);
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="asi"></param>
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="ii"></param>
        public InputsType(InputsObj ii)
        {
            // Default values
            this.SourceFile = null;
            this.SearchDatabase = null;
            this.SpectraData = null;

            if (ii.SourceFiles != null && ii.SourceFiles.Count > 0)
            {
                this.SourceFile = new List<SourceFileType>(ii.SourceFiles.Count);
                this.SourceFile.AddRange(ii.SourceFiles, sf => new SourceFileType(sf));
            }
            if (ii.SearchDatabases != null && ii.SearchDatabases.Count > 0)
            {
                this.SearchDatabase = new List<SearchDatabaseType>(ii.SearchDatabases.Count);
                this.SearchDatabase.AddRange(ii.SearchDatabases, sd => new SearchDatabaseType(sd));
            }
            if (ii.SpectraDataList != null && ii.SpectraDataList.Count > 0)
            {
                this.SpectraData = new List<SpectraDataType>(ii.SpectraDataList.Count);
                this.SpectraData.AddRange(ii.SpectraDataList, sd => new SpectraDataType(sd));
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="dc"></param>
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="apc"></param>
        public AnalysisProtocolCollectionType(AnalysisProtocolCollectionObj apc)
        {
            // Default values
            this.SpectrumIdentificationProtocol = null;
            this.ProteinDetectionProtocol = null;

            if (apc.SpectrumIdentificationProtocols != null && apc.SpectrumIdentificationProtocols.Count > 0)
            {
                this.SpectrumIdentificationProtocol = new List<SpectrumIdentificationProtocolType>(apc.SpectrumIdentificationProtocols.Count);
                this.SpectrumIdentificationProtocol.AddRange(apc.SpectrumIdentificationProtocols, sip => new SpectrumIdentificationProtocolType(sip));
            }
            if (apc.ProteinDetectionProtocol != null)
            {
                this.ProteinDetectionProtocol = new ProteinDetectionProtocolType(apc.ProteinDetectionProtocol);
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="ac"></param>
        public AnalysisCollectionType(AnalysisCollectionObj ac)
        {
            // Default values
            this.SpectrumIdentification = null;
            this.ProteinDetection = null;

            if (ac.SpectrumIdentifications != null && ac.SpectrumIdentifications.Count > 0)
            {
                this.SpectrumIdentification = new List<SpectrumIdentificationType>(ac.SpectrumIdentifications.Count);
                this.SpectrumIdentification.AddRange(ac.SpectrumIdentifications, si => new SpectrumIdentificationType(si));
            }
            if (ac.ProteinDetection != null)
            {
                this.ProteinDetection = new ProteinDetectionType(ac.ProteinDetection);
            }
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
        /// Constructor - create from corresponding IdentData type
        /// </summary>
        /// <param name="sc"></param>
        public SequenceCollectionType(SequenceCollectionObj sc)
        {
            // Default values
            this.DBSequence = null;
            this.Peptide = null;
            this.PeptideEvidence = null;

            if (sc.DBSequences != null && sc.DBSequences.Count > 0)
            {
                this.DBSequence = new List<DBSequenceType>(sc.DBSequences.Count);
                this.DBSequence.AddRange(sc.DBSequences, dbs => new DBSequenceType(dbs));
            }
            if (sc.Peptides != null && sc.Peptides.Count > 0)
            {
                this.Peptide = new List<PeptideType>(sc.Peptides.Count);
                this.Peptide.AddRange(sc.Peptides, p => new PeptideType(p));
            }
            if (sc.PeptideEvidences != null && sc.PeptideEvidences.Count > 0)
            {
                this.PeptideEvidence = new List<PeptideEvidenceType>(sc.PeptideEvidences.Count);
                this.PeptideEvidence.AddRange(sc.PeptideEvidences, pe => new PeptideEvidenceType(pe));
            }
        }

        /*
        /// <remarks>min 1, max unbounded (mzIdentML 1.1)</remarks>
        /// <remarks>min 0, max unbounded (mzIdentML 1.2, 0 only valid if additional search params contains "de novo search" cvParam)</remarks>
        //public List<DBSequenceType> DBSequences

        /// <remarks>min 0, max unbounded</remarks>
        //public List<PeptideType> Peptides

        /// <remarks>min 0, max unbounded</remarks>
        //public List<PeptideEvidenceType> PeptideEvidences*/
    }
}
