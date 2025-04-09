using System;
using PSI_Interface.MSData.mzML;
using NUnit.Framework;

namespace Interface_Tests.MSDataTests.mzMLTests
{
    /// <summary>
    /// Summary description for mzMLReadTests
    /// </summary>
    [TestFixture]
    public class mzMLReadTests
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
        // [TestCase(@"mzML\VA139IMSMS_noIndex.mzML", 3145)]
        // [TestCase(@"mzML\VA139IMSMS_noIndex.mzML.gz", 3145)]
        // [TestCase(@"mzML\VA139IMSMS.mzML", 3145)]
        // [TestCase(@"mzML\VA139IMSMS_compressed.mzML", 3145)]
        // [TestCase(@"mzML\VA139IMSMS.mzML.gz", 3145)]
        // [TestCase(@"mzML\sample1-A_BB2_01_922.mzML", 43574)]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML", 9293)]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML.gz", 9293)]
        [TestCase(@"MzML\Angiotensin_AllScans.mzML", 1775)]
        [TestCase(@"MzML\Angiotensin_AllScans_NoIndex.mzML", 1775)]

        public void MzMLReadTest(string inputFileRelativePath, int expectedSpectra)
        {
            if (!TestPath.FindInputFile(inputFileRelativePath, out var sourceFile))
            {
                Console.WriteLine("File not found: " + inputFileRelativePath);
                return;
            }

            var reader = new MzMLReader(sourceFile.FullName);
            var mzMLData = reader.Read();

            Console.WriteLine("Spectrum count: " + mzMLData.run.spectrumList.count);
            Console.WriteLine("Array length: " + mzMLData.run.spectrumList.spectrum.Count);

            Assert.AreEqual(expectedSpectra.ToString(), mzMLData.run.spectrumList.count, "Spectrum Count");
            Assert.AreEqual(expectedSpectra, mzMLData.run.spectrumList.spectrum.Count, "Array length");
        }
    }
}
