using System.Collections.Generic;
using System.IO;
/*********************************************************************************************************************************
 * Work TODO:
 * TODO: Conversion from array of bytes to numeric arrays (BinaryDataArray)
 * TODO: Fix count reporting - no "setting" capability, but report list/array size
 * TODO: Some structure cleanup
 * TODO: Configuration for linking CVRefs, ReferenceableParamGroupRefs, InstrumentConfigurations, Etc.
 * 
 * NOTE: this is the primary interface, since there are many checks and like that cannot be managed properly in the serialization class.
 *       they may be combined in the future once everything is figured out, but for now it will be easier to copy things over from mzML when reading and over to mzML when writing.
 * 
 * 
 ********************************************************************************************************************************/
using PSI_Interface.CV;

namespace PSI_Interface.MSData
{
    /// <summary>
    /// mzML mzMLType
    /// </summary>
    /// <remarks>This is the root element for the Proteomics Standards Initiative (PSI) mzML schema, which 
    /// is intended to capture the use of a mass spectrometer, the data generated, and 
    /// the initial processing of that data (to the level of the peak list)</remarks>
    public partial class MSData
    {
        internal CVTranslator CvTranslator = new CVTranslator(); // Create a generic translator by default; must be re-mapped when reading a file
        private MSDataList<CVType> _cvList;
        private FileDescriptionType _fileDescription;
        private MSDataList<ReferenceableParamGroupType> _referenceableParamGroupList;
        private MSDataList<SampleType> _sampleList;
        private MSDataList<SoftwareType> _softwareList;
        private MSDataList<ScanSettingsType> _scanSettingsList;
        private MSDataList<InstrumentConfigurationType> _instrumentConfigurationList;
        private MSDataList<DataProcessingType> _dataProcessingList;
        private RunType _run;
        
        /// min 1, max 1
        public MSDataList<CVType> CVList // TODO: enforce quantity
        {
            get { return this._cvList; }
            set
            {
                this._cvList = value;
                if (this._cvList != null)
                {
                    this._cvList.MsData = this;
                    CvTranslator = new CVTranslator(this._cvList);
                }
                else
                {
                    CvTranslator = new CVTranslator();
                }
            }
        }

        /// min 1, max 1
        public FileDescriptionType FileDescription // TODO: enforce quantity
        {
            get { return this._fileDescription; }
            set
            {
                this._fileDescription = value;
                if (this._fileDescription != null)
                {
                    this._fileDescription.MsData = this;
                }
            }
        }

        /// min 0, max 1
        public MSDataList<ReferenceableParamGroupType> ReferenceableParamGroupList
        {
            get { return this._referenceableParamGroupList; }
            set
            {
                this._referenceableParamGroupList = value;
                if (this._referenceableParamGroupList != null)
                {
                    this._referenceableParamGroupList.MsData = this;
                }
            }
        }

        /// min 0, max 1
        public MSDataList<SampleType> SampleList
        {
            get { return this._sampleList; }
            set
            {
                this._sampleList = value;
                if (this._sampleList != null)
                {
                    this._sampleList.MsData = this;
                }
            }
        }

        /// min 1, max 1
        public MSDataList<SoftwareType> SoftwareList // TODO: enforce quantity
        {
            get { return this._softwareList; }
            set
            {
                this._softwareList = value;
                if (this._softwareList != null)
                {
                    this._softwareList.MsData = this;
                }
            }
        }

        /// min 0, max 1
        public MSDataList<ScanSettingsType> ScanSettingsList
        {
            get { return this._scanSettingsList; }
            set
            {
                this._scanSettingsList = value;
                if (this._scanSettingsList != null)
                {
                    this._scanSettingsList.MsData = this;
                }
            }
        }

        /// min 1, max 1
        public MSDataList<InstrumentConfigurationType> InstrumentConfigurationList // TODO: enforce quantity
        {
            get { return this._instrumentConfigurationList; }
            set
            {
                this._instrumentConfigurationList = value;
                if (this._instrumentConfigurationList != null)
                {
                    this._instrumentConfigurationList.MsData = this;
                }
            }
        }

        /// min 1, max 1
        public MSDataList<DataProcessingType> DataProcessingList // TODO: enforce quantity
        {
            get { return this._dataProcessingList; }
            set
            {
                this._dataProcessingList = value;
                if (this._dataProcessingList != null)
                {
                    this._dataProcessingList.MsData = this;
                }
            }
        }

        /// min 1, max 1
        public RunType Run // TODO: enforce quantity
        {
            get { return this._run; }
            set
            {
                this._run = value;
                if (this._run != null)
                {
                    this._run.MsData = this;
                }
            }
        }

        /// <remarks>An optional accession number for the mzML document used for storage, e.g. in PRIDE.</remarks>
        /// Optional Attribute
        /// string
        public string Accession { get; set; } // TODO: what?

        /// <remarks>An optional id for the mzML document used for referencing from external files. It is recommended to use LSIDs when possible.</remarks>
        /// Optional Attribute
        /// string
        public string Id { get; set; } // TODO: enforce validity?

        /// <remarks>The version of this mzML document.</remarks>
        /// Required Attribute
        /// string
        public string Version { get; set; } // TODO: Enforce requirement
    }
    /*
    /// <summary>
    /// mzML CVListType
    /// </summary>
    /// <remarks>Container for one or more controlled vocabulary definitions.</remarks>
    public partial class CVListType
    {
        private List<CVType> cvs;

        /// min 1, max unbounded
        public CVType[] cv
        {
            get { return cvs.ToArray(); }
            set { cvs = value.ToList(); } // TODO: recreate the CVTranslator when setting... (Add a hook that runs whenever it is modified?)
        }

        /// <remarks>The number of CV definitions in this mzML file.</remarks>
        /// Required Attribute
        /// non-negative integer
        public string count
        {
            get { return cvs.Count.ToString(); }
            set
            {
                // We really don't want to set anything...
                //countField = value;
            }
        }
    }
    */
    /// <summary>
    /// mzML CVType
    /// </summary>
    /// <remarks>Information about an ontology or CV source and a short 'lookup' tag to refer to.</remarks>
    public partial class CVType
    {
        /// <remarks>The short label to be used as a reference tag with which to refer to this particular Controlled Vocabulary source description (e.g., from the cvLabel attribute, in CVParamType elements).</remarks>
        /// Required Attribute
        /// ID
        public string Id { get; set; } // TODO: enforce requirement, uniqueness among dataset

        /// <remarks>The usual name for the resource (e.g. The PSI-MS Controlled Vocabulary).</remarks>
        /// Required Attribute
        /// string
        public string FullName { get; set; } // TODO: enforce requirement

        /// <remarks>The version of the CV from which the referred-to terms are drawn.</remarks>
        /// Optional Attribute
        /// string
        public string Version { get; set; } // TODO: enforce requirement

        /// <remarks>The URI for the resource.</remarks>
        /// Required Attribute
        /// anyURI
        public string URI { get; set; } // TODO: enforce requirement
    }
    /*
    /// <summary>
    /// mzML DataProcessingListType
    /// </summary>
    /// <remarks>List and descriptions of data processing applied to this data.</remarks>
    public partial class DataProcessingListType
    {
        private List<DataProcessingType> dataProcessingField;
        private string countField;

        /// min 1, max unbounded
        public DataProcessingType[] dataProcessing
        {
            get { return dataProcessingField.ToArray(); }
            set { dataProcessingField = value.ToList(); }
        }

        /// <remarks>The number of DataProcessingTypes in this mzML file.</remarks>
        /// Required Attribute
        /// non-negative integer
        public string count
        {
            get { return countField; }
            set { countField = value; }
        }
    }
    */
    /// <summary>
    /// mzML DataProcessingType
    /// </summary>
    /// <remarks>Description of the way in which a particular software was used.</remarks>
    public partial class DataProcessingType
    {
        private MSDataList<ProcessingMethodType> _processingMethod;

        /// <remarks>Description of the default peak processing method. 
        /// This element describes the base method used in the generation of a particular mzML file. 
        /// Variable methods should be described in the appropriate acquisition section - if 
        /// no acquisition-specific details are found, then this information serves as the default.</remarks>
        /// min 1, max unbounded
        public MSDataList<ProcessingMethodType> ProcessingMethod // TODO: enforce quantity
        {
            get { return this._processingMethod; }
            set
            {
                this._processingMethod = value;
                if (this._processingMethod != null)
                {
                    this._processingMethod.MsData = this.MsData;
                }
            }
        }

