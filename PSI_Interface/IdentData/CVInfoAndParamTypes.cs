using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData
{
    /// <summary>
    ///     MzIdentML cvType : Container CVListType
    /// </summary>
    /// <remarks>A source controlled vocabulary from which cvParams will be obtained.</remarks>
    /// <remarks>CVListType: The list of controlled vocabularies used in the file.</remarks>
    /// <remarks>CVListType: child element cv of type cvType, min 1, max unbounded</remarks>
    public class CVInfo : IdentDataInternalTypeAbstract, IEquatable<CVInfo>
    {
        #region Constructors
        /// <summary>
        ///     Constructor
        /// </summary>
        public CVInfo()
        {
            FullName = null;
            Version = null;
            URI = null;
            Id = null;
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="cv"></param>
        /// <param name="idata"></param>
        public CVInfo(cvType cv, IdentDataObj idata)
            : base(idata)
        {
            FullName = cv.fullName;
            Version = cv.version;
            URI = cv.uri;
            Id = cv.id;
        }
        #endregion

        #region Properties
        /// <remarks>The full name of the CV.</remarks>
        /// Required Attribute
        /// string
        public string FullName { get; set; }

        /// <remarks>The version of the CV.</remarks>
        /// Optional Attribute
        /// string
        public string Version { get; set; }

        /// <remarks>The URI of the source CV.</remarks>
        /// Required Attribute
        /// anyURI
        public string URI { get; set; }

        /// <remarks>The unique identifier of this cv within the document to be referenced by cvParam elements.</remarks>
        /// Required Attribute
        /// string
        public string Id { get; set; }
        #endregion

        #region Object Equality
        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CVInfo other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (FullName == other.FullName && Version == other.Version && URI == other.URI)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as CVInfo;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        /// <summary>
        ///     Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = FullName != null ? FullName.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Version != null ? Version.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (URI != null ? URI.GetHashCode() : 0);
                return hashCode;
            }
        }
        #endregion
    }

    /// <summary>
    ///     MzIdentML AbstractParamType; Used instead of: ParamType, ParamGroup, ParamListType
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
        ///     Constructor
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
        ///     Create an object using the contents of the corresponding MzIdentML object
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

        /// <remarks>The name of the parameter.</remarks>
        /// Required Attribute
        /// string
        public abstract string Name { get; set; }

        /// <remarks>The user-entered value of the parameter.</remarks>
        /// Optional Attribute
        /// string
        public abstract string Value { get; set; }

        /// <summary>
        ///     CV term for unit enum identifier
        /// </summary>
        public CV.CV.CVID UnitCvid
        {
            get { return _unitCvid; }
            set
            {
                _unitCvid = value;
                _unitsSet = true;
            }
        }

        /// <remarks>An accession number identifying the unit within the OBO foundry Unit CV.</remarks>
        /// Optional Attribute
        /// string
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

        /// <remarks>The name of the unit.</remarks>
        /// Optional Attribute
        /// string
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

        /// <remarks>
        ///     If a unit term is referenced, this attribute must refer to the CV 'id' attribute defined in the cvList in this
        ///     file.
        /// </remarks>
        /// Optional Attribute
        /// string
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
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
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
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
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
        ///     Object hash code
        /// </summary>
        /// <returns></returns>
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

    /// <summary>
    ///     MzIdentML ParamType
    /// </summary>
    /// <remarks>Helper type to allow either a cvParam or a userParam to be provided for an element.</remarks>
    public class ParamObj : IdentDataInternalTypeAbstract, IEquatable<ParamObj>
    {
        private ParamBaseObj _item;

        #region Constructors
        /// <summary>
        ///     Constructor
        /// </summary>
        public ParamObj()
        {
            _item = null;
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="p"></param>
        /// <param name="idata"></param>
        public ParamObj(ParamType p, IdentDataObj idata)
            : base(idata)
        {
            _item = null;

            if (p.Item != null)
            {
                if (p.Item is CVParamType)
                {
                    _item = new CVParamObj(p.Item as CVParamType, IdentData);
                }
                else if (p.Item is UserParamType)
                {
                    _item = new UserParamObj(p.Item as UserParamType, IdentData);
                }
            }
        }
        #endregion

        #region Properties
        /// <remarks>min 1, max 1</remarks>
        public ParamBaseObj Item
        {
            get { return _item; }
            set
            {
                _item = value;
                if (_item != null)
                {
                    _item.IdentData = IdentData;
                }
            }
        }
        #endregion

        #region Object Equality
        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ParamObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(Item, other.Item))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as ParamObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        /// <summary>
        ///     Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = Item != null ? Item.GetHashCode() : 0;
            return hashCode;
        }
        #endregion
    }

    /// <summary>
    ///     MzIdentML CVParamType; Container types: ToleranceType
    /// </summary>
    /// <remarks>A single entry from an ontology or a controlled vocabulary.</remarks>
    /// <remarks>ToleranceType: The tolerance of the search given as a plus and minus value with units.</remarks>
    /// <remarks>
    ///     ToleranceType: child element cvParam of type CVParamType, min 1, max unbounded "CV terms capturing the
    ///     tolerance plus and minus values."
    /// </remarks>
    public class CVParamObj : ParamBaseObj, IEquatable<CVParamObj>
    {
        private string _cvRef;
        //private string _name;
        //private string _accession;
        private string _value;

        #region Constructors
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="cvid"></param>
        /// <param name="value"></param>
        public CVParamObj(CV.CV.CVID cvid = CV.CV.CVID.CVID_Unknown, string value = null)
        {
            _cvRef = null;
            //this._name = null;
            //this._accession = null;
            _value = value;

            Cvid = cvid;
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="cvp"></param>
        /// <param name="idata"></param>
        //public CVParam(CVParamType cvp, IdentData idata)
        //    : base(cvp, idata)
        public CVParamObj(CVParamType cvp, IdentDataObj idata)
            : base(idata)
        {
            CVRef = cvp.cvRef;
            //this._name = cvp.name;
            //this._accession = cvp.accession;
            Accession = cvp.accession;
            _value = cvp.value;

            UnitCvRef = cvp.unitCvRef;
            //this._unitAccession = cvp.unitAccession;
            UnitAccession = cvp.unitAccession;
            //this._unitName = cvp.unitName;

            //this._cvid = CV.CV.CVID.CVID_Unknown;
        }
        #endregion

        #region Properties
        /// <summary>
        ///     CV term enum
        /// </summary>
        public CV.CV.CVID Cvid { get; set; }

        /// <remarks>A reference to the cv element from which this term originates.</remarks>
        /// Required Attribute
        /// string
        public string CVRef
        {
            get
            {
                //return this._cvRef; 
                return IdentData.CvTranslator.ConvertOboCVRef(CV.CV.TermData[Cvid].CVRef);
            }
            set
            {
                _cvRef = IdentData.CvTranslator.ConvertFileCVRef(value);
                //this._cvRef = value;
            }
        }

        /// <remarks>The accession or ID number of this CV term in the source CV.</remarks>
        /// Required Attribute
        /// string
        public string Accession
        {
            get
            {
                return CV.CV.TermData[Cvid].Id;
                //return this._accession; 
            }
            set
            {
                //this._accession = value; 
                if (CV.CV.TermAccessionLookup.ContainsKey(_cvRef) &&
                    CV.CV.TermAccessionLookup[_cvRef].ContainsKey(value))
                {
                    //this.Cvid = CV.CV.TermAccessionLookup[oboAcc];
                    Cvid = CV.CV.TermAccessionLookup[_cvRef][value];
                }
                else
                {
                    Cvid = CV.CV.CVID.CVID_Unknown;
                }
            }
        }

        /// <remarks>The name of the parameter.</remarks>
        /// Required Attribute
        /// string
        public override string Name
        {
            get
            {
                return CV.CV.TermData[Cvid].Name;
                //return this._name; 
            }
            set
            {
                /*this._name = value;*/
            } // Don't want to do anything here. public interface uses CVID
        }

        /// <remarks>The user-entered value of the parameter.</remarks>
        /// Optional Attribute
        /// string
        public override string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        #endregion

        /// <summary>
        ///     Convert the value of the CVParam to type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ValueAs<T>() where T : IConvertible
        {
            return (T)Convert.ChangeType(Value, typeof(T));
        }

        #region Object Equality
        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CVParamObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Cvid == other.Cvid && Value == other.Value &&
                UnitCvid == other.UnitCvid)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as CVParamObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
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
                hashCode = (hashCode * 397) ^ Cvid.GetHashCode();
                hashCode = (hashCode * 397) ^ (Value != null ? Value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ UnitCvid.GetHashCode();
                return hashCode;
            }
        }
        #endregion
    }

    /// <summary>
    ///     Object for containing multiple CVParamObjs
    /// </summary>
    public class CVParamGroupObj : IdentDataInternalTypeAbstract, IEquatable<CVParamGroupObj>
    {
        private IdentDataList<CVParamObj> _cvParams;

        #region Constructors
        /// <summary>
        ///     Constructor
        /// </summary>
        public CVParamGroupObj()
        {
            CVParams = new IdentDataList<CVParamObj>();
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="cvpg"></param>
        /// <param name="idata"></param>
        public CVParamGroupObj(ICVParamGroup cvpg, IdentDataObj idata)
            : base(idata)
        {
            _cvParams = null;

            if (cvpg.cvParam != null && cvpg.cvParam.Count > 0)
            {
                CVParams = new IdentDataList<CVParamObj>();
                foreach (var cvp in cvpg.cvParam)
                {
                    CVParams.Add(new CVParamObj(cvp, IdentData));
                }
            }
        }
        #endregion

        #region Properties
        /// <summary>
        ///     List of CVParams
        /// </summary>
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
        #endregion

        #region Object Equality
        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CVParamGroupObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(CVParams, other.CVParams))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as CVParamGroupObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        /// <summary>
        ///     Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = CVParams != null ? CVParams.GetHashCode() : 0;
            return hashCode;
        }
        #endregion
    }

    /// <summary>
    ///     MzIdentML UserParamType
    /// </summary>
    /// <remarks>A single user-defined parameter.</remarks>
    public class UserParamObj : ParamBaseObj, IEquatable<UserParamObj>
    {
        private string _name;
        private string _value;

        #region Constructors

        /// <summary>
        ///     Constructor
        /// </summary>
        public UserParamObj()
        {
            _name = null;
            _value = null;
            Type = null;
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="up"></param>
        /// <param name="idata"></param>
        public UserParamObj(UserParamType up, IdentDataObj idata)
            : base(idata)
        {
            _name = up.name;
            _value = up.value;
            Type = up.type;

            UnitCvRef = up.unitCvRef;
            //this._unitAccession = up.unitAccession;
            UnitAccession = up.unitAccession;
            //this._unitName = up.unitName;
        }

        #endregion

        #region Properties

        /// <remarks>The name of the parameter.</remarks>
        /// Required Attribute
        /// string
        public override string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <remarks>The user-entered value of the parameter.</remarks>
        /// Optional Attribute
        /// string
        public override string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <remarks>The datatype of the parameter, where appropriate (e.g.: xsd:float).</remarks>
        /// Optional Attribute
        /// string
        public string Type { get; set; }

        #endregion

        #region Object Equality

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(UserParamObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Name == other.Name && Value == other.Value && Type == other.Type &&
                UnitCvid == other.UnitCvid)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as UserParamObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
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
                hashCode = (hashCode * 397) ^ (Value != null ? Value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Type != null ? Type.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ UnitCvid.GetHashCode();
                return hashCode;
            }
        }

        #endregion
    }

    /// <summary>
    ///     MzIdentML ParamListType
    /// </summary>
    /// <remarks>Helper type to allow multiple cvParams or userParams to be given for an element.</remarks>
    public class ParamListObj : IdentDataInternalTypeAbstract, IEquatable<ParamListObj>
    {
        private IdentDataList<ParamBaseObj> _items;

        #region Constructors
        /// <summary>
        ///     Constructor
        /// </summary>
        public ParamListObj()
        {
            Items = new IdentDataList<ParamBaseObj>();
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="pl"></param>
        /// <param name="idata"></param>
        public ParamListObj(ParamListType pl, IdentDataObj idata)
            : base(idata)
        {
            _items = null;

            if (pl != null && pl.Items.Count > 0)
            {
                Items = new IdentDataList<ParamBaseObj>();
                foreach (var p in pl.Items)
                {
                    if (p is CVParamType)
                    {
                        Items.Add(new CVParamObj(p as CVParamType, IdentData));
                    }
                    else if (p is UserParamType)
                    {
                        Items.Add(new UserParamObj(p as UserParamType, IdentData));
                    }
                }
            }
        }
        #endregion

        #region Properties
        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<ParamBaseObj> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                if (_items != null)
                {
                    _items.IdentData = IdentData;
                }
            }
        }
        #endregion

        #region Object Equality
        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ParamListObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(Items, other.Items))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as ParamListObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        /// <summary>
        ///     Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = Items != null ? Items.GetHashCode() : 0;
            return hashCode;
        }
        #endregion
    }

    /// <summary>
    ///     Object for containing multiple CVParamObjs and UserParamObjs
    /// </summary>
    public class ParamGroupObj : CVParamGroupObj, IEquatable<ParamGroupObj>
    {
        private IdentDataList<UserParamObj> _userParams;

        #region Constructors
        /// <summary>
        ///     Constructor
        /// </summary>
        public ParamGroupObj()
        {
            UserParams = new IdentDataList<UserParamObj>();
        }

        /// <summary>
        ///     Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="pg"></param>
        /// <param name="idata"></param>
        public ParamGroupObj(IParamGroup pg, IdentDataObj idata)
            : base(pg, idata)
        {
            _userParams = null;

            if (pg.userParam != null && pg.userParam.Count > 0)
            {
                UserParams = new IdentDataList<UserParamObj>();
                foreach (var up in pg.userParam)
                {
                    UserParams.Add(new UserParamObj(up, IdentData));
                }
            }
        }
        #endregion

        #region Properties
        /// <summary>
        ///     List of UserParams
        /// </summary>
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
        #endregion

        #region Object Equality
        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ParamGroupObj other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other == null)
            {
                return false;
            }

            if (Equals(CVParams, other.CVParams) && Equals(UserParams, other.UserParams))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var o = other as ParamGroupObj;
            if (o == null)
            {
                return false;
            }
            return Equals(o);
        }

        /// <summary>
        ///     Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = CVParams != null ? CVParams.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (UserParams != null ? UserParams.GetHashCode() : 0);
                return hashCode;
            }
        }
        #endregion
    }
}