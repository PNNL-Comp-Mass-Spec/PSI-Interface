using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI_Interface.MSData.mzML
{
    /// <summary>
    /// Supported schema formats for MzML
    /// </summary>
    public enum MzMLSchemaType : byte
    {
        /// <summary>
        /// MzML schema
        /// </summary>
        MzML,
        /// <summary>
        /// Indexed MzML wrapper schema
        /// </summary>
        IndexedMzML,
    }
}
