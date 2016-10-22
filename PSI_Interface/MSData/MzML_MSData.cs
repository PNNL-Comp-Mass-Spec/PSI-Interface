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
using PSI_Interface.MSData.mzML;

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
        /// <summary>
        /// Translating Constructor from a mzMLType object
        /// </summary>
        /// <param name="mzML"></param>
        public MSData(mzMLType mzML)
        {
            // Default values....
            this.CvTranslator = new CVTranslator();
            this.CVList = null;
            this.FileDescription = null;
            this.ReferenceableParamGroupList = null;
            this.SampleList = null;
            this.SoftwareList = null;
            this.ScanSettingsList = null;
            this.InstrumentConfigurationList = null;
            this.DataProcessingList = null;
            this.Run = null;

            if (mzML.cvList != null && mzML.cvList.cv != null && mzML.cvList.cv.Count > 0)
            {
                this.CVList = new MSDataList<CVInfo>();
                foreach (var cv in mzML.cvList.cv)
                {
                    this.CVList.Add(new CVInfo(cv, this));
                }
                this.CvTranslator = new CVTranslator(this.CVList);
            }
            if (mzML.fileDescription != null)
            {
                this.FileDescription = new FileDescription(mzML.fileDescription, this);
            }
            if (mzML.referenceableParamGroupList != null && mzML.referenceableParamGroupList.referenceableParamGroup != null && mzML.referenceableParamGroupList.referenceableParamGroup.Count > 0)
            {
                this.ReferenceableParamGroupList = new MSDataList<ReferenceableParamGroup>();
                foreach (var rpg in mzML.referenceableParamGroupList.referenceableParamGroup)
                {
                    this.ReferenceableParamGroupList.Add(new ReferenceableParamGroup(rpg, this));
                }
            }
            if (mzML.sampleList != null && mzML.sampleList.sample != null && mzML.sampleList.sample.Count > 0)
            {
                this.SampleList = new MSDataList<SampleInfo>();
                foreach (var s in mzML.sampleList.sample)
                {
                    this.SampleList.Add(new SampleInfo(s, this));
                }

            }
            if (mzML.softwareList != null && mzML.softwareList.software != null && mzML.softwareList.software.Count > 0)
            {
                this.SoftwareList = new MSDataList<SoftwareInfo>();
                foreach (var s in mzML.softwareList.software)
                {
                    this.SoftwareList.Add(new SoftwareInfo(s, this));
                }
            }
            if (mzML.scanSettingsList != null && mzML.scanSettingsList.scanSettings != null && mzML.scanSettingsList.scanSettings.Count > 0)
            {
                this.ScanSettingsList = new MSDataList<ScanSettingsInfo>();
                foreach (var ss in mzML.scanSettingsList.scanSettings)
                {
                    this.ScanSettingsList.Add(new ScanSettingsInfo(ss, this));
                }
            }
            if (mzML.instrumentConfigurationList != null && mzML.instrumentConfigurationList.instrumentConfiguration != null && mzML.instrumentConfigurationList.instrumentConfiguration.Count > 0)
            {
                this.InstrumentConfigurationList = new MSDataList<InstrumentConfigurationInfo>();
                foreach (var ic in mzML.instrumentConfigurationList.instrumentConfiguration)
                {
                    this.InstrumentConfigurationList.Add(new InstrumentConfigurationInfo(ic, this));
                }
            }
            if (mzML.dataProcessingList != null && mzML.dataProcessingList.dataProcessing != null && mzML.dataProcessingList.dataProcessing.Count > 0)
            {
                this.DataProcessingList = new MSDataList<DataProcessingInfo>();
                foreach (var dp in mzML.dataProcessingList.dataProcessing)
                {
                    this.DataProcessingList.Add(new DataProcessingInfo(dp, this));
                }
            }
            if (mzML.run != null)
            {
                this.Run = new Run(mzML.run, this);
            }
            this.Accession = mzML.accession;
            this.Id = mzML.id;
            this.Version = mzML.version;
        }
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
    public partial class CVInfo
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="cv"></param>
        /// <param name="instance"></param>
        public CVInfo(CVType cv, MSData instance)
            : base(instance)
        {
            this.Id = cv.id;
            this.FullName = cv.fullName;
            this.Version = cv.version;
            this.URI = cv.URI;
        }
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
    public partial class DataProcessingInfo
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="dp"></param>
        /// <param name="instance"></param>
        public DataProcessingInfo(DataProcessingType dp, MSData instance)
            : base(instance)
        {
            // Default values
            this.ProcessingMethods = null;

            if (dp.processingMethod != null && dp.processingMethod.Count > 0)
            {
                this.ProcessingMethods = new MSDataList<ProcessingMethodInfo>();
                foreach (var pm in dp.processingMethod)
                {
                    this.ProcessingMethods.Add(new ProcessingMethodInfo(pm, this.MsData));
                }
            }
            this.Id = dp.id;
        }
    }

    /// <summary>
    /// mzML ProcessingMethodType
    /// </summary>
    public partial class ProcessingMethodInfo : ParamGroup
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="pm"></param>
        /// <param name="instance"></param>
        public ProcessingMethodInfo(ProcessingMethodType pm, MSData instance)
            : base(pm, instance)
        {
            this.Order = uint.Parse(pm.order);
            this.SoftwareRef = pm.softwareRef;
        }
    }

    /// <summary>
    /// mzML ParamGroupType
    /// </summary>
    /// <remarks>Structure allowing the use of a controlled (cvParam) or uncontrolled vocabulary (userParam), or a reference to a predefined set of these in this mzML file (paramGroupRef).</remarks>
    public partial class ParamGroup
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="pg"></param>
        /// <param name="instance"></param>
        public ParamGroup(ParamGroupType pg, MSData instance)
            : base(instance)
        {
            // Default values
            this.ReferenceableParamGroupRefs = null;
            this.CVParams = null;
            this.UserParams = null;

            if (pg.referenceableParamGroupRef != null && pg.referenceableParamGroupRef.Count > 0)
            {
                this.ReferenceableParamGroupRefs = new MSDataList<ReferenceableParamGroupRef>();
                foreach (var rpg in pg.referenceableParamGroupRef)
                {
                    this.ReferenceableParamGroupRefs.Add(new ReferenceableParamGroupRef(rpg, this.MsData));
                }
            }
            if (pg.cvParam != null && pg.cvParam.Count > 0)
            {
                this.CVParams = new MSDataList<CVParam>();
                foreach (var cv in pg.cvParam)
                {
                    this.CVParams.Add(new CVParam(cv, this.MsData));
                }
            }
            if (pg.userParam != null && pg.userParam.Count > 0)
            {
                this.UserParams = new MSDataList<UserParam>();
                foreach (var up in pg.userParam)
                {
                    this.UserParams.Add(new UserParam(up, this.MsData));
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
    public partial class ReferenceableParamGroup
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="rpg"></param>
        /// <param name="instance"></param>
        public ReferenceableParamGroup(ReferenceableParamGroupType rpg, MSData instance)
            : base(instance)
        {
            // Default values
            this.CVParams = null;
            this.UserParams = null;

            if (rpg.cvParam != null && rpg.cvParam.Count > 0)
            {
                this.CVParams = new MSDataList<CVParam>();
                foreach (var cv in rpg.cvParam)
                {
                    this.CVParams.Add(new CVParam(cv, this.MsData));
                }
            }
            if (rpg.userParam != null && rpg.userParam.Count > 0)
            {
                this.UserParams = new MSDataList<UserParam>();
                foreach (var up in rpg.userParam)
                {
                    this.UserParams.Add(new UserParam(up, this.MsData));
                }
            }
            this.Id = rpg.id;
        }
    }

    /// <summary>
    /// mzML ReferenceableParamGroupRefType
    /// </summary>
    /// <remarks>A reference to a previously defined ParamGroup, which is a reusable container of one or more cvParams.</remarks>
    public partial class ReferenceableParamGroupRef
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="rpgr"></param>
        /// <param name="instance"></param>
        public ReferenceableParamGroupRef(ReferenceableParamGroupRefType rpgr, MSData instance)
            : base(instance)
        {
            this.Ref = rpgr.@ref;
        }
    }

    /// <summary>
    /// mzML CVParamType
    /// </summary>
    /// <remarks>This element holds additional data or annotation. Only controlled values are allowed here.</remarks>
    public partial class CVParam : ParamBase
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="cvp"></param>
        /// <param name="instance"></param>
        public CVParam(CVParamType cvp, MSData instance)
            : base(instance)
        {
            this.CVRef = cvp.cvRef;
            this.Accession = cvp.accession;
            //this.Name = cvp.name; // TODO: shouldn't be needed.
            this.Value = cvp.value;
            this.UnitCVRef = cvp.unitCvRef;
            this.UnitAccession = cvp.unitAccession;
            //this.UnitName = cvp.unitName; // TODO: shouldn't be needed.
        }
    }

    /// <summary>
    /// mzML UserParamType
    /// </summary>
    /// <remarks>Uncontrolled user parameters (essentially allowing free text). 
    /// Before using these, one should verify whether there is an appropriate 
    /// CV term available, and if so, use the CV term instead</remarks>
    public partial class UserParam : ParamBase
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="up"></param>
        /// <param name="instance"></param>
        public UserParam(UserParamType up, MSData instance)
            : base(instance)
        {
            this.Name = up.name;
            this.Type = up.type;
            this.Value = up.value;
            this.UnitCVRef = up.unitCvRef;
            //this._unitCvRef = this.MsData.CvTranslator.ConvertFileCVRef(up.unitCvRef);
            this.UnitAccession = up.unitAccession;
            //this.UnitName = up.unitName; // TODO: shouldn't be needed.
        }
    }

    /// <summary>
    /// ParamBase
    /// </summary>
    /// <remarks>Base type for CVParam and UserParam to reduce code duplication.</remarks>
    public partial class ParamBase
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="instance"></param>
        public ParamBase(MSData instance) // TODO: Is there a good way to pass data into this from mzML?
            : base(instance)
        {
            //this.Name = up.name;
            //this.Type = up.type;
            //this.Value = up.value;
            this._unitsSet = false;
            //this.UnitCVRef = up.unitCvRef;
            ////this._unitCvRef = this.MsData.CvTranslator.ConvertFileCVRef(up.unitCvRef);
            //this.UnitAccession = up.unitAccession;
            ////this.UnitName = up.unitName; // TODO: shouldn't be needed.
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

        /// min 1, max unbounded
        public Precursor[] precursor
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
    /// mzML Precursor
    /// </summary>
    /// <remarks>The method of precursor ion selection and activation</remarks>
    public partial class Precursor
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="p"></param>
        /// <param name="instance"></param>
        public Precursor(PrecursorType p, MSData instance)
            : base(instance)
        {
            // Default values
            this.IsolationWindow = null;
            this.SelectedIonList = null;
            this.Activation = null;

            if (p.isolationWindow != null)
            {
                this.IsolationWindow = new ParamGroup(p.isolationWindow, this.MsData);
            }
            if (p.selectedIonList != null && p.selectedIonList.selectedIon != null && p.selectedIonList.selectedIon.Count > 0)
            {
                this.SelectedIonList = new MSDataList<ParamGroup>();
                foreach (var si in p.selectedIonList.selectedIon)
                {
                    this.SelectedIonList.Add(new ParamGroup(si, this.MsData));
                }
            }
            if (p.activation != null)
            {
                this.Activation = new ParamGroup(p.activation, this.MsData);
            }
            this.SpectrumRef = p.spectrumRef;
            this.SourceFileRef = p.sourceFileRef;
            this.ExternalSpectrumID = p.externalSpectrumID;
        }
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
    public partial class BinaryDataArray : ParamGroup
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="bda"></param>
        /// <param name="instance"></param>
        /// <param name="defaultArrayLength"></param>
        public BinaryDataArray(BinaryDataArrayType bda, MSData instance, int defaultArrayLength) // TODO: will need another parameter to pass in the default array length....
            : base(bda, instance)
        {
            this.DataProcessingRef = bda.dataProcessingRef;
            if (bda.arrayLength != null)
            {
                this.ArrayLength = int.Parse(bda.arrayLength);
            }
            else
            {
                //this._arrayLength = this.BdaDefaultArrayLength;
                this._arrayLength = defaultArrayLength;
            }
            this._isCompressed = false;
            this._dataWidth = 4;
            foreach (var cv in CVParams)
            {
                if (CV.CV.RelationsChildren[CV.CV.CVID.MS_binary_data_array].Contains(cv.Cvid))
                {
                    if (_revTypeMap.ContainsKey(cv.Cvid))
                    {
                        this._dataType = _revTypeMap[cv.Cvid];
                    }
                }
                if (CV.CV.RelationsChildren[CV.CV.CVID.MS_binary_data_type].Contains(cv.Cvid))
                {
                    if (cv.Cvid == CV.CV.CVID.MS_16_bit_float_OBSOLETE)
                    {
                        this._dataWidth = 2;
                    }
                    else if (cv.Cvid == CV.CV.CVID.MS_32_bit_float)
                    {
                        this._dataWidth = 4;
                    }
                    else if (cv.Cvid == CV.CV.CVID.MS_64_bit_float)
                    {
                        this._dataWidth = 8;
                    }
                }
                if (CV.CV.RelationsChildren[CV.CV.CVID.MS_binary_data_compression_type].Contains(cv.Cvid))
                {
                    if (cv.Cvid == CV.CV.CVID.MS_no_compression)
                    {
                        this._isCompressed = false;
                    }
                    else if (cv.Cvid == CV.CV.CVID.MS_zlib_compression)
                    {
                        this._isCompressed = true;
                    }
                }
            }
            this.Binary = bda.binary;
        }
    }

    /// <summary>
    /// mzML ScanListType
    /// </summary>
    /// <remarks>List and descriptions of scans.</remarks>
    public partial class ScanList : ParamGroup
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="sl"></param>
        /// <param name="instance"></param>
        public ScanList(ScanListType sl, MSData instance)
            : base(sl, instance)
        {
            // Default value
            this.Scan = null;

            if (sl.scan != null && sl.scan.Count > 0)
            {
                this.Scan = new MSDataList<Scan>();
                foreach (var s in sl.scan)
                {
                    this.Scan.Add(new Scan(s, this.MsData));
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
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="s"></param>
        /// <param name="instance"></param>
        public Scan(ScanType s, MSData instance)
            : base(s, instance)
        {
            // Default value
            this.ScanWindowList = null;

            if (s.scanWindowList != null && s.scanWindowList.scanWindow != null && s.scanWindowList.scanWindow.Count > 0)
            {
                this.ScanWindowList = new MSDataList<ParamGroup>();
                foreach (var sw in s.scanWindowList.scanWindow)
                {
                    this.ScanWindowList.Add(new ParamGroup(sw, this.MsData));
                }
            }
            this.SpectrumRef = s.spectrumRef;
            this.SourceFileRef = s.sourceFileRef;
            this.ExternalSpectrumID = s.externalSpectrumID;
            this.InstrumentConfigurationRef = s.instrumentConfigurationRef;
        }
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
    public partial class SpectrumList
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="sl"></param>
        /// <param name="instance"></param>
        public SpectrumList(SpectrumListType sl, MSData instance)
            : base(instance)
        {
            // Default value
            this.Spectra = null;

            if (sl.spectrum != null && sl.spectrum.Count > 0)
            {
                this.Spectra = new MSDataList<Spectrum>();
                foreach (var s in sl.spectrum)
                {
                    this.Spectra.Add(new Spectrum(s, this.MsData));
                }
            }
            this.DefaultDataProcessingRef = sl.defaultDataProcessingRef;
        }
    }

    /// <summary>
    /// mzML SpectrumType
    /// </summary>
    /// <remarks>The structure that captures the generation of a peak list (including the underlying acquisitions). 
    /// Also describes some of the parameters for the mass spectrometer for a given acquisition (or list of acquisitions).</remarks>
    public partial class Spectrum : ParamGroup
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="s"></param>
        /// <param name="instance"></param>
        public Spectrum(SpectrumType s, MSData instance)
            : base(s, instance)
        {
            // Default values
            this.ScanList = null;
            this.PrecursorList = null;
            this.BinaryDataArrayList = null;

            if (s.scanList != null)
            {
                this.ScanList = new ScanList(s.scanList, this.MsData);
            }
            if (s.precursorList != null && s.precursorList.precursor != null && s.precursorList.precursor.Count > 0)
            {
                this.PrecursorList = new MSDataList<Precursor>();
                foreach (var p in s.precursorList.precursor)
                {
                    this.PrecursorList.Add(new Precursor(p, this.MsData));
                }
            }
            if (s.binaryDataArrayList != null && s.binaryDataArrayList.binaryDataArray != null && s.binaryDataArrayList.binaryDataArray.Count > 0)
            {
                this.BinaryDataArrayList = new MSDataList<BinaryDataArray>();
                foreach (var bda in s.binaryDataArrayList.binaryDataArray)
                {
                    this.BinaryDataArrayList.Add(new BinaryDataArray(bda, this.MsData, s.defaultArrayLength));
                }
            }
            this.Index = s.index;
            this.Id = s.id;
            this.SpotID = s.spotID;
            this.DefaultArrayLength = s.defaultArrayLength;
            this.DataProcessingRef = s.dataProcessingRef;
            this.SourceFileRef = s.sourceFileRef;
        }
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
    public partial class Product
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="p"></param>
        /// <param name="instance"></param>
        public Product(ProductType p, MSData instance)
            : base(instance)
        {
            // Default value
            this.IsolationWindow = null;

            if (p.isolationWindow != null)
            {
                this.IsolationWindow = new ParamGroup(p.isolationWindow, this.MsData);
            }
        }
    }

    /// <summary>
    /// mzML RunType
    /// </summary>
    /// <remarks>A run in mzML should correspond to a single, consecutive and coherent set of scans on an instrument.</remarks>
    public partial class Run : ParamGroup
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="r"></param>
        /// <param name="instance"></param>
        public Run(RunType r, MSData instance)
            : base(r, instance)
        {
            // Default values
            this.SpectrumList = null;
            this.ChromatogramList = null;

            if (r.spectrumList != null)
            {
                this.SpectrumList = new SpectrumList(r.spectrumList, this.MsData);
            }
            if (r.chromatogramList != null)
            {
                this.ChromatogramList = new ChromatogramList(r.chromatogramList, this.MsData);
            }
            this.Id = r.id;
            this.DefaultInstrumentConfigurationRef = r.defaultInstrumentConfigurationRef;
            this.DefaultSourceFileRef = r.defaultSourceFileRef;
            this.SampleRef = r.sampleRef;
            this.StartTimeStamp = r.startTimeStamp;
            this.StartTimeStampSpecified = r.startTimeStampSpecified;
        }
    }

    /// <summary>
    /// mzML ChromatogramListType
    /// </summary>
    /// <remarks>List of chromatograms.</remarks>
    public partial class ChromatogramList
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="cl"></param>
        /// <param name="instance"></param>
        public ChromatogramList(ChromatogramListType cl, MSData instance)
            : base(instance)
        {
            // Default value
            this.Chromatograms = null;

            if (cl.chromatogram != null && cl.chromatogram.Count > 0)
            {
                this.Chromatograms = new MSDataList<Chromatogram>();
                foreach (var c in cl.chromatogram)
                {
                    this.Chromatograms.Add(new Chromatogram(c, this.MsData));
                }
            }
            this.DefaultDataProcessingRef = cl.defaultDataProcessingRef;
        }
    }

    /// <summary>
    /// mzML ChromatogramType
    /// </summary>
    /// <remarks>A single Chromatogram</remarks>
    public partial class Chromatogram : ParamGroup
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="c"></param>
        /// <param name="instance"></param>
        public Chromatogram(ChromatogramType c, MSData instance)
            : base(c, instance)
        {
            // Default values
            this.Precursor = null;
            this.Product = null;
            this.BinaryDataArrayList = null;

            if (c.precursor != null)
            {
                this.Precursor = new Precursor(c.precursor, this.MsData);
            }
            if (c.product != null)
            {
                this.Product = new Product(c.product, this.MsData);
            }
            if (c.binaryDataArrayList != null && c.binaryDataArrayList.binaryDataArray != null && c.binaryDataArrayList.binaryDataArray.Count > 0)
            {
                this.BinaryDataArrayList = new MSDataList<BinaryDataArray>();
                foreach (var bda in c.binaryDataArrayList.binaryDataArray)
                {
                    this.BinaryDataArrayList.Add(new BinaryDataArray(bda, this.MsData, c.defaultArrayLength));
                }
            }
            this.Index = c.index;
            this.Id = c.id;
            this.DefaultArrayLength = c.defaultArrayLength;
            this.DataProcessingRef = c.dataProcessingRef;
        }
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
    public partial class ScanSettingsInfo : ParamGroup
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="ss"></param>
        /// <param name="instance"></param>
        public ScanSettingsInfo(ScanSettingsType ss, MSData instance)
            : base(ss, instance)
        {
            // Default values
            this.SourceFileRefList = null;
            this.TargetList = null;

            if (ss.sourceFileRefList != null && ss.sourceFileRefList.sourceFileRef != null && ss.sourceFileRefList.sourceFileRef.Count > 0)
            {
                this.SourceFileRefList = new MSDataList<SourceFileRef>();
                foreach (var sfr in ss.sourceFileRefList.sourceFileRef)
                {
                    this.SourceFileRefList.Add(new SourceFileRef(sfr, this.MsData));
                }
            }
            if (ss.targetList != null && ss.targetList.target != null && ss.targetList.target.Count > 0)
            {
                this.TargetList = new MSDataList<ParamGroup>();
                foreach (var t in ss.targetList.target)
                {
                    this.TargetList.Add(new ParamGroup(t, this.MsData));
                }
            }
            this.Id = ss.id;
        }
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
    public partial class SourceFileRef
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="sfr"></param>
        /// <param name="instance"></param>
        public SourceFileRef(SourceFileRefType sfr, MSData instance)
            : base(instance)
        {
            this.Ref = sfr.@ref;
        }
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
    public partial class SoftwareInfo : ParamGroup
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="s"></param>
        /// <param name="instance"></param>
        public SoftwareInfo(SoftwareType s, MSData instance)
            : base(s, instance)
        {
            this.Id = s.id;
            this.Version = s.version;
        }
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
    public partial class InstrumentConfigurationInfo : ParamGroup
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="ic"></param>
        /// <param name="instance"></param>
        public InstrumentConfigurationInfo(InstrumentConfigurationType ic, MSData instance)
            : base(ic, instance)
        {
            // Default values
            this.ComponentList = null;
            this.SoftwareRef = null;

            if (ic.componentList != null)
            {
                this.ComponentList = new ComponentList(ic.componentList, this.MsData);
            }
            if (ic.softwareRef != null)
            {
                this.SoftwareRef = new SoftwareRef(ic.softwareRef, this.MsData);
            }
            this.Id = ic.id;
            this.ScanSettingsRef = ic.scanSettingsRef;
        }
    }

    /// <summary>
    /// mzML ComponentListType
    /// </summary>
    /// <remarks>List with the different components used in the mass spectrometer. At least one source, one mass analyzer and one detector need to be specified.</remarks>
    public partial class ComponentList
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="cl"></param>
        /// <param name="instance"></param>
        public ComponentList(ComponentListType cl, MSData instance)
            : base(instance)
        {
            // Default values
            this.Sources = null;
            this.Analyzers = null;
            this.Detectors = null;

            if (cl.source != null && cl.source.Count > 0)
            {
                this.Sources = new MSDataList<SourceComponent>();
                foreach (var s in cl.source)
                {
                    this.Sources.Add(new SourceComponent(s, this.MsData));
                }
            }
            if (cl.analyzer != null && cl.analyzer.Count > 0)
            {
                this.Analyzers = new MSDataList<AnalyzerComponent>();
                foreach (var a in cl.analyzer)
                {
                    this.Analyzers.Add(new AnalyzerComponent(a, this.MsData));
                }
            }
            if (cl.detector != null && cl.detector.Count > 0)
            {
                this.Detectors = new MSDataList<DetectorComponent>();
                foreach (var d in cl.detector)
                {
                    this.Detectors.Add(new DetectorComponent(d, this.MsData));
                }
            }
        }
    }

    /// <summary>
    /// mzML ComponentType
    /// </summary>
    public partial class Component : ParamGroup
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="c"></param>
        /// <param name="instance"></param>
        public Component(ComponentType c, MSData instance)
            : base(c, instance)
        {
            this.Order = c.order;
        }
    }

    /// <summary>
    /// mzML SourceComponentType
    /// </summary>
    /// <remarks>This element must be used to describe a Source Component Type. 
    /// This is a PRIDE3-specific modification of the core MzML schema that does not 
    /// have any impact on the base schema validation.</remarks>
    public partial class SourceComponent : Component
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="instance"></param>
        public SourceComponent(SourceComponentType sc, MSData instance)
            : base(sc, instance)
        { }
    }

    /// <summary>
    /// mzML AnalyzerComponentType
    /// </summary>
    /// <remarks>This element must be used to describe an Analyzer Component Type. 
    /// This is a PRIDE3-specific modification of the core MzML schema that does not 
    /// have any impact on the base schema validation.</remarks>
    public partial class AnalyzerComponent : Component
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="instance"></param>
        public AnalyzerComponent(AnalyzerComponentType ac, MSData instance)
            : base(ac, instance)
        { }
    }

    /// <summary>
    /// mzML DetectorComponentType
    /// </summary>
    /// <remarks>This element must be used to describe a Detector Component Type. 
    /// This is a PRIDE3-specific modification of the core MzML schema that does not 
    /// have any impact on the base schema validation.</remarks>
    public partial class DetectorComponent : Component
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="instance"></param>
        public DetectorComponent(DetectorComponentType dc, MSData instance)
            : base(dc, instance)
        { }
    }

    /// <summary>
    /// mzML SoftwareRefType
    /// </summary>
    /// <remarks>Reference to a previously defined software element</remarks>
    public partial class SoftwareRef
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="sr"></param>
        /// <param name="instance"></param>
        public SoftwareRef(SoftwareRefType sr, MSData instance)
            : base(instance)
        {
            this.Ref = sr.@ref;
        }
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
    public partial class SampleInfo : ParamGroup
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="s"></param>
        /// <param name="instance"></param>
        public SampleInfo(SampleType s, MSData instance)
            : base(s, instance)
        {
            this.Id = s.id;
            this.Name = s.name;
        }
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
    public partial class SourceFileInfo : ParamGroup
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="sf"></param>
        /// <param name="instance"></param>
        public SourceFileInfo(SourceFileType sf, MSData instance)
            : base(sf, instance)
        {
            this.Id = sf.id;
            this.Name = sf.name;
            this.Location = sf.location;
        }
    }

    /// <summary>
    /// mzML FileDescriptionType
    /// </summary>
    /// <remarks>Information pertaining to the entire mzML file (i.e. not specific to any part of the data set) is stored here.</remarks>
    public partial class FileDescription
    {
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="fd"></param>
        /// <param name="instance"></param>
        public FileDescription(FileDescriptionType fd, MSData instance)
            : base(instance)
        {
            // Default values
            this.FileContentInfo = null;
            this.SourceFileList = null;
            this.ContactInfo = null;

            if (fd.fileContent != null)
            {
                this.FileContentInfo = new ParamGroup(fd.fileContent, this.MsData);
            }
            if (fd.sourceFileList != null && fd.sourceFileList.sourceFile != null && fd.sourceFileList.sourceFile.Count > 0)
            {
                this.SourceFileList = new MSDataList<SourceFileInfo>();
                foreach (var sf in fd.sourceFileList.sourceFile)
                {
                    this.SourceFileList.Add(new SourceFileInfo(sf, this.MsData));
                }
            }
            if (fd.contact != null && fd.contact.Count > 0)
            {
                this.ContactInfo = new MSDataList<ParamGroup>();
                foreach (var pg in fd.contact)
                {
                    this.ContactInfo.Add(new ParamGroup(pg, this.MsData));
                }
            }
        }
    }
}
