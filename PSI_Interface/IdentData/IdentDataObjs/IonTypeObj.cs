using System;
using System.Collections.Generic;
using System.Linq;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    ///     MzIdentML IonTypeType: list form is FragmentationType
    /// </summary>
    /// <remarks>
    ///     IonType defines the index of fragmentation ions being reported, importing a CV term for the type of ion e.g. b ion.
    ///     Example: if b3 b7 b8 and b10 have been identified, the index attribute will contain 3 7 8 10, and the corresponding
    ///     values
    ///     will be reported in parallel arrays below
    /// </remarks>
    /// <remarks>FragmentationType: The product ions identified in this result.</remarks>
    /// <remarks>FragmentationType: child element IonType, of type IonTypeType, min 1, max unbounded</remarks>
    public class IonTypeObj : IdentDataInternalTypeAbstract, IEquatable<IonTypeObj>
    {
        private IdentDataList<CVParamObj> _cvParams;
        private IdentDataList<UserParamObj> _userParams;

        private IdentDataList<FragmentArrayObj> _fragmentArrays;

        /// <summary>
        ///     Constructor
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
        ///     Create an object using the contents of the corresponding MzIdentML object
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

            if (it.FragmentArray != null && it.FragmentArray.Count > 0)
            {
                FragmentArrays.AddRange(it.FragmentArray, f => new FragmentArrayObj(f, IdentData));
            }

            if (it.cvParam != null && it.cvParam.Count > 0)
            {
                CVParams.AddRange(it.cvParam.Select(cvp => new CVParamObj(cvp, idata)));
            }

            if (it.userParam != null && it.userParam.Count > 0)
            {
                UserParams.AddRange(it.userParam.Select(up => new UserParamObj(up, idata)));
            }
        }

        /// <remarks>min 0, max unbounded</remarks>
        public IdentDataList<FragmentArrayObj> FragmentArrays
        {
            get { return _fragmentArrays; }
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
        /// mzIdentML 1.2 addition: In case more information about the ions annotation has to be conveyed, that has no fit in FragmentArray. Note: It is suggested that the value attribute takes the form of a list of the same size as FragmentArray values. However, there is no formal encoding and it cannot be expeceted that other software will process or impart that information properly.
        /// </summary>
        /// <remarks>min 0, max n (mzIdentML 1.2)</remarks>
        public IdentDataList<UserParamObj> UserParams
        {
            get { return _userParams; }
            set
            {
                _userParams = value;
                if (_userParams != null)
                {
                    _userParams.IdentData = IdentData;
                }
            }
        }

        /// <remarks>The type of ion identified.</remarks>
        /// <remarks>(mzIdentML 1.2 add) In the case of neutral losses, one term should report the ion type, a second term should report the neutral loss - note: this is a change in practice from mzIdentML 1.1.</remarks>
        /// <remarks>min 1, max 1 (mzIdentML 1.1)</remarks>
        /// <remarks>min 1, max n (mzIdentML 1.2)</remarks>
        public IdentDataList<CVParamObj> CVParams
        {
            get { return _cvParams; }
            set
            {
                _cvParams = value;
                if (_cvParams != null)
                {
                    _cvParams.IdentData = IdentData;
                }
            }
        }

        /// <remarks>
        ///     The index of ions identified as integers, following standard notation for a-c, x-z e.g. if b3 b5 and b6 have
        ///     been identified, the index would store "3 5 6". For internal ions, the index contains pairs defining the start and
        ///     end point - see specification document for examples. For immonium ions, the index is the position of the identified
        ///     ion within the peptide sequence - if the peptide contains the same amino acid in multiple positions that cannot be
        ///     distinguished, all positions should be given.
        /// </remarks>
        /// <remarks>(mzIdentML 1.2 add) For precursor ions, including neutral losses, the index value MUST be 0. For any other ions not related to the position within the peptide sequence e.g. quantification reporter ions, the index value MUST be 0.</remarks>
        /// Optional Attribute
        /// listOfIntegers: string, space-separated integers
        public List<string> Index { get; set; }

        /// <remarks>The charge of the identified fragmentation ions.</remarks>
        /// Required Attribute
        /// integer
        public int Charge { get; set; }

        #region Object Equality

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as IonTypeObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
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

            if (Charge == other.Charge && Equals(FragmentArrays, other.FragmentArrays) &&
                Equals(CVParams, other.CVParams) && Equals(UserParams, other.UserParams) && Equals(Index, other.Index))
            {
                return true;
            }
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
                var hashCode = Charge;
                hashCode = (hashCode * 397) ^ (FragmentArrays != null ? FragmentArrays.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Index != null ? Index.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CVParams != null ? CVParams.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}