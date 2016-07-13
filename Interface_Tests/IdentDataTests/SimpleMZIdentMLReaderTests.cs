using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PSI_Interface.IdentData;

namespace Interface_Tests.IdentDataTests
{
    [TestFixture]
    public class SimpleMZIdentMLReaderTests
    {
        [Test]
        [TestCase(@"MzIdentML\Cyano_GC_07_11_25Aug09_Draco_09-05-02.mzid", 1, 10894, 11612, 8806, 4507)]
        [TestCase(@"MzIdentML\Cyano_GC_07_11_25Aug09_Draco_09-05-02_pwiz.mzid", 1, 10894, 11612, 8806, 4507)]
        [TestCase(@"MzIdentML\Mixed_subcell-50a_31Aug10_Falcon_10-07-40_msgfplus.mzid.gz", 1, 18427, 20218, 17224, 5665)]
        public void ReadFile(string path, int expectedSpecLists, int expectedSpecResults, int expectedSpecItems, int expectedPeptides, int expectedSeqs)
        {
            var reader = new SimpleMZIdentMLReader();
            var results = reader.Read(Path.Combine(TestPath.ExtTestDataDirectory, path));
            int specResults = 0;
            int specItems = 0;
            foreach (var specItem in results.Identifications)
            {
                System.Console.WriteLine(specItem.PepEvidence[0].DbSeq.Accession);
            }
            //Assert.AreEqual(expectedSpecLists, identData.DataCollection.AnalysisData.SpectrumIdentificationList.Count, "Spectrum Identification Lists");
            ////Assert.AreEqual(expectedSpecResults, identData.DataCollection.AnalysisData.SpectrumIdentificationList[0].SpectrumIdentificationResult.Length, "Spectrum Identification Results");
            //Assert.AreEqual(expectedSpecResults, specResults, "Spectrum Identification Results");
            //Assert.AreEqual(expectedSpecItems, specItems, "Spectrum Identification Items");
            //Assert.AreEqual(expectedPeptides, identData.SequenceCollection.Peptides.Count, "Peptide Matches");
            //Assert.AreEqual(expectedSeqs, identData.SequenceCollection.DBSequences.Count, "DB Sequences");
        }
    }
}
