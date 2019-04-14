using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solver
{
    static class Utilities
    {
        public static T ArgMax<T, TComp>(this IEnumerable<T> elements, Func<T, TComp> func) where TComp : IComparable<TComp>
        {
            var it = elements.GetEnumerator();
            if (!it.MoveNext())
                return default(T);

            T arg = it.Current;
            TComp max = func(arg);
            while (it.MoveNext())
            {
                T cur_arg = it.Current;
                TComp cur_max = func(cur_arg);
                if (cur_max.CompareTo(max) > 0)
                {
                    arg = cur_arg;
                    max = cur_max;
                }
            }

            return arg;
        }

        public static IEnumerable<LinkedListNode<T>> SelectNodes<T>(this LinkedList<T> list)
        {
            var node = list.First;
            while (node != null)
            {
                yield return node;
                node = node.Next;
            }
        }
    }
}
