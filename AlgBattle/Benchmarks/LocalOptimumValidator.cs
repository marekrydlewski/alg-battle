using AlgBattle.DataReaders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgBattle.Benchmarks
{
    class LocalOptimumValidator
    {        
        public bool CheckLocalOptimum(int[] solution, QapData data, bool startZeroIndex)
        {
            int size = data.Distances.Length;
            QapSolutionBenchmark benchmark = new QapSolutionBenchmark();
            var bestFitness = 0.0;
            if (startZeroIndex)
            {
                bestFitness = benchmark.RateSolutionIndexedFromZero(solution, data);
            }
            else
            {
                bestFitness = benchmark.RateSolution(solution, data);
            }            
            for (int i = 0; i < size- 2; i++)
            {
                for (int j = i + 1; i < size- 1; i++)
                {
                    var tempSolution = solution;
                    var temp = tempSolution[i];
                    tempSolution[i] = tempSolution[j];
                    tempSolution[j] = temp;
                    var fitness = 0.0;
                    if (startZeroIndex)
                    {
                        fitness = benchmark.RateSolutionIndexedFromZero(tempSolution, data);
                    }
                    else
                    {
                        fitness = benchmark.RateSolution(tempSolution, data);
                    }
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
