using System;
using System.Collections.Generic;
using System.Linq;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML SpectrumIdentificationResultType
    /// </summary>
    /// <remarks>
    /// All identifications made from searching one spectrum. For PMF data, all peptide identifications
    /// will be listed underneath as SpectrumIdentificationItems. For MS/MS data, there will be ranked
    /// SpectrumIdentificationItems corresponding to possible different peptide IDs.
    /// </remarks>
    /// <remarks>
    /// CVParams/UserParams: Scores or parameters associated with the SpectrumIdentificationResult
    /// (i.e the set of SpectrumIdentificationItems derived from one spectrum) e.g. the number of peptide
    /// sequences within the parent tolerance for this spectrum.
    /// </remarks>
    public class SpectrumIdentificationResultObj : ParamGroupObj, IIdentifiableType, IEquatable<SpectrumIdentificationResultObj>
    {
        private SpectraDataObj _spectraData;
        private string _spectraDataRef;

        private IdentDataList<SpectrumIdentificationItemObj> _spectrumIdentificationItems;

        /// <summary>
        /// Constructor
        /// </summary>
        public SpectrumIdentificationResultObj()
        {
            Id = null;
            Name = null;
            SpectrumID = null;
            _spectraDataRef = null;

            _spectraData = null;
            SpectrumIdentificationItems = new IdentDataList<SpectrumIdentificationItemObj>(1);
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="sir"></param>
        /// <param name="idata"></param>
        public SpectrumIdentificationResultObj(SpectrumIdentificationResultType sir, IdentDataObj idata)
            : base(sir, idata)
        {
            Id = sir.id;
            Name = sir.name;
            SpectrumID = sir.spectrumID;
            SpectraDataRef = sir.spectraData_ref;

            SpectrumIdentificationItems = new IdentDataList<SpectrumIdentificationItemObj>(1);

            if (sir.SpectrumIdentificationItem?.Count > 0)
            {
                SpectrumIdentificationItems.AddRange(sir.SpectrumIdentificationItem, sii => new SpectrumIdentificationItemObj(sii, IdentData));
            }
        }

        /// <summary>min 1, max unbounded</summary>
        public IdentDataList<SpectrumIdentificationItemObj> SpectrumIdentificationItems
        {
            get => _spectrumIdentificationItems;
            set
            {
                _spectrumIdentificationItems = value;

                if (_spectrumIdentificationItems != null)
                    _spectrumIdentificationItems.IdentData = IdentData;
            }
        }

        /// <summary>
        /// The locally unique id for the spectrum in the spectra data set specified by SpectraData_ref.
        /// External guidelines are provided on the use of consistent identifiers for spectra in different external formats.
        /// </summary>
        /// <remarks>Required Attribute</remarks>
        public string SpectrumID { get; set; }

        /// <summary>A reference to a spectra data set (e.g. a spectra file).</summary>
        /// <remarks>Required Attribute</remarks>
        protected internal string SpectraDataRef
        {
            get
            {
                if (_spectraData != null)
                    return _spectraData.Id;
                return _spectraDataRef;
            }
            set
            {
                _spectraDataRef = value;

                if (!string.IsNullOrWhiteSpace(value))
                    SpectraData = IdentData.FindSpectraData(value);
            }
        }

        /// <summary>A reference to a spectra data set (e.g. a spectra file).</summary>
        /// <remarks>Required Attribute</remarks>
        public SpectraDataObj SpectraData
        {
            get => _spectraData;
            set
            {
                _spectraData = value;

                if (_spectraData != null)
                {
                    _spectraData.IdentData = IdentData;
                    _spectraDataRef = _spectraData.Id;
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

        /// <summary>
        /// Sort the SpectrumIdentificationItems by rank, ascending
        /// </summary>
        public void Sort()
        {
            SpectrumIdentificationItems.Sort((a, b) => a.Rank.CompareTo(b.Rank));
        }

        /// <summary>
        /// Determine the lowest specEValue in the SpectrumIdentificationItems
        /// </summary>
        public double BestSpecEVal()
        {
            return SpectrumIdentificationItems.Min(x => x.GetSpecEValue());
        }

        /// <summary>
        /// Changes the rank so that items are ranked by specEValue, ascending
        /// Also re-writes the ids
        /// </summary>
        public void ReRankBySpecEValue()
        {
            var siiIdBase = Id.ToUpper().Replace("SIR", "SII") + "_";
            var idCount = SpectrumIdentificationItems.Count;

            if (idCount > 1)
            {
                SpectrumIdentificationItems.Sort((a, b) => a.GetSpecEValue().CompareTo(b.GetSpecEValue()));
            }

            for (var i = 0; i < idCount; i++)
            {
                var rank = i + 1;
                SpectrumIdentificationItems[i].Rank = rank;
                SpectrumIdentificationItems[i].Id = siiIdBase + rank;
            }

            if (idCount < 2)
                return;

            Sort();
        }

        /// <summary>
        /// Remove all SpectrumIdentificationItems that have a specEValue greater than the best specEValue in the list.
        /// </summary>
        public void RemoveMatchesNotBestSpecEValue()
        {
            if (SpectrumIdentificationItems.Count < 2)
                return;

            // Previously, we used .RemoveAll() to remove all spectrum ID items that had a higher SpecEValue than the "best" SpecEValue
            // This had the side effect of losing parent protein references, which is less than ideal
            // Instead, when looking for IDs to remove, do not remove items that are the same peptide, since we want to keep track of the parent sequences (and thus proteins) for each peptide

            // This was in use before February 2025
            // var best = BestSpecEVal();
            // SpectrumIdentificationItems.RemoveAll(item => item.GetSpecEValue() > bestEVal);

            var specIDsByEValue = SpectrumIDsBySpecEValue();
            var bestEVal = specIDsByEValue.First().Key;

            var highestScoringItems = specIDsByEValue.First().Value;

            // When removing the other IDs, do not remove items that are the same peptide, since we want to keep track of the parent sequences (and thus proteins) for all of the peptides
            // If they are the same peptide, then we need to merge the SpectrumIdentificationItems, which really only requires copying the PeptideEvidenceRef to the better-scoring SpectrumIdentificationItem

            // Use .ToList() to create a distinct list, and allow modification of the original
            foreach (var specId in SpectrumIdentificationItems.ToList())
            {
                if (specId.GetSpecEValue() <= bestEVal)
                    continue;

                var matchedItem = highestScoringItems.FirstOrDefault(comparisonItem => comparisonItem.Peptide.Equals(specId.Peptide));

                if (matchedItem != null)
                {
                    // Matched an existing peptide match; copy the PeptideEvidenceRefs to the higher-scoring peptide match
                    matchedItem.PeptideEvidences.AddRange(specId.PeptideEvidences);
                }

                // Remove the item from the list
                SpectrumIdentificationItems.Remove(specId);
            }
        }

        /// <summary>
        /// Return a dictionary that maps specEValues to spectrum ID
        /// </summary>
        public SortedDictionary<double, List<SpectrumIdentificationItemObj>> SpectrumIDsBySpecEValue()
        {
            var specIDsByEValue = new SortedDictionary<double, List<SpectrumIdentificationItemObj>>();

            foreach (var specID in SpectrumIdentificationItems)
            {
                var eValue = specID.GetSpecEValue();

                if (specIDsByEValue.TryGetValue(eValue, out var matchingSpecId))
                {
                    matchingSpecId.Add(specID);
                }
                else
                {
                    specIDsByEValue.Add(eValue, new List<SpectrumIdentificationItemObj> { specID });
                }
            }

            return specIDsByEValue;
        }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is SpectrumIdentificationResultObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(SpectrumIdentificationResultObj other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return Name == other.Name && SpectrumID == other.SpectrumID &&
                   Equals(SpectrumIdentificationItems, other.SpectrumIdentificationItems) &&
                   Equals(SpectraData, other.SpectraData) && ParamsEquals(other);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (SpectrumID?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (SpectrumIdentificationItems?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (SpectraData?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (CVParams?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (UserParams?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}
