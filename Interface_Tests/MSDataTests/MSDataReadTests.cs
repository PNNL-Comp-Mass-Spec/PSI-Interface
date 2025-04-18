﻿using System;
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
        [TestCase(@"MzML\Angiotensin_AllScans.mzML", 1775, false)]
        [TestCase(@"MzML\Angiotensin_AllScans_NoIndex.mzML", 1775, false)]
        [TestCase(@"MzML\Angiotensin_AllScans.mzML", 1775, true)]
        [TestCase(@"MzML\Angiotensin_AllScans_NoIndex.mzML", 1775, true)]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML", 9293, false)]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML.gz", 9293, false)]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14_NoIndex.mzML", 9293, false)]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML", 9293, true)]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML.gz", 9293, true)]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14_NoIndex.mzML", 9293, true)]
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

                Console.WriteLine();

                try
                {
                    // Call .Read() again; an exception should occur
                    reader.Read();
                    Assert.Fail("Repeated call to reader.Read() should have raised an exception");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("As expected, calling .Read() again results in exception \"{0}\"", ex.Message);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception: {0}", ex.Message);
            }
            finally
            {
                try
                {
                    tempFile.Delete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("Error deleting file {0}: {1}", tempFile.FullName, ex.Message);

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    tempFile.Delete();

                    Assert.Fail("File successfully deleted after garbage collection, but use of GC should not have been required");
                }
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
