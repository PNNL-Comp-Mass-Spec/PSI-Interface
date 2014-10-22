using System;
using System.IO;
using Interface_Tests;
using NUnit.Framework;
using PSI_Interface.mzML;

namespace Interface_Tests.mzMLTests
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
		public void MzMLTest(string inPath, string outPath, int expectedSpectra)
		{
			var reader = new MzMLReader(Path.Combine(TestPath.ExtTestDataDirectory, inPath));
			mzMLType mzMLData = reader.Read();
			Assert.AreEqual(expectedSpectra.ToString(), mzMLData.run.spectrumList.count.ToString(), "Stored Count");
			Assert.AreEqual(expectedSpectra, mzMLData.run.spectrumList.spectrum.Length, "Array length");
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
		public void MzMLIndexedTest(string path, int expectedSpectra)
		{
			var reader = new MzMLReader(Path.Combine(TestPath.ExtTestDataDirectory, path));
			mzMLType mzMLData = reader.Read();
			Assert.AreEqual(expectedSpectra.ToString(), mzMLData.run.spectrumList.count.ToString(), "Stored Count");
			Assert.AreEqual(expectedSpectra, mzMLData.run.spectrumList.spectrum.Length, "Array length");
		}*/
	}
}
