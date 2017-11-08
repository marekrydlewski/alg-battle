using AlgBattle.DataReaders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgBattle.Benchmarks
{
    class LocalOptimumValidator
    {        
        public bool CheckLocalOptimum(int[] solution, QapData data)
        {
            int size = data.Distances.Length;
            QapSolutionBenchmark benchmark = new QapSolutionBenchmark();
            var bestFitness = benchmark.RateSolution(solution, data);
            for (int i = 0; i < size- 2; i++)
            {
                for (int j = i + 1; i < size- 1; i++)
                {
                    var tempSolution = solution;
                    var temp = tempSolution[i];
                    tempSolution[i] = tempSolution[j];
                    tempSolution[j] = temp;
                    var fitness = benchmark.RateSolution(tempSolution, data);
                    if (fitness < bestFitness)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
