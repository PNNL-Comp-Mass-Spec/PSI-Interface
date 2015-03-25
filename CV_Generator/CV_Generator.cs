using System;

namespace CV_Generator
{
	public class CV_Generator
	{
        [STAThread]
	    public static void Main(string[] args)
        {
            var cvCreator = new CVWriter();
            cvCreator.WriteFile("CV_generated.cs");
        }
	}
}
