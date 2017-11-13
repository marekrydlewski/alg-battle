using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using AlgBattle.Benchmarks;
using AlgBattle.DataReaders;
using AlgBattle.Solvers;

namespace AlgBattle.Utils
{
    public class TestExecutioner
    {
        public void RunTest(int reps =  5)
        {
            var outputNameTime = "chrOutput_time.csv";
            var outputNameScore = "chrOutput_score.csv";
            List<string> files1 = new List<string>{ "bur26h", "chr15a", "chr18a", "chr20a", "chr22a", "chr25a"};
            var bench = new QapSolutionBenchmark();
            var outputScore = new int [files1.Count, 5];
            var outputTime = new double [files1.Count, 5];

            for (int i = 0; i < files1.Count;  ++i)
            {
                string s = files1[i];
                var qapDataReader = new QapDataFileReader();
                var data = qapDataReader.ReadData(@"../AlgBattle/Data/BaseData/" + s + ".dat");
                var solution = qapDataReader.ReadSolution(@"../AlgBattle/Data/BaseData/" + s + ".sln");
                var algorithms = new List<QapSolver> { new QapRandomSolver(data), new QapHeuristicSolver(data), new QapGreedyLocalSolver(data), new QapGreedyLocalSolver(data), new QapSteepestLocalSolver(data) };

                Stopwatch sw = new Stopwatch();
                int mediumRate = 0;

                for (int a = 0; a < algorithms.Count; ++a)
                {
                    var algorithm = algorithms[a];
                    for (int j = 0; j < reps; j++)
                    {
                        var randomSolver = new QapRandomSolver(data);
                        sw.Start();
                        var randomSolution = randomSolver.GetSolution();
                        sw.Stop();
                        int rate = bench.RateSolution(randomSolution, data);
                        mediumRate += rate;
                    }
                    mediumRate /= reps;
                    var mediumTime = sw.Elapsed / reps;
                    sw.Reset();

                    //save
                    outputScore[i, a] = mediumRate;
                    outputTime[i, a] = mediumTime.TotalMilliseconds;
                }
            }

            using (StreamWriter file = File.AppendText(outputNameTime))
            {
                for (int i = 0; i < 5; ++i)
                {
                    for (int j = 0; j < files1.Count; ++j)
                    {
                        file.Write(outputTime[j, i] + ",");
                    }
                    file.Write("\n");
                }
            }

            using (StreamWriter file = File.AppendText(outputNameScore))
            {
                for (int i = 0; i < 5; ++i)
                {
                    for (int j = 0; j < files1.Count; ++j)
                    {
                        file.Write(outputScore[j, i] + ",");
                    }
                    file.Write("\n");
                }
            }
        }
    }
}
