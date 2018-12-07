using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;

namespace PSI_Interface.IdentData
{
    /// <summary>
    /// Read and perform some processing on a MZIdentML file
    /// Processes the data into an LCMS DataSet
    /// </summary>
    public class SimpleMZIdentMLReader
    {
        /// <summary>
        /// A wrapper around an exception that provides a single level of abstraction, but no additional detail.
        /// </summary>
        public class DuplicateKeyException : Exception
        {
            /// <summary>
            /// Default constructor
            /// </summary>
            public DuplicateKeyException() : base() { }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="message"></param>
            public DuplicateKeyException(string message) : base(message) { }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="message"></param>
            /// <param name="innerEx"></param>
            public DuplicateKeyException(string message, Exception innerEx) : base(message, innerEx) { }
        }

        /// <summary>
        /// Initialize a MzIdentMlReader object
        /// </summary>
        /// <param name="skipDuplicates">If true, duplicate IDs for DBSequence, Peptide, and PeptideEvidence will be dropped, rather than causing an exception</param>
        /// <param name="errorReporter">Action to perform when the parsing reports errors (output to console, log, etc.)</param>
        public SimpleMZIdentMLReader(bool skipDuplicates = false, Action<string> errorReporter = null)
        {
            dropDuplicates = skipDuplicates;
            errorReportAction = errorReporter;
        }

