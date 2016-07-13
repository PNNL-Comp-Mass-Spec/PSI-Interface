using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Xml.Linq;

namespace PSI_Interface.IdentData
{
    /// <summary>
    /// Read and perform some processing on a MZIdentML file
    /// Processes the data into an LCMS DataSet
    /// </summary>
    public class SimpleMZIdentMLReader
    {
        /// <summary>
        /// Initialize a MzIdentMlReader object
        /// </summary>
        public SimpleMZIdentMLReader()
        {
        }
        
        /*/// <summary>
        /// Initialize a MzIdentMlReader object
        /// </summary>
        /// <param name="options">Options used for the MSGFPlus Target Filter</param>
        public SimpleMZIdentMLReader(Options options)
        {
            ReaderOptions = options;
        }*/

        #region NativeId Conversion
        /// <summary>
        /// Provides functionality to interpret a NativeID as a integer scan number
        /// Code is ported from MSData.cpp in ProteoWizard
        /// </summary>
        public static class NativeIdConversion
        {
            private static Dictionary<string, string> ParseNativeId(string nativeId)
            {
                var tokens = nativeId.Split(new char[] { '\t', ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
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
        private readonly Dictionary<string, SpectrumIdItem> m_specItems = new Dictionary<string, SpectrumIdItem>();

        public class SpectrumIdItem
        {
            #region Spectrum ID Public Properties

            public SpectrumIdItem()
            {
                PepEvidence = new List<PeptideEvidence>();
                AllParamsDict = new Dictionary<string, string>();
            }

            public string SpecItemId { get; set; }

            public bool PassThreshold { get; set; }

            public int Rank { get; set; }

            public PeptideRef Peptide { get; set; }

            public double CalMz { get; set; }

            public double ExperimentalMz { get; set; }

            public int Charge { get; set; }

            public List<PeptideEvidence> PepEvidence { get; private set; }

            public Dictionary<string, string> AllParamsDict { get; private set; }

            public int PepEvCount { get; set; }

            public int RawScore { get; set; }

            public int DeNovoScore { get; set; }

            public double SpecEv { get; set; }

            public double EValue { get; set; }

            public double QValue { get; set; }

            public double PepQValue { get; set; }

            public int IsoError { get; set; }

            public bool IsDtaSpectrum { get; set; }

            public int ScanNumCVParam
            {
                get { return _scanNum; }
            }

            private int _scanNum = -1;

            public int ScanNum
            {
                get
                {
                    int num;
                    // Do not parse the SpectrumID for DTA file search results - the index is the DTA file index, not the spectrum index
                    if (!IsDtaSpectrum && !string.IsNullOrWhiteSpace(NativeId) && NativeIdConversion.TryGetScanNumberInt(NativeId, out num))
                    {
                        return num;
                    }
                    return _scanNum;
                }
                set { _scanNum = value; }
            }

            public string NativeId { get; set; }
            #endregion

        }

        private bool _isFromDTA = false;

        public class DatabaseSequence
        {
            public string Accession { get; set; }

            public int Length { get; set; }

            public string ProteinDescription { get; set; }
        }

        public class Modification
        {
            public double Mass { get; set; }

            public string Tag { get; set; }
        }

        public class PeptideRef
        {
            private readonly Dictionary<int, Modification> m_mods;

            public PeptideRef()
            {
                m_mods = new Dictionary<int, Modification>();
            }

            public string Sequence { get; set; }

            public void ModsAdd(int location, Modification mod)
            {
                m_mods.Add(location, mod);
            }

            public Dictionary<int, Modification> Mods
            {
                get { return m_mods; }
            }
        }

        public class PeptideEvidence
        {
            public bool IsDecoy { get; set; }

            public string Post { get; set; }

            public string Pre { get; set; }

            public int End { get; set; }

            public int Start { get; set; }

            public PeptideRef PeptideRef { get; set; }

            public DatabaseSequence DbSeq { get; set; }
        }

        public class SimpleMZIdentMLData
        {
            public SimpleMZIdentMLData(string path)
            {
                DatasetFile = path;
            }

            public readonly List<SpectrumIdItem> Identifications = new List<SpectrumIdItem>();
            public string DatasetFile { get; private set; }
        }


        /// <summary>
        /// Entry point for MZIdentMLReader, overriden from PHRPReaderBase
        /// Read the MZIdentML file, map the data to MSGF+ data, compute the NETs, and return the LCMS DataSet
        /// </summary>
        /// <param name="path">Path to *.mzid/mzIdentML file</param>
        /// <returns>LCMSDataSet</returns>
        /// <remarks>
        /// XML Reader parses an MZIdentML file, storing data as follows:
        ///   PeptideRef holds Peptide data, such as sequence, number, and type of modifications
        ///   Database Information holds the length of the peptide and the protein description
        ///   Peptide Evidence holds the pre, post, start and end for the peptide for Tryptic End calculations.
        /// The element that holds the most information is the Spectrum ID Item, which has the calculated mz,
        /// experimental mz, charge state, MSGF raw score, Denovo score, MSGF SpecEValue, MSGF EValue,
        /// MSGF QValue, MSGR PepQValue, Scan number as well as which peptide it is and which evidences
        /// it has from the analysis run.
        ///
        /// After the XML Reader, it then goes through each Spectrum ID item and maps the appropriate values
        /// to the appropriate variables as a MSGF+ result. If the result passes the filter for MSGF+, it
        /// then adds the data for if there are modifications and adds the result to a running list of results.
        /// When all the results are tabulated, it passes them through to the AnalysisHelper class to calculate
        /// both the observed and the predicted NETs and then returns an LCMSDataSet of the results with the MZIdent tool
        /// </remarks>
        public SimpleMZIdentMLData Read(string path)
        {
            // Set a large buffer size. Doesn't affect gzip reading speed, but speeds up non-gzipped
            Stream file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 65536);

            if (path.EndsWith(".mzid.gz"))
            {
                file = new GZipStream(file, CompressionMode.Decompress);
            }

            var xSettings = new XmlReaderSettings { IgnoreWhitespace = true };
            var reader = XmlReader.Create(new StreamReader(file, System.Text.Encoding.UTF8, true, 65536), xSettings);

            // Read in the file
            ReadMzIdentMl(reader);

            var results = new SimpleMZIdentMLData(path);
            results.Identifications.AddRange(m_specItems.Values);
            return results;
        }

        /// <summary>
        /// Read and parse a .mzid file, or mzIdentML
        /// Files are commonly larger than 30 MB, so use a streaming reader instead of a DOM reader
        /// </summary>
        /// <param name="reader">XmlReader object for the file to be read</param>
        private void ReadMzIdentMl(XmlReader reader)
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
                            reader.Skip();
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
                            reader.ReadEndElement(); // "SequenceCollection", if it exists, must have child nodes
                            break;
                        case "AnalysisCollection":
                            // Schema requirements: one instance of this element
                            reader.Skip();
                            break;
                        case "AnalysisProtocolCollection":
                            // Schema requirements: one instance of this element
                            reader.Skip();
                            break;
                        case "DataCollection":
                            // Schema requirements: one instance of this element
                            // Use reader.ReadSubtree() to provide an XmlReader that is only valid for the element and child nodes
                            ReadDataCollection(reader.ReadSubtree());
                            reader.ReadEndElement(); // "DataCollection" must have child nodes
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
            reader.Close();
        }

        /// <summary>
        /// Handle DBSequence element
        /// Called by ReadSequenceCollection (xml hierarchy)
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the single DBSequence element</param>
        private void ReadDbSequence(XmlReader reader)
        {
            reader.MoveToContent();
            string id = reader.GetAttribute("id");
            if (id != null)
            {
                var dbSeq = new DatabaseSequence
                {
                    Length = Convert.ToInt32(reader.GetAttribute("length")),
                    Accession = reader.GetAttribute("accession")
                };
                if (reader.ReadToDescendant("cvParam"))
                {
                    dbSeq.ProteinDescription = reader.GetAttribute("value"); //.Split(' ')[0];
                }
                m_database.Add(id, dbSeq);
            }
            reader.Close();
        }

        /// <summary>
        /// Handle Peptide element
        /// Called by ReadSequenceCollection (xml hierarchy)
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the single Peptide element</param>
        private void ReadPeptide(XmlReader reader)
        {
            reader.MoveToContent();
            string id = reader.GetAttribute("id");
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
                    var mods = new KeyValuePair<int, Modification>(Convert.ToInt32(reader.GetAttribute("location")),
                                                                                                mod);
                    // Read down to get the name of the modification, then add the modification to the peptide reference
                    reader.ReadToDescendant("cvParam"); // The cvParam child node is required

                    mod.Tag = reader.GetAttribute("name");
                    pepRef.ModsAdd(mods.Key, mods.Value);

                    // There could theoretically exist more than one cvParam element. Clear them out.
                    while (reader.ReadToNextSibling("cvParam"))
                    {
                        // This is supposed to be empty. The loop condition does everything that needs to happen
                    }
                    reader.ReadEndElement(); // Consume EndElement for Modification
                }
                m_peptides.Add(id, pepRef);
            }
            reader.Close();
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
                Post = reader.GetAttribute("post"),
                Pre = reader.GetAttribute("pre"),
                End = Convert.ToInt32(reader.GetAttribute("end")),
                Start = Convert.ToInt32(reader.GetAttribute("start")),
                PeptideRef = m_peptides[reader.GetAttribute("peptide_ref")],
                DbSeq = m_database[reader.GetAttribute("dBSequence_ref")]
            };
            m_evidences.Add(reader.GetAttribute("id"), pepEvidence);
            reader.Close();
        }