        /// <remarks>A unique identifier for this data processing that is unique across all DataProcessingTypes.</remarks>
        /// Required Attribute
        /// ID
        public string Id { get; set; } // TODO: enforce requirement, uniqueness among DataProcessing in dataset
    }

    /// <summary>
    /// mzML ProcessingMethodType
    /// </summary>
    public partial class ProcessingMethodType : ParamGroupType
    {
        /// <remarks>This attributes allows a series of consecutive steps to be placed in the correct order.</remarks>
        /// Required Attribute
        /// non-negative integer
        public uint Order { get; set; } // TODO: enforce requirement, 1 to n contiguous

        /// <remarks>This attribute must reference the 'id' of the appropriate SoftwareType.</remarks>
        /// Required Attribute
        /// IDREF
        public string SoftwareRef { get; set; } // TODO: enforce requirement, validity
    }

    /// <summary>
    /// mzML ParamGroupType
    /// </summary>
    /// <remarks>Structure allowing the use of a controlled (cvParam) or uncontrolled vocabulary (userParam), or a reference to a predefined set of these in this mzML file (paramGroupRef).</remarks>
    public partial class ParamGroupType
    {
        private MSDataList<ReferenceableParamGroupRefType> _referenceableParamGroupRef;
        private MSDataList<CVParamType> _cVParam;
        private MSDataList<UserParamType> _userParam;

        /// min 0, max unbounded
        public MSDataList<ReferenceableParamGroupRefType> ReferenceableParamGroupRef
        {
            get { return this._referenceableParamGroupRef; }
            set
            {
                this._referenceableParamGroupRef = value;
                if (this._referenceableParamGroupRef != null)
                {
                    this._referenceableParamGroupRef.MsData = this.MsData;
                }
            }
        }
        //public ParamGroupType[] referenceableParamGroupRef
        //{
        //  get { return this._paramGroupRefs.ToArray(); }
        //  set { this._paramGroupRefs = value.ToList(); }
        //}

        /// min 0, max unbounded
        public MSDataList<CVParamType> CVParam
        {
            get { return this._cVParam; }
            set
            {
                this._cVParam = value;
                if (this._cVParam != null)
                {
                    this._cVParam.MsData = this.MsData;
                }
            }
        }

        /// min 0, max unbounded
        public MSDataList<UserParamType> UserParam
        {
            get { return this._userParam; }
            set
            {
                this._userParam = value;
                if (this._userParam != null)
                {
                    this._userParam.MsData = this.MsData;
                }
            }
        }
    }
    /*
    /// <summary>
    /// mzML ReferenceableParamGroupListType
    /// </summary>
    /// <remarks>Container for a list of referenceableParamGroups</remarks>
    public partial class ReferenceableParamGroupListType
    {
        private List<ReferenceableParamGroupType> referenceableParamGroupField;
        private string countField;

        /// min 1, max unbounded
        public ReferenceableParamGroupType[] referenceableParamGroup
        {
            get { return referenceableParamGroupField.ToArray(); }
            set { referenceableParamGroupField = value.ToList(); }
        }

        /// <remarks>The number of ParamGroups defined in this mzML file.</remarks>
        /// Required Attribute
        /// non-negative integer
        public string count
        {
            get { return countField; }
            set { countField = value; }
        }
    }
    */
    /// <summary>
    /// mzML ReferenceableParamGroupType
    /// </summary>
    /// <remarks>A collection of CVParam and UserParam elements that can be referenced from elsewhere in this mzML document by using the 'paramGroupRef' element in that location to reference the 'id' attribute value of this element.</remarks>
    public partial class ReferenceableParamGroupType
    {
        private MSDataList<CVParamType> _cVParam;
        private MSDataList<UserParamType> _userParam;

        /// min 0, max unbounded
        public MSDataList<CVParamType> CVParam
        {
            get { return this._cVParam; }
            set
            {
                this._cVParam = value;
                if (this._cVParam != null)
                {
                    this._cVParam.MsData = this.MsData;
                }
            }
        }

        /// min 0, max unbounded
        public MSDataList<UserParamType> UserParam
        {
            get { return this._userParam; }
            set
            {
                this._userParam = value;
                if (this._userParam != null)
                {
                    this._userParam.MsData = this.MsData;
                }
            }
        }

        /// <remarks>The identifier with which to reference this ReferenceableParamGroup.</remarks>
        /// Required Attribute
        /// ID
        public string Id { get; set; } // TODO: enforce requirement, uniqueness
    }

    /// <summary>
    /// mzML ReferenceableParamGroupRefType
    /// </summary>
    /// <remarks>A reference to a previously defined ParamGroup, which is a reusable container of one or more cvParams.</remarks>
    public partial class ReferenceableParamGroupRefType
    {
        /// <remarks>Reference to the id attribute in a referenceableParamGroup.</remarks>
        /// Required Attribute
        /// IDREF
        public string @Ref { get; set; } // TODO: enforce requirement, validity
    }

    /// <summary>
    /// mzML CVParamType
    /// </summary>
    /// <remarks>This element holds additional data or annotation. Only controlled values are allowed here.</remarks>
    public partial class CVParamType
    {
        private string _cvRef = "??";
        //private CVType _CVRef;
        //private string _accession;
        private string _value;
        //private string _name;
        //private string _unitAccession;
        //private string _unitName;
        //private string _unitCvRef = "??";
        //private CVType _UnitCVRef;

        //private CV.CV.CVID _cvid;
        //private CV.CV.CVID _unitCvid;

        //[System.Xml.Serialization.XmlIgnore]
        public CV.CV.CVID Cvid { get; set; }

        //[System.Xml.Serialization.XmlIgnore]
        //public CV.CV.CVID UnitCvid { get; set; }

        /// <remarks>A reference to the CV 'id' attribute as defined in the cvList in this mzML file.</remarks>
        /// Required Attribute
        /// IDREF
        internal string CVRef
        {
            get
            {
                //return this.Accession.Split(new[] { ':' })[0];
                return this.MsData.CvTranslator.ConvertOboCVRef(CV.CV.TermData[this.Cvid].CVRef);
                //return this._cvRef;
                //return this._CVRef.Id;
            }
            set
            {
                this._cvRef = this.MsData.CvTranslator.ConvertFileCVRef(value);
                // have to set up a dictionary or something similar...
                //CVRef = cvs[value];
            }
        }

        /// <remarks>The accession number of the referred-to term in the named resource (e.g.: MS:000012).</remarks>
        /// Required Attribute
        /// string
        internal string Accession
        {
            get
            {
                return CV.CV.TermData[this.Cvid].Id;
                //return this._accession;
            } // TODO: change this return to a value mapped from the cvid
            set
            {
                //this._accession = value;
                //var oboAcc = MsData.CvTranslator.ConvertFileAccession(value);
                //if (CV.CV.TermAccessionLookup.ContainsKey(oboAcc))
                if (CV.CV.TermAccessionLookup.ContainsKey(_cvRef) && CV.CV.TermAccessionLookup[_cvRef].ContainsKey(value))
                {
                    //this.Cvid = CV.CV.TermAccessionLookup[oboAcc];
                    this.Cvid = CV.CV.TermAccessionLookup[_cvRef][value];
                }
                else
                {
                    this.Cvid = CV.CV.CVID.CVID_Unknown;
                }
            } // TODO: map this to a cvid, and store the cvid, and don't store the accession
        }

        /// <remarks>The actual name for the parameter, from the referred-to controlled vocabulary. This should be the preferred name associated with the specified accession number.</remarks>
        /// Required Attribute
        /// string
        internal string Name
        {
            get
            {
                return CV.CV.TermData[this.Cvid].Name;
                //return this._name;
            } // TODO: change this return to a value mapped from the cvid
            //set { this._name = value; } // TODO: remove this when accession to cvid mapping is complete
        }

        /// <remarks>The value for the parameter; may be absent if not appropriate, or a numeric or symbolic value, or may itself be CV (legal values for a parameter should be enumerated and defined in the ontology).</remarks>
        /// Optional Attribute
        /// string
        public string Value
        {
            get { return this._value; }
            set { this._value = value; }
        } // TODO: Perform validation of the value according to CVID/UnitCVID?

