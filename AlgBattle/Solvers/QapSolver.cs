using System;
using System.Linq;
using AlgBattle.DataReaders;
using AlgBattle.Benchmarks;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AlgBattle.Solvers
{
    public abstract class QapSolver
    {
        public QapData Data { get; set; }

        public RandomGenerator Rnd { get; set; } = new RandomGenerator();

        public QapSolutionBenchmark SolutionBenchmark { get; set; } = new QapSolutionBenchmark();

        public QapSolver(QapData data)
        {
            this.Data = data;
        }

        public abstract int[] GetSolution();

        public int[] GetRandomInitSolution()
        {
            var init = Enumerable.Range(0, Data.Size).ToArray();
            Rnd.Shuffle(init);
            return init;
        }

        public int[] GetGreedyInitSolution()
        {
            int[] solution = Enumerable.Repeat(-1, Data.Size).ToArray();
            var flows = Flatten2DimArrayWithIndexes(Data.Flows).Where(x => x.Item2 != x.Item3).ToList();
            var distances = Flatten2DimArrayWithIndexes(Data.Distances).Where(x => x.Item2 != x.Item3).ToList();

            flows.Sort((a, b) => b.Item1.CompareTo(a.Item1)); //descending
            distances.Sort((a, b) => a.Item1.CompareTo(b.Item1)); //ascending
            var costs = flows.Zip(distances, (f, d) => new {
                Cost = f.Item1 * d.Item1,
                DistanceX = d.Item2,
                DistanceY = d.Item3,
                FlowX = f.Item2,
                FlowY = f.Item3
            }).ToList();

            //first 2 assignments
            int currMin = costs[0].Cost;
            var candidates = costs.TakeWhile(x => x.Cost == currMin).ToList();
            var currMove = Rnd.RandomElement(candidates);
            solution[currMove.DistanceX] = currMove.FlowX;
            solution[currMove.DistanceY] = currMove.FlowY;

            //n - 2 assignments
            var remainingFacilities = Enumerable.Range(0, Data.Size).Where(x => x != currMove.FlowX && x != currMove.FlowY).ToList();
            int alreadyInserted = 2;
            while (alreadyInserted < solution.Length)
            {
                int minCost = 100000000;
                int newLocation = -1, newFacility = -1;
                for (int f = 0; f < remainingFacilities.Count; ++f)
                {
                    for (int l = 0; l < solution.Length; ++l)
                    {
                        if (solution[l] == -1)
                        {
                            int proposedCost = SolutionBenchmark.RateInsert(solution, Data, remainingFacilities[f], l);
                            if (proposedCost < minCost)
                            {
                                minCost = proposedCost;
                                newLocation = l;
                                newFacility = remainingFacilities[f];
                            }
                        }
                    }
                }
                solution[newLocation] = newFacility;
                alreadyInserted++;
                remainingFacilities.Remove(newFacility);
            }
            return solution;
        }

        private List<Tuple<int, int, int>> Flatten2DimArrayWithIndexes(int[][] arr)
        {
            var flatten = new List<Tuple<int, int, int>>();
            for (int x = 0; x < arr.Length; x++)
            {
                for (int y = 0; y < arr[x].Length; y++)
                {
                    flatten.Add(new Tuple<int, int, int>(arr[x][y], x, y));
                }
            }
            return flatten;
        }
    }
}
