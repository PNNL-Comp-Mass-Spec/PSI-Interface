using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using PSI_Interface.IdentData;

namespace Interface_Tests.IdentDataTests
{
    [TestFixture]
    public class SimpleMZIdentMLReaderTests
    {
        // Ignore Spelling: Ident

        [Test]
        [TestCase(@"MzIdentML\Cyano_GC_07_11_25Aug09_Draco_09-05-02_msgfplus.mzid           ", 11443, 12172, 8898, 4361)]
        [TestCase(@"MzIdentML\Cyano_GC_07_11_25Aug09_Draco_09-05-02_msgfplus.mzid.gz        ", 11443, 12172, 8898, 4361)]
        [TestCase(@"MzIdentML\Mixed_subcell-50a_31Aug10_Falcon_10-07-40_msgfplus.mzid       ", 15510 ,16486, 13519, 4912)]
        [TestCase(@"MzIdentML\Mixed_subcell-50a_31Aug10_Falcon_10-07-40_msgfplus.mzid.gz    ", 15510, 16486, 13519, 4912)]
        [TestCase(@"MzIdentML\QC_Shew_17_02_Frac-06_11Oct18_Tiger_18-08-04_msgfplus.mzid    ", 296, 327, 272, 264)]
        [TestCase(@"MzIdentML\Cj_media_DOC_R2_23Feb15_Arwen_14-12-03_NoResults_msgfplus.mzid", 0, 0, 0, 0)]
        public void ReadFile(string inputFileRelativePath, int expectedNativeIDs, int expectedResults, int expectedPeptides, int expectedProteinSeqs)
        {
            if (!TestPath.FindInputFile(inputFileRelativePath, out var sourceFile))
            {
                Console.WriteLine("File not found: " + inputFileRelativePath);
                return;
            }

            var reader = new SimpleMZIdentMLReader();
            var spectrumIDs = new SortedSet<string>();
            var peptides = new SortedSet<string>();
            var proteinSeqs = new SortedSet<string>();

            var results = reader.Read(sourceFile.FullName);
            var specResults = 0;
            var resultCountTotal = results.Identifications.Count;

            foreach (var specItem in results.Identifications)
            {
                specResults++;

                if (!spectrumIDs.Contains(specItem.NativeId))
                    spectrumIDs.Add(specItem.NativeId);

                foreach (var evidenceItem in specItem.PepEvidence)
                {
                    if (!peptides.Contains(evidenceItem.SequenceWithNumericMods))
                        peptides.Add(evidenceItem.SequenceWithNumericMods);

                    if (!proteinSeqs.Contains(evidenceItem.DbSeq.Accession))
                        proteinSeqs.Add(evidenceItem.DbSeq.Accession);
                }

                if (specResults % 1000 == 0)
                    Console.WriteLine("{0,6:N0} / {1,6:N0}", specResults, resultCountTotal);
            }

            Console.WriteLine();
            Console.WriteLine("Native IDs: {0,6:N0}", spectrumIDs.Count);
            Console.WriteLine("Spectrum Identification Results: {0,6:N0}", specResults);
            Console.WriteLine("Unique Peptides: {0,6:N0}", peptides.Count);
            Console.WriteLine("Unique Protein Sequences: {0,6:N0}", proteinSeqs.Count);

            Assert.AreEqual(expectedResults, specResults, "Spectrum Identification Results");
            Assert.AreEqual(expectedNativeIDs, spectrumIDs.Count, "Native IDs");
            Assert.AreEqual(expectedPeptides, peptides.Count, "Unique Peptides");
            Assert.AreEqual(expectedProteinSeqs, proteinSeqs.Count, "Unique Protein Sequences");
        }

        [Test]
        [TestCase(@"MzIdentML\Cyano_GC_07_11_25Aug09_Draco_09-05-02_msgfplus.mzid           ", 11443, 12172, 8898, 4361)]
        [TestCase(@"MzIdentML\Cyano_GC_07_11_25Aug09_Draco_09-05-02_msgfplus.mzid.gz        ", 11443, 12172, 8898, 4361)]
        [TestCase(@"MzIdentML\Mixed_subcell-50a_31Aug10_Falcon_10-07-40_msgfplus.mzid       ", 15510, 16486, 13519, 4912)]
        [TestCase(@"MzIdentML\Mixed_subcell-50a_31Aug10_Falcon_10-07-40_msgfplus.mzid.gz    ", 15510, 16486, 13519, 4912)]
        [TestCase(@"MzIdentML\Cj_media_DOC_R2_23Feb15_Arwen_14-12-03_NoResults_msgfplus.mzid", 0, 0, 0, 0)]
        public void ReadFileLowMem(string inputFileRelativePath, int expectedNativeIDs, int expectedResults, int expectedPeptides, int expectedProteinSeqs)
        {
            if (!TestPath.FindInputFile(inputFileRelativePath, out var sourceFile))
            {
                Console.WriteLine("File not found: " + inputFileRelativePath);
                return;
            }

            var reader = new SimpleMZIdentMLReader();
            var spectrumIDs = new SortedSet<string>();
            var peptides = new SortedSet<string>();
            var proteinSeqs = new SortedSet<string>();
            var specResults = 0;

            using (var results = reader.ReadLowMem(sourceFile.FullName))
            {
                var resultIdentifications = results.Identifications.ToList();
                var resultCountTotal = resultIdentifications.Count;

                foreach (var specItem in resultIdentifications)
                {
                    specResults++;

                    if (!spectrumIDs.Contains(specItem.NativeId))
                        spectrumIDs.Add(specItem.NativeId);

                    foreach (var evidenceItem in specItem.PepEvidence)
                    {
                        if (!peptides.Contains(evidenceItem.SequenceWithNumericMods))
                            peptides.Add(evidenceItem.SequenceWithNumericMods);

                        if (!proteinSeqs.Contains(evidenceItem.DbSeq.Accession))
                            proteinSeqs.Add(evidenceItem.DbSeq.Accession);
                    }

                    if (specResults % 1000 == 0)
                        Console.WriteLine("{0,6:N0} / {1,6:N0}", specResults, resultCountTotal);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Spectrum Identification Results: {0,6:N0}", specResults);
            Console.WriteLine("Native IDs: {0,6:N0}", spectrumIDs.Count);
            Console.WriteLine("Unique Peptides: {0,6:N0}", peptides.Count);
            Console.WriteLine("Unique Protein Sequences: {0,6:N0}", proteinSeqs.Count);

            Assert.AreEqual(expectedResults, specResults, "Spectrum Identification Results");
            Assert.AreEqual(expectedNativeIDs, spectrumIDs.Count, "Native IDs");
            Assert.AreEqual(expectedPeptides, peptides.Count, "Unique Peptides");
            Assert.AreEqual(expectedProteinSeqs, proteinSeqs.Count, "Unique Protein Sequences");
        }
    }
}
