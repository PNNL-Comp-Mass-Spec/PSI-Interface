using System.Collections.Generic;

namespace PSI_Interface.MSData
{
    /// <summary>
    /// This is a subclass of List, specifically because there needs to be a connection from the MSData class to all other objects it contains
    /// for handling of CVParams and references
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MSDataList<T> : List<T> where T : MSDataInternalTypeAbstract
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MSDataList()
        {
            this._msData = new MSData(false);
        }
        //public event EventHandler OnAdd;

        // Experiment at implicitly converting from a List<T> to a MSDataList<T> fails (not allowed on base class of type);
        // Trying the same with IList or IEnumerable also fails (not allowed on interfaces);
        // But, I can do it with an array, but it is potentially more expensive (from conversion to array)
        // The only reason to to allow those using the interface to use a list, without using a special function to set the list...
        // Not sure if it is worth the cost.
        /// <summary>
        /// Convert array to list
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static implicit operator MSDataList<T>(T[] items)
        {
            return new MSDataList<T>(items);
        }

        /// <summary>
        /// Create array from IEnumerable
        /// </summary>
        /// <param name="items"></param>
        public MSDataList(IEnumerable<T> items)
        {
            this._msData = new MSData(false);
            this.AddRange(items);
        }

        private MSData _msData;
        private int _defaultArrayLength;

        /// <summary>
        /// Reference to root object
        /// </summary>
        public MSData MsData
        {
            get { return _msData; }
            set
            {
                if (!ReferenceEquals(this._msData, value))
                {
                    this._msData = value;
                    foreach (T item in this)
                    {
                        item.MsData = this._msData;
                    }
                }
            }
        }

        /// <summary>
        /// Default length of binary data array
        /// </summary>
        public int DefaultArrayLength
        {
            get { return _defaultArrayLength; }
            set
            {
                _defaultArrayLength = value;
                foreach (T item in this)
                {
                    item.BdaDefaultArrayLength = this._defaultArrayLength;
                }
            }
        }

        /// <summary>
        /// Add an item
        /// </summary>
        /// <param name="item"></param>
        new public void Add(T item)
        {
            //if (null != OnAdd)
            //{
            //    OnAdd(this, null);
            //}
            item.MsData = this._msData;
            item.BdaDefaultArrayLength = this._defaultArrayLength;
            base.Add(item);
        }

        /// <summary>
        /// Add a group of items
        /// </summary>
        /// <param name="items"></param>
        new public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                item.MsData = this._msData;
                item.BdaDefaultArrayLength = this._defaultArrayLength;
                base.Add(item);
            }
        }
    }
}
