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

// ReSharper disable RedundantExtendsListEntry

namespace PSI_Interface.MSData
{
    // ReSharper disable once CommentTypo
    // Ignore Spelling: bda, cvp, Referenceable, rpg, rpgr, sfr

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
            CvTranslator = new CVTranslator();
            CVList = null;
            FileDescription = null;
            ReferenceableParamGroupList = null;
            SampleList = null;
            SoftwareList = null;
            ScanSettingsList = null;
            InstrumentConfigurationList = null;
            DataProcessingList = null;
            Run = null;

            if (mzML.cvList?.cv?.Count > 0)
            {
                CVList = new MSDataList<CVInfo>();

                foreach (var cv in mzML.cvList.cv)
                {
                    CVList.Add(new CVInfo(cv, this));
                }
                CvTranslator = new CVTranslator(CVList);
            }

            if (mzML.fileDescription != null)
            {
                FileDescription = new FileDescription(mzML.fileDescription, this);
            }

            if (mzML.referenceableParamGroupList?.referenceableParamGroup?.Count > 0)
            {
                ReferenceableParamGroupList = new MSDataList<ReferenceableParamGroup>();

                foreach (var rpg in mzML.referenceableParamGroupList.referenceableParamGroup)
                {
                    ReferenceableParamGroupList.Add(new ReferenceableParamGroup(rpg, this));
                }
            }

            if (mzML.sampleList?.sample?.Count > 0)
            {
                SampleList = new MSDataList<SampleInfo>();

                foreach (var s in mzML.sampleList.sample)
                {
                    SampleList.Add(new SampleInfo(s, this));
                }
            }

            if (mzML.softwareList?.software?.Count > 0)
            {
                SoftwareList = new MSDataList<SoftwareInfo>();

                foreach (var s in mzML.softwareList.software)
                {
                    SoftwareList.Add(new SoftwareInfo(s, this));
                }
            }

            if (mzML.scanSettingsList?.scanSettings?.Count > 0)
            {
                ScanSettingsList = new MSDataList<ScanSettingsInfo>();

                foreach (var ss in mzML.scanSettingsList.scanSettings)
                {
                    ScanSettingsList.Add(new ScanSettingsInfo(ss, this));
                }
            }

            if (mzML.instrumentConfigurationList?.instrumentConfiguration?.Count > 0)
            {
                InstrumentConfigurationList = new MSDataList<InstrumentConfigurationInfo>();

                foreach (var ic in mzML.instrumentConfigurationList.instrumentConfiguration)
                {
                    InstrumentConfigurationList.Add(new InstrumentConfigurationInfo(ic, this));
                }
            }

            if (mzML.dataProcessingList?.dataProcessing?.Count > 0)
            {
                DataProcessingList = new MSDataList<DataProcessingInfo>();

                foreach (var dp in mzML.dataProcessingList.dataProcessing)
                {
                    DataProcessingList.Add(new DataProcessingInfo(dp, this));
                }
            }

            if (mzML.run != null)
            {
                Run = new Run(mzML.run, this);
            }

