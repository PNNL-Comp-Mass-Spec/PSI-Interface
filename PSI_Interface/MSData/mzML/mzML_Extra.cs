﻿
//
// Data translation code from MSData to mzML.
//

using System;
using System.Collections.Generic;

// ReSharper disable RedundantExtendsListEntry

namespace PSI_Interface.MSData.mzML
{
    /// <summary>
    /// mzML mzMLType
    /// </summary>
    /// <remarks>This is the root element for the Proteomics Standards Initiative (PSI) mzML schema, which
    /// is intended to capture the use of a mass spectrometer, the data generated, and
    /// the initial processing of that data (to the level of the peak list)</remarks>
    public partial class mzMLType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public mzMLType()
        {
            cvList = null;
            fileDescription = null;
            referenceableParamGroupList = null;
            sampleList = null;
            softwareList = null;
            scanSettingsList = null;
            instrumentConfigurationList = null;
            dataProcessingList = null;
            run = null;
            accession = null;
            id = null;
            version = null;
        }
        /*
        /// <remarks>min 1, max 1</remarks>
        //public CVListType cvList

        /// <remarks>min 1, max 1</remarks>
        //public FileDescriptionType fileDescription

        /// <remarks>min 0, max 1</remarks>
        //public ReferenceableParamGroupListType referenceableParamGroupList

        /// <remarks>min 0, max 1</remarks>
        //public SampleListType sampleList

        /// <remarks>min 1, max 1</remarks>
        //public SoftwareListType softwareList

        /// <remarks>min 0, max 1</remarks>
        //public ScanSettingsListType scanSettingsList

        /// <remarks>min 1, max 1</remarks>
        //public InstrumentConfigurationListType instrumentConfigurationList

        /// <remarks>min 1, max 1</remarks>
        //public DataProcessingListType dataProcessingList

        /// <remarks>min 1, max 1</remarks>
        //public RunType run

        /// <summary>An optional accession number for the mzML document used for storage, e.g. in PRIDE.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string accession

        /// <summary>An optional id for the mzML document used for referencing from external files. It is recommended to use LSIDs when possible.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string id

        /// <summary>The version of this mzML document.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string version*/
    }

    /// <summary>
    /// mzML CVListType
    /// </summary>
    /// <remarks>Container for one or more controlled vocabulary definitions.</remarks>
    public partial class CVListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CVListType()
        {
            cv = new List<CVType>();
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public CVType[] cv

        /// <summary>The number of CV definitions in this mzML file.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string count*/
    }

    /// <summary>
    /// mzML CVType
    /// </summary>
    /// <remarks>Information about an ontology or CV source and a short 'lookup' tag to refer to.</remarks>
    public partial class CVType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CVType()
        {
            id = null;
            fullName = null;
            version = null;
            URI = null;
        }

        /*
        /// <remarks>The short label to be used as a reference tag with which to refer to this particular Controlled Vocabulary source description (e.g., from the cvLabel attribute, in CVParamType elements).</remarks>
        /// <remarks>Required Attribute</remarks>
        /// ID
        //public string id

        /// <summary>The usual name for the resource (e.g. The PSI-MS Controlled Vocabulary).</summary>
        /// <remarks>Required Attribute</remarks>
        //public string fullName

        /// <summary>The version of the CV from which the referred-to terms are drawn.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string version

        /// <summary>The URI for the resource.</summary>
        /// <remarks>Required Attribute</remarks>
        /// anyURI
        //public string URI*/
    }

    /// <summary>
    /// mzML DataProcessingListType
    /// </summary>
    /// <remarks>List and descriptions of data processing applied to this data.</remarks>
    public partial class DataProcessingListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DataProcessingListType()
        {
            dataProcessingField = new List<DataProcessingType>();
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public DataProcessingType[] dataProcessing

        /// <summary>The number of DataProcessingTypes in this mzML file.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string count*/
    }

    /// <summary>
    /// mzML DataProcessingType
    /// </summary>
    /// <remarks>Description of the way in which a particular software was used.</remarks>
    public partial class DataProcessingType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DataProcessingType()
        {
            processingMethodField = new List<ProcessingMethodType>();
            id = null;
        }

        /*
        /// <remarks>Description of the default peak processing method.
        /// This element describes the base method used in the generation of a particular mzML file.
        /// Variable methods should be described in the appropriate acquisition section - if
        /// no acquisition-specific details are found, then this information serves as the default.</remarks>
        /// <remarks>min 1, max unbounded</remarks>
        //public ProcessingMethodType[] processingMethod

        /// <summary>A unique identifier for this data processing that is unique across all DataProcessingTypes.</summary>
        /// <remarks>Required Attribute</remarks>
        /// ID
        //public string id*/
    }

    /// <summary>
    /// mzML ProcessingMethodType
    /// </summary>
    public partial class ProcessingMethodType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ProcessingMethodType()
        {
            order = null;
            softwareRef = null;
        }

        /*
        /// <remarks>This attributes allows a series of consecutive steps to be placed in the correct order.</remarks>
        /// <remarks>Required Attribute</remarks>
        //public string order

        /// <summary>This attribute must reference the 'id' of the appropriate SoftwareType.</summary>
        /// <remarks>Required Attribute</remarks>
        /// <returns>IDREF</returns>
        //public string softwareRef*/
    }

    /// <summary>
    /// mzML ParamGroupType
    /// </summary>
    /// <remarks>Structure allowing the use of a controlled (cvParam) or uncontrolled vocabulary (userParam), or a reference to a predefined set of these in this mzML file (paramGroupRef).</remarks>
    public partial class ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ParamGroupType()
        {
            referenceableParamGroupRefField = new List<ReferenceableParamGroupRefType>();
            cvParamField = new List<CVParamType>();
            userParamField = new List<UserParamType>();
        }

        /*
        /// <remarks>min 0, max unbounded</remarks>
        //public ReferenceableParamGroupRefType[] referenceableParamGroupRef

        /// <remarks>min 0, max unbounded</remarks>
        //public CVParamType[] cvParam

        /// <remarks>min 0, max unbounded</remarks>
        //public UserParamType[] userParam*/
    }

    /// <summary>
    /// mzML ReferenceableParamGroupListType
    /// </summary>
    /// <remarks>Container for a list of referenceableParamGroups</remarks>
    public partial class ReferenceableParamGroupListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReferenceableParamGroupListType()
        {
            referenceableParamGroupField = new List<ReferenceableParamGroupType>();
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public ReferenceableParamGroupType[] referenceableParamGroup

        /// <summary>The number of ParamGroups defined in this mzML file.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string count*/
    }

    /// <summary>
    /// mzML ReferenceableParamGroupType
    /// </summary>
    /// <remarks>A collection of CVParam and UserParam elements that can be referenced from elsewhere in this mzML document by using the 'paramGroupRef' element in that location to reference the 'id' attribute value of this element.</remarks>
    public partial class ReferenceableParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReferenceableParamGroupType()
        {
            cvParamField = new List<CVParamType>();
            userParamField = new List<UserParamType>();
            id = null;
        }

        /*
        /// <remarks>min 0, max unbounded</remarks>
        //public CVParamType[] cvParam

        /// <remarks>min 0, max unbounded</remarks>
        //public UserParamType[] userParam

        /// <summary>The identifier with which to reference this ReferenceableParamGroup.</summary>
        /// <remarks>Required Attribute</remarks>
        /// ID
        //public string id*/
    }

    /// <summary>
    /// mzML ReferenceableParamGroupRefType
    /// </summary>
    /// <remarks>A reference to a previously defined ParamGroup, which is a reusable container of one or more cvParams.</remarks>
    public partial class ReferenceableParamGroupRefType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReferenceableParamGroupRefType()
        {
            @ref = null;
        }

        /*
        /// <remarks>Reference to the id attribute in a referenceableParamGroup.</remarks>
        /// <remarks>Required Attribute</remarks>
        /// <returns>IDREF</returns>
        //public string @ref*/
    }

    /// <summary>
    /// mzML CVParamType
    /// </summary>
    /// <remarks>This element holds additional data or annotation. Only controlled values are allowed here.</remarks>
    public partial class CVParamType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CVParamType()
        {
            cvRef = null;
            accession = null;
            name = null;
            value = null;
            unitCvRef = null;
            unitAccession = null;
            unitName = null;
        }

        /*
        /// <remarks>A reference to the CV 'id' attribute as defined in the cvList in this mzML file.</remarks>
        /// <remarks>Required Attribute</remarks>
        /// <returns>IDREF</returns>
        //public string cvRef

        /// <summary>The accession number of the referred-to term in the named resource (e.g.: MS:000012).</summary>
        /// <remarks>Required Attribute</remarks>
        //public string accession

        /// <summary>The actual name for the parameter, from the referred-to controlled vocabulary. This should be the preferred name associated with the specified accession number.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string name

        /// <summary>The value for the parameter; may be absent if not appropriate, or a numeric or symbolic value, or may itself be CV (legal values for a parameter should be enumerated and defined in the ontology).</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string value

        /// <summary>If a unit term is referenced, this attribute must refer to the CV 'id' attribute defined in the cvList in this mzML file.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>IDREF</returns>
        //public string unitCvRef

        /// <summary>An optional CV accession number for the unit term associated with the value, if any (e.g., 'UO:0000266' for 'electron volt').</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string unitAccession

        /// <summary>An optional CV name for the unit accession number, if any (e.g., 'electron volt' for 'UO:0000266' ).</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string unitName*/
    }

    /// <summary>
    /// mzML UserParamType
    /// </summary>
    /// <remarks>Uncontrolled user parameters (essentially allowing free text).
    /// Before using these, one should verify whether there is an appropriate
    /// CV term available, and if so, use the CV term instead</remarks>
    public partial class UserParamType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UserParamType()
        {
            name = null;
            type = null;
            value = null;
            unitAccession = null;
            unitName = null;
            unitCvRef = null;
        }

        /*
        /// <remarks>The name for the parameter.</remarks>
        /// <remarks>Required Attribute</remarks>
        //public string name

        /// <summary>The datatype of the parameter, where appropriate (e.g.: xsd:float).</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string type

        /// <summary>The value for the parameter, where appropriate.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string value

        /// <summary>An optional CV accession number for the unit term associated with the value, if any (e.g., 'UO:0000266' for 'electron volt').</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string unitAccession

        /// <summary>An optional CV name for the unit accession number, if any (e.g., 'electron volt' for 'UO:0000266' ).</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string unitName

        /// <summary>If a unit term is referenced, this attribute must refer to the CV 'id' attribute defined in the cvList in this mzML file.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>IDREF</returns>
        //public string unitCvRef*/
    }

    /// <summary>
    /// mzML PrecursorListType
    /// </summary>
    /// <remarks>List and descriptions of precursor isolations to the spectrum currently being described, ordered.</remarks>
    public partial class PrecursorListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PrecursorListType()
        {
            precursorField = new List<PrecursorType>();
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public PrecursorType[] precursor

        /// <summary>The number of precursor isolations in this list.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string count*/
    }

    /// <summary>
    /// mzML PrecursorType
    /// </summary>
    /// <remarks>The method of precursor ion selection and activation</remarks>
    public partial class PrecursorType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PrecursorType()
        {
            isolationWindow = null;
            selectedIonList = null;
            activation = null;
            spectrumRef = null;
            sourceFileRef = null;
            externalSpectrumID = null;
        }

        /*
        /// <remarks>This element captures the isolation (or 'selection') window configured to isolate one or more ions.</remarks>
        /// <remarks>min 0, max 1</remarks>
        //public ParamGroupType isolationWindow

        /// <summary>A list of ions that were selected.</summary>
        /// <remarks>min 0, max 1</remarks>
        //public SelectedIonListType selectedIonList

        /// <summary>The type and energy level used for activation.</summary>
        /// <remarks>min 1, max 1</remarks>
        //public ParamGroupType activation

        /// <summary>For precursor spectra that are local to this document, this attribute must be used to reference the 'id' attribute of the spectrum corresponding to the precursor spectrum.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string spectrumRef

        /// <summary>For precursor spectra that are external to this document, this attribute must reference the 'id' attribute of a sourceFile representing that external document.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>IDREF</returns>
        //public string sourceFileRef

        /// <summary>For precursor spectra that are external to this document, this string must correspond to the 'id' attribute of a spectrum in the external document indicated by 'sourceFileRef'.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string externalSpectrumID*/
    }

    /// <summary>
    /// mzML SelectedIonListType
    /// </summary>
    /// <remarks>The list of selected precursor ions.</remarks>
    public partial class SelectedIonListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SelectedIonListType()
        {
            selectedIonField = new List<ParamGroupType>();
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public ParamGroupType[] selectedIon

        /// <summary>The number of selected precursor ions defined in this list.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string count*/
    }

    /// <summary>
    /// mzML BinaryDataArrayListType
    /// </summary>
    /// <remarks>List of binary data arrays.</remarks>
    public partial class BinaryDataArrayListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BinaryDataArrayListType()
        {
            binaryDataArrayField = new List<BinaryDataArrayType>();
        }

        /*
        /// <remarks>Data point arrays for default data arrays (m/z, intensity, time) and meta data arrays.
        /// Default data arrays must not have the attributes 'arrayLength' and 'dataProcessingRef'.</remarks>
        /// <remarks>min 2, max unbounded</remarks>
        //public BinaryDataArrayType[] binaryDataArray

        /// <summary>The number of binary data arrays defined in this list.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string count*/
    }

    /// <summary>
    /// mzML BinaryDataArrayType
    /// </summary>
    /// <remarks>The structure into which encoded binary data goes. Byte ordering is always little endian (Intel style).
    /// Computers using a different endian style must convert to/from little endian when writing/reading mzML</remarks>
    public partial class BinaryDataArrayType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BinaryDataArrayType()
        {
            binary = null;
            arrayLength = null;
            dataProcessingRef = null;
            encodedLength = null;
        }

        /*
        /// <remarks>The actual base64 encoded binary data. The byte order is always 'little endian'.</remarks>
        /// base64Binary
        //public byte[] binary

        /// <remarks>This optional attribute may override the 'defaultArrayLength' defined in SpectrumType.
        /// The two default arrays (m/z and intensity) should NEVER use this override option, and should
        /// therefore adhere to the 'defaultArrayLength' defined in SpectrumType. Parsing software can thus
        /// safely choose to ignore arrays of lengths different from the one defined in the 'defaultArrayLength' SpectrumType element.</remarks>
        /// <remarks>Optional Attribute</remarks>
        //public string arrayLength

        /// <summary>This optional attribute may reference the 'id' attribute of the appropriate dataProcessing.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>IDREF</returns>
        //public string dataProcessingRef

        /// <summary>The encoded length of the binary data array.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string encodedLength*/
    }

    /// <summary>
    /// mzML ScanListType
    /// </summary>
    /// <remarks>List and descriptions of scans.</remarks>
    public partial class ScanListType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ScanListType()
        {
            scanField = new List<ScanType>();
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public ScanType[] scan

        /// <summary>the number of scans defined in this list.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string count*/
    }

    /// <summary>
    /// mzML ScanType
    /// </summary>
    /// <remarks>Scan or acquisition from original raw file used to create this peak list, as specified in sourceF</remarks>
    public partial class ScanType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ScanType()
        {
            scanWindowList = null;
            spectrumRef = null;
            sourceFileRef = null;
            externalSpectrumID = null;
            instrumentConfigurationRef = null;
        }

        /*
        /// <remarks>Container for a list of scan windows.</remarks>
        /// <remarks>min 0, max 1</remarks>
        //public ScanWindowListType scanWindowList

        /// <summary>For scans that are local to this document, this attribute can be used to reference the 'id' attribute of the spectrum corresponding to the scan.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string spectrumRef

        /// <summary>If this attribute is set, it must reference the 'id' attribute of a sourceFile representing the external document containing the spectrum referred to by 'externalSpectrumID'.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>IDREF</returns>
        //public string sourceFileRef

        /// <summary>For scans that are external to this document, this string must correspond to the 'id' attribute of a spectrum in the external document indicated by 'sourceFileRef'.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string externalSpectrumID

        /// <summary>This attribute can optionally reference the 'id' attribute of the appropriate instrument configuration.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>IDREF</returns>
        //public string instrumentConfigurationRef*/
    }

    /// <summary>
    /// mzML ScanWindowListType
    /// </summary>
    public partial class ScanWindowListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ScanWindowListType()
        {
            scanWindowField = new List<ParamGroupType>();
        }

        /*
        /// <remarks>A range of m/z values over which the instrument scans and acquires a spectrum.</remarks>
        /// <remarks>min 1, max unbounded</remarks>
        //public ParamGroupType[] scanWindow

        /// <summary>The number of scan windows defined in this list.</summary>
        /// <remarks>Required Attribute</remarks>
        //public int count*/
    }

    /// <summary>
    /// mzML SpectrumListType
    /// </summary>
    /// <remarks>List and descriptions of spectra.</remarks>
    public partial class SpectrumListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SpectrumListType()
        {
            spectrumField = new List<SpectrumType>();
            defaultDataProcessingRef = null;
        }

        /*
        /// <remarks>min 0, max unbounded</remarks>
        //public SpectrumType[] spectrum

        /// <summary>The number of spectra defined in this mzML file.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string count

        /// <remarks>This attribute MUST reference the 'id' of the default data processing for the spectrum list.
        /// If an acquisition does not reference any data processing, it implicitly refers to this data processing.
        /// This attribute is required because the minimum amount of data processing that any format will undergo is "conversion to mzML".</remarks>
        /// <remarks>Required Attribute</remarks>
        /// <returns>IDREF</returns>
        //public string defaultDataProcessingRef*/
    }

    /// <summary>
    /// mzML SpectrumType
    /// </summary>
    /// <remarks>The structure that captures the generation of a peak list (including the underlying acquisitions).
    /// Also describes some of the parameters for the mass spectrometer for a given acquisition (or list of acquisitions).</remarks>
    public partial class SpectrumType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SpectrumType()
        {
            scanList = null;
            precursorList = null;
            productList = null;
            binaryDataArrayList = null;
            id = null;
            spotID = null;
            index = null;
            defaultArrayLength = 0;
            dataProcessingRef = null;
            sourceFileRef = null;
        }

        /*
        /// <remarks>min 0, max 1</remarks>
        //public ScanListType scanList

        /// <remarks>min 0, max 1</remarks>
        //public PrecursorListType PrecursorList

        /// <remarks>min 0, max 1</remarks>
        //public ProductListType ProductList

        /// <remarks>min 0, max 1</remarks>
        //public BinaryDataArrayListType BinaryDataArrayList

        /// <summary>The zero-based, consecutive index of  the spectrum in the SpectrumList.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string index

        /// <remarks>The native identifier for a spectrum. For unmerged native spectra or spectra from older open file formats,
        /// the format of the identifier is defined in the PSI-MS CV and referred to in the mzML header.
        /// External documents may use this identifier together with the mzML filename or accession to reference a particular spectrum.</remarks>
        /// <remarks>Required Attribute</remarks>
        /// Regex: "\S+=\S+( \S+=\S+)*"
        //public string id

        /// <summary>The identifier for the spot from which this spectrum was derived, if a MALDI or similar run.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string spotID

        /// <summary>Default length of binary data arrays contained in this element.</summary>
        /// <remarks>Required Attribute</remarks>
        //public int defaultArrayLength

        /// <summary>This attribute can optionally reference the 'id' of the appropriate dataProcessing.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>IDREF</returns>
        //public string dataProcessingRef

        /// <summary>This attribute can optionally reference the 'id' of the appropriate sourceFile.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>IDREF</returns>
        //public string sourceFileRef*/
    }

    /// <summary>
    /// mzML ProductListType
    /// </summary>
    /// <remarks>List and descriptions of product isolations to the spectrum currently being described, ordered.</remarks>
    public partial class ProductListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductListType()
        {
            productField = new List<ProductType>();
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public ProductType[] product

        /// <summary>The number of product isolations in this list.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string count*/
    }

    /// <summary>
    /// mzML ProductType
    /// </summary>
    /// <remarks>The method of product ion selection and activation in a precursor ion scan</remarks>
    public partial class ProductType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductType()
        {
            isolationWindow = null;
        }

        /*
        /// <remarks>This element captures the isolation (or 'selection') window configured to isolate one or more ions.</remarks>
        /// <remarks>min 0, max 1</remarks>
        //public ParamGroupType isolationWindow*/
    }

    /// <summary>
    /// mzML RunType
    /// </summary>
    /// <remarks>A run in mzML should correspond to a single, consecutive and coherent set of scans on an instrument.</remarks>
    public partial class RunType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RunType()
        {
            spectrumList = null;
            chromatogramList = null;
            id = null;
            defaultInstrumentConfigurationRef = null;
            defaultSourceFileRef = null;
            sampleRef = null;
            startTimeStamp = DateTime.Now;
            startTimeStampSpecified = false;
        }

        /*
        /// <remarks>All mass spectra and the acquisitions underlying them are described and attached here.
        /// Subsidiary data arrays are also both described and attached here.</remarks>
        /// <remarks>min 0, max 1</remarks>
        //public SpectrumListType spectrumList

        /// <summary>All chromatograms for this run.</summary>
        /// <remarks>min 0, max 1</remarks>
        //public ChromatogramListType chromatogramList

        /// <summary>A unique identifier for this run.</summary>
        /// <remarks>Required Attribute</remarks>
        /// ID
        //public string id

        /// <remarks>This attribute must reference the 'id' of the default instrument configuration.
        /// If a scan does not reference an instrument configuration, it implicitly refers to this configuration.</remarks>
        /// <remarks>Required Attribute</remarks>
        /// <returns>IDREF</returns>
        //public string defaultInstrumentConfigurationRef

        /// <remarks>This attribute can optionally reference the 'id' of the default source file.
        /// If a spectrum or scan does not reference a source file and this attribute is set, then it implicitly refers to this source file.</remarks>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>IDREF</returns>
        //public string defaultSourceFileRef

        /// <summary>This attribute must reference the 'id' of the appropriate sample.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>IDREF</returns>
        //public string sampleRef

        /// <summary>The optional start timestamp of the run, in UT.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public System.DateTime startTimeStamp

        /// "Ignored" Attribute - only used to signify existence of valid value in startTimeStamp
        //public bool startTimeStampSpecified*/
    }

    /// <summary>
    /// mzML ChromatogramListType
    /// </summary>
    /// <remarks>List of chromatograms.</remarks>
    public partial class ChromatogramListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ChromatogramListType()
        {
            chromatogramField = new List<ChromatogramType>();
            defaultDataProcessingRef = null;
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public ChromatogramType[] chromatogram

        /// <summary>The number of chromatograms defined in this mzML file.</summary>
        /// <remarks>Required Attribute</remarks>
        /// Non-negative integer
        //public string count

        /// <remarks>This attribute MUST reference the 'id' of the default data processing for the chromatogram list.
        /// If an acquisition does not reference any data processing, it implicitly refers to this data processing.
        /// This attribute is required because the minimum amount of data processing that any format will undergo is "conversion to mzML".</remarks>
        /// <remarks>Required Attribute</remarks>
        /// <returns>IDREF</returns>
        //public string defaultDataProcessingRef*/
    }

    /// <summary>
    /// mzML ChromatogramType
    /// </summary>
    /// <remarks>A single Chromatogram</remarks>
    public partial class ChromatogramType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ChromatogramType()
        {
            precursor = null;
            product = null;
            binaryDataArrayList = null;
            index = null;
            id = null;
            defaultArrayLength = 0;
            dataProcessingRef = null;
        }

        /*
        /// <remarks>min 0, max 1</remarks>
        //public PrecursorType precursor

        /// <remarks>min 0, max 1</remarks>
        //public ProductType product

        /// <remarks>min 1, max 1</remarks>
        //public BinaryDataArrayListType BinaryDataArrayList

        /// <summary>The zero-based index for this chromatogram in the chromatogram List</summary>
        /// <remarks>Required Attribute</remarks>
        //public string index

        /// <summary>A unique identifier for this chromatogram</summary>
        /// <remarks>Required Attribute</remarks>
        //public string id

        /// <summary>Default length of binary data arrays contained in this element.</summary>
        /// <remarks>Required Attribute</remarks>
        //public int defaultArrayLength

        /// <summary>This attribute can optionally reference the 'id' of the appropriate dataProcessing.</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>IDREF</returns>
        //public string dataProcessingRef*/
    }

    /// <summary>
    /// mzML ScanSettingListType
    /// </summary>
    /// <remarks>List with the descriptions of the acquisition settings applied prior to the start of data acquisition.</remarks>
    public partial class ScanSettingsListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ScanSettingsListType()
        {
            scanSettingsField = new List<ScanSettingsType>();
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public ScanSettingsType[] scanSettings

        /// <summary>The number of AcquisitionType elements in this list.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string count*/
    }

    /// <summary>
    /// mzML ScanSettingsType
    /// </summary>
    /// <remarks>Description of the acquisition settings of the instrument prior to the start of the run.</remarks>
    public partial class ScanSettingsType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ScanSettingsType()
        {
            sourceFileRefList = null;
            targetList = null;
            id = null;
        }

        /*
        /// <remarks>List with the source files containing the acquisition settings.</remarks>
        /// <remarks>min 0, max 1</remarks>
        //public SourceFileRefListType sourceFileRefList

        /// <summary>Target list (or 'inclusion list') configured prior to the run.</summary>
        /// <remarks>min 0, max 1</remarks>
        //public TargetListType targetList

        /// <summary>A unique identifier for this acquisition setting.</summary>
        /// <remarks>Required Attribute</remarks>
        /// ID
        //public string id*/
    }

    /// <summary>
    /// mzML SourceFileRefListType
    /// </summary>
    public partial class SourceFileRefListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SourceFileRefListType()
        {
            sourceFileRefField = new List<SourceFileRefType>();
        }

        /*
        /// <remarks>Reference to a previously defined sourceFile.</remarks>
        /// <remarks>min 0, max unbounded</remarks>
        //public SourceFileRefType[] sourceFileRef

        /// <summary>This number of source files referenced in this list.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string count*/
    }

    /// <summary>
    /// mzML SourceFileRefType
    /// </summary>
    public partial class SourceFileRefType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SourceFileRefType()
        {
            @ref = null;
        }

        /*
        /// <remarks>This attribute must reference the 'id' of the appropriate sourceFile.</remarks>
        /// <remarks>Required Attribute</remarks>
        /// <returns>IDREF</returns>
        //public string @ref*/
    }

    /// <summary>
    /// mzML TargetListType
    /// </summary>
    /// <remarks>Target list (or 'inclusion list') configured prior to the run.</remarks>
    public partial class TargetListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TargetListType()
        {
            targetField = new List<ParamGroupType>();
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public ParamGroupType[] target

        /// <summary>The number of TargetType elements in this list.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string count*/
    }

    /// <summary>
    /// mzML SoftwareListType
    /// </summary>
    /// <remarks>List and descriptions of software used to acquire and/or process the data in this mzML file.</remarks>
    public partial class SoftwareListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SoftwareListType()
        {
            softwareField = new List<SoftwareType>();
        }

        /*
        /// <remarks>A piece of software.</remarks>
        /// <remarks>min 1, max unbounded</remarks>
        //public SoftwareType[] software

        /// <summary>The number of software items defined in this mzML file.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string count*/
    }

    /// <summary>
    /// mzML SoftwareType
    /// </summary>
    /// <remarks>Software information</remarks>
    public partial class SoftwareType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SoftwareType()
        {
            id = null;
            version = null;
        }

        /*
        /// <remarks>An identifier for this software that is unique across all SoftwareTypes.</remarks>
        /// <remarks>Required Attribute</remarks>
        /// ID
        //public string id

        /// <summary>The software version.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string version*/
    }

    /// <summary>
    /// mzML InstrumentConfigurationListType
    /// </summary>
    /// <remarks>List and descriptions of instrument configurations.
    /// At least one instrument configuration must be specified, even if it is
    /// only to specify that the instrument is unknown. In that case,
    /// the "instrument model" term is used to indicate the unknown
    /// instrument in the instrumentConfiguration.</remarks>
    public partial class InstrumentConfigurationListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public InstrumentConfigurationListType()
        {
            instrumentConfigurationField = new List<InstrumentConfigurationType>();
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public InstrumentConfigurationType[] instrumentConfiguration

        /// <summary>The number of instrument configurations present in this list.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string count*/
    }

    /// <summary>
    /// mzML InstrumentConfigurationType
    /// </summary>
    /// <remarks>Description of a particular hardware configuration of a mass spectrometer.
    /// Each configuration must have one (and only one) of the three different components used for an analysis.
    /// For hybrid instruments, such as an LTQ-FT, there must be one configuration for each permutation of
    /// the components that is used in the document. For software configuration, use a ReferenceableParamGroup element</remarks>
    public partial class InstrumentConfigurationType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public InstrumentConfigurationType()
        {
            componentList = null;
            softwareRef = null;
            id = null;
            scanSettingsRef = null;
        }

        /*
        /// <remarks>min 0, max 1</remarks>
        //public ComponentListType componentList

        /// <remarks>min 0, max 1</remarks>
        //public SoftwareRefType softwareRef

        /// <summary>An identifier for this instrument configuration.</summary>
        /// <remarks>Required Attribute</remarks>
        /// ID
        //public string id

        /// <summary>Optional Attribute</summary>
        /// <returns>IDREF</returns>
        //public string scanSettingsRef*/
    }

    /// <summary>
    /// mzML ComponentListType
    /// </summary>
    /// <remarks>List with the different components used in the mass spectrometer. At least one source, one mass analyzer and one detector need to be specified.</remarks>
    public partial class ComponentListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ComponentListType()
        {
            sourceField = new List<SourceComponentType>();
            analyzerField = new List<AnalyzerComponentType>();
            detectorField = new List<DetectorComponentType>();
        }

        /*
        /// <remarks>A source component.</remarks>
        /// <remarks>min 1, max unbounded</remarks>
        //public SourceComponentType[] source

        /// <summary>A mass analyzer (or mass filter) component.</summary>
        /// <remarks>min 1, max unbounded</remarks>
        //public AnalyzerComponentType[] analyzer

        /// <summary>A detector component.</summary>
        /// <remarks>min 1, max unbounded</remarks>
        //public DetectorComponentType[] detector

        /// <summary>The number of components in this list.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string count*/
    }

    /// <summary>
    /// mzML ComponentType
    /// </summary>
    public partial class ComponentType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ComponentType()
        {
            order = 0;
        }

        /*
        /// <remarks>This attribute must be used to indicate the order in which the components
        /// are encountered from source to detector (e.g., in a Q-TOF, the quadrupole would
        /// have the lower order number, and the TOF the higher number of the two).</remarks>
        /// <remarks>Required Attribute</remarks>
        //public int order*/
    }

    /// <summary>
    /// mzML SourceComponentType
    /// </summary>
    /// <remarks>This element must be used to describe a Source Component Type.
    /// This is a PRIDE3-specific modification of the core MzML schema that does not
    /// have any impact on the base schema validation.</remarks>
    public partial class SourceComponentType : ComponentType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SourceComponentType()
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
        /// <summary>
        /// Constructor
        /// </summary>
        public AnalyzerComponentType()
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
        /// <summary>
        /// Constructor
        /// </summary>
        public DetectorComponentType()
        { }
    }

    /// <summary>
    /// mzML SoftwareRefType
    /// </summary>
    /// <remarks>Reference to a previously defined software element</remarks>
    public partial class SoftwareRefType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SoftwareRefType()
        {
            @ref = null;
        }

        /*
        /// <remarks>This attribute must be used to reference the 'id' attribute of a software element.</remarks>
        /// <remarks>Required Attribute</remarks>
        /// <returns>IDREF</returns>
        //public string @ref*/
    }

    /// <summary>
    /// mzML SampleListType
    /// </summary>
    /// <remarks>List and descriptions of samples.</remarks>
    public partial class SampleListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SampleListType()
        {
            sampleField = new List<SampleType>();
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public SampleType[] sample

        /// <summary>The number of Samples defined in this mzML file.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string count*/
    }

    /// <summary>
    /// mzML SampleType
    /// </summary>
    /// <remarks>Expansible description of the sample used to generate the dataset, named in sampleName.</remarks>
    public partial class SampleType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SampleType()
        {
            id = null;
            name = null;
        }

        /*
        /// <remarks>A unique identifier across the samples with which to reference this sample description.</remarks>
        /// <remarks>Required Attribute</remarks>
        /// ID
        //public string id

        /// <summary>An optional name for the sample description, mostly intended as a quick mnemonic.</summary>
        /// <remarks>Optional Attribute</remarks>
        //public string name*/
    }

    /// <summary>
    /// mzML SourceFileListType
    /// </summary>
    /// <remarks>List and descriptions of the source files this mzML document was generated or derived from</remarks>
    public partial class SourceFileListType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SourceFileListType()
        {
            sourceFileField = new List<SourceFileType>();
        }

        /*
        /// <remarks>min 1, max unbounded</remarks>
        //public SourceFileType[] sourceFile

        /// <summary>Number of source files used in generating the instance document.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string count*/
    }

    /// <summary>
    /// mzML SourceFileType
    /// </summary>
    /// <remarks>Description of the source file, including location and type.</remarks>
    public partial class SourceFileType : ParamGroupType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SourceFileType()
        {
            id = null;
            name = null;
            location = null;
        }

        /*
        /// <remarks>An identifier for this file.</remarks>
        /// <remarks>Required Attribute</remarks>
        /// ID
        //public string id

        /// <summary>Name of the source file, without reference to location (either URI or local path).</summary>
        /// <remarks>Required Attribute</remarks>
        //public string name

        /// <summary>URI-formatted location where the file was retrieved.</summary>
        /// <remarks>Required Attribute</remarks>
        //public string location*/
    }

    /// <summary>
    /// mzML FileDescriptionType
    /// </summary>
    /// <remarks>Information pertaining to the entire mzML file (i.e. not specific to any part of the data set) is stored here.</remarks>
    public partial class FileDescriptionType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FileDescriptionType()
        {
            fileContent = null;
            sourceFileList = null;
            contactField = new List<ParamGroupType>();
        }

        /*
        /// <remarks>This summarizes the different types of spectra that can be expected in the file.
        /// This is expected to aid processing software in skipping files that do not contain appropriate
        /// spectrum types for it. It should also describe the nativeID format used in the file by referring to an appropriate CV term.</remarks>
        /// <remarks>min 1, max 1</remarks>
        //public ParamGroupType fileContent

        /// <remarks>min 0, max 1</remarks>
        //public SourceFileListType sourceFileList

        /// <remarks>min 0, max unbounded</remarks>
        //public ParamGroupType[] contact*/
    }
}
