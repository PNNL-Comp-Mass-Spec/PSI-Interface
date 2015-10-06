using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PSI_Interface.IdentData.mzIdentML
{
    public interface ICVParamGroup
    {
        List<CVParamType> cvParam { get; set; }
    }

    public interface IParamGroup : ICVParamGroup
    {
        List<UserParamType> userParam { get; set; }
    }

    internal static class ParamGroupFunctions
    {
        public static void CopyParamGroup(IParamGroup target, ParamGroupObj source)
        {
            CopyCVParamGroup(target, source);
            target.userParam = null;
            if (source.UserParams != null)
            {
                target.userParam = new List<UserParamType>();
                foreach (var up in source.UserParams)
                {
                    target.userParam.Add(new UserParamType(up));
                }
            }
        }

        public static void CopyCVParamGroup(ICVParamGroup target, CVParamGroupObj source)
        {
            target.cvParam = null;
            if (source.CVParams != null)
            {
                target.cvParam = new List<CVParamType>();
                foreach (var cvp in source.CVParams)
                {
                    target.cvParam.Add(new CVParamType(cvp));
                }
            }
            
        }
    }
}
