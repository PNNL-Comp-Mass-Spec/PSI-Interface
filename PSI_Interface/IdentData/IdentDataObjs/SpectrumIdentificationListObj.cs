using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML SpectrumIdentificationListType
    /// </summary>
    /// <remarks>Represents the set of all search results from SpectrumIdentification.</remarks>
    /// <remarks>CVParams/UserParams: Scores or output parameters associated with the SpectrumIdentificationList.</remarks>
    public class SpectrumIdentificationListObj : ParamGroupObj, IIdentifiableType, IEquatable<SpectrumIdentificationListObj>
    {
        private IdentDataList<MeasureObj> _fragmentationTables;
        private long _numSequencesSearched;
        private IdentDataList<SpectrumIdentificationResultObj> _spectrumIdentificationResults;

        /// <summary>
        ///     Constructor
        /// </summary>
        public SpectrumIdentificationListObj()
        {
            Id = null;
            Name = null;
            _numSequencesSearched = -1;
            NumSequencesSearchedSpecified = false;

            FragmentationTables = new IdentDataList<MeasureObj>(1);
            SpectrumIdentificationResults = new IdentDataList<SpectrumIdentificationResultObj>(1);
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="sil"></param>
        /// <param name="idata"></param>
        public SpectrumIdentificationListObj(SpectrumIdentificationListType sil, IdentDataObj idata)
            : base(sil, idata)
        {
            Id = sil.id;
            Name = sil.name;
            _numSequencesSearched = sil.numSequencesSearched;
            NumSequencesSearchedSpecified = sil.numSequencesSearchedSpecified;

            FragmentationTables = new IdentDataList<MeasureObj>(1);
            SpectrumIdentificationResults = new IdentDataList<SpectrumIdentificationResultObj>(1);

            if ((sil.FragmentationTable != null) && (sil.FragmentationTable.Count > 0))
            {
                FragmentationTables.AddRange(sil.FragmentationTable, f => new MeasureObj(f, IdentData));
            }
            if ((sil.SpectrumIdentificationResult != null) && (sil.SpectrumIdentificationResult.Count > 0))
            {
                SpectrumIdentificationResults.AddRange(sil.SpectrumIdentificationResult, sir => new SpectrumIdentificationResultObj(sir, IdentData));
            }
        }

        /// <remarks>min 0, max 1</remarks>
        public IdentDataList<MeasureObj> FragmentationTables
        {
            get { return _fragmentationTables; }
            set
            {
                _fragmentationTables = value;
                if (_fragmentationTables != null)
                    _fragmentationTables.IdentData = IdentData;
            }
        }

        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<SpectrumIdentificationResultObj> SpectrumIdentificationResults
        {
            get { return _spectrumIdentificationResults; }
            set
            {
                _spectrumIdentificationResults = value;
                if (_spectrumIdentificationResults != null)
                    _spectrumIdentificationResults.IdentData = IdentData;
            }
        }

        /// <remarks>
        ///     The number of database sequences searched against. This value should be provided unless a de novo search has
        ///     been performed.
        /// </remarks>
        /// Optional Attribute
        /// long
        public long NumSequencesSearched
        {
            get { return _numSequencesSearched; }
            set
            {
                _numSequencesSearched = value;
                NumSequencesSearchedSpecified = true;
            }
        }

        /// Attribute Existence
        protected internal bool NumSequencesSearchedSpecified { get; private set; }

        /// <remarks>
        ///     An identifier is an unambiguous string that is unique within the scope
        ///     (i.e. a document, a set of related documents, or a repository) of its use.
        /// </remarks>
        /// Required Attribute
        /// string
        public string Id { get; set; }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        public string Name { get; set; }

        /// <summary>
        ///     Sort the result list by the best SpecEValue
        /// </summary>
        public void Sort()
        {
            foreach (var sir in SpectrumIdentificationResults)
                sir.Sort();
            SpectrumIdentificationResults.Sort((a, b) =>
                    a.BestSpecEVal().CompareTo(b.BestSpecEVal()));
        }

        #region Object Equality

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as SpectrumIdentificationListObj;
            if (o == null)
                return false;
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(SpectrumIdentificationListObj other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;

            if ((Name == other.Name) && (NumSequencesSearched == other.NumSequencesSearched) &&
                Equals(FragmentationTables, other.FragmentationTables) &&
                Equals(SpectrumIdentificationResults, other.SpectrumIdentificationResults) &&
                Equals(CVParams, other.CVParams) && Equals(UserParams, other.UserParams))
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
                var hashCode = Name != null ? Name.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ NumSequencesSearched.GetHashCode();
                hashCode = (hashCode * 397) ^ (FragmentationTables != null ? FragmentationTables.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SpectrumIdentificationResults != null ? SpectrumIdentificationResults.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}