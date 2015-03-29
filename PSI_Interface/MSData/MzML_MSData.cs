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
        public MSData(PSI_Interface.MSData.mzML.mzMLType mzML)
        {
            // Default values....
            this.CVList = null;
            this.FileDescription = null;
            this.ReferenceableParamGroupList = null;
            this.SampleList = null;
            this.SoftwareList = null;
            this.ScanSettingsList = null;
            this.InstrumentConfigurationList = null;
            this.DataProcessingList = null;
            this.Run = null;

            if (mzML.cvList != null && mzML.cvList.cv != null)
            {
                this.CVList = new MSDataList<CVType>();
                foreach (var cv in mzML.cvList.cv)
                {
                    this.CVList.Add(new CVType(cv, this));
                }
            }
            if (mzML.fileDescription != null)
            {
                this.FileDescription = new FileDescriptionType(mzML.fileDescription, this);
            }
            if (mzML.referenceableParamGroupList != null && mzML.referenceableParamGroupList.referenceableParamGroup != null)
            {
                this.ReferenceableParamGroupList = new MSDataList<ReferenceableParamGroupType>();
                foreach (var rpg in mzML.referenceableParamGroupList.referenceableParamGroup)
                {
                    this.ReferenceableParamGroupList.Add(new ReferenceableParamGroupType(rpg, this));
                }
            }
            if (mzML.sampleList != null && mzML.sampleList.sample != null)
            {
                this.SampleList = new MSDataList<SampleType>();
                foreach (var s in mzML.sampleList.sample)
                {
                    this.SampleList.Add(new SampleType(s, this));
                }

            }
            if (mzML.softwareList != null && mzML.softwareList.software != null)
            {
                this.SoftwareList = new MSDataList<SoftwareType>();
                foreach (var s in mzML.softwareList.software)
                {
                    this.SoftwareList.Add(new SoftwareType(s, this));
                }
            }
            if (mzML.scanSettingsList != null && mzML.scanSettingsList.scanSettings != null)
            {
                this.ScanSettingsList = new MSDataList<ScanSettingsType>();
                foreach (var ss in mzML.scanSettingsList.scanSettings)
                {
                    this.ScanSettingsList.Add(new ScanSettingsType(ss, this));
                }
            }
            if (mzML.instrumentConfigurationList != null && mzML.instrumentConfigurationList.instrumentConfiguration != null)
            {
                this.InstrumentConfigurationList = new MSDataList<InstrumentConfigurationType>();
                foreach (var ic in mzML.instrumentConfigurationList.instrumentConfiguration)
                {
                    this.InstrumentConfigurationList.Add(new InstrumentConfigurationType(ic, this));
                }
            }
            if (mzML.dataProcessingList != null && mzML.dataProcessingList.dataProcessing != null)
            {
                this.DataProcessingList = new MSDataList<DataProcessingType>();
                foreach (var dp in mzML.dataProcessingList.dataProcessing)
                {
                    this.DataProcessingList.Add(new DataProcessingType(dp, this));
                }
            }
            if (mzML.run != null)
            {
                this.Run = new RunType(mzML.run, this);
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
    public partial class CVType
    {
        public CVType(PSI_Interface.MSData.mzML.CVType cv, MSData instance)
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
    public partial class DataProcessingType
    {
        public DataProcessingType(PSI_Interface.MSData.mzML.DataProcessingType dp, MSData instance)
            : base(instance)
        {
            // Default values
            this.ProcessingMethod = null;

            if (dp.processingMethod != null)
            {
                this.ProcessingMethod = new MSDataList<ProcessingMethodType>();
                foreach (var pm in dp.processingMethod)
                {
                    this.ProcessingMethod.Add(new ProcessingMethodType(pm, this.MsData));
                }
            }
            this.Id = dp.id;
        }
    }

    /// <summary>
    /// mzML ProcessingMethodType
    /// </summary>
    public partial class ProcessingMethodType : ParamGroupType
    {
        public ProcessingMethodType(PSI_Interface.MSData.mzML.ProcessingMethodType pm, MSData instance)
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
    public partial class ParamGroupType
    {
        public ParamGroupType(PSI_Interface.MSData.mzML.ParamGroupType pg, MSData instance)
            : base(instance)
        {
            // Default values
            this.ReferenceableParamGroupRef = null;
            this.CVParam = null;
            this.UserParam = null;

            if (pg.referenceableParamGroupRef != null)
            {
                this.ReferenceableParamGroupRef = new MSDataList<ReferenceableParamGroupRefType>();
                foreach (var rpg in pg.referenceableParamGroupRef)
                {
                    this.ReferenceableParamGroupRef.Add(new ReferenceableParamGroupRefType(rpg, this.MsData));
                }
            }
            if (pg.cvParam != null)
            {
                this.CVParam = new MSDataList<CVParamType>();
                foreach (var cv in pg.cvParam)
                {
                    this.CVParam.Add(new CVParamType(cv, this.MsData));
                }
            }
            if (pg.userParam != null)
            {
                this.UserParam = new MSDataList<UserParamType>();
                foreach (var up in pg.userParam)
                {
                    this.UserParam.Add(new UserParamType(up, this.MsData));
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
        public ReferenceableParamGroupType(PSI_Interface.MSData.mzML.ReferenceableParamGroupType rpg, MSData instance)
            : base(instance)
        {
            // Default values
            this.CVParam = null;
            this.UserParam = null;

            if (rpg.cvParam != null)
            {
                this.CVParam = new MSDataList<CVParamType>();
                foreach (var cv in rpg.cvParam)
                {
                    this.CVParam.Add(new CVParamType(cv, this.MsData));
                }
            }
            if (rpg.userParam != null)
            {
                this.UserParam = new MSDataList<UserParamType>();
                foreach (var up in rpg.userParam)
                {
                    this.UserParam.Add(new UserParamType(up, this.MsData));
                }
            }
            this.Id = rpg.id;
        }
    }

    /// <summary>
    /// mzML ReferenceableParamGroupRefType
    /// </summary>
    /// <remarks>A reference to a previously defined ParamGroup, which is a reusable container of one or more cvParams.</remarks>
    public partial class ReferenceableParamGroupRefType
    {
        public ReferenceableParamGroupRefType(PSI_Interface.MSData.mzML.ReferenceableParamGroupRefType rpgr, MSData instance)
            : base(instance)
        {
            this.Ref = rpgr.@ref;
        }
    }

    /// <summary>
    /// mzML CVParamType
    /// </summary>
    /// <remarks>This element holds additional data or annotation. Only controlled values are allowed here.</remarks>
    public partial class CVParamType : ParamType
    {
        public CVParamType(PSI_Interface.MSData.mzML.CVParamType cvp, MSData instance)
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
    public partial class UserParamType : ParamType
    {
        public UserParamType(PSI_Interface.MSData.mzML.UserParamType up, MSData instance)
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
    /// ParamType
    /// </summary>
    /// <remarks>Base type for CVParam and UserParam to reduce code duplication.</remarks>
    public partial class ParamType
    {
        public ParamType(MSData instance) // TODO: Is there a good way to pass data into this from mzML?
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
        public PrecursorType(PSI_Interface.MSData.mzML.PrecursorType p, MSData instance)
            : base(instance)
        {
            // Default values
            this.IsolationWindow = null;
            this.SelectedIonList = null;
            this.Activation = null;

            if (p.isolationWindow != null)
            {
                this.IsolationWindow = new ParamGroupType(p.isolationWindow, this.MsData);
            }
            if (p.selectedIonList != null && p.selectedIonList.selectedIon != null)
            {
                this.SelectedIonList = new MSDataList<ParamGroupType>();
                foreach (var si in p.selectedIonList.selectedIon)
                {
                    this.SelectedIonList.Add(new ParamGroupType(si, this.MsData));
                }
            }
            if (p.activation != null)
            {
                this.Activation = new ParamGroupType(p.activation, this.MsData);
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
    public partial class BinaryDataArrayType : ParamGroupType
    {
        public BinaryDataArrayType(PSI_Interface.MSData.mzML.BinaryDataArrayType bda, MSData instance) // TODO: will need another parameter to pass in the default array length....
            : base(bda, instance)
        {
            this.DataProcessingRef = bda.dataProcessingRef;
            if (bda.arrayLength != null) // TODO: properly handle the arraylength in all cases (i.e., when converting MSData -> mzML, return "null" if the array length is not different from the default.
            {
                this.ArrayLength = uint.Parse(bda.arrayLength);
                this._expectedArrayLength = int.Parse(bda.arrayLength);
            }
            else
            {
                this.ArrayLength = 0;
                this._expectedArrayLength = this.BdaDefaultArrayLength;
            }
            this._isCompressed = false;
            this._dataWidth = 4;
            foreach (var cv in CVParam)
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
    public partial class ScanListType : ParamGroupType
    {
        public ScanListType(PSI_Interface.MSData.mzML.ScanListType sl, MSData instance)
            : base(sl, instance)
        {
            // Default value
            this.Scan = null;

            if (sl.scan != null)
            {
                this.Scan = new MSDataList<ScanType>();
                foreach (var s in sl.scan)
                {
                    this.Scan.Add(new ScanType(s, this.MsData));
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
        public ScanType(PSI_Interface.MSData.mzML.ScanType s, MSData instance)
            : base(s, instance)
        {
            // Default value
            this.ScanWindowList = null;

            if (s.scanWindowList != null && s.scanWindowList.scanWindow != null)
            {
                this.ScanWindowList = new MSDataList<ParamGroupType>();
                foreach (var sw in s.scanWindowList.scanWindow)
                {
                    this.ScanWindowList.Add(new ParamGroupType(sw, this.MsData));
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
    public partial class SpectrumListType
    {
        public SpectrumListType(PSI_Interface.MSData.mzML.SpectrumListType sl, MSData instance)
            : base(instance)
        {
            // Default value
            this.Spectrum = null;

            if (sl.spectrum != null)
            {
                this.Spectrum = new MSDataList<SpectrumType>();
                foreach (var s in sl.spectrum)
                {
                    this.Spectrum.Add(new SpectrumType(s, this.MsData));
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
    public partial class SpectrumType : ParamGroupType
    {
        public SpectrumType(PSI_Interface.MSData.mzML.SpectrumType s, MSData instance)
            : base(s, instance)
        {
            // Default values
            this.ScanList = null;
            this.PrecursorList = null;
            this.BinaryDataArrayList = null;

            if (s.scanList != null)
            {
                this.ScanList = new ScanListType(s.scanList, this.MsData);
            }
            if (s.precursorList != null && s.precursorList.precursor != null)
            {
                this.PrecursorList = new MSDataList<PrecursorType>();
                foreach (var p in s.precursorList.precursor)
                {
                    this.PrecursorList.Add(new PrecursorType(p, this.MsData));
                }
            }
            if (s.binaryDataArrayList != null && s.binaryDataArrayList.binaryDataArray != null)
            {
                this.BinaryDataArrayList = new MSDataList<BinaryDataArrayType>();
                foreach (var bda in s.binaryDataArrayList.binaryDataArray)
                {
                    this.BinaryDataArrayList.Add(new BinaryDataArrayType(bda, this.MsData));
                }
            }
            this.Index = s.index;
            this.Id = s.id;
            this.SpotID = s.spotID;
            //this.DefaultArrayLength = s.defaultArrayLength; // TODO: Fix this.
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
    public partial class ProductType
    {
        public ProductType(PSI_Interface.MSData.mzML.ProductType p, MSData instance)
            : base(instance)
        {
            // Default value
            this.IsolationWindow = null;

            if (p.isolationWindow != null)
            {
                this.IsolationWindow = new ParamGroupType(p.isolationWindow, this.MsData);
            }
        }
    }

    /// <summary>
    /// mzML RunType
    /// </summary>
    /// <remarks>A run in mzML should correspond to a single, consecutive and coherent set of scans on an instrument.</remarks>
    public partial class RunType : ParamGroupType
    {
        public RunType(PSI_Interface.MSData.mzML.RunType r, MSData instance)
            : base(r, instance)
        {
            // Default values
            this.SpectrumList = null;
            this.ChromatogramList = null;

            if (r.spectrumList != null)
            {
                this.SpectrumList = new SpectrumListType(r.spectrumList, this.MsData);
            }
            if (r.chromatogramList != null)
            {
                this.ChromatogramList = new ChromatogramListType(r.chromatogramList, this.MsData);
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
    public partial class ChromatogramListType
    {
        public ChromatogramListType(PSI_Interface.MSData.mzML.ChromatogramListType cl, MSData instance)
            : base(instance)
        {
            // Default value
            this.Chromatogram = null;

            if (cl.chromatogram != null)
            {
                this.Chromatogram = new MSDataList<ChromatogramType>();
                foreach (var c in cl.chromatogram)
                {
                    this.Chromatogram.Add(new ChromatogramType(c, this.MsData));
                }
            }
            this.DefaultDataProcessingRef = cl.defaultDataProcessingRef;
        }
    }

    /// <summary>
    /// mzML ChromatogramType
    /// </summary>
    /// <remarks>A single Chromatogram</remarks>
    public partial class ChromatogramType : ParamGroupType
    {
        public ChromatogramType(PSI_Interface.MSData.mzML.ChromatogramType c, MSData instance)
            : base(c, instance)
        {
            // Default values
            this.Precursor = null;
            this.Product = null;
            this.BinaryDataArrayList = null;

            if (c.precursor != null)
            {
                this.Precursor = new PrecursorType(c.precursor, this.MsData);
            }
            if (c.product != null)
            {
                this.Product = new ProductType(c.product, this.MsData);
            }
            if (c.binaryDataArrayList != null && c.binaryDataArrayList.binaryDataArray != null)
            {
                this.BinaryDataArrayList = new MSDataList<BinaryDataArrayType>();
                foreach (var bda in c.binaryDataArrayList.binaryDataArray)
                {
                    this.BinaryDataArrayList.Add(new BinaryDataArrayType(bda, this.MsData));
                }
            }
            this.Index = c.index;
            this.Id = c.id;
            //this.DefaultArrayLength = c.defaultArrayLength; // TODO: fix appropriately
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
    public partial class ScanSettingsType : ParamGroupType
    {
        public ScanSettingsType(PSI_Interface.MSData.mzML.ScanSettingsType ss, MSData instance)
            : base(ss, instance)
        {
            // Default values
            this.SourceFileRefList = null;
            this.TargetList = null;

            if (ss.sourceFileRefList != null && ss.sourceFileRefList.sourceFileRef != null)
            {
                this.SourceFileRefList = new MSDataList<SourceFileRefType>();
                foreach (var sfr in ss.sourceFileRefList.sourceFileRef)
                {
                    this.SourceFileRefList.Add(new SourceFileRefType(sfr, this.MsData));
                }
            }
            if (ss.targetList != null && ss.targetList.target != null)
            {
                this.TargetList = new MSDataList<ParamGroupType>();
                foreach (var t in ss.targetList.target)
                {
                    this.TargetList.Add(new ParamGroupType(t, this.MsData));
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
    public partial class SourceFileRefType
    {
        public SourceFileRefType(PSI_Interface.MSData.mzML.SourceFileRefType sfr, MSData instance)
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
    public partial class SoftwareType : ParamGroupType
    {
        public SoftwareType(PSI_Interface.MSData.mzML.SoftwareType s, MSData instance)
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
    public partial class InstrumentConfigurationType : ParamGroupType
    {
        public InstrumentConfigurationType(PSI_Interface.MSData.mzML.InstrumentConfigurationType ic, MSData instance)
            : base(ic, instance)
        {
            // Default values
            this.ComponentList = null;
            this.SoftwareRef = null;

            if (ic.componentList != null)
            {
                this.ComponentList = new ComponentListType(ic.componentList, this.MsData);
            }
            if (ic.softwareRef != null)
            {
                this.SoftwareRef = new SoftwareRefType(ic.softwareRef, this.MsData);
            }
            this.Id = ic.id;
            this.ScanSettingsRef = ic.scanSettingsRef;
        }
    }

    /// <summary>
    /// mzML ComponentListType
    /// </summary>
    /// <remarks>List with the different components used in the mass spectrometer. At least one source, one mass analyzer and one detector need to be specified.</remarks>
    public partial class ComponentListType
    {
        public ComponentListType(PSI_Interface.MSData.mzML.ComponentListType cl, MSData instance)
            : base(instance)
        {
            // Default values
            this.Source = null;
            this.Analyzer = null;
            this.Detector = null;

            if (cl.source != null)
            {
                this.Source = new MSDataList<SourceComponentType>();
                foreach (var s in cl.source)
                {
                    this.Source.Add(new SourceComponentType(s, this.MsData));
                }
            }
            if (cl.analyzer != null)
            {
                this.Analyzer = new MSDataList<AnalyzerComponentType>();
                foreach (var a in cl.analyzer)
                {
                    this.Analyzer.Add(new AnalyzerComponentType(a, this.MsData));
                }
            }
            if (cl.detector != null)
            {
                this.Detector = new MSDataList<DetectorComponentType>();
                foreach (var d in cl.detector)
                {
                    this.Detector.Add(new DetectorComponentType(d, this.MsData));
                }
            }
        }
    }

    /// <summary>
    /// mzML ComponentType
    /// </summary>
    public partial class ComponentType : ParamGroupType
    {
        public ComponentType(PSI_Interface.MSData.mzML.ComponentType c, MSData instance)
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
    public partial class SourceComponentType : ComponentType
    {
        public SourceComponentType(PSI_Interface.MSData.mzML.SourceComponentType sc, MSData instance)
            : base(sc, instance)
        { }
    }

    /// <summary>
    /// mzML AnalyzerComponentType
    /// </summary>
    /// <remarks>This element must be used to describe an Analyzer Component Type. 
    /// This is a PRIDE3-specific modification of the core MzML schema that does not 
    /// have any impact on the base schema validation.</remarks>
    public partial class AnalyzerComponentType : ComponentType
    {
        public AnalyzerComponentType(PSI_Interface.MSData.mzML.AnalyzerComponentType ac, MSData instance)
            : base(ac, instance)
        { }
    }

    /// <summary>
    /// mzML DetectorComponentType
    /// </summary>
    /// <remarks>This element must be used to describe a Detector Component Type. 
    /// This is a PRIDE3-specific modification of the core MzML schema that does not 
    /// have any impact on the base schema validation.</remarks>
    public partial class DetectorComponentType : ComponentType
    {
        public DetectorComponentType(PSI_Interface.MSData.mzML.DetectorComponentType dc, MSData instance)
            : base(dc, instance)
        { }
    }

    /// <summary>
    /// mzML SoftwareRefType
    /// </summary>
    /// <remarks>Reference to a previously defined software element</remarks>
    public partial class SoftwareRefType
    {
        public SoftwareRefType(PSI_Interface.MSData.mzML.SoftwareRefType sr, MSData instance)
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
    public partial class SampleType : ParamGroupType
    {
        public SampleType(PSI_Interface.MSData.mzML.SampleType s, MSData instance)
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
    public partial class SourceFileType : ParamGroupType
    {
        public SourceFileType(PSI_Interface.MSData.mzML.SourceFileType sf, MSData instance)
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
    public partial class FileDescriptionType
    {
        public FileDescriptionType(PSI_Interface.MSData.mzML.FileDescriptionType fd, MSData instance)
            : base(instance)
        {
            // Default values
            this.FileContent = null;
            this.SourceFileList = null;
            this.Contact = null;

            if (fd.fileContent != null)
            {
                this.FileContent = new ParamGroupType(fd.fileContent, this.MsData);
            }
            if (fd.sourceFileList != null && fd.sourceFileList.sourceFile != null)
            {
                this.SourceFileList = new MSDataList<SourceFileType>();
                foreach (var sf in fd.sourceFileList.sourceFile)
                {
                    this.SourceFileList.Add(new SourceFileType(sf, this.MsData));
                }
            }
            if (fd.contact != null)
            {
                this.Contact = new MSDataList<ParamGroupType>();
                foreach (var pg in fd.contact)
                {
                    this.Contact.Add(new ParamGroupType(pg, this.MsData));
                }
            }
        }
    }
}
