using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PSI_Interface.MSData;
using PSI_Interface.MSData.mzML;

namespace Interface_Tests.MSDataTests
{
	/// <summary>
	/// Summary description for mzMLReadTests
	/// </summary>
	[TestFixture]
	public class MSDataWriteTests
	{
        public MSDataWriteTests()
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
			MSData mzMLData = new MSData(reader.Read());
			//Assert.AreEqual(expectedSpectra.ToString(), mzMLData.Run.SpectrumList.Count.ToString(), "Stored Count");
			Assert.AreEqual(expectedSpectra, mzMLData.Run.SpectrumList.Spectra.Count, "Array length");
			var writer = new MzMLWriter(Path.Combine(TestPath.ExtTestDataDirectory, outPath));
			writer.MzMLType = MzMLSchemaType.MzML;
			writer.Write(new mzMLType(mzMLData));
		}

        /*
		[Test]
		[TestCase(@"mzML\VA139IMSMS.mzML", 3145)]
		[TestCase(@"mzML\VA139IMSMS_compressed.mzML", 3145)]
		[TestCase(@"mzML\VA139IMSMS.mzML.gz", 3145)]
		[TestCase(@"mzML\sample1-A_BB2_01_922.mzML", 43574)]
        public void MzMLIndexedWriteTest(string inPath, string outPath, int expectedSpectra)
		{
            // TODO: Output will not have really close resemblance to the input, because of the transfer from indexedmzML to non-indexed mzML.
            // TODO: Indexed mzML write will write a bad file - the index will be incorrect, and the easiest way to fix it may involve reading the file back in, scanning and storing the offsets, and then replacing them.
            // TODO:
            // TODO: Also, need to have the SHA1 hash of the file up to the hash location....
            // TODO: 
			var reader = new MzMLReader(Path.Combine(TestPath.ExtTestDataDirectory, inPath));
			MSData mzMLData = new MSData(reader.Read());
			//Assert.AreEqual(expectedSpectra.ToString(), mzMLData.Run.SpectrumList.Count.ToString(), "Stored Count");
            Assert.AreEqual(expectedSpectra, mzMLData.Run.SpectrumList.Spectra.Count, "Array length");
            var writer = new MzMLWriter(Path.Combine(TestPath.ExtTestDataDirectory, outPath));
            writer.MzMLType = MzMLSchemaType.MzML;
            writer.Write(new mzMLType(mzMLData));
		}*/
    }
}