        /// <remarks>If a unit term is referenced, this attribute must refer to the CV 'id' attribute defined in the cvList in this mzML file.</remarks>
        /// Optional Attribute
        /// IDREF
        /*internal string UnitCVRef
        {
            get
            {
                //return this.UnitAccession.Split(new[] { ':' })[0];
                return this.MsData.CvTranslator.ConvertOboCVRef(CV.CV.TermData[this.UnitCvid].CVRef);
                //return this._unitCvRef;
                //return this._UnitCVRef.Id;
            }
            set
            {
                this._unitCvRef = this.MsData.CvTranslator.ConvertFileCVRef(value);
                // have to set up a dictionary or something similar...
                //UnitCVRef = cvs[value];
            }
        }*/

        /// <remarks>An optional CV accession number for the unit term associated with the value, if any (e.g., 'UO:0000266' for 'electron volt').</remarks>
        /// Optional Attribute
        /// string
        /*internal string UnitAccession
        {
            get
            {
                return CV.CV.TermData[this.UnitCvid].Id;
                //return this._unitAccession;
            } // TODO: change this return to a value mapped from the cvid
            set
            {
                //this._unitAccession = value;
                //var oboAcc = MsData.CvTranslator.ConvertFileAccession(value);
                //if (CV.CV.TermAccessionLookup.ContainsKey(oboAcc))
                if (CV.CV.TermAccessionLookup.ContainsKey(_unitCvRef) && CV.CV.TermAccessionLookup[_unitCvRef].ContainsKey(value))
                {
                    this.UnitCvid = CV.CV.TermAccessionLookup[_unitCvRef][value];
                }
                else
                {
                    this.UnitCvid = CV.CV.CVID.CVID_Unknown;
                }
            } // TODO: map this to a cvid, and store the cvid, and don't store the accession
        }*/

        /// <remarks>An optional CV name for the unit accession number, if any (e.g., 'electron volt' for 'UO:0000266' ).</remarks>
        /// Optional Attribute
        /// string
        /*internal string UnitName
        {
            get
            {
                return CV.CV.TermData[this.UnitCvid].Name;
                //return this._unitName;
            } // TODO: change this return to a value mapped from the cvid
            //set { this._unitName = value; } // TODO: remove this when accession to cvid mapping is complete
        }*/
    }

    /// <summary>
    /// mzML UserParamType
    /// </summary>
    /// <remarks>Uncontrolled user parameters (essentially allowing free text). 
    /// Before using these, one should verify whether there is an appropriate 
    /// CV term available, and if so, use the CV term instead</remarks>
    public partial class UserParamType
    {
        //private string _unitAccession;
        //private string _unitName;
        //private string _unitCvRef = "??";
        //private CV.CV.CVID _unitCvid;
        //private CVType _unitCVRef;

        //public CV.CV.CVID UnitCvid { get; set; }

        /// <remarks>The name for the parameter.</remarks>
        /// Required Attribute
        /// string
        public string Name { get; set; }

        /// <remarks>The datatype of the parameter, where appropriate (e.g.: xsd:float).</remarks>
        /// Optional Attribute
        /// string
        public string Type { get; set; }

        /// <remarks>The value for the parameter, where appropriate.</remarks>
        /// Optional Attribute
        /// string
        public string Value { get; set; }

        /// <remarks>An optional CV accession number for the unit term associated with the value, if any (e.g., 'UO:0000266' for 'electron volt').</remarks>
        /// Optional Attribute
        /// string
        /*internal string UnitAccession
        {
            get
            {
                return MsData.CvTranslator.ConvertOboAccession(CV.CV.TermData[this.UnitCvid].Id);
                //return this._unitAccession;
            }
            set
            {
                //this._unitAccession = value;
                //var oboAcc = MsData.CvTranslator.ConvertFileAccession(value);
                //if (CV.CV.TermAccessionLookup.ContainsKey(oboAcc))
                if (CV.CV.TermAccessionLookup.ContainsKey(_unitCvRef) && CV.CV.TermAccessionLookup[_unitCvRef].ContainsKey(value))
                {
                    this.UnitCvid = CV.CV.TermAccessionLookup[_unitCvRef][value];
                }
                else
                {
                    this.UnitCvid = CV.CV.CVID.CVID_Unknown;
                }
            }
        }*/

        /// <remarks>An optional CV name for the unit accession number, if any (e.g., 'electron volt' for 'UO:0000266' ).</remarks>
        /// Optional Attribute
        /// string
        /*internal string UnitName
        {
            get
            {
                return CV.CV.TermData[this.UnitCvid].Name;
                //return this._unitName;
            }
            //set { this._unitName = value; }
        }*/

        /// <remarks>If a unit term is referenced, this attribute must refer to the CV 'id' attribute defined in the cvList in this mzML file.</remarks>
        /// Optional Attribute
        /// IDREF
        /*internal string UnitCVRef
        {
            get
            {
                //return this.UnitAccession.Split(new[] {':'})[0];
                return this.MsData.CvTranslator.ConvertOboCVRef(CV.CV.TermData[this.UnitCvid].CVRef);
                //return this._unitCvRef;
                //return this._unitCVRef.Id;
            }
            set
            {
                //this._unitCvRef = value;
                this._unitCvRef = this.MsData.CvTranslator.ConvertFileCVRef(value);
                // have to set up a dictionary or something similar...
                //UnitCVRef = cvs[value];
            }
        }*/
    }

    /// <summary>
    /// ParamType
    /// </summary>
    /// <remarks>Base type for CVParam and UserParam to reduce code duplication.</remarks>
    public partial class ParamType
    {
        //private string _unitAccession;
        //private string _unitName;
        private string _unitCvRef;
        //private CVType _UnitCVRef;
        private CV.CV.CVID _unitCvid = CV.CV.CVID.CVID_Unknown;
        private bool _unitsSet = false;

        //[System.Xml.Serialization.XmlIgnore]
        public CV.CV.CVID UnitCvid
        {
            get { return this._unitCvid; }
            set
            {
                this._unitCvid = value;
                this._unitsSet = true;
            }
        }

        /// <remarks>If a unit term is referenced, this attribute must refer to the CV 'id' attribute defined in the cvList in this mzML file.</remarks>
        /// Optional Attribute
        /// IDREF
        internal string UnitCVRef
        {
            get
            {
                //return this.UnitAccession.Split(new[] { ':' })[0];
                if (this._unitsSet)
                {
                    return this.MsData.CvTranslator.ConvertOboCVRef(CV.CV.TermData[this.UnitCvid].CVRef);
                }
                return null;
                //return this._unitCvRef;
                //return this._UnitCVRef.Id;
            }
            set
            {
                this._unitCvRef = value;
                if (value != null)
                {
                    this._unitCvRef = this.MsData.CvTranslator.ConvertFileCVRef(value);
                }
                // have to set up a dictionary or something similar...
                //UnitCVRef = cvs[value];
            }
        }

        /// <remarks>An optional CV accession number for the unit term associated with the value, if any (e.g., 'UO:0000266' for 'electron volt').</remarks>
        /// Optional Attribute
        /// string
        internal string UnitAccession
        {
            get
            {
                if (this._unitsSet)
                {
                    return CV.CV.TermData[this.UnitCvid].Id;
                }
                return null;
                //return this._unitAccession;
            } // TODO: change this return to a value mapped from the cvid
            set
            {
                //this._unitAccession = value;
                //var oboAcc = MsData.CvTranslator.ConvertFileAccession(value);
                //if (CV.CV.TermAccessionLookup.ContainsKey(oboAcc))
                if (value != null && CV.CV.TermAccessionLookup.ContainsKey(_unitCvRef) && CV.CV.TermAccessionLookup[_unitCvRef].ContainsKey(value))
                {
                    this._unitsSet = true;
                    this.UnitCvid = CV.CV.TermAccessionLookup[_unitCvRef][value];
                }
                else
                {
                    this.UnitCvid = CV.CV.CVID.CVID_Unknown;
                }
            } // TODO: map this to a cvid, and store the cvid, and don't store the accession
        }

        /// <remarks>An optional CV name for the unit accession number, if any (e.g., 'electron volt' for 'UO:0000266' ).</remarks>
        /// Optional Attribute
        /// string
        internal string UnitName
        {
            get
            {
                if (this._unitsSet)
                {
                    return CV.CV.TermData[this.UnitCvid].Name;
                }
                return null;
                //return this._unitName;
            } // TODO: change this return to a value mapped from the cvid
            //set { this._unitName = value; } // TODO: remove this when accession to cvid mapping is complete
        }
    }

