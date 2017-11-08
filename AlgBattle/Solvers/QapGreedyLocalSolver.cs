using System;
using System.Collections.Generic;
using System.Text;
using AlgBattle.DataReaders;
using AlgBattle.Benchmarks;

namespace AlgBattle.Solvers
{
    public class QapGreedyLocalSolver: QapSolver
    {
        public QapGreedyLocalSolver(QapData data) : base(data)
        {
        }

        public override int[] GetSolution()
        {
            QapSolution solution = new QapSolution
            {
                Size = Data.Distances.Length,
                Solution = this.GetList(this.GetRandomInitSolution())
            };
            DeltaSolutionBenchmark benchmark = new DeltaSolutionBenchmark(Data, solution);
            bool isLocalMinimum = false;
            while (!isLocalMinimum)
            {
                if (!CheckIfBetterNeighborExist(benchmark))
                {
                    isLocalMinimum = true;
                }
            }
            return benchmark.ActualBestSolution.Solution.ToArray();
        }

        private bool CheckIfBetterNeighborExist(DeltaSolutionBenchmark benchmark)
        {
            for (int i = 0; i < benchmark.ActualBestSolution.Size - 2; i++)
            {
                for (int j = i + 1; i < benchmark.ActualBestSolution.Size - 1; i++)
                {
                    if (benchmark.CheckIfSolutionChangeIsBetter(i, j))
                    {
                        benchmark.ChangeSolution(i, j);
                        return true;                        
                    }
                }
            }
            return false;
        }
    }
}
