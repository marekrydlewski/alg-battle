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
            var outputName = "chrOutput.csv";
            List<string> files1 = new List<string>{ "chr12.a, chr15.a, chr18a, chr20a, chr22a, chr25a"};
            var bench = new QapSolutionBenchmark();

            foreach (string s in files1)
            {
                var qapDataReader = new QapDataFileReader();
                var data = qapDataReader.ReadData(@"Data/BaseData/" + s + ".dat");
                var solution = qapDataReader.ReadSolution(@"Data/BaseData/" + s + ".sln");
                var algorithms = new List<QapSolver> { new QapRandomSolver(data), new QapHeuristicSolver(data), new QapGreedyLocalSolver(data), new QapGreedyLocalSolver(data), new QapSteepestLocalSolver(data) };
                var output = new List<string[]>();
                Stopwatch sw = new Stopwatch();
                int mediumRate = 0;
                foreach (var algorithm in algorithms)
                {
                    for (int i = 0; i < reps; i++)
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
                }
            }

            using (System.IO.TextWriter writer = File.CreateText(outputName))
            {
                //write
            }
        }
    }
}