    /*
    /// <summary>
    /// mzML PrecursorListType
    /// </summary>
    /// <remarks>List and descriptions of precursor isolations to the spectrum currently being described, ordered.</remarks>
    public partial class PrecursorListType
    {
        private List<PrecursorType> precursorField;
        private string countField;

        /// min 1, max unbounded
        public PrecursorType[] precursor
        {
            get { return precursorField.ToArray(); }
            set { precursorField = value.ToList(); }
        }

        /// <remarks>The number of precursor isolations in this list.</remarks>
        /// Required Attribute
        /// non-negative integer
        public string count
        {
            get { return countField; }
            set { countField = value; }
        }
    }
    */

    /// <summary>
    /// mzML PrecursorType
    /// </summary>
    /// <remarks>The method of precursor ion selection and activation</remarks>
    public partial class PrecursorType
    {
        private ParamGroupType _isolationWindow;
        private MSDataList<ParamGroupType> _selectedIonList;
        private ParamGroupType _activation;

        /// <remarks>This element captures the isolation (or 'selection') window configured to isolate one or more ions.</remarks>
        /// min 0, max 1
        public ParamGroupType IsolationWindow
        {
            get { return this._isolationWindow; }
            set
            {
                this._isolationWindow = value;
                if (this._isolationWindow != null)
                {
                    this._isolationWindow.MsData = this.MsData;
                }
            }
        }

        /// <remarks>A list of ions that were selected.</remarks>
        /// min 0, max 1
        //public SelectedIonListType selectedIonList
        public MSDataList<ParamGroupType> SelectedIonList
        {
            get { return this._selectedIonList; }
            set
            {
                this._selectedIonList = value;
                if (this._selectedIonList != null)
                {
                    this._selectedIonList.MsData = this.MsData;
                }
            }
        }

        /// <remarks>The type and energy level used for activation.</remarks>
        /// min 1, max 1
        public ParamGroupType Activation
        {
            get { return this._activation; }
            set
            {
                this._activation = value;
                if (this._activation != null)
                {
                    this._activation.MsData = this.MsData;
                }
            }
        }

        /// <remarks>For precursor spectra that are local to this document, this attribute must be used to reference the 'id' attribute of the spectrum corresponding to the precursor spectrum.</remarks>
        /// Optional Attribute
        /// string
        public string SpectrumRef { get; set; } // TODO: enforce validity, proper usage

        /// <remarks>For precursor spectra that are external to this document, this attribute must reference the 'id' attribute of a sourceFile representing that external document.</remarks>
        /// Optional Attribute
        /// IDREF
        public string SourceFileRef { get; set; } // TODO: enforce validity

        /// <remarks>For precursor spectra that are external to this document, this string must correspond to the 'id' attribute of a spectrum in the external document indicated by 'sourceFileRef'.</remarks>
        /// Optional Attribute
        /// string
        public string ExternalSpectrumID { get; set; } // TODO: enforce validity
    }

    /*
    /// <summary>
    /// mzML SelectedIonListType
    /// </summary>
    /// <remarks>The list of selected precursor ions.</remarks>
    public partial class SelectedIonListType
    {
        private List<ParamGroupType> selectedIonField;
        private string countField;

        /// min 1, max unbounded
        public ParamGroupType[] selectedIon
        {
            get { return selectedIonField.ToArray(); }
            set { selectedIonField = value.ToList(); }
        }

        /// <remarks>The number of selected precursor ions defined in this list.</remarks>
        /// Required Attribute
        /// non-negative integer
        public string count
        {
            get { return countField; }
            set { countField = value; }
        }
    }
    */
    /*
    /// <summary>
    /// mzML BinaryDataArrayListType
    /// </summary>
    /// <remarks>List of binary data arrays.</remarks>
    public partial class BinaryDataArrayListType
    {
        private List<BinaryDataArrayType> binaryDataArrayField;
        private string countField;

        /// <remarks>Data point arrays for default data arrays (m/z, intensity, time) and meta data arrays. 
        /// Default data arrays must not have the attributes 'arrayLength' and 'dataProcessingRef'.</remarks>
        /// min 2, max unbounded
        public BinaryDataArrayType[] binaryDataArray
        {
            get { return binaryDataArrayField.ToArray(); }
            set { binaryDataArrayField = value.ToList(); }
        }

        /// <remarks>The number of binary data arrays defined in this list.</remarks>
        /// Required Attribute
        /// non-negative integer
        public string count
        {
            get { return countField; }
            set { countField = value; }
        }
    }
    */

    /// <summary>
    /// mzML BinaryDataArrayType
    /// </summary>
    /// <remarks>The structure into which encoded binary data goes. Byte ordering is always little endian (Intel style). 
    /// Computers using a different endian style must convert to/from little endian when writing/reading mzML</remarks>
    public partial class BinaryDataArrayType : ParamGroupType
    {
        //CV Rules: 
        //MUST supply a *child* term of MS:1000572 (binary data compression type) only once
        //  e.g.: MS:1000574 (zlib compression)
        //  e.g.: MS:1000576 (no compression)
        //MUST supply a *child* term of MS:1000513 (binary data array) only once
        //  e.g.: MS:1000514 (m/z array)
        //  e.g.: MS:1000515 (intensity array)
        //  e.g.: MS:1000516 (charge array)
        //  e.g.: MS:1000517 (signal to noise array)
        //  e.g.: MS:1000595 (time array)
        //  e.g.: MS:1000617 (wavelength array)
        //  e.g.: MS:1000786 (non-standard data array)
        //  e.g.: MS:1000820 (flow rate array)
        //  e.g.: MS:1000821 (pressure array)
        //  e.g.: MS:1000822 (temperature array)
        //MUST supply a *child* term of MS:1000518 (binary data type) only once
        //  e.g.: MS:1000521 (32-bit float)
        //  e.g.: MS:1000523 (64-bit float)
        public enum ArrayType : int
        {
            m_z,
            intensity,
            charge,
            signal_to_noise,
            time,
            wavelength,
            non_standard_data,
            flow_rate,
            pressure,
            temperature,
        }

        private static readonly Dictionary<ArrayType, CV.CV.CVID> _typeMap = new Dictionary<ArrayType, CV.CV.CVID>()
        {
            {ArrayType.m_z, CV.CV.CVID.MS_m_z_array},
            {ArrayType.intensity, CV.CV.CVID.MS_intensity_array},
            {ArrayType.charge, CV.CV.CVID.MS_charge_array},
            {ArrayType.signal_to_noise, CV.CV.CVID.MS_signal_to_noise_array},
            {ArrayType.time, CV.CV.CVID.MS_time_array},
            {ArrayType.wavelength, CV.CV.CVID.MS_wavelength_array},
            {ArrayType.non_standard_data, CV.CV.CVID.MS_non_standard_data_array},
            {ArrayType.flow_rate, CV.CV.CVID.MS_flow_rate_array},
            {ArrayType.pressure, CV.CV.CVID.MS_pressure_array},
            {ArrayType.temperature, CV.CV.CVID.MS_temperature_array},
        };

        private static readonly Dictionary<CV.CV.CVID, ArrayType> _revTypeMap = new Dictionary<CV.CV.CVID, ArrayType>()
        {
            {CV.CV.CVID.MS_m_z_array, ArrayType.m_z},
            {CV.CV.CVID.MS_intensity_array, ArrayType.intensity},
            {CV.CV.CVID.MS_charge_array, ArrayType.charge},
            {CV.CV.CVID.MS_signal_to_noise_array, ArrayType.signal_to_noise},
            {CV.CV.CVID.MS_time_array, ArrayType.time},
            {CV.CV.CVID.MS_wavelength_array, ArrayType.wavelength},
            {CV.CV.CVID.MS_non_standard_data_array, ArrayType.non_standard_data},
            {CV.CV.CVID.MS_flow_rate_array, ArrayType.flow_rate},
            {CV.CV.CVID.MS_pressure_array, ArrayType.pressure},
            {CV.CV.CVID.MS_temperature_array, ArrayType.temperature},
        };

        // Data values that are processed from the other data; some is converted, some is from the cvParams.
        private int _expectedArrayLength; // Only used for reading from mzML, so no external access. // TODO: is this superceded by bdaDefaultArrayLength?
        private double[] _data;
        private ArrayType _dataType;
        private int _dataWidth;
        private bool _isCompressed;

        //[System.Xml.Serialization.XmlIgnore] // can use this attribute to ignore a field/property.
        public double[] Data
        {
            get { return this._data; }
            set { this._data = value; }
        }

