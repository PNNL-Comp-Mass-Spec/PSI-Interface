using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI_Interface.MSData
{
	/// <summary>
	/// mzML mzMLType
	/// </summary>
	/// <remarks>This is the root element for the Proteomics Standards Initiative (PSI) mzML schema, which 
	/// is intended to capture the use of a mass spectrometer, the data generated, and 
	/// the initial processing of that data (to the level of the peak list)</remarks>
	public class MSData
	{
		private CVListType _cvList;
		private FileDescriptionType fileDescriptionField;
		private ReferenceableParamGroupListType referenceableParamGroupListField;
		private SampleListType sampleListField;
		private SoftwareListType softwareListField;
		private ScanSettingsListType scanSettingsListField;
		private InstrumentConfigurationListType instrumentConfigurationListField;
		private DataProcessingListType dataProcessingListField;
		private RunType runField;
		private string accessionField;
		private string versionField;
		private string idField;

		/// min 1, max 1
		public CVListType cvList
		{
			get { return _cvList; }
			set { _cvList = value; }
		}

		/// min 1, max 1
		public FileDescriptionType fileDescription
		{
			get { return fileDescriptionField; }
			set { fileDescriptionField = value; }
		}

		/// min 0, max 1
		public ReferenceableParamGroupListType referenceableParamGroupList
		{
			get { return referenceableParamGroupListField; }
			set { referenceableParamGroupListField = value; }
		}

		/// min 0, max 1
		public SampleListType sampleList
		{
			get { return sampleListField; }
			set { sampleListField = value; }
		}

		/// min 1, max 1
		public SoftwareListType softwareList
		{
			get { return softwareListField; }
			set { softwareListField = value; }
		}

		/// min 0, max 1
		public ScanSettingsListType scanSettingsList
		{
			get { return scanSettingsListField; }
			set { scanSettingsListField = value; }
		}

		/// min 1, max 1
		public InstrumentConfigurationListType instrumentConfigurationList
		{
			get { return instrumentConfigurationListField; }
			set { instrumentConfigurationListField = value; }
		}

		/// min 1, max 1
		public DataProcessingListType dataProcessingList
		{
			get { return dataProcessingListField; }
			set { dataProcessingListField = value; }
		}

		/// min 1, max 1
		public RunType run
		{
			get { return runField; }
			set { runField = value; }
		}

		/// <remarks>An optional accession number for the mzML document used for storage, e.g. in PRIDE.</remarks>
		/// Optional Attribute
		/// string
		public string accession
		{
			get { return accessionField; }
			set { accessionField = value; }
		}

		/// <remarks>An optional id for the mzML document used for referencing from external files. It is recommended to use LSIDs when possible.</remarks>
		/// Optional Attribute
		/// string
		public string id
		{
			get { return idField; }
			set { idField = value; }
		}

		/// <remarks>The version of this mzML document.</remarks>
		/// Required Attribute
		/// string
		public string version
		{
			get { return versionField; }
			set { versionField = value; }
		}
	}

	/// <summary>
	/// mzML CVListType
	/// </summary>
	/// <remarks>Container for one or more controlled vocabulary definitions.</remarks>
	public class CVListType
	{
		private List<CVType> cvs;

		/// min 1, max unbounded
		public CVType[] cv
		{
			get { return cvs.ToArray(); }
			set { cvs = value.ToList(); }
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

	/// <summary>
	/// mzML CVType
	/// </summary>
	/// <remarks>Information about an ontology or CV source and a short 'lookup' tag to refer to.</remarks>
	public class CVType
	{
		private string idField;
		private string fullNameField;
		private string versionField;
		private string uRIField;

		/// <remarks>The short label to be used as a reference tag with which to refer to this particular Controlled Vocabulary source description (e.g., from the cvLabel attribute, in CVParamType elements).</remarks>
		/// Required Attribute
		/// ID
		public string id
		{
			get { return idField; }
			set { idField = value; }
		}

		/// <remarks>The usual name for the resource (e.g. The PSI-MS Controlled Vocabulary).</remarks>
		/// Required Attribute
		/// string
		public string fullName
		{
			get { return fullNameField; }
			set { fullNameField = value; }
		}

		/// <remarks>The version of the CV from which the referred-to terms are drawn.</remarks>
		/// Optional Attribute
		/// string
		public string version
		{
			get { return versionField; }
			set { versionField = value; }
		}

		/// <remarks>The URI for the resource.</remarks>
		/// Required Attribute
		/// anyURI
		public string URI
		{
			get { return uRIField; }
			set { uRIField = value; }
		}
	}

	/// <summary>
	/// mzML DataProcessingListType
	/// </summary>
	/// <remarks>List and descriptions of data processing applied to this data.</remarks>
	public class DataProcessingListType
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

	/// <summary>
	/// mzML DataProcessingType
	/// </summary>
	/// <remarks>Description of the way in which a particular software was used.</remarks>
	public class DataProcessingType
	{
		private List<ProcessingMethodType> processingMethodField;
		private string idField;

		/// <remarks>Description of the default peak processing method. 
		/// This element describes the base method used in the generation of a particular mzML file. 
		/// Variable methods should be described in the appropriate acquisition section - if 
		/// no acquisition-specific details are found, then this information serves as the default.</remarks>
		/// min 1, max unbounded
		public ProcessingMethodType[] processingMethod
		{
			get { return processingMethodField.ToArray(); }
			set { processingMethodField = value.ToList(); }
		}

		/// <remarks>A unique identifier for this data processing that is unique across all DataProcessingTypes.</remarks>
		/// Required Attribute
		/// ID
		public string id
		{
			get { return idField; }
			set { idField = value; }
		}
	}

	/// <summary>
	/// mzML ProcessingMethodType
	/// </summary>
	public class ProcessingMethodType : ParamGroupType
	{
		private string orderField;
		private string softwareRefField;

		/// <remarks>This attributes allows a series of consecutive steps to be placed in the correct order.</remarks>
		/// Required Attribute
		/// non-negative integer
		public string order
		{
			get { return orderField; }
			set { orderField = value; }
		}

		/// <remarks>This attribute must reference the 'id' of the appropriate SoftwareType.</remarks>
		/// Required Attribute
		/// IDREF
		public string softwareRef
		{
			get { return softwareRefField; }
			set { softwareRefField = value; }
		}
	}

	/// <summary>
	/// mzML ParamGroupType
	/// </summary>
	/// <remarks>Structure allowing the use of a controlled (cvParam) or uncontrolled vocabulary (userParam), or a reference to a predefined set of these in this mzML file (paramGroupRef).</remarks>
	public class ParamGroupType
	{
		private List<ReferenceableParamGroupRefType> referenceableParamGroupRefField;
		//private List<ParamGroupType> paramGroupRefs;
		private List<CVParamType> cvParamField;
		private List<UserParamType> userParamField;

		/// min 0, max unbounded
		public ReferenceableParamGroupRefType[] referenceableParamGroupRef
		{
			get { return referenceableParamGroupRefField.ToArray(); }
			set { referenceableParamGroupRefField = value.ToList(); }
		}
		//public ParamGroupType[] referenceableParamGroupRef
		//{
		//	get { return paramGroupRefs.ToArray(); }
		//	set { paramGroupRefs = value.ToList(); }
		//}

		/// min 0, max unbounded
		public CVParamType[] cvParam
		{
			get { return cvParamField.ToArray(); }
			set { cvParamField = value.ToList(); }
		}

		/// min 0, max unbounded
		public UserParamType[] userParam
		{
			get { return userParamField.ToArray(); }
			set { userParamField = value.ToList(); }
		}
	}

	/// <summary>
	/// mzML ReferenceableParamGroupListType
	/// </summary>
	/// <remarks>Container for a list of referenceableParamGroups</remarks>
	public class ReferenceableParamGroupListType
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

	/// <summary>
	/// mzML ReferenceableParamGroupType
	/// </summary>
	/// <remarks>A collection of CVParam and UserParam elements that can be referenced from elsewhere in this mzML document by using the 'paramGroupRef' element in that location to reference the 'id' attribute value of this element.</remarks>
	public class ReferenceableParamGroupType
	{
		private List<CVParamType> cvParamField;
		private List<UserParamType> userParamField;
		private string idField;

		/// min 0, max unbounded
		public CVParamType[] cvParam
		{
			get { return cvParamField.ToArray(); }
			set { cvParamField = value.ToList(); }
		}

		/// min 0, max unbounded
		public UserParamType[] userParam
		{
			get { return userParamField.ToArray(); }
			set { userParamField = value.ToList(); }
		}

		/// <remarks>The identifier with which to reference this ReferenceableParamGroup.</remarks>
		/// Required Attribute
		/// ID
		public string id
		{
			get { return idField; }
			set { idField = value; }
		}
	}

	/// <summary>
	/// mzML ReferenceableParamGroupRefType
	/// </summary>
	/// <remarks>A reference to a previously defined ParamGroup, which is a reusable container of one or more cvParams.</remarks>
	public class ReferenceableParamGroupRefType
	{
		private string refField;

		/// <remarks>Reference to the id attribute in a referenceableParamGroup.</remarks>
		/// Required Attribute
		/// IDREF
		public string @ref
		{
			get { return refField; }
			set { refField = value; }
		}
	}

	/// <summary>
	/// mzML CVParamType
	/// </summary>
	/// <remarks>This element holds additional data or annotation. Only controlled values are allowed here.</remarks>
	public class CVParamType
	{
		private string cvRefField;
		private CVType CVRef;
		private string accessionField;
		private string valueField;
		private string nameField;
		private string unitAccessionField;
		private string unitNameField;
		private string unitCvRefField;
		private CVType UnitCVRef;

		/// <remarks>A reference to the CV 'id' attribute as defined in the cvList in this mzML file.</remarks>
		/// Required Attribute
		/// IDREF
		public string cvRef
		{
			get
			{
				return cvRefField;
				return CVRef.id;
			}
			set
			{
				cvRefField = value;
				// have to set up a dictionary or something similar...
				//CVRef = cvs[value];
			}
		}

		/// <remarks>The accession number of the referred-to term in the named resource (e.g.: MS:000012).</remarks>
		/// Required Attribute
		/// string
		public string accession
		{
			get { return accessionField; }
			set { accessionField = value; }
		}

		/// <remarks>The actual name for the parameter, from the referred-to controlled vocabulary. This should be the preferred name associated with the specified accession number.</remarks>
		/// Required Attribute
		/// string
		public string name
		{
			get { return nameField; }
			set { nameField = value; }
		}

		/// <remarks>The value for the parameter; may be absent if not appropriate, or a numeric or symbolic value, or may itself be CV (legal values for a parameter should be enumerated and defined in the ontology).</remarks>
		/// Optional Attribute
		/// string
		public string value
		{
			get { return valueField; }
			set { valueField = value; }
		}

		/// <remarks>If a unit term is referenced, this attribute must refer to the CV 'id' attribute defined in the cvList in this mzML file.</remarks>
		/// Optional Attribute
		/// IDREF
		public string unitCvRef
		{
			get
			{
				return unitCvRefField;
				return UnitCVRef.id;
			}
			set
			{
				unitCvRefField = value;
				// have to set up a dictionary or something similar...
				//UnitCVRef = cvs[value];
			}
		}

		/// <remarks>An optional CV accession number for the unit term associated with the value, if any (e.g., 'UO:0000266' for 'electron volt').</remarks>
		/// Optional Attribute
		/// string
		public string unitAccession
		{
			get { return unitAccessionField; }
			set { unitAccessionField = value; }
		}

		/// <remarks>An optional CV name for the unit accession number, if any (e.g., 'electron volt' for 'UO:0000266' ).</remarks>
		/// Optional Attribute
		/// string
		public string unitName
		{
			get { return unitNameField; }
			set { unitNameField = value; }
		}
	}

	/// <summary>
	/// mzML UserParamType
	/// </summary>
	/// <remarks>Uncontrolled user parameters (essentially allowing free text). 
	/// Before using these, one should verify whether there is an appropriate 
	/// CV term available, and if so, use the CV term instead</remarks>
	public class UserParamType
	{
		private string nameField;
		private string typeField;
		private string valueField;
		private string unitAccessionField;
		private string unitNameField;
		private string unitCvRefField;
		private CVType UnitCVRef;

		/// <remarks>The name for the parameter.</remarks>
		/// Required Attribute
		/// string
		public string name
		{
			get { return nameField; }
			set { nameField = value; }
		}

		/// <remarks>The datatype of the parameter, where appropriate (e.g.: xsd:float).</remarks>
		/// Optional Attribute
		/// string
		public string type
		{
			get { return typeField; }
			set { typeField = value; }
		}

		/// <remarks>The value for the parameter, where appropriate.</remarks>
		/// Optional Attribute
		/// string
		public string value
		{
			get { return valueField; }
			set { valueField = value; }
		}

		/// <remarks>An optional CV accession number for the unit term associated with the value, if any (e.g., 'UO:0000266' for 'electron volt').</remarks>
		/// Optional Attribute
		/// string
		public string unitAccession
		{
			get { return unitAccessionField; }
			set { unitAccessionField = value; }
		}

		/// <remarks>An optional CV name for the unit accession number, if any (e.g., 'electron volt' for 'UO:0000266' ).</remarks>
		/// Optional Attribute
		/// string
		public string unitName
		{
			get { return unitNameField; }
			set { unitNameField = value; }
		}

		/// <remarks>If a unit term is referenced, this attribute must refer to the CV 'id' attribute defined in the cvList in this mzML file.</remarks>
		/// Optional Attribute
		/// IDREF
		public string unitCvRef
		{
			get
			{
				return unitCvRefField;
				return UnitCVRef.id;
			}
			set
			{
				unitCvRefField = value;
				// have to set up a dictionary or something similar...
				//UnitCVRef = cvs[value];
			}
		}
	}

	/// <summary>
	/// mzML ChromatogramType
	/// </summary>
	/// <remarks>A single Chromatogram</remarks>
	public class ChromatogramType : ParamGroupType
	{
		private PrecursorType precursorField;
		private ProductType productField;
		private BinaryDataArrayListType binaryDataArrayListField;
		private string idField;
		private string indexField;
		private int defaultArrayLengthField;
		private string dataProcessingRefField;

		/// min 0, max 1
		public PrecursorType precursor
		{
			get { return this.precursorField; }
			set { precursorField = value; }
		}

		/// min 0, max 1
		public ProductType product
		{
			get { return productField; }
			set { productField = value; }
		}

		/// min 1, max 1
		public BinaryDataArrayListType binaryDataArrayList
		{
			get { return binaryDataArrayListField; }
			set { binaryDataArrayListField = value; }
		}

		/// <remarks>The zero-based index for this chromatogram in the chromatogram list</remarks>
		/// Required Attribute
		/// non-negative integer
		public string index
		{
			get { return indexField; }
			set { indexField = value; }
		}

		/// <remarks>A unique identifier for this chromatogram</remarks>
		/// Required Attribute
		/// string
		public string id
		{
			get { return idField; }
			set { idField = value; }
		}

		/// <remarks>Default length of binary data arrays contained in this element.</remarks>
		/// Required Attribute
		/// integer
		public int defaultArrayLength
		{
			get { return defaultArrayLengthField; }
			set { defaultArrayLengthField = value; }
		}

		/// <remarks>This attribute can optionally reference the 'id' of the appropriate dataProcessing.</remarks>
		/// Optional Attribute
		/// IDREF
		public string dataProcessingRef
		{
			get { return dataProcessingRefField; }
			set { dataProcessingRefField = value; }
		}
	}

	/// <summary>
	/// mzML PrecursorListType
	/// </summary>
	/// <remarks>List and descriptions of precursor isolations to the spectrum currently being described, ordered.</remarks>
	public class PrecursorListType
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

	/// <summary>
	/// mzML PrecursorType
	/// </summary>
	/// <remarks>The method of precursor ion selection and activation</remarks>
	public class PrecursorType
	{
		private ParamGroupType isolationWindowField;
		private SelectedIonListType selectedIonListField;
		private ParamGroupType activationField;
		private string spectrumRefField;
		private string sourceFileRefField;
		private string externalSpectrumIDField;

		/// <remarks>This element captures the isolation (or 'selection') window configured to isolate one or more ions.</remarks>
		/// min 0, max 1
		public ParamGroupType isolationWindow
		{
			get { return isolationWindowField; }
			set { isolationWindowField = value; }
		}

		/// <remarks>A list of ions that were selected.</remarks>
		/// min 0, max 1
		public SelectedIonListType selectedIonList
		{
			get { return selectedIonListField; }
			set { selectedIonListField = value; }
		}

		/// <remarks>The type and energy level used for activation.</remarks>
		public ParamGroupType activation
		{
			get { return activationField; }
			set { activationField = value; }
		}

		/// <remarks>For precursor spectra that are local to this document, this attribute must be used to reference the 'id' attribute of the spectrum corresponding to the precursor spectrum.</remarks>
		/// Optional Attribute
		/// string
		public string spectrumRef
		{
			get { return spectrumRefField; }
			set { spectrumRefField = value; }
		}

		/// <remarks>For precursor spectra that are external to this document, this attribute must reference the 'id' attribute of a sourceFile representing that external document.</remarks>
		/// Optional Attribute
		/// IDREF
		public string sourceFileRef
		{
			get { return sourceFileRefField; }
			set { sourceFileRefField = value; }
		}

		/// <remarks>For precursor spectra that are external to this document, this string must correspond to the 'id' attribute of a spectrum in the external document indicated by 'sourceFileRef'.</remarks>
		/// Optional Attribute
		/// string
		public string externalSpectrumID
		{
			get { return externalSpectrumIDField; }
			set { externalSpectrumIDField = value; }
		}
	}

	/// <summary>
	/// mzML SelectedIonListType
	/// </summary>
	/// <remarks>The list of selected precursor ions.</remarks>
	public class SelectedIonListType
	{
		private List<ParamGroupType> selectedIonField;
		private string countField;

		/// min1, max unbounded
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

	/// <summary>
	/// mzML BinaryDataArrayListType
	/// </summary>
	/// <remarks>List of binary data arrays.</remarks>
	public class BinaryDataArrayListType
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

	/// <summary>
	/// mzML BinaryDataArrayType
	/// </summary>
	/// <remarks>The structure into which encoded binary data goes. Byte ordering is always little endian (Intel style). 
	/// Computers using a different endian style must convert to/from little endian when writing/reading mzML</remarks>
	public class BinaryDataArrayType : ParamGroupType
	{
		private byte[] binaryField;
		private string arrayLengthField;
		private string dataProcessingRefField;
		private string encodedLengthField;

		/// <remarks>The actual base64 encoded binary data. The byte order is always 'little endian'.</remarks>
		/// base64Binary
		public byte[] binary
		{
			get { return binaryField; }
			set { binaryField = value; }
		}

		/// <remarks>This optional attribute may override the 'defaultArrayLength' defined in SpectrumType. 
		/// The two default arrays (m/z and intensity) should NEVER use this override option, and should 
		/// therefore adhere to the 'defaultArrayLength' defined in SpectrumType. Parsing software can thus 
		/// safely choose to ignore arrays of lengths different from the one defined in the 'defaultArrayLength' SpectrumType element.</remarks>
		/// Optional Attribute
		/// non-negative integer
		public string arrayLength
		{
			get { return arrayLengthField; }
			set { arrayLengthField = value; }
		}

		/// <remarks>This optional attribute may reference the 'id' attribute of the appropriate dataProcessing.</remarks>
		/// Optional Attribute
		/// IDREF
		public string dataProcessingRef
		{
			get { return dataProcessingRefField; }
			set { dataProcessingRefField = value; }
		}

		/// <remarks>The encoded length of the binary data array.</remarks>
		/// Required Attribute
		/// non-negative integer
		public string encodedLength
		{
			get { return encodedLengthField; }
			set { encodedLengthField = value; }
		}
	}

	/// <summary>
	/// mzML ScanListType
	/// </summary>
	/// <remarks>List and descriptions of scans.</remarks>
	public class ScanListType : ParamGroupType
	{
		private List<ScanType> scanField;
		private string countField;

		/// min 1, max unbounded
		public ScanType[] scan
		{
			get { return scanField.ToArray(); }
			set { scanField = value.ToList(); }
		}

		/// <remarks>the number of scans defined in this list.</remarks>
		/// Required Attribute
		/// non-negative integer
		public string count
		{
			get { return countField; }
			set { countField = value; }
		}
	}

	/// <summary>
	/// mzML ScanType
	/// </summary>
	/// <remarks>Scan or acquisition from original raw file used to create this peak list, as specified in sourceF</remarks>
	public class ScanType : ParamGroupType
	{
		private ScanWindowListType scanWindowListField;
		private string spectrumRefField;
		private string sourceFileRefField;
		private string externalSpectrumIDField;
		private string instrumentConfigurationRefField;

		/// <remarks>Container for a list of scan windows.</remarks>
		/// min 0, max 1
		public ScanWindowListType scanWindowList
		{
			get { return scanWindowListField; }
			set { scanWindowListField = value; }
		}

		/// <remarks>For scans that are local to this document, this attribute can be used to reference the 'id' attribute of the spectrum corresponding to the scan.</remarks>
		/// Optional Attribute
		/// string
		public string spectrumRef
		{
			get { return spectrumRefField; }
			set { spectrumRefField = value; }
		}

		/// <remarks>If this attribute is set, it must reference the 'id' attribute of a sourceFile representing the external document containing the spectrum referred to by 'externalSpectrumID'.</remarks>
		/// Optional Attribute
		/// IDREF
		public string sourceFileRef
		{
			get { return sourceFileRefField; }
			set { sourceFileRefField = value; }
		}

		/// <remarks>For scans that are external to this document, this string must correspond to the 'id' attribute of a spectrum in the external document indicated by 'sourceFileRef'.</remarks>
		/// Optional Attribute
		/// string
		public string externalSpectrumID
		{
			get { return externalSpectrumIDField; }
			set { externalSpectrumIDField = value; }
		}

		/// <remarks>This attribute can optionally reference the 'id' attribute of the appropriate instrument configuration.</remarks>
		/// Optional Attribute
		/// IDREF
		public string instrumentConfigurationRef
		{
			get { return instrumentConfigurationRefField; }
			set { instrumentConfigurationRefField = value; }
		}
	}

	/// <summary>
	/// mzML ScanWindowListType
	/// </summary>
	/// <remarks></remarks>
	public class ScanWindowListType
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

	/// <summary>
	/// mzML SpectrumListType
	/// </summary>
	/// <remarks>List and descriptions of spectra.</remarks>
	public class SpectrumListType
	{
		private List<SpectrumType> spectrumField;
		private string countField;
		private string defaultDataProcessingRefField;

		/// min 0, max unbounded
		public SpectrumType[] spectrum
		{
			get { return spectrumField.ToArray(); }
			set { spectrumField = value.ToList(); }
		}

		/// <remarks>The number of spectra defined in this mzML file.</remarks>
		/// Required Attribute
		/// non-negative integer
		public string count
		{
			get { return countField; }
			set { countField = value; }
		}

		/// <remarks>This attribute MUST reference the 'id' of the default data processing for the spectrum list. 
		/// If an acquisition does not reference any data processing, it implicitly refers to this data processing. 
		/// This attribute is required because the minimum amount of data processing that any format will undergo is "conversion to mzML".</remarks>
		/// Required Attribute
		/// IDREF
		public string defaultDataProcessingRef
		{
			get { return defaultDataProcessingRefField; }
			set { defaultDataProcessingRefField = value; }
		}
	}

	/// <summary>
	/// mzML SpectrumType
	/// </summary>
	/// <remarks>The structure that captures the generation of a peak list (including the underlying acquisitions). 
	/// Also describes some of the parameters for the mass spectrometer for a given acquisition (or list of acquisitions).</remarks>
	public class SpectrumType : ParamGroupType
	{
		private ScanListType scanListField;
		private PrecursorListType precursorListField;
		private ProductListType productListField;
		private BinaryDataArrayListType binaryDataArrayListField;
		private string idField;
		private string spotIDField;
		private string indexField;
		private int defaultArrayLengthField;
		private string dataProcessingRefField;
		private string sourceFileRefField;

		/// min 0, max 1
		public ScanListType scanList
		{
			get { return scanListField; }
			set { scanListField = value; }
		}

		/// min 0, max 1
		public PrecursorListType precursorList
		{
			get { return precursorListField; }
			set { precursorListField = value; }
		}

		/// min 0, max 1
		public ProductListType productList
		{
			get { return productListField; }
			set { productListField = value; }
		}

		/// min 0, max 1
		public BinaryDataArrayListType binaryDataArrayList
		{
			get { return binaryDataArrayListField; }
			set { binaryDataArrayListField = value; }
		}

		/// <remarks>The zero-based, consecutive index of  the spectrum in the SpectrumList.</remarks>
		/// Required Attribute
		/// non-negative integer
		public string index
		{
			get { return indexField; }
			set { indexField = value; }
		}

		/// <remarks>The native identifier for a spectrum. For unmerged native spectra or spectra from older open file formats, 
		/// the format of the identifier is defined in the PSI-MS CV and referred to in the mzML header. 
		/// External documents may use this identifier together with the mzML filename or accession to reference a particular spectrum.</remarks>
		/// Required Attribute
		/// Regex: "\S+=\S+( \S+=\S+)*"
		public string id
		{
			get { return idField; }
			set { idField = value; }
		}

		/// <remarks>The identifier for the spot from which this spectrum was derived, if a MALDI or similar run.</remarks>
		/// Optional Attribute
		/// string
		public string spotID
		{
			get { return spotIDField; }
			set { spotIDField = value; }
		}

		/// <remarks>Default length of binary data arrays contained in this element.</remarks>
		/// Required Attribute
		/// integer
		public int defaultArrayLength
		{
			get { return defaultArrayLengthField; }
			set { defaultArrayLengthField = value; }
		}

		/// <remarks>This attribute can optionally reference the 'id' of the appropriate dataProcessing.</remarks>
		/// Optional Attribute
		/// IDREF
		public string dataProcessingRef
		{
			get { return dataProcessingRefField; }
			set { dataProcessingRefField = value; }
		}

		/// <remarks>This attribute can optionally reference the 'id' of the appropriate sourceFile.</remarks>
		/// Optional Attribute
		/// IDREF
		public string sourceFileRef
		{
			get { return sourceFileRefField; }
			set { sourceFileRefField = value; }
		}
	}

	/// <summary>
	/// mzML ProductListType
	/// </summary>
	/// <remarks>List and descriptions of product isolations to the spectrum currently being described, ordered.</remarks>
	public class ProductListType
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

	/// <summary>
	/// mzML ProductType
	/// </summary>
	/// <remarks>The method of product ion selection and activation in a precursor ion scan</remarks>
	public class ProductType
	{
		private ParamGroupType isolationWindowField;

		/// <remarks>This element captures the isolation (or 'selection') window configured to isolate one or more ions.</remarks>
		/// min 0, max 1
		public ParamGroupType isolationWindow
		{
			get { return isolationWindowField; }
			set { isolationWindowField = value; }
		}
	}

	/// <summary>
	/// mzML RunType
	/// </summary>
	/// <remarks>A run in mzML should correspond to a single, consecutive and coherent set of scans on an instrument.</remarks>
	public class RunType : ParamGroupType
	{
		private SpectrumListType spectrumListField;
		private ChromatogramListType chromatogramListField;
		private string idField;
		private string defaultInstrumentConfigurationRefField;
		private string defaultSourceFileRefField;
		private string sampleRefField;
		private System.DateTime startTimeStampField;
		private bool startTimeStampFieldSpecified;

		/// <remarks>All mass spectra and the acquisitions underlying them are described and attached here. 
		/// Subsidiary data arrays are also both described and attached here.</remarks>
		/// min 0, max 1
		public SpectrumListType spectrumList
		{
			get { return spectrumListField; }
			set { spectrumListField = value; }
		}

		/// <remarks>All chromatograms for this run.</remarks>
		/// min 0, max 1
		public ChromatogramListType chromatogramList
		{
			get { return chromatogramListField; }
			set { chromatogramListField = value; }
		}

		/// <remarks>A unique identifier for this run.</remarks>
		/// Required Attribute
		/// ID
		public string id
		{
			get { return idField; }
			set { idField = value; }
		}

		/// <remarks>This attribute must reference the 'id' of the default instrument configuration. 
		/// If a scan does not reference an instrument configuration, it implicitly refers to this configuration.</remarks>
		/// Required Attribute
		/// IDREF
		public string defaultInstrumentConfigurationRef
		{
			get { return defaultInstrumentConfigurationRefField; }
			set { defaultInstrumentConfigurationRefField = value; }
		}

		/// <remarks>This attribute can optionally reference the 'id' of the default source file. 
		/// If a spectrum or scan does not reference a source file and this attribute is set, then it implicitly refers to this source file.</remarks>
		/// Optional Attribute
		/// IDREF
		public string defaultSourceFileRef
		{
			get { return defaultSourceFileRefField; }
			set { defaultSourceFileRefField = value; }
		}

		/// <remarks>This attribute must reference the 'id' of the appropriate sample.</remarks>
		/// Optional Attribute
		/// IDREF
		public string sampleRef
		{
			get { return sampleRefField; }
			set { sampleRefField = value; }
		}

		/// <remarks>The optional start timestamp of the run, in UT.</remarks>
		/// Optional Attribute
		/// DateTime
		public System.DateTime startTimeStamp
		{
			get { return startTimeStampField; }
			set { startTimeStampField = value; }
		}

		/// "Ignored" Attribute - only used to signify existence of valid value in startTimeStamp
		public bool startTimeStampSpecified
		{
			get { return startTimeStampFieldSpecified; }
			set { startTimeStampFieldSpecified = value; }
		}
	}

	/// <summary>
	/// mzML ChromatogramListType
	/// </summary>
	/// <remarks>List of chromatograms.</remarks>
	public class ChromatogramListType
	{
		private List<ChromatogramType> chromatogramField;
		private string countField;
		private string defaultDataProcessingRefField;

		/// <remarks></remarks>
		/// min 1, max unbounded
		public ChromatogramType[] chromatogram
		{
			get { return chromatogramField.ToArray(); }
			set { chromatogramField = value.ToList(); }
		}

		/// <remarks>The number of chromatograms defined in this mzML file.</remarks>
		/// Required Attribute
		/// Non-negative integer
		public string count
		{
			get { return countField; }
			set { countField = value; }
		}

		/// <remarks>This attribute MUST reference the 'id' of the default data processing for the chromatogram list. 
		/// If an acquisition does not reference any data processing, it implicitly refers to this data processing. 
		/// This attribute is required because the minimum amount of data processing that any format will undergo is "conversion to mzML".</remarks>
		/// Required Attribute
		/// IDREF
		public string defaultDataProcessingRef
		{
			get { return defaultDataProcessingRefField; }
			set { defaultDataProcessingRefField = value; }
		}
	}

	/// <summary>
	/// mzML ScanSettingListType
	/// </summary>
	/// <remarks>List with the descriptions of the acquisition settings applied prior to the start of data acquisition.</remarks>
	public class ScanSettingsListType
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

	/// <summary>
	/// mzML ScanSettingsType
	/// </summary>
	/// <remarks>Description of the acquisition settings of the instrument prior to the start of the run.</remarks>
	public class ScanSettingsType : ParamGroupType
	{
		private SourceFileRefListType sourceFileRefListField;
		private TargetListType targetListField;
		private string idField;

		/// <remarks>List with the source files containing the acquisition settings.</remarks>
		/// min 0, max 1
		public SourceFileRefListType sourceFileRefList
		{
			get { return sourceFileRefListField; }
			set { sourceFileRefListField = value; }
		}

		/// <remarks>Target list (or 'inclusion list') configured prior to the run.</remarks>
		/// min 0, max 1
		public TargetListType targetList
		{
			get { return targetListField; }
			set { targetListField = value; }
		}

		/// <remarks>A unique identifier for this acquisition setting.</remarks>
		/// Required Attribute
		/// ID
		public string id
		{
			get { return idField; }
			set { idField = value; }
		}
	}

	/// <summary>
	/// mzML SourceFileRefListType
	/// </summary>
	public class SourceFileRefListType
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

	/// <summary>
	/// mzML SourceFileRefType
	/// </summary>
	/// <remarks></remarks>
	public class SourceFileRefType
	{
		private string refField;

		/// <remarks>This attribute must reference the 'id' of the appropriate sourceFile.</remarks>
		/// Required Attribute
		/// IDREF
		public string @ref
		{
			get { return refField; }
			set { refField = value; }
		}
	}

	/// <summary>
	/// mzML TargetListType
	/// </summary>
	/// <remarks>Target list (or 'inclusion list') configured prior to the run.</remarks>
	public class TargetListType
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

	/// <summary>
	/// mzML SoftwareListType
	/// </summary>
	/// <remarks>List and descriptions of software used to acquire and/or process the data in this mzML file.</remarks>
	public class SoftwareListType
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

	/// <summary>
	/// mzML SoftwareType
	/// </summary>
	/// <remarks>Software information</remarks>
	public class SoftwareType : ParamGroupType
	{
		private string idField;
		private string versionField;

		/// <remarks>An identifier for this software that is unique across all SoftwareTypes.</remarks>
		/// Required Attribute
		/// ID
		public string id
		{
			get { return idField; }
			set { idField = value; }
		}

		/// <remarks>The software version.</remarks>
		/// Required Attribute
		/// string
		public string version
		{
			get { return versionField; }
			set { versionField = value; }
		}
	}

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

	/// <summary>
	/// mzML InstrumentConfigurationType
	/// </summary>
	/// <remarks>Description of a particular hardware configuration of a mass spectrometer. 
	/// Each configuration must have one (and only one) of the three different components used for an analysis. 
	/// For hybrid instruments, such as an LTQ-FT, there must be one configuration for each permutation of 
	/// the components that is used in the document. For software configuration, use a ReferenceableParamGroup element</remarks>
	public class InstrumentConfigurationType : ParamGroupType
	{
		private ComponentListType componentListField;
		private SoftwareRefType softwareRefField;
		private string idField;
		private string scanSettingsRefField;

		/// min 0, max 1
		public ComponentListType componentList
		{
			get { return componentListField; }
			set { componentListField = value; }
		}

		/// min 0, max 1
		public SoftwareRefType softwareRef
		{
			get { return softwareRefField; }
			set { softwareRefField = value; }
		}

		/// <remarks>An identifier for this instrument configuration.</remarks>
		/// Required Attribute
		/// ID
		public string id
		{
			get { return idField; }
			set { idField = value; }
		}

		/// Optional Attribute
		/// IDREF
		public string scanSettingsRef
		{
			get { return scanSettingsRefField; }
			set { scanSettingsRefField = value; }
		}
	}

	/// <summary>
	/// mzML ComponentListType
	/// </summary>
	/// <remarks>List with the different components used in the mass spectrometer. At least one source, one mass analyzer and one detector need to be specified.</remarks>
	public class ComponentListType
	{
		private List<SourceComponentType> sourceField;
		private List<AnalyzerComponentType> analyzerField;
		private List<DetectorComponentType> detectorField;
		private string countField;

		/// <remarks>A source component.</remarks>
		/// min 1, max unbounded
		public SourceComponentType[] source
		{
			get { return sourceField.ToArray(); }
			set { sourceField = value.ToList(); }
		}

		/// <remarks>A mass analyzer (or mass filter) component.</remarks>
		/// min 1, max unbounded
		public AnalyzerComponentType[] analyzer
		{
			get { return analyzerField.ToArray(); }
			set { analyzerField = value.ToList(); }
		}

		/// <remarks>A detector component.</remarks>
		/// min 1, max unbounded
		public DetectorComponentType[] detector
		{
			get { return detectorField.ToArray(); }
			set { detectorField = value.ToList(); }
		}

		/// <remarks>The number of components in this list.</remarks>
		/// Required Attribute
		/// non-negative integer
		public string count
		{
			get { return countField; }
			set { countField = value; }
		}
	}

	/// <summary>
	/// mzML ComponentType
	/// </summary>
	public class ComponentType : ParamGroupType
	{
		private int orderField;

		/// <remarks>This attribute must be used to indicate the order in which the components 
		/// are encountered from source to detector (e.g., in a Q-TOF, the quadrupole would 
		/// have the lower order number, and the TOF the higher number of the two).</remarks>
		/// Required Attribute
		/// integer
		public int order
		{
			get { return orderField; }
			set { orderField = value; }
		}
	}

	/// <summary>
	/// mzML SourceComponentType
	/// </summary>
	/// <remarks>This element must be used to describe a Source Component Type. 
	/// This is a PRIDE3-specific modification of the core MzML schema that does not 
	/// have any impact on the base schema validation.</remarks>
	public class SourceComponentType : ComponentType
	{
	}

	/// <summary>
	/// mzML AnalyzerComponentType
	/// </summary>
	/// <remarks>This element must be used to describe an Analyzer Component Type. 
	/// This is a PRIDE3-specific modification of the core MzML schema that does not 
	/// have any impact on the base schema validation.</remarks>
	public class AnalyzerComponentType : ComponentType
	{
	}

	/// <summary>
	/// mzML DetectorComponentType
	/// </summary>
	/// <remarks>This element must be used to describe a Detector Component Type. 
	/// This is a PRIDE3-specific modification of the core MzML schema that does not 
	/// have any impact on the base schema validation.</remarks>
	public class DetectorComponentType : ComponentType
	{
	}

	/// <summary>
	/// mzML SoftwareRefType
	/// </summary>
	/// <remarks>Reference to a previously defined software element</remarks>
	public class SoftwareRefType
	{
		private string refField;

		/// <remarks>This attribute must be used to reference the 'id' attribute of a software element.</remarks>
		/// Required Attribute
		/// IDREF
		public string @ref
		{
			get { return refField; }
			set { refField = value; }
		}
	}

	/// <summary>
	/// mzML SampleListType
	/// </summary>
	/// <remarks>List and descriptions of samples.</remarks>
	public class SampleListType
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

	/// <summary>
	/// mzML SampleType
	/// </summary>
	/// <remarks>Expansible description of the sample used to generate the dataset, named in sampleName.</remarks>
	public class SampleType : ParamGroupType
	{
		private string idField;
		private string nameField;

		/// <remarks>A unique identifier across the samples with which to reference this sample description.</remarks>
		/// Required Attribute
		/// ID
		public string id
		{
			get { return idField; }
			set { idField = value; }
		}

		/// <remarks>An optional name for the sample description, mostly intended as a quick mnemonic.</remarks>
		/// Optional Attribute
		/// string
		public string name
		{
			get { return nameField; }
			set { nameField = value; }
		}
	}

	/// <summary>
	/// mzML SourceFileListType
	/// </summary>
	/// <remarks>List and descriptions of the source files this mzML document was generated or derived from</remarks>
	public class SourceFileListType
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

	/// <summary>
	/// mzML SourceFileType
	/// </summary>
	/// <remarks>Description of the source file, including location and type.</remarks>
	public class SourceFileType : ParamGroupType
	{
		private string idField;
		private string nameField;
		private string locationField;

		/// <remarks>An identifier for this file.</remarks>
		/// Required Attribute
		/// ID
		public string id
		{
			get { return idField; }
			set { idField = value; }
		}

		/// <remarks>Name of the source file, without reference to location (either URI or local path).</remarks>
		/// Required Attribute
		/// string
		public string name
		{
			get { return nameField; }
			set { nameField = value; }
		}

		/// <remarks>URI-formatted location where the file was retrieved.</remarks>
		/// Required Attribute
		/// anyURI
		public string location
		{
			get { return locationField; }
			set { locationField = value; }
		}
	}

	/// <summary>
	/// mzML FileDescriptionType
	/// </summary>
	/// <remarks>Information pertaining to the entire mzML file (i.e. not specific to any part of the data set) is stored here.</remarks>
	public class FileDescriptionType
	{
		private ParamGroupType fileContentField;
		private SourceFileListType sourceFileListField;
		private List<ParamGroupType> contactField;

		/// <remarks>This summarizes the different types of spectra that can be expected in the file. 
		/// This is expected to aid processing software in skipping files that do not contain appropriate 
		/// spectrum types for it. It should also describe the nativeID format used in the file by referring to an appropriate CV term.</remarks>
		/// min 1, max 1
		public ParamGroupType fileContent
		{
			get { return fileContentField; }
			set { fileContentField = value; }
		}

		/// min 0, max 1
		public SourceFileListType sourceFileList
		{
			get { return sourceFileListField; }
			set { sourceFileListField = value; }
		}

		/// min 0, max unbounded
		public ParamGroupType[] contact
		{
			get { return contactField.ToArray(); }
			set { contactField = value.ToList(); }
		}
	}
}
