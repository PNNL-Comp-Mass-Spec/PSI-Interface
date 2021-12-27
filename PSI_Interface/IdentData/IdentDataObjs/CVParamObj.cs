using System;
using PSI_Interface.IdentData.mzIdentML;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML CVParamType; Container types: ToleranceType
    /// </summary>
    /// <remarks>A single entry from an ontology or a controlled vocabulary.</remarks>
    /// <remarks>ToleranceType: The tolerance of the search given as a plus and minus value with units.</remarks>
    /// <remarks>
    /// ToleranceType: child element cvParam of type CVParamType, min 1, max unbounded "CV terms capturing the
    /// tolerance plus and minus values."
    /// </remarks>
    public class CVParamObj : ParamBaseObj, IEquatable<CVParamObj>
    {
        private string _cvRef;
        private string _name;
        //private string _accession;
        private string _value;

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cvid"></param>
        /// <param name="value"></param>
        public CVParamObj(CV.CV.CVID cvid = CV.CV.CVID.CVID_Unknown, string value = null)
        {
            _cvRef = null;
            _name = null;
            //this._accession = null;
            _value = value;

            Cvid = cvid;
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="cvp"></param>
        /// <param name="idata"></param>
        //public CVParam(CVParamType cvp, IdentData idata)
        //    : base(cvp, idata)
        public CVParamObj(CVParamType cvp, IdentDataObj idata)
            : base(idata)
        {
            CVRef = cvp.cvRef;
            //this._accession = cvp.accession;
            Accession = cvp.accession;
            Name = cvp.name;
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
        /// CV term enum
        /// </summary>
        public CV.CV.CVID Cvid { get; set; }

        /// <summary>A reference to the cv element from which this term originates.</summary>
        /// <remarks>Required Attribute</remarks>
        public string CVRef
        {
            get => IdentData.CvTranslator.ConvertOboCVRef(CV.CV.TermData[Cvid].CVRef);
            set => _cvRef = IdentData.CvTranslator.ConvertFileCVRef(value);
        }

        /// <summary>The accession or ID number of this CV term in the source CV.</summary>
        /// <remarks>Required Attribute</remarks>
        public string Accession
        {
            get => CV.CV.TermData[Cvid].Id;
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

        /// <summary>The name of the parameter.</summary>
        /// <remarks>Required Attribute</remarks>
        public override string Name
        {
            get
            {
                if (Cvid != CV.CV.CVID.CVID_Unknown)
                {
                    return CV.CV.TermData[Cvid].Name;
                }
                return _name;
            }
            set
            {
                if (Cvid == CV.CV.CVID.CVID_Unknown)
                {
                    _name = value;
                }
            } // Only store name if the CVID is Unknown.
        }

        /// <summary>The user-entered value of the parameter.</summary>
        /// <remarks>Optional Attribute</remarks>
        public override string Value
        {
            get => _value;
            set => _value = value;
        }
        #endregion

        /// <summary>
        /// Convert the value of the CVParam to type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public T ValueAs<T>() where T : IConvertible
        {
            return (T)Convert.ChangeType(Value, typeof(T));
        }

        #region Object Equality
        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
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

            return Cvid == other.Cvid && Value == other.Value &&
                   UnitCvid == other.UnitCvid;
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is CVParamObj o && Equals(o);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
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
}
