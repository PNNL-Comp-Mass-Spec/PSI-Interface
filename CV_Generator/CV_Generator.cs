using System;

namespace CV_Generator
{
    public static class CV_Generator
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var cvCreator = new CVWriter();
            //cvCreator.WriteSingleFile("CV_generated.cs");
            cvCreator.WriteFileSet("CV");
        }
    }
}
