using System.Collections.Generic;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// This is a subclass of List, specifically because there needs to be a connection from the IdentData class to all other objects it contains
    /// for handling of CVParams and references
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IdentDataList<T> : List<T> where T : IdentDataInternalTypeAbstract
    {
        /// <summary>
        /// Create a new list
        /// </summary>
        public IdentDataList()
        {
            this._identData = null;
        }

        //public event EventHandler OnAdd;

        // Experiment at implicitly converting from a List<T> to a IdentDataList<T> fails (not allowed on base class of type);
        // Trying the same with IList or IEnumerable also fails (not allowed on interfaces);
        // But, I can do it with an array, but it is potentially more expensive (from conversion to array)
        // The only reason to to allow those using the interface to use a list, without using a special function to set the list...
        // Not sure if it is worth the cost.
        /// <summary>
        /// Attempt at converting from an array to an IdentDataList
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static implicit operator IdentDataList<T>(T[] items)
        {
            return new IdentDataList<T>(items);
        }

        /// <summary>
        /// Create a new list and populate it with the provided items.
        /// </summary>
        /// <param name="items"></param>
        public IdentDataList(IEnumerable<T> items) : base(items)
        {
            this._identData = null;
        }

        private IdentDataObj _identData;

        /// <summary>
        /// IdentData object that all items in the list should be tied to.
        /// </summary>
        public IdentDataObj IdentData
        {
            get { return _identData; }
            set
            {
                if (!ReferenceEquals(this._identData, value))
                {
                    _identData = value;
                    foreach (T item in this)
                    {
                        item.IdentData = this._identData;
                        //item.CascadeProperties();
                    }
                }
            }
        }

        internal void CascadeProperties()
        {
            foreach (T item in this)
            {
                item.IdentData = this._identData;
                item.CascadeProperties();
            }
        }

        /// <summary>
        /// Add an item to the list, setting the IdentData property of the added object
        /// </summary>
        /// <param name="item"></param>
        public new void Add(T item)
        {
            //if (null != OnAdd)
            //{
            //    OnAdd(this, null);
            //}
            item.IdentData = this._identData;
            base.Add(item);
        }

        /// <summary>
        /// Add a range of items to the list, setting the IdentData property of each added object
        /// </summary>
        /// <param name="items"></param>
        public new void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                item.IdentData = this._identData;
                base.Add(item);
            }
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var o = obj as IdentDataList<T>;
            if (o == null)
            {
                return false;
            }
            return this.Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IdentDataList<T> other)
        {
            if (this.Count == 0 && (other == null || other.Count == 0))
            {
                return true;
            }
            if (other == null || other.Count == 0)
            {
                return false;
            }
            if (this.Count != other.Count)
            {
                return false;
            }
            foreach (var item in this)
            {
                var found = false;

                foreach (var item2 in other)
                {
                    if (item.Equals(item2))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                if (this.Count == 0)
                {
                    return 0;
                }
                var hashCode = 1;
                foreach (var item in this)
                {
                    hashCode = (hashCode * 397) ^ item.GetHashCode();
                }
                return hashCode;
            }
        }
    }

    /// <summary>
    /// Extensions that require certain data types
    /// </summary>
    public static class IdentDataListExtensions
    {
        /// <summary>
        /// Get the CVParam that matches the specified CV term, defaulting to the value provided if not found.
        /// </summary>
        /// <param name="cvParamList"></param>
        /// <param name="cvid"></param>
        /// <param name="valueIfNotFound"></param>
        /// <returns></returns>
        public static CVParamObj GetCvParam(this IdentDataList<CVParamObj> cvParamList, CV.CV.CVID cvid, string valueIfNotFound)
        {
            var defaultCvParam = new CVParamObj(CV.CV.CVID.CVID_Unknown, valueIfNotFound);
            if (cvParamList == null || cvParamList.Count == 0)
            {
                return defaultCvParam;
            }

            foreach (var cvp in cvParamList)
            {
                if (cvp.Cvid == cvid)
                {
                    return cvp;
                }
            }
            return defaultCvParam;
        }

        /// <summary>
        /// Get the CVParam that matches the specified CV term, defaulting to the value provided if not found.
        /// </summary>
        /// <param name="paramList"></param>
        /// <param name="cvid"></param>
        /// <param name="valueIfNotFound"></param>
        /// <returns></returns>
        public static CVParamObj GetCvParam(this IdentDataList<ParamBaseObj> paramList, CV.CV.CVID cvid, string valueIfNotFound)
        {
            var defaultCvParam = new CVParamObj(CV.CV.CVID.CVID_Unknown, valueIfNotFound);
            if (paramList == null || paramList.Count == 0)
            {
                return defaultCvParam;
            }

            foreach (var p in paramList)
            {
                var cvp = p as CVParamObj;
                if (cvp != null && cvp.Cvid == cvid)
                {
                    return cvp;
                }
            }
            return defaultCvParam;
        }
    }
}
