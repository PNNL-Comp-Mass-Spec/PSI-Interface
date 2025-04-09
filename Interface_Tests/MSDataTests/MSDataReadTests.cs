using System;
using System.IO;
using NUnit.Framework;
using PSI_Interface.MSData;
using PSI_Interface.MSData.mzML;

namespace Interface_Tests.MSDataTests
{
    [TestFixture]
    internal class MSDataReadTests
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
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML", 9293, false)]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML.gz", 9293, false)]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML", 9293, true)]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML.gz", 9293, true)]
        public void TestDeleteMzMLAfterRead(string inputFileRelativePath, int expectedSpectra, bool useMSDataWrapper)
        {
            if (!TestPath.FindInputFile(inputFileRelativePath, out var sourceFile))
            {
                Console.WriteLine("File not found: " + inputFileRelativePath);
                return;
            }

            var tempDirectory = Path.GetTempPath();
            var tempFileName = string.Format("{0}_tmp{1}", Path.GetFileNameWithoutExtension(sourceFile.Name), Path.GetExtension(sourceFile.Name));
            var tempFile = new FileInfo(Path.Combine(tempDirectory, tempFileName));

            try
            {
                if (tempFile.Exists)
                {
                    tempFile.Delete();
                }

                sourceFile.CopyTo(tempFile.FullName);

                var reader = new MzMLReader(tempFile.FullName);

                if (!useMSDataWrapper)
                {
                    // Option 1:
                    var mzMLData = reader.Read();
                    Console.WriteLine("Spectrum count reported by MzMLReader: " + mzMLData.run.spectrumList.count);
                    Assert.AreEqual(expectedSpectra, int.Parse(mzMLData.run.spectrumList.count), "Spectrum count");
                }
                else
                {
                    // Option 2:
                    var mzMLData = new MSData(reader.Read());
                    Console.WriteLine("Spectrum count reported by MSData: " + mzMLData.Run.SpectrumList.Spectra.Count);
                    Assert.AreEqual(expectedSpectra, mzMLData.Run.SpectrumList.Spectra.Count, "Spectrum count");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception: {0}", ex.Message);
            }
            finally
            {
                tempFile.Delete();
            }
        }

        [Test]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML", 9293)]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML.gz", 9293)]
        public void TestMzMLRead(string inputFileRelativePath, int expectedSpectra)
        {
            if (!TestPath.FindInputFile(inputFileRelativePath, out var sourceFile))
            {
                Console.WriteLine("File not found: " + inputFileRelativePath);
                return;
            }

            var reader = new MzMLReader(sourceFile.FullName);
            var mzMLData = new MSData(reader.Read());

            Console.WriteLine("Spectrum count: " + mzMLData.Run.SpectrumList.Spectra.Count);
            Assert.AreEqual(expectedSpectra, mzMLData.Run.SpectrumList.Spectra.Count, "Spectrum count");
        }
    }
}
