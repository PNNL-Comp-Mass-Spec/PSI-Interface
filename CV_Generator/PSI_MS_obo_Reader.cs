using System.Collections.Generic;

namespace CV_Generator
{
    public class PSI_MS_obo_Reader
    {
        // http://psidev.cvs.sourceforge.net/viewvc/psidev/psi/psi-ms/mzML/controlledVocabulary/psi-ms.obo
        public OBO_File FileData;
        public List<OBO_File> ImportedFileData = new List<OBO_File>();
        public const string Url = "http://psidev.cvs.sourceforge.net/viewvc/psidev/psi/psi-ms/mzML/controlledVocabulary/psi-ms.obo";

        public void Read()
        {
            var reader = new OBO_Reader();
            reader.Read(Url);
            FileData = reader.FileData;
            ImportedFileData = reader.ImportedFileData;
        }
    }
}
