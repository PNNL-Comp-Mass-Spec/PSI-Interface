using System;
using System.Collections.Generic;
using System.Linq;

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
        /// <remarks>By default limit the initial allocation</remarks>
        public IdentDataList() : base(1)
        {
            _identData = null;
        }

        /// <summary>
        /// Create a new list with the specified initial capacity
        /// </summary>
        /// <param name="capacity">The initial capacity</param>
        public IdentDataList(int capacity) : base(capacity)
        {
            _identData = null;
        }

        private Dictionary<string, T> idMap;

        /// <summary>
        /// When called, initializes an internal dictionary that then tracks all adds and allows fast ID-to-object reference resolving (only valid for objects implementing <see cref="IIdentifiableType"/>)
        /// </summary>
        /// <remarks>Will ignore duplicate IDs, only ever adding the first it encounters</remarks>
        public void AddIdMap()
        {
            if (idMap != null)
            {
                return;
            }

            idMap = new Dictionary<string, T>();

            foreach (var item in this)
            {
                if (item is IIdentifiableType idType && !idMap.ContainsKey(idType.Id))
                {
                    idMap.Add(idType.Id, item);
                }
            }
        }

        /// <summary>
        /// Remove the internal dictionary
        /// </summary>
        public void RemoveIdMap()
        {
            idMap = null;
        }

        /// <summary>
        /// Find the first item that matches <paramref name="id"/>. Much faster if <see cref="AddIdMap"/> is called beforehand (and this is called before <see cref="RemoveIdMap"/> is called)
        /// </summary>
        /// <param name="id"></param>
        public T GetItemById(string id)
        {
            if (idMap != null)
            {
                if (idMap.TryGetValue(id, out var value))
                {
                    return value;
                }

                return null;
            }

            var results = this.Where(x => x is IIdentifiableType idType && id.Equals(idType.Id)).ToList();

            if (results.Count > 0)
            {
                return results[0];
            }

            return null;
        }

        //public event EventHandler OnAdd;

        // Experiment at implicitly converting from a List<T> to a IdentDataList<T> fails (not allowed on base class of type);
        // Trying the same with IList or IEnumerable also fails (not allowed on interfaces);
        // But, I can do it with an array, but it is potentially more expensive (from conversion to array)
        // The only reason is to allow those using the interface to use a list, without using a special function to set the list...
        // Not sure if it is worth the cost.
        /// <summary>
        /// Attempt at converting from an array to an IdentDataList
        /// </summary>
        /// <param name="items"></param>
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
            _identData = null;
        }

        private IdentDataObj _identData;

        /// <summary>
        /// IdentData object that all items in the list should be tied to.
        /// </summary>
        public IdentDataObj IdentData
        {
            get => _identData;
            set
            {
                if (!ReferenceEquals(_identData, value))
                {
                    _identData = value;
                    foreach (var item in this)
                    {
                        item.IdentData = _identData;
                        //item.CascadeProperties();
                    }
                }
            }
        }

        internal void CascadeProperties(bool force = false)
        {
            foreach (var item in this)
            {
                item.IdentData = _identData;
                if (force)
                {
                    item.CascadeProperties();
                }
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
            item.IdentData = _identData;
            if (idMap != null && item is IIdentifiableType idType && !idMap.ContainsKey(idType.Id))
            {
                idMap.Add(idType.Id, item);
            }
            base.Add(item);
        }

        /// <summary>
        /// Add a range of items to the list, setting the IdentData property of each added object
        /// </summary>
        /// <param name="items"></param>
        public new void AddRange(IEnumerable<T> items)
        {
            if (items is ICollection<T> itemsColl)
            {
                // ICollection: make use of the List<T> implementation optimizations for AddRange.
                foreach (var item in itemsColl)
                {
                    item.IdentData = _identData;
                    if (idMap != null && item is IIdentifiableType idType && !idMap.ContainsKey(idType.Id))
                    {
                        idMap.Add(idType.Id, item);
                    }
                }

                base.AddRange(itemsColl);
            }
            else
            {
                foreach (var item in items)
                {
                    Add(item);
                }
            }
        }

        /// <summary>
        /// Add a range of items to the list, setting the IdentData property of each added object
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="items">IEnumerable of items, of a different type than <typeparamref name="T"/></param>
        /// <param name="transform">Transform to convert from <typeparamref name="TInput"/> to <typeparamref name="T"/></param>
        /// <param name="dropNull">If true, items that are null after the transform will not be added</param>
        internal void AddRange<TInput>(IEnumerable<TInput> items, Func<TInput, T> transform, bool dropNull = true)
        {
            if (items is ICollection<TInput> itemsColl)
            {
                // Ensure capacity
                if (Capacity < Count + itemsColl.Count)
                {
                    Capacity = Count + itemsColl.Count + 1;
                }
            }

            if (dropNull)
            {
                AddRange(items.Select(transform).Where(x => x != null));
            }
            else
            {
                AddRange(items.Select(transform));
            }
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="obj"></param>
        public override bool Equals(object obj)
        {
            if (!(obj is IdentDataList<T> o))
            {
                return false;
            }
            return Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(IdentDataList<T> other)
        {
            if (Count == 0 && (other == null || other.Count == 0))
            {
                return true;
            }
            if (other == null || other.Count == 0)
            {
                return false;
            }
            if (Count != other.Count)
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
        public override int GetHashCode()
        {
            unchecked
            {
                if (Count == 0)
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
        // ReSharper disable once UnusedMember.Global
        public static CVParamObj GetCvParam(this IdentDataList<ParamBaseObj> paramList, CV.CV.CVID cvid, string valueIfNotFound)
        {
            var defaultCvParam = new CVParamObj(CV.CV.CVID.CVID_Unknown, valueIfNotFound);
            if (paramList == null || paramList.Count == 0)
            {
                return defaultCvParam;
            }

            foreach (var p in paramList)
            {
                if (p is CVParamObj cvp && cvp.Cvid == cvid)
                {
                    return cvp;
                }
            }
            return defaultCvParam;
        }
    }
}
