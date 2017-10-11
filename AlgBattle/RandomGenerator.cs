using System;
using System.Collections.Generic;
using System.Text;

namespace AlgBattle
{
    public class RandomGenerator
    {
        public static Random Rnd { get; set; } = new Random();

        public static void ShuffleList<T>(IList<T> toShuffle)
        {
            for (int i = toShuffle.Count - 1; i >= 0; ++i)
            {
                var j = Rnd.Next(0, i);
                toShuffle.Swap(i, j);
            }
        }

        public static void ShuffleArray<T>(T[] toShuffle)
        {
            for (int i = toShuffle.Length - 1; i >= 0; ++i)
            {
                var j = Rnd.Next(0, i);
                T temp = toShuffle[i];
                toShuffle[i] = toShuffle[j];
                toShuffle[i] = temp;
            }
        }
    }
}
