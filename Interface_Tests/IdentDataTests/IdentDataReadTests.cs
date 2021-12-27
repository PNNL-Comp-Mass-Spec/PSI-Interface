using System;
using System.IO;
using NUnit.Framework;
using PSI_Interface.IdentData;
using PSI_Interface.IdentData.mzIdentML;

namespace Interface_Tests.IdentDataTests
{
    internal class IdentDataReadTests
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
        // [TestCase(@"MzIdentML\Cyano_GC_07_11_25Aug09_Draco_09-05-02.mzid", 10894, 11612, 8806, 4507)]
        // [TestCase(@"MzIdentML\Cyano_GC_07_11_25Aug09_Draco_09-05-02_pwiz.mzid", 10894, 11612, 8806, 4507)]
        // [TestCase(@"MzIdentML\Mixed_subcell-50a_31Aug10_Falcon_10-07-40_msgfplus.mzid.gz", 18427, 20218, 17224, 5665)]
        [TestCase(@"MzIdentML\Cyano_GC_07_11_25Aug09_Draco_09-05-02_msgfplus.mzid           ", 11443, 12172, 8889, 4361)]
        [TestCase(@"MzIdentML\Cyano_GC_07_11_25Aug09_Draco_09-05-02_msgfplus.mzid.gz        ", 11443, 12172, 8889, 4361)]
        [TestCase(@"MzIdentML\Mixed_subcell-50a_31Aug10_Falcon_10-07-40_msgfplus.mzid       ", 15510, 16486, 13486, 4912)]
        [TestCase(@"MzIdentML\Mixed_subcell-50a_31Aug10_Falcon_10-07-40_msgfplus.mzid.gz    ", 15510, 16486, 13486, 4912)]
        [TestCase(@"MzIdentML\Cj_media_DOC_R2_23Feb15_Arwen_14-12-03_NoResults_msgfplus.mzid", 0, 0, 0, 0)]
        public void MzIdentMLReadTest(string inputFileRelativePath, int expectedSpecResults, int expectedSpecItems, int expectedPeptides, int expectedSeqs)
        {
            if (!TestPath.FindInputFile(inputFileRelativePath, out var sourceFile))
            {
                Console.WriteLine("File not found: " + inputFileRelativePath);
                return;
            }

            var identData = new IdentDataObj(MzIdentMlReaderWriter.Read(sourceFile.FullName));
            var specResults = 0;
            var specItems = 0;
            foreach (var specList in identData.DataCollection.AnalysisData.SpectrumIdentificationList)
            {
                if (specList.SpectrumIdentificationResults == null)
                    continue;

                specResults += specList.SpectrumIdentificationResults.Count;
                foreach (var specResult in specList.SpectrumIdentificationResults)
                {
                    specItems += specResult.SpectrumIdentificationItems.Count;
                }
            }

            var observedPeptides = 0;
            if (identData.SequenceCollection.Peptides != null)
                observedPeptides = identData.SequenceCollection.Peptides.Count;

            var observeProteins = 0;
            if (identData.SequenceCollection.DBSequences != null)
                observeProteins = identData.SequenceCollection.DBSequences.Count;

            Console.WriteLine();
            Console.WriteLine("Spectrum Identification Results: {0,6:N0}", specResults);
            Console.WriteLine("Native IDs: {0,6:N0}", specItems);
            Console.WriteLine("Unique Peptides: {0,6:N0}", observedPeptides);
            Console.WriteLine("Unique Protein Sequences: {0,6:N0}", observeProteins);

            Assert.AreEqual(expectedSpecResults, specResults, "Spectrum Identification Results");
            Assert.AreEqual(expectedSpecItems, specItems, "Native IDs");
            Assert.AreEqual(expectedPeptides, observedPeptides, "Unique Peptides");
            Assert.AreEqual(expectedSeqs, observeProteins, "Unique Protein Sequences");
        }
    }
}
