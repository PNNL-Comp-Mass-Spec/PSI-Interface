using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using PSI_Interface.MSData;

namespace Interface_Tests.MSDataTests
{
    public class SimpleMzMLReaderTests
    {
        /// <summary>
        /// When using non-random access, iterate through spectra using reader.ReadAllSpectra()
        /// </summary>
        /// <param name="inputFileRelativePath"></param>
        /// <param name="expectedSpectra"></param>
        /// <param name="includePeaks"></param>
        [Test]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML", 9293, false)]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML.gz", 9293, false)]
        [TestCase(@"MzML\Dionex_BSA_OTOT-08_Scans1-200.mzML", 200, true)]
        [TestCase(@"MzML\Dionex_BSA_OTOT-08_Scans200-300.mzML", 101, true)]
        public void ReadMzMLTest(string inputFileRelativePath, int expectedSpectra, bool includePeaks)
        {
            if (!TestPath.FindInputFile(inputFileRelativePath, out var sourceFile))
            {
                Console.WriteLine("File not found: " + inputFileRelativePath);
                return;
            }

            using (var reader = new SimpleMzMLReader(sourceFile.FullName, false, true))
            {
                Assert.AreEqual(expectedSpectra, reader.NumSpectra);
                var specCount = 0;
                foreach (var spec in reader.ReadAllSpectra(includePeaks))
                {
                    if (specCount < 100 || specCount >= expectedSpectra - 100)
                    {
                        Console.WriteLine("Spectrum {0,4}, NativeID {1,-45} has {2:N0} data points", spec.ScanNumber, spec.NativeId, spec.Peaks.Length);
                    }
                    else if (specCount == 100)
                    {
                        Console.WriteLine("...");
                    }

                    specCount++;
                }
                Assert.AreEqual(expectedSpectra, specCount);
            }
        }

        /// <summary>
        /// Open the file with random access, either immediately or after instantiating the reader
        /// Obtain spectra data with reader.ReadMassSpectrum()
        /// </summary>
        /// <param name="inputFileRelativePath"></param>
        /// <param name="expectedSpectra"></param>
        /// <param name="includePeaks"></param>
        /// <param name="immediateRandomAccess"></param>
        [Test]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML", 9293, false, true)]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML.gz", 9293, false, true)]
        [TestCase(@"MzML\Dionex_BSA_OTOT-08_Scans1-200.mzML", 200, true, false)]
        [TestCase(@"MzML\Dionex_BSA_OTOT-08_Scans1-200.mzML", 200, true, true)]
        [TestCase(@"MzML\Dionex_BSA_OTOT-08_Scans200-300.mzML", 101, true, false)]
        [TestCase(@"MzML\Dionex_BSA_OTOT-08_Scans200-300.mzML", 101, true, true)]
        public void ReadMzMLTestRandom(string inputFileRelativePath, int expectedSpectra, bool includePeaks, bool immediateRandomAccess)
        {
            if (!TestPath.FindInputFile(inputFileRelativePath, out var sourceFile))
            {
                Console.WriteLine("File not found: " + inputFileRelativePath);
                return;
            }

            using (var reader = new SimpleMzMLReader(sourceFile.FullName, immediateRandomAccess, true))
            {
                if (!immediateRandomAccess)
                {
                    reader.TryMakeRandomAccessCapable();
                }

                Assert.AreEqual(expectedSpectra, reader.NumSpectra);
                var stepSize = Math.Max(1, (expectedSpectra / 100));

                // Note: calling .ReadMassSpectrum with scanNumber = 1 returns the first spectrum in the file, regardless of its actual scan number
                var scanNumber = 1;
                while (scanNumber <= expectedSpectra)
                {
                    var spec = reader.ReadMassSpectrum(scanNumber, includePeaks);

                    if (includePeaks)
                        Console.WriteLine("Spectrum {0,4}, NativeID {1,-45}, scan {2,4} has {3:N0} data points", spec.ScanNumber, spec.NativeId, spec.NativeIdScanNumber, spec.Peaks.Length);
                    else
                        Console.WriteLine("Spectrum {0,4}, NativeID {1,-45}, scan {2,4} (peaks not loaded)", spec.ScanNumber, spec.NativeId, spec.NativeIdScanNumber);

                    scanNumber += stepSize;
                }
            }
        }

        /// <summary>
        /// Open the file with random access and obtain specific mass spectra by scan number
        /// </summary>
        /// <param name="inputFileRelativePath"></param>
        /// <param name="expectedSpectra"></param>
        /// <param name="includePeaks"></param>
        /// <param name="scanNumberList">Comma-separated list of scan numbers to obtain</param>
        /// <param name="expectedMissingScanNumbers">Comma-separated list of scan numbers for which reader.GetSpectrumForScan() should return null</param>
        [Test]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML", 9293, false, "1,10,100,3200,6000,9293", "")]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML.gz", 9293, false, "1,10,100,3200,6000,9293,10000", "10000")]
        [TestCase(@"MzML\Dionex_BSA_OTOT-08_Scans1-200.mzML", 200, true, "1,10,100,175,200,300", "300")]
        [TestCase(@"MzML\Dionex_BSA_OTOT-08_Scans200-300.mzML", 101, true, "1,100,200,215,250,285,300", "1,100")]
        public void ReadMzMLTestRetrieveByScanNumber(string inputFileRelativePath, int expectedSpectra, bool includePeaks, string scanNumberList, string expectedMissingScanNumbers)
        {
            if (!TestPath.FindInputFile(inputFileRelativePath, out var sourceFile))
            {
                Console.WriteLine("File not found: " + inputFileRelativePath);
                return;
            }

            var scanNumbers = ParseDelimitedIntegerList(scanNumberList);

            var expectedMissingScans = ParseDelimitedIntegerList(expectedMissingScanNumbers);

            using (var reader = new SimpleMzMLReader(sourceFile.FullName, true, true))
            {
                Assert.AreEqual(expectedSpectra, reader.NumSpectra);

                foreach (var scanNumber in scanNumbers)
                {
                    var spec = reader.GetSpectrumForScan(scanNumber, includePeaks);

                    if (spec == null)
                    {
                        if (expectedMissingScans.Contains(scanNumber))
                        {
                            Console.WriteLine("Scan {0} not found; this is expected", scanNumber);
                            continue;
                        }
                        Assert.Fail("GetSpectrumForScan returned null for scan {0}", scanNumber);
                    }

                    if (includePeaks)
                        Console.WriteLine("Spectrum {0,4}, NativeID {1,-45}, scan {2,4} has {3:N0} data points", spec.ScanNumber, spec.NativeId, spec.NativeIdScanNumber, spec.Peaks.Length);
                    else
                        Console.WriteLine("Spectrum {0,4}, NativeID {1,-45}, scan {2,4} (peaks not loaded)", spec.ScanNumber, spec.NativeId, spec.NativeIdScanNumber);

                    Assert.AreEqual(scanNumber, spec.NativeIdScanNumber);
                }
            }
        }

        /// <summary>
        /// Retrieve spectra using the given artificial (1-based) scan number
        /// Compare the actual scan numbers to expected values
        /// </summary>
        /// <param name="inputFileRelativePath"></param>
        /// <param name="artificialScanNumberList"></param>
        /// <param name="expectedActualScanNumberList"></param>
        [Test]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML", "0,1,10,100,3200,6000,9293,10000", "0,1,10,100,3200,6000,9293,0")]
        [TestCase(@"MzML\Dionex_BSA_OTOT-08_Scans1-200.mzML", "1,10,100,175,200,201,300", "1,10,100,175,200,0,0")]
        [TestCase(@"MzML\Dionex_BSA_OTOT-08_Scans200-300.mzML", "1,50,95,101,102,200", "200,249,294,300,0,0")]
        public void ReadMzMLCompareScanNumbers(string inputFileRelativePath, string artificialScanNumberList, string expectedActualScanNumberList)
        {
            if (!TestPath.FindInputFile(inputFileRelativePath, out var sourceFile))
            {
                Console.WriteLine("File not found: " + inputFileRelativePath);
                return;
            }

            var artificialScanNumbers = ParseOrderedIntegerList(artificialScanNumberList);

            var expectedActualScanNumbers = ParseOrderedIntegerList(expectedActualScanNumberList);

            // Run the test twice, first with randomAccess disabled, then with randomAccess enabled
            for (var i = 0; i < 2; i++)
            {
                var randomAccess = i > 0;

                using (var reader = new SimpleMzMLReader(sourceFile.FullName, randomAccess, true))
                {
                    for (var j = 0; j < artificialScanNumbers.Count; j++)
                    {
                        var artificialScanNumber = artificialScanNumbers[j];
                        var expectedScanNumber = expectedActualScanNumbers[j];

                        var spectrum = reader.ReadMassSpectrum(artificialScanNumber, false);

                        if (expectedScanNumber <= 0)
                        {
                            Console.WriteLine("Spectrum {0,4} does not exist; this was expected", artificialScanNumber);

                            Assert.IsNull(spectrum);
                            continue;
                        }
                        Assert.IsNotNull(spectrum);

                        Console.WriteLine("Spectrum {0,4}, NativeID {1,-45}, scan {2,4}", spectrum.ScanNumber, spectrum.NativeId, spectrum.NativeIdScanNumber);

                        Assert.AreEqual(expectedScanNumber, spectrum.NativeIdScanNumber);

                        var comparisonSpectrum = reader.GetSpectrumForScan(spectrum.NativeIdScanNumber, false);

                        Assert.AreEqual(comparisonSpectrum.NativeId, spectrum.NativeId);
                    }
                }

                Console.WriteLine();
            }
        }

        private static SortedSet<int> ParseDelimitedIntegerList(string scanNumberList)
        {
            var scanNumbers = new SortedSet<int>();
            foreach (var item in scanNumberList.Split(','))
            {
                if (int.TryParse(item, out var scanNumber))
                {
                    scanNumbers.Add(scanNumber);
                }
            }

            return scanNumbers;
        }

        private static List<int> ParseOrderedIntegerList(string scanNumberList)
        {
            var scanNumbers = new List<int>();
            foreach (var item in scanNumberList.Split(','))
            {
                if (int.TryParse(item, out var scanNumber))
                {
                    scanNumbers.Add(scanNumber);
                }
            }

            return scanNumbers;
        }

        /// <summary>
        /// Open the file with random access and obtain chromatogram data using reader.ReadChromatogram()
        /// </summary>
        /// <remarks>
        /// If the file is not initially opened with random access enabled,
        /// reader.NumChromatograms will report 0 and the chromatogram data cannot be read
        /// </remarks>
        /// <param name="inputFileRelativePath"></param>
        /// <param name="expectedChromatograms"></param>
        /// <param name="immediateRandomAccess"></param>
        [Test]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML", 1, true)]
        [TestCase(@"MzML\Rush2_p14RR_62_26Oct17_Smeagol-WRUSHCol3_75x20a_SRM.mzML", 967, true)]
        [TestCase(@"MzML\Rush2_p14RR_62_26Oct17_Smeagol-WRUSHCol3_75x20a_srmasspectra.mzML", 1, true)]
        [TestCase(@"MzML\TdyQc1_Fnl_All_25Oct17_Balzac-W1sta1_SRM.mzML", 4701, true)]
        [TestCase(@"MzML\Dionex_BSA_OTOT-08_Scans1-200.mzML", 1, false)]
        [TestCase(@"MzML\Dionex_BSA_OTOT-08_Scans1-200.mzML", 1, true)]
        [TestCase(@"MzML\Dionex_BSA_OTOT-08_Scans200-300.mzML", 1, false)]
        [TestCase(@"MzML\Dionex_BSA_OTOT-08_Scans200-300.mzML", 1, true)]
        public void ReadMzMLChromatogramsTestDelayedRandom(string inputFileRelativePath, int expectedChromatograms, bool immediateRandomAccess)
        {
            if (!TestPath.FindInputFile(inputFileRelativePath, out var sourceFile))
            {
                Console.WriteLine("File not found: " + inputFileRelativePath);
                return;
            }

            using (var reader = new SimpleMzMLReader(sourceFile.FullName, immediateRandomAccess, true))
            {
                if (!immediateRandomAccess)
                {
                    // For non-random access readers, the number of chromatograms (as reported by NumChromatograms)
                    // will not get populated until after all the spectra (if any) have been read
                    // Therefore, NumChromatograms should currently be zero since immediateRandomAccess was false
                    Assert.AreEqual(0, reader.NumChromatograms);

                    // You cannot use reader.TryMakeRandomAccessCapable() to make the chromatograms available
                    // Thus, exit this method
                    return;
                }

                Assert.AreEqual(expectedChromatograms, reader.NumChromatograms);
                var stepSize = Math.Max(1, (expectedChromatograms / 100));

                var chromatogramNumber = 1;
                while (chromatogramNumber <= expectedChromatograms)
                {
                    var chrom = reader.ReadChromatogram(chromatogramNumber);
                    Console.WriteLine("Chromatogram {0,2}, NativeID {1,-35} has {2:N0} data points", chrom.Index, chrom.Id, chrom.Intensities.Length);

                    chromatogramNumber += stepSize;
                }
            }
        }

        /// <summary>
        /// When using non-random access, iterate through chromatograms using reader.ReadAllChromatograms()
        /// Prior to doing so, all of the spectra must be read using reader.ReadAllSpectra()
        /// </summary>
        /// <param name="inputFileRelativePath"></param>
        /// <param name="expectedSpectra"></param>
        /// <param name="expectedChromatograms"></param>
        [Test]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML", 9293, 1)]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML.gz", 9293, 1)]
        [TestCase(@"MzML\Rush2_p14RR_62_26Oct17_Smeagol-WRUSHCol3_75x20a_SRM.mzML", 0, 967)]
        [TestCase(@"MzML\Rush2_p14RR_62_26Oct17_Smeagol-WRUSHCol3_75x20a_srmasspectra.mzML", 104278, 1)]
        [TestCase(@"MzML\TdyQc1_Fnl_All_25Oct17_Balzac-W1sta1_SRM.mzML", 0, 4701)]
        public void ReadMzMLChromatogramsTestNonRandom(string inputFileRelativePath, int expectedSpectra, int expectedChromatograms)
        {
            if (!TestPath.FindInputFile(inputFileRelativePath, out var sourceFile))
            {
                Console.WriteLine("File not found: " + inputFileRelativePath);
                return;
            }

            using (var reader = new SimpleMzMLReader(sourceFile.FullName, false, true))
            {
                Assert.AreEqual(expectedSpectra, reader.NumSpectra);
                if (expectedSpectra != 0)
                {
                    Assert.AreEqual(0, reader.NumChromatograms);
                }
                var specCount = 0;
                foreach (var spec in reader.ReadAllSpectra(true))
                {
                    if (specCount < 100 || specCount >= expectedSpectra - 100)
                    {
                        Console.WriteLine("Spectrum {0,4}, NativeID {1,-45} has {2:N0} data points", spec.ScanNumber, spec.NativeId, spec.Peaks.Length);
                    }
                    else if (specCount == 100)
                    {
                        Console.WriteLine("...");
                    }

                    specCount++;
                }
                Assert.AreEqual(expectedSpectra, specCount);
                Assert.AreEqual(expectedChromatograms, reader.NumChromatograms);

                var chromCount = 0;
                foreach (var chrom in reader.ReadAllChromatograms(false))
                {
                    if (chrom == null)
                    {
                        Console.WriteLine("ReadAllChromatograms returned null for chromatogram index {0}", chromCount);
                    }
                    else
                    {
                        Console.WriteLine("Chromatogram {0}, NativeID {1,-45} has {2:N0} data points", chrom.Index, chrom.Id, chrom.Intensities.Length);
                    }

                    chromCount++;
                }
                Assert.AreEqual(expectedChromatograms, reader.NumChromatograms);
                Assert.AreEqual(expectedChromatograms, chromCount);
            }
        }

        /// <summary>
        /// When using random access, we can immediately iterate through chromatograms using reader.ReadAllChromatograms()
        /// </summary>
        /// <param name="inputFileRelativePath"></param>
        /// <param name="expectedChromatograms"></param>
        [Test]
        [TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML", 1)]
        [TestCase(@"MzML\Rush2_p14RR_62_26Oct17_Smeagol-WRUSHCol3_75x20a_SRM.mzML", 967)]
        [TestCase(@"MzML\Rush2_p14RR_62_26Oct17_Smeagol-WRUSHCol3_75x20a_srmasspectra.mzML", 1)]
        [TestCase(@"MzML\TdyQc1_Fnl_All_25Oct17_Balzac-W1sta1_SRM.mzML", 4701)]
        [TestCase(@"MzML\Dionex_BSA_OTOT-08_Scans1-200.mzML", 1)]
        [TestCase(@"MzML\Dionex_BSA_OTOT-08_Scans200-300.mzML", 1)]
        //[TestCase(@"MzML\QC_Shew_16_01-15f_MPA_02redo_8Nov16_Tiger_16-02-14.mzML.gz", 9293)] // implemented, but decompresses first
        public void ReadMzMLChromatogramsTestRandom(string inputFileRelativePath, int expectedChromatograms)
        {
            if (!TestPath.FindInputFile(inputFileRelativePath, out var sourceFile))
            {
                Console.WriteLine("File not found: " + inputFileRelativePath);
                return;
            }

            using (var reader = new SimpleMzMLReader(sourceFile.FullName, true, true))
            {
                Assert.AreEqual(expectedChromatograms, reader.NumChromatograms);
                var chromCount = 0;
                foreach (var chrom in reader.ReadAllChromatograms(true))
                {
                    if (chrom == null)
                    {
                        Console.WriteLine("ReadAllChromatograms returned null for chromatogram index {0}", chromCount);
                    }
                    else
                    {
                        Console.WriteLine("Chromatogram {0}, NativeID {1,-45} has {2:N0} data points", chrom.Index, chrom.Id, chrom.Intensities.Length);
                    }

                    chromCount++;
                }
                Assert.AreEqual(expectedChromatograms, chromCount);
            }
        }
    }
}
