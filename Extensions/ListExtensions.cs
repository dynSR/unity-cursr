using System;
using System.Collections.Generic;

namespace UnityTools.Extensions {
    public static class ListExtensions {
        public static bool IsEmpty<T>(this List<T> source) => source.Count == 0;

        public static void RemoveWhere<T>(this List<T> source, Predicate<T> predicate) {
            if (source.IsEmpty()) return;
            for (int i = source.Count - 1; i >= 0; i--)
                if (predicate(source[i])) {
                    source.RemoveAt(i);
                }
        }

        public static void AddUnique<T>(this List<T> source, T item) {
            if (!source.Contains(item)) source.Add(item);
        }

        public static void AddRangeUnique<T>(this List<T> source, IEnumerable<T> items) {
            foreach (T item in items) {
                if (!source.Contains(item)) source.Add(item);
            }
        }
    }
}