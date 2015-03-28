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
        public MSDataList()
        { }
        //public event EventHandler OnAdd;

        // Experiment at implicitly converting from a List<T> to a MSDataList<T> fails (not allowed on base class of type);
        // Trying the same with IList or IEnumerable also fails (not allowed on interfaces);
        // But, I can do it with an array, but it is potentially more expensive (from conversion to array)
        // The only reason to to allow those using the interface to use a list, without using a special function to set the list...
        // Not sure if it is worth the cost.
        public static implicit operator MSDataList<T>(T[] items)
        {
            return new MSDataList<T>(items);
        }

        public MSDataList(IEnumerable<T> items)
        {
            this.AddRange(items);
        }

        private MSData _msData;
        private int _defaultArrayLength;

        public MSData MsData
        {
            get { return _msData; }
            set
            {
                _msData = value;
                foreach (T item in this)
                {
                    item.MsData = this.MsData;
                }
            }
        }

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

        new public void Add(T item)
        {
            //if (null != OnAdd)
            //{
            //    OnAdd(this, null);
            //}
            item.MsData = this.MsData;
            item.BdaDefaultArrayLength = this._defaultArrayLength;
            base.Add(item);
        }

        new public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                item.MsData = this.MsData;
                item.BdaDefaultArrayLength = this._defaultArrayLength;
                base.Add(item);
            }
        }
    }
}