        #region NativeId Conversion
        /// <summary>
        /// Provides functionality to interpret a NativeID as a integer scan number
        /// Code is ported from MSData.cpp in ProteoWizard
        /// </summary>
        public static class NativeIdConversion
        {
            private static Dictionary<string, string> ParseNativeId(string nativeId)
            {
                var tokens = nativeId.Split(new[] { '\t', ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                var map = new Dictionary<string, string>();
                foreach (var token in tokens)
                {
                    var equals = token.IndexOf('=');
                    var name = token.Substring(0, equals);
                    var value = token.Substring(equals + 1);
                    map.Add(name, value);
                }
                return map;
            }

            /// <summary>
            /// Performs a "long.TryParse" on the interpreted scan number (single shot function)
            /// </summary>
            /// <param name="nativeId"></param>
            /// <param name="num"></param>
            /// <returns></returns>
            public static bool TryGetScanNumberLong(string nativeId, out long num)
            {
                return long.TryParse(GetScanNumber(nativeId), out num);
            }

            /// <summary>
            /// Performs a "int.TryParse" on the interpreted scan number (single shot function)
            /// </summary>
            /// <param name="nativeId"></param>
            /// <param name="num"></param>
            /// <returns></returns>
            public static bool TryGetScanNumberInt(string nativeId, out int num)
            {
                return int.TryParse(GetScanNumber(nativeId), out num);
            }

            /// <summary>
            /// Returns the integer-only portion of the nativeID that can be used for a scan number
            /// If the nativeID cannot be interpreted, the original value is returned.
            /// </summary>
            /// <param name="nativeId"></param>
            /// <returns></returns>
            public static string GetScanNumber(string nativeId)
            {
                // TODO: Add interpreter for Waters' S0F1, S1F1, S0F2,... format
                //switch (nativeIdFormat)
                //{
                //    case MS_spectrum_identifier_nativeID_format: // mzData
                //        return value(id, "spectrum");
                //
                //    case MS_multiple_peak_list_nativeID_format: // MGF
                //        return value(id, "index");
                //
                //    case MS_Agilent_MassHunter_nativeID_format:
                //        return value(id, "scanId");
                //
                //    case MS_Thermo_nativeID_format:
                //        // conversion from Thermo nativeIDs assumes default controller information
                //        if (id.find("controllerType=0 controllerNumber=1") != 0)
                //            return "";
                //
                //        // fall through to get scan
                //
                //    case MS_Bruker_Agilent_YEP_nativeID_format:
                //    case MS_Bruker_BAF_nativeID_format:
                //    case MS_scan_number_only_nativeID_format:
                //        return value(id, "scan");
                //
                //    default:
                //        if (bal::starts_with(id, "scan=")) return value(id, "scan");
                //        else if (bal::starts_with(id, "index=")) return value(id, "index");
                //        return "";
                //}
                if (nativeId.Contains("="))
                {
                    var map = ParseNativeId(nativeId);
                    if (map.ContainsKey("spectrum"))
                    {
                        return map["spectrum"];
                    }
                    if (map.ContainsKey("index"))
                    {
                        return map["index"];
                    }
                    if (map.ContainsKey("scanId"))
                    {
                        return map["scanId"];
                    }
                    if (map.ContainsKey("scan"))
                    {
                        return map["scan"];
                    }
                }

                // No equals sign, don't have parser breakdown
                // Or key data not found in breakdown of nativeId
                return nativeId;
            }

            //public static string GetNativeId(string scanNumber)
            //{
            //    switch (nativeIdFormat)
            //    {
            //        case MS_Thermo_nativeID_format:
            //            return "controllerType=0 controllerNumber=1 scan=" + scanNumber;
            //
            //        case MS_spectrum_identifier_nativeID_format:
            //            return "spectrum=" + scanNumber;
            //
            //        case MS_multiple_peak_list_nativeID_format:
            //            return "index=" + scanNumber;
            //
            //        case MS_Agilent_MassHunter_nativeID_format:
            //            return "scanId=" + scanNumber;
            //
            //        case MS_Bruker_Agilent_YEP_nativeID_format:
            //        case MS_Bruker_BAF_nativeID_format:
            //        case MS_scan_number_only_nativeID_format:
            //            return "scan=" + scanNumber;
            //
            //        default:
            //            return "";
            //    }
            //}
        }
        #endregion

        private readonly Dictionary<string, DatabaseSequence> m_database = new Dictionary<string, DatabaseSequence>();
        private readonly Dictionary<string, PeptideRef> m_peptides = new Dictionary<string, PeptideRef>();
        private readonly Dictionary<string, PeptideEvidence> m_evidences = new Dictionary<string, PeptideEvidence>();
        //private readonly Dictionary<string, SpectrumIdItem> m_specItems = new Dictionary<string, SpectrumIdItem>();
        private readonly Dictionary<string, Regex> m_decoyDbAccessionRegex = new Dictionary<string, Regex>();
        private string spectrumFile = string.Empty;
        private string softwareCvAccession = string.Empty;
        private string softwareName = string.Empty;
        private string softwareVersion = string.Empty;
        private bool isSpectrumIdNotAScanNum;
        private CancellationToken cancellationToken = default(CancellationToken);
        private readonly bool dropDuplicates;
        private readonly Action<string> errorReportAction;
        private readonly List<SearchModification> searchModifications = new List<SearchModification>();

        /// <summary>
        /// Information about a single search result
        /// </summary>
        public class SpectrumIdItem
        {
            #region Spectrum ID Public Properties

            /// <summary>
            /// Constructor
            /// </summary>
            public SpectrumIdItem()
            {
                PepEvidence = new List<PeptideEvidence>();
                AllParamsDict = new Dictionary<string, string>();
            }

            /// <summary>
            /// Unique identifier for this search result
            /// </summary>
            public string SpecItemId { get; set; }

            /// <summary>
            /// If the result passes the threshold provided to the search tool; if no threshold was provided, this is always true.
            /// </summary>
            public bool PassThreshold { get; set; }

            /// <summary>
            /// Rank of this result. Only used if there are multiple peptide results for a single spectrum
            /// </summary>
            /// <remarks>Used in numerical order, with 1 being the best result</remarks>
            public int Rank { get; set; }

            /// <summary>
            /// The peptide associated with this search result
            /// </summary>
            public PeptideRef Peptide { get; set; }

            /// <summary>
            /// Calculated, or Theoretical, m/z
            /// </summary>
            public double CalMz { get; set; }

            /// <summary>
            /// Experimental m/z
            /// </summary>
            public double ExperimentalMz { get; set; }

            /// <summary>
            /// Charge state of the peptide for this result
            /// </summary>
            public int Charge { get; set; }

            /// <summary>
            /// List of Peptide evidences associated with this result
            /// </summary>
            /// <remarks>
            /// There may be multiple peptide evidences associated with a single result. This means
            /// that there are multiple occurrences of the specified peptide in the protein database
            /// that was used by the search, in the same protein or in multiple proteins.
            /// </remarks>
            public List<PeptideEvidence> PepEvidence { get; }

            /// <summary>
            /// Dictionary of all CVParams provided with this search result
            /// </summary>
            public Dictionary<string, string> AllParamsDict { get; }

            /// <summary>
            /// Count of Peptide evidences associated with this result
            /// </summary>
            public int PepEvCount { get; set; }

            /// <summary>
            /// Raw score from MS-GF+
            /// </summary>
            public double RawScore { get; set; }

            /// <summary>
            /// DeNovo score from MS-GF+
            /// </summary>
            public int DeNovoScore { get; set; }

            /// <summary>
            /// SpecEValue score from MS-GF+
            /// </summary>
            public double SpecEv { get; set; }

            /// <summary>
            /// EValue score from MS-GF+
            /// </summary>
            public double EValue { get; set; }

            /// <summary>
            /// QValue score from MS-GF+
            /// </summary>
            public double QValue { get; set; }

            /// <summary>
            /// PepQValue score from MS-GF+
            /// </summary>
            public double PepQValue { get; set; }

            /// <summary>
            /// Isotope Error
            /// </summary>
            public int IsoError { get; set; }

            /// <summary>
            /// If the mzid results are from searching a DTA file
            /// </summary>
            [Obsolete("Use IsSpectrumIdNotTheScanNumber")]
            public bool IsDtaSpectrum
            {
                get => IsSpectrumIdNotTheScanNumber;
                set => IsSpectrumIdNotTheScanNumber = value;
            }

            /// <summary>
            /// If the mzid results are from searching a DTA file
            /// </summary>
            public bool IsSpectrumIdNotTheScanNumber { get; set; }

            /// <summary>
            /// Scan number, as specified in the mzid using a CVParam (if available)
            /// </summary>
            public int ScanNumCVParam => scanNum;

            private int scanNum = -1;

            /// <summary>
            /// Spectrum scan number
            /// </summary>
            public int ScanNum
            {
                get
                {
                    // Do not parse the SpectrumID for DTA file search results - the index is the DTA file index, not the spectrum index
                    if (!IsSpectrumIdNotTheScanNumber && !string.IsNullOrWhiteSpace(NativeId) && NativeIdConversion.TryGetScanNumberInt(NativeId, out var num))
                    {
                        return num;
                    }
                    return scanNum;
                }
                set => scanNum = value;
            }

            /// <summary>
            /// Scan time (elution time / retention time) in minutes
            /// </summary>
            public double ScanTimeMinutes { get; set; }

            /// <summary>
            /// Spectrum native id (if mzid contains this information)
            /// </summary>
            public string NativeId { get; set; }
            #endregion

        }

        /// <summary>
        /// Protein information
        /// </summary>
        public class DatabaseSequence
        {
            /// <summary>
            /// Accession code
            /// </summary>
            public string Accession { get; set; }

            /// <summary>
            /// Length of protein
            /// </summary>
            public int Length { get; set; }

            /// <summary>
            /// Protein description
            /// </summary>
            public string ProteinDescription { get; set; }

            /// <summary>
            /// Database Id - internal reference in mzid file
            /// </summary>
            internal string DatabaseId { get; set; }
        }

        /// <summary>
        /// Modification information
        /// </summary>
        public class Modification
        {
            /// <summary>
            /// Monoisotopic mass of the modification
            /// </summary>
            public double Mass { get; set; }

            /// <summary>
            /// Modification name
            /// </summary>
            public string Tag => Name;

            /// <summary>
            /// Modification name
            /// </summary>
            public string Name { get; set; }
        }

        /// <summary>
        /// Search modification information; information from <see cref="Modification"/> should be mappable to one of these objects
        /// </summary>
        public class SearchModification : Modification
        {
            /// <summary>
            /// If the modification is fixed/static
            /// </summary>
            public bool IsFixed { get; set; }

            /// <summary>
            /// If the modification affects the N-Terminus
            /// </summary>
            public bool IsNTerm { get; set; }

            /// <summary>
            /// If the modification affects the C-Terminus
            /// </summary>
            public bool IsCTerm { get; set; }

            /// <summary>
            /// Residues that can be affected by this modification; '.' is used to signify terminus modifications that can affect any residue
            /// </summary>
            public string Residues { get; set; }

            /// <summary>
            /// Compares 2 search modifications for equality, excluding <see cref="Residues"/> from the comparison
            /// </summary>
            /// <param name="other"></param>
            /// <returns>true if the modifications match (with the exception of the residues)</returns>
            /// <remarks>MS-GF+ adds all modifications and affected sites as distinct search modifications, so Phos STY becomes Phos S, Phos T, and Phos Y.</remarks>
            public bool AreModificationsSimilar(SearchModification other)
            {
                return Name.Equals(other.Name) && Mass.Equals(other.Mass) && IsFixed == other.IsFixed && IsNTerm == other.IsNTerm &&
                       IsCTerm == other.IsCTerm;
            }
        }

        /// <summary>
        /// Peptide information, including modifications
        /// </summary>
        public class PeptideRef
        {
            private readonly List<KeyValuePair<int, Modification>> mods;

            /// <summary>
            /// Constructor
            /// </summary>
            public PeptideRef()
            {
                mods = new List<KeyValuePair<int, Modification>>(10);
            }

            /// <summary>
            /// Peptide sequence
            /// </summary>
            public string Sequence { get; set; }

            /// <summary>
            /// Add the specified modification to the peptide at the specified location
            /// </summary>
            /// <param name="location">location of the modification in the peptide</param>
            /// <param name="mod">modification</param>
            public void ModsAdd(int location, Modification mod)
            {
                mods.Add(new KeyValuePair<int, Modification>(location, mod));
            }

            /// <summary>
            /// The List of modifications affecting this peptide - key (location of modification) should be considered as being 1-based (see remarks)
            /// </summary>
            /// <remarks>Key is location of the modification within the peptide - position in peptide sequence,
            /// counted from the N-terminus residue, starting at position 1. Specific modifications to the N-terminus
            /// should be given the location 0. Modification to the C-terminus should be given as peptide length + 1.
            /// If the modification location is unknown e.g. for PMF data, this attribute should be omitted.
            /// (See mzIdentML specification, version 1.1.0)</remarks>
            public List<KeyValuePair<int, Modification>> Mods => mods;

            /// <summary>
            /// Peptide sequence with numeric mods but without a prefix or suffix residue
            /// For the sequence without mods, use <see cref="Sequence"/>
            /// </summary>
            public string SequenceWithNumericMods
            {
                get
                {
                    if (string.IsNullOrEmpty(sequenceWithNumericMods))
                    {
                        var sequenceText = Sequence; // Strings are immutable, don't need a copy (cannot change the value of Sequence using sequenceText)
                        var sequenceOrigLength = Sequence.Length;
                        // Insert the mods from the last occurring to the first.
                        foreach (var mod in Mods.OrderByDescending(x => x.Key))
                        {
                            var sign = "+";
                            if (mod.Value.Mass < 0)
                            {
                                // Mod is negative, C# will include the minus sign before the number
                                sign = "";
                            }
                            // Using "0.0##" to use 3 decimal places, but drop trailing zeros - "F3" would keep trailing zeros.
                            var loc = mod.Key;
                            if (loc > sequenceOrigLength)
                            {
                                // C-terminal modification - the location is sequence length + 1, but it really just goes at the end.
                                loc = sequenceOrigLength;
                            }
                            var leftSide = sequenceText.Substring(0, loc);
                            var rightSide = sequenceText.Substring(loc);
                            sequenceText = leftSide + sign + string.Format("{0:0.0##}", mod.Value.Mass) + rightSide;
                        }
                        sequenceWithNumericMods = sequenceText;
                    }
                    return sequenceWithNumericMods;
                }
            }

            private string sequenceWithNumericMods;
        }

        /// <summary>
        /// Peptide result information
        /// </summary>
        public class PeptideEvidence
        {
            /// <summary>
            /// If the result is a decoy
            /// </summary>
            public bool IsDecoy { get; set; }

            /// <summary>
            /// Set to true if the 'isDecoy' attribute was present in the mzid file for this peptide evidence
            /// </summary>
            internal bool HadDecoyAttribute { get; set; }

            /// <summary>
            /// Peptide suffix / post flanking residue
            /// </summary>
            /// <remarks>Is "-" for C-term</remarks>
            public string Post { get; set; }

            /// <summary>
            /// Peptide prefix / previous flanking residue
            /// </summary>
            /// <remarks>Is "-" for N-term</remarks>
            public string Pre { get; set; }

            /// <summary>
            /// Index of the last amino acid in peptide in the protein sequence (using 1-based indexing on the protein sequence)
            /// </summary>
            public int End { get; set; }

            /// <summary>
            /// Index of the first amino acid in peptide in the protein sequence (using 1-based indexing on the protein sequence)
            /// </summary>
            public int Start { get; set; }

            /// <summary>
            /// Peptide and modification information
            /// </summary>
            public PeptideRef PeptideRef { get; set; }

            /// <summary>
            /// Peptide sequence with numeric mods and prefix/suffix residues
            /// For the sequence without mods, use <see cref="PSI_Interface.IdentData.SimpleMZIdentMLReader.PeptideRef.Sequence"/>
            /// </summary>
            public string SequenceWithNumericMods
            {
                get
                {
                    if (string.IsNullOrEmpty(sequenceWithNumericMods))
                    {
                        var sequenceText = PeptideRef.SequenceWithNumericMods;
                        sequenceText = Pre + "." + sequenceText + "." + Post;
                        sequenceWithNumericMods = sequenceText;
                    }
                    return sequenceWithNumericMods;
                }
            }

            private string sequenceWithNumericMods = string.Empty;

            /// <summary>
            /// Protein information
            /// </summary>
            public DatabaseSequence DbSeq { get; set; }
        }

        /// <summary>
        /// Container class for holding the mzIdentML metadata
        /// </summary>
        public abstract class SimpleMZIdentMLMetaData
        {
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="path">path to the mzid file</param>
            protected SimpleMZIdentMLMetaData(string path)
            {
                DatasetFile = path;
            }

            /// <summary>
            /// Path to the mzid file
            /// </summary>
            public string DatasetFile { get; }

            /// <summary>
            /// Name of the spectrum file used for the search
            /// </summary>
            public string SpectrumFile { get; internal set; }

            /// <summary>
            /// Get the name of the analysis software
            /// </summary>
            public string AnalysisSoftware { get; internal set; }

            /// <summary>
            /// Get the Controlled Vocabulary accession of the analysis software, if available
            /// </summary>
            public string AnalysisSoftwareCvAccession { get; internal set; }

            /// <summary>
            /// Get the version of the analysis software, if provided
            /// </summary>
            public string AnalysisSoftwareVersion { get; internal set; }

            /// <summary>
            /// The modification settings for the search
            /// </summary>
            public List<SearchModification> SearchModifications { get; } = new List<SearchModification>();
        }

        /// <summary>
        /// Container class for holding the mzIdentML data, when reading all results into memory
        /// </summary>
        public class SimpleMZIdentMLData : SimpleMZIdentMLMetaData
        {
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="path">path to the mzid file</param>
            internal SimpleMZIdentMLData(string path) : base(path)
            {
            }

            /// <summary>
            /// List of identifications contained in <see cref="SimpleMZIdentMLMetaData.DatasetFile"/>
            /// </summary>
            public readonly List<SpectrumIdItem> Identifications = new List<SpectrumIdItem>();
        }

        /// <summary>
        /// Container class for holding the mzIdentML data, when reading low-memory style; disposable to properly close the XML reader
        /// </summary>
        public class SimpleMZIdentMLDataLowMem : SimpleMZIdentMLMetaData, IDisposable
        {
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="path">path to the mzid file</param>
            /// <param name="identificationsEnumerator"></param>
            /// <param name="dataReader"></param>
            internal SimpleMZIdentMLDataLowMem(string path, IEnumerator<SpectrumIdItem> identificationsEnumerator, XmlReader dataReader) : base(path)
            {
                identEnumerator = identificationsEnumerator;
                xmlDataReader = dataReader;
            }

            private readonly XmlReader xmlDataReader;
            private readonly IEnumerator<SpectrumIdItem> identEnumerator;

            /// <summary>
            /// List of identifications contained in <see cref="SimpleMZIdentMLMetaData.DatasetFile"/>
            /// </summary>
            public IEnumerable<SpectrumIdItem> Identifications
            {
                get
                {
                    // if MoveNext has previously been called, return the current item
                    if (identEnumerator.Current != null)
                    {
                        yield return identEnumerator.Current;
                    }

                    // Return all of the items
                    while (identEnumerator.MoveNext())
                    {
                        yield return identEnumerator.Current;
                    }
                }
            }

            /// <summary>
            /// Close the xmlReader that is supplying data to <see cref="Identifications"/>
            /// </summary>
            public void Dispose()
            {
                xmlDataReader?.Dispose();
            }
        }

        /// <summary>
        /// Entry point for SimpleMZIdentMLReader
        /// Read the MZIdentML file, map the data to easy-to-use objects, and return the collection of objects
        /// </summary>
        /// <param name="path">Path to *.mzid/mzIdentML file</param>
        /// <param name="cancelToken">Cancellation token, to interrupt reading between spectra</param>
        /// <returns><see cref="SimpleMZIdentMLData"/></returns>
        /// <remarks>
        /// XML Reader parses an MZIdentML file, storing data as follows:
        ///   PeptideRef holds Peptide data, such as sequence, number, and type of modifications
        ///   Database Information holds the length of the peptide and the protein description
        ///   Peptide Evidence holds the pre, post, start and end for the peptide for Tryptic End calculations.
        /// The element that holds the most information is the Spectrum ID Item, which has the calculated mz,
        /// experimental mz, charge state, MSGF raw score, DeNovo score, MSGF SpecEValue, MSGF EValue,
        /// MSGF QValue, MSGR PepQValue, Scan number as well as which peptide it is and which evidences
        /// it has from the analysis run.
        /// </remarks>
        public SimpleMZIdentMLData Read(string path, CancellationToken cancelToken = default(CancellationToken))
        {
            cancellationToken = cancelToken;
            var sourceFile = new FileInfo(path);
            if (!sourceFile.Exists)
                throw new FileNotFoundException(".mzID file not found", path);

            // Set a large buffer size. Doesn't affect gzip reading speed, but speeds up non-gzipped
            // Stream file should be closed properly via close/dispose chaining from the XmlReader
            Stream file = new FileStream(sourceFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 65536);
            if (sourceFile.Name.Trim().EndsWith(".mzid.gz", StringComparison.OrdinalIgnoreCase))
            {
                file = new GZipStream(file, CompressionMode.Decompress);
            }

            var results = new SimpleMZIdentMLData(sourceFile.FullName);
            var xSettings = new XmlReaderSettings { IgnoreWhitespace = true };
            using (var reader = XmlReader.Create(new StreamReader(file, System.Text.Encoding.UTF8, true, 65536), xSettings))
            {
                // Read in the file
                var spectrumMatches = ReadMzIdentMl(reader);

                // Read first, otherwise the member data fields will not be populated
                results.Identifications.AddRange(spectrumMatches);

                results.SpectrumFile = spectrumFile;
                results.AnalysisSoftware = softwareName;
                results.AnalysisSoftwareVersion = softwareVersion;
                results.AnalysisSoftwareCvAccession = softwareCvAccession;
                results.SearchModifications.AddRange(searchModifications);
            }
            return results;
        }

        /// <summary>
        /// Entry point for SimpleMZIdentMLReader
        /// Read the MZIdentML file, map the data to easy-to-use objects, and return the collection of objects
        /// Returns only the metadata, and reads the identifications as they are read from the returned object
        /// </summary>
        /// <param name="path">Path to *.mzid/mzIdentML file</param>
        /// <param name="cancelToken">Cancellation token, to interrupt reading between spectra</param>
        /// <returns><see cref="SimpleMZIdentMLData"/></returns>
        /// <remarks>
        /// XML Reader parses an MZIdentML file, storing data as follows:
        ///   PeptideRef holds Peptide data, such as sequence, number, and type of modifications
        ///   Database Information holds the length of the peptide and the protein description
        ///   Peptide Evidence holds the pre, post, start and end for the peptide for Tryptic End calculations.
        /// The element that holds the most information is the Spectrum ID Item, which has the calculated mz,
        /// experimental mz, charge state, MSGF raw score, DeNovo score, MSGF SpecEValue, MSGF EValue,
        /// MSGF QValue, MSGR PepQValue, Scan number as well as which peptide it is and which evidences
        /// it has from the analysis run.
        /// </remarks>
        public SimpleMZIdentMLDataLowMem ReadLowMem(string path, CancellationToken cancelToken = default(CancellationToken))
        {
            cancellationToken = cancelToken;
            var sourceFile = new FileInfo(path);
            if (!sourceFile.Exists)
                throw new FileNotFoundException(".mzID file not found", path);

            // Set a large buffer size. Doesn't affect gzip reading speed, but speeds up non-gzipped
            Stream file = new FileStream(sourceFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 65536);

            if (sourceFile.Name.Trim().EndsWith(".mzid.gz", StringComparison.OrdinalIgnoreCase))
            {
                file = new GZipStream(file, CompressionMode.Decompress);
            }

            var xSettings = new XmlReaderSettings { IgnoreWhitespace = true };
            var reader = XmlReader.Create(new StreamReader(file, System.Text.Encoding.UTF8, true, 65536), xSettings);

            var enumerator = ReadMzIdentMl(reader).GetEnumerator();

            // Read in the file metadata, stopping after the first yield return.
            enumerator.MoveNext();

            var results = new SimpleMZIdentMLDataLowMem(sourceFile.FullName, enumerator, reader)
            {
                SpectrumFile = spectrumFile,
                AnalysisSoftware = softwareName,
                AnalysisSoftwareVersion = softwareVersion,
                AnalysisSoftwareCvAccession = softwareCvAccession,
            };

            results.SearchModifications.AddRange(searchModifications);

            return results;
        }

        /// <summary>
        /// If the reader is at an EndElement, read it so we can move on to the next node
        /// Otherwise, if the element name matches currentNodeName, move to the next node
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="currentNodeName"></param>
        private void PossiblyReadEndElement(XmlReader reader, string currentNodeName)
        {
            // If the Node is empty, the end element may not be present
            if (reader.NodeType == XmlNodeType.EndElement)
            {
                reader.ReadEndElement();
            }
            else if (reader.Name == currentNodeName)
            {
                // Empty Node; move on to the next element
                reader.Read();
            }
        }

        /// <summary>
        /// Read and parse a .mzid file, or mzIdentML
        /// Files are commonly larger than 30 MB, so use a streaming reader instead of a DOM reader
        /// </summary>
        /// <param name="reader">XmlReader object for the file to be read</param>
        private IEnumerable<SpectrumIdItem> ReadMzIdentMl(XmlReader reader)
        {
            // Handle disposal of allocated object correctly
            using (reader)
            {
                // Guarantee a move to the root node
                reader.MoveToContent();
                // Consume the MzIdentML root tag
                // Throws exception if we are not at the "MzIdentML" tag.
                // This is a critical error; we want to stop processing for this file if we encounter this error
                reader.ReadStartElement("MzIdentML");
                // Read the next node - should be the first child node
                while (reader.ReadState == ReadState.Interactive)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }
                    // Handle exiting out properly at EndElement tags
                    if (reader.NodeType != XmlNodeType.Element)
                    {
                        reader.Read();
                        continue;
                    }
                    // Handle each 1st level as a chunk
                    switch (reader.Name)
                    {
                        case "cvList":
                            // Schema requirements: one instance of this element
                            reader.Skip();
                            break;
                        case "AnalysisSoftwareList":
                            // Schema requirements: zero to one instances of this element
                            ReadAnalysisSoftwareList(reader.ReadSubtree());
                            PossiblyReadEndElement(reader, "AnalysisSoftwareList");
                            break;
                        case "Provider":
                            // Schema requirements: zero to one instances of this element
                            reader.Skip();
                            break;
                        case "AuditCollection":
                            // Schema requirements: zero to one instances of this element
                            reader.Skip();
                            break;
                        case "AnalysisSampleCollection":
                            // Schema requirements: zero to one instances of this element
                            reader.Skip();
                            break;
                        case "SequenceCollection":
                            // Schema requirements: zero to one instances of this element
                            // Use reader.ReadSubtree() to provide an XmlReader that is only valid for the element and child nodes
                            ReadSequenceCollection(reader.ReadSubtree());
                            PossiblyReadEndElement(reader, "SequenceCollection");
                            break;
                        case "AnalysisCollection":
                            // Schema requirements: one instance of this element
                            reader.Skip();
                            break;
                        case "AnalysisProtocolCollection":
                            // Schema requirements: one instance of this element
                            ReadAnalysisProtocolCollection(reader.ReadSubtree());
                            PossiblyReadEndElement(reader, "AnalysisProtocolCollection");
                            break;
                        case "DataCollection":
                            // Schema requirements: one instance of this element
                            // Use reader.ReadSubtree() to provide an XmlReader that is only valid for the element and child nodes
                            // we can use "return ReadDataCollection(...)", but then that would immediately exit the function, without any subsequent operations occurring (like closing the reader)
                            foreach (var item in ReadDataCollection(reader.ReadSubtree()))
                            {
                                yield return item;
                            }
                            PossiblyReadEndElement(reader, "DataCollection");
                            break;
                        case "BibliographicReference":
                            // Schema requirements: zero to many instances of this element
                            reader.Skip();
                            break;
                        default:
                            // We are not reading anything out of the tag, so bypass it
                            reader.Skip();
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Handle the child nodes of the AnalysisSoftwareList element
        /// Called by ReadMzIdentML (xml hierarchy)
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the single AnalysisSoftwareList element</param>
        private void ReadAnalysisSoftwareList(XmlReader reader)
        {
            reader.MoveToContent();
            reader.ReadStartElement("AnalysisSoftwareList"); // Throws exception if we are not at the "SequenceCollection" tag.
            while (reader.ReadState == ReadState.Interactive)
            {
                // Handle exiting out properly at EndElement tags
                if (reader.NodeType != XmlNodeType.Element)
                {
                    reader.Read();
                    continue;
                }
                switch (reader.Name)
                {
                    case "AnalysisSoftware":
                        // Schema requirements: one to many instances of this element
                        // Use reader.ReadSubtree() to provide an XmlReader that is only valid for the element and child nodes
                        ReadAnalysisSoftware(reader.ReadSubtree());
                        PossiblyReadEndElement(reader, "AnalysisSoftware");
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }
            reader.Dispose();
        }

        /// <summary>
        /// Handle the child nodes of the AnalysisSoftware element
        /// Called by ReadAnalysisSoftwareList (xml hierarchy)
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the AnalysisSoftware element</param>
        private void ReadAnalysisSoftware(XmlReader reader)
        {
            reader.MoveToContent();
            var version = reader.GetAttribute("version");
            if (!string.IsNullOrWhiteSpace(version) && string.IsNullOrWhiteSpace(softwareVersion))
            {
                softwareVersion = version;
            }
            reader.ReadStartElement("AnalysisSoftware"); // Throws exception if we are not at the "AnalysisSoftware" tag.
            while (reader.ReadState == ReadState.Interactive)
            {
                // Handle exiting out properly at EndElement tags
                if (reader.NodeType != XmlNodeType.Element)
                {
                    reader.Read();
                    continue;
                }
                switch (reader.Name)
                {
                    case "SoftwareName":
                        // Schema requirements: one to many instances of this element
                        // Use reader.ReadSubtree() to provide an XmlReader that is only valid for the element and child nodes
                        var innerReader = reader.ReadSubtree();
                        innerReader.MoveToContent();
                        innerReader.ReadStartElement("SoftwareName");
                        while (innerReader.ReadState == ReadState.Interactive)
                        {
                            if (innerReader.NodeType != XmlNodeType.Element)
                            {
                                innerReader.Read();
                                continue;
                            }
                            if (innerReader.Name.Equals("cvParam") || innerReader.Name.Equals("userParam"))
                            {
                                var name = innerReader.GetAttribute("name");
                                if (!string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(softwareName))
                                {
                                    softwareName = name;
                                }
                                var accession = reader.GetAttribute("accession");
                                if (!string.IsNullOrWhiteSpace(accession) && string.IsNullOrWhiteSpace(softwareCvAccession))
                                {
                                    softwareCvAccession = accession;
                                }
                            }
                            innerReader.Read();
                        }
                        innerReader.Dispose();
                        // Consume the EndElement
                        PossiblyReadEndElement(reader, "SoftwareName");
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }
            reader.Dispose();
        }

        /// <summary>
        /// Handle the child nodes of the SequenceCollection element
        /// Called by ReadMzIdentML (xml hierarchy)
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the single SequenceCollection element</param>
        private void ReadSequenceCollection(XmlReader reader)
        {
            reader.MoveToContent();
            reader.ReadStartElement("SequenceCollection"); // Throws exception if we are not at the "SequenceCollection" tag.
            while (reader.ReadState == ReadState.Interactive)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                // Handle exiting out properly at EndElement tags
                if (reader.NodeType != XmlNodeType.Element)
                {
                    reader.Read();
                    continue;
                }
                switch (reader.Name)
                {
                    case "DBSequence":
                        // Schema requirements: one to many instances of this element
                        // Use reader.ReadSubtree() to provide an XmlReader that is only valid for the element and child nodes
                        ReadDbSequence(reader.ReadSubtree());
                        // "DBSequence" might not have any child nodes
                        // We will either consume the EndElement, or the same element that was passed to ReadDBSequence (in case of no child nodes)
                        reader.Read();
                        break;
                    case "Peptide":
                        // Schema requirements: zero to many instances of this element
                        // Use reader.ReadSubtree() to provide an XmlReader that is only valid for the element and child nodes
                        ReadPeptide(reader.ReadSubtree());
                        // "Peptide" might not have any child nodes
                        // We will either consume the EndElement, or the same element that was passed to ReadPeptide (in case of no child nodes)
                        reader.Read();
                        break;
                    case "PeptideEvidence":
                        // Schema requirements: zero to many instances of this element
                        // Use reader.ReadSubtree() to provide an XmlReader that is only valid for the element and child nodes
                        ReadPeptideEvidence(reader.ReadSubtree());
                        // "PeptideEvidence" might not have any child nodes; guarantee advance
                        // We will either consume the EndElement, or the same element that was passed to ReadPeptideEvidence (in case of no child nodes)
                        reader.Read();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }
            reader.Dispose();
        }

        /// <summary>
        /// Handle DBSequence element
        /// Called by ReadSequenceCollection (xml hierarchy)
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the single DBSequence element</param>
        private void ReadDbSequence(XmlReader reader)
        {
            reader.MoveToContent();
            var id = reader.GetAttribute("id");
            var dbId = reader.GetAttribute("searchDatabase_ref");
            if (id != null)
            {
                var dbSeq = new DatabaseSequence
                {
                    DatabaseId = dbId,
                    Length = Convert.ToInt32(reader.GetAttribute("length")),
                    Accession = reader.GetAttribute("accession")
                };
                if (reader.ReadToDescendant("cvParam"))
                {
                    dbSeq.ProteinDescription = reader.GetAttribute("value"); //.Split(' ')[0];
                }

                if (m_database.ContainsKey(id))
                {
                    if (!dropDuplicates)
                    {
                        throw new DuplicateKeyException($"Cannot add duplicate DBSequence id \"{id}\"!");
                    }

                    errorReportAction?.Invoke($"Dropping duplicate DBSequence id \"{id}\"!");
                }
                else
                {
                    m_database.Add(id, dbSeq);
                }
            }
            reader.Dispose();
        }

        /// <summary>
        /// Handle Peptide element
        /// Called by ReadSequenceCollection (xml hierarchy)
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the single Peptide element</param>
        private void ReadPeptide(XmlReader reader)
        {
            reader.MoveToContent();
            var id = reader.GetAttribute("id");
            if (id != null)
            {
                var pepRef = new PeptideRef();
                reader.ReadToDescendant("PeptideSequence");
                pepRef.Sequence = reader.ReadElementContentAsString(); // record the peptide sequence, and consume the start and end elements
                // Read in all the modifications
                // If a modification exists, we are already on the start tag for it
                while (reader.Name == "Modification")
                {
                    var mod = new Modification
                    {
                        Mass = Convert.ToDouble(reader.GetAttribute("monoisotopicMassDelta"))
                    };
                    var mods = new KeyValuePair<int, Modification>(Convert.ToInt32(reader.GetAttribute("location")), mod);
                    // Read down to get the name of the modification, then add the modification to the peptide reference
                    reader.ReadToDescendant("cvParam"); // The cvParam child node is required

                    mod.Name = reader.GetAttribute("name");
                    var modAcc = reader.GetAttribute("accession");
                    var modVal = reader.GetAttribute("value");
                    if ("MS:1001460".Equals(modAcc) && !string.IsNullOrWhiteSpace(modVal))
                    {
                        // MS-GF+ stores a modification name given in the mods file as the value for "unknown modification"
                        // Read it and use it, instead of the less useful "unknown modification"
                        mod.Name = modVal;
                    }
                    // TODO: neutral losses are also defined here, so make sure to read those appropriately...
                    pepRef.ModsAdd(mods.Key, mods.Value);

                    // There could theoretically exist more than one cvParam element. Clear them out.
                    while (reader.ReadToNextSibling("cvParam"))
                    {
                        // TODO: A neutral loss may be defined here.
                        // This is supposed to be empty. The loop condition does everything that needs to happen
                    }
                    reader.ReadEndElement(); // Consume EndElement for Modification
                }

                if (m_peptides.ContainsKey(id))
                {
                    if (!dropDuplicates)
                    {
                        throw new DuplicateKeyException($"Cannot add duplicate peptide id \"{id}\": {pepRef.SequenceWithNumericMods}");
                    }

                    errorReportAction?.Invoke($"Dropping duplicate peptide id \"{id}\": {pepRef.SequenceWithNumericMods}");
                }
                else
                {
                    m_peptides.Add(id, pepRef);
                }
            }
            reader.Dispose();
        }

        /// <summary>
        /// Handle PeptideEvidence element
        /// Called by ReadSequenceCollection (xml hierarchy)
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the single PeptideEvidence element</param>
        private void ReadPeptideEvidence(XmlReader reader)
        {
            reader.MoveToContent();
            var pepEvidence = new PeptideEvidence
            {
                IsDecoy = Convert.ToBoolean(reader.GetAttribute("isDecoy")),
                HadDecoyAttribute = !string.IsNullOrWhiteSpace(reader.GetAttribute("isDecoy")),
                Post = reader.GetAttribute("post"),
                Pre = reader.GetAttribute("pre"),
                End = Convert.ToInt32(reader.GetAttribute("end")),
                Start = Convert.ToInt32(reader.GetAttribute("start")),
                // ReSharper disable once AssignNullToNotNullAttribute
                PeptideRef = m_peptides[reader.GetAttribute("peptide_ref")],
                // ReSharper disable once AssignNullToNotNullAttribute
                DbSeq = m_database[reader.GetAttribute("dBSequence_ref")]
            };
            var id = reader.GetAttribute("id");

            // ReSharper disable once AssignNullToNotNullAttribute
            if (m_evidences.ContainsKey(id))
            {
                if (!dropDuplicates)
                {
                    throw new DuplicateKeyException($"Cannot add duplicate peptideEvidence id \"{id}\"!");
                }

                errorReportAction?.Invoke($"Skipping duplicate peptideEvidence id \"{id}\"!");
            }
            else
            {
                m_evidences.Add(id, pepEvidence);
            }

            reader.Dispose();
        }

        /// <summary>
        /// Handle AnalysisProtocolCollection element
        /// Called by ReadMzIdentML (xml hierarchy)
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the single AnalysisProtocolCollection element</param>
        private void ReadAnalysisProtocolCollection(XmlReader reader)
        {
            reader.MoveToContent();
            reader.ReadStartElement("AnalysisProtocolCollection"); // Throws exception if we are not at the "AnalysisProtocolCollection" tag.
            while (reader.ReadState == ReadState.Interactive)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                // Handle exiting out properly at EndElement tags
                if (reader.NodeType != XmlNodeType.Element)
                {
                    reader.Read();
                    continue;
                }
                switch (reader.Name)
                {
                    case "SpectrumIdentificationProtocol":
                        // Schema requirements: one to many instances of this element
                        // Use reader.ReadSubtree() to provide an XmlReader that is only valid for the element and child nodes
                        ReadSpectrumIdentificationProtocol(reader.ReadSubtree());
                        PossiblyReadEndElement(reader, "SpectrumIdentificationProtocol");
                        break;
                    case "ProteinDetectionProtocol":
                        // Schema requirements: zero to one instances of this element
                        reader.Skip();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }
            reader.Dispose();
        }

        /// <summary>
        /// Handle SpectrumIdentificationProtocol element
        /// Called by ReadMzIdentML (xml hierarchy)
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the single SpectrumIdentificationProtocol element</param>
        private void ReadSpectrumIdentificationProtocol(XmlReader reader)
        {
            reader.MoveToContent();
            reader.ReadStartElement("SpectrumIdentificationProtocol"); // Throws exception if we are not at the "SpectrumIdentificationProtocol" tag.
            while (reader.ReadState == ReadState.Interactive)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                // Handle exiting out properly at EndElement tags
                if (reader.NodeType != XmlNodeType.Element)
                {
                    reader.Read();
                    continue;
                }
                switch (reader.Name)
                {
                    case "SearchType":
                        // Schema requirements: one instance of this element
                        reader.Skip();
                        break;
                    case "AdditionalSearchParams":
                        // Schema requirements: zero to one instances of this element
                        reader.Skip();
                        break;
                    case "ModificationParams":
                        // Schema requirements: zero to one instances of this element
                        // Use reader.ReadSubtree() to provide an XmlReader that is only valid for the element and child nodes
                        ReadModificationParams(reader.ReadSubtree());
                        PossiblyReadEndElement(reader, "ModificationParams");
                        break;
                    case "Enzymes":
                        // Schema requirements: zero to one instances of this element
                        reader.Skip();
                        break;
                    case "MassTable":
                        // Schema requirements: zero to many instances of this element
                        reader.Skip();
                        break;
                    case "FragmentTolerance":
                        // Schema requirements: zero to one instances of this element
                        reader.Skip();
                        break;
                    case "ParentTolerance":
                        // Schema requirements: zero to one instances of this element
                        reader.Skip();
                        break;
                    case "Threshold":
                        // Schema requirements: one instance of this element
                        reader.Skip();
                        break;
                    case "DatabaseFilters":
                        // Schema requirements: zero to one instances of this element
                        reader.Skip();
                        break;
                    case "DatabaseTranslation":
                        // Schema requirements: zero to one instances of this element
                        reader.Skip();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }
            reader.Dispose();
        }

        /// <summary>
        /// Handle ModificationParams element
        /// Called by ReadMzIdentML (xml hierarchy)
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the single ModificationParams element</param>
        private void ReadModificationParams(XmlReader reader)
        {
            reader.MoveToContent();
            reader.ReadStartElement("ModificationParams"); // Throws exception if we are not at the "ModificationParams" tag.
            while (reader.ReadState == ReadState.Interactive)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                // Handle exiting out properly at EndElement tags
                if (reader.NodeType != XmlNodeType.Element)
                {
                    reader.Read();
                    continue;
                }
                switch (reader.Name)
                {
                    case "SearchModification":
                        // Schema requirements: one to many instances of this element
                        // Use reader.ReadSubtree() to provide an XmlReader that is only valid for the element and child nodes
                        ReadSearchModification(reader.ReadSubtree());
                        PossiblyReadEndElement(reader, "SearchModification");
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }
            reader.Dispose();
        }

        /// <summary>
        /// Handle SearchModification element
        /// Called by ReadMzIdentML (xml hierarchy)
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the single SearchModification element</param>
        private void ReadSearchModification(XmlReader reader)
        {
            var modSetting = new SearchModification();
            reader.MoveToContent();

            modSetting.IsFixed = Convert.ToBoolean(reader.GetAttribute("fixedMod"));
            modSetting.Mass = Convert.ToDouble(reader.GetAttribute("massDelta"));
            modSetting.Residues = reader.GetAttribute("residues");

            reader.ReadStartElement("SearchModification"); // Throws exception if we are not at the "SearchModification" tag.
            while (reader.ReadState == ReadState.Interactive)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                // Handle exiting out properly at EndElement tags
                if (reader.NodeType != XmlNodeType.Element)
                {
                    reader.Read();
                    continue;
                }
                switch (reader.Name)
                {
                    case "SpecificityRules":
                        // Schema requirements: zero to one instances of this element
                        // Use reader.ReadSubtree() to provide an XmlReader that is only valid for the element and child nodes
                        ReadSpecificityRules(reader.ReadSubtree(), modSetting);
                        PossiblyReadEndElement(reader, "SpecificityRules");
                        break;
                    case "cvParam":
                        // Schema requirements: one to many instances of this element
                        // Use reader.ReadSubtree() to provide an XmlReader that is only valid for the element and child nodes
                        var accession = reader.GetAttribute("accession");
                        if (accession != null)
                        {
                            if (accession.IndexOf("unimod", StringComparison.OrdinalIgnoreCase) >= 0 || accession.Equals("MS:1001460"))
                            {
                                modSetting.Name = reader.GetAttribute("name");
                            }
                        }
                        reader.Read(); // Consume the cvParam element (no child nodes)
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }


            if (searchModifications.Any(x => x.AreModificationsSimilar(modSetting)))
            {
                // if only the residues don't match, then just add the residues to the existing modification
                var existing = searchModifications.First(x => x.AreModificationsSimilar(modSetting));
                existing.Residues += modSetting.Residues;
            }
            else
            {
                searchModifications.Add(modSetting);
            }

            reader.Dispose();
        }

        /// <summary>
        /// Handle SpecificityRules element
        /// Called by ReadMzIdentML (xml hierarchy)
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the single SpecificityRules element</param>
        /// <param name="modSetting">SearchModification object</param>
        private void ReadSpecificityRules(XmlReader reader, SearchModification modSetting)
        {
            reader.MoveToContent();
            reader.ReadStartElement("SpecificityRules"); // Throws exception if we are not at the "SpecificityRules" tag.
            while (reader.ReadState == ReadState.Interactive)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                // Handle exiting out properly at EndElement tags
                if (reader.NodeType != XmlNodeType.Element)
                {
                    reader.Read();
                    continue;
                }
                switch (reader.Name)
                {
                    case "cvParam":
                        // Schema requirements: one to many instances of this element
                        // Use reader.ReadSubtree() to provide an XmlReader that is only valid for the element and child nodes
                        var accession = reader.GetAttribute("accession");
                        if (accession != null)
                        {
                            if (accession.Equals("MS:1001189") || accession.Equals("MS:1002057"))
                            {
                                // Modification Specificity Peptide-N-Term: MS:1001189
                                // Modification Specificity Protein-N-Term: MS:1002057
                                modSetting.IsNTerm = true;
                            }
                            else if (accession.Equals("MS:1001190") || accession.Equals("MS:1002058"))
                            {
                                // Modification Specificity Peptide-C-Term: MS:1001190
                                // Modification Specificity Protein-C-Term: MS:1002058
                                modSetting.IsCTerm = true;
                            }
                        }
                        reader.Read(); // Consume the cvParam element (no child nodes)
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Dispose();
        }

        /// <summary>
        /// Handle the child nodes of the DataCollection element
        /// Called by ReadMzIdentML (xml hierarchy)
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the single DataCollection element</param>
        private IEnumerable<SpectrumIdItem> ReadDataCollection(XmlReader reader)
        {
            reader.MoveToContent();
            reader.ReadStartElement("DataCollection"); // Throws exception if we are not at the "DataCollection" tag.
            while (reader.ReadState == ReadState.Interactive)
            {
                // Handle exiting out properly at EndElement tags
                if (reader.NodeType != XmlNodeType.Element)
                {
                    reader.Read();
                    continue;
                }
                switch (reader.Name)
                {
                    case "Inputs":
                        ReadInputs(reader.ReadSubtree());
                        PossiblyReadEndElement(reader, "Inputs");
                        break;
                    case "AnalysisData":
                        // Schema requirements: one and only one instance of this element
                        // Use reader.ReadSubtree() to provide an XmlReader that is only valid for the element and child nodes
                        // we can use "return ReadDataCollection(...)", but then that would immediately exit the function, without any subsequent operations occurring (like closing the reader)
                        foreach (var item in ReadAnalysisData(reader.ReadSubtree()))
                        {
                            yield return item;
                        }
                        PossiblyReadEndElement(reader, "AnalysisData");
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }
            reader.Dispose();
        }

        /// <summary>
        /// Handle child nodes of Inputs element
        /// Called by ReadDataCollection (xml hierarchy)
        /// Currently we are only working with the SpectraData child elements
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the single AnalysisData element</param>
        private void ReadInputs(XmlReader reader)
        {
            reader.MoveToContent();
            reader.ReadStartElement("Inputs"); // Throws exception if we are not at the "AnalysisData" tag.
            while (reader.ReadState == ReadState.Interactive)
            {
                // Handle exiting out properly at EndElement tags
                if (reader.NodeType != XmlNodeType.Element)
                {
                    reader.Read();
                    continue;
                }

                switch (reader.Name)
                {
                    case "SpectraData":
                        // Schema requirements: one to many instances of this element
                        // location attribute: required
                        // id attribute: required
                        // SpectrumIDFormat child element: required
                        var location = reader.GetAttribute("location");
                        spectrumFile = Path.GetFileName(location);
                        if (location != null && (location.Trim().EndsWith("_dta.txt", StringComparison.OrdinalIgnoreCase)
                                                 || location.Trim().EndsWith(".mgf", StringComparison.OrdinalIgnoreCase)
                                                 || location.Trim().EndsWith(".ms2", StringComparison.OrdinalIgnoreCase)))
                        {
                            isSpectrumIdNotAScanNum = true;
                        }
                        reader.Skip(); // "SpectraData" child nodes - we currently don't use them.
                        break;
                    case "SearchDatabase":
                        // Schema requirements: zero to many instances of this element
                        ReadSearchDatabase(reader.ReadSubtree());
                        PossiblyReadEndElement(reader, "SearchDatabase");
                        break;
                    case "userParam":
                        // Schema requirements: zero to many instances of this element
                        reader.Skip();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }
            reader.Dispose();

            if (m_decoyDbAccessionRegex.Count > 0)
            {
                foreach (var pepEv in m_evidences.Values)
                {
                    if (!pepEv.HadDecoyAttribute)
                    {
                        var dbSeq = pepEv.DbSeq;
                        if (m_decoyDbAccessionRegex.ContainsKey(dbSeq.DatabaseId))
                        {
                            pepEv.IsDecoy = m_decoyDbAccessionRegex[dbSeq.DatabaseId].IsMatch(dbSeq.Accession);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handle child nodes of SearchDatabase element
        /// Called by ReadDataCollection (xml hierarchy)
        /// Currently we are only working with the SpectraData child elements
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the single AnalysisData element</param>
        private void ReadSearchDatabase(XmlReader reader)
        {
            reader.MoveToContent();
            var id = reader.GetAttribute("id");
            reader.ReadStartElement("SearchDatabase"); // Throws exception if we are not at the "AnalysisData" tag.
            while (reader.ReadState == ReadState.Interactive)
            {
                // Handle exiting out properly at EndElement tags
                if (reader.NodeType != XmlNodeType.Element)
                {
                    reader.Read();
                    continue;
                }

                switch (reader.Name)
                {
                    case "FileFormat":
                        // Schema requirements: zero to one instances of this element
                        reader.Skip();
                        break;
                    case "DatabaseName":
                        // Schema requirements: one instance of this element
                        reader.Skip();
                        break;
                    case "cvParam":
                        // Schema requirements: zero to many instances of this element
                        if (reader.GetAttribute("accession") == "MS:1001283")
                        {
                            var regexString = reader.GetAttribute("value");

                            // ReSharper disable once AssignNullToNotNullAttribute
                            var regex = new Regex(regexString, RegexOptions.Compiled | RegexOptions.IgnoreCase);

                            // ReSharper disable once AssignNullToNotNullAttribute
                            m_decoyDbAccessionRegex.Add(id, regex);
                        }
                        reader.Read(); // Consume the cvParam element (no child nodes)
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }
            reader.Dispose();
        }

        /// <summary>
        /// Handle child nodes of AnalysisData element
        /// Called by ReadDataCollection (xml hierarchy)
        /// Currently we are only working with the SpectrumIdentificationList child elements
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the single AnalysisData element</param>
        private IEnumerable<SpectrumIdItem> ReadAnalysisData(XmlReader reader)
        {
            reader.MoveToContent();
            reader.ReadStartElement("AnalysisData"); // Throws exception if we are not at the "AnalysisData" tag.
            while (reader.ReadState == ReadState.Interactive)
            {
                // Handle exiting out properly at EndElement tags
                if (reader.NodeType != XmlNodeType.Element)
                {
                    reader.Read();
                    continue;
                }
                if (reader.Name == "SpectrumIdentificationList")
                {
                    // Schema requirements: one to many instances of this element
                    // Use reader.ReadSubtree() to provide an XmlReader that is only valid for the element and child nodes
                    // we can use "return ReadDataCollection(...)", but then that would immediately exit the function, without any subsequent operations occurring (like closing the reader)
                    foreach (var item in ReadSpectrumIdentificationList(reader.ReadSubtree()))
                    {
                        yield return item;
                    }
                    PossiblyReadEndElement(reader, "SpectrumIdentificationList");
                }
                else
                {
                    reader.Skip();
                }
            }
            reader.Dispose();
        }

        /// <summary>
        /// Handle the child nodes of a SpectrumIdentificationList element
        /// Called by ReadAnalysisData (xml hierarchy)
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the single SpectrumIdentificationList element</param>
        private IEnumerable<SpectrumIdItem> ReadSpectrumIdentificationList(XmlReader reader)
        {
            reader.MoveToContent();
            reader.ReadStartElement("SpectrumIdentificationList"); // Throws exception if we are not at the "SpectrumIdentificationList" tag.
            while (reader.ReadState == ReadState.Interactive)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                // Handle exiting out properly at EndElement tags
                if (reader.NodeType != XmlNodeType.Element)
                {
                    reader.Read();
                    continue;
                }
                if (reader.Name == "SpectrumIdentificationResult")
                {
                    // Schema requirements: one to many instances of this element
                    // Use reader.ReadSubtree() to provide an XmlReader that is only valid for the element and child nodes
                    // we can use "return ReadDataCollection(...)", but then that would immediately exit the function, without any subsequent operations occurring (like closing the reader, or reading the remaining spectrum identification results)
                    foreach (var item in ReadSpectrumIdentificationResult(reader.ReadSubtree()))
                    {
                        yield return item;
                    }
                    PossiblyReadEndElement(reader, "SpectrumIdentificationResult");
                }
                else
                {
                    reader.Skip();
                }
            }
            reader.Dispose();
        }

        /// <summary>
        /// Handle a single SpectrumIdentificationResult element and child nodes
        /// Called by ReadSpectrumIdentificationList (xml hierarchy)
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the single SpectrumIdentificationResult element</param>
        private IEnumerable<SpectrumIdItem> ReadSpectrumIdentificationResult(XmlReader reader)
        {
            var specRes = new List<SpectrumIdItem>();

            reader.MoveToContent();
            var nativeId = reader.GetAttribute("spectrumID");
            var scanNum = -1;
            var scanTimeMinutes = 0d;

            reader.ReadStartElement("SpectrumIdentificationResult"); // Throws exception if we are not at the "SpectrumIdentificationResult" tag.
            while (reader.ReadState == ReadState.Interactive)
            {
                // Handle exiting out properly at EndElement tags
                if (reader.NodeType != XmlNodeType.Element)
                {
                    reader.Read();
                    continue;
                }

                switch (reader.Name)
                {
                    case "SpectrumIdentificationItem":
                        // Schema requirements: one to many instances of this element
                        specRes.Add(ReadSpectrumIdentificationItem(reader.ReadSubtree()));
                        PossiblyReadEndElement(reader, "SpectrumIdentificationItem");
                        break;
                    case "cvParam":
                        // Schema requirements: zero to many instances of this element
                        if (reader.GetAttribute("accession") == "MS:1001115")
                        {
                            scanNum = Convert.ToInt32(reader.GetAttribute("value"));
                        }
                        else if (reader.GetAttribute("accession") == "MS:1000016")
                        {
                            // Parse out the scan time
                            double.TryParse(reader.GetAttribute("value"), out var scanTime);

                            // Determine the units
                            var unitAccession = reader.GetAttribute("unitAccession");

                            if (unitAccession == "UO:0000031")
                            {
                                // Minutes
                                scanTimeMinutes = scanTime;
                            }
                            else if (unitAccession == "UO:0000010")
                            {
                                // Seconds
                                scanTimeMinutes = scanTime / 60;
                            }
                            else
                            {
                                // Unrecognized units
                                scanTimeMinutes = scanTime;
                            }
                        }

                        reader.Read(); // Consume the cvParam element (no child nodes)
                        break;
                    case "userParam":
                        // Schema requirements: zero to many instances of this element
                        reader.Skip();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }
            foreach (var item in specRes)
            {
                item.ScanNum = scanNum;
                item.NativeId = nativeId;
                item.ScanTimeMinutes = scanTimeMinutes;
                yield return item;
            }
            reader.Dispose();
        }

        /// <summary>
        /// Handle a single SpectrumIdentificationItem element and child nodes
        /// Called by ReadSpectrumIdentificationResult (xml hierarchy)
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the single SpectrumIdentificationItem element</param>
        private SpectrumIdItem ReadSpectrumIdentificationItem(XmlReader reader)
        {
            reader.MoveToContent(); // Move to the "SpectrumIdentificationItem" element
            var specItem = new SpectrumIdItem
            {
                IsSpectrumIdNotTheScanNumber = isSpectrumIdNotAScanNum,
                PepEvCount = 0,
                SpecItemId = reader.GetAttribute("id"),
                PassThreshold = Convert.ToBoolean(reader.GetAttribute("passThreshold")),
                Rank = Convert.ToInt32(reader.GetAttribute("rank")),
                // ReSharper disable once AssignNullToNotNullAttribute
                Peptide = m_peptides[reader.GetAttribute("peptide_ref")],
                CalMz = Convert.ToDouble(reader.GetAttribute("calculatedMassToCharge")),
                ExperimentalMz =
                    Convert.ToDouble(reader.GetAttribute("experimentalMassToCharge")),
                Charge = Convert.ToInt32(reader.GetAttribute("chargeState"))
            };

            // Read all child PeptideEvidenceRef tags
            reader.ReadToDescendant("PeptideEvidenceRef");
            while (reader.Name == "PeptideEvidenceRef")
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                specItem.PepEvidence.Add(m_evidences[reader.GetAttribute("peptideEvidence_ref")]);
                reader.Read();
                if (reader.NodeType == XmlNodeType.EndElement)
                {
                    reader.ReadEndElement();
                }
            }

            // Parse all of the cvParam/userParam fields
            while (reader.Name == "cvParam" || reader.Name == "userParam")
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                specItem.AllParamsDict.Add(reader.GetAttribute("name"), reader.GetAttribute("value"));
                switch (reader.GetAttribute("name"))
                {
                    case "MSPathFinder:RawScore":
                    case "MS-GF:RawScore":
                        specItem.RawScore = Convert.ToDouble(reader.GetAttribute("value"));
                        break;
                    case "MS-GF:DeNovoScore":
                        specItem.DeNovoScore = Convert.ToInt32(reader.GetAttribute("value"));
                        break;
                    case "MSPathFinder:SpecEValue":
                    case "MS-GF:SpecEValue":
                        specItem.SpecEv = Convert.ToDouble(reader.GetAttribute("value"));
                        break;
                    case "MSPathFinder:EValue":
                    case "MS-GF:EValue":
                        specItem.EValue = Convert.ToDouble(reader.GetAttribute("value"));
                        break;
                    case "MSPathFinder:QValue":
                    case "MS-GF:QValue":
                        specItem.QValue = Convert.ToDouble(reader.GetAttribute("value"));
                        break;
                    case "MSPathFinder:PepQValue":
                    case "MS-GF:PepQValue":
                        specItem.PepQValue = Convert.ToDouble(reader.GetAttribute("value"));
                        break;
                    case "IsotopeError":
                        // userParam field
                        specItem.IsoError = Convert.ToInt32(reader.GetAttribute("value"));
                        break;
                    case "AssumedDissociationMethod":
                        // userParam field
                        break;
                }
                reader.Read();
                if (reader.NodeType == XmlNodeType.EndElement)
                {
                    reader.ReadEndElement();
                }
            }
            specItem.PepEvCount = specItem.PepEvidence.Count;

            reader.Dispose();

            return specItem;
        }
    }
}
