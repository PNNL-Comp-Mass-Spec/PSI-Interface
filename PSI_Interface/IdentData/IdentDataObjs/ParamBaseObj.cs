using System;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML AbstractParamType; Used instead of: ParamType, ParamGroup, ParamListType
    /// </summary>
    /// <remarks>Abstract entity allowing either cvParam or userParam to be referenced in other schemas.</remarks>
    /// <remarks>PramGroup: A choice of either a cvParam or userParam.</remarks>
    /// <remarks>ParamType: Helper type to allow either a cvParam or a userParam to be provided for an element.</remarks>
    /// <remarks>ParamListType: Helper type to allow multiple cvParams or userParams to be given for an element.</remarks>
    public abstract class ParamBaseObj : IdentDataInternalTypeAbstract, IEquatable<ParamBaseObj>
    {
        private CV.CV.CVID _unitCvid;
        //private string _unitAccession;
        //private string _unitName;
        private string _unitCvRef;
        private bool _unitsSet;

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        protected ParamBaseObj()
        {
            //this._unitAccession = null;
            //this._unitName = null;
            _unitCvRef = null;
            _unitsSet = false;

            _unitCvid = CV.CV.CVID.CVID_Unknown;
        }

        /*public ParamBase(AbstractParamType ap, IdentData idata)
            : base(idata)
        {
            this._unitsSet = false;
            this.UnitCvRef = ap.unitCvRef;
            //this._unitAccession = ap.unitAccession;
            this.UnitAccession = ap.unitAccession;
            //this._unitName = ap.unitName;

            //this._unitCvid = CV.CV.CVID.CVID_Unknown;
        }*/

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="idata"></param>
        protected ParamBaseObj(IdentDataObj idata)
            : base(idata)
        {
            _unitsSet = false;
            //this.UnitCvRef = ap.unitCvRef;
            //this._unitAccession = ap.unitAccession;
            //this.UnitAccession = ap.unitAccession;
            //this._unitName = ap.unitName;

            //this._unitCvid = CV.CV.CVID.CVID_Unknown;
        }
        #endregion

        #region Properties
        // Name and value are abstract properties, because name will be handled differently in CVParams, and value can also have restrictions based on the CVParam.

        /// <summary>The name of the parameter.</summary>
        /// <remarks>Required Attribute</remarks>
        public abstract string Name { get; set; }

        /// <summary>The user-entered value of the parameter.</summary>
        /// <remarks>Optional Attribute</remarks>
        public abstract string Value { get; set; }

        /// <summary>
        /// CV term for unit enum identifier
        /// </summary>
        public CV.CV.CVID UnitCvid
        {
            get => _unitCvid;
            set
            {
                _unitCvid = value;
                _unitsSet = true;
            }
        }

        /// <summary>An accession number identifying the unit within the OBO foundry Unit CV.</summary>
        /// <remarks>Optional Attribute</remarks>
        public string UnitAccession
        {
            get
            {
                if (_unitsSet)
                {
                    return CV.CV.TermData[UnitCvid].Id;
                }
                return null;
                //return this._unitAccession;
            }
            set
            {
                //this._unitAccession = value;
                if (value != null && CV.CV.TermAccessionLookup.ContainsKey(_unitCvRef) &&
                    CV.CV.TermAccessionLookup[_unitCvRef].ContainsKey(value))
                {
                    _unitsSet = true;
                    _unitCvid = CV.CV.TermAccessionLookup[_unitCvRef][value];
                }
                else
                {
                    _unitCvid = CV.CV.CVID.CVID_Unknown;
                }
            }
        }

        /// <summary>The name of the unit.</summary>
        /// <remarks>Optional Attribute</remarks>
        public string UnitName
        {
            get
            {
                if (_unitsSet)
                {
                    return CV.CV.TermData[UnitCvid].Name;
                }
                return null;
                //return this._unitName;
            }
            //set { this._unitName = value; }
        }

        /// <summary>
        /// If a unit term is referenced, this attribute must refer to the CV 'id' attribute defined in the cvList in this
        /// file.
        /// </summary>
        /// <remarks>Optional Attribute</remarks>
        public string UnitCvRef
        {
            get
            {
                if (_unitsSet)
                {
                    return IdentData.CvTranslator.ConvertOboCVRef(CV.CV.TermData[UnitCvid].CVRef);
                }
                return null;
                //return this._unitCvRef;
            }
            set
            {
                _unitCvRef = value;
                if (value != null)
                {
                    _unitCvRef = IdentData.CvTranslator.ConvertFileCVRef(value);
                }
            }
        }
        #endregion

        #region Object Equality
        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(ParamBaseObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Name == other.Name && UnitCvid == other.UnitCvid)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            var o = other as ParamBaseObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name != null ? Name.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ UnitCvid.GetHashCode();
                return hashCode;
            }
        }

        #endregion
    }
}
