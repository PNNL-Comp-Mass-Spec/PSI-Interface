using System;
using System.Collections.Generic;
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
        private CVParamObj _cvParam;

        private IdentDataList<FragmentArrayObj> _fragmentArrays;

        /// <summary>
        ///     Constructor
        /// </summary>
        public IonTypeObj()
        {
            Charge = 0;

            FragmentArrays = new IdentDataList<FragmentArrayObj>();
            _cvParam = null;
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

            _fragmentArrays = null;
            _cvParam = null;
            Index = null;

            if (it.FragmentArray != null && it.FragmentArray.Count > 0)
            {
                FragmentArrays = new IdentDataList<FragmentArrayObj>();
                foreach (var f in it.FragmentArray)
                {
                    FragmentArrays.Add(new FragmentArrayObj(f, IdentData));
                }
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

        /// <remarks>The type of ion identified.</remarks>
        /// <remarks>min 1, max 1</remarks>
        public CVParamObj CVParam
        {
            get { return _cvParam; }
            set
            {
                _cvParam = value;
                if (_cvParam != null)
                {
                    _cvParam.IdentData = IdentData;
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
                Equals(CVParam, other.CVParam) && Equals(Index, other.Index))
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
                hashCode = (hashCode * 397) ^ (CVParam != null ? CVParam.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}