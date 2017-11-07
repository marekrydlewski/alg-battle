using System;
using System.Collections.Generic;
using System.Text;
using AlgBattle.DataReaders;
using AlgBattle.Benchmarks;

namespace AlgBattle.Solvers
{
    public class QapSteepestLocalSolver : QapSolver
    {
        public QapSteepestLocalSolver(QapData data) : base(data)
        {
        }

        public override int[] GetSolution()
        {
            QapSolution solution = new QapSolution
            {
                Size = Data.Distances.Length,
                Solution = this.GetList(this.GetRandomInitSolution())
            };
            QapSolutionBenchmark benchmark = new QapSolutionBenchmark(Data, solution);
            bool isLocalMinimum = false;
            while (!isLocalMinimum)
            {
                if (!CheckBestNeighbor(benchmark))
                {
                    isLocalMinimum = true;
                }
            }
            return benchmark.ActualBestSolution.Solution.ToArray();
        }

        private bool CheckBestNeighbor(QapSolutionBenchmark benchmark)
        {
            int bestI = -1;
            int bestJ = -1;
            int bestScore = benchmark.ActualBestSolution.Score;

            for (int i = 0; i < benchmark.ActualBestSolution.Size - 2; i++)
            {
                for (int j = i + 1; i < benchmark.ActualBestSolution.Size - 1; i++)
                {
                    int neighborScore = benchmark.RateSolutionChange(i, j);
                    if (neighborScore>bestScore)
                    {
                        bestScore = neighborScore;
                        bestI = i;
                        bestJ = j;                        
                    }
                }
            }
            if (bestJ == -1 || bestI == -1)
            {
                return false;
            }
            else
            {
                benchmark.ChangeSolution(bestI, bestJ);
                return true;
            }            
        }
    }
}