            Accession = mzML.accession;
            Id = mzML.id;
            Version = mzML.version;
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
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="cv"></param>
        /// <param name="instance"></param>
        public CVInfo(CVType cv, MSData instance)
            : base(instance)
        {
            Id = cv.id;
            FullName = cv.fullName;
            Version = cv.version;
            URI = cv.URI;
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
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="dp"></param>
        /// <param name="instance"></param>
        public DataProcessingInfo(DataProcessingType dp, MSData instance)
            : base(instance)
        {
            // Default values
            ProcessingMethods = null;

            if (dp.processingMethod?.Count > 0)
            {
                ProcessingMethods = new MSDataList<ProcessingMethodInfo>();

                foreach (var pm in dp.processingMethod)
                {
                    ProcessingMethods.Add(new ProcessingMethodInfo(pm, MsData));
                }
            }
            Id = dp.id;
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
            Order = uint.Parse(pm.order);
            SoftwareRef = pm.softwareRef;
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
            ReferenceableParamGroupRefs = null;
            CVParams = null;
            UserParams = null;

            if (pg.referenceableParamGroupRef?.Count > 0)
            {
                ReferenceableParamGroupRefs = new MSDataList<ReferenceableParamGroupRef>();

                foreach (var rpg in pg.referenceableParamGroupRef)
                {
                    ReferenceableParamGroupRefs.Add(new ReferenceableParamGroupRef(rpg, MsData));
                }
            }

            if (pg.cvParam?.Count > 0)
            {
                CVParams = new MSDataList<CVParam>();

                foreach (var cv in pg.cvParam)
                {
                    CVParams.Add(new CVParam(cv, MsData));
                }
            }

            if (pg.userParam?.Count > 0)
            {
                UserParams = new MSDataList<UserParam>();

                foreach (var up in pg.userParam)
                {
                    UserParams.Add(new UserParam(up, MsData));
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
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="rpg"></param>
        /// <param name="instance"></param>
        public ReferenceableParamGroup(ReferenceableParamGroupType rpg, MSData instance)
            : base(instance)
        {
            // Default values
            CVParams = null;
            UserParams = null;

            if (rpg.cvParam?.Count > 0)
            {
                CVParams = new MSDataList<CVParam>();

                foreach (var cv in rpg.cvParam)
                {
                    CVParams.Add(new CVParam(cv, MsData));
                }
            }

            if (rpg.userParam?.Count > 0)
            {
                UserParams = new MSDataList<UserParam>();

                foreach (var up in rpg.userParam)
                {
                    UserParams.Add(new UserParam(up, MsData));
                }
            }

            Id = rpg.id;
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
            Ref = rpgr.@ref;
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
            CVRef = cvp.cvRef;
            Accession = cvp.accession;
            //this.Name = cvp.name; // TODO: shouldn't be needed.
            Value = cvp.value;
            UnitCVRef = cvp.unitCvRef;
            UnitAccession = cvp.unitAccession;
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
            Name = up.name;
            Type = up.type;
            Value = up.value;
            UnitCVRef = up.unitCvRef;
            //this._unitCvRef = this.MsData.CvTranslator.ConvertFileCVRef(up.unitCvRef);
            UnitAccession = up.unitAccession;
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
            _unitsSet = false;
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
            IsolationWindow = null;
            SelectedIonList = null;
            Activation = null;

            if (p.isolationWindow != null)
            {
                IsolationWindow = new ParamGroup(p.isolationWindow, MsData);
            }

            if (p.selectedIonList?.selectedIon?.Count > 0)
            {
                SelectedIonList = new MSDataList<ParamGroup>();

                foreach (var si in p.selectedIonList.selectedIon)
                {
                    SelectedIonList.Add(new ParamGroup(si, MsData));
                }
            }

            if (p.activation != null)
            {
                Activation = new ParamGroup(p.activation, MsData);
            }

            SpectrumRef = p.spectrumRef;
            SourceFileRef = p.sourceFileRef;
            ExternalSpectrumID = p.externalSpectrumID;
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
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="bda"></param>
        /// <param name="instance"></param>
        /// <param name="defaultArrayLength"></param>
        public BinaryDataArray(BinaryDataArrayType bda, MSData instance, int defaultArrayLength) // TODO: will need another parameter to pass in the default array length....
            : base(bda, instance)
        {
            DataProcessingRef = bda.dataProcessingRef;

            if (bda.arrayLength != null)
            {
                ArrayLength = int.Parse(bda.arrayLength);
            }
            else
            {
                //this._arrayLength = this.BdaDefaultArrayLength;
                _arrayLength = defaultArrayLength;
            }

            _isCompressed = false;
            _dataWidth = 4;

            foreach (var cv in CVParams)
            {
                if (CV.CV.RelationsChildren[CV.CV.CVID.MS_binary_data_array].Contains(cv.Cvid))
                {
                    if (_revTypeMap.TryGetValue(cv.Cvid, out var dataType))
                    {
                        _dataType = dataType;
                    }
                }

                if (CV.CV.RelationsChildren[CV.CV.CVID.MS_binary_data_type].Contains(cv.Cvid))
                {
                    if (cv.Cvid == CV.CV.CVID.MS_16_bit_float_OBSOLETE)
                    {
                        _dataWidth = 2;
                    }
                    else if (cv.Cvid == CV.CV.CVID.MS_32_bit_float)
                    {
                        _dataWidth = 4;
                    }
                    else if (cv.Cvid == CV.CV.CVID.MS_64_bit_float)
                    {
                        _dataWidth = 8;
                    }
                }

                if (CV.CV.RelationsChildren[CV.CV.CVID.MS_binary_data_compression_type].Contains(cv.Cvid))
                {
                    if (cv.Cvid == CV.CV.CVID.MS_no_compression)
                    {
                        _isCompressed = false;
                    }
                    else if (cv.Cvid == CV.CV.CVID.MS_zlib_compression)
                    {
                        _isCompressed = true;
                    }
                }
            }

            Binary = bda.binary;
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
            Scan = null;

            if (sl.scan?.Count > 0)
            {
                Scan = new MSDataList<Scan>();

                foreach (var s in sl.scan)
                {
                    Scan.Add(new Scan(s, MsData));
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
            ScanWindowList = null;

            if (s.scanWindowList?.scanWindow?.Count > 0)
            {
                ScanWindowList = new MSDataList<ParamGroup>();

                foreach (var sw in s.scanWindowList.scanWindow)
                {
                    ScanWindowList.Add(new ParamGroup(sw, MsData));
                }
            }
            SpectrumRef = s.spectrumRef;
            SourceFileRef = s.sourceFileRef;
            ExternalSpectrumID = s.externalSpectrumID;
            InstrumentConfigurationRef = s.instrumentConfigurationRef;
        }
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
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="sl"></param>
        /// <param name="instance"></param>
        public SpectrumList(SpectrumListType sl, MSData instance)
            : base(instance)
        {
            // Default value
            Spectra = null;

            if (sl.spectrum?.Count > 0)
            {
                Spectra = new MSDataList<Spectrum>();

                foreach (var s in sl.spectrum)
                {
                    Spectra.Add(new Spectrum(s, MsData));
                }
            }
            DefaultDataProcessingRef = sl.defaultDataProcessingRef;
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
            ScanList = null;
            PrecursorList = null;
            BinaryDataArrayList = null;

            if (s.scanList != null)
            {
                ScanList = new ScanList(s.scanList, MsData);
            }

            if (s.precursorList?.precursor?.Count > 0)
            {
                PrecursorList = new MSDataList<Precursor>();

                foreach (var p in s.precursorList.precursor)
                {
                    PrecursorList.Add(new Precursor(p, MsData));
                }
            }

            if (s.binaryDataArrayList?.binaryDataArray?.Count > 0)
            {
                BinaryDataArrayList = new MSDataList<BinaryDataArray>();

                foreach (var bda in s.binaryDataArrayList.binaryDataArray)
                {
                    BinaryDataArrayList.Add(new BinaryDataArray(bda, MsData, s.defaultArrayLength));
                }
            }
            Index = s.index;
            Id = s.id;
            SpotID = s.spotID;
            DefaultArrayLength = s.defaultArrayLength;
            DataProcessingRef = s.dataProcessingRef;
            SourceFileRef = s.sourceFileRef;
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
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="p"></param>
        /// <param name="instance"></param>
        public Product(ProductType p, MSData instance)
            : base(instance)
        {
            // Default value
            IsolationWindow = null;

            if (p.isolationWindow != null)
            {
                IsolationWindow = new ParamGroup(p.isolationWindow, MsData);
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
            SpectrumList = null;
            ChromatogramList = null;

            if (r.spectrumList != null)
            {
                SpectrumList = new SpectrumList(r.spectrumList, MsData);
            }

            if (r.chromatogramList != null)
            {
                ChromatogramList = new ChromatogramList(r.chromatogramList, MsData);
            }

            Id = r.id;
            DefaultInstrumentConfigurationRef = r.defaultInstrumentConfigurationRef;
            DefaultSourceFileRef = r.defaultSourceFileRef;
            SampleRef = r.sampleRef;
            StartTimeStamp = r.startTimeStamp;
            StartTimeStampSpecified = r.startTimeStampSpecified;
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
            Chromatograms = null;

            if (cl.chromatogram?.Count > 0)
            {
                Chromatograms = new MSDataList<Chromatogram>();

                foreach (var c in cl.chromatogram)
                {
                    Chromatograms.Add(new Chromatogram(c, MsData));
                }
            }
            DefaultDataProcessingRef = cl.defaultDataProcessingRef;
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
            Precursor = null;
            Product = null;
            BinaryDataArrayList = null;

            if (c.precursor != null)
            {
                Precursor = new Precursor(c.precursor, MsData);
            }

            if (c.product != null)
            {
                Product = new Product(c.product, MsData);
            }

            if (c.binaryDataArrayList?.binaryDataArray?.Count > 0)
            {
                BinaryDataArrayList = new MSDataList<BinaryDataArray>();

                foreach (var bda in c.binaryDataArrayList.binaryDataArray)
                {
                    BinaryDataArrayList.Add(new BinaryDataArray(bda, MsData, c.defaultArrayLength));
                }
            }

            Index = c.index;
            Id = c.id;
            DefaultArrayLength = c.defaultArrayLength;
            DataProcessingRef = c.dataProcessingRef;
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
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="ss"></param>
        /// <param name="instance"></param>
        public ScanSettingsInfo(ScanSettingsType ss, MSData instance)
            : base(ss, instance)
        {
            // Default values
            SourceFileRefList = null;
            TargetList = null;

            if (ss.sourceFileRefList?.sourceFileRef?.Count > 0)
            {
                SourceFileRefList = new MSDataList<SourceFileRef>();

                foreach (var sfr in ss.sourceFileRefList.sourceFileRef)
                {
                    SourceFileRefList.Add(new SourceFileRef(sfr, MsData));
                }
            }

            if (ss.targetList?.target?.Count > 0)
            {
                TargetList = new MSDataList<ParamGroup>();

                foreach (var t in ss.targetList.target)
                {
                    TargetList.Add(new ParamGroup(t, MsData));
                }
            }

            Id = ss.id;
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
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="sfr"></param>
        /// <param name="instance"></param>
        public SourceFileRef(SourceFileRefType sfr, MSData instance)
            : base(instance)
        {
            Ref = sfr.@ref;
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
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="s"></param>
        /// <param name="instance"></param>
        public SoftwareInfo(SoftwareType s, MSData instance)
            : base(s, instance)
        {
            Id = s.id;
            Version = s.version;
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
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="ic"></param>
        /// <param name="instance"></param>
        public InstrumentConfigurationInfo(InstrumentConfigurationType ic, MSData instance)
            : base(ic, instance)
        {
            // Default values
            ComponentList = null;
            SoftwareRef = null;

            if (ic.componentList != null)
            {
                ComponentList = new ComponentList(ic.componentList, MsData);
            }

            if (ic.softwareRef != null)
            {
                SoftwareRef = new SoftwareRef(ic.softwareRef, MsData);
            }

            Id = ic.id;
            ScanSettingsRef = ic.scanSettingsRef;
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
            Sources = null;
            Analyzers = null;
            Detectors = null;

            if (cl.source?.Count > 0)
            {
                Sources = new MSDataList<SourceComponent>();

                foreach (var s in cl.source)
                {
                    Sources.Add(new SourceComponent(s, MsData));
                }
            }

            if (cl.analyzer?.Count > 0)
            {
                Analyzers = new MSDataList<AnalyzerComponent>();

                foreach (var a in cl.analyzer)
                {
                    Analyzers.Add(new AnalyzerComponent(a, MsData));
                }
            }

            if (cl.detector?.Count > 0)
            {
                Detectors = new MSDataList<DetectorComponent>();

                foreach (var d in cl.detector)
                {
                    Detectors.Add(new DetectorComponent(d, MsData));
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
            Order = c.order;
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
            Ref = sr.@ref;
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
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="s"></param>
        /// <param name="instance"></param>
        public SampleInfo(SampleType s, MSData instance)
            : base(s, instance)
        {
            Id = s.id;
            Name = s.name;
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
        /// <summary>
        /// Translating Constructor from a mzMLType child object
        /// </summary>
        /// <param name="sf"></param>
        /// <param name="instance"></param>
        public SourceFileInfo(SourceFileType sf, MSData instance)
            : base(sf, instance)
        {
            Id = sf.id;
            Name = sf.name;
            Location = sf.location;
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
            FileContentInfo = null;
            SourceFileList = null;
            ContactInfo = null;

            if (fd.fileContent != null)
            {
                FileContentInfo = new ParamGroup(fd.fileContent, MsData);
            }

            if (fd.sourceFileList?.sourceFile?.Count > 0)
            {
                SourceFileList = new MSDataList<SourceFileInfo>();

                foreach (var sf in fd.sourceFileList.sourceFile)
                {
                    SourceFileList.Add(new SourceFileInfo(sf, MsData));
                }
            }

            if (fd.contact?.Count > 0)
            {
                ContactInfo = new MSDataList<ParamGroup>();

                foreach (var pg in fd.contact)
                {
                    ContactInfo.Add(new ParamGroup(pg, MsData));
                }
            }
        }
    }
}
