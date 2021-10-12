using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Misc
{
    static class Extensions
    {
        public static string Join<T>(this IEnumerable<T> enumerable, string separator)
        {
            return string.Join(separator, enumerable);
        }

        public static List<E> Map<T, E>(this IEnumerable<T> source, Func<T, E> action)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (action == null) throw new ArgumentNullException("action");

            List<E> values = new List<E>();

            foreach (T item in source)
            {
                E value = action(item);
                if (value != null)
                    values.Add(value);
            }

            return values;
        }

        public static List<E> Map<T, E>(this IEnumerable<T> source, Func<T, int, E> action)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (action == null) throw new ArgumentNullException("action");

            List<E> values = new List<E>();

            int index = 0;

            foreach (T item in source)
            {
                E value = action(item, index);
                index++;
                if (value != null)
                    values.Add(value);
            }

            return values;
        }

        public static bool Contains<T>(this T[] array, T value)
        {
            return array.Contains((val) => val.Equals(value));
        }

        public static bool Contains<T>(this T[] array, Func<T, bool> action)
        {
            return Array.Exists(array, val => action(val));
        }

        public static T Next<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) + 1;
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }

        public static T Previous<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) - 1;
            return (j < 0) ? Arr[Arr.Length - 1] : Arr[j];
        }

        public static void AddOrInsert<T>(this IList<T> list, T value)
        {
            if (!list.Contains(value))
            {
                list.Add(value);
            }
            else
            {
                int index = list.IndexOf(value);
                list.Remove(value);
                list.Insert(index, value);
            }
        }

        public static void AddOrReplace<T, D>(this Dictionary<T, D> dictionary, T key, D value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }
            else
            {
                dictionary.Remove(key);
                dictionary.Add(key, value);
            }
        }
    }
}
