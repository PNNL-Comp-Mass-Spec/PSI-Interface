using System.Collections.Generic;
using PSI_Interface.IdentData.IdentDataObjs;

namespace PSI_Interface.IdentData.mzIdentML
{
    /// <summary>
    /// Interface for an object that contains a list of CVParams
    /// </summary>
    public interface ICVParamGroup
    {
        /// <summary>
        /// List of CVParams
        /// </summary>
        List<CVParamType> cvParam { get; set; }
    }

    /// <summary>
    /// Interface for an object that contains a list of userParams
    /// </summary>
    public interface IParamGroup : ICVParamGroup
    {
        /// <summary>
        /// List of UserParams
        /// </summary>
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
