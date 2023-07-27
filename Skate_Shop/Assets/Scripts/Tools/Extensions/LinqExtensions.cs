using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace UnityTools.Extentions
{
    public static class LinqExtensions
    {
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

        public static IEnumerable<T> OfType<T>(this IEnumerable<T> ts, Type type)
        {
            return ts.Where(x => x.GetType().IsAssignableFrom(type));
        }
    }
}