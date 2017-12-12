using AlgBattle.Benchmarks;
using AlgBattle.DataReaders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgBattle.Solvers
{
    public class QapAnnealingSolver : QapSolver
    {
        public double Temp { get; set; }

        public double TempMin { get; set; }

        public double TempZero { get; set; }

        public QapAnnealingSolver(QapData data) : base(data)
        {
        }

        public override int[] GetSolution()
        {
            var currSolution = this.GetRandomInitSolution();
            //var currScore = this.SolutionBenchmark.RateSolution(currSolution, Data);
            int currScore = 0;
            QapSolution solution = new QapSolution
            {
                Size = Data.Distances.Length,
                Solution = this.GetList(currSolution)
            };
            FirstSolution = solution.Solution.ToArray();
            DeltaSolutionBenchmark benchmark = new DeltaSolutionBenchmark(Data, solution);
            int bestEver = Int32.MaxValue;
            TempZero = currSolution.Length * 800;
            Temp = TempZero;
            TempMin = 1;
            Steps = 10;
            while(Temp > TempMin)
            {
                for(int i = 0; i < 10; i++)
                {
                    int x = this.Rnd.Rnd.Next(0, currSolution.Length);
                    int y = this.Rnd.Rnd.Next(0, currSolution.Length);

                    currScore = benchmark.ActualBestSolution.Score;
                    var proposedScore = benchmark.RateSolutionChange(x, y);
                    if (proposedScore < bestEver) bestEver = proposedScore;
                    if (AcceptSolution(proposedScore, currScore, Temp))
                    {
                        benchmark.ChangeSolution(x, y);
                        //swap
                        int temp = currSolution[x];
                        currSolution[x] = currSolution[y];
                        currSolution[y] = temp;
                    }
                    this.CheckedElems++;
                }
                Steps++;
                if (Temp < 200)
                {
                    UpdateTemp();
                }
                else if (Temp < 10000)
                {
                    UpdateTemp3();
                }
                else
                {
                    UpdateTemp4();
                }
                //UpdateTemp();

                //Console.WriteLine(Temp);
            }
            //Console.WriteLine(bestEver);
            return currSolution;
        }

        public void UpdateTemp()
        {
            Temp = (0.99 * Temp);
        }

        public void UpdateTemp2()
        {
            Temp = (Math.Pow(0.82, Steps) * TempZero);
        }

        public void UpdateTemp3()
        {
            Temp = 10000 / (1 + 8 * Math.Log(1 + Steps));
        }

        public void UpdateTemp4()
        {
            Temp = (0.82 * Temp);
        }



        public bool AcceptSolution(int newScore, int oldScore, double divider)
        {
            if (newScore <= oldScore)
                return true;
            else
            {
                var boltzmann = Math.Exp(-(newScore - oldScore) / divider);
                var r = this.Rnd.Rnd.NextDouble();
                //Console.WriteLine(boltzmann);
                return r < boltzmann;
            }
        }

        public override int GetSwapCounter()
        {
            throw new NotImplementedException();
        }
    }
}
