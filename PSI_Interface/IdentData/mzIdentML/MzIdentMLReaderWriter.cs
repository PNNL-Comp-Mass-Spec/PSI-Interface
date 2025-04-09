using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PSI_Interface.IdentData.mzIdentML
{
    /// <summary>
    /// Class for reading an MzIdentML file into MzIdentMLType group objects
    /// </summary>
    public static class MzIdentMlReaderWriter
    {
        // Ignore Spelling: ident

        /// <summary>
        /// Read the mzid file into MzIdentMLType objects
        /// </summary>
        public static MzIdentMLType Read(string filePath, int bufferSize = 65536)
        {
            var detectedVersion = DetectFileSchemaVersion(filePath);

            XmlSerializer serializer;

            if (detectedVersion.StartsWith("1.2"))
            {
                var overrides = GetMzIdentMl12Overrides(out _, out _);
                serializer = new XmlSerializer(typeof(MzIdentMLType), overrides);
            }
            else
            {
                serializer = new XmlSerializer(typeof(MzIdentMLType), "http://psidev.info/psi/pi/mzIdentML/1.1");
            }

            using (var reader = CreateReader(filePath, bufferSize))
            {
                return (MzIdentMLType)serializer.Deserialize(reader);
            }
        }

        private static Stream CreateReader(string filePath, int bufferSize)
        {
            var sourceFile = new FileInfo(filePath);

            if (!sourceFile.Exists)
                throw new FileNotFoundException(".mzID file not found", filePath);

            Stream reader = new FileStream(sourceFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, bufferSize);

            if (sourceFile.Name.Trim().EndsWith(".gz", StringComparison.OrdinalIgnoreCase))
            {
                reader = new GZipStream(reader, CompressionMode.Decompress);
            }

            return reader;
        }

        /// <summary>
        /// Write the provided data to the file
        /// </summary>
        /// <param name="identData"></param>
        /// <param name="filePath">Path to file to be written, with extension of .mzid[.gz]</param>
        /// <param name="bufferSize">File stream buffer size</param>
        public static void Write(MzIdentMLType identData, string filePath, int bufferSize = 65536)
        {
            XmlSerializer serializer;

            if (identData.version.StartsWith("1.2"))
            {
                var overrides = GetMzIdentMl12Overrides(out var ns, out var xsdUrl);
                identData.xsiSchemaLocation = $"{ns} {xsdUrl}";
                serializer = new XmlSerializer(typeof(MzIdentMLType), overrides);
            }
            else
            {
                serializer = new XmlSerializer(typeof(MzIdentMLType), "http://psidev.info/psi/pi/mzIdentML/1.1");
            }

            using (var writer = CreateWriter(filePath, bufferSize))
            {
                serializer.Serialize(writer, identData);
            }
        }

        private static XmlWriter CreateWriter(string filePath, int bufferSize)
        {
            Stream writer = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite, bufferSize);

            if (filePath.Trim().EndsWith(".gz", StringComparison.OrdinalIgnoreCase))
            {
                writer = new GZipStream(writer, CompressionMode.Compress);
            }

            var utf8EncodingNoMark = new UTF8Encoding(false); // DO NOT ADD THE BYTE ORDER MARK!!!

            var xSettings = new XmlWriterSettings
            {
                CloseOutput = true,
                NewLineChars = "\n",
                Indent = true,
                Encoding = utf8EncodingNoMark
            };

            return XmlWriter.Create(new StreamWriter(writer, utf8EncodingNoMark, bufferSize), xSettings);
        }

        private static string DetectFileSchemaVersion(string filePath)
        {
            using (var reader = new StreamReader(CreateReader(filePath, 8192)))
            {
                var count = 0;
                const string nsKey = "http://psidev.info/psi/pi/mzIdentML/";

                while (!reader.EndOfStream && count < 20)
                {
                    var line = reader.ReadLine();
                    count++;

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var index = line.IndexOf(nsKey, StringComparison.OrdinalIgnoreCase);

                    if (index < 0)
                        continue;

                    var keyEnd = index + nsKey.Length;
                    var maxCount = Math.Min(10, line.Length - keyEnd);
                    var focus = line.Substring(index + nsKey.Length, maxCount);
                    var split = focus.Split(' ', '"');

                    return split[0];
                }
            }

            return "1.1";
        }

        private static XmlAttributeOverrides GetMzIdentMl12Overrides(out string mzIdentMl12Url, out string xsdUrl)
        {
            mzIdentMl12Url = "http://psidev.info/psi/pi/mzIdentML/1.2";
            xsdUrl = "https://raw.githubusercontent.com/HUPO-PSI/mzIdentML/master/schema/mzIdentML1.2.0.xsd";

            return GenerateMzIdentMlOverrides(mzIdentMl12Url);
        }

        private static XmlAttributeOverrides GenerateMzIdentMlOverrides(string namespaceUrl)
        {
            // root override: affects only MzIdentMLType
            //[System.Xml.Serialization.XmlRootAttribute("MzIdentML", Namespace = "http://psidev.info/psi/pi/mzIdentML/1.1", IsNullable = false)]
            // all:
            //[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://psidev.info/psi/pi/mzIdentML/1.1")]
            var xmlOverrides = new XmlAttributeOverrides();
            var xmlTypeNsOverride = new XmlAttributes();
            var xmlTypeNs = new XmlTypeAttribute { Namespace = namespaceUrl };
            xmlTypeNsOverride.XmlType = xmlTypeNs;

            var xmlRootOverrides = new XmlAttributes {
                XmlType = xmlTypeNs
            };

            var xmlRootOverride = new XmlRootAttribute
            {
                ElementName = "MzIdentML",
                Namespace = namespaceUrl,
                IsNullable = false
            };

            xmlRootOverrides.XmlRoot = xmlRootOverride;

            xmlOverrides.Add(typeof(MzIdentMLType), xmlRootOverrides);
            xmlOverrides.Add(typeof(cvType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(SpectrumIdentificationItemRefType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(PeptideHypothesisType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(FragmentArrayType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(IonTypeType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(CVParamType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(UserParamType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(ParamType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(ParamListType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(PeptideEvidenceRefType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(AnalysisDataType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(SpectrumIdentificationListType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(MeasureType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(IdentifiableType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(BibliographicReferenceType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(ProteinDetectionHypothesisType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(ProteinAmbiguityGroupType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(ProteinDetectionListType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(SpectrumIdentificationItemType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(SpectrumIdentificationResultType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(ExternalDataType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(FileFormatType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(SpectraDataType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(SpectrumIDFormatType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(SourceFileType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(SearchDatabaseType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(ProteinDetectionProtocolType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(TranslationTableType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(MassTableType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(ResidueType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(AmbiguousResidueType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(EnzymeType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(SpectrumIdentificationProtocolType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(SpecificityRulesType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(SearchModificationType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(EnzymesType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(FilterType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(DatabaseTranslationType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(ProtocolApplicationType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(ProteinDetectionType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(InputSpectrumIdentificationsType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(SpectrumIdentificationType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(InputSpectraType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(SearchDatabaseRefType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(PeptideEvidenceType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(PeptideType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(ModificationType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(SubstitutionModificationType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(DBSequenceType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(SampleType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(ContactRoleType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(RoleType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(SubSampleType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(AbstractContactType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(OrganizationType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(ParentOrganizationType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(PersonType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(AffiliationType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(ProviderType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(AnalysisSoftwareType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(InputsType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(DataCollectionType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(AnalysisProtocolCollectionType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(AnalysisCollectionType), xmlTypeNsOverride);
            xmlOverrides.Add(typeof(SequenceCollectionType), xmlTypeNsOverride);

            return xmlOverrides;
        }
    }
}
