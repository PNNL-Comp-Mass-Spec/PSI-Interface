using System;

namespace CV_Generator
{
    public static class CV_Generator
    {
        [STAThread]
        public static void Main()
        {
            var cvCreator = new CVWriter();
            Console.WriteLine();
            cvCreator.WriteFileSet("CV");
            Console.WriteLine();
            Console.WriteLine("Processing complete");
            System.Threading.Thread.Sleep(1500);
        }
    }
}
