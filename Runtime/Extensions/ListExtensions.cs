using System;
using System.Collections.Generic;

namespace CursR.Runtime.Extensions {
    public static class ListExtensions {
        public static bool IsEmpty<T>(this List<T> source) => source.Count == 0;

        public static void RemoveWhere<T>(this List<T> source, Predicate<T> predicate) {
            if (source.IsEmpty()) return;
            for (int i = source.Count - 1; i >= 0; i--)
                if (predicate(source[i])) {
                    source.RemoveAt(i);
                }
        }
    }
}