using AlgBattle.DataReaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AlgBattle.Benchmarks
{
    public class QapSolutionBenchmark
    {
        public int RateSolution(int[] sol, QapData data)
        {
            //indexes are locations, values means facilites
            //distances of locations, flows of facilites
            int fitness = 0;
            for (int i = 0; i < sol.Count(); ++i)
            {
                for (int j = i + 1; j < sol.Count(); ++j)
                {
                    int xi = sol[i];
                    int xj = sol[j];
                    fitness += data.Distances[i][j] * data.Flows[xi][xj];
                }
            }
            return fitness;
        }

        public int RateSolution(int[] sol, QapData data, int currentCost, int swapX, int swapY)
        {
            throw new NotImplementedException();
        }

        public int RateInsert(int[] sol, QapData data, int facility, int location)
        {
            int cost = 0;
            for (int i = 0; i < sol.Length; ++i)
            {
                if (sol[i] != -1)
                {
                    cost += data.Distances[i][location] * data.Flows[sol[i]][facility];
                }
            }
            return cost;
        }
    }
}
