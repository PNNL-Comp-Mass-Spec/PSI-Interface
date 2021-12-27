using System;
using System.Collections.Generic;
using System.Linq;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML IonTypeType: list form is FragmentationType
    /// </summary>
    /// <remarks>
    /// IonType defines the index of fragmentation ions being reported, importing a CV term for the type of ion e.g. b ion.
    /// Example: if b3 b7 b8 and b10 have been identified, the index attribute will contain 3 7 8 10, and the corresponding
    /// values
    /// will be reported in parallel arrays below
    /// </remarks>
    /// <remarks>FragmentationType: The product ions identified in this result.</remarks>
    /// <remarks>FragmentationType: child element IonType, of type IonTypeType, min 1, max unbounded</remarks>
    public class IonTypeObj : IdentDataInternalTypeAbstract, IEquatable<IonTypeObj>
    {
        private IdentDataList<CVParamObj> _cvParams;
        private IdentDataList<UserParamObj> _userParams;

        private IdentDataList<FragmentArrayObj> _fragmentArrays;

        /// <summary>
        /// Constructor
        /// </summary>
        public IonTypeObj()
        {
            Charge = 0;

            FragmentArrays = new IdentDataList<FragmentArrayObj>(1);
            CVParams = new IdentDataList<CVParamObj>(1);
            UserParams = new IdentDataList<UserParamObj>(1);
            Index = new List<string>();
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="it"></param>
        /// <param name="idata"></param>
        public IonTypeObj(IonTypeType it, IdentDataObj idata)
            : base(idata)
        {
            Charge = it.charge;

            FragmentArrays = new IdentDataList<FragmentArrayObj>(1);
            CVParams = new IdentDataList<CVParamObj>(1);
            UserParams = new IdentDataList<UserParamObj>(1);
            Index = new List<string>();

            if (it.FragmentArray?.Count > 0)
            {
                FragmentArrays.AddRange(it.FragmentArray, f => new FragmentArrayObj(f, IdentData));
            }

            if (it.cvParam?.Count > 0)
            {
                CVParams.AddRange(it.cvParam.Select(cvp => new CVParamObj(cvp, idata)));
            }

            if (it.userParam?.Count > 0)
            {
                UserParams.AddRange(it.userParam.Select(up => new UserParamObj(up, idata)));
            }
        }

        /// <summary>min 0, max unbounded</summary>
        public IdentDataList<FragmentArrayObj> FragmentArrays
        {
            get => _fragmentArrays;
            set
            {
                _fragmentArrays = value;
                if (_fragmentArrays != null)
                {
                    _fragmentArrays.IdentData = IdentData;
                }
            }
        }
        /// <summary>
        /// mzIdentML 1.2 addition: In case more information about the ions annotation has to be conveyed, that has no fit in FragmentArray.
        /// Note: It is suggested that the value attribute takes the form of a list of the same size as FragmentArray values.
        /// However, there is no formal encoding and it cannot be expected that other software will process or impart that information properly.
        /// </summary>
        /// <remarks>min 0, max n (mzIdentML 1.2)</remarks>
        public IdentDataList<UserParamObj> UserParams
        {
            get => _userParams;
            set
            {
                _userParams = value;
                if (_userParams != null)
                {
                    _userParams.IdentData = IdentData;
                }
            }
        }

        /// <summary>The type of ion identified.</summary>
        /// <remarks>
        /// (mzIdentML 1.2 add) In the case of neutral losses, one term should report the ion type, a second term should report the neutral loss;
        /// Note: this is a change in practice from mzIdentML 1.1.
        /// </remarks>
        /// <remarks>min 1, max 1 (mzIdentML 1.1)</remarks>
        /// <remarks>min 1, max n (mzIdentML 1.2)</remarks>
        public IdentDataList<CVParamObj> CVParams
        {
            get => _cvParams;
            set
            {
                _cvParams = value;
                if (_cvParams != null)
                {
                    _cvParams.IdentData = IdentData;
                }
            }
        }

        /// <summary>
        /// <para>
        /// The index of ions identified as integers, following standard notation for a-c, x-z e.g. if b3 b5 and b6 have
        /// been identified, the index would store "3 5 6". For internal ions, the index contains pairs defining the start and
        /// end point - see specification document for examples. For immonium ions, the index is the position of the identified
        /// ion within the peptide sequence - if the peptide contains the same amino acid in multiple positions that cannot be
        /// distinguished, all positions should be given.
        /// </para>
        /// <para>
        /// (mzIdentML 1.2 add) For precursor ions, including neutral losses, the index value MUST be 0.
        /// For any other ions not related to the position within the peptide sequence
        /// e.g. quantification reporter ions, the index value MUST be 0.
        /// </para>
        /// </summary>
        /// <remarks>Optional Attribute</remarks>
        public List<string> Index { get; set; }

        /// <summary>The charge of the identified fragmentation ions.</summary>
        /// <remarks>Required Attribute</remarks>
        public int Charge { get; set; }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is IonTypeObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(IonTypeObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other == null)
            {
                return false;
            }

            return Charge == other.Charge && Equals(FragmentArrays, other.FragmentArrays) &&
                   Equals(CVParams, other.CVParams) && Equals(UserParams, other.UserParams) && Equals(Index, other.Index);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Charge;
                hashCode = (hashCode * 397) ^ (FragmentArrays?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Index?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (CVParams?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (UserParams?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}
