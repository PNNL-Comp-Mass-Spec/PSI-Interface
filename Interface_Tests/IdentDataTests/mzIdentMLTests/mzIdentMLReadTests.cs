using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PSI_Interface.IdentData.mzIdentML;

namespace Interface_Tests.IdentDataTests.mzIdentMLTests
{
    class mzIdentMLReadTests
    {
        public mzIdentMLReadTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        [TestCase(@"MzIdentML\Cyano_GC_07_11_25Aug09_Draco_09-05-02.mzid", 1, 10894, 11612, 8806, 4507)]
        [TestCase(@"MzIdentML\Cyano_GC_07_11_25Aug09_Draco_09-05-02_pwiz.mzid", 1, 10894, 11612, 8806, 4507)]
        [TestCase(@"MzIdentML\Mixed_subcell-50a_31Aug10_Falcon_10-07-40_msgfplus.mzid.gz", 1, 18427, 20218, 17224, 5665)]
        public void MzIdentMLReadTest(string path, int expectedSpecLists, int expectedSpecResults, int expectedSpecItems, int expectedPeptides, int expectedSeqs)
        {
            MzIdentMLType identData = MzIdentMlReaderWriter.Read(Path.Combine(TestPath.ExtTestDataDirectory, path));
            int specResults = 0;
            int specItems = 0;
            foreach (var specList in identData.DataCollection.AnalysisData.SpectrumIdentificationList)
            {
                specResults += specList.SpectrumIdentificationResult.Count;
                foreach (var specResult in specList.SpectrumIdentificationResult)
                {
                    specItems += specResult.SpectrumIdentificationItem.Count;
                }
            }
            Assert.AreEqual(expectedSpecLists, identData.DataCollection.AnalysisData.SpectrumIdentificationList.Count, "Spectrum Identification Lists");
            //Assert.AreEqual(expectedSpecResults, identData.DataCollection.AnalysisData.SpectrumIdentificationList[0].SpectrumIdentificationResult.Length, "Spectrum Identification Results");
            Assert.AreEqual(expectedSpecResults, specResults, "Spectrum Identification Results");
            Assert.AreEqual(expectedSpecItems, specItems, "Spectrum Identification Items");
            Assert.AreEqual(expectedPeptides, identData.SequenceCollection.Peptide.Count, "Peptide Matches");
            Assert.AreEqual(expectedSeqs, identData.SequenceCollection.DBSequence.Count, "DB Sequences");
        }
    }
}
