﻿using AlgBattle.Benchmarks;
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
            LengthOfMemory = 5;
        }

        public override int[] GetSolution()
        {
            var size = this.Data.Size;

            QapSolution solution = new QapSolution
            {
                Size = Data.Distances.Length,
                Solution = this.GetList(this.GetRandomInitSolution())
            };
            FirstSolution = solution.Solution.ToArray();
            DeltaSolutionBenchmark benchmark = new DeltaSolutionBenchmark(Data, solution);

            var solutions = new SortedList<int, Tuple<int, int>>();
            var currSolution = FirstSolution;
            var bestScore = benchmark.ActualBestSolution.Score;
            var bestSolution = new List<int>();

            while(true)
            {
                for (int i = 0; i < benchmark.ActualBestSolution.Size - 2; i++)
                {
                    for (int j = i + 1; j < benchmark.ActualBestSolution.Size - 1; j++)
                    {
                        int neighborScore = benchmark.RateSolutionChange(i, j);
                        solutions.Add(neighborScore, Tuple.Create(i, j));
                    }
                }

                bool changed = false;
                foreach (var entry in solutions)
                {
                    //if legal
                    var x = entry.Value.Item1;
                    var y = entry.Value.Item2;
                    var proposedScore = entry.Key;
                    if (Memory[x][y] == 0 || proposedScore < bestScore)
                    {
                        Memory[x][y] = LengthOfMemory;
                        Memory[y][x] = LengthOfMemory;

                        benchmark.ChangeSolution(x, y);
                        //swap
                        int temp = currSolution[x];
                        currSolution[x] = currSolution[y];
                        currSolution[y] = temp;
                        changed = true;
                        if(benchmark.ActualBestSolution.Score < bestScore)
                        {
                            bestSolution = benchmark.ActualBestSolution.Solution;
                        }
                        break;
                    }
                }
                if (!changed)
                {
                    //taki co najmniej psuje
                    var x = solutions.Values[0].Item1;
                    var y = solutions.Values[0].Item2;

                    Memory[x][y] = LengthOfMemory;
                    Memory[y][x] = LengthOfMemory;

                    benchmark.ChangeSolution(x, y);
                    //swap
                    int temp = currSolution[x];
                    currSolution[x] = currSolution[y];
                    currSolution[y] = temp;
                }
                TickMemoryDown();
            }

            return bestSolution.ToArray();
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
