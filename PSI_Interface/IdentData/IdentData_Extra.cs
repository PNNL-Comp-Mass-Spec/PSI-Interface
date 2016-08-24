﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PSI_Interface.CV;
using PSI_Interface.Utils;

namespace PSI_Interface.IdentData
{
    /// <summary>
    /// MzIdentML MzIdentMLType
    /// </summary>
    /// <remarks>The upper-most hierarchy level of mzIdentML with sub-containers for example describing software, 
    /// protocols and search results (spectrum identifications or protein detection results).</remarks>
    public partial class IdentDataObj : IIdentifiableType
    {
        public IdentDataObj(bool createTranslator = true)
        {
            this._id = null;
            this._name = null;
            this._version = null;
            this._creationDate = System.DateTime.Now;
            this.CreationDateSpecified = false;

            //this.CvTranslator = new CVTranslator(); // Create a generic translator by default; must be re-mapped when reading a file
            this.CvTranslator = null;
            if (createTranslator)
            {
                //this.CvTranslator = new CVTranslator(); // Create a generic translator by default; must be re-mapped when reading a file
                this.DefaultCV(); // Create a generic translator by default; must be re-mapped when reading a file
            }
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
        }

        public void DefaultCV()
        {
            this.CVList = new IdentDataList<CVInfo>();
            foreach (var cv in CV.CV.CVInfoList)
            {
                if (cv.Id.ToLower() == "pato")
                {
                    continue;
                }
                var newcv = new CVInfo();
                newcv.FullName = cv.Name;
                newcv.Id = cv.Id;
                newcv.URI = cv.URI;
                newcv.Version = cv.Version;
                this.CVList.Add(newcv);
            }
            this.CvTranslator = new CVTranslator(this.CVList);
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

        public void RebuildLists()
        {
            // TODO: Not Implementing for now
            //_auditCollection.Clear();
            //_analysisSoftwareList.Clear();
            //_analysisSampleCollection.Clear();
            AnalysisProtocolCollection.RebuildSIPList();
            SequenceCollection.RebuildLists();
            DataCollection.RebuildLists();
        }

        protected internal SpectrumIdentificationItemObj FindSpectrumIdentificationItem(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                foreach (var sil in this.DataCollection.AnalysisData.SpectrumIdentificationList)
                {
                    foreach (var sir in sil.SpectrumIdentificationResults)
                    {
                        var result = sir.SpectrumIdentificationItems.Where(item => item.Id == id).ToList();
                        if (result.Count > 0)
                        {
                            return result.First();
                        }
                    }
                }
            }
            catch // Ignore errors; must resolve reference later...
            { }
            return null;
        }

        protected internal DbSequenceObj FindDbSequence(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                var result = this.SequenceCollection.DBSequences.AsParallel().Where(item => item.Id == id).ToList();
                if (result.Count > 0)
                {
                    return result.First();
                }
            }
            catch // Ignore errors; must resolve reference later...
            { }
            return null;
        }

