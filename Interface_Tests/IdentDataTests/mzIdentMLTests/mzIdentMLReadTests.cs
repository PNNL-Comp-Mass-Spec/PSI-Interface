using System;
using System.IO;
using NUnit.Framework;
using PSI_Interface.IdentData.mzIdentML;

namespace Interface_Tests.IdentDataTests.mzIdentMLTests
{
    internal class mzIdentMLReadTests
    {
        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [Test]
        [TestCase(@"MzIdentML\Cyano_GC_07_11_25Aug09_Draco_09-05-02_msgfplus.mzid           ", 1, 11443, 12172, 8889, 4361)]
        [TestCase(@"MzIdentML\Cyano_GC_07_11_25Aug09_Draco_09-05-02_msgfplus.mzid.gz        ", 1, 11443, 12172, 8889, 4361)]
        [TestCase(@"MzIdentML\Mixed_subcell-50a_31Aug10_Falcon_10-07-40_msgfplus.mzid       ", 1, 15510, 16486, 13486, 4912)]
        [TestCase(@"MzIdentML\Mixed_subcell-50a_31Aug10_Falcon_10-07-40_msgfplus.mzid.gz    ", 1, 15510, 16486, 13486, 4912)]
        [TestCase(@"MzIdentML\Cj_media_DOC_R2_23Feb15_Arwen_14-12-03_NoResults_msgfplus.mzid", 1, 0, 0, 0, 0)]
        public void MzIdentMLReadTest(string inputFileRelativePath, int expectedSpecLists, int expectedSpecResults, int expectedSpecItems, int expectedPeptides, int expectedSeqs)
        {
            if (!TestPath.FindInputFile(inputFileRelativePath, out var sourceFile))
            {
                Console.WriteLine("File not found: " + inputFileRelativePath);
                return;
            }

            var identData = MzIdentMlReaderWriter.Read(sourceFile.FullName);
            var specResults = 0;
            var specItems = 0;
            foreach (var specList in identData.DataCollection.AnalysisData.SpectrumIdentificationList)
            {
                specResults += specList.SpectrumIdentificationResult.Count;
                foreach (var specResult in specList.SpectrumIdentificationResult)
                {
                    specItems += specResult.SpectrumIdentificationItem.Count;
                }
            }

            var observedPeptides = identData.SequenceCollection.Peptide.Count;
            var observeProteins = identData.SequenceCollection.DBSequence.Count;

            Console.WriteLine();
            Console.WriteLine("Spectrum Identification Lists: {0}", identData.DataCollection.AnalysisData.SpectrumIdentificationList.Count);
            Console.WriteLine("Spectrum Identification Results: {0,6:N0}", specResults);
            Console.WriteLine("Spectrum Identification Items: {0,6:N0}", specItems);
            Console.WriteLine("Unique Peptides: {0,6:N0}", observedPeptides);
            Console.WriteLine("Unique Protein Sequences: {0,6:N0}", observeProteins);

            Assert.AreEqual(expectedSpecLists, identData.DataCollection.AnalysisData.SpectrumIdentificationList.Count, "Spectrum Identification Lists");
            Assert.AreEqual(expectedSpecResults, specResults, "Spectrum Identification Results");
            Assert.AreEqual(expectedSpecItems, specItems, "Spectrum Identification Items");
            Assert.AreEqual(expectedPeptides, observedPeptides, "Unique Peptides");
            Assert.AreEqual(expectedSeqs, observeProteins, "Unique Protein Sequences");
        }
    }
}
