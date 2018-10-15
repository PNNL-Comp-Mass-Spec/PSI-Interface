using System;
using NUnit.Framework;

namespace Interface_Tests
{
    public static class TestPath
    {
        static TestPath()
        {
            // External test directory
            ExtTestDataDirectory = @"\\proto-2\UnitTest_Files\PSI_Interface";

            // The Execution directory
            var dirFinder = Environment.CurrentDirectory;
            if (!dirFinder.ToLower().Contains("bin"))
            {
                dirFinder = TestContext.CurrentContext.TestDirectory;
            }
            // Find the bin folder...
            while (!string.IsNullOrWhiteSpace(dirFinder) && !dirFinder.EndsWith("bin"))
            {
                //Console.WriteLine("Project: " + dirFinder);
                dirFinder = System.IO.Path.GetDirectoryName(dirFinder);
            }
            //Console.WriteLine("Project: " + dirFinder);

            // Local test directory
            TestDirectory = System.IO.Path.GetDirectoryName(dirFinder);

            if (string.IsNullOrEmpty(TestDirectory))
            {
                throw new Exception("Path.GetDirectoryName returned null for path " + dirFinder);
            }

            // Local test\TestData directory
            TestDataDirectory = System.IO.Path.Combine(TestDirectory, "TestData");

            // The Project/Solution Directory name
            ProjectDirectory = System.IO.Path.GetDirectoryName(TestDirectory);

            /*
            Console.WriteLine("Remote Test directory: " + ExtTestDataDirectory);
            Console.WriteLine("Local Test directory: " + TestDirectory);
            Console.WriteLine("Local TestData directory: " + TestDataDirectory);
            Console.WriteLine("Project directory name: " + ProjectDirectory);
            Console.WriteLine();
            */
        }

        public static string ExtTestDataDirectory { get; }

        public static string TestDirectory { get; }

        public static string TestDataDirectory { get; }

        public static string ProjectDirectory { get; }
    }
}
