using CV_Generator.OBO_Objects;

namespace CV_Generator
{
    public class Unimod_obo_Reader
    {
        // http://www.unimod.org/obo/unimod.obo
        public OBO_File FileData;
        public const string Url = "http://www.unimod.org/obo/unimod.obo";

        public void Read()
        {
            var reader = new OBO_Reader();
            reader.Read(Url);
            FileData = reader.FileData;
        }
    }
}
