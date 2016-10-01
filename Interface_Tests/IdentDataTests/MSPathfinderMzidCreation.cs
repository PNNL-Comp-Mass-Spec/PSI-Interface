using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using NUnit.Framework;
using PSI_Interface.CV;
using PSI_Interface.IdentData;
using PSI_Interface.IdentData.IdentDataObjs;
using PSI_Interface.IdentData.mzIdentML;

namespace Interface_Tests.IdentDataTests
{
    [TestFixture]
    public class MsPathfinderMzidCreation
    {
        [Test]
        public void CreateMzidFile()
        {
            var dir = @"F:\MSPathfinder_Tests";
            var datasetName = "QC_ShewIntact_16_12AUG16_Bane_16-03-19";
            var input = Path.Combine(dir, "QC_ShewIntact_16_12AUG16_Bane_16-03-19_IcTda.tsv");
            var dbName = "ID_005435_435B0CDA.fasta";
            var database = Path.Combine(dir, "ID_005435_435B0CDA.fasta");
            var output = Path.Combine(dir, "QC_ShewIntact_16_12AUG16_Bane_16-03-19.mzid");

            var creator = new IdentDataCreator("MSPathFinder_" + datasetName, "MSPathFinder_" + datasetName);
            var soft = creator.AddAnalysisSoftware("Software_1", "MSPathFinder", "1.3", CV.CVID.CVID_Unknown, "MSPathFinder");
            var settings = creator.AddAnalysisSettings(soft, "Settings_1", CV.CVID.MS_ms_ms_search);
            var searchDb = creator.AddSearchDatabase(Path.Combine(dir, dbName), 1000000, dbName, CV.CVID.CVID_Unknown,
                CV.CVID.MS_FASTA_format);
            var specData = creator.AddSpectraData(Path.Combine(dir, datasetName + ".raw"), datasetName, CV.CVID.MS_Thermo_nativeID_format,
                CV.CVID.MS_Thermo_RAW_format);

            foreach (var result in ReadMsPathfinderResults(input))
            {
                var native = "controllerType=0 controllerNumber=1 scan=" + result.Scan;
                var spec = creator.AddSpectrumIdentification(specData, native, result.Scan, result.MostAbundantIsotopeMz,
                    result.Charge, 1, result.MostAbundantIsotopeMz);
                var pep = new PeptideObj(result.Sequence);
                foreach (var mod in result.Modifications)
                {
                    var modObj = new ModificationObj(CV.CVID.MS_unknown_modification, mod.Item1, mod.Item2);
                    pep.Modifications.Add(modObj);
                }
                spec.Peptide = pep;

                var dbSeq = new DbSequenceObj(searchDb, result.ProteinLength, result.ProteinName,
                    result.ProteinDesc);

                var pepEv = new PeptideEvidenceObj(dbSeq, pep, result.Start, result.End, result.Pre, result.Post, false);
                spec.AddPeptideEvidence(pepEv);

                spec.CVParams.Add(new CVParamObj(){Cvid = CV.CVID.MS_monoisotopic_mass_OBSOLETE, Value = result.Mass.ToString(CultureInfo.InvariantCulture), UnitCvid = CV.CVID.MS_m_z,});
                spec.CVParams.Add(new CVParamObj(){Cvid = CV.CVID.MS_chemical_compound_formula, Value = result.Composition,});
                spec.CVParams.Add(new CVParamObj(){Cvid = CV.CVID.MS_number_of_matched_peaks, Value = result.NumMatchedFragments.ToString(),});
                spec.CVParams.Add(new CVParamObj(){Cvid = CV.CVID.MS_SEQUEST_probability, Value = result.Probability.ToString(CultureInfo.InvariantCulture),});
                spec.CVParams.Add(new CVParamObj() { Cvid = CV.CVID.MS_MS_GF_SpecEValue, Value = result.SpecEValue.ToString(CultureInfo.InvariantCulture), });
                spec.CVParams.Add(new CVParamObj() { Cvid = CV.CVID.MS_MS_GF_SpecEValue, Value = result.EValue.ToString(CultureInfo.InvariantCulture), });
                spec.CVParams.Add(new CVParamObj() { Cvid = CV.CVID.MS_MS_GF_QValue, Value = result.QValue.ToString(CultureInfo.InvariantCulture), });
                spec.CVParams.Add(new CVParamObj() { Cvid = CV.CVID.MS_MS_GF_PepQValue, Value = result.PepQValue.ToString(CultureInfo.InvariantCulture), });
            }

            var identData = creator.GetIdentData();

            MzIdentMlReaderWriter.Write(new MzIdentMLType(identData), output);
        }

