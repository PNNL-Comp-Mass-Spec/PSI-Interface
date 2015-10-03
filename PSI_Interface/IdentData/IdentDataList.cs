using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI_Interface.IdentData
{
    /// <summary>
    /// This is a subclass of List, specifically because there needs to be a connection from the IdentData class to all other objects it contains
    /// for handling of CVParams and references
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IdentDataList<T> : List<T> where T : IdentDataInternalTypeAbstract
    {
        public IdentDataList()
        {
            this._identData = new IdentData(false);
        }

        //public event EventHandler OnAdd;

        // Experiment at implicitly converting from a List<T> to a IdentDataList<T> fails (not allowed on base class of type);
        // Trying the same with IList or IEnumerable also fails (not allowed on interfaces);
        // But, I can do it with an array, but it is potentially more expensive (from conversion to array)
        // The only reason to to allow those using the interface to use a list, without using a special function to set the list...
        // Not sure if it is worth the cost.
        public static implicit operator IdentDataList<T>(T[] items)
        {
            return new IdentDataList<T>(items);
        }

        public IdentDataList(IEnumerable<T> items)
        {
            this._identData = new IdentData(false);
            this.AddRange(items);
        }

        private IdentData _identData;

        public IdentData IdentData
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
                    }
                }
            }
        }

        public new void Add(T item)
        {
            //if (null != OnAdd)
            //{
            //    OnAdd(this, null);
            //}
            item.IdentData = this._identData;
            base.Add(item);
        }

        public new void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                item.IdentData = this._identData;
                base.Add(item);
            }
        }
    }
}
