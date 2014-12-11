using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CV_Generator;
using NUnit.Framework;

namespace Interface_Tests.CVTests
{
	class CVTests
	{
		public CVTests()
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
		public void UnimodTests()
		{
			var reader = new Unimod_obo_Reader();
			reader.Read();
			var fileData = reader.FileData;

		}

		[Test]
		public void PSI_MSTests()
		{
			var reader = new PSI_MS_obo_Reader();
			reader.Read();
			var fileData = reader.FileData;
			var imports = reader.ImportedFileData;
		}
	}
}
