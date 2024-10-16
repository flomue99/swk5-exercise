using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagement
{
    internal static class CollectionExtensions
    {
        //with this -> it will be a extension for ICollection
        public static void AddAll<T>(this ICollection<T> target, IEnumerable<T> source)
        {
            foreach (T item in source)
            {
               target.Add(item);
            }
        }
    }
}
