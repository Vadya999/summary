using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace UnityTools.Extentions
{
    public static class LinqExtensions
    {
        public static int IndexOf<T>(this IEnumerable<T> collection, T searchItem)
        {
            int index = 0;

            foreach (var item in collection)
            {
                if (EqualityComparer<T>.Default.Equals(item, searchItem))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        public static void Fill<T>(this List<T> list, int size)
        {
            while (list.Count < size)
            {
                list.Add(default(T));
            }
        }

        public static void ForEach<T>(this IEnumerable<T> ts, Action<T> action)
        {
            foreach (var item in ts)
            {
                action(item);
            }
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            int counter = 0;
            foreach (var item in source)
            {
                action(item, counter++);
            }
            return source;
        }

        public static T GetRandom<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ElementAt(Random.Range(0, enumerable.Count()));
        }

        public static T GetRandom<T>(this IEnumerable<T> collection, System.Random random)
        {
            var index = random.Next(0, collection.Count() - 1);
            return collection.ElementAt(index);
        }

        public static IEnumerable<T> OfType<T>(this IEnumerable<T> ts, Type type)
        {
            return ts.Where(x => x.GetType().IsAssignableFrom(type));
        }
    }
}