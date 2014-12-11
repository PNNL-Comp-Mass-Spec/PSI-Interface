using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_Generator
{
	public class Unimod_obo_Reader
	{
		// https://code.google.com/p/psi-pi/source/browse/trunk/cv/unimod.obo (Pretty)
		// https://psi-pi.googlecode.com/svn/trunk/cv/unimod.obo (Straight text)
		public OBO_File FileData;
		public const string Url = "https://psi-pi.googlecode.com/svn/trunk/cv/unimod.obo";

		public void Read()
		{
			var reader = new OBO_Reader();
			reader.Read(Url);
			FileData = reader.FileData;
		}
	}
}