        protected internal PeptideObj FindPeptide(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                var result = this.SequenceCollection.Peptides.AsParallel().Where(item => item.Id == id).ToList();
                if (result.Count > 0)
                {
                    return result.First();
                }
            }
            catch // Ignore errors; must resolve reference later...
            { }
            return null;
        }

        protected internal PeptideEvidenceObj FindPeptideEvidence(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                var result = this.SequenceCollection.PeptideEvidences.AsParallel().Where(item => item.Id == id).ToList();
                if (result.Count > 0)
                {
                    return result.First();
                }
            }
            catch // Ignore errors; must resolve reference later...
            { }
            return null;
        }

        protected internal MeasureObj FindMeasure(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                foreach (var sil in this.DataCollection.AnalysisData.SpectrumIdentificationList)
                {
                    var result = sil.FragmentationTables.Where(item => item.Id == id).ToList();
                    if (result.Count > 0)
                    {
                        return result.First();
                    }
                }
            }
            catch // Ignore errors; must resolve reference later...
            { }
            return null;
        }

        protected internal MassTableObj FindMassTable(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                foreach (var sip in this.AnalysisProtocolCollection.SpectrumIdentificationProtocols)
                {
                    var result = sip.MassTables.Where(item => item.Id == id).ToList();
                    if (result.Count > 0)
                    {
                        return result.First();
                    }
                }
            }
            catch // Ignore errors; must resolve reference later...
            { }
            return null;
        }

        protected internal SampleObj FindSample(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                var result = this.AnalysisSampleCollection.Where(item => item.Id == id).ToList();
                if (result.Count > 0)
                {
                    return result.First();
                }
            }
            catch // Ignore errors; must resolve reference later...
            { }
            return null;
        }

        protected internal AnalysisSoftwareObj FindAnalysisSoftware(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                var result = this.AnalysisSoftwareList.Where(item => item.Id == id).ToList();
                if (result.Count > 0)
                {
                    return result.First();
                }
            }
            catch // Ignore errors; must resolve reference later...
            { }
            return null;
        }

        protected internal OrganizationObj FindOrganization(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                var result = this.AuditCollection.Where(item => item.Id == id).ToList();
                if (result.Count > 0)
                {
                    foreach (var item in result)
                    {
                        var o = item as OrganizationObj;
                        if (o != null)
                        {
                            return o;
                        }
                    }
                }
            }
            catch // Ignore errors; must resolve reference later...
            { }
            return null;
        }

        protected internal AbstractContactObj FindContact(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                var result = this.AuditCollection.Where(item => item.Id == id).ToList();
                if (result.Count > 0)
                {
                    return result.First();
                }
            }
            catch // Ignore errors; must resolve reference later...
            { }
            return null;
        }

        protected internal SearchDatabaseInfo FindSearchDatabase(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                var result = this.DataCollection.Inputs.SearchDatabases.Where(item => item.Id == id).ToList();
                if (result.Count > 0)
                {
                    return result.First();
                }
            }
            catch // Ignore errors; must resolve reference later...
            { }
            return null;
        }

        protected internal SpectraDataObj FindSpectraData(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                var result = this.DataCollection.Inputs.SpectraDataList.Where(item => item.Id == id).ToList();
                if (result.Count > 0)
                {
                    return result.First();
                }
            }
            catch // Ignore errors; must resolve reference later...
            { }
            return null;
        }

        protected internal SpectrumIdentificationListObj FindSpectrumIdentificationList(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                var result = this.DataCollection.AnalysisData.SpectrumIdentificationList.Where(item => item.Id == id).ToList();
                if (result.Count > 0)
                {
                    return result.First();
                }
            }
            catch // Ignore errors; must resolve reference later...
            { }
            return null;
        }

        protected internal SpectrumIdentificationProtocolObj FindSpectrumIdentificationProtocol(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                var result = this.AnalysisProtocolCollection.SpectrumIdentificationProtocols.Where(item => item.Id == id).ToList();
                if (result.Count > 0)
                {
                    return result.First();
                }
            }
            catch // Ignore errors; must resolve reference later...
            { }
            return null;
        }

        protected internal ProteinDetectionProtocolObj FindProteinDetectionProtocol(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                if (this.AnalysisProtocolCollection.ProteinDetectionProtocol.Id == id)
                {
                    return this.AnalysisProtocolCollection.ProteinDetectionProtocol;
                }
            }
            catch // Ignore errors; must resolve reference later...
            { }
            return null;
        }

        protected internal ProteinDetectionListObj FindProteinDetectionList(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                if (this.DataCollection.AnalysisData.ProteinDetectionList.Id == id)
                {
                    return this.DataCollection.AnalysisData.ProteinDetectionList;
                }
            }
            catch // Ignore errors; must resolve reference later...
            { }
            return null;
        }

        protected internal TranslationTableObj FindTranslationTable(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                foreach (var sip in this.AnalysisProtocolCollection.SpectrumIdentificationProtocols)
                {
                    var result = sip.DatabaseTranslation.TranslationTables.Where(item => item.Id == id).ToList();
                    if (result.Count > 0)
                    {
                        return result.First();
                    }
                }
            }
            catch // Ignore errors; must resolve reference later...
            { }
            return null;
        }

        public override bool Equals(object other)
        {
            var o = other as IdentDataObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(IdentDataObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && this.Version == other.Version && Equals(this.CVList, other.CVList) &&
                Equals(this.AnalysisSoftwareList, other.AnalysisSoftwareList) && Equals(this.Provider, other.Provider) &&
                Equals(this.AuditCollection, other.AuditCollection) &&
                Equals(this.AnalysisSampleCollection, other.AnalysisSampleCollection) &&
                Equals(this.SequenceCollection, other.SequenceCollection) &&
                Equals(this.AnalysisCollection, other.AnalysisCollection) &&
                Equals(this.AnalysisProtocolCollection, other.AnalysisProtocolCollection) &&
                Equals(this.DataCollection, other.DataCollection) &&
                Equals(this.BibliographicReferences, other.BibliographicReferences))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (CVList != null ? CVList.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AnalysisSoftwareList != null ? AnalysisSoftwareList.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Provider != null ? Provider.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AuditCollection != null ? AuditCollection.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AnalysisSampleCollection != null ? AnalysisSampleCollection.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SequenceCollection != null ? SequenceCollection.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AnalysisCollection != null ? AnalysisCollection.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AnalysisProtocolCollection != null ? AnalysisProtocolCollection.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DataCollection != null ? DataCollection.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (BibliographicReferences != null ? BibliographicReferences.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ CreationDate.GetHashCode();
                hashCode = (hashCode * 397) ^ (Version != null ? Version.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Id != null ? Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                return hashCode;
            }
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
        public CVInfo()
        {
            this.FullName = null;
            this.Version = null;
            this.URI = null;
            this.Id = null;
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

        public override bool Equals(object other)
        {
            var o = other as CVInfo;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(CVInfo other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.FullName == other.FullName && this.Version == other.Version && this.URI == other.URI)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (FullName != null ? FullName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Version != null ? Version.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (URI != null ? URI.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationItemRefType
    /// </summary>
    /// <remarks>Reference(s) to the SpectrumIdentificationItem element(s) that support the given PeptideEvidence element. 
    /// Using these references it is possible to indicate which spectra were actually accepted as evidence for this 
    /// peptide identification in the given protein.</remarks>
    public partial class SpectrumIdentificationItemRefObj : IdentDataInternalTypeAbstract
    {
        public SpectrumIdentificationItemRefObj()
        {
            this._spectrumIdentificationItemRef = null;

            this._spectrumIdentificationItem = null;
        }

        /// <remarks>A reference to the SpectrumIdentificationItem element(s).</remarks>
        /// Required Attribute
        /// string
        //public string SpectrumIdentificationItemRef

        public override bool Equals(object other)
        {
            var o = other as SpectrumIdentificationItemRefObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(SpectrumIdentificationItemRefObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.SpectrumIdentificationItem, other.SpectrumIdentificationItem))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (SpectrumIdentificationItem != null ? SpectrumIdentificationItem.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML PeptideHypothesisType
    /// </summary>
    /// <remarks>Peptide evidence on which this ProteinHypothesis is based by reference to a PeptideEvidence element.</remarks>
    public partial class PeptideHypothesisObj : IdentDataInternalTypeAbstract
    {
        public PeptideHypothesisObj()
        {
            this._peptideEvidenceRef = null;

            this._peptideEvidence = null;
            this._spectrumIdentificationItems = null;
        }

        /// min 1, max unbounded
        //public IdentDataList<SpectrumIdentificationItemRefInfo> SpectrumIdentificationItemRef

        /// <remarks>A reference to the PeptideEvidence element on which this hypothesis is based.</remarks>
        /// Required Attribute
        /// string
        //public string PeptideEvidenceRef

        public override bool Equals(object other)
        {
            var o = other as PeptideHypothesisObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(PeptideHypothesisObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.PeptideEvidence, other.PeptideEvidence) &&
                Equals(this.SpectrumIdentificationItems, other.SpectrumIdentificationItems))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (PeptideEvidence != null ? PeptideEvidence.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SpectrumIdentificationItems != null ? SpectrumIdentificationItems.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML FragmentArrayType
    /// </summary>
    /// <remarks>An array of values for a given type of measure and for a particular ion type, in parallel to the index of ions identified.</remarks>
    public partial class FragmentArrayObj : IdentDataInternalTypeAbstract
    {
        public FragmentArrayObj()
        {
            this._measureRef = null;

            this._measure = null;
            this._values = null;
        }

        /// <remarks>The values of this particular measure, corresponding to the index defined in ion type</remarks>
        /// Required Attribute
        /// listOfFloats: string, space-separated floats
        //public List<float> Values

        /// <remarks>A reference to the Measure defined in the FragmentationTable</remarks>
        /// Required Attribute
        /// string
        //public string MeasureRef

        public override bool Equals(object other)
        {
            var o = other as FragmentArrayObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(FragmentArrayObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.Measure, other.Measure) && Equals(this.Values, other.Values))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Measure != null ? Measure.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Values != null ? Values.GetHashCode() : 0);
                return hashCode;
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
        public IonTypeObj()
        {
            this._charge = 0;

            this._fragmentArrays = null;
            this._cvParam = null;
            this._index = null;
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

        public override bool Equals(object other)
        {
            var o = other as IonTypeObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(IonTypeObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Charge == other.Charge && Equals(this.FragmentArrays, other.FragmentArrays) &&
                Equals(this.CVParam, other.CVParam) && Equals(this.Index, other.Index))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Charge;
                hashCode = (hashCode * 397) ^ (FragmentArrays != null ? FragmentArrays.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Index != null ? Index.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParam != null ? CVParam.GetHashCode() : 0);
                return hashCode;
            }
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
        public CVParamObj()
        {
            this._cvRef = null;
            //this._name = null;
            //this._accession = null;
            this._value = null;

            this._cvid = CV.CV.CVID.CVID_Unknown;
        }

        public CVParamObj(CV.CV.CVID cvid, string value)
        {
            this._cvRef = null;
            //this._name = null;
            //this._accession = null;
            this._value = value;

            this._cvid = cvid;
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

        public T ValueAs<T>() where T : IConvertible
        {
            return (T) Convert.ChangeType(Value, typeof (T));
        }

        public override bool Equals(object other)
        {
            var o = other as CVParamObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(CVParamObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Cvid == other.Cvid && this.Value == other.Value &&
                this.UnitCvid == other.UnitCvid)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Cvid.GetHashCode();
                hashCode = (hashCode * 397) ^ (Value != null ? Value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ UnitCvid.GetHashCode();
                return hashCode;
            }
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
        public ParamBaseObj()
        {
            //this._unitAccession = null;
            //this._unitName = null;
            this._unitCvRef = null;
            this._unitsSet = false;

            this._unitCvid = CV.CV.CVID.CVID_Unknown;
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

        public override bool Equals(object other)
        {
            var o = other as ParamBaseObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(ParamBaseObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && this.UnitCvid == other.UnitCvid)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ UnitCvid.GetHashCode();
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML UserParamType
    /// </summary>
    /// <remarks>A single user-defined parameter.</remarks>
    public partial class UserParamObj : ParamBaseObj
    {
        public UserParamObj()
        {
            this._name = null;
            this._value = null;
            this._type = null;
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

        public override bool Equals(object other)
        {
            var o = other as UserParamObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(UserParamObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && this.Value == other.Value && this.Type == other.Type &&
                this.UnitCvid == other.UnitCvid)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Value != null ? Value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Type != null ? Type.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ UnitCvid.GetHashCode();
                return hashCode;
            }
        }
    }

    public partial class CVParamGroupObj : IdentDataInternalTypeAbstract
    {
        public CVParamGroupObj()
        {
            this._cvParams = null;
        }

        //public IdentDataList<CVParam> CVParams

        public override bool Equals(object other)
        {
            var o = other as CVParamGroupObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(CVParamGroupObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.CVParams, other.CVParams))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (CVParams != null ? CVParams.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    public partial class ParamGroupObj : CVParamGroupObj
    {
        public ParamGroupObj()
        {
            this._userParams = null;
        }

        //public IdentDataList<UserParam> UserParams

        public override bool Equals(object other)
        {
            var o = other as ParamGroupObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(ParamGroupObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.CVParams, other.CVParams) && Equals(this.UserParams, other.UserParams))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML ParamType
    /// </summary>
    /// <remarks>Helper type to allow either a cvParam or a userParam to be provided for an element.</remarks>
    public partial class ParamObj : IdentDataInternalTypeAbstract
    {
        public ParamObj()
        {
            this._item = null;
        }

        /// min 1, max 1
        //public ParamBase Item

        public override bool Equals(object other)
        {
            var o = other as ParamObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(ParamObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.Item, other.Item))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Item != null ? Item.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML ParamListType
    /// </summary>
    /// <remarks>Helper type to allow multiple cvParams or userParams to be given for an element.</remarks>
    public partial class ParamListObj : IdentDataInternalTypeAbstract
    {
        public ParamListObj()
        {
            this._items = null;
        }

        /// min 1, max unbounded
        //public IdentDataList<ParamBase> Items

        public override bool Equals(object other)
        {
            var o = other as ParamListObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(ParamListObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.Items, other.Items))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Items != null ? Items.GetHashCode() : 0);
                return hashCode;
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
        public PeptideEvidenceRefObj()
        {
            this._peptideEvidenceRef = null;

            this._peptideEvidence = null;
        }

        /// <remarks>A reference to the PeptideEvidenceItem element(s).</remarks>
        /// Required Attribute
        /// string
        //public string PeptideEvidenceRef

        public override bool Equals(object other)
        {
            var o = other as PeptideEvidenceRefObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(PeptideEvidenceRefObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.PeptideEvidence, other.PeptideEvidence))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (PeptideEvidence != null ? PeptideEvidence.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML AnalysisDataType
    /// </summary>
    /// <remarks>Data sets generated by the analyses, including peptide and protein lists.</remarks>
    public partial class AnalysisDataObj : IdentDataInternalTypeAbstract
    {
        public AnalysisDataObj()
        {
            this._spectrumIdentificationList = null;
            this._proteinDetectionList = null;
        }

        //private long _idCounter = 0;

        /// min 1, max unbounded
        //public IdentDataList<SpectrumIdentificationList> SpectrumIdentificationList

        /// min 0, max 1
        //public ProteinDetectionList ProteinDetectionList

        internal void RebuildLists()
        {
            // Don't do anything; we'll assume this data is correct, to avoid losing any data.
            //_idCounter = 0;
            // Don't clear; primary data container
            //_spectrumIdentificationList.Clear();
        }

        public override bool Equals(object other)
        {
            var o = other as AnalysisDataObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(AnalysisDataObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.SpectrumIdentificationList, other.SpectrumIdentificationList) &&
                Equals(this.ProteinDetectionList, other.ProteinDetectionList))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (SpectrumIdentificationList != null ? SpectrumIdentificationList.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ProteinDetectionList != null ? ProteinDetectionList.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationListType
    /// </summary>
    /// <remarks>Represents the set of all search results from SpectrumIdentification.</remarks>
    public partial class SpectrumIdentificationListObj : ParamGroupObj, IIdentifiableType
    {
        public SpectrumIdentificationListObj()
        {
            this._id = null;
            this._name = null;
            this._numSequencesSearched = -1;
            this.NumSequencesSearchedSpecified = false;

            this._fragmentationTables = null;
            this._spectrumIdentificationResults = null;
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

        public void Sort()
        {
            foreach (var sir in this.SpectrumIdentificationResults)
            {
                sir.Sort();
            }
            this.SpectrumIdentificationResults.Sort((a, b) => 
                a.BestSpecEVal().CompareTo(b.BestSpecEVal()));
        }

        public override bool Equals(object other)
        {
            var o = other as SpectrumIdentificationListObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(SpectrumIdentificationListObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && this.NumSequencesSearched == other.NumSequencesSearched &&
                Equals(this.FragmentationTables, other.FragmentationTables) &&
                Equals(this.SpectrumIdentificationResults, other.SpectrumIdentificationResults) &&
                Equals(this.CVParams, other.CVParams) && Equals(this.UserParams, other.UserParams))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ NumSequencesSearched.GetHashCode();
                hashCode = (hashCode * 397) ^ (FragmentationTables != null ? FragmentationTables.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SpectrumIdentificationResults != null ? SpectrumIdentificationResults.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }
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
        public MeasureObj()
        {
            this._id = null;
            this._name = null;
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

        public override bool Equals(object other)
        {
            var o = other as MeasureObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(MeasureObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && Equals(this.CVParams, other.CVParams))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                return hashCode;
            }
        }
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
        public BibliographicReferenceObj()
        {
            this._year = 1990;
            this.YearSpecified = false;
            this._id = null;
            this._name = null;
            this.Authors = null;
            this.Publication = null;
            this.Publisher = null;
            this.Editor = null;
            this.Volume = null;
            this.Issue = null;
            this.Pages = null;
            this.Title = null;
            this.DOI = null;
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

        public override bool Equals(object other)
        {
            var o = other as BibliographicReferenceObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(BibliographicReferenceObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && this.Authors == other.Authors && this.DOI == other.DOI &&
                this.Year == other.Year && this.Publication == other.Publication && this.Publisher == other.Publisher &&
                this.Editor == other.Editor && this.Pages == other.Pages && this.Title == other.Title)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Authors != null ? Authors.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DOI != null ? DOI.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Year;
                hashCode = (hashCode * 397) ^ (Publication != null ? Publication.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Publisher != null ? Publisher.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Editor != null ? Editor.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Pages != null ? Pages.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Title != null ? Title.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML ProteinDetectionHypothesisType
    /// </summary>
    /// <remarks>A single result of the ProteinDetection analysis (i.e. a protein).</remarks>
    public partial class ProteinDetectionHypothesisObj : ParamGroupObj, IIdentifiableType
    {
        public ProteinDetectionHypothesisObj()
        {
            this._id = null;
            this._name = null;
            this._dBSequenceRef = null;
            this._passThreshold = false;

            this._dBSequence = null;
            this._peptideHypotheses = null;
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

        public override bool Equals(object other)
        {
            var o = other as ProteinDetectionHypothesisObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(ProteinDetectionHypothesisObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && this.PassThreshold == other.PassThreshold &&
                Equals(this.DBSequence, other.DBSequence) && Equals(this.PeptideHypotheses, other.PeptideHypotheses) &&
                Equals(this.CVParams, other.CVParams) && Equals(this.UserParams, other.UserParams))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ PassThreshold.GetHashCode();
                hashCode = (hashCode * 397) ^ (DBSequence != null ? DBSequence.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PeptideHypotheses != null ? PeptideHypotheses.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML ProteinAmbiguityGroupType
    /// </summary>
    /// <remarks>A set of logically related results from a protein detection, for example to represent conflicting assignments of peptides to proteins.</remarks>
    public partial class ProteinAmbiguityGroupObj : ParamGroupObj, IIdentifiableType
    {
        public ProteinAmbiguityGroupObj()
        {
            this._id = null;
            this._name = null;

            this._proteinDetectionHypotheses = null;
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

        public override bool Equals(object other)
        {
            var o = other as ProteinAmbiguityGroupObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(ProteinAmbiguityGroupObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && Equals(this.ProteinDetectionHypotheses, other.ProteinDetectionHypotheses) &&
                Equals(this.CVParams, other.CVParams) && Equals(this.UserParams, other.UserParams))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ProteinDetectionHypotheses != null ? ProteinDetectionHypotheses.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML ProteinDetectionListType
    /// </summary>
    /// <remarks>The protein list resulting from a protein detection process.</remarks>
    public partial class ProteinDetectionListObj : ParamGroupObj, IIdentifiableType
    {
        public ProteinDetectionListObj()
        {
            this._id = null;
            this._name = null;

            this._proteinAmbiguityGroups = null;
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

        public override bool Equals(object other)
        {
            var o = other as ProteinDetectionListObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(ProteinDetectionListObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && Equals(this.ProteinAmbiguityGroups, other.ProteinAmbiguityGroups) &&
                Equals(this.CVParams, other.CVParams) && Equals(this.UserParams, other.UserParams))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ProteinAmbiguityGroups != null ? ProteinAmbiguityGroups.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationItemType
    /// </summary>
    /// <remarks>An identification of a single (poly)peptide, resulting from querying an input spectra, along with 
    /// the set of confidence values for that identification. PeptideEvidence elements should be given for all 
    /// mappings of the corresponding Peptide sequence within protein sequences.</remarks>
    public partial class SpectrumIdentificationItemObj : ParamGroupObj, IIdentifiableType
    {
        public SpectrumIdentificationItemObj()
        {
            this._id = null;
            this._name = null;
            this._chargeState = 0;
            this._experimentalMassToCharge = -1;
            this._calculatedMassToCharge = -1;
            this.CalculatedMassToChargeSpecified = false;
            this._calculatedPI = -1;
            this.CalculatedPISpecified = false;
            this._peptideRef = null;
            this._rank = -1;
            this._passThreshold = false;
            this._massTableRef = null;
            this._sampleRef = null;

            this._peptide = null;
            this._massTable = null;
            this._sample = null;
            this._peptideEvidences = null;
            this._fragmentations = null;
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

        public double GetSpecEValue()
        {
            return this.CVParams.GetCvParam(CV.CV.CVID.MS_MS_GF_SpecEValue, "1").ValueAs<double>();
        }

        public override bool Equals(object other)
        {
            var o = other as SpectrumIdentificationItemObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(SpectrumIdentificationItemObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && this.ChargeState == other.ChargeState &&
                this.ExperimentalMassToCharge.Equals(other.ExperimentalMassToCharge) &&
                this.CalculatedMassToCharge.Equals(other.CalculatedMassToCharge) &&
                this.CalculatedPI.Equals(other.CalculatedPI) && this.Rank == other.Rank &&
                this.PassThreshold == other.PassThreshold && Equals(this.Peptide, other.Peptide) &&
                Equals(this.MassTable, other.MassTable) && Equals(this.Sample, other.Sample) &&
                Equals(this.PeptideEvidences, other.PeptideEvidences) &&
                Equals(this.Fragmentations, other.Fragmentations) && Equals(this.CVParams, other.CVParams) &&
                Equals(this.UserParams, other.UserParams))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ ChargeState;
                hashCode = (hashCode * 397) ^ ExperimentalMassToCharge.GetHashCode();
                hashCode = (hashCode * 397) ^ CalculatedMassToCharge.GetHashCode();
                hashCode = (hashCode * 397) ^ CalculatedPI.GetHashCode();
                hashCode = (hashCode * 397) ^ Rank;
                hashCode = (hashCode * 397) ^ PassThreshold.GetHashCode();
                hashCode = (hashCode * 397) ^ (Peptide != null ? Peptide.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (MassTable != null ? MassTable.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Sample != null ? Sample.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PeptideEvidences != null ? PeptideEvidences.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Fragmentations != null ? Fragmentations.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
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
        public SpectrumIdentificationResultObj()
        {
            this._id = null;
            this._name = null;
            this._spectrumID = null;
            this._spectraDataRef = null;

            this._spectraData = null;
            this._spectrumIdentificationItems = null;
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

        /// <summary>
        /// Sort the SpectrumIdentificationItems by rank, ascending
        /// </summary>
        public void Sort()
        {
            this.SpectrumIdentificationItems.Sort((a, b) => a.Rank.CompareTo(b.Rank));
        }

        /// <summary>
        /// The lowest specEvalue in the SpectrumIdentificationItems
        /// </summary>
        /// <returns></returns>
        public double BestSpecEVal()
        {
            this.Sort();
            return this.SpectrumIdentificationItems.First().GetSpecEValue();
        }

        /// <summary>
        /// Changes the rank so that items are ranked by specEValue, ascending
        /// Also re-writes the ids
        /// </summary>
        public void ReRankBySpecEValue()
        {
            var siiIdBase = this.Id.ToUpper().Replace("SIR", "SII") + "_";
            this.SpectrumIdentificationItems.Sort((a, b) => a.GetSpecEValue().CompareTo(b.GetSpecEValue()));
            for (var i = 0; i < this.SpectrumIdentificationItems.Count; i++)
            {
                var rank = i + 1;
                this.SpectrumIdentificationItems[i].Rank = rank;
                this.SpectrumIdentificationItems[i].Id = siiIdBase + rank;
            }
            this.Sort();
        }

        /// <summary>
        /// Remove all SpectrumIdentificationItems that have a specEValue greater than the best specEValue in the list.
        /// </summary>
        public void RemoveMatchesNotBestSpecEValue()
        {
            this.ReRankBySpecEValue();
            var best = this.SpectrumIdentificationItems.First().GetSpecEValue();
            this.SpectrumIdentificationItems.RemoveAll(item => item.GetSpecEValue() > best);
        }

        public override bool Equals(object other)
        {
            var o = other as SpectrumIdentificationResultObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(SpectrumIdentificationResultObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && this.SpectrumID == other.SpectrumID &&
                Equals(this.SpectrumIdentificationItems, other.SpectrumIdentificationItems) &&
                Equals(this.SpectraData, other.SpectraData) && Equals(this.CVParams, other.CVParams) &&
                Equals(this.UserParams, other.UserParams))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SpectrumID != null ? SpectrumID.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SpectrumIdentificationItems != null ? SpectrumIdentificationItems.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SpectraData != null ? SpectraData.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }
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
        public FileFormatInfo()
        {
            this._cvParam = null;
        }

        /// <remarks>cvParam capturing file formats</remarks>
        /// Optional Attribute
        /// min 1, max 1
        //public CVParam CVParam

        public override bool Equals(object other)
        {
            var o = other as FileFormatInfo;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(FileFormatInfo other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.CVParam, other.CVParam))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (CVParam != null ? CVParam.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML SpectraDataType
    /// </summary>
    /// <remarks>A data set containing spectra data (consisting of one or more spectra).</remarks>
    public partial class SpectraDataObj : IdentDataInternalTypeAbstract, IExternalDataType
    {
        public SpectraDataObj()
        {
            this._id = null;
            this._name = null;
            this._externalFormatDocumentation = null;
            this._location = null;

            this._spectrumIDFormat = null;
            this._fileFormat = null;
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

        public override bool Equals(object other)
        {
            var o = other as SpectraDataObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(SpectraDataObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && this.ExternalFormatDocumentation == other.ExternalFormatDocumentation &&
                Path.GetFileName(this.Location) == Path.GetFileName(other.Location) && Equals(this.SpectrumIDFormat, other.SpectrumIDFormat) &&
                Equals(this.FileFormat, other.FileFormat))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ExternalFormatDocumentation != null ? ExternalFormatDocumentation.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Location != null ? Location.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SpectrumIDFormat != null ? SpectrumIDFormat.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FileFormat != null ? FileFormat.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML SpectrumIDFormatType
    /// </summary>
    /// <remarks>The format of the spectrum identifier within the source file</remarks>
    public partial class SpectrumIDFormatObj : IdentDataInternalTypeAbstract
    {
        public SpectrumIDFormatObj()
        {
            this._cvParam = null;
        }

        /// <remarks>CV term capturing the type of identifier used.</remarks>
        /// min 1, max 1
        //public CVParam CVParam

        public override bool Equals(object other)
        {
            var o = other as SpectrumIDFormatObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(SpectrumIDFormatObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.CVParam, other.CVParam))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (CVParam != null ? CVParam.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML SourceFileType
    /// </summary>
    /// <remarks>A file from which this mzIdentML instance was created.</remarks>
    public partial class SourceFileInfo : ParamGroupObj, IExternalDataType
    {
        public SourceFileInfo()
        {
            this._id = null;
            this._name = null;
            this._externalFormatDocumentation = null;
            this._location = null;

            this._fileFormat = null;
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

        public override bool Equals(object other)
        {
            var o = other as SourceFileInfo;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(SourceFileInfo other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && this.ExternalFormatDocumentation == other.ExternalFormatDocumentation &&
                Path.GetFileName(this.Location) == Path.GetFileName(other.Location) && Equals(this.FileFormat, other.FileFormat) &&
                Equals(this.CVParams, other.CVParams) && Equals(this.UserParams, other.UserParams))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ExternalFormatDocumentation != null ? ExternalFormatDocumentation.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Location != null ? Location.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FileFormat != null ? FileFormat.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML SearchDatabaseType
    /// </summary>
    /// <remarks>A database for searching mass spectra. Examples include a set of amino acid sequence entries, or annotated spectra libraries.</remarks>
    public partial class SearchDatabaseInfo : CVParamGroupObj, IExternalDataType
    {
        public SearchDatabaseInfo()
        {
            this._id = null;
            this._name = null;
            this._version = null;
            this._releaseDate = System.DateTime.Now;
            this.ReleaseDateSpecified = false;
            this._numDatabaseSequences = -1;
            this.NumDatabaseSequencesSpecified = false;
            this._numResidues = -1;
            this.NumResiduesSpecified = false;
            this._externalFormatDocumentation = null;
            this._location = null;

            this._databaseName = null;
            this._fileFormat = null;
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

        public override bool Equals(object other)
        {
            var o = other as SearchDatabaseInfo;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(SearchDatabaseInfo other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && this.Version == other.Version &&
                this.NumDatabaseSequences == other.NumDatabaseSequences && this.NumResidues == other.NumResidues &&
                this.ExternalFormatDocumentation == other.ExternalFormatDocumentation &&
                Path.GetFileName(this.Location) == Path.GetFileName(other.Location) &&
                Equals(this.DatabaseName, other.DatabaseName) && Equals(this.FileFormat, other.FileFormat) &&
                Equals(this.CVParams, other.CVParams))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Version != null ? Version.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ NumDatabaseSequences.GetHashCode();
                hashCode = (hashCode * 397) ^ NumResidues.GetHashCode();
                hashCode = (hashCode * 397) ^ (ExternalFormatDocumentation != null ? ExternalFormatDocumentation.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Location != null ? Location.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DatabaseName != null ? DatabaseName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FileFormat != null ? FileFormat.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML ProteinDetectionProtocolType
    /// </summary>
    /// <remarks>The parameters and settings of a ProteinDetection process.</remarks>
    public partial class ProteinDetectionProtocolObj : IdentDataInternalTypeAbstract, IIdentifiableType
    {
        public ProteinDetectionProtocolObj()
        {
            this._id = null;
            this._name = null;
            this._analysisSoftwareRef = null;

            this._analysisSoftware = null;
            this._analysisParams = null;
            this._threshold = null;
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

        public override bool Equals(object other)
        {
            var o = other as ProteinDetectionProtocolObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(ProteinDetectionProtocolObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && Equals(this.AnalysisSoftware, other.AnalysisSoftware) &&
                Equals(this.AnalysisParams, other.AnalysisParams) && Equals(this.Threshold, other.Threshold))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AnalysisSoftware != null ? AnalysisSoftware.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AnalysisParams != null ? AnalysisParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Threshold != null ? Threshold.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML TranslationTableType
    /// </summary>
    /// <remarks>The table used to translate codons into nucleic acids e.g. by reference to the NCBI translation table.</remarks>
    public partial class TranslationTableObj : CVParamGroupObj, IIdentifiableType
    {
        public TranslationTableObj()
        {
            this._id = null;
            this._name = null;
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

        public override bool Equals(object other)
        {
            var o = other as TranslationTableObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(TranslationTableObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && Equals(this.CVParams, other.CVParams))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML MassTableType
    /// </summary>
    /// <remarks>The masses of residues used in the search.</remarks>
    public partial class MassTableObj : ParamGroupObj, IIdentifiableType
    {
        public MassTableObj()
        {
            this._id = null;
            this._name = null;

            this._residues = null;
            this._ambiguousResidues = null;
            this._msLevels = null;
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

        public override bool Equals(object other)
        {
            var o = other as MassTableObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(MassTableObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && Equals(this.Residues, other.Residues) &&
                Equals(this.AmbiguousResidues, other.AmbiguousResidues) &&
                ListUtils.ListEqualsUnOrdered(this.MsLevels, other.MsLevels) && Equals(this.CVParams, other.CVParams) &&
                Equals(this.UserParams, other.UserParams))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Residues != null ? Residues.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AmbiguousResidues != null ? AmbiguousResidues.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (MsLevels != null ? MsLevels.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML ResidueType
    /// </summary>
    public partial class ResidueObj : IdentDataInternalTypeAbstract
    {
        public ResidueObj()
        {
            this._code = null;
            this._mass = -1;
        }

        /// <remarks>The single letter code for the residue.</remarks>
        /// Required Attribute
        /// chars, string, regex: "[ABCDEFGHIJKLMNOPQRSTUVWXYZ]{1}"
        //public string Code

        /// <remarks>The residue mass in Daltons (not including any fixed modifications).</remarks>
        /// Required Attribute
        /// float
        //public float Mass

        public override bool Equals(object other)
        {
            var o = other as ResidueObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(ResidueObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Code == other.Code && this.Mass.Equals(other.Mass))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Code != null ? Code.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Mass.GetHashCode();
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML AmbiguousResidueType
    /// </summary>
    /// <remarks>Ambiguous residues e.g. X can be specified by the Code attribute and a set of parameters 
    /// for example giving the different masses that will be used in the search.</remarks>
    public partial class AmbiguousResidueObj : ParamGroupObj
    {
        public AmbiguousResidueObj()
        {
            this._code = null;
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

        public override bool Equals(object other)
        {
            var o = other as AmbiguousResidueObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(AmbiguousResidueObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Code == other.Code && Equals(this.CVParams, other.CVParams) &&
                Equals(this.UserParams, other.UserParams))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Code != null ? Code.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML EnzymeType
    /// </summary>
    /// <remarks>The details of an individual cleavage enzyme should be provided by giving a regular expression 
    /// or a CV term if a "standard" enzyme cleavage has been performed.</remarks>
    public partial class EnzymeObj : IdentDataInternalTypeAbstract, IIdentifiableType
    {
        public EnzymeObj()
        {
            this._id = null;
            this._name = null;
            this._siteRegexp = null;
            this._nTermGain = null;
            this._cTermGain = null;
            this._semiSpecific = false;
            this.SemiSpecificSpecified = false;
            this._missedCleavages = -1;
            this.MissedCleavagesSpecified = false;
            this._minDistance = -1;
            this.MinDistanceSpecified = false;

            this._enzymeName = null;
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

        public override bool Equals(object other)
        {
            var o = other as EnzymeObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(EnzymeObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && this.SiteRegexp == other.SiteRegexp && this.NTermGain == other.NTermGain &&
                this.CTermGain == other.CTermGain && this.SemiSpecific == other.SemiSpecific &&
                this.MissedCleavages == other.MissedCleavages && this.MinDistance == other.MinDistance &&
                Equals(this.EnzymeName, other.EnzymeName))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SiteRegexp != null ? SiteRegexp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (NTermGain != null ? NTermGain.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CTermGain != null ? CTermGain.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ SemiSpecific.GetHashCode();
                hashCode = (hashCode * 397) ^ MissedCleavages.GetHashCode();
                hashCode = (hashCode * 397) ^ MinDistance.GetHashCode();
                hashCode = (hashCode * 397) ^ (EnzymeName != null ? EnzymeName.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML SpectrumIdentificationProtocolType
    /// </summary>
    /// <remarks>The parameters and settings of a SpectrumIdentification analysis.</remarks>
    public partial class SpectrumIdentificationProtocolObj : IdentDataInternalTypeAbstract, IIdentifiableType
    {
        public SpectrumIdentificationProtocolObj()
        {
            this._id = null;
            this._name = null;
            this._analysisSoftwareRef = null;

            this._analysisSoftware = null;
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

        public override bool Equals(object other)
        {
            var o = other as SpectrumIdentificationProtocolObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(SpectrumIdentificationProtocolObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && Equals(this.AnalysisSoftware, other.AnalysisSoftware) &&
                Equals(this.SearchType, other.SearchType) &&
                Equals(this.AdditionalSearchParams, other.AdditionalSearchParams) &&
                Equals(this.MassTables, other.MassTables) && Equals(this.ModificationParams, other.ModificationParams) &&
                Equals(this.Enzymes, other.Enzymes) && Equals(this.FragmentTolerances, other.FragmentTolerances) &&
                Equals(this.ParentTolerances, other.ParentTolerances) && Equals(this.Threshold, other.Threshold) &&
                Equals(this.DatabaseFilters, other.DatabaseFilters) &&
                Equals(this.DatabaseTranslation, other.DatabaseTranslation))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AnalysisSoftware != null ? AnalysisSoftware.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SearchType != null ? SearchType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AdditionalSearchParams != null ? AdditionalSearchParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (MassTables != null ? MassTables.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ModificationParams != null ? ModificationParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Enzymes != null ? Enzymes.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FragmentTolerances != null ? FragmentTolerances.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ParentTolerances != null ? ParentTolerances.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Threshold != null ? Threshold.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DatabaseFilters != null ? DatabaseFilters.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DatabaseTranslation != null ? DatabaseTranslation.GetHashCode() : 0);
                return hashCode;
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
        public SpecificityRulesListObj()
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
        public SearchModificationObj()
        {
            this._fixedMod = false;
            this._massDelta = 0;
            this._residues = null;

            this._specificityRules = null;
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

        public override bool Equals(object other)
        {
            var o = other as SearchModificationObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(SearchModificationObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.FixedMod == other.FixedMod && this.MassDelta.Equals(other.MassDelta) &&
                this.Residues == other.Residues && Equals(this.SpecificityRules, other.SpecificityRules) &&
                Equals(this.CVParams, other.CVParams))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = FixedMod.GetHashCode();
                hashCode = (hashCode * 397) ^ MassDelta.GetHashCode();
                hashCode = (hashCode * 397) ^ (Residues != null ? Residues.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SpecificityRules != null ? SpecificityRules.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML EnzymesType
    /// </summary>
    /// <remarks>The list of enzymes used in experiment</remarks>
    public partial class EnzymeListObj : IdentDataInternalTypeAbstract
    {
        public EnzymeListObj()
        {
            this._independent = false;
            this.IndependentSpecified = false;

            this._enzymes = null;
        }

        /// min 1, max unbounded
        //public IdentDataList<Enzyme> Enzymes

        /// <remarks>If there are multiple enzymes specified, this attribute is set to true if cleavage with different enzymes is performed independently.</remarks>
        /// Optional Attribute
        /// boolean
        //public bool Independent

        /// Attribute Existence
        //public bool IndependentSpecified

        public override bool Equals(object other)
        {
            var o = other as EnzymeListObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(EnzymeListObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Independent == other.Independent && Equals(this.Enzymes, other.Enzymes))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Independent.GetHashCode();
                hashCode = (hashCode * 397) ^ (Enzymes != null ? Enzymes.GetHashCode() : 0);
                return hashCode;
            }
        }
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
        public FilterInfo()
        {
            this._filterType = null;
            this._include = null;
            this._exclude = null;
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

        public override bool Equals(object other)
        {
            var o = other as FilterInfo;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(FilterInfo other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.FilterType, other.FilterType) && Equals(this.Include, other.Include) &&
                Equals(this.Exclude, other.Exclude))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (FilterType != null ? FilterType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Include != null ? Include.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Exclude != null ? Exclude.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML DatabaseTranslationType
    /// </summary>
    /// <remarks>A specification of how a nucleic acid sequence database was translated for searching.</remarks>
    public partial class DatabaseTranslationObj : IdentDataInternalTypeAbstract
    {
        public DatabaseTranslationObj()
        {
            this._translationTables = null;
            this._frames = null;
        }

        /// min 1, max unbounded
        //public IdentDataList<TranslationTable> TranslationTable

        /// <remarks>The frames in which the nucleic acid sequence has been translated as a space separated IdentDataList</remarks>
        /// Optional Attribute
        /// listOfAllowedFrames: space-separated string, valid values -3, -2, -1, 1, 2, 3
        //public List<int> Frames

        public override bool Equals(object other)
        {
            var o = other as DatabaseTranslationObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(DatabaseTranslationObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.TranslationTables, other.TranslationTables) &&
                ListUtils.ListEqualsUnOrdered(this.Frames, other.Frames))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (TranslationTables != null ? TranslationTables.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Frames != null ? Frames.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML ProtocolApplicationType
    /// </summary>
    /// <remarks>The use of a protocol with the requisite Parameters and ParameterValues. 
    /// ProtocolApplications can take Material or Data (or both) as input and produce Material or Data (or both) as output.</remarks>
    public abstract partial class ProtocolApplicationObj : IdentDataInternalTypeAbstract, IIdentifiableType
    {
        public ProtocolApplicationObj()
        {
            this._id = null;
            this._name = null;
            this._activityDate = System.DateTime.Now;
            this.ActivityDateSpecified = false;
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

        public override bool Equals(object other)
        {
            var o = other as ProtocolApplicationObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(ProtocolApplicationObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML ProteinDetectionType
    /// </summary>
    /// <remarks>An Analysis which assembles a set of peptides (e.g. from a spectra search analysis) to proteins.</remarks>
    public partial class ProteinDetectionObj : ProtocolApplicationObj
    {
        public ProteinDetectionObj()
        {
            this._proteinDetectionListRef = null;
            this._proteinDetectionProtocolRef = null;

            this._proteinDetectionList = null;
            this._proteinDetectionProtocol = null;
            this._inputSpectrumIdentifications = null;
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

        public override bool Equals(object other)
        {
            var o = other as ProteinDetectionObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(ProteinDetectionObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && Equals(this.InputSpectrumIdentifications, other.InputSpectrumIdentifications) &&
                Equals(this.ProteinDetectionList, other.ProteinDetectionList) &&
                Equals(this.ProteinDetectionProtocol, other.ProteinDetectionProtocol))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (InputSpectrumIdentifications != null ? InputSpectrumIdentifications.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ProteinDetectionList != null ? ProteinDetectionList.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ProteinDetectionProtocol != null ? ProteinDetectionProtocol.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML InputSpectrumIdentificationsType
    /// </summary>
    /// <remarks>The lists of spectrum identifications that are input to the protein detection process.</remarks>
    public partial class InputSpectrumIdentificationsObj : IdentDataInternalTypeAbstract
    {
        public InputSpectrumIdentificationsObj()
        {
            this._spectrumIdentificationListRef = null;

            this._spectrumIdentificationList = null;
        }

        /// <remarks>A reference to the list of spectrum identifications that were input to the process.</remarks>
        /// Required Attribute
        /// string
        //public string SpectrumIdentificationListRef

        public override bool Equals(object other)
        {
            var o = other as InputSpectrumIdentificationsObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(InputSpectrumIdentificationsObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.SpectrumIdentificationList, other.SpectrumIdentificationList))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (SpectrumIdentificationList != null ? SpectrumIdentificationList.GetHashCode() : 0);
                return hashCode;
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
        public SpectrumIdentificationObj()
        {
            this._spectrumIdentificationProtocolRef = null;
            this._spectrumIdentificationListRef = null;

            this._spectrumIdentificationProtocol = null;
            this._spectrumIdentificationList = null;
            this._inputSpectra = null;
            this._searchDatabases = null;
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

        public override bool Equals(object other)
        {
            var o = other as SpectrumIdentificationObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(SpectrumIdentificationObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && Equals(this.InputSpectra, other.InputSpectra) &&
                Equals(this.SpectrumIdentificationList, other.SpectrumIdentificationList) &&
                Equals(this.SpectrumIdentificationProtocol, other.SpectrumIdentificationProtocol))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (InputSpectra != null ? InputSpectra.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SpectrumIdentificationList != null ? SpectrumIdentificationList.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SpectrumIdentificationProtocol != null ? SpectrumIdentificationProtocol.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML InputSpectraType
    /// </summary>
    /// <remarks>The attribute referencing an identifier within the SpectraData section.</remarks>
    public partial class InputSpectraRefObj : IdentDataInternalTypeAbstract
    {
        public InputSpectraRefObj()
        {
            this._spectraDataRef = null;

            this._spectraData = null;
        }

        /// <remarks>A reference to the SpectraData element which locates the input spectra to an external file.</remarks>
        /// Optional Attribute
        /// string
        //public string SpectraDataRef

        public override bool Equals(object other)
        {
            var o = other as InputSpectraRefObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(InputSpectraRefObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.SpectraData, other.SpectraData))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (SpectraData != null ? SpectraData.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML SearchDatabaseRefType
    /// </summary>
    /// <remarks>One of the search databases used.</remarks>
    public partial class SearchDatabaseRefObj : IdentDataInternalTypeAbstract
    {
        public SearchDatabaseRefObj()
        {
            this._searchDatabaseRef = null;

            this._searchDatabase = null;
        }

        /// <remarks>A reference to the database searched.</remarks>
        /// Optional Attribute
        /// string
        //public string SearchDatabaseRef

        public override bool Equals(object other)
        {
            var o = other as SearchDatabaseRefObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(SearchDatabaseRefObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.SearchDatabase, other.SearchDatabase))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (SearchDatabase != null ? SearchDatabase.GetHashCode() : 0);
                return hashCode;
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
        public PeptideEvidenceObj()
        {
            this._dBSequenceRef = null;
            this._peptideRef = null;
            this._start = -1;
            this.StartSpecified = false;
            this._end = -1;
            this.EndSpecified = false;
            this._pre = null;
            this._post = null;
            this._translationTableRef = null;
            this._frame = -1;
            this.FrameSpecified = false;
            this._isDecoy = false;
            this._id = null;
            this._name = null;

            this._dBSequence = null;
            this._peptide = null;
            this._translationTable = null;
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

        public override bool Equals(object other)
        {
            var o = other as PeptideEvidenceObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(PeptideEvidenceObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }
            if (this.IsDecoy == other.IsDecoy && this.Start == other.Start && this.End == other.End &&
                this.Pre == other.Pre && this.Post == other.Post && this.Frame == other.Frame && this.Name == other.Name &&
                Equals(this.TranslationTable, other.TranslationTable) && Equals(this.Peptide, other.Peptide) &&
                Equals(this.DBSequence, other.DBSequence) && Equals(this.CVParams, other.CVParams) &&
                Equals(this.UserParams, other.UserParams))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsDecoy.GetHashCode();
                hashCode = (hashCode * 397) ^ Start;
                hashCode = (hashCode * 397) ^ End;
                hashCode = (hashCode * 397) ^ Pre.GetHashCode();
                hashCode = (hashCode * 397) ^ Post.GetHashCode();
                hashCode = (hashCode * 397) ^ Frame;
                hashCode = (hashCode * 397) ^ (TranslationTable != null ? TranslationTable.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Peptide != null ? Peptide.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DBSequence != null ? DBSequence.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML PeptideType
    /// </summary>
    /// <remarks>One (poly)peptide (a sequence with modifications). The combination of Peptide sequence and modifications must be unique in the file.</remarks>
    public partial class PeptideObj : ParamGroupObj, IIdentifiableType
    {
        public PeptideObj()
        {
            this._id = null;
            this._name = null;
            this._peptideSequence = null;

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

        public override bool Equals(object other)
        {
            var o = other as PeptideObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(PeptideObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && this.PeptideSequence == other.PeptideSequence &&
                Equals(this.Modifications, other.Modifications) &&
                Equals(this.SubstitutionModifications, other.SubstitutionModifications) &&
                Equals(this.CVParams, other.CVParams) && Equals(this.UserParams, other.UserParams))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PeptideSequence != null ? PeptideSequence.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Modifications != null ? Modifications.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SubstitutionModifications != null ? SubstitutionModifications.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
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
    public partial class ModificationObj : CVParamGroupObj
    {
        public ModificationObj()
        {
            this._location = -1;
            this.LocationSpecified = false;
            this._avgMassDelta = 0;
            this.AvgMassDeltaSpecified = false;
            this._monoisotopicMassDelta = 0;
            this.MonoisotopicMassDeltaSpecified = false;

            this._residues = null;
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

        public override bool Equals(object other)
        {
            var o = other as ModificationObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(ModificationObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.AvgMassDelta.Equals(other.AvgMassDelta) &&
                this.MonoisotopicMassDelta.Equals(other.MonoisotopicMassDelta) && this.Location == other.Location &&
                ListUtils.ListEqualsUnOrdered(this.Residues, other.Residues) && Equals(this.CVParams, other.CVParams))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = AvgMassDelta.GetHashCode();
                hashCode = (hashCode * 397) ^ MonoisotopicMassDelta.GetHashCode();
                hashCode = (hashCode * 397) ^ Location;
                hashCode = (hashCode * 397) ^ (Residues != null ? Residues.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML SubstitutionModificationType
    /// </summary>
    /// <remarks>A modification where one residue is substituted by another (amino acid change).</remarks>
    public partial class SubstitutionModificationObj : IdentDataInternalTypeAbstract
    {
        public SubstitutionModificationObj()
        {
            this._originalResidue = null;
            this._replacementResidue = null;
            this._location = -1;
            this.LocationSpecified = false;
            this._avgMassDelta = 0;
            this.AvgMassDeltaSpecified = false;
            this._monoisotopicMassDelta = 0;
            this.MonoisotopicMassDeltaSpecified = false;
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

        public override bool Equals(object other)
        {
            var o = other as SubstitutionModificationObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(SubstitutionModificationObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.AvgMassDelta.Equals(other.AvgMassDelta) && this.Location == other.Location &&
                this.MonoisotopicMassDelta.Equals(other.MonoisotopicMassDelta) &&
                this.OriginalResidue == other.OriginalResidue && this.ReplacementResidue == other.ReplacementResidue)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = AvgMassDelta.GetHashCode();
                hashCode = (hashCode * 397) ^ Location;
                hashCode = (hashCode * 397) ^ MonoisotopicMassDelta.GetHashCode();
                hashCode = (hashCode * 397) ^ (OriginalResidue != null ? OriginalResidue.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ReplacementResidue != null ? ReplacementResidue.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML DBSequenceType
    /// </summary>
    /// <remarks>A database sequence from the specified SearchDatabase (nucleic acid or amino acid). 
    /// If the sequence is nucleic acid, the source nucleic acid sequence should be given in 
    /// the seq attribute rather than a translated sequence.</remarks>
    public partial class DbSequenceObj : ParamGroupObj, IIdentifiableType
    {
        public DbSequenceObj()
        {
            this._id = null;
            this._name = null;
            this._seq = null;
            this._length = -1;
            this.LengthSpecified = false;
            this._searchDatabaseRef = null;
            this._accession = null;

            this._searchDatabase = null;
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

        public new bool Equals(object other)
        {
            var o = other as DbSequenceObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(DbSequenceObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Accession == other.Accession && this.Length == other.Length && this.Name == other.Name &&
                this.Seq == other.Seq && Equals(this.SearchDatabase, other.SearchDatabase) &&
                Equals(this.CVParams, other.CVParams) && Equals(this.UserParams, other.UserParams))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Accession != null ? Accession.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Length;
                hashCode = (hashCode * 397) ^ (Seq != null ? Seq.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SearchDatabase != null ? SearchDatabase.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
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
    public partial class SampleObj : ParamGroupObj, IIdentifiableType
    {
        public SampleObj()
        {
            this._id = null;
            this._name = null;

            this._contactRoles = null;
            this._subSamples = null;
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

        public new bool Equals(object other)
        {
            var o = other as SampleObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(SampleObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && Equals(this.ContactRoles, other.ContactRoles) &&
                Equals(this.SubSamples, other.SubSamples) && Equals(this.CVParams, other.CVParams) &&
                Equals(this.UserParams, other.UserParams))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ContactRoles != null ? ContactRoles.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SubSamples != null ? SubSamples.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML ContactRoleType
    /// </summary>
    /// <remarks>The role that a Contact plays in an organization or with respect to the associating class. 
    /// A Contact may have several Roles within scope, and as such, associations to ContactRole 
    /// allow the use of a Contact in a certain manner. Examples might include a provider, or a data analyst.</remarks>
    public partial class ContactRoleObj : IdentDataInternalTypeAbstract
    {
        public ContactRoleObj()
        {
            this._contactRef = null;

            this._contact = null;
            this._role = null;
        }

        /// min 1, max 1
        //public RoleInfo Role

        /// <remarks>When a ContactRole is used, it specifies which Contact the role is associated with.</remarks>
        /// Required Attribute
        /// string
        //public string ContactRef

        public override bool Equals(object other)
        {
            var o = other as ContactRoleObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(ContactRoleObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.Contact, other.Contact) && Equals(this.Role, other.Role))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Contact != null ? Contact.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Role != null ? Role.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML RoleType
    /// </summary>
    /// <remarks>The roles (lab equipment sales, contractor, etc.) the Contact fills.</remarks>
    public partial class RoleObj : IdentDataInternalTypeAbstract
    {
        public RoleObj()
        {
            this._cvParam = null;
        }

        /// <remarks>CV term for contact roles, such as software provider.</remarks>
        /// min 1, max 1
        //public CVParam CVParam

        public override bool Equals(object other)
        {
            var o = other as RoleObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(RoleObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.CVParam, other.CVParam))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (CVParam != null ? CVParam.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML SubSampleType
    /// </summary>
    /// <remarks>References to the individual component samples within a mixed parent sample.</remarks>
    public partial class SubSampleObj : IdentDataInternalTypeAbstract
    {
        public SubSampleObj()
        {
            this._sampleRef = null;

            this._sample = null;
        }

        /// <remarks>A reference to the child sample.</remarks>
        /// Required  Attribute
        /// string
        //public string SampleRef

        public override bool Equals(object other)
        {
            var o = other as SubSampleObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(SubSampleObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.Sample, other.Sample))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Sample != null ? Sample.GetHashCode() : 0);
                return hashCode;
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
        public AbstractContactObj()
        {
            this._id = null;
            this._name = null;
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

        public override bool Equals(object other)
        {
            var o = other as AbstractContactObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(AbstractContactObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && Equals(this.CVParams, other.CVParams) &&
                Equals(this.UserParams, other.UserParams))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML OrganizationType
    /// </summary>
    /// <remarks>Organizations are entities like companies, universities, government agencies. 
    /// Any additional information such as the address, email etc. should be supplied either as CV parameters or as user parameters.</remarks>
    public partial class OrganizationObj : AbstractContactObj
    {
        public OrganizationObj()
        {
            this._parent = null;
        }

        /// min 0, max 1
        //public ParentOrganization Parent

        public override bool Equals(object other)
        {
            var o = other as OrganizationObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(OrganizationObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && Equals(this.Parent, other.Parent) &&
                Equals(this.CVParams, other.CVParams) && Equals(this.UserParams, other.UserParams))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Parent != null ? Parent.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// Base class for identical ParentOrganization and AffiliationInfo
    /// </summary>
    public partial class OrganizationRefObj : IdentDataInternalTypeAbstract
    {
        public OrganizationRefObj()
        {
            this._organizationRef = null;

            this._organization = null;
        }

        /// <remarks>A reference to the organization this contact belongs to.</remarks>
        /// Required Attribute
        /// string
        //public string OrganizationRef

        public override bool Equals(object other)
        {
            var o = other as OrganizationRefObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(OrganizationRefObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.Organization, other.Organization))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Organization != null ? Organization.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML ParentOrganizationType
    /// </summary>
    /// <remarks>The containing organization (the university or business which a lab belongs to, etc.)</remarks>
    public partial class ParentOrganizationObj : OrganizationRefObj
    {
        public ParentOrganizationObj() : base()
        { }

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
        public PersonObj()
        {
            this._lastName = null;
            this._firstName = null;
            this._midInitials = null;

            this._affiliations = null;
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

        public override bool Equals(object other)
        {
            var o = other as PersonObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(PersonObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && this.LastName == other.LastName && this.FirstName == other.FirstName &&
                this.MidInitials == other.MidInitials && Equals(this.Affiliations, other.Affiliations) &&
                Equals(this.CVParams, other.CVParams) && Equals(this.UserParams, other.UserParams))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LastName != null ? LastName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FirstName != null ? FirstName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (MidInitials != null ? MidInitials.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Affiliations != null ? Affiliations.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML AffiliationType
    /// </summary>
    public partial class AffiliationObj : OrganizationRefObj
    {
        public AffiliationObj() : base()
        { }

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
        public ProviderObj()
        {
            this._id = null;
            this._name = null;
            this._analysisSoftwareRef = null;

            this._analysisSoftware = null;
            this._contactRole = null;
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

        public override bool Equals(object other)
        {
            var o = other as ProviderObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(ProviderObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && Equals(this.ContactRole, other.ContactRole) &&
                Equals(this.AnalysisSoftware, other.AnalysisSoftware))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ContactRole != null ? ContactRole.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AnalysisSoftware != null ? AnalysisSoftware.GetHashCode() : 0);
                return hashCode;
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
        public AnalysisSoftwareObj()
        {
            this._id = null;
            this._name = null;
            this._customizations = null;
            this._version = null;
            this._uri = null;

            this._contactRole = null;
            this._softwareName = null;
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

        public override bool Equals(object other)
        {
            var o = other as AnalysisSoftwareObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(AnalysisSoftwareObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (this.Name == other.Name && this.Customizations == other.Customizations && this.URI == other.URI &&
                this.Version == other.Version && Equals(this.ContactRole, other.ContactRole) &&
                Equals(this.SoftwareName, other.SoftwareName))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Customizations != null ? Customizations.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (URI != null ? URI.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Version != null ? Version.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ContactRole != null ? ContactRole.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SoftwareName != null ? SoftwareName.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML InputsType
    /// </summary>
    /// <remarks>The inputs to the analyses including the databases searched, the spectral data and the source file converted to mzIdentML.</remarks>
    public partial class InputsObj : IdentDataInternalTypeAbstract
    {
        public InputsObj()
        {
            this._sourceFiles = null;
            this._searchDatabases = null;
            this._spectraDataList = null;
        }

        private long _specDataIdCounter = 0;
        private long _searchDbIdCounter = 0;

        /// min 0, max unbounded
        //public IdentDataList<SourceFileInfo> SourceFile

        /// min 0, max unbounded
        //public IdentDataList<SearchDatabaseInfo> SearchDatabase

        /// min 1, max unbounde
        //public IdentDataList<SpectraData> SpectraData

        internal void RebuildLists()
        {
            RebuildSearchDatabaseList();
            RebuildSpectraDataList();
        }

        private void RebuildSearchDatabaseList()
        {
            _searchDbIdCounter = 0;
            _searchDatabases.Clear();

            foreach (var dbSeq in this.IdentData.SequenceCollection.DBSequences)
            {
                if (_searchDatabases.Any(item => item.Equals(dbSeq.SearchDatabase)))
                {
                    continue;
                }

                dbSeq.SearchDatabase.Id = "SearchDB_" + _searchDbIdCounter;
                _searchDbIdCounter++;
                _searchDatabases.Add(dbSeq.SearchDatabase);
            }

            foreach (var specId in this.IdentData.AnalysisCollection.SpectrumIdentifications)
            {
                foreach (var dbSeq in specId.SearchDatabases)
                {
                    if (_searchDatabases.Any(item => item.Equals(dbSeq.SearchDatabase)))
                    {
                        continue;
                    }

                    dbSeq.SearchDatabase.Id = "SearchDB_" + _searchDbIdCounter;
                    _searchDbIdCounter++;
                    _searchDatabases.Add(dbSeq.SearchDatabase);
                }
            }
        }

        private void RebuildSpectraDataList()
        {
            _specDataIdCounter = 0;
            _spectraDataList.Clear();

            foreach (var sil in this.IdentData.DataCollection.AnalysisData.SpectrumIdentificationList)
            {
                foreach (var spectraData in sil.SpectrumIdentificationResults)
            {
                if (_spectraDataList.Any(item => item.Equals(spectraData.SpectraData)))
                {
                    continue;
                }

                spectraData.SpectraData.Id = "SID_" + _specDataIdCounter;
                _specDataIdCounter++;
                _spectraDataList.Add(spectraData.SpectraData);
            }}

            foreach (var specId in this.IdentData.AnalysisCollection.SpectrumIdentifications)
            {
                foreach (var spectraData in specId.InputSpectra)
                {
                    if (_spectraDataList.Any(item => item.Equals(spectraData.SpectraData)))
                    {
                        continue;
                    }

                    spectraData.SpectraData.Id = "SID_" + _specDataIdCounter;
                    _specDataIdCounter++;
                    _spectraDataList.Add(spectraData.SpectraData);
                }
            }
        }

        public override bool Equals(object other)
        {
            var o = other as InputsObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(InputsObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.SourceFiles, other.SourceFiles) && Equals(this.SearchDatabases, other.SearchDatabases) &&
                Equals(this.SpectraDataList, other.SpectraDataList))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (SourceFiles != null ? SourceFiles.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SearchDatabases != null ? SearchDatabases.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SpectraDataList != null ? SpectraDataList.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML DataCollectionType
    /// </summary>
    /// <remarks>The collection of input and output data sets of the analyses.</remarks>
    public partial class DataCollectionObj : IdentDataInternalTypeAbstract
    {
        public DataCollectionObj()
        {
            this._inputs = null;
            this._analysisData = null;
        }

        /// min 1, max 1
        //public InputsInfo Inputs

        /// min 1, max 1
        //public AnalysisData AnalysisData

        internal void RebuildLists()
        {
            Inputs.RebuildLists();
            AnalysisData.RebuildLists();
        }

        public override bool Equals(object other)
        {
            var o = other as DataCollectionObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(DataCollectionObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.Inputs, other.Inputs) && Equals(this.AnalysisData, other.AnalysisData))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Inputs != null ? Inputs.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AnalysisData != null ? AnalysisData.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    /// <summary>
    /// MzIdentML AnalysisProtocolCollectionType
    /// </summary>
    /// <remarks>The collection of protocols which include the parameters and settings of the performed analyses.</remarks>
    public partial class AnalysisProtocolCollectionObj : IdentDataInternalTypeAbstract
    {
        public AnalysisProtocolCollectionObj()
        {
            this._spectrumIdentificationProtocols = null;
            this._proteinDetectionProtocol = null;
        }

        private static long _idCounter = 0;

        /// min 1, max unbounded
        //public IdentDataList<SpectrumIdentificationProtocol> SpectrumIdentificationProtocol

        /// min 0, max 1
        //public ProteinDetectionProtocol ProteinDetectionProtocol

        internal void RebuildSIPList()
        {
            _idCounter = 0;
            _spectrumIdentificationProtocols.Clear();

            foreach (var specId in this.IdentData.AnalysisCollection.SpectrumIdentifications)
            {
                if (_spectrumIdentificationProtocols.Any(item => item.Equals(specId.SpectrumIdentificationProtocol)))
                {
                    continue;
                }

                specId.SpectrumIdentificationProtocol.Id = "SpecIdentProtocol_" + _idCounter;
                _idCounter++;
                _spectrumIdentificationProtocols.Add(specId.SpectrumIdentificationProtocol);
            }
        }

        public override bool Equals(object other)
        {
            var o = other as AnalysisProtocolCollectionObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(AnalysisProtocolCollectionObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.ProteinDetectionProtocol, other.ProteinDetectionProtocol) &&
                Equals(this.SpectrumIdentificationProtocols, other.SpectrumIdentificationProtocols))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (ProteinDetectionProtocol != null ? ProteinDetectionProtocol.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SpectrumIdentificationProtocols != null ? SpectrumIdentificationProtocols.GetHashCode() : 0);
                return hashCode;
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
        public AnalysisCollectionObj()
        {
            this._spectrumIdentifications = null;
            this._proteinDetection = null;
        }

        /// min 1, max unbounded
        //public IdentDataList<SpectrumIdentification> SpectrumIdentification

        /// min 0, max 1
        //public ProteinDetection ProteinDetection

        public override bool Equals(object other)
        {
            var o = other as AnalysisCollectionObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(AnalysisCollectionObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.ProteinDetection, other.ProteinDetection) &&
                Equals(this.SpectrumIdentifications, other.SpectrumIdentifications))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (ProteinDetection != null ? ProteinDetection.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SpectrumIdentifications != null ? SpectrumIdentifications.GetHashCode() : 0);
                return hashCode;
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
        public SequenceCollectionObj()
        {
            this._dBSequences = null;
            this._peptides = null;
            this._peptideEvidences = null;
        }

        private long _dBSeqIdCounter = 0;
        private long _pepIdCounter = 0;
        private long _pepEvIdCounter = 0;

        /// min 1, max unbounded
        //public IdentDataList<DBSequence> DBSequences

        /// min 0, max unbounded
        //public IdentDataList<Peptide> Peptides

        /// min 0, max unbounded
        //public IdentDataList<PeptideEvidence> PeptideEvidences

        internal void RebuildLists()
        {
            RebuildPeptideEvidenceList();
            RebuildPeptideList();
            RebuildDbSequenceList();
        }

        private void RebuildPeptideEvidenceList()
        {
            _pepEvIdCounter = 0;
            _peptideEvidences.Clear();

            foreach (var sil in this.IdentData.DataCollection.AnalysisData.SpectrumIdentificationList)
            {
                foreach (var sir in sil.SpectrumIdentificationResults)
                {
                    foreach (var sii in sir.SpectrumIdentificationItems)
                    {
                        foreach (var pepEv in sii.PeptideEvidences)
                        {
                            if (_peptideEvidences.Any(item => item.Equals(pepEv.PeptideEvidence)))
                            {
                                continue;
                            }

                            pepEv.PeptideEvidence.Id = "Pep_" + _pepEvIdCounter;
                            _pepEvIdCounter++;
                            _peptideEvidences.Add(pepEv.PeptideEvidence);
                        }
                    }
                }
            }
        }

        private void RebuildPeptideList()
        {
            _pepIdCounter = 0;
            _peptides.Clear();

            foreach (var sil in this.IdentData.DataCollection.AnalysisData.SpectrumIdentificationList)
            {
                foreach (var sir in sil.SpectrumIdentificationResults)
                {
                    foreach (var sii in sir.SpectrumIdentificationItems)
                    {
                        if (_peptides.Any(item => item.Equals(sii.Peptide)))
                        {
                            continue;
                        }

                        sii.Peptide.Id = "Pep_" + _pepIdCounter;
                        _pepIdCounter++;
                        _peptides.Add(sii.Peptide);
                    }
                }
            }

            foreach (var pepEv in _peptideEvidences)
            {
                if (_peptides.Any(item => item.Equals(pepEv.Peptide)))
                {
                    continue;
                }

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
                {
                    continue;
                }

                pepEv.DBSequence.Id = "DBSeq_" + _dBSeqIdCounter;
                _dBSeqIdCounter++;
                _dBSequences.Add(pepEv.DBSequence);
            }
        }

        public override bool Equals(object other)
        {
            var o = other as SequenceCollectionObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        public bool Equals(SequenceCollectionObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(this.DBSequences, other.DBSequences) && Equals(this.Peptides, other.Peptides) &&
                Equals(this.PeptideEvidences, other.PeptideEvidences))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (DBSequences != null ? DBSequences.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Peptides != null ? Peptides.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PeptideEvidences != null ? PeptideEvidences.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}