using System;
using System.Collections.Generic;
using System.Linq;

namespace PSI_Interface.IdentData.mzIdentML
{
    /// <summary>
    /// Extension methods for mzIdentML namespace
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// AddRange overload to perform optimized allocations when we need to perform a transform on the source list to convert types before adding it.
        /// </summary>
        /// <typeparam name="T">List contained type</typeparam>
        /// <typeparam name="TInput">Input IEnumerable contained type</typeparam>
        /// <param name="list">target list</param>
        /// <param name="items">
        /// source list/IEnumerable. If a list, the target list will have capacity changed
        /// to allow adding all of the items in this list without further re-allocations.
        /// </param>
        /// <param name="transform">Transform function to convert source items to target items</param>
        /// <param name="dropNull">If set to false, null items (after the transform) will be added to the target list</param>
        public static void AddRange<T, TInput>(this List<T> list, IEnumerable<TInput> items, Func<TInput, T> transform, bool dropNull = true)
        {
            if (items is ICollection<TInput> itemsColl)
            {
                // Ensure capacity
                if (list.Capacity < list.Count + itemsColl.Count)
                {
                    list.Capacity = list.Count + itemsColl.Count + 1;
                }
            }

            if (dropNull)
            {
                list.AddRange(items.Select(transform).Where(x => x != null));
            }
            else
            {
                list.AddRange(items.Select(transform));
            }
        }
    }
}
