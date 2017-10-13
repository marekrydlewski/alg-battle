using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgBattle
{
    public static class IListExtensions
    {
        public static void Swap<T>(
            this IList<T> list,
            int firstIndex,
            int secondIndex
        )
        {
            if (firstIndex == secondIndex)
            {
                return;
            }
            T temp = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temp;
        }
    }

    public static class IEnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Chunk<T>(
            this IEnumerable<T> source,
            int chunksize)
        {
            var pos = 0;
            while (source.Skip(pos).Any())
            {
                yield return source.Skip(pos).Take(chunksize);
                pos += chunksize;
            }
        }
    }
}
