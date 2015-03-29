using System.IO;
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
		public mzMLReadTests()
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
		[TestCase(@"mzML\VA139IMSMS_noIndex.mzML", 3145)]
		[TestCase(@"mzML\VA139IMSMS_noIndex.mzML.gz", 3145)]
		public void MzMLReadTest(string path, int expectedSpectra)
		{
			var reader = new MzMLReader(Path.Combine(TestPath.ExtTestDataDirectory, path));
			mzMLType mzMLData = reader.Read();
			Assert.AreEqual(expectedSpectra.ToString(), mzMLData.run.spectrumList.count.ToString(), "Stored Count");
			Assert.AreEqual(expectedSpectra, mzMLData.run.spectrumList.spectrum.Count, "Array length");
		}

		[Test]
		[TestCase(@"mzML\VA139IMSMS.mzML", 3145)]
		[TestCase(@"mzML\VA139IMSMS_compressed.mzML", 3145)]
		[TestCase(@"mzML\VA139IMSMS.mzML.gz", 3145)]
		[TestCase(@"mzML\sample1-A_BB2_01_922.mzML", 43574)]
		public void MzMLIndexedReadTest(string path, int expectedSpectra)
		{
			var reader = new MzMLReader(Path.Combine(TestPath.ExtTestDataDirectory, path));
			mzMLType mzMLData = reader.Read();
			Assert.AreEqual(expectedSpectra.ToString(), mzMLData.run.spectrumList.count.ToString(), "Stored Count");
			Assert.AreEqual(expectedSpectra, mzMLData.run.spectrumList.spectrum.Count, "Array length");
		}
	}
}
