using Artefact.TextBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Artefact.Misc
{
    internal static class Extensions
    {
        public static string Join<T>(this IEnumerable<T> enumerable, string separator)
        {
            return string.Join(separator, enumerable);
        }

        public static IEnumerable<E> Map<T, E>(this IEnumerable<T> source, Func<T, E> action)
        {
            return source.Map((item, _) => action(item));
        }

        public static IEnumerable<E> Map<T, E>(this IEnumerable<T> source, Func<T, int, E> action)
        {
            int index = 0;

            foreach (T item in source)
            {
                E value = action(item, index);
                index++;
                if (value != null)
                    yield return value;
            }
        }

        public static IEnumerable<T> GetSetFlags<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException($"Argument {typeof(T).FullName} is not an Enum");

            long lValue = Convert.ToInt64(src);
            foreach (T flag in Enum.GetValues(typeof(T)).Cast<T>())
            {
                long lFlag = Convert.ToInt64(flag);
                if ((lValue & lFlag) != 0)
                    yield return flag;
            }
        }

        /// <summary>
        /// Adds or inserts an item into a list at a specific index if it already exists
        /// </summary>
        /// <typeparam name="T">The type of the object stored in the <paramref name="list"/></typeparam>
        /// <param name="list">Source list</param>
        /// <param name="value">Value to be added/replaced</param>
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

        /// <summary>
        /// Adds or replaces a value in a dictionary
        /// </summary>
        /// <typeparam name="T">The type of the key</typeparam>
        /// <typeparam name="D">The type of the value</typeparam>
        /// <param name="dictionary">The source dictionary</param>
        /// <param name="key">The key</param>
        /// <param name="value">The value to be added or replaced</param>
        public static void AddOrReplace<T, D>(this IDictionary<T, D> dictionary, T key, D value)
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

        /// <summary>
        /// Trims a string to remove color data from it
        /// </summary>
        /// <param name="str">The string to trim</param>
        /// <returns>String without color data</returns>
        public static string TrimColor(this string str)
        {
            str = Regex.Replace(str, StringColorBuilder.RE_COLOR_START_PATTERN, "");
            str = Regex.Replace(str, StringColorBuilder.RE_COLOR_END_PATTERN, "");
            return str;
        }
    }
}
