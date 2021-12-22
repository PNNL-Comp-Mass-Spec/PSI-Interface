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

using System.Collections.Generic;
using PSI_Interface.CV;

// ReSharper disable RedundantExtendsListEntry
// ReSharper disable UnusedMember.Global

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
        // Ignore Spelling: proteomics

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="createTranslator"></param>
        public MSData(bool createTranslator = true)
        {
            this.CvTranslator = null;
            if (createTranslator)
            {
                //this.CvTranslator = new CVTranslator();
                this.DefaultCV();
            }
            this.CVList = null;
            this.FileDescription = null;
            this.ReferenceableParamGroupList = null;
            this.SampleList = null;
            this.SoftwareList = null;
            this.ScanSettingsList = null;
            this.InstrumentConfigurationList = null;
            this.DataProcessingList = null;
            this.Run = null;
            this.Accession = null;
            this.Id = null;
            this.Version = null;
        }

        /// <summary>
        /// Configure the default CV library
        /// </summary>
        public void DefaultCV()
        {
            this.CVList = new MSDataList<CVInfo>();
            foreach (var cv in CV.CV.CVInfoList)
            {
                if (string.Equals(cv.Id, "pato", System.StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(cv.Id, "unimod", System.StringComparison.OrdinalIgnoreCase))
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
                //countField = null;
            }
        }
    }
    */
    /// <summary>
    /// mzML CVType
    /// </summary>
    /// <remarks>Information about an ontology or CV source and a short 'lookup' tag to refer to.</remarks>
    public partial class CVInfo : MSDataInternalTypeAbstract
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CVInfo()
        {
            this.Id = null;
            this.FullName = null;
            this.Version = null;
            this.URI = null;
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
    public partial class DataProcessingInfo : MSDataInternalTypeAbstract
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DataProcessingInfo()
        {
            this.ProcessingMethods = null;
            this.Id = null;
        }
    }

    /// <summary>
    /// mzML ProcessingMethodType
    /// </summary>
    public partial class ProcessingMethodInfo : ParamGroup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ProcessingMethodInfo()
        {
            this.Order = 0;
            this.SoftwareRef = null;
        }
    }

    /// <summary>
    /// mzML ParamGroupType
    /// </summary>
    /// <remarks>Structure allowing the use of a controlled (cvParam) or uncontrolled vocabulary (userParam), or a reference to a predefined set of these in this mzML file (paramGroupRef).</remarks>
    public partial class ParamGroup : MSDataInternalTypeAbstract
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ParamGroup()
        {
            this.ReferenceableParamGroupRefs = null;
            this.CVParams = null;
            this.UserParams = null;
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
    public partial class ReferenceableParamGroup : MSDataInternalTypeAbstract
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReferenceableParamGroup()
        {
            this.CVParams = null;
            this.UserParams = null;
            this.Id = null;
        }
    }

    /// <summary>
    /// mzML ReferenceableParamGroupRefType
    /// </summary>
    /// <remarks>A reference to a previously defined ParamGroup, which is a reusable container of one or more cvParams.</remarks>
    public partial class ReferenceableParamGroupRef : MSDataInternalTypeAbstract
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReferenceableParamGroupRef()
        {
            this.Ref = null;
        }
    }

    /// <summary>
    /// mzML CVParamType
    /// </summary>
    /// <remarks>This element holds additional data or annotation. Only controlled values are allowed here.</remarks>
    public partial class CVParam : ParamBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CVParam()
        {
            //this.CVRef = null;
            this._cvRef = "??";
            this.Cvid = CV.CV.CVID.CVID_Unknown;
            //this.Accession = null;
            //this.Name = null;
            this.Value = null;
            //this.UnitCVRef = null;
            //this._unitCvRef = "??";
            //this.UnitCvid = CV.CV.CVID.CVID_Unknown;
            //this.UnitAccession = null;
            //this.UnitName = null;
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
        /// Constructor
        /// </summary>
        public UserParam()
        {
            this.Name = null;
            this.Type = null;
            this.Value = null;
            //this.UnitCVRef = null;
            //this._unitCvRef = "??";
            //this.UnitCvid = CV.CV.CVID.CVID_Unknown;
            //this.UnitAccession = null;
            //this.UnitName = null;
        }
    }

    /// <summary>
    /// ParamBase
    /// </summary>
    /// <remarks>Base type for CVParam and UserParam to reduce code duplication.</remarks>
    public partial class ParamBase : MSDataInternalTypeAbstract
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ParamBase()
        {
            //this.Value = null;
            //this.UnitCVRef = null;
            this._unitsSet = false;
            this._unitCvRef = null;
            this._unitCvid = CV.CV.CVID.CVID_Unknown;
            //this.UnitAccession = null;
            //this.UnitName = null;
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
    public partial class Precursor : MSDataInternalTypeAbstract
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Precursor()
        {
            this.IsolationWindow = null;
            this.SelectedIonList = null;
            this.Activation = null;
            this.SpectrumRef = null;
            this.SourceFileRef = null;
            this.ExternalSpectrumID = null;
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
        /// Constructor
        /// </summary>
        public BinaryDataArray()
        {
            this.DataProcessingRef = null;
            this.ArrayLength = 0;
            this._arrayLength = 0;
            this._isCompressed = false;
            this._dataWidth = 8;
            this.Data = null;
        }
    }

    /// <summary>
    /// mzML ScanListType
    /// </summary>
    /// <remarks>List and descriptions of scans.</remarks>
    public partial class ScanList : ParamGroup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ScanList()
        {
            this.Scan = null;
        }
    }

    /// <summary>
    /// mzML ScanType
    /// </summary>
    /// <remarks>Scan or acquisition from original raw file used to create this peak list, as specified in sourceF</remarks>
    public partial class Scan : ParamGroup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Scan()
        {
            this.ScanWindowList = null;
            this.SpectrumRef = null;
            this.SourceFileRef = null;
            this.ExternalSpectrumID = null;
            this.InstrumentConfigurationRef = null;
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
    public partial class SpectrumList : MSDataInternalTypeAbstract
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SpectrumList()
        {
            this.Spectra = null;
            this.DefaultDataProcessingRef = null;
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
        /// Constructor
        /// </summary>
        public Spectrum()
        {
            this.ScanList = null;
            this.PrecursorList = null;
            this.BinaryDataArrayList = null;
            this.Index = null;
            this.Id = null;
            this.SpotID = null;
            //this.DefaultArrayLength = s.defaultArrayLength; // TODO: Fix this.
            this.DataProcessingRef = null;
            this.SourceFileRef = null;
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
    public partial class Product : MSDataInternalTypeAbstract
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Product()
        {
            this.IsolationWindow = null;
        }
    }

    /// <summary>
    /// mzML RunType
    /// </summary>
    /// <remarks>A run in mzML should correspond to a single, consecutive and coherent set of scans on an instrument.</remarks>
    public partial class Run : ParamGroup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Run()
        {
            this.SpectrumList = null;
            this.ChromatogramList = null;
            this.Id = null;
            this.DefaultInstrumentConfigurationRef = null;
            this.DefaultSourceFileRef = null;
            this.SampleRef = null;
            this.StartTimeStamp = System.DateTime.Now;
            this.StartTimeStampSpecified = false;
        }
    }

    /// <summary>
    /// mzML ChromatogramListType
    /// </summary>
    /// <remarks>List of chromatograms.</remarks>
    public partial class ChromatogramList : MSDataInternalTypeAbstract
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ChromatogramList()
        {
            this.Chromatograms = null;
            this.DefaultDataProcessingRef = null;
        }
    }

    /// <summary>
    /// mzML ChromatogramType
    /// </summary>
    /// <remarks>A single Chromatogram</remarks>
    public partial class Chromatogram : ParamGroup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Chromatogram()
        {
            this.Precursor = null;
            this.Product = null;
            this.BinaryDataArrayList = null;
            this.Index = null;
            this.Id = null;
            //this.DefaultArrayLength = c.defaultArrayLength; // TODO: fix appropriately
            this.DataProcessingRef = null;
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
        /// Constructor
        /// </summary>
        public ScanSettingsInfo()
        {
            this.SourceFileRefList = null;
            this.TargetList = null;
            this.Id = null;
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
    public partial class SourceFileRef : MSDataInternalTypeAbstract
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SourceFileRef()
        {
            this.Ref = null;
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
        /// Constructor
        /// </summary>
        public SoftwareInfo()
        {
            this.Id = null;
            this.Version = null;
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
        /// Constructor
        /// </summary>
        public InstrumentConfigurationInfo()
        {
            this.ComponentList = null;
            this.SoftwareRef = null;
            this.Id = null;
            this.ScanSettingsRef = null;
        }
    }

    /// <summary>
    /// mzML ComponentListType
    /// </summary>
    /// <remarks>List with the different components used in the mass spectrometer. At least one source, one mass analyzer and one detector need to be specified.</remarks>
    public partial class ComponentList : MSDataInternalTypeAbstract
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ComponentList()
        {
            this.Sources = null;
            this.Analyzers = null;
            this.Detectors = null;
        }
    }

    /// <summary>
    /// mzML ComponentType
    /// </summary>
    public partial class Component : ParamGroup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Component()
        {
            this.Order = 0;
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
        /// Constructor
        /// </summary>
        public SourceComponent()
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
        /// Constructor
        /// </summary>
        public AnalyzerComponent()
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
        /// Constructor
        /// </summary>
        public DetectorComponent()
        { }
    }

    /// <summary>
    /// mzML SoftwareRefType
    /// </summary>
    /// <remarks>Reference to a previously defined software element</remarks>
    public partial class SoftwareRef : MSDataInternalTypeAbstract
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SoftwareRef()
        {
            this.Ref = null;
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
        /// Constructor
        /// </summary>
        public SampleInfo()
        {
            this.Id = null;
            this.Name = null;
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
        /// Constructor
        /// </summary>
        public SourceFileInfo()
        {
            this.Id = null;
            this.Name = null;
            this.Location = null;
        }
    }

    /// <summary>
    /// mzML FileDescriptionType
    /// </summary>
    /// <remarks>Information pertaining to the entire mzML file (i.e. not specific to any part of the data set) is stored here.</remarks>
    public partial class FileDescription : MSDataInternalTypeAbstract
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FileDescription()
        {
            this.FileContentInfo = null;
            this.SourceFileList = null;
            this.ContactInfo = null;
        }
    }
}