        public ArrayType DataType
        {
            get { return this._dataType; }
            set
            {
                this._dataType = value;
                // Make sure the appropriate cvParam is set...
                for (int i = 0; i < CVParam.Count; i++)
                {
                    foreach (var cvid in CV.CV.RelationsChildren[CV.CV.CVID.MS_binary_data_array])
                    {
                        if (CVParam[i].Cvid == cvid)
                        {
                            CVParam.RemoveAt(i);
                            i--;
                            break; // break out of inner loop.
                        }
                    }
                }
                var cv = new CVParamType();
                cv.Cvid = _typeMap[_dataType];
                CVParam.Add(cv);
            }
        }

        public int DataWidth
        {
            get { return this._dataWidth; }
            set
            {
                if (value != 4 && value != 8)
                {
                    // only 4 and 8 are valid, 2 is obsolete
                    return;
                }
                this._dataWidth = value;
                // Make sure the appropriate cvParam is set...
                for (int i = 0; i < CVParam.Count; i++)
                {
                    foreach (var cvid in CV.CV.RelationsChildren[CV.CV.CVID.MS_binary_data_type])
                    {
                        if (CVParam[i].Cvid == cvid)
                        {
                            CVParam.RemoveAt(i);
                            i--;
                            break; // break out of inner loop.
                        }
                    }
                }
                var cv = new CVParamType();
                cv.Cvid = CV.CV.CVID.CVID_Unknown;
                if (this._dataWidth == 4)
                {
                    cv.Cvid = CV.CV.CVID.MS_32_bit_float;
                }
                else if (this._dataWidth == 8)
                {
                    cv.Cvid = CV.CV.CVID.MS_64_bit_float;
                }
                CVParam.Add(cv);
            }
        }

        public bool IsCompressed
        {
            get { return this._isCompressed; }
            set
            {
                this._isCompressed = value;
                // Make sure the appropriate cvParam is set...
                for (int i = 0; i < CVParam.Count; i++)
                {
                    foreach (var cvid in CV.CV.RelationsChildren[CV.CV.CVID.MS_binary_data_compression_type])
                    {
                        if (CVParam[i].Cvid == cvid)
                        {
                            CVParam.RemoveAt(i);
                            i--;
                            break; // break out of inner loop.
                        }
                    }
                }
                CVParamType cv = new CVParamType();
                cv.Cvid = CV.CV.CVID.MS_no_compression;
                if (this._isCompressed)
                {
                    cv.Cvid = CV.CV.CVID.MS_zlib_compression;
                }
                CVParam.Add(cv);
            }
        }

        public int DataLength
        {
            get { return this._data.Length; }
        }

        /// <remarks>The actual base64 encoded binary data. The byte order is always 'little endian'.</remarks>
        /// base64Binary
        public byte[] Binary
        {
            get { return Base64Conversion.EncodeBytes(this._data, this._dataWidth, this._isCompressed); }
            set { this._data = Base64Conversion.DecodeBytes(value, this._dataWidth, (int)ArrayLength, this._isCompressed); }
        }

        /// <remarks>This optional attribute may override the 'defaultArrayLength' defined in SpectrumType. 
        /// The two default arrays (m/z and intensity) should NEVER use this override option, and should 
        /// therefore adhere to the 'defaultArrayLength' defined in SpectrumType. Parsing software can thus 
        /// safely choose to ignore arrays of lengths different from the one defined in the 'defaultArrayLength' SpectrumType element.</remarks>
        /// Optional Attribute
        /// non-negative integer
        public uint ArrayLength { get; set; } // TODO: enforce appropriate usage

        /// <remarks>This optional attribute may reference the 'id' attribute of the appropriate dataProcessing.</remarks>
        /// Optional Attribute
        /// IDREF
        public string DataProcessingRef { get; set; } // TODO: enforce validity
    }

    /// <summary>
    /// mzML ScanListType
    /// </summary>
    /// <remarks>List and descriptions of scans.</remarks>
    public partial class ScanListType : ParamGroupType
    {
        private MSDataList<ScanType> _scan;

        /// min 1, max unbounded
        public MSDataList<ScanType> Scan // TODO: enforce quantity
        {
            get { return this._scan; }
            set
            {
                this._scan = value;
                if (this._scan != null)
                {
                    this._scan.MsData = this.MsData;
                }
            }
        }
    }

    /// <summary>
    /// mzML ScanType
    /// </summary>
    /// <remarks>Scan or acquisition from original raw file used to create this peak list, as specified in sourceF</remarks>
    public partial class ScanType : ParamGroupType
    {
        private MSDataList<ParamGroupType> _scanWindowList;

        /// <remarks>Container for a list of scan windows.</remarks>
        /// min 0, max 1
        //public ScanWindowListType scanWindowList
        public MSDataList<ParamGroupType> ScanWindowList
        {
            get { return this._scanWindowList; }
            set
            {
                this._scanWindowList = value;
                if (this._scanWindowList != null)
                {
                    this._scanWindowList.MsData = this.MsData;
                }
            }
        }

        /// <remarks>For scans that are local to this document, this attribute can be used to reference the 'id' attribute of the spectrum corresponding to the scan.</remarks>
        /// Optional Attribute
        /// string
        public string SpectrumRef { get; set; } // TODO: enforce validity

        /// <remarks>If this attribute is set, it must reference the 'id' attribute of a sourceFile representing the external document containing the spectrum referred to by 'externalSpectrumID'.</remarks>
        /// Optional Attribute
        /// IDREF
        public string SourceFileRef { get; set; } // TODO: enforce validity

        /// <remarks>For scans that are external to this document, this string must correspond to the 'id' attribute of a spectrum in the external document indicated by 'sourceFileRef'.</remarks>
        /// Optional Attribute
        /// string
        public string ExternalSpectrumID { get; set; } // TODO: enforce validity

        /// <remarks>This attribute can optionally reference the 'id' attribute of the appropriate instrument configuration.</remarks>
        /// Optional Attribute
        /// IDREF
        public string InstrumentConfigurationRef { get; set; } // TODO: enforce validity
    }

    /*
    /// <summary>
    /// mzML ScanWindowListType
    /// </summary>
    /// <remarks></remarks>
    public partial class ScanWindowListType
    {
        private List<ParamGroupType> scanWindowField;
        private int countField;

        /// <remarks>A range of m/z values over which the instrument scans and acquires a spectrum.</remarks>
        /// min 1, max unbounded
        public ParamGroupType[] scanWindow
        {
            get { return scanWindowField.ToArray(); }
            set { scanWindowField = value.ToList(); }
        }

        /// <remarks>The number of scan windows defined in this list.</remarks>
        /// Required Attribute
        /// int
        public int count
        {
            get { return countField; }
            set { countField = value; }
        }
    }
    */

    /// <summary>
    /// mzML SpectrumListType
    /// </summary>
    /// <remarks>List and descriptions of spectra.</remarks>
    public partial class SpectrumListType
    {
        private MSDataList<SpectrumType> _spectrum;

        /// min 0, max unbounded
        public MSDataList<SpectrumType> Spectrum
        {
            get { return this._spectrum; }
            set
            {
                this._spectrum = value;
                if (this._spectrum != null)
                {
                    this._spectrum.MsData = this.MsData;
                }
            }
        }

        /// <remarks>This attribute MUST reference the 'id' of the default data processing for the spectrum list. 
        /// If an acquisition does not reference any data processing, it implicitly refers to this data processing. 
        /// This attribute is required because the minimum amount of data processing that any format will undergo is "conversion to mzML".</remarks>
        /// Required Attribute
        /// IDREF
        public string DefaultDataProcessingRef { get; set; } // TODO: enforce requirement, validity
    }

    /// <summary>
    /// mzML SpectrumType
    /// </summary>
    /// <remarks>The structure that captures the generation of a peak list (including the underlying acquisitions). 
    /// Also describes some of the parameters for the mass spectrometer for a given acquisition (or list of acquisitions).</remarks>
    public partial class SpectrumType : ParamGroupType
    {
        private ScanListType _scanList;
        private MSDataList<PrecursorType> _precursorList;
        private MSDataList<ProductType> _productList;
        private MSDataList<BinaryDataArrayType> _binaryDataArrayList;
        private int _defaultArrayLength;

        /// min 0, max 1
        public ScanListType ScanList
        {
            get { return this._scanList; }
            set
            {
                this._scanList = value;
                if (this._scanList != null)
                {
                    this._scanList.MsData = this.MsData;
                }
            }
        }

        /// min 0, max 1
        public MSDataList<PrecursorType> PrecursorList
        {
            get { return this._precursorList; }
            set
            {
                this._precursorList = value;
                if (this._precursorList != null)
                {
                    this._precursorList.MsData = this.MsData;
                }
            }
        }

