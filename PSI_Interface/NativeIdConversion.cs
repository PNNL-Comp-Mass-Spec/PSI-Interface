using System;
using System.Collections.Generic;

namespace PSI_Interface
{
    /// <summary>
    /// Provides functionality to interpret a NativeID as a integer scan number
    /// Code is ported from MSData.cpp in ProteoWizard
    /// </summary>
    public static class NativeIdConversion
    {
        private static Dictionary<string, string> ParseNativeId(string nativeId)
        {
            var tokens = nativeId.Split(new[] { '\t', ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var map = new Dictionary<string, string>();
            foreach (var token in tokens)
            {
                var equals = token.IndexOf('=');
                var name = token.Substring(0, equals);
                var value = token.Substring(equals + 1);
                map.Add(name, value);
            }
            return map;
        }

        /// <summary>
        /// Performs a "long.TryParse" on the interpreted scan number (single shot function)
        /// </summary>
        /// <param name="nativeId"></param>
        /// <param name="num"></param>
        public static bool TryGetScanNumberLong(string nativeId, out long num)
        {
            return long.TryParse(GetScanNumber(nativeId), out num);
        }

        /// <summary>
        /// Performs a "int.TryParse" on the interpreted scan number (single shot function)
        /// </summary>
        /// <param name="nativeId"></param>
        /// <param name="num"></param>
        public static bool TryGetScanNumberInt(string nativeId, out int num)
        {
            return int.TryParse(GetScanNumber(nativeId), out num);
        }

        /// <summary>
        /// Returns the integer-only portion of the nativeID that can be used for a scan number
        /// If the nativeID cannot be interpreted, the original value is returned.
        /// </summary>
        /// <param name="nativeId"></param>
        public static string GetScanNumber(string nativeId)
        {
            // TODO: Add interpreter for Waters' S0F1, S1F1, S0F2,... format
            //switch (nativeIdFormat)
            //{
            //    case MS_spectrum_identifier_nativeID_format: // mzData
            //        return value(id, "spectrum");
            //
            //    case MS_multiple_peak_list_nativeID_format: // MGF
            //        return value(id, "index");
            //
            //    case MS_Agilent_MassHunter_nativeID_format:
            //        return value(id, "scanId");
            //
            //    case MS_Thermo_nativeID_format:
            //        // conversion from Thermo nativeIDs assumes default controller information
            //        if (id.find("controllerType=0 controllerNumber=1") != 0)
            //            return "";
            //
            //        // fall through to get scan
            //
            //    case MS_Bruker_Agilent_YEP_nativeID_format:
            //    case MS_Bruker_BAF_nativeID_format:
            //    case MS_scan_number_only_nativeID_format:
            //        return value(id, "scan");
            //
            //    default:
            //        if (bal::starts_with(id, "scan=")) return value(id, "scan");
            //        else if (bal::starts_with(id, "index=")) return value(id, "index");
            //        return "";
            //}
            if (nativeId.Contains("="))
            {
                var map = ParseNativeId(nativeId);
                if (map.ContainsKey("spectrum"))
                {
                    return map["spectrum"];
                }
                if (map.ContainsKey("index"))
                {
                    return map["index"];
                }
                if (map.ContainsKey("scanId"))
                {
                    return map["scanId"];
                }
                if (map.ContainsKey("scan"))
                {
                    return map["scan"];
                }
            }

            // No equals sign, don't have parser breakdown
            // Or key data not found in breakdown of nativeId
            return nativeId;
        }

        //public static string GetNativeId(string scanNumber)
        //{
        //    switch (nativeIdFormat)
        //    {
        //        case MS_Thermo_nativeID_format:
        //            return "controllerType=0 controllerNumber=1 scan=" + scanNumber;
        //
        //        case MS_spectrum_identifier_nativeID_format:
        //            return "spectrum=" + scanNumber;
        //
        //        case MS_multiple_peak_list_nativeID_format:
        //            return "index=" + scanNumber;
        //
        //        case MS_Agilent_MassHunter_nativeID_format:
        //            return "scanId=" + scanNumber;
        //
        //        case MS_Bruker_Agilent_YEP_nativeID_format:
        //        case MS_Bruker_BAF_nativeID_format:
        //        case MS_scan_number_only_nativeID_format:
        //            return "scan=" + scanNumber;
        //
        //        default:
        //            return "";
        //    }
        //}
    }
}
