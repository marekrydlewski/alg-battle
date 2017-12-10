using AlgBattle.Benchmarks;
using AlgBattle.DataReaders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgBattle.Solvers
{
    public class QapTabuSolver : QapSolver
    {    
        public int[][] Memory { get; set; }

        public int LengthOfMemory { get; set; }

        public QapTabuSolver(QapData data) : base(data)
        {
            Memory = CreateMemory(data.Size);
        }

        public override int[] GetSolution()
        {
            var size = this.Data.Size;
            LengthOfMemory = 5;

            QapSolution solution = new QapSolution
            {
                Size = Data.Distances.Length,
                Solution = this.GetList(this.GetRandomInitSolution())
            };
            FirstSolution = solution.Solution.ToArray();
            DeltaSolutionBenchmark benchmark = new DeltaSolutionBenchmark(Data, solution);


            throw new NotImplementedException();
        }

        private int[][] CreateMemory(int size)
        {
            var memory = new int[size][];
            for (int i =0; i < size; ++i)
            {
                memory[i] = new int[size];
            }
            //zeros
            return memory;
        }

        private void ZeroMemory()
        {
            for (int i = 0; i < Memory.Length; ++i)
            {
                for(int j = 0; j < Memory[i].Length; ++j)
                {
                    Memory[i][j] = 0;
                }
            }
        }

        private void TickMemoryDown()
        {
            for (int i = 0; i < Memory.Length; ++i)
            {
                for (int j = 0; j < Memory[i].Length; ++j)
                {
                    if (Memory[i][j] != 0)
                        Memory[i][j]--;
                }
            }
        }

        public override int GetSwapCounter()
        {
            throw new NotImplementedException();
        }
    }
}