        /// min 0, max 1
        public MSDataList<ProductType> ProductList
        {
            get { return this._productList; }
            set
            {
                this._productList = value;
                if (this._productList != null)
                {
                    this._productList.MsData = this.MsData;
                }
            }
        }

        /// min 0, max 1
        public MSDataList<BinaryDataArrayType> BinaryDataArrayList
        {
            get { return this._binaryDataArrayList; }
            set
            {
                this._binaryDataArrayList = value;
                if (this._binaryDataArrayList != null)
                {
                    this._binaryDataArrayList.MsData = this.MsData;
                    this._binaryDataArrayList.DefaultArrayLength = this._defaultArrayLength;
                }
            }
        }

        /// <remarks>The zero-based, consecutive index of  the spectrum in the SpectrumList.</remarks>
        /// Required Attribute
        /// non-negative integer
        public string Index { get; set; } // TODO: enforce requirement, 0-n contiguous values in spectrumList

        /// <remarks>The native identifier for a spectrum. For unmerged native spectra or spectra from older open file formats, 
        /// the format of the identifier is defined in the PSI-MS CV and referred to in the mzML header. 
        /// External documents may use this identifier together with the mzML filename or accession to reference a particular spectrum.</remarks>
        /// Required Attribute
        /// Regex: "\S+=\S+( \S+=\S+)*"
        public string Id { get; set; } // TODO: enforce consistency? (same style?)

        /// <remarks>The identifier for the spot from which this spectrum was derived, if a MALDI or similar run.</remarks>
        /// Optional Attribute
        /// string
        public string SpotID { get; set; }

        /// <remarks>Default length of binary data arrays contained in this element.</remarks>
        /// Required Attribute
        /// integer
        public int DefaultArrayLength
        {
            get
            {
                int mzLength = 0;
                int intensityLength = 0;
                foreach (var bda in BinaryDataArrayList)
                {
                    if (bda.DataType == BinaryDataArrayType.ArrayType.m_z)
                    {
                        mzLength = bda.DataLength;
                    }
                    else if (bda.DataType == BinaryDataArrayType.ArrayType.intensity)
                    {
                        intensityLength = bda.DataLength;
                    }
                }
                if (mzLength == intensityLength)
                {
                    this._defaultArrayLength = mzLength;
                    return mzLength;
                }
                else
                {
                    throw new InvalidDataException("Spectrum: m/z and intensity arrays are not the same length.");
                }
            }
            set
            {
                this._defaultArrayLength = value;
                if (this._binaryDataArrayList != null)
                {
                    this._binaryDataArrayList.DefaultArrayLength = this._defaultArrayLength;
                }
            }
        }

        /// <remarks>This attribute can optionally reference the 'id' of the appropriate dataProcessing.</remarks>
        /// Optional Attribute
        /// IDREF
        public string DataProcessingRef { get; set; } // TODO: enforce validity

        /// <remarks>This attribute can optionally reference the 'id' of the appropriate sourceFile.</remarks>
        /// Optional Attribute
        /// IDREF
        public string SourceFileRef { get; set; } // TODO: enforce validity
    }

    /*
    /// <summary>
    /// mzML ProductListType
    /// </summary>
    /// <remarks>List and descriptions of product isolations to the spectrum currently being described, ordered.</remarks>
    public partial class ProductListType
    {
        private List<ProductType> productField;
        private string countField;

        /// min 1, max unbounded
        public ProductType[] product
        {
            get { return productField.ToArray(); }
            set { productField = value.ToList(); }
        }

        /// <remarks>The number of product isolations in this list.</remarks>
        /// Required Attribute
        /// non-negative integer
        public string count
        {
            get { return countField; }
            set { countField = value; }
        }
    }
    */

    /// <summary>
    /// mzML ProductType
    /// </summary>
    /// <remarks>The method of product ion selection and activation in a precursor ion scan</remarks>
    public partial class ProductType
    {
        private ParamGroupType _isolationWindow;

        /// <remarks>This element captures the isolation (or 'selection') window configured to isolate one or more ions.</remarks>
        /// min 0, max 1
        public ParamGroupType IsolationWindow
        {
            get { return this._isolationWindow; }
            set
            {
                this._isolationWindow = value;
                if (this._isolationWindow != null)
                {
                    this._isolationWindow.MsData = this.MsData;
                }
            }
        }
    }

    /// <summary>
    /// mzML RunType
    /// </summary>
    /// <remarks>A run in mzML should correspond to a single, consecutive and coherent set of scans on an instrument.</remarks>
    public partial class RunType : ParamGroupType
    {
        private SpectrumListType _spectrumList;
        private ChromatogramListType _chromatogramList;
        private System.DateTime _startTimeStampField;

        /// <remarks>All mass spectra and the acquisitions underlying them are described and attached here. 
        /// Subsidiary data arrays are also both described and attached here.</remarks>
        /// min 0, max 1
        public SpectrumListType SpectrumList
        {
            get { return this._spectrumList; }
            set
            {
                this._spectrumList = value;
                if (this._spectrumList != null)
                {
                    this._spectrumList.MsData = this.MsData;
                }
            }
        }

        /// <remarks>All chromatograms for this run.</remarks>
        /// min 0, max 1
        public ChromatogramListType ChromatogramList
        {
            get { return this._chromatogramList; }
            set
            {
                this._chromatogramList = value;
                if (this._chromatogramList != null)
                {
                    this._chromatogramList.MsData = this.MsData;
                }
            }
        }

        /// <remarks>A unique identifier for this run.</remarks>
        /// Required Attribute
        /// ID
        public string Id { get; set; } // TODO: enforce uniqueness

        /// <remarks>This attribute must reference the 'id' of the default instrument configuration. 
        /// If a scan does not reference an instrument configuration, it implicitly refers to this configuration.</remarks>
        /// Required Attribute
        /// IDREF
        public string DefaultInstrumentConfigurationRef { get; set; } // TODO: enforce requirement, validity

        /// <remarks>This attribute can optionally reference the 'id' of the default source file. 
        /// If a spectrum or scan does not reference a source file and this attribute is set, then it implicitly refers to this source file.</remarks>
        /// Optional Attribute
        /// IDREF
        public string DefaultSourceFileRef { get; set; } // TODO: enforce validity

        /// <remarks>This attribute must reference the 'id' of the appropriate sample.</remarks>
        /// Optional Attribute
        /// IDREF
        public string SampleRef { get; set; } // TODO: enforce validity

        /// <remarks>The optional start timestamp of the run, in UT.</remarks>
        /// Optional Attribute
        /// DateTime
        public System.DateTime StartTimeStamp
        {
            get { return this._startTimeStampField; }
            set
            {
                this._startTimeStampField = value;
                StartTimeStampSpecified = true;
            }
        }

        /// "Ignored" Attribute - only used to signify existence of valid value in startTimeStamp
        public bool StartTimeStampSpecified { get; private set; }
    }

    /// <summary>
    /// mzML ChromatogramListType
    /// </summary>
    /// <remarks>List of chromatograms.</remarks>
    public partial class ChromatogramListType
    {
        private MSDataList<ChromatogramType> _chromatogram;

        /// <remarks></remarks>
        /// min 1, max unbounded
        public MSDataList<ChromatogramType> Chromatogram // TODO: enforce quantity
        {
            get { return this._chromatogram; }
            set
            {
                this._chromatogram = value;
                if (this._chromatogram != null)
                {
                    this._chromatogram.MsData = this.MsData;
                }
            }
        }

        /// <remarks>This attribute MUST reference the 'id' of the default data processing for the chromatogram list. 
        /// If an acquisition does not reference any data processing, it implicitly refers to this data processing. 
        /// This attribute is required because the minimum amount of data processing that any format will undergo is "conversion to mzML".</remarks>
        /// Required Attribute
        /// IDREF
        public string DefaultDataProcessingRef { get; set; } // TODO: enforce requirement, validity of reference in dataset
    }

    /// <summary>
    /// mzML ChromatogramType
    /// </summary>
    /// <remarks>A single Chromatogram</remarks>
    public partial class ChromatogramType : ParamGroupType
    {
        private PrecursorType _precursor;
        private ProductType _product;
        private MSDataList<BinaryDataArrayType> _binaryDataArrayList;
        private int _defaultArrayLength;

        /// min 0, max 1
        public PrecursorType Precursor
        {
            get { return this._precursor; }
            set
            {
                this._precursor = value;
                if (this._precursor != null)
                {
                    this._precursor.MsData = this.MsData;
                }
            }
        }

