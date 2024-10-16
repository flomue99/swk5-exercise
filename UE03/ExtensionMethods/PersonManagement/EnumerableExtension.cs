using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace PersonManagement
{
    internal static class EnumerableExtension
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (T item in items)
            {
                action(item);
            }
        }

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            foreach (var item in items)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<R> Map<T, R>(this IEnumerable<T> items, Func<T, R> transform)
        {
            foreach (T item in items)
            {
                yield return transform(item);
            }
        }

        public static T MaxBy<T>(this IEnumerable<T> items, Comparison<T> comparer)
        {
            using IEnumerator<T> e = items.GetEnumerator();
            if (!e.MoveNext())
            {
                throw new ArgumentException("MaxBy not defined for empty collection");
            }

            T maxItem = e.Current;
            while (e.MoveNext())
            {
                if(comparer(maxItem, e.Current) > 0)
                {
                    maxItem = e.Current;
                }
            }

            return maxItem;
        }
    }
}
