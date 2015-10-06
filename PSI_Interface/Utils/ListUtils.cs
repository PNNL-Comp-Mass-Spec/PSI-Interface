using System.Collections.Generic;

namespace PSI_Interface.Utils
{
    public static class ListUtils
    {
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
