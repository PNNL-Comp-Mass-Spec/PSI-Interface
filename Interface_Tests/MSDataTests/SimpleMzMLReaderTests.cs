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
    }
}