        private class MsPathfinderResult
        {
            public int Scan { get; set; }
            public string Pre { get; set; }
            public string Sequence { get; set; }
            public string Post { get; set; }
            public List<Tuple<string, int>> Modifications { get; set; }
            public string Composition { get; set; }
            public string ProteinName { get; set; }
            public string ProteinDesc { get; set; }
            public int ProteinLength { get; set; }
            public int Start { get; set; }
            public int End { get; set; }
            public int Charge { get; set; }
            public double MostAbundantIsotopeMz { get; set; }
            public double Mass { get; set; }
            public int NumMatchedFragments { get; set; }
            public double Probability { get; set; }
            public double SpecEValue { get; set; }
            public double EValue { get; set; }
            public double QValue { get; set; }
            public double PepQValue { get; set; }
        }

        private IEnumerable<MsPathfinderResult> ReadMsPathfinderResults(string path)
        {
            // "Scan	Pre	Sequence	Post	Modifications	Composition	ProteinName	ProteinDesc	ProteinLength	Start	End	Charge	MostAbundantIsotopeMz	Mass	#MatchedFragments	Probability	SpecEValue	EValue"
            // "Scan	Pre	Sequence	Post	Modifications	Composition	ProteinName	ProteinDesc	ProteinLength	Start	End	Charge	MostAbundantIsotopeMz	Mass	#MatchedFragments	Probability	SpecEValue	EValue	QValue	PepQValue"
            using (var reader = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                var line = reader.ReadLine();
                if (line != null && line.StartsWith("Scan\tPre\tSequence\tPost\tModifications"))
                {
                    line = reader.ReadLine();
                }
                while (line != null && !reader.EndOfStream)
                {
                    var tokens = line.Split('\t');
                    var result = new MsPathfinderResult();
                    result.Modifications = new List<Tuple<string, int>>();
                    if (tokens.Length > 0)
                    {
                        result.Scan = int.Parse(tokens[0]);
                    }
                    if (tokens.Length > 1)
                    {
                        result.Pre = tokens[1];
                    }
                    if (tokens.Length > 2)
                    {
                        result.Sequence = tokens[2];
                    }
                    if (tokens.Length > 3)
                    {
                        result.Post = tokens[3];
                    }
                    if (tokens.Length > 4 && !string.IsNullOrWhiteSpace(tokens[4]))
                    {
                        var modList = tokens[4];
                        foreach (var token in modList.Split(','))
                        {
                            var tokens3 = token.Split(' ');
                            result.Modifications.Add(new Tuple<string, int>(tokens3[0], int.Parse(tokens3[1])));
                        }
                    }
                    if (tokens.Length > 5)
                    {
                        result.Composition = tokens[5];
                    }
                    if (tokens.Length > 6)
                    {
                        result.ProteinName = tokens[6];
                    }
                    if (tokens.Length > 7)
                    {
                        result.ProteinDesc = tokens[7];
                    }
                    if (tokens.Length > 8)
                    {
                        result.ProteinLength = int.Parse(tokens[8]);
                    }
                    if (tokens.Length > 9)
                    {
                        result.Start = int.Parse(tokens[9]);
                    }
                    if (tokens.Length > 10)
                    {
                        result.End = int.Parse(tokens[10]);
                    }
                    if (tokens.Length > 11)
                    {
                        result.Charge = int.Parse(tokens[11]);
                    }
                    if (tokens.Length > 12)
                    {
                        result.MostAbundantIsotopeMz = double.Parse(tokens[12]);
                    }
                    if (tokens.Length > 13)
                    {
                        result.Mass = double.Parse(tokens[13]);
                    }
                    if (tokens.Length > 14)
                    {
                        result.NumMatchedFragments = int.Parse(tokens[14]);
                    }
                    if (tokens.Length > 15)
                    {
                        result.Probability = double.Parse(tokens[15]);
                    }
                    if (tokens.Length > 16)
                    {
                        result.SpecEValue = double.Parse(tokens[16]);
                    }
                    if (tokens.Length > 17)
                    {
                        result.EValue = double.Parse(tokens[17]);
                    }
                    if (tokens.Length > 18)
                    {
                        result.QValue = double.Parse(tokens[18]);
                    }
                    if (tokens.Length > 19)
                    {
                        result.PepQValue = double.Parse(tokens[19]);
                    }

                    yield return result;

                    line = reader.ReadLine();
                }
            }
        }
    }
}
