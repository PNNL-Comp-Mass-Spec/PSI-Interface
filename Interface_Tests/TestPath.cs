using System;

namespace Interface_Tests
{
	public static class TestPath
	{
        static TestPath()
        {
			//TestDirectory = @"\\protoapps\UserData\Gibbons\testData\";
			ExtTestDataDirectory = @"E:\PSI_Interface_TestData";

			// The Execution directory
			var dirFinder = Environment.CurrentDirectory;
			// Find the bin folder...
	        while (!string.IsNullOrWhiteSpace(dirFinder) && !dirFinder.EndsWith("bin"))
	        {
				//Console.WriteLine("Project: " + dirFinder);
		        dirFinder = System.IO.Path.GetDirectoryName(dirFinder);
	        }
			//Console.WriteLine("Project: " + dirFinder);
			// The Directory for MTDBCreatorTestSuite
			TestDirectory = System.IO.Path.GetDirectoryName(dirFinder);
			// The TestSuite\TestData Directory
			TestDataDirectory = System.IO.Path.Combine(TestDirectory, "TestData");
			// The Project/Solution Directory
			ProjectDirectory = System.IO.Path.GetDirectoryName(TestDirectory);
			//Console.WriteLine("TestSuite: " + TestSuiteDirectory);
			//Console.WriteLine("TestSuiteTestData: " + TestSuiteTestDataDirectory);
			//Console.WriteLine("ProjectDir: " + ProjectDirectory);
        }

		public static string ExtTestDataDirectory
        {
            get;
            private set;
        }

		public static string TestDirectory
		{
			get;
			private set;
		}

		public static string TestDataDirectory
		{
			get;
			private set;
		}

		public static string ProjectDirectory
		{
			get;
			private set;
		}
	}
}