        /// <summary>
        /// Handle the child nodes of the DataCollection element
        /// Called by ReadMzIdentML (xml hierarchy)
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the single DataCollection element</param>
        private void ReadDataCollection(XmlReader reader)
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
                        reader.ReadEndElement(); // "Inputs" must have child nodes
                        break;
                    case "AnalysisData":
                        // Schema requirements: one and only one instance of this element
                        // Use reader.ReadSubtree() to provide an XmlReader that is only valid for the element and child nodes
                        ReadAnalysisData(reader.ReadSubtree());
                        reader.ReadEndElement(); // "AnalysisData" must have child nodes
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }
            reader.Close();
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
                if (reader.Name == "SpectraData")
                {
                    // Schema requirements: one to many instances of this element
                    // location attribute: required
                    // id attribute: required
                    // SpectrumIDFormat child element: required
                    var location = reader.GetAttribute("location");
                    if (location != null && location.ToLower().EndsWith("_dta.txt"))
                    {
                        _isFromDTA = true;
                    }
                    reader.Skip(); // "SpectrumIdentificationList" must have child nodes
                }
                else
                {
                    reader.Skip();
                }
            }
            reader.Close();
        }

        /// <summary>
        /// Handle child nodes of AnalysisData element
        /// Called by ReadDataCollection (xml hierarchy)
        /// Currently we are only working with the SpectrumIdentificationList child elements
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the single AnalysisData element</param>
        private void ReadAnalysisData(XmlReader reader)
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
                    ReadSpectrumIdentificationList(reader.ReadSubtree());
                    reader.ReadEndElement(); // "SpectrumIdentificationList" must have child nodes
                }
                else
                {
                    reader.Skip();
                }
            }
            reader.Close();
        }

        /// <summary>
        /// Handle the child nodes of a SpectrumIdentificationList element
        /// Called by ReadAnalysisData (xml hierarchy)
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the single SpectrumIdentificationList element</param>
        private void ReadSpectrumIdentificationList(XmlReader reader)
        {
            reader.MoveToContent();
            reader.ReadStartElement("SpectrumIdentificationList"); // Throws exception if we are not at the "SpectrumIdentificationList" tag.
            while (reader.ReadState == ReadState.Interactive)
            {
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
                    ReadSpectrumIdentificationResult(reader.ReadSubtree());
                    reader.ReadEndElement(); // "SpectrumIdentificationResult" must have child nodes
                }
                else
                {
                    reader.Skip();
                }
            }
            reader.Close();
        }

        /// <summary>
        /// Handle a single SpectrumIdentificationResult element and child nodes
        /// Called by ReadSpectrumIdentificationList (xml hierarchy)
        /// </summary>
        /// <param name="reader">XmlReader that is only valid for the scope of the single SpectrumIdentificationResult element</param>
        private void ReadSpectrumIdentificationResult(XmlReader reader)
        {
            var specRes = new List<SpectrumIdItem>();

            reader.MoveToContent();
            var nativeId = reader.GetAttribute("spectrumID");
            int scanNum = -1;
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
                        reader.ReadEndElement(); // "SpectrumIdentificationItem" must have child nodes
                        break;
                    case "cvParam":
                        // Schema requirements: zero to many instances of this element
                        if (reader.GetAttribute("accession") == "MS:1001115")
                        {
                            scanNum = Convert.ToInt32(reader.GetAttribute("value"));
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
                m_specItems.Add(item.SpecItemId, item);
            }
            reader.Close();
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
                IsDtaSpectrum = _isFromDTA,
                PepEvCount = 0,
                SpecItemId = reader.GetAttribute("id"),
                PassThreshold = Convert.ToBoolean(reader.GetAttribute("passThreshold")),
                Rank = Convert.ToInt32(reader.GetAttribute("rank")),
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
                specItem.PepEvidence.Add(m_evidences[reader.GetAttribute("peptideEvidence_ref")]);
                reader.Read();
            }

            // Parse all of the cvParam/userParam fields
            while (reader.Name == "cvParam" || reader.Name == "userParam")
            {
                specItem.AllParamsDict.Add(reader.GetAttribute("name"), reader.GetAttribute("value"));
                switch (reader.GetAttribute("name"))
                {
                    case "MS-GF:RawScore":
                        specItem.RawScore = Convert.ToInt32(reader.GetAttribute("value"));
                        break;
                    case "MS-GF:DeNovoScore":
                        specItem.DeNovoScore = Convert.ToInt32(reader.GetAttribute("value"));
                        break;
                    case "MS-GF:SpecEValue":
                        specItem.SpecEv = Convert.ToDouble(reader.GetAttribute("value"));
                        break;
                    case "MS-GF:EValue":
                        specItem.EValue = Convert.ToDouble(reader.GetAttribute("value"));
                        break;
                    case "MS-GF:QValue":
                        specItem.QValue = Convert.ToDouble(reader.GetAttribute("value"));
                        break;
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
            }
            specItem.PepEvCount = specItem.PepEvidence.Count;

            reader.Close();

            return specItem;
        }
    }
}
