namespace UnityTools.Extensions {
    public static class ArrayExtensions {
        public static bool IsEmpty<T>(this T[] source) => source.Length == 0;
    }
}