using AlgBattle.DataReaders;
using AlgBattle.Solvers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AlgBattle.Benchmarks
{
    class EfficiencyMeter
    {
        public String InstanceName { get; set; }
        public int MaxRepetitionsWithoutImprove{ get; set; }        

        public void run()
        {
            string outputFileName = InstanceName + ".csv";
            var qapDataReader = new QapDataFileReader();
            QapSolutionBenchmark benchmark = new QapSolutionBenchmark();
            var data = qapDataReader.ReadData(@"../AlgBattle/Data/BaseData/" + InstanceName + ".dat");
            var optimalSolution = qapDataReader.ReadSolution(@"../AlgBattle/Data/BaseData/" + InstanceName + ".sln");
            Console.WriteLine("Start processing");
            QapSteepestLocalSolver sSolver = new QapSteepestLocalSolver(data);
            RunSolver(sSolver, data, benchmark, optimalSolution, "efficiency_steepest_" + outputFileName);
            Console.WriteLine("Steepest done");
            QapGreedyLocalSolver gSolver = new QapGreedyLocalSolver(data);
            RunSolver(gSolver, data, benchmark, optimalSolution, "efficiency_greedy_" + outputFileName);
            Console.WriteLine("greedy done");
            QapAnnealingSolver aSolver = new QapAnnealingSolver(data);
            RunSolver(aSolver, data, benchmark, optimalSolution, "efficiency_annealing_" + outputFileName);
            Console.WriteLine("annealing done");
            QapTabuSolver tSolver = new QapTabuSolver(data);
            RunSolver(tSolver, data, benchmark, optimalSolution, "efficiency_tabu_" + outputFileName);
            Console.WriteLine("tabu done");
        }

        private void RunSolver(QapSolver solver, QapData data, QapSolutionBenchmark benchmark, QapSolution optimalSolution, String outputFileName)
        {
            ulong bestSolutionScore = 0;
            int repetitionsWithoutProgress = 0;
            List<double[]> list = new List<double[]>();
            Stopwatch sw = new Stopwatch();
            while (true)
            {
                sw.Start();
                var lastSolution = solver.GetSolution();
                sw.Stop();
                var lastSolutionScore = benchmark.RateSolution(lastSolution, data);
                if ((bestSolutionScore == 0) || (bestSolutionScore > lastSolutionScore))
                {
                    bestSolutionScore = lastSolutionScore;
                    repetitionsWithoutProgress = 0;
                }
                else
                {
                    repetitionsWithoutProgress++;
                }
                var efficiency = bestSolutionScore / (ulong)optimalSolution.Score;
                var time = sw.Elapsed.TotalMilliseconds;
                list.Add(new double[] { efficiency, time});

                if (repetitionsWithoutProgress > MaxRepetitionsWithoutImprove) break;
                if (efficiency > 0.99999999999999999999) break;
            }

            using (StreamWriter file = File.AppendText(outputFileName))
            {
                foreach (double[] line in list)
                {
                    file.WriteLine(line[0].ToString() + ';' + line[1].ToString() + ';');
                }
            }
        }
    }
}
