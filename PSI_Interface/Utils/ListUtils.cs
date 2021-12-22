using System.Collections.Generic;

namespace PSI_Interface.Utils
{
    /// <summary>
    /// Utilities for comparing two lists
    /// </summary>
    public static class ListUtils
    {
        /// <summary>
        /// Check for equality of two lists, without ordering them first
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        public static bool ListEqualsUnOrdered<T>(List<T> first, List<T> second)
        {
            if ((first == null || first.Count == 0) && (second == null || second.Count == 0))
            {
                return true;
            }
            if ((first == null || first.Count == 0) || (second == null || second.Count == 0))
            {
                return false;
            }
            if (first.Count != second.Count)
            {
                return false;
            }

            foreach (var item in first)
            {
                var found = false;

                foreach (var item2 in second)
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
        /// Check for equality of two lists, requiring them to be a index by index match
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        // ReSharper disable once UnusedMember.Global
        public static bool ListEqualsOrdered<T>(List<T> first, List<T> second)
        {
            if ((first == null || first.Count == 0) && (second == null || second.Count == 0))
            {
                return true;
            }
            if ((first == null || first.Count == 0) || (second == null || second.Count == 0))
            {
                return false;
            }
            if (first.Count != second.Count)
            {
                return false;
            }

            for (var i = 0; i < first.Count; i++)
            {
                if (!first[i].Equals(second[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
