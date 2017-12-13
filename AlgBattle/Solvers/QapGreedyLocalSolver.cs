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
            FirstSolution = solution.Solution.ToArray();
            DeltaSolutionBenchmark benchmark = new DeltaSolutionBenchmark(Data, solution);
            bool isLocalMinimum = false;
            while (!isLocalMinimum)
            {
                if (!CheckIfBetterNeighborExist(benchmark))
                {
                    isLocalMinimum = true;
                }
            }
            //Steps = benchmark.SwapCounter;
            return benchmark.ActualBestSolution.Solution.ToArray();
        }

        public override int GetSwapCounter()
        {
            throw new NotImplementedException();
        }

        private bool CheckIfBetterNeighborExist(DeltaSolutionBenchmark benchmark)
        {
            for (int i = 0; i < benchmark.ActualBestSolution.Size - 1; i++)
            {
                for (int j = i + 1; j < benchmark.ActualBestSolution.Size; j++)
                {
                    CheckedElems++;
                    if (benchmark.CheckIfSolutionChangeIsBetter(i, j))
                    {
                        benchmark.ChangeSolution(i, j);
                        Steps++;
                        return true;                        
                    }
                }
            }
            return false;
        }
    }
}
