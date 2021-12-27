using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML DataCollectionType
    /// </summary>
    /// <remarks>The collection of input and output data sets of the analyses.</remarks>
    public class DataCollectionObj : IdentDataInternalTypeAbstract, IEquatable<DataCollectionObj>
    {
        private AnalysisDataObj _analysisData;

        private InputsObj _inputs;

        /// <summary>
        /// Constructor
        /// </summary>
        public DataCollectionObj()
        {
            _inputs = null;
            _analysisData = null;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="idata"></param>
        public DataCollectionObj(DataCollectionType dc, IdentDataObj idata)
            : base(idata)
        {
            _inputs = null;
            _analysisData = null;

            idata.DataCollection = this;

            if (dc.Inputs != null)
                _inputs = new InputsObj(dc.Inputs, IdentData);
            if (dc.AnalysisData != null)
                _analysisData = new AnalysisDataObj(dc.AnalysisData, IdentData);
        }

        /// <summary>min 1, max 1</summary>
        public InputsObj Inputs
        {
            get => _inputs;
            set
            {
                _inputs = value;
                if (_inputs != null)
                    _inputs.IdentData = IdentData;
            }
        }

        /// <summary>min 1, max 1</summary>
        public AnalysisDataObj AnalysisData
        {
            get => _analysisData;
            set
            {
                _analysisData = value;
                if (_analysisData != null)
                    _analysisData.IdentData = IdentData;
            }
        }

        internal void RebuildLists()
        {
            Inputs.RebuildLists();
            AnalysisData.RebuildLists();
        }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is DataCollectionObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(DataCollectionObj other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return Equals(Inputs, other.Inputs) && Equals(AnalysisData, other.AnalysisData);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Inputs?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (AnalysisData?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}
