using System;
using System.IO;
using NUnit.Framework;
using PSI_Interface.MSData;

namespace Interface_Tests.MSDataTests
{
    public class SimpleMzMLReaderTests
    {
        [Test]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML", 9293)]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML.gz", 9293)]
        public void ReadMzMLTest(string path, int expectedSpectra)
        {
            var sourceFile = new FileInfo(Path.Combine(TestPath.ExtTestDataDirectory, path));
            if (!sourceFile.Exists)
            {
                Console.WriteLine("File not found: " + sourceFile.FullName);
                return;
            }

            using (var reader = new SimpleMzMLReader(sourceFile.FullName, false, true))
            {
                Assert.AreEqual(expectedSpectra, reader.NumSpectra);
                var specCount = 0;
                foreach (var spec in reader.ReadAllSpectra(false))
                {
                    specCount++;
                }
                Assert.AreEqual(expectedSpectra, specCount);
            }
        }

        [Test]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML", 1)]
        [TestCase(@"MzML\Rush2_p14RR_62_26Oct17_Smeagol-WRUSHCol3_75x20a_SRM.mzML", 967)]
        [TestCase(@"MzML\Rush2_p14RR_62_26Oct17_Smeagol-WRUSHCol3_75x20a_srmasspectra.mzML", 1)]
        [TestCase(@"MzML\TdyQc1_Fnl_All_25Oct17_Balzac-W1sta1_SRM.mzML", 4701)]
        //[TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML.gz", 9293)] // implemented, but decompresses first
        public void ReadMzMLChromatogramsTestRandom(string path, int expectedChromatograms)
        {
            var sourceFile = new FileInfo(Path.Combine(TestPath.ExtTestDataDirectory, path));
            if (!sourceFile.Exists)
            {
                Console.WriteLine("File not found: " + sourceFile.FullName);
                return;
            }

            using (var reader = new SimpleMzMLReader(sourceFile.FullName, true, true))
            {
                Assert.AreEqual(expectedChromatograms, reader.NumChromatograms);
                var chromCount = 0;
                foreach (var chrom in reader.ReadAllChromatograms(false))
                {
                    chromCount++;
                }
                Assert.AreEqual(expectedChromatograms, chromCount);
            }
        }

        [Test]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML", 9293, 1)]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML.gz", 9293, 1)]
        [TestCase(@"MzML\Rush2_p14RR_62_26Oct17_Smeagol-WRUSHCol3_75x20a_SRM.mzML", 0, 967)]
        [TestCase(@"MzML\Rush2_p14RR_62_26Oct17_Smeagol-WRUSHCol3_75x20a_srmasspectra.mzML", 104278, 1)]
        [TestCase(@"MzML\TdyQc1_Fnl_All_25Oct17_Balzac-W1sta1_SRM.mzML", 0, 4701)]
        public void ReadMzMLChromatogramsTestNonRandom(string path, int expectedSpectra, int expectedChromatograms)
        {
            var sourceFile = new FileInfo(Path.Combine(TestPath.ExtTestDataDirectory, path));
            if (!sourceFile.Exists)
            {
                Console.WriteLine("File not found: " + sourceFile.FullName);
                return;
            }

            using (var reader = new SimpleMzMLReader(sourceFile.FullName, false, true))
            {
                Assert.AreEqual(expectedSpectra, reader.NumSpectra);
                if (expectedSpectra != 0)
                {
                    Assert.AreEqual(0, reader.NumChromatograms);
                }
                var specCount = 0;
                foreach (var spec in reader.ReadAllSpectra(false))
                {
                    specCount++;
                }
                Assert.AreEqual(expectedSpectra, specCount);
                Assert.AreEqual(expectedChromatograms, reader.NumChromatograms);

                var chromCount = 0;
                foreach (var chrom in reader.ReadAllChromatograms(false))
                {
                    chromCount++;
                }
                Assert.AreEqual(expectedChromatograms, reader.NumChromatograms);
                Assert.AreEqual(expectedChromatograms, chromCount);
            }
        }
    }
}
