using System.IO;
using System.Linq;
using CV_Generator;
using CV_Generator.OBO_Objects;
using NUnit.Framework;
using PSI_Interface.CV;
using PSI_Interface.IdentData.IdentDataObjs;

namespace Interface_Tests.CVTests
{
    internal class CVTests
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
        [TestCase("Unimod_terms.txt")]
        public void UnimodTests(string outFileName)
        {
            var outFile = new FileInfo(Path.Combine(TestPath.ExtTestDataDirectory, outFileName));

            var reader = new Unimod_obo_Reader();
            reader.Read();
            var fileData = reader.FileData;

            WriteCVTerms(outFile, fileData);
        }

        [Test]
        [TestCase("PSI-MS_terms.txt")]
        public void PSI_MSTests(string outFileName)
        {
            var outFile = new FileInfo(Path.Combine(TestPath.ExtTestDataDirectory, outFileName));

            var reader = new PSI_MS_obo_Reader();
            reader.Read();
            var fileData = reader.FileData;
            var imports = reader.ImportedFileData;

            WriteCVTerms(outFile, fileData);

            foreach (var oboFile in imports)
            {
                var importedFile = new FileInfo(Path.Combine(TestPath.ExtTestDataDirectory,
                    Path.GetFileNameWithoutExtension(outFileName) + " imported " + oboFile.Name + ".txt"));

                WriteCVTerms(importedFile, oboFile);
            }
        }

        private void WriteCVTerms(FileSystemInfo outFile, OBO_File fileData)
        {
            using (var writer = new StreamWriter(new FileStream(outFile.FullName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)))
            {
                writer.WriteLine("Version=" + fileData.Version);
                writer.WriteLine();

                var formatString = "{0,-20} {1,-50} {2}";
                writer.WriteLine(formatString, "Key", "Value", "Definition");
                writer.WriteLine(formatString, "---", "-----", "----------");

                foreach (var cvTerm in fileData.Terms)
                {
                    writer.WriteLine(formatString, cvTerm.Key, TrimLength(cvTerm.Value.Name, 50), cvTerm.Value.Def);
                }
            }
        }

        private string TrimLength(string value, int maxLength)
        {
            if (value.Length <= maxLength)
                return value;

            return value.Substring(0, maxLength - 3) + "...";
        }

        [Test]
        public void TestUnimodNameMapping()
        {
            var termGroup = CV.TermAccessionLookup["UNIMOD"].Where(x => x.Value != CV.CVID.UNIMOD_unimod_root_node);
            foreach (var term in termGroup)
            {
                var info = CV.TermData[term.Value];
                var mod = new ModificationObj(CV.CVID.CVID_Unknown, info.Name, 5, 42);
                if (mod.CVParams.Count == 1)
                {
                    Assert.AreEqual(term.Value, mod.CVParams[0].Cvid, "Enums do not match!");
                }
                else
                {
                    var foundMatch = false;
                    foreach (var cvParam in mod.CVParams)
                    {
                        if (cvParam.Cvid.Equals(term.Value))
                        {
                            foundMatch = true;
                            break;
                        }
                    }
                    if (!foundMatch)
                    {
                        Assert.Fail("No matching enum found! {0}", term.Value);
                    }
                }
            }
        }
    }
}
