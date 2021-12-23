using System;
using System.IO;
using NUnit.Framework;
using PSI_Interface.MSData;
using PSI_Interface.MSData.mzML;

namespace Interface_Tests.MSDataTests
{
    [TestFixture]
    class MSDataReadTests
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
        //[TestCase(@"mzML\VA139IMSMS_noIndex.mzML", 3145)]
        //[TestCase(@"mzML\VA139IMSMS_noIndex.mzML.gz", 3145)]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML", 9293)]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML.gz", 9293)]
        public void MzMLReadTest(string inputFileRelativePath, int expectedSpectra)
        {
            if (!TestPath.FindInputFile(inputFileRelativePath, out var sourceFile))
            {
                Console.WriteLine("File not found: " + inputFileRelativePath);
                return;
            }

            var reader = new MzMLReader(Path.Combine(TestPath.ExtTestDataDirectory, sourceFile.FullName));
            //mzMLType mzMLData = reader.Read();
            var mzMLData = new MSData(reader.Read());

            Console.WriteLine("Spectrum count: " + mzMLData.Run.SpectrumList.Spectra.Count);
            Assert.AreEqual(expectedSpectra, mzMLData.Run.SpectrumList.Spectra.Count, "Spectrum count");
        }

        [Test]
        //[TestCase(@"mzML\VA139IMSMS.mzML", 3145)]
        //[TestCase(@"mzML\VA139IMSMS_compressed.mzML", 3145)]
        //[TestCase(@"mzML\VA139IMSMS.mzML.gz", 3145)]
        //[TestCase(@"mzML\sample1-A_BB2_01_922.mzML", 43574)]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML", 9293)]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML.gz", 9293)]
        public void MzMLIndexedReadTest(string inputFileRelativePath, int expectedSpectra)
        {
            if (!TestPath.FindInputFile(inputFileRelativePath, out var sourceFile))
            {
                Console.WriteLine("File not found: " + inputFileRelativePath);
                return;
            }

            var reader = new MzMLReader(Path.Combine(TestPath.ExtTestDataDirectory, sourceFile.FullName));
            //mzMLType mzMLData = reader.Read();
            var mzMLData = new MSData(reader.Read());

            Console.WriteLine("Spectrum count: " + mzMLData.Run.SpectrumList.Spectra.Count);
            Assert.AreEqual(expectedSpectra, mzMLData.Run.SpectrumList.Spectra.Count, "Spectrum count");
        }
    }
}
