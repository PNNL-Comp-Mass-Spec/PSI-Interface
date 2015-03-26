using System.Collections.Generic;
using PSI_Interface.MSData;

namespace PSI_Interface.CV
{
    public class CVTranslator
    {
        private readonly Dictionary<string, string> _fileToObo = new Dictionary<string, string>(); 
        private readonly Dictionary<string, string> _oboToFile = new Dictionary<string, string>();

        public CVTranslator()
        {
            foreach (var cv in CV.CVInfoList)
            {
                _oboToFile.Add(cv.Id, cv.Id);
                _fileToObo.Add(cv.Id, cv.Id);
            }
        }

        public CVTranslator(List<CV.CVInfo> fileCvInfo)
        {
            foreach (var cv in CV.CVInfoList)
            {
                foreach (var fcv in fileCvInfo)
                {
                    var cvFilename = cv.URI.Substring(cv.URI.LastIndexOf("/") + 1);
                    var fcvFilename = fcv.URI.Substring(fcv.URI.LastIndexOf("/") + 1);
                    if (cvFilename.ToLower().Equals(fcvFilename.ToLower()))
                    {
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

        public CVTranslator(CVType[] fileCvInfo)
        {
            foreach (var cv in CV.CVInfoList)
            {
                foreach (var fcv in fileCvInfo)
                {
                    var cvFilename = cv.URI.Substring(cv.URI.LastIndexOf("/") + 1);
                    var fcvFilename = fcv.URI.Substring(fcv.URI.LastIndexOf("/") + 1);
                    if (cvFilename.ToLower().Equals(fcvFilename.ToLower()))
                    {
                        _oboToFile.Add(cv.Id, fcv.id);
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

        public string ConvertFileAccession(string accession)
        {
            return ConvertAccession(accession, _fileToObo);
        }

        public string ConvertOboAccession(string accession)
        {
            return ConvertAccession(accession, _oboToFile);
        }

        private string ConvertAccession(string accession, Dictionary<string, string> map)
        {
            string[] parts = accession.Split(new[] {':'});
            if (map.ContainsKey(parts[0]))
            {
                return map[parts[0]] + ":" + parts[1];
            }
            return null;
        }
    }
}
