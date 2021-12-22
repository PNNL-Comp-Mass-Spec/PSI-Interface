using System;
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

// ReSharper disable RedundantExtendsListEntry

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
        // Ignore Spelling: cv, cvid, cvs, endian, oboAcc, proteomics, referenceable, unmerged, xsd, zlib

        internal CVTranslator CvTranslator;// = new CVTranslator(); // Create a generic translator by default; must be re-mapped when reading a file
        private MSDataList<CVInfo> _cvList;
        private FileDescription _fileDescription;
        private MSDataList<ReferenceableParamGroup> _referenceableParamGroupList;
        private MSDataList<SampleInfo> _sampleList;
        private MSDataList<SoftwareInfo> _softwareList;
        private MSDataList<ScanSettingsInfo> _scanSettingsList;
        private MSDataList<InstrumentConfigurationInfo> _instrumentConfigurationList;
        private MSDataList<DataProcessingInfo> _dataProcessingList;
        private Run _run;

        /// <summary>MS Data List</summary>
        /// <remarks>min 1, max 1</remarks>
        public MSDataList<CVInfo> CVList // TODO: enforce quantity
        {
            get => this._cvList;
            set
            {
                this._cvList = value;
                if (this._cvList != null)
                {
                    this._cvList.MsData = this;
                    this.CvTranslator = new CVTranslator(this._cvList);
                }
                else
                {
                    this.CvTranslator = new CVTranslator();
                }
            }
        }

        /// <summary>File Description</summary>
        /// <remarks>min 1, max 1</remarks>
        public FileDescription FileDescription // TODO: enforce quantity
        {
            get => this._fileDescription;
            set
            {
                this._fileDescription = value;
                if (this._fileDescription != null)
                {
                    this._fileDescription.MsData = this;
                }
            }
        }

        /// <summary>Referenceable Param Group List</summary>
        /// <remarks>min 0, max 1</remarks>
        public MSDataList<ReferenceableParamGroup> ReferenceableParamGroupList
        {
            get => this._referenceableParamGroupList;
            set
            {
                this._referenceableParamGroupList = value;
                if (this._referenceableParamGroupList != null)
                {
                    this._referenceableParamGroupList.MsData = this;
                }
            }
        }

        /// <summary>Sample List</summary>
        /// <remarks>min 0, max 1</remarks>
        public MSDataList<SampleInfo> SampleList
        {
            get => this._sampleList;
            set
            {
                this._sampleList = value;
                if (this._sampleList != null)
                {
                    this._sampleList.MsData = this;
                }
            }
        }

        /// <summary>Software List</summary>
        /// <remarks>min 1, max 1</remarks>
        public MSDataList<SoftwareInfo> SoftwareList // TODO: enforce quantity
        {
            get => this._softwareList;
            set
            {
                this._softwareList = value;
                if (this._softwareList != null)
                {
                    this._softwareList.MsData = this;
                }
            }
        }

        /// <summary>Scan Settings List</summary>
        /// <remarks>min 0, max 1</remarks>
        public MSDataList<ScanSettingsInfo> ScanSettingsList
        {
            get => this._scanSettingsList;
            set
            {
                this._scanSettingsList = value;
                if (this._scanSettingsList != null)
                {
                    this._scanSettingsList.MsData = this;
                }
            }
        }

        /// <summary>Instrument Configuration List</summary>
        /// <remarks>min 1, max 1</remarks>
        public MSDataList<InstrumentConfigurationInfo> InstrumentConfigurationList // TODO: enforce quantity
        {
            get => this._instrumentConfigurationList;
            set
            {
                this._instrumentConfigurationList = value;
                if (this._instrumentConfigurationList != null)
                {
                    this._instrumentConfigurationList.MsData = this;
                }
            }
        }

        /// <summary>Data Processing List</summary>
        /// <remarks>min 1, max 1</remarks>
        public MSDataList<DataProcessingInfo> DataProcessingList // TODO: enforce quantity
        {
            get => this._dataProcessingList;
            set
            {
                this._dataProcessingList = value;
                if (this._dataProcessingList != null)
                {
                    this._dataProcessingList.MsData = this;
                }
            }
        }

        /// <summary>Run Instance</summary>
        /// <remarks>min 1, max 1</remarks>
        public Run Run // TODO: enforce quantity
        {
            get => this._run;
            set
            {
                this._run = value;
                if (this._run != null)
                {
                    this._run.MsData = this;
                }
            }
        }

        /// <summary>An optional accession number for the mzML document used for storage, e.g. in PRIDE.</summary>
        /// <remarks>Optional Attribute</remarks>
        public string Accession { get; set; } // TODO: what?

        /// <summary>An optional id for the mzML document used for referencing from external files. It is recommended to use LSIDs when possible.</summary>
        /// <remarks>Optional Attribute</remarks>
        public string Id { get; set; } // TODO: enforce validity?

        /// <summary>The version of this mzML document.</summary>
        /// <remarks>Required Attribute</remarks>
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

        /// <remarks>min 1, max unbounded</remarks>
        public CVType[] cv
        {
            get { return cvs.ToArray(); }
            set { cvs = value.ToList(); } // TODO: recreate the CVTranslator when setting... (Add a hook that runs whenever it is modified?)
        }

        /// <summary>The number of CV definitions in this mzML file.</summary>
        /// <remarks>Required Attribute</remarks>
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
    public partial class CVInfo
    {
        /// <summary>
        /// The short label to be used as a reference tag with which to refer to this particular Controlled Vocabulary source description
        /// (e.g., from the cvLabel attribute, in CVParamType elements).
        /// </summary>
        /// <remarks>Required Attribute</remarks>
        public string Id { get; set; } // TODO: enforce requirement, uniqueness among dataset

        /// <summary>The usual name for the resource (e.g. The PSI-MS Controlled Vocabulary).</summary>
        /// <remarks>Required Attribute</remarks>
        public string FullName { get; set; } // TODO: enforce requirement

        /// <summary>The version of the CV from which the referred-to terms are drawn.</summary>
        /// <remarks>Optional Attribute</remarks>
        public string Version { get; set; } // TODO: enforce requirement

        /// <summary>The URI for the resource.</summary>
        /// <remarks>Required Attribute</remarks>
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

        /// <remarks>min 1, max unbounded</remarks>
        public DataProcessingType[] dataProcessing
        {
            get { return dataProcessingField.ToArray(); }
            set { dataProcessingField = value.ToList(); }
        }

        /// <summary>The number of DataProcessingTypes in this mzML file.</summary>
        /// <remarks>Required Attribute</remarks>
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
    public partial class DataProcessingInfo
    {
        private MSDataList<ProcessingMethodInfo> _processingMethods;

        /// <summary>Description of the default peak processing method.</summary>
        /// <remarks>
        /// <para>
        /// This element describes the base method used in the generation of a particular mzML file.
        /// Variable methods should be described in the appropriate acquisition section - if
        /// no acquisition-specific details are found, then this information serves as the default.
        /// </para>
        /// <para>
        /// <remarks>min 1, max unbounded</remarks>
        /// </para>
        /// </remarks>
        public MSDataList<ProcessingMethodInfo> ProcessingMethods // TODO: enforce quantity
        {
            get => this._processingMethods;
            set
            {
                this._processingMethods = value;
                if (this._processingMethods != null)
                {
                    this._processingMethods.MsData = this.MsData;
                }
            }
        }

        /// <summary>A unique identifier for this data processing that is unique across all DataProcessingTypes.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Id { get; set; } // TODO: enforce requirement, uniqueness among DataProcessing in dataset
    }

    /// <summary>
    /// mzML ProcessingMethodType
    /// </summary>
    public partial class ProcessingMethodInfo : ParamGroup
    {
        /// <summary>This attributes allows a series of consecutive steps to be placed in the correct order.</summary>
        /// <remarks>Required Attribute</remarks>
        public uint Order { get; set; } // TODO: enforce requirement, 1 to n contiguous

        /// <summary>This attribute must reference the 'id' of the appropriate SoftwareType.</summary>
        /// <remarks>Required Attribute</remarks>
        /// <returns>IDREF</returns>
        public string SoftwareRef { get; set; } // TODO: enforce requirement, validity
    }

    /// <summary>
    /// mzML ParamGroupType
    /// </summary>
    /// <remarks>Structure allowing the use of a controlled (cvParam) or uncontrolled vocabulary (userParam), or a reference to a predefined set of these in this mzML file (paramGroupRef).</remarks>
    public partial class ParamGroup
    {
        private MSDataList<ReferenceableParamGroupRef> _referenceableParamGroupRefs;
        private MSDataList<CVParam> _cVParams;
        private MSDataList<UserParam> _userParams;

        /// <summary>Referenceable Param Group Refs</summary>
        /// <remarks>min 0, max unbounded</remarks>
        public MSDataList<ReferenceableParamGroupRef> ReferenceableParamGroupRefs
        {
            get => this._referenceableParamGroupRefs;
            set
            {
                this._referenceableParamGroupRefs = value;
                if (this._referenceableParamGroupRefs != null)
                {
                    this._referenceableParamGroupRefs.MsData = this.MsData;
                }
            }
        }
        //public ParamGroupType[] referenceableParamGroupRef
        //{
        //  get { return this._paramGroupRefs.ToArray(); }
        //  set { this._paramGroupRefs = value.ToList(); }
        //}

        /// <summary>CV Params</summary>
        /// <remarks>min 0, max unbounded</remarks>
        public MSDataList<CVParam> CVParams
        {
            get => this._cVParams;
            set
            {
                this._cVParams = value;
                if (this._cVParams != null)
                {
                    this._cVParams.MsData = this.MsData;
                }
            }
        }

        /// <summary>User Params</summary>
        /// <remarks>min 0, max unbounded</remarks>
        public MSDataList<UserParam> UserParams
        {
            get => this._userParams;
            set
            {
                this._userParams = value;
                if (this._userParams != null)
                {
                    this._userParams.MsData = this.MsData;
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

        /// <remarks>min 1, max unbounded</remarks>
        public ReferenceableParamGroupType[] referenceableParamGroup
        {
            get { return referenceableParamGroupField.ToArray(); }
            set { referenceableParamGroupField = value.ToList(); }
        }

        /// <summary>The number of ParamGroups defined in this mzML file.</summary>
        /// <remarks>Required Attribute</remarks>
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
    public partial class ReferenceableParamGroup
    {
        private MSDataList<CVParam> _cVParams;
        private MSDataList<UserParam> _userParams;

        /// <summary>CV Params</summary>
        /// <remarks>min 0, max unbounded</remarks>
        public MSDataList<CVParam> CVParams
        {
            get => this._cVParams;
            set
            {
                this._cVParams = value;
                if (this._cVParams != null)
                {
                    this._cVParams.MsData = this.MsData;
                }
            }
        }

        /// <summary>User Params</summary>
        /// <remarks>min 0, max unbounded</remarks>
        public MSDataList<UserParam> UserParams
        {
            get => this._userParams;
            set
            {
                this._userParams = value;
                if (this._userParams != null)
                {
                    this._userParams.MsData = this.MsData;
                }
            }
        }

        /// <summary>The identifier with which to reference this ReferenceableParamGroup.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Id { get; set; } // TODO: enforce requirement, uniqueness
    }

    /// <summary>
    /// mzML ReferenceableParamGroupRefType
    /// </summary>
    /// <remarks>A reference to a previously defined ParamGroup, which is a reusable container of one or more cvParams.</remarks>
    public partial class ReferenceableParamGroupRef
    {
        /// <summary>Reference to the id attribute in a referenceableParamGroup.</summary>
        /// <remarks>Required Attribute</remarks>
        /// <returns>IDREF</returns>
        public string @Ref { get; set; } // TODO: enforce requirement, validity
    }

    /// <summary>
    /// mzML CVParamType
    /// </summary>
    /// <remarks>This element holds additional data or annotation. Only controlled values are allowed here.</remarks>
    public partial class CVParam
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
        /// <summary>
        /// CV term enum
        /// </summary>
        public CV.CV.CVID Cvid { get; set; }

        //[System.Xml.Serialization.XmlIgnore]
        //public CV.CV.CVID UnitCvid { get; set; }

        /// <summary>A reference to the CV 'id' attribute as defined in the cvList in this mzML file.</summary>
        /// <remarks>Required Attribute</remarks>
        /// <returns>IDREF</returns>
        internal string CVRef
        {
            get => this.MsData.CvTranslator.ConvertOboCVRef(CV.CV.TermData[this.Cvid].CVRef);
            set => this._cvRef = this.MsData.CvTranslator.ConvertFileCVRef(value);
        }

        /// <summary>The accession number of the referred-to term in the named resource (e.g.: MS:000012).</summary>
        /// <remarks>Required Attribute</remarks>
        internal string Accession
        {
            get => CV.CV.TermData[this.Cvid].Id;
            // TODO: change this return to a value mapped from the cvid
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

        /// <summary>The actual name for the parameter, from the referred-to controlled vocabulary. This should be the preferred name associated with the specified accession number.</summary>
        /// <remarks>Required Attribute</remarks>
        internal string Name => CV.CV.TermData[this.Cvid].Name;

        /// <summary>The value for the parameter; may be absent if not appropriate, or a numeric or symbolic value, or may itself be CV (legal values for a parameter should be enumerated and defined in the ontology).</summary>
        /// <remarks>Optional Attribute</remarks>
        public string Value
        {
            get => this._value;
            set => this._value = value;
        } // TODO: Perform validation of the value according to CVID/UnitCVID?

        /*
        /// <remarks>If a unit term is referenced, this attribute must refer to the CV 'id' attribute defined in the cvList in this mzML file.</remarks>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>IDREF</returns>
        internal string UnitCVRef
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

        /*
        /// <remarks>An optional CV accession number for the unit term associated with the value, if any (e.g., 'UO:0000266' for 'electron volt').</remarks>
        /// <remarks>Optional Attribute</remarks>
        internal string UnitAccession
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

        /*
        /// <remarks>An optional CV name for the unit accession number, if any (e.g., 'electron volt' for 'UO:0000266' ).</remarks>
        /// <remarks>Optional Attribute</remarks>
        internal string UnitName
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
    public partial class UserParam
    {
        //private string _unitAccession;
        //private string _unitName;
        //private string _unitCvRef = "??";
        //private CV.CV.CVID _unitCvid;
        //private CVType _unitCVRef;

        //public CV.CV.CVID UnitCvid { get; set; }

        /// <summary>The name for the parameter.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Name { get; set; }

        /// <summary>The data type of the parameter, where appropriate (e.g.: xsd:float).</summary>
        /// <remarks>Optional Attribute</remarks>
        public string Type { get; set; }

        /// <summary>The value for the parameter, where appropriate.</summary>
        /// <remarks>Optional Attribute</remarks>
        public string Value { get; set; }

        /*
        /// <remarks>An optional CV accession number for the unit term associated with the value, if any (e.g., 'UO:0000266' for 'electron volt').</remarks>
        /// <remarks>Optional Attribute</remarks>
        internal string UnitAccession
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

        /*
        /// <remarks>An optional CV name for the unit accession number, if any (e.g., 'electron volt' for 'UO:0000266' ).</remarks>
        /// <remarks>Optional Attribute</remarks>
        internal string UnitName
        {
            get
            {
                return CV.CV.TermData[this.UnitCvid].Name;
                //return this._unitName;
            }
            //set { this._unitName = value; }
        }*/

        /*
        /// <remarks>If a unit term is referenced, this attribute must refer to the CV 'id' attribute defined in the cvList in this mzML file.</remarks>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>IDREF</returns>
        internal string UnitCVRef
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
    /// ParamBase
    /// </summary>
    /// <remarks>Base type for CVParam and UserParam to reduce code duplication.</remarks>
    public partial class ParamBase
    {
        //private string _unitAccession;
        //private string _unitName;
        private string _unitCvRef;
        //private CVType _UnitCVRef;
        private CV.CV.CVID _unitCvid = CV.CV.CVID.CVID_Unknown;
        private bool _unitsSet;

        //[System.Xml.Serialization.XmlIgnore]
        /// <summary>
        /// CV term enum for the units
        /// </summary>
        public CV.CV.CVID UnitCvid
        {
            get => this._unitCvid;
            set
            {
                this._unitCvid = value;
                this._unitsSet = true;
            }
        }

        /// <summary>If a unit term is referenced, this attribute must refer to the CV 'id' attribute defined in the cvList in this mzML file.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>IDREF</returns>
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

        /// <summary>An optional CV accession number for the unit term associated with the value, if any (e.g., 'UO:0000266' for 'electron volt').</summary>
        /// <remarks>Optional Attribute</remarks>
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
                    this._unitCvid = CV.CV.TermAccessionLookup[_unitCvRef][value];
                }
                else
                {
                    this._unitCvid = CV.CV.CVID.CVID_Unknown;
                }
            } // TODO: map this to a cvid, and store the cvid, and don't store the accession
        }

        /// <summary>An optional CV name for the unit accession number, if any (e.g., 'electron volt' for 'UO:0000266' ).</summary>
        /// <remarks>Optional Attribute</remarks>
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
        private List<Precursor> precursorField;
        private string countField;

        /// <remarks>min 1, max unbounded</remarks>
        public Precursor[] precursor
        {
            get { return precursorField.ToArray(); }
            set { precursorField = value.ToList(); }
        }

        /// <summary>The number of precursor isolations in this list.</summary>
        /// <remarks>Required Attribute</remarks>
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
    public partial class Precursor
    {
        private ParamGroup _isolationWindow;
        private MSDataList<ParamGroup> _selectedIonList;
        private ParamGroup _activation;

        /// <summary>This element captures the isolation (or 'selection') window configured to isolate one or more ions.</summary>
        /// <remarks>min 0, max 1</remarks>
        public ParamGroup IsolationWindow
        {
            get => this._isolationWindow;
            set
            {
                this._isolationWindow = value;
                if (this._isolationWindow != null)
                {
                    this._isolationWindow.MsData = this.MsData;
                }
            }
        }

        /// <summary>A list of ions that were selected.</summary>
        /// <remarks>min 0, max 1</remarks>
        //public SelectedIonListType selectedIonList
        public MSDataList<ParamGroup> SelectedIonList
        {
            get => this._selectedIonList;
            set
            {
                this._selectedIonList = value;
                if (this._selectedIonList != null)
                {
                    this._selectedIonList.MsData = this.MsData;
                }
            }
        }

        /// <summary>The type and energy level used for activation.</summary>
        /// <remarks>min 1, max 1</remarks>
        public ParamGroup Activation
        {
            get => this._activation;
            set
            {
                this._activation = value;
                if (this._activation != null)
                {
                    this._activation.MsData = this.MsData;
                }
            }
        }

        /// <summary>For precursor spectra that are local to this document, this attribute must be used to reference the 'id' attribute of the spectrum corresponding to the precursor spectrum.</summary>
        /// <remarks>Optional Attribute</remarks>
        public string SpectrumRef { get; set; } // TODO: enforce validity, proper usage

        /// <summary>For precursor spectra that are external to this document, this attribute must reference the 'id' attribute of a sourceFile representing that external document.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>IDREF</returns>
        public string SourceFileRef { get; set; } // TODO: enforce validity

        /// <summary>For precursor spectra that are external to this document, this string must correspond to the 'id' attribute of a spectrum in the external document indicated by 'sourceFileRef'.</summary>
        /// <remarks>Optional Attribute</remarks>
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

        /// <remarks>min 1, max unbounded</remarks>
        public ParamGroupType[] selectedIon
        {
            get { return selectedIonField.ToArray(); }
            set { selectedIonField = value.ToList(); }
        }

        /// <summary>The number of selected precursor ions defined in this list.</summary>
        /// <remarks>Required Attribute</remarks>
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
        /// <remarks>min 2, max unbounded</remarks>
        public BinaryDataArrayType[] binaryDataArray
        {
            get { return binaryDataArrayField.ToArray(); }
            set { binaryDataArrayField = value.ToList(); }
        }

        /// <summary>The number of binary data arrays defined in this list.</summary>
        /// <remarks>Required Attribute</remarks>
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
    public partial class BinaryDataArray : ParamGroup
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
        /// <summary>
        /// Type of data in the binary data array
        /// </summary>
        public enum ArrayType : int
        {
            /// <summary>m/z data</summary>
            m_z,

            /// <summary>intensity data</summary>
            intensity,

            /// <summary>charge data</summary>
            charge,

            /// <summary>signal to noise data</summary>
            signal_to_noise,

            /// <summary>time data</summary>
            time,

            /// <summary>wavelength data</summary>
            wavelength,

            /// <summary>other data</summary>
            non_standard_data,

            /// <summary>flow rate data</summary>
            flow_rate,

            /// <summary>pressure data</summary>
            pressure,

            /// <summary>temperature data</summary>
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
        private int _arrayLength; // Only used for reading from mzML, so no external access. // TODO: is this superseded by bdaDefaultArrayLength?
        private double[] _data;
        private ArrayType _dataType;
        private int _dataWidth;
        private bool _isCompressed;

        //[System.Xml.Serialization.XmlIgnore] // can use this attribute to ignore a field/property.
        /// <summary>
        /// Array of data, in the form of doubles
        /// </summary>
        public double[] Data
        {
            get => this._data;
            set => this._data = value;
        }

        /// <summary>
        /// Type of data in the array
        /// </summary>
        public ArrayType DataType
        {
            get => this._dataType;
            set
            {
                this._dataType = value;
                // Make sure the appropriate cvParam is set...
                for (var i = 0; i < CVParams.Count; i++)
                {
                    foreach (var cvid in CV.CV.RelationsChildren[CV.CV.CVID.MS_binary_data_array])
                    {
                        if (CVParams[i].Cvid == cvid)
                        {
                            CVParams.RemoveAt(i);
                            i--;
                            break; // break out of inner loop.
                        }
                    }
                }
                var cv = new CVParam();
                cv.Cvid = _typeMap[_dataType];
                CVParams.Add(cv);
            }
        }

        /// <summary>
        /// Byte width of the data in the array - 4 bytes or 8 bytes
        /// </summary>
        public int DataWidth
        {
            get => this._dataWidth;
            set
            {
                if (value != 4 && value != 8)
                {
                    // only 4 and 8 are valid, 2 is obsolete
                    return;
                }
                this._dataWidth = value;
                // Make sure the appropriate cvParam is set...
                for (var i = 0; i < CVParams.Count; i++)
                {
                    foreach (var cvid in CV.CV.RelationsChildren[CV.CV.CVID.MS_binary_data_type])
                    {
                        if (CVParams[i].Cvid == cvid)
                        {
                            CVParams.RemoveAt(i);
                            i--;
                            break; // break out of inner loop.
                        }
                    }
                }
                var cv = new CVParam();
                cv.Cvid = CV.CV.CVID.CVID_Unknown;
                if (this._dataWidth == 4)
                {
                    cv.Cvid = CV.CV.CVID.MS_32_bit_float;
                }
                else if (this._dataWidth == 8)
                {
                    cv.Cvid = CV.CV.CVID.MS_64_bit_float;
                }
                CVParams.Add(cv);
            }
        }

        /// <summary>
        /// If the binary data form of the array is compressed
        /// </summary>
        public bool IsCompressed
        {
            get => this._isCompressed;
            set
            {
                this._isCompressed = value;
                // Make sure the appropriate cvParam is set...
                for (var i = 0; i < CVParams.Count; i++)
                {
                    foreach (var cvid in CV.CV.RelationsChildren[CV.CV.CVID.MS_binary_data_compression_type])
                    {
                        if (CVParams[i].Cvid == cvid)
                        {
                            CVParams.RemoveAt(i);
                            i--;
                            break; // break out of inner loop.
                        }
                    }
                }
                var cv = new CVParam();
                cv.Cvid = CV.CV.CVID.MS_no_compression;
                if (this._isCompressed)
                {
                    cv.Cvid = CV.CV.CVID.MS_zlib_compression;
                }
                CVParams.Add(cv);
            }
        }

        /// <summary>
        /// Length of the data array
        /// </summary>
        public int DataLength => this._data.Length;

        /// <summary>The actual base64 encoded binary data. The byte order is always 'little endian'.</summary>
        /// <returns>base64 encoded binary data</returns>
        public byte[] Binary
        {
            get => Base64Conversion.EncodeBytes(this._data, this._dataWidth, this._isCompressed);
            set => this._data = Base64Conversion.DecodeBytes(value, this._dataWidth, this._arrayLength, this._isCompressed);
        }

        /// <summary>
        /// <para>
        /// This optional attribute may override the 'defaultArrayLength' defined in SpectrumType.
        /// </para>
        /// <para>
        /// The two default arrays (m/z and intensity) should NEVER use this override option, and should
        /// therefore adhere to the 'defaultArrayLength' defined in SpectrumType. Parsing software can thus
        /// safely choose to ignore arrays of lengths different from the one defined in the 'defaultArrayLength' SpectrumType element.
        /// </para>
        /// </summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>non-negative integer</returns>
        public int ArrayLength // TODO: enforce appropriate usage
        {
            get => this._data.Length;
            // Return value solely based upon present data
            private set => this._arrayLength = value;
            // Allow setting from other constructors.
        }

        /// <summary>This optional attribute may reference the 'id' attribute of the appropriate dataProcessing.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>IDREF</returns>
        public string DataProcessingRef { get; set; } // TODO: enforce validity
    }

    /// <summary>
    /// mzML ScanListType
    /// </summary>
    /// <remarks>List and descriptions of scans.</remarks>
    public partial class ScanList : ParamGroup
    {
        private MSDataList<Scan> _scan;

        /// <summary>List of Scans</summary>
        /// <remarks>min 1, max unbounded</remarks>
        public MSDataList<Scan> Scan // TODO: enforce quantity
        {
            get => this._scan;
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
    public partial class Scan : ParamGroup
    {
        private MSDataList<ParamGroup> _scanWindowList;

        /// <summary>Container for a list of scan windows.</summary>
        /// <remarks>min 0, max 1</remarks>
        //public ScanWindowListType scanWindowList
        public MSDataList<ParamGroup> ScanWindowList
        {
            get => this._scanWindowList;
            set
            {
                this._scanWindowList = value;
                if (this._scanWindowList != null)
                {
                    this._scanWindowList.MsData = this.MsData;
                }
            }
        }

        /// <summary>For scans that are local to this document, this attribute can be used to reference the 'id' attribute of the spectrum corresponding to the scan.</summary>
        /// <remarks>Optional Attribute</remarks>
        public string SpectrumRef { get; set; } // TODO: enforce validity

        /// <summary>If this attribute is set, it must reference the 'id' attribute of a sourceFile representing the external document containing the spectrum referred to by 'externalSpectrumID'.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>IDREF</returns>
        public string SourceFileRef { get; set; } // TODO: enforce validity

        /// <summary>For scans that are external to this document, this string must correspond to the 'id' attribute of a spectrum in the external document indicated by 'sourceFileRef'.</summary>
        /// <remarks>Optional Attribute</remarks>
        public string ExternalSpectrumID { get; set; } // TODO: enforce validity

        /// <summary>This attribute can optionally reference the 'id' attribute of the appropriate instrument configuration.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>IDREF</returns>
        public string InstrumentConfigurationRef { get; set; } // TODO: enforce validity
    }

    /*
    /// <summary>
    /// mzML ScanWindowListType
    /// </summary>
    public partial class ScanWindowListType
    {
        private List<ParamGroupType> scanWindowField;
        private int countField;

        /// <summary>A range of m/z values over which the instrument scans and acquires a spectrum.</summary>
        /// <remarks>min 1, max unbounded</remarks>
        public ParamGroupType[] scanWindow
        {
            get { return scanWindowField.ToArray(); }
            set { scanWindowField = value.ToList(); }
        }

        /// <summary>The number of scan windows defined in this list.</summary>
        /// <remarks>Required Attribute</remarks>
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
    public partial class SpectrumList
    {
        private MSDataList<Spectrum> _spectra;

        /// <summary>List of Spectra</summary>
        /// <remarks>min 0, max unbounded</remarks>
        public MSDataList<Spectrum> Spectra
        {
            get => this._spectra;
            set
            {
                this._spectra = value;
                if (this._spectra != null)
                {
                    this._spectra.MsData = this.MsData;
                }
            }
        }

        /// <summary>This attribute MUST reference the 'id' of the default data processing for the spectrum list.</summary>
        /// <remarks>
        /// <para>
        /// If an acquisition does not reference any data processing, it implicitly refers to this data processing.
        /// This attribute is required because the minimum amount of data processing that any format will undergo is "conversion to mzML".
        /// </para>
        /// <para>
        /// Required Attribute
        /// </para>
        /// </remarks>
        /// <returns>IDREF</returns>
        public string DefaultDataProcessingRef { get; set; } // TODO: enforce requirement, validity
    }

    /// <summary>
    /// mzML SpectrumType
    /// </summary>
    /// <remarks>The structure that captures the generation of a peak list (including the underlying acquisitions).
    /// Also describes some of the parameters for the mass spectrometer for a given acquisition (or list of acquisitions).</remarks>
    public partial class Spectrum : ParamGroup
    {
        private ScanList _scanList;
        private MSDataList<Precursor> _precursorList;
        private MSDataList<Product> _productList;
        private MSDataList<BinaryDataArray> _binaryDataArrayList;
        private int _defaultArrayLength;

        /// <summary>Scan List</summary>
        /// <remarks>min 0, max 1</remarks>
        public ScanList ScanList
        {
            get => this._scanList;
            set
            {
                this._scanList = value;
                if (this._scanList != null)
                {
                    this._scanList.MsData = this.MsData;
                }
            }
        }

        /// <summary>Precursor List</summary>
        /// <remarks>min 0, max 1</remarks>
        public MSDataList<Precursor> PrecursorList
        {
            get => this._precursorList;
            set
            {
                this._precursorList = value;
                if (this._precursorList != null)
                {
                    this._precursorList.MsData = this.MsData;
                }
            }
        }

        /// <summary>Product List</summary>
        /// <remarks>min 0, max 1</remarks>
        public MSDataList<Product> ProductList
        {
            get => this._productList;
            set
            {
                this._productList = value;
                if (this._productList != null)
                {
                    this._productList.MsData = this.MsData;
                }
            }
        }

        /// <summary>Binary Data Array List</summary>
        /// <remarks>min 0, max 1</remarks>
        public MSDataList<BinaryDataArray> BinaryDataArrayList
        {
            get => this._binaryDataArrayList;
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

        /// <summary>The zero-based, consecutive index of  the spectrum in the SpectrumList.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Index { get; set; } // TODO: enforce requirement, 0-n contiguous values in spectrumList

        /// <summary>The native identifier for a spectrum. For unmerged native spectra or spectra from older open file formats,
        /// the format of the identifier is defined in the PSI-MS CV and referred to in the mzML header.
        /// External documents may use this identifier together with the mzML filename or accession to reference a particular spectrum.</summary>
        /// <remarks>Required Attribute</remarks>
        /// <returns>RegEx: "\S+=\S+( \S+=\S+)*"</returns>
        public string Id { get; set; } // TODO: enforce consistency? (same style?)

        /// <summary>The identifier for the spot from which this spectrum was derived, if a MALDI or similar run.</summary>
        /// <remarks>Optional Attribute</remarks>
        public string SpotID { get; set; }

        /// <summary>Default length of binary data arrays contained in this element.</summary>
        /// <remarks>Required Attribute</remarks>
        public int DefaultArrayLength
        {
            get
            {
                var mzLength = 0;
                var intensityLength = 0;
                foreach (var bda in BinaryDataArrayList)
                {
                    if (bda.DataType == BinaryDataArray.ArrayType.m_z)
                    {
                        mzLength = bda.DataLength;
                    }
                    else if (bda.DataType == BinaryDataArray.ArrayType.intensity)
                    {
                        intensityLength = bda.DataLength;
                    }
                }
                if (mzLength == intensityLength)
                {
                    this._defaultArrayLength = mzLength;
                    if (this._binaryDataArrayList != null)
                    {
                        this._binaryDataArrayList.DefaultArrayLength = this._defaultArrayLength;
                    }
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

        /// <summary>This attribute can optionally reference the 'id' of the appropriate dataProcessing.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>IDREF</returns>
        public string DataProcessingRef { get; set; } // TODO: enforce validity

        /// <summary>This attribute can optionally reference the 'id' of the appropriate sourceFile.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>IDREF</returns>
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

        /// <remarks>min 1, max unbounded</remarks>
        public ProductType[] product
        {
            get { return productField.ToArray(); }
            set { productField = value.ToList(); }
        }

        /// <summary>The number of product isolations in this list.</summary>
        /// <remarks>Required Attribute</remarks>
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
    public partial class Product
    {
        private ParamGroup _isolationWindow;

        /// <summary>This element captures the isolation (or 'selection') window configured to isolate one or more ions.</summary>
        /// <remarks>min 0, max 1</remarks>
        public ParamGroup IsolationWindow
        {
            get => this._isolationWindow;
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
    public partial class Run : ParamGroup
    {
        private SpectrumList _spectrumList;
        private ChromatogramList _chromatogramList;
        private System.DateTime _startTimeStampField;

        /// <summary>
        /// All mass spectra and the acquisitions underlying them are described and attached here.
        /// Subsidiary data arrays are also both described and attached here.
        /// </summary>
        /// <remarks>min 0, max 1</remarks>
        public SpectrumList SpectrumList
        {
            get => this._spectrumList;
            set
            {
                this._spectrumList = value;
                if (this._spectrumList != null)
                {
                    this._spectrumList.MsData = this.MsData;
                }
            }
        }

        /// <summary>All chromatograms for this run.</summary>
        /// <remarks>min 0, max 1</remarks>
        public ChromatogramList ChromatogramList
        {
            get => this._chromatogramList;
            set
            {
                this._chromatogramList = value;
                if (this._chromatogramList != null)
                {
                    this._chromatogramList.MsData = this.MsData;
                }
            }
        }

        /// <summary>A unique identifier for this run.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Id { get; set; } // TODO: enforce uniqueness

        /// <summary>
        /// This attribute must reference the 'id' of the default instrument configuration.
        /// If a scan does not reference an instrument configuration, it implicitly refers to this configuration.
        /// </summary>
        /// <remarks>Required Attribute</remarks>
        /// <returns>IDREF</returns>
        public string DefaultInstrumentConfigurationRef { get; set; } // TODO: enforce requirement, validity

        /// <summary>
        /// This attribute can optionally reference the 'id' of the default source file.
        /// If a spectrum or scan does not reference a source file and this attribute is set, then it implicitly refers to this source file.
        /// </summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>IDREF</returns>
        public string DefaultSourceFileRef { get; set; } // TODO: enforce validity

        /// <summary>This attribute must reference the 'id' of the appropriate sample.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>IDREF</returns>
        public string SampleRef { get; set; } // TODO: enforce validity

        /// <summary>The optional start timestamp of the run, in UT.</summary>
        /// <remarks>Optional Attribute</remarks>
        public System.DateTime StartTimeStamp
        {
            get => this._startTimeStampField;
            set
            {
                this._startTimeStampField = value;
                StartTimeStampSpecified = value > DateTime.MinValue;
            }
        }

        /// <summary>
        /// "Ignored" Attribute - only used to signify existence of valid value in startTimeStamp
        /// </summary>
        public bool StartTimeStampSpecified { get; private set; }
    }

    /// <summary>
    /// mzML ChromatogramListType
    /// </summary>
    /// <remarks>List of chromatograms.</remarks>
    public partial class ChromatogramList
    {
        private MSDataList<Chromatogram> _chromatograms;

        /// <summary>Chromatograms</summary>
        /// <remarks>min 1, max unbounded</remarks>
        public MSDataList<Chromatogram> Chromatograms // TODO: enforce quantity
        {
            get => this._chromatograms;
            set
            {
                this._chromatograms = value;
                if (this._chromatograms != null)
                {
                    this._chromatograms.MsData = this.MsData;
                }
            }
        }

        /// <summary>
        /// This attribute MUST reference the 'id' of the default data processing for the chromatogram list.
        /// If an acquisition does not reference any data processing, it implicitly refers to this data processing.
        /// This attribute is required because the minimum amount of data processing that any format will undergo is "conversion to mzML".
        /// </summary>
        /// <remarks>Required Attribute</remarks>
        /// <returns>IDREF</returns>
        public string DefaultDataProcessingRef { get; set; } // TODO: enforce requirement, validity of reference in dataset
    }

    /// <summary>
    /// mzML ChromatogramType
    /// </summary>
    /// <remarks>A single Chromatogram</remarks>
    public partial class Chromatogram : ParamGroup
    {
        private Precursor _precursor;
        private Product _product;
        private MSDataList<BinaryDataArray> _binaryDataArrayList;
        private int _defaultArrayLength;

        /// <summary>Precursor</summary>
        /// <remarks>min 0, max 1</remarks>
        public Precursor Precursor
        {
            get => this._precursor;
            set
            {
                this._precursor = value;
                if (this._precursor != null)
                {
                    this._precursor.MsData = this.MsData;
                }
            }
        }

        /// <summary>Product</summary>
        /// <remarks>min 0, max 1</remarks>
        public Product Product
        {
            get => this._product;
            set
            {
                this._product = value;
                if (this._product != null)
                {
                    this._product.MsData = this.MsData;
                }
            }
        }

        /// <summary>Binary Data Array List</summary>
        /// <remarks>min 1, max 1</remarks>
        public MSDataList<BinaryDataArray> BinaryDataArrayList // TODO: enforce requirement
        {
            get => this._binaryDataArrayList;
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

        /// <summary>The zero-based index for this chromatogram in the chromatogram list</summary>
        /// <remarks>Required Attribute</remarks>
        public string Index { get; set; } // TODO: enforce requirement, 0-n contiguous values in chromatogram list.

        /// <summary>A unique identifier for this chromatogram</summary>
        /// <remarks>Required Attribute</remarks>
        public string Id { get; set; } // TODO: enforce requirement

        /// <summary>Default length of binary data arrays contained in this element.</summary>
        /// <remarks>Required Attribute</remarks>
        public int DefaultArrayLength
        {
            get
            {
                var timeLength = 0;
                var intensityLength = 0;
                foreach (var bda in BinaryDataArrayList)
                {
                    if (bda.DataType == BinaryDataArray.ArrayType.time)
                    {
                        timeLength = bda.DataLength;
                    }
                    else if (bda.DataType == BinaryDataArray.ArrayType.intensity)
                    {
                        intensityLength = bda.DataLength;
                    }
                }
                if (timeLength == intensityLength)
                {
                    this._defaultArrayLength = timeLength;
                    if (this._binaryDataArrayList != null)
                    {
                        this._binaryDataArrayList.DefaultArrayLength = this._defaultArrayLength;
                    }
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

        /// <summary>This attribute can optionally reference the 'id' of the appropriate dataProcessing.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>IDREF</returns>
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

        /// <remarks>min 1, max unbounded</remarks>
        public ScanSettingsType[] scanSettings
        {
            get { return scanSettingsField.ToArray(); }
            set { scanSettingsField = value.ToList(); }
        }

        /// <summary>The number of AcquisitionType elements in this list.</summary>
        /// <remarks>Required Attribute</remarks>
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
    public partial class ScanSettingsInfo : ParamGroup
    {
        private MSDataList<SourceFileRef> _sourceFileRefList;
        private MSDataList<ParamGroup> _targetList;

        /// <summary>List with the source files containing the acquisition settings.</summary>
        /// <remarks>min 0, max 1</remarks>
        public MSDataList<SourceFileRef> SourceFileRefList
        {
            get => this._sourceFileRefList;
            set
            {
                this._sourceFileRefList = value;
                if (this._sourceFileRefList != null)
                {
                    this._sourceFileRefList.MsData = this.MsData;
                }
            }
        }

        /// <summary>Target list (or 'inclusion list') configured prior to the run.</summary>
        /// <remarks>min 0, max 1</remarks>
        //public TargetListType targetList
        public MSDataList<ParamGroup> TargetList
        {
            get => this._targetList;
            set
            {
                this._targetList = value;
                if (this._targetList != null)
                {
                    this._targetList.MsData = this.MsData;
                }
            }
        }

        /// <summary>A unique identifier for this acquisition setting.</summary>
        /// <remarks>Required Attribute</remarks>
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

        /// <summary>Reference to a previously defined sourceFile.</summary>
        /// <remarks>min 0, max unbounded</remarks>
        public SourceFileRefType[] sourceFileRef
        {
            get { return sourceFileRefField.ToArray(); }
            set { sourceFileRefField = value.ToList(); }
        }

        /// <summary>This number of source files referenced in this list.</summary>
        /// <remarks>Required Attribute</remarks>
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
    public partial class SourceFileRef
    {
        /// <summary>This attribute must reference the 'id' of the appropriate sourceFile.</summary>
        /// <remarks>Required Attribute</remarks>
        /// <returns>IDREF</returns>
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

        /// <remarks>min 1, max unbounded</remarks>
        public ParamGroupType[] target
        {
            get { return targetField.ToArray(); }
            set { targetField = value.ToList(); }
        }

        /// <summary>The number of TargetType elements in this list.</summary>
        /// <remarks>Required Attribute</remarks>
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

        /// <summary>A piece of software.</summary>
        /// <remarks>min 1, max unbounded</remarks>
        public SoftwareType[] software
        {
            get { return softwareField.ToArray(); }
            set { softwareField = value.ToList(); }
        }

        /// <summary>The number of software names defined in this mzML file.</summary>
        /// <remarks>Required Attribute</remarks>
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
    public partial class SoftwareInfo : ParamGroup
    {
        /// <summary>An identifier for this software that is unique across all SoftwareTypes.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Id { get; set; } // TODO: enforce requirement and uniqueness in dataset

        /// <summary>The software version.</summary>
        /// <remarks>Required Attribute</remarks>
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

        /// <remarks>min 1, max unbounded</remarks>
        public InstrumentConfigurationType[] instrumentConfiguration
        {
            get { return instrumentConfigurationField.ToArray(); }
            set { instrumentConfigurationField = value.ToList(); }
        }

        /// <summary>The number of instrument configurations present in this list.</summary>
        /// <remarks>Required Attribute</remarks>
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
    public partial class InstrumentConfigurationInfo : ParamGroup
    {
        private ComponentList _componentList;
        private SoftwareRef _softwareRef;

        /// <summary>Component List</summary>
        /// <remarks>min 0, max 1</remarks>
        public ComponentList ComponentList
        {
            get => this._componentList;
            set
            {
                this._componentList = value;
                if (this._componentList != null)
                {
                    this._componentList.MsData = this.MsData;
                }
            }
        }

        /// <summary>Software Ref</summary>
        /// <remarks>min 0, max 1</remarks>
        public SoftwareRef SoftwareRef
        {
            get => this._softwareRef;
            set
            {
                this._softwareRef = value;
                if (this._softwareRef != null)
                {
                    this._softwareRef.MsData = this.MsData;
                }
            }
        }

        /// <summary>An identifier for this instrument configuration.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Id { get; set; } // TODO: enforce requirement

        /// <summary>Optional Attribute</summary>
        /// <returns>IDREF</returns>
        public string ScanSettingsRef { get; set; }
    }

    /// <summary>
    /// mzML ComponentListType
    /// </summary>
    /// <remarks>List with the different components used in the mass spectrometer. At least one source, one mass analyzer and one detector need to be specified.</remarks>
    public partial class ComponentList
    {
        private MSDataList<SourceComponent> _source;
        private MSDataList<AnalyzerComponent> _analyzer;
        private MSDataList<DetectorComponent> _detector;

        /// <summary>A source component.</summary>
        /// <remarks>min 1, max unbounded</remarks>
        public MSDataList<SourceComponent> Sources // TODO: enforce quantity
        {
            get => this._source;
            set
            {
                this._source = value;
                if (this._source != null)
                {
                    this._source.MsData = this.MsData;
                }
            }
        }

        /// <summary>A mass analyzer (or mass filter) component.</summary>
        /// <remarks>min 1, max unbounded</remarks>
        public MSDataList<AnalyzerComponent> Analyzers // TODO: enforce quantity
        {
            get => this._analyzer;
            set
            {
                this._analyzer = value;
                if (this._analyzer != null)
                {
                    this._analyzer.MsData = this.MsData;
                }
            }
        }

        /// <summary>A detector component.</summary>
        /// <remarks>min 1, max unbounded</remarks>
        public MSDataList<DetectorComponent> Detectors //TODO: enforce quantity
        {
            get => this._detector;
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
    public partial class Component : ParamGroup
    {
        /// <summary>This attribute must be used to indicate the order in which the components
        /// are encountered from source to detector (e.g., in a Q-TOF, the quadrupole would
        /// have the lower order number, and the TOF the higher number of the two).</summary>
        /// <remarks>Required Attribute</remarks>
        public int Order { get; set; } // TODO: restrict to ordering from 1 to n in dataset, no gaps.
    }

    /// <summary>
    /// mzML SourceComponentType
    /// </summary>
    /// <remarks>This element must be used to describe a Source Component Type.
    /// This is a PRIDE3-specific modification of the core MzML schema that does not
    /// have any impact on the base schema validation.</remarks>
    public partial class SourceComponent : Component
    {
    }

    /// <summary>
    /// mzML AnalyzerComponentType
    /// </summary>
    /// <remarks>This element must be used to describe an Analyzer Component Type.
    /// This is a PRIDE3-specific modification of the core MzML schema that does not
    /// have any impact on the base schema validation.</remarks>
    public partial class AnalyzerComponent : Component
    {
    }

    /// <summary>
    /// mzML DetectorComponentType
    /// </summary>
    /// <remarks>This element must be used to describe a Detector Component Type.
    /// This is a PRIDE3-specific modification of the core MzML schema that does not
    /// have any impact on the base schema validation.</remarks>
    public partial class DetectorComponent : Component
    {
    }

    /// <summary>
    /// mzML SoftwareRefType
    /// </summary>
    /// <remarks>Reference to a previously defined software element</remarks>
    public partial class SoftwareRef
    {
        /// <summary>This attribute must be used to reference the 'id' attribute of a software element.</summary>
        /// <remarks>Required Attribute</remarks>
        /// <returns>IDREF</returns>
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

        /// <remarks>min 1, max unbounded</remarks>
        public SampleType[] sample
        {
            get { return sampleField.ToArray(); }
            set { sampleField = value.ToList(); }
        }

        /// <summary>The number of Samples defined in this mzML file.</summary>
        /// <remarks>Required Attribute</remarks>
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
    public partial class SampleInfo : ParamGroup
    {
        /// <summary>A unique identifier across the samples with which to reference this sample description.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Id { get; set; } // TODO: restrict to unique in dataset // TODO: enforce required attribute (constructor?)

        /// <summary>An optional name for the sample description, mostly intended as a quick mnemonic.</summary>
        /// <remarks>Optional Attribute</remarks>
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

        /// <remarks>min 1, max unbounded</remarks>
        public SourceFileType[] sourceFile
        {
            get { return sourceFileField.ToArray(); }
            set { sourceFileField = value.ToList(); }
        }

        /// <summary>Number of source files used in generating the instance document.</summary>
        /// <remarks>Required Attribute</remarks>
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
    public partial class SourceFileInfo : ParamGroup
    {
        /// <summary>An identifier for this file.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Id { get; set; } // TODO: enforce required attribute

        /// <summary>Name of the source file, without reference to location (either URI or local path).</summary>
        /// <remarks>Required Attribute</remarks>
        public string Name { get; set; } // TODO: enforce required attribute

        /// <summary>URI-formatted location where the file was retrieved.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Location { get; set; } // TODO: enforce required attribute
    }

    /// <summary>
    /// mzML FileDescriptionType
    /// </summary>
    /// <remarks>Information pertaining to the entire mzML file (i.e. not specific to any part of the data set) is stored here.</remarks>
    public partial class FileDescription
    {
        private ParamGroup _fileContent;
        private MSDataList<SourceFileInfo> _sourceFileList;
        private MSDataList<ParamGroup> _contact;

        /// <summary>This summarizes the different types of spectra that can be expected in the file.
        /// This is expected to aid processing software in skipping files that do not contain appropriate
        /// spectrum types for it. It should also describe the nativeID format used in the file by referring to an appropriate CV term.</summary>
        /// <remarks>min 1, max 1</remarks>
        public ParamGroup FileContentInfo // TODO: enforce quantity
        {
            get => this._fileContent;
            set
            {
                this._fileContent = value;
                if (this._fileContent != null)
                {
                    this._fileContent.MsData = this.MsData;
                }
            }
        }

        /// <summary>
        /// Source File List
        /// </summary>
        /// <remarks>min 0, max 1</remarks>
        public MSDataList<SourceFileInfo> SourceFileList
        {
            get => this._sourceFileList;
            set
            {
                this._sourceFileList = value;
                if (this._sourceFileList != null)
                {
                    this._sourceFileList.MsData = this.MsData;
                }
            }
        }

        /// <summary>
        /// Contact info
        /// </summary>
        /// <remarks>min 0, max unbounded</remarks>
        public MSDataList<ParamGroup> ContactInfo
        {
            get => this._contact;
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
