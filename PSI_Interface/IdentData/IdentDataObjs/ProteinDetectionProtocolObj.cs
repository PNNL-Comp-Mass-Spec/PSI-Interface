using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML ProteinDetectionProtocolType
    /// </summary>
    /// <remarks>The parameters and settings of a ProteinDetection process.</remarks>
    public class ProteinDetectionProtocolObj : IdentDataInternalTypeAbstract, IIdentifiableType, IEquatable<ProteinDetectionProtocolObj>
    {
        private ParamListObj _analysisParams;
        private AnalysisSoftwareObj _analysisSoftware;
        private string _analysisSoftwareRef;
        private ParamListObj _threshold;

        /// <summary>
        /// Constructor
        /// </summary>
        public ProteinDetectionProtocolObj()
        {
            Id = null;
            Name = null;
            _analysisSoftwareRef = null;

            _analysisSoftware = null;
            _analysisParams = null;
            _threshold = null;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="pdp"></param>
        /// <param name="idata"></param>
        public ProteinDetectionProtocolObj(ProteinDetectionProtocolType pdp, IdentDataObj idata)
            : base(idata)
        {
            Id = pdp.id;
            Name = pdp.name;
            AnalysisSoftwareRef = pdp.analysisSoftware_ref;

            _analysisParams = null;
            _threshold = null;

            if (pdp.AnalysisParams != null)
                _analysisParams = new ParamListObj(pdp.AnalysisParams, IdentData);
            if (pdp.Threshold != null)
                _threshold = new ParamListObj(pdp.Threshold, IdentData);
        }

        /// <remarks>The parameters and settings for the protein detection given as CV terms.</remarks>
        /// <remarks>min 0, max 1</remarks>
        public ParamListObj AnalysisParams
        {
            get => _analysisParams;
            set
            {
                _analysisParams = value;
                if (_analysisParams != null)
                    _analysisParams.IdentData = IdentData;
            }
        }

        /// <remarks>
        /// The threshold(s) applied to determine that a result is significant.
        /// If multiple terms are used it is assumed that all conditions are satisfied by the passing results.
        /// </remarks>
        /// <remarks>min 1, max 1</remarks>
        public ParamListObj Threshold
        {
            get => _threshold;
            set
            {
                _threshold = value;
                if (_threshold != null)
                    _threshold.IdentData = IdentData;
            }
        }

        /// <remarks>The protein detection software used, given as a reference to the SoftwareCollection section.</remarks>
        /// Required Attribute
        /// string
        protected internal string AnalysisSoftwareRef
        {
            get
            {
                if (_analysisSoftware != null)
                    return _analysisSoftware.Id;
                return _analysisSoftwareRef;
            }
            set
            {
                _analysisSoftwareRef = value;
                if (!string.IsNullOrWhiteSpace(value))
                    AnalysisSoftware = IdentData.FindAnalysisSoftware(value);
            }
        }

        /// <remarks>The protein detection software used, given as a reference to the SoftwareCollection section.</remarks>
        /// Required Attribute
        /// string
        public AnalysisSoftwareObj AnalysisSoftware
        {
            get => _analysisSoftware;
            set
            {
                _analysisSoftware = value;
                if (_analysisSoftware != null)
                {
                    _analysisSoftware.IdentData = IdentData;
                    _analysisSoftwareRef = _analysisSoftware.Id;
                }
            }
        }

        /// <remarks>
        /// An identifier is an unambiguous string that is unique within the scope
        /// (i.e. a document, a set of related documents, or a repository) of its use.
        /// </remarks>
        /// Required Attribute
        /// string
        public string Id { get; set; }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name { get; set; }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as ProteinDetectionProtocolObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ProteinDetectionProtocolObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if ((Name == other.Name) && Equals(AnalysisSoftware, other.AnalysisSoftware) &&
                Equals(AnalysisParams, other.AnalysisParams) && Equals(Threshold, other.Threshold))
                return true;
            return false;
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name != null ? Name.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (AnalysisSoftware != null ? AnalysisSoftware.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AnalysisParams != null ? AnalysisParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Threshold != null ? Threshold.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}