using System;
using System.Collections.Generic;

namespace SiriusModel.InOut.Base
{
    public static class CollectionHelper
    {
        public static T[] Merge<T>(this T[] array, params T[][] toMerges)
        {
            foreach (var toMerge in toMerges)
            {
                var lastArrayLength = array.Length;
                Array.Resize(ref array, array.Length + toMerge.Length);
                toMerge.CopyTo(array, lastArrayLength);
            }
            return array;
        }

        public static void Iterate<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var t in enumerable)
            {
                action.Invoke(t);
            }
        }
    }
}
