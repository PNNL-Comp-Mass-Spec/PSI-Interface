using System.IO;
using NUnit.Framework;
using PSI_Interface.MSData.mzML;

namespace Interface_Tests.MSDataTests.mzMLTests
{
    /// <summary>
    /// Summary description for mzMLReadTests
    /// </summary>
    [TestFixture]
    public class mzMLWriteTests
    {
        public mzMLWriteTests()
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
        [TestCase(@"mzML\VA139IMSMS_noIndex.mzML", @"mzML\output\VA139IMSMS_noIndex.mzML", 3145)]
        [TestCase(@"mzML\VA139IMSMS_noIndex.mzML.gz", @"mzML\output\VA139IMSMS_noIndex.mzML.gz", 3145)]
        public void MzMLWriteTest(string inPath, string outPath, int expectedSpectra)
        {
            var reader = new MzMLReader(Path.Combine(TestPath.ExtTestDataDirectory, inPath));
            mzMLType mzMLData = reader.Read();
            Assert.AreEqual(expectedSpectra.ToString(), mzMLData.run.spectrumList.count.ToString(), "Stored Count");
            Assert.AreEqual(expectedSpectra, mzMLData.run.spectrumList.spectrum.Count, "Array length");
            var writer = new MzMLWriter(Path.Combine(TestPath.ExtTestDataDirectory, outPath));
            writer.MzMLType = MzMLSchemaType.MzML;
            writer.Write(mzMLData);
        }

        /*
        [Test]
        [TestCase(@"mzML\VA139IMSMS.mzML", 3145)]
        [TestCase(@"mzML\VA139IMSMS_compressed.mzML", 3145)]
        [TestCase(@"mzML\VA139IMSMS.mzML.gz", 3145)]
        [TestCase(@"mzML\sample1-A_BB2_01_922.mzML", 43574)]
        public void MzMLIndexedWriteTest(string path, int expectedSpectra)
        {
            // TODO: Output will not have really close resemblance to the input, because of the transfer from indexedmzML to non-indexed mzML.
            // TODO: Indexed mzML write will write a bad file - the index will be incorrect, and the easiest way to fix it may involve reading the file back in, scanning and storing the offsets, and then replacing them.
            // TODO:
            // TODO: Also, need to have the SHA1 hash of the file up to the hash location....
            // TODO: 
            var reader = new MzMLReader(Path.Combine(TestPath.ExtTestDataDirectory, path));
            mzMLType mzMLData = reader.Read();
            Assert.AreEqual(expectedSpectra.ToString(), mzMLData.run.spectrumList.count.ToString(), "Stored Count");
            Assert.AreEqual(expectedSpectra, mzMLData.run.spectrumList.spectrum.Length, "Array length");
        }*/
    }
}
