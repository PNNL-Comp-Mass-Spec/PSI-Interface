using System;
using System.IO;
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

            if (!dirFinder.ToLower().Contains(string.Format("{0}bin", Path.DirectorySeparatorChar)))
            {
                dirFinder = TestContext.CurrentContext.TestDirectory;
            }

            // Find the bin folder...
            while (!string.IsNullOrWhiteSpace(dirFinder) && !dirFinder.EndsWith("bin"))
            {
                //Console.WriteLine("Project: " + dirFinder);
                dirFinder = Path.GetDirectoryName(dirFinder);
            }
            //Console.WriteLine("Project: " + dirFinder);

            // Local test directory
            TestDirectory = Path.GetDirectoryName(dirFinder);

            if (string.IsNullOrEmpty(TestDirectory))
            {
                throw new Exception("Path.GetDirectoryName returned null for path " + dirFinder);
            }

            // Local test\TestData directory
            TestDataDirectory = Path.Combine(TestDirectory, "TestData");

            // The Project/Solution Directory name
            ProjectDirectory = Path.GetDirectoryName(TestDirectory);

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

        public static bool FindInputFile(string inputFileRelativePath, out FileInfo inputFile)
        {
            var localSourceFile1 = new FileInfo(Path.Combine(TestDataDirectory, inputFileRelativePath));

            if (localSourceFile1.Exists)
            {
                inputFile = localSourceFile1;

                return true;
            }

            var localSourceFile2 = new FileInfo(Path.Combine(TestDataDirectory, Path.GetFileName(inputFileRelativePath)));

            if (localSourceFile2.Exists)
            {
                inputFile = localSourceFile2;

                return true;
            }

            var remoteFile = new FileInfo(Path.Combine(ExtTestDataDirectory, inputFileRelativePath));

            if (remoteFile.Exists)
            {
                inputFile = remoteFile;

                return true;
            }

            inputFile = null;

            return false;
        }
    }
}
