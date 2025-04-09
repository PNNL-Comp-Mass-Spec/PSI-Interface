using System;
using System.IO;
using NUnit.Framework;
using PSI_Interface.IdentData;
using PSI_Interface.IdentData.mzIdentML;

namespace Interface_Tests.IdentDataTests
{
    internal class IdentDataWriteTests
    {
        // Ignore Spelling: Ident

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

        /// <summary>
        /// Read/Write using MzIdentMlReaderWriter
        /// but use PSI_Interface.IdentData.IdentDataObj
        /// </summary>
        /// <remarks>
        /// cv id="MS" fullName="Proteomics Standards Initiative Mass Spectrometry Ontology" version="4.0.2"
        ///    uri="https://raw.githubusercontent.com/HUPO-PSI/psi-ms-CV/master/psi-ms.obo"
        /// cv id = "UNIMOD" fullName="UNIMOD" version="2016:09:23 13:49"
        ///    uri="http://www.unimod.org/obo/unimod.obo"
        /// cv id = "UO" fullName="Unit Ontology" version="releases/2016-05-13"
        ///    uri="http://www.berkeleybop.org/ontologies/uo/uo.obo"
        /// </remarks>
        /// <param name="inPath"></param>
        /// <param name="outFolderName"></param>
        /// <param name="expectedSpecLists"></param>
        /// <param name="expectedSpecResults"></param>
        /// <param name="expectedSpecItems"></param>
        /// <param name="expectedPeptides"></param>
        /// <param name="expectedProteinSequences"></param>
        [Test]
        // [TestCase(@"MzIdentML\Cyano_GC_07_11_25Aug09_Draco_09-05-02.mzid",                  "output", 1, 10894, 11612, 8806, 4507)]
        // [TestCase(@"MzIdentML\Cyano_GC_07_11_25Aug09_Draco_09-05-02_pwiz.mzid",             "output", 1, 10894, 11612, 8806, 4507)]
        // [TestCase(@"MzIdentML\Cyano_GC_07_11_25Aug09_Draco_09-05-02_pwiz_mod.mzid",         "output", 1, 10894, 11612, 8806, 4507)]
        // [TestCase(@"MzIdentML\Mixed_subcell-50a_31Aug10_Falcon_10-07-40_msgfplus.mzid.gz",  "output", 1, 18427, 20218, 17224, 5665)]
        [TestCase(@"MzIdentML\Cyano_GC_07_11_25Aug09_Draco_09-05-02_msgfplus.mzid           ", "output_IdentDataObj", 1, 11443, 12172, 8889, 4361)]
        [TestCase(@"MzIdentML\Cyano_GC_07_11_25Aug09_Draco_09-05-02_msgfplus.mzid.gz        ", "output_IdentDataObj", 1, 11443, 12172, 8889, 4361)]
        [TestCase(@"MzIdentML\Mixed_subcell-50a_31Aug10_Falcon_10-07-40_msgfplus.mzid       ", "output_IdentDataObj", 1, 15510, 16486, 13486, 4912)]
        [TestCase(@"MzIdentML\Mixed_subcell-50a_31Aug10_Falcon_10-07-40_msgfplus.mzid.gz    ", "output_IdentDataObj", 1, 15510, 16486, 13486, 4912)]
        [TestCase(@"MzIdentML\Cj_media_DOC_R2_23Feb15_Arwen_14-12-03_NoResults_msgfplus.mzid", "output_IdentDataObj", 1, 0, 0, 0, 0)]
        public void MzIdentMLWriteTest(string inPath, string outFolderName, int expectedSpecLists, int expectedSpecResults, int expectedSpecItems, int expectedPeptides, int expectedProteinSequences)
        {
            var sourceFile = new FileInfo(Path.Combine(TestPath.ExtTestDataDirectory, inPath));

            if (!sourceFile.Exists)
            {
                Console.WriteLine("File not found: " + sourceFile.FullName);
                return;
            }

            if (sourceFile.DirectoryName == null)
                throw new DirectoryNotFoundException("Cannot determine the parent folder of " + sourceFile.FullName);

            var outFolder = new DirectoryInfo(Path.Combine(sourceFile.DirectoryName, outFolderName));

            if (!outFolder.Exists)
                outFolder.Create();

            var outFile = new FileInfo(Path.Combine(outFolder.FullName, sourceFile.Name));

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
            Console.WriteLine("Spectrum Identification Lists: {0}", identData.DataCollection.AnalysisData.SpectrumIdentificationList.Count);
            Console.WriteLine("Spectrum Identification Results: {0,6:N0}", specResults);
            Console.WriteLine("Spectrum Identification Items: {0,6:N0}", specItems);
            Console.WriteLine("Unique Peptides: {0,6:N0}", observedPeptides);
            Console.WriteLine("Unique Protein Sequences: {0,6:N0}", observeProteins);

            Assert.AreEqual(expectedSpecLists, identData.DataCollection.AnalysisData.SpectrumIdentificationList.Count, "Spectrum Identification Lists");
            Assert.AreEqual(expectedSpecResults, specResults, "Spectrum Identification Results");
            Assert.AreEqual(expectedSpecItems, specItems, "Spectrum Identification Items");
            Assert.AreEqual(expectedPeptides, observedPeptides, "Unique Peptides");
            Assert.AreEqual(expectedProteinSequences, observeProteins, "Unique Protein Sequences");

            identData.DefaultCV();
            MzIdentMlReaderWriter.Write(new MzIdentMLType(identData), outFile.FullName);
        }
    }
}
