using System;
using System.Collections.Generic;
using System.Linq;
using PSI_Interface.MSData;
using PSI_Interface.SharedInterfaces;

namespace PSI_Interface.CV
{
    /// <summary>
    /// Interfaces between the CVRef values used in a file and the CVRef values used internally, for the cases when they don't match
    /// </summary>
    public class CVTranslator
    {
        private readonly Dictionary<string, string> _fileToObo = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _oboToFile = new Dictionary<string, string>();

        /// <summary>
        /// Create a default translator - the values used in the file will match the internal values
        /// </summary>
        public CVTranslator()
        {
            foreach (var cv in CV.CVInfoList)
            {
                _oboToFile.Add(cv.Id, cv.Id);
                _fileToObo.Add(cv.Id, cv.Id);
            }
        }

        /// <summary>
        /// Create a translator between a CVInfo object and the internal values
        /// </summary>
        /// <param name="fileCvInfo"></param>
        public CVTranslator(IEnumerable<CV.CVInfo> fileCvInfo) : this(fileCvInfo.Cast<ICVInfo>())
        { }

        /// <summary>
        /// Create a translator between a mzid file and the internal values
        /// </summary>
        /// <param name="fileCvInfo"></param>
        public CVTranslator(IEnumerable<IdentData.IdentDataObjs.CVInfo> fileCvInfo) : this(fileCvInfo.Cast<ICVInfo>())
        { }

        /// <summary>
        /// Create a translator between a mzML file and the internal values
        /// </summary>
        /// <param name="fileCvInfo"></param>
        public CVTranslator(IEnumerable<CVInfo> fileCvInfo) : this(fileCvInfo.Cast<ICVInfo>())
        { }

        /// <summary>
        /// Create a translator between a file type and the internal values
        /// </summary>
        /// <param name="fileCvInfo"></param>
        private CVTranslator(IEnumerable<ICVInfo> fileCvInfo)
        {
            var cvInfos = fileCvInfo.ToList();
            foreach (var cv in CV.CVInfoList)
            {
                foreach (var fcv in cvInfos)
                {
                    var cvFilename = cv.URI.Substring(cv.URI.LastIndexOf("/", StringComparison.Ordinal) + 1);
                    var fcvFilename = fcv.URI.Substring(fcv.URI.LastIndexOf("/", StringComparison.Ordinal) + 1);
                    if (cvFilename.Equals(fcvFilename, StringComparison.OrdinalIgnoreCase) && !_oboToFile.ContainsValue(fcv.Id))
                    {
                        if (cv.Id.Equals("PEFF", StringComparison.OrdinalIgnoreCase) ^ fcv.Id.IndexOf("PEFF", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            // XOR: if only one or the other is PEFF, don't add it here since it is probably going to mess up the main MS cv namespace
                            continue;
                        }

                        if (cv.Id.Equals("NCIT", StringComparison.OrdinalIgnoreCase) ^ fcv.Id.IndexOf("NCIT", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            // XOR: if only one or the other is NCIT, don't add it here since it is probably going to mess up the main MS cv namespace
                            continue;
                        }

                        _oboToFile.Add(cv.Id, fcv.Id);
                    }
                }
                if (!_oboToFile.ContainsKey(cv.Id))
                {
                    _oboToFile.Add(cv.Id, cv.Id);
                }
            }

            foreach (var mapping in _oboToFile)
            {
                _fileToObo.Add(mapping.Value, mapping.Key);
            }
        }

        /// <summary>
        /// Convert a file cvRef to the internally used cvRef
        /// </summary>
        /// <param name="cvRef"></param>
        public string ConvertFileCVRef(string cvRef)
        {
            return ConvertCVRef(cvRef, _fileToObo);
        }

        /// <summary>
        /// Convert an internal cvRef to the cvRef that should be used in the file
        /// </summary>
        /// <param name="cvRef"></param>
        public string ConvertOboCVRef(string cvRef)
        {
            var newCvRef = ConvertCVRef(cvRef, _oboToFile);
            if (string.IsNullOrWhiteSpace(newCvRef))
            {
                return null;
            }

            return newCvRef;
        }

        private static string ConvertCVRef(string cvRef, IReadOnlyDictionary<string, string> map)
        {
            if (string.IsNullOrWhiteSpace(cvRef))
            {
                return "";
            }
            if (map.ContainsKey(cvRef))
            {
                return map[cvRef];
            }
            return "";
        }
}
}