        /// min 0, max 1
        public ProductType Product
        {
            get { return this._product; }
            set
            {
                this._product = value;
                if (this._product != null)
                {
                    this._product.MsData = this.MsData;
                }
            }
        }

        /// min 1, max 1
        public MSDataList<BinaryDataArrayType> BinaryDataArrayList // TODO: enforce requirement
        {
            get { return this._binaryDataArrayList; }
            set
            {
                this._binaryDataArrayList = value;
                if (this._binaryDataArrayList != null)
                {
                    this._binaryDataArrayList.MsData = this.MsData;
                    this._binaryDataArrayList.DefaultArrayLength = this._defaultArrayLength;
                }
            }
        }

        /// <remarks>The zero-based index for this chromatogram in the chromatogram list</remarks>
        /// Required Attribute
        /// non-negative integer
        public string Index { get; set; } // TODO: enforce requirement, 0-n contiguous values in chromatogram list.

        /// <remarks>A unique identifier for this chromatogram</remarks>
        /// Required Attribute
        /// string
        public string Id { get; set; } // TODO: enforce requirement

        /// <remarks>Default length of binary data arrays contained in this element.</remarks>
        /// Required Attribute
        /// integer
        public int DefaultArrayLength
        {
            get
            {
                int timeLength = 0;
                int intensityLength = 0;
                foreach (var bda in BinaryDataArrayList)
                {
                    if (bda.DataType == BinaryDataArrayType.ArrayType.time)
                    {
                        timeLength = bda.DataLength;
                    }
                    else if (bda.DataType == BinaryDataArrayType.ArrayType.intensity)
                    {
                        intensityLength = bda.DataLength;
                    }
                }
                if (timeLength == intensityLength)
                {
                    this._defaultArrayLength = timeLength;
                    return timeLength;
                }
                else
                {
                    throw new InvalidDataException("Spectrum: m/z and intensity arrays are not the same length.");
                }
            }
            set
            {
                this._defaultArrayLength = value;
                if (this._binaryDataArrayList != null)
                {
                    this._binaryDataArrayList.DefaultArrayLength = this._defaultArrayLength;
                }
            }
        }

        /// <remarks>This attribute can optionally reference the 'id' of the appropriate dataProcessing.</remarks>
        /// Optional Attribute
        /// IDREF
        public string DataProcessingRef { get; set; }
    }

    /*
    /// <summary>
    /// mzML ScanSettingListType
    /// </summary>
    /// <remarks>List with the descriptions of the acquisition settings applied prior to the start of data acquisition.</remarks>
    public partial class ScanSettingsListType
    {
        private List<ScanSettingsType> scanSettingsField;
        private string countField;

        /// min 1, max unbounded
        public ScanSettingsType[] scanSettings
        {
            get { return scanSettingsField.ToArray(); }
            set { scanSettingsField = value.ToList(); }
        }

        /// <remarks>The number of AcquisitionType elements in this list.</remarks>
        /// Required Attribute
        /// non-negative integer
        public string count
        {
            get { return countField; }
            set { countField = value; }
        }
    }
    */

    /// <summary>
    /// mzML ScanSettingsType
    /// </summary>
    /// <remarks>Description of the acquisition settings of the instrument prior to the start of the run.</remarks>
    public partial class ScanSettingsType : ParamGroupType
    {
        private MSDataList<SourceFileRefType> _sourceFileRefList;
        private MSDataList<ParamGroupType> _targetList;

        /// <remarks>List with the source files containing the acquisition settings.</remarks>
        /// min 0, max 1
        public MSDataList<SourceFileRefType> SourceFileRefList
        {
            get { return this._sourceFileRefList; }
            set
            {
                this._sourceFileRefList = value;
                if (this._sourceFileRefList != null)
                {
                    this._sourceFileRefList.MsData = this.MsData;
                }
            }
        }

        /// <remarks>Target list (or 'inclusion list') configured prior to the run.</remarks>
        /// min 0, max 1
        //public TargetListType targetList
        public MSDataList<ParamGroupType> TargetList
        {
            get { return this._targetList; }
            set
            {
                this._targetList = value;
                if (this._targetList != null)
                {
                    this._targetList.MsData = this.MsData;
                }
            }
        }

        /// <remarks>A unique identifier for this acquisition setting.</remarks>
        /// Required Attribute
        /// ID
        public string Id { get; set; } // TODO: Enforce requirement
    }

    /*
    /// <summary>
    /// mzML SourceFileRefListType
    /// </summary>
    public partial class SourceFileRefListType
    {
        private List<SourceFileRefType> sourceFileRefField;
        private string countField;

        /// <remarks>Reference to a previously defined sourceFile.</remarks>
        /// min 0, max unbounded
        public SourceFileRefType[] sourceFileRef
        {
            get { return sourceFileRefField.ToArray(); }
            set { sourceFileRefField = value.ToList(); }
        }

        /// <remarks>This number of source files referenced in this list.</remarks>
        /// Required Attribute
        /// non-negative integer
        public string count
        {
            get { return countField; }
            set { countField = value; }
        }
    }
    */

    /// <summary>
    /// mzML SourceFileRefType
    /// </summary>
    /// <remarks></remarks>
    public partial class SourceFileRefType
    {
        /// <remarks>This attribute must reference the 'id' of the appropriate sourceFile.</remarks>
        /// Required Attribute
        /// IDREF
        public string @Ref { get; set; } // TODO: enforce requirement
    }

    /*
    /// <summary>
    /// mzML TargetListType
    /// </summary>
    /// <remarks>Target list (or 'inclusion list') configured prior to the run.</remarks>
    public partial class TargetListType
    {
        private List<ParamGroupType> targetField;
        private string countField;

        /// min 1, max unbounded
        public ParamGroupType[] target
        {
            get { return targetField.ToArray(); }
            set { targetField = value.ToList(); }
        }

        /// <remarks>The number of TargetType elements in this list.</remarks>
        /// Required Attribute
        /// non-negative integer
        public string count
        {
            get { return countField; }
            set { countField = value; }
        }
    }
    */
    /*
    /// <summary>
    /// mzML SoftwareListType
    /// </summary>
    /// <remarks>List and descriptions of software used to acquire and/or process the data in this mzML file.</remarks>
    public partial class SoftwareListType
    {
        private List<SoftwareType> softwareField;
        private string countField;

        /// <remarks>A piece of software.</remarks>
        /// min 1, max unbounded
        public SoftwareType[] software
        {
            get { return softwareField.ToArray(); }
            set { softwareField = value.ToList(); }
        }

        /// <remarks>The number of softwares defined in this mzML file.</remarks>
        /// Required Attribute
        /// non-negative integer
        public string count
        {
            get { return countField; }
            set { countField = value; }
        }
    }
    */

    /// <summary>
    /// mzML SoftwareType
    /// </summary>
    /// <remarks>Software information</remarks>
    public partial class SoftwareType : ParamGroupType
    {
        /// <remarks>An identifier for this software that is unique across all SoftwareTypes.</remarks>
        /// Required Attribute
        /// ID
        public string Id { get; set; } // TODO: enforce requirement and uniqueness in dataset

        /// <remarks>The software version.</remarks>
        /// Required Attribute
        /// string
        public string Version { get; set; } // TODO: enforce requirement
    }

    /*
    /// <summary>
    /// mzML InstrumentConfigurationListType
    /// </summary>
    /// <remarks>List and descriptions of instrument configurations. 
    /// At least one instrument configuration must be specified, even if it is 
    /// only to specify that the instrument is unknown. In that case, 
    /// the "instrument model" term is used to indicate the unknown 
    /// instrument in the instrumentConfiguration.</remarks>
    public class InstrumentConfigurationListType
    {
        private List<InstrumentConfigurationType> instrumentConfigurationField;
        private string countField;

        /// min 1, max unbounded
        public InstrumentConfigurationType[] instrumentConfiguration
        {
            get { return instrumentConfigurationField.ToArray(); }
            set { instrumentConfigurationField = value.ToList(); }
        }

        /// <remarks>The number of instrument configurations present in this list.</remarks>
        /// Required Attribute
        /// non-negative integer
        public string count
        {
            get { return countField; }
            set { countField = value; }
        }
    }
    */

    /// <summary>
    /// mzML InstrumentConfigurationType
    /// </summary>
    /// <remarks>Description of a particular hardware configuration of a mass spectrometer. 
    /// Each configuration must have one (and only one) of the three different components used for an analysis. 
    /// For hybrid instruments, such as an LTQ-FT, there must be one configuration for each permutation of 
    /// the components that is used in the document. For software configuration, use a ReferenceableParamGroup element</remarks>
    public partial class InstrumentConfigurationType : ParamGroupType
    {
        private ComponentListType _componentList;
        private SoftwareRefType _softwareRef;

