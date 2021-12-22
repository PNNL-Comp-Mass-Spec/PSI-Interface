using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData
{
    /// <summary>
    /// Class for reading/writing an MzIdentML file with IdentDataObj group objects
    /// </summary>
    public static class IdentDataReaderWriter
    {
        /// <summary>
        /// Read the mzid file into IdentData class objects
        /// </summary>
        public static IdentDataObj Read(string filePath, int bufferSize = 65536)
        {
            return new IdentDataObj(MzIdentMlReaderWriter.Read(filePath, bufferSize));
        }

        /// <summary>
        /// Write the provided data to the file
        /// </summary>
        /// <param name="identData"></param>
        /// <param name="filePath">Path to file to be written, with extension of .mzid[.gz]</param>
        /// <param name="bufferSize">File stream buffer size</param>
        public static void Write(IdentDataObj identData, string filePath, int bufferSize = 65536)
        {
            MzIdentMlReaderWriter.Write(new MzIdentMLType(identData), filePath, bufferSize);
        }
    }
}
