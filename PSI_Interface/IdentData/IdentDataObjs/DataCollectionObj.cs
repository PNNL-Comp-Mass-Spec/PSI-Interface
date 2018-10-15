using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML DataCollectionType
    /// </summary>
    /// <remarks>The collection of input and output data sets of the analyses.</remarks>
    public class DataCollectionObj : IdentDataInternalTypeAbstract, IEquatable<DataCollectionObj>
    {
        private AnalysisDataObj _analysisData;

        private InputsObj _inputs;

        /// <summary>
        ///     Constructor
        /// </summary>
        public DataCollectionObj()
        {
            _inputs = null;
            _analysisData = null;
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
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

        /// <remarks>min 1, max 1</remarks>
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

        /// <remarks>min 1, max 1</remarks>
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
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as DataCollectionObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(DataCollectionObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if (Equals(Inputs, other.Inputs) && Equals(AnalysisData, other.AnalysisData))
                return true;
            return false;
        }

        /// <summary>
        ///     Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Inputs != null ? Inputs.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (AnalysisData != null ? AnalysisData.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}