        /// min 0, max 1
        public ComponentListType ComponentList
        {
            get { return this._componentList; }
            set
            {
                this._componentList = value;
                if (this._componentList != null)
                {
                    this._componentList.MsData = this.MsData;
                }
            }
        }

        /// min 0, max 1
        public SoftwareRefType SoftwareRef
        {
            get { return this._softwareRef; }
            set
            {
                this._softwareRef = value;
                if (this._softwareRef != null)
                {
                    this._softwareRef.MsData = this.MsData;
                }
            }
        }

        /// <remarks>An identifier for this instrument configuration.</remarks>
        /// Required Attribute
        /// ID
        public string Id { get; set; } // TODO: enforce requirement

        /// Optional Attribute
        /// IDREF
        public string ScanSettingsRef { get; set; }
    }

    /// <summary>
    /// mzML ComponentListType
    /// </summary>
    /// <remarks>List with the different components used in the mass spectrometer. At least one source, one mass analyzer and one detector need to be specified.</remarks>
    public partial class ComponentListType
    {
        private MSDataList<SourceComponentType> _source;
        private MSDataList<AnalyzerComponentType> _analyzer;
        private MSDataList<DetectorComponentType> _detector;

        /// <remarks>A source component.</remarks>
        /// min 1, max unbounded
        public MSDataList<SourceComponentType> Source // TODO: enforce quantity
        {
            get { return this._source; }
            set
            {
                this._source = value;
                if (this._source != null)
                {
                    this._source.MsData = this.MsData;
                }
            }
        }

        /// <remarks>A mass analyzer (or mass filter) component.</remarks>
        /// min 1, max unbounded
        public MSDataList<AnalyzerComponentType> Analyzer // TODO: enforce quantity
        {
            get { return this._analyzer; }
            set
            {
                this._analyzer = value;
                if (this._analyzer != null)
                {
                    this._analyzer.MsData = this.MsData;
                }
            }
        }

        /// <remarks>A detector component.</remarks>
        /// min 1, max unbounded
        public MSDataList<DetectorComponentType> Detector //TODO: enforce quantity
        {
            get { return this._detector; }
            set
            {
                this._detector = value;
                if (this._detector != null)
                {
                    this._detector.MsData = this.MsData;
                }
            }
        }
    }

    /// <summary>
    /// mzML ComponentType
    /// </summary>
    public partial class ComponentType : ParamGroupType
    {
        /// <remarks>This attribute must be used to indicate the order in which the components 
        /// are encountered from source to detector (e.g., in a Q-TOF, the quadrupole would 
        /// have the lower order number, and the TOF the higher number of the two).</remarks>
        /// Required Attribute
        /// integer
        public int Order { get; set; } // TODO: restrict to ordering from 1 to n in dataset, no gaps.
    }

    /// <summary>
    /// mzML SourceComponentType
    /// </summary>
    /// <remarks>This element must be used to describe a Source Component Type. 
    /// This is a PRIDE3-specific modification of the core MzML schema that does not 
    /// have any impact on the base schema validation.</remarks>
    public partial class SourceComponentType : ComponentType
    {
    }

    /// <summary>
    /// mzML AnalyzerComponentType
    /// </summary>
    /// <remarks>This element must be used to describe an Analyzer Component Type. 
    /// This is a PRIDE3-specific modification of the core MzML schema that does not 
    /// have any impact on the base schema validation.</remarks>
    public partial class AnalyzerComponentType : ComponentType
    {
    }

    /// <summary>
    /// mzML DetectorComponentType
    /// </summary>
    /// <remarks>This element must be used to describe a Detector Component Type. 
    /// This is a PRIDE3-specific modification of the core MzML schema that does not 
    /// have any impact on the base schema validation.</remarks>
    public partial class DetectorComponentType : ComponentType
    {
    }

    /// <summary>
    /// mzML SoftwareRefType
    /// </summary>
    /// <remarks>Reference to a previously defined software element</remarks>
    public partial class SoftwareRefType
    {
        /// <remarks>This attribute must be used to reference the 'id' attribute of a software element.</remarks>
        /// Required Attribute
        /// IDREF
        public string @Ref { get; set; } // TODO: restrict to existing software element 'id' attribute // TODO: enforce required attribute (constructor?)
    }

    /*
    /// <summary>
    /// mzML SampleListType
    /// </summary>
    /// <remarks>List and descriptions of samples.</remarks>
    public partial class SampleListType
    {
        private List<SampleType> sampleField;
        private string countField;

        /// min 1, max unbounded
        public SampleType[] sample
        {
            get { return sampleField.ToArray(); }
            set { sampleField = value.ToList(); }
        }

        /// <remarks>The number of Samples defined in this mzML file.</remarks>
        /// Required Attribute
        /// non-negative integer
        public string count
        {
            get { return countField; }
            set { countField = value; }
        }
    }
    */

    /// <summary>
    /// mzML SampleType
    /// </summary>
    /// <remarks>Expansible description of the sample used to generate the dataset, named in sampleName.</remarks>
    public partial class SampleType : ParamGroupType
    {
        /// <remarks>A unique identifier across the samples with which to reference this sample description.</remarks>
        /// Required Attribute
        /// ID
        public string Id { get; set; } // TODO: restrict to unique in dataset // TODO: enforce required attribute (constructor?)

        /// <remarks>An optional name for the sample description, mostly intended as a quick mnemonic.</remarks>
        /// Optional Attribute
        /// string
        public string Name { get; set; }
    }

    /*
    /// <summary>
    /// mzML SourceFileListType
    /// </summary>
    /// <remarks>List and descriptions of the source files this mzML document was generated or derived from</remarks>
    public partial class SourceFileListType
    {
        private List<SourceFileType> sourceFileField;
        private string countField;

        /// min 1, max unbounded
        public SourceFileType[] sourceFile
        {
            get { return sourceFileField.ToArray(); }
            set { sourceFileField = value.ToList(); }
        }

        /// <remarks>Number of source files used in generating the instance document.</remarks>
        /// Required Attribute
        /// non-negative integer
        public string count
        {
            get { return countField; }
            set { countField = value; }
        }
    }
    */

    /// <summary>
    /// mzML SourceFileType
    /// </summary>
    /// <remarks>Description of the source file, including location and type.</remarks>
    public partial class SourceFileType : ParamGroupType
    {
        /// <remarks>An identifier for this file.</remarks>
        /// Required Attribute
        /// ID
        public string Id { get; set; } // TODO: enforce required attribute

        /// <remarks>Name of the source file, without reference to location (either URI or local path).</remarks>
        /// Required Attribute
        /// string
        public string Name { get; set; } // TODO: enforce required attribute

        /// <remarks>URI-formatted location where the file was retrieved.</remarks>
        /// Required Attribute
        /// anyURI
        public string Location { get; set; } // TODO: enforce required attribute
    }

    /// <summary>
    /// mzML FileDescriptionType
    /// </summary>
    /// <remarks>Information pertaining to the entire mzML file (i.e. not specific to any part of the data set) is stored here.</remarks>
    public partial class FileDescriptionType
    {
        private ParamGroupType _fileContent;
        private MSDataList<SourceFileType> _sourceFileList;
        private MSDataList<ParamGroupType> _contact;

        /// <remarks>This summarizes the different types of spectra that can be expected in the file. 
        /// This is expected to aid processing software in skipping files that do not contain appropriate 
        /// spectrum types for it. It should also describe the nativeID format used in the file by referring to an appropriate CV term.</remarks>
        /// min 1, max 1
        public ParamGroupType FileContent // TODO: enforce quantity
        {
            get { return this._fileContent; }
            set
            {
                this._fileContent = value;
                if (this._fileContent != null)
                {
                    this._fileContent.MsData = this.MsData;
                }
            }
        }

        /// min 0, max 1
        public MSDataList<SourceFileType> SourceFileList
        {
            get { return this._sourceFileList; }
            set
            {
                this._sourceFileList = value;
                if (this._sourceFileList != null)
                {
                    this._sourceFileList.MsData = this.MsData;
                }
            }
        }

        /// min 0, max unbounded
        public MSDataList<ParamGroupType> Contact
        {
            get { return this._contact; }
            set
            {
                this._contact = value;
                if (this._contact != null)
                {
                    this._contact.MsData = this.MsData;
                }
            }
        }
    }
}
