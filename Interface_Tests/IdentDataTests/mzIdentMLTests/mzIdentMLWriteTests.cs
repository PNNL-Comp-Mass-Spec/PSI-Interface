using System;
using System.IO;
using NUnit.Framework;
using PSI_Interface.IdentData.mzIdentML;

namespace Interface_Tests.IdentDataTests.mzIdentMLTests
{
    internal class mzIdentMLWriteTests
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

        /// <summary>
        /// Read/Write using MzIdentMlReaderWriter
        /// but use PSI_Interface.IdentData.IdentDataObj
        /// </summary>
        /// <remarks>
        /// cv id="PSI-MS" fullName="PSI-MS" version="3.30.0"
        ///    uri="http://psidev.cvs.sourceforge.net/viewvc/*checkout*/psidev/psi/psi-ms/mzML/controlledVocabulary/psi-ms.obo"
        /// cv id="UNIMOD" fullName="UNIMOD"
        ///    uri="http://www.unimod.org/obo/unimod.obo"
        /// cv id="UO" fullName="UNIT-ONTOLOGY"
        ///    uri="http://obo.cvs.sourceforge.net/*checkout*/obo/obo/ontology/phenotype/unit.obo"
        /// </remarks>
        /// <param name="inPath"></param>
        /// <param name="outFolderName"></param>
        /// <param name="expectedSpecLists"></param>
        /// <param name="expectedSpecResults"></param>
        /// <param name="expectedSpecItems"></param>
        /// <param name="expectedPeptides"></param>
        /// <param name="expectedSeqs"></param>
        [Test]
        // [TestCase(@"MzIdentML\Cyano_GC_07_11_25Aug09_Draco_09-05-02.mzid","output", 1, 10894, 11612, 8806, 4507)]
        // [TestCase(@"MzIdentML\Cyano_GC_07_11_25Aug09_Draco_09-05-02_pwiz.mzid", "output", 1, 10894, 11612, 8806, 4507)]
        // [TestCase(@"MzIdentML\Mixed_subcell-50a_31Aug10_Falcon_10-07-40_msgfplus.mzid.gz", "output", 1, 18427, 20218, 17224, 5665)]
        [TestCase(@"MzIdentML\Cyano_GC_07_11_25Aug09_Draco_09-05-02_msgfplus.mzid           ", "output_MzIdentMLType", 1, 11443, 12172, 8889, 4361)]
        [TestCase(@"MzIdentML\Cyano_GC_07_11_25Aug09_Draco_09-05-02_msgfplus.mzid.gz        ", "output_MzIdentMLType", 1, 11443, 12172, 8889, 4361)]
        [TestCase(@"MzIdentML\Mixed_subcell-50a_31Aug10_Falcon_10-07-40_msgfplus.mzid       ", "output_MzIdentMLType", 1, 15510, 16486, 13486, 4912)]
        [TestCase(@"MzIdentML\Mixed_subcell-50a_31Aug10_Falcon_10-07-40_msgfplus.mzid.gz    ", "output_MzIdentMLType", 1, 15510, 16486, 13486, 4912)]
        public void MzIdentMLWriteTest(string inPath, string outFolderName, int expectedSpecLists, int expectedSpecResults, int expectedSpecItems, int expectedPeptides, int expectedSeqs)
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
            var identData = MzIdentMlReaderWriter.Read(Path.Combine(TestPath.ExtTestDataDirectory, inPath));
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

            Console.WriteLine();
            Console.WriteLine("Spectrum Identification Lists: {0}", identData.DataCollection.AnalysisData.SpectrumIdentificationList.Count);
            Console.WriteLine("Spectrum Identification Results: {0,6:N0}", specResults);
            Console.WriteLine("Spectrum Identification Items: {0,6:N0}", specItems);
            Console.WriteLine("Unique Peptides: {0,6:N0}", identData.SequenceCollection.Peptide.Count);
            Console.WriteLine("Unique Protein Sequences: {0,6:N0}", identData.SequenceCollection.DBSequence.Count);

            Assert.AreEqual(expectedSpecLists, identData.DataCollection.AnalysisData.SpectrumIdentificationList.Count, "Spectrum Identification Lists");
            Assert.AreEqual(expectedSpecResults, specResults, "Spectrum Identification Results");
            Assert.AreEqual(expectedSpecItems, specItems, "Spectrum Identification Items");
            Assert.AreEqual(expectedPeptides, identData.SequenceCollection.Peptide.Count, "Unique Peptides");
            Assert.AreEqual(expectedSeqs, identData.SequenceCollection.DBSequence.Count, "Unique Protein Sequences");

            MzIdentMlReaderWriter.Write(identData, outFile.FullName);
        }

        /*
        [Test]
        [TestCase(@"mzML\VA139IMSMS.mzML", 3145)]
        [TestCase(@"mzML\VA139IMSMS_compressed.mzML", 3145)]
        [TestCase(@"mzML\VA139IMSMS.mzML.gz", 3145)]
        [TestCase(@"mzML\sample1-A_BB2_01_922.mzML", 43574)]
        public void MzMLIndexedTest(string path, int expectedSpectra)
        {
            var reader = new MzMLReader(Path.Combine(TestPath.ExtTestDataDirectory, path));
            mzMLType mzMLData = reader.Read();
            Assert.AreEqual(expectedSpectra.ToString(), mzMLData.run.spectrumList.count.ToString(), "Stored Count");
            Assert.AreEqual(expectedSpectra, mzMLData.run.spectrumList.spectrum.Length, "Array length");
        }*/
    }
}
