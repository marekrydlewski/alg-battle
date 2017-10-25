using AlgBattle.DataReaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                for (int j = i; j < sol.Count(); ++j)
                {
                    int xi = sol[i];
                    int xj = sol[j];
                    fitness += data.Distances[i][j] * data.Flows[xi][xj];
                }
            }
            return fitness;
        }
    }
}
