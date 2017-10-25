using System;
using System.Linq;
using AlgBattle.DataReaders;
using AlgBattle.Benchmarks;
using System.Collections.Generic;

namespace AlgBattle.Solvers
{
    public abstract class QapSolver
    {
        public QapData Data { get; set; }

        public RandomGenerator Rnd { get; set; } = new RandomGenerator();

        public QapSolutionBenchmark SolutionBenchmark { get; set; } = new QapSolutionBenchmark();

        abstract public int[] GetSolution();

        public int[] GetRandomInitSolution()
        {
            var init = Enumerable.Range(0, Data.Size).ToArray();
            Rnd.Shuffle(init);
            return init;
        }

        public int[] GetGreedyInitSolution()
        {
            int[] solution = Enumerable.Repeat(-1, Data.Size).ToArray();
            var flows = Flatten2DimArrayWithIndexes(Data.Flows);
            var distances = Flatten2DimArrayWithIndexes(Data.Distances);

            flows.Sort((a, b) => b.Item1.CompareTo(a.Item1)); //descending
            distances.Sort((a, b) => a.Item1.CompareTo(b.Item1)); //ascending

            throw new NotImplementedException();
        }

        private List<Tuple<int, int, int>> Flatten2DimArrayWithIndexes(int[][] arr)
        {
            var flatten = new List<Tuple<int, int, int>>();
            for (int x = 0; x < arr.Count(); x++)
            {
                for (int y = 0; y < arr[x].Count(); y++)
                {
                    flatten.Add(new Tuple<int, int, int>(arr[x][y], x, y));
                }
            }
            return flatten;
        }
    }
}
