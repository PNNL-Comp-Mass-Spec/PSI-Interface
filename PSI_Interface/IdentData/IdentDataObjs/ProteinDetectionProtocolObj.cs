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

        /// <summary>The parameters and settings for the protein detection given as CV terms.</summary>
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

        /// <summary>
        /// The threshold(s) applied to determine that a result is significant.
        /// If multiple terms are used it is assumed that all conditions are satisfied by the passing results.
        /// </summary>
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

        /// <summary>The protein detection software used, given as a reference to the SoftwareCollection section.</summary>
        /// <remarks>Required Attribute</remarks>
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

        /// <summary>The protein detection software used, given as a reference to the SoftwareCollection section.</summary>
        /// <remarks>Required Attribute</remarks>
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

        /// <summary>
        /// An identifier is an unambiguous string that is unique within the scope
        /// (i.e. a document, a set of related documents, or a repository) of its use.
        /// </summary>
        /// <remarks>Required Attribute</remarks>
        public string Id { get; set; }

        /// <summary>The potentially ambiguous common identifier, such as a human-readable name for the instance.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Name { get; set; }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is ProteinDetectionProtocolObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(ProteinDetectionProtocolObj other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return Name == other.Name && Equals(AnalysisSoftware, other.AnalysisSoftware) &&
                   Equals(AnalysisParams, other.AnalysisParams) && Equals(Threshold, other.Threshold);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (AnalysisSoftware?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (AnalysisParams?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Threshold?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}
