using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using AlgBattle.Benchmarks;
using AlgBattle.DataReaders;
using AlgBattle.Solvers;

namespace AlgBattle.Utils
{
    public class TestExecutioner
    {
        private static readonly IList<string> FileNames = new List<string> { "chr12a", "chr15a", "chr18a", "chr20a", "chr22a", "chr25a" };

        private int GetMedian(List<int> numbers)
        {
            int numberCount = numbers.Count();
            int halfIndex = numbers.Count() / 2;
            var sortedNumbers = numbers.OrderBy(n => n);
            double median;
            if ((numberCount % 2) == 0)
            {
                median = sortedNumbers.ElementAt(halfIndex) +
                           sortedNumbers.ElementAt((halfIndex - 1) / 2);
            }
            else
            {
                median = sortedNumbers.ElementAt(halfIndex);
            }
            return Convert.ToInt32(median);
        }
   
        public void RunTest(int reps =  5, string outputName = "output", IList<string> fileNames = null)
        {
            fileNames = fileNames ?? FileNames;
            var outputNameTime = outputName +  "_time.csv";
            var outputNameScore = outputName + "_score.csv";
            var outputNameMin = outputName + "_min.csv";
            var outputNameMax = outputName + "_max.csv";
            var outputNameMedian = outputName + "_median.csv";

            var bench = new QapSolutionBenchmark();
            var outputScore = new int [fileNames.Count, 5];
            var outputTime = new double [fileNames.Count, 5];

            var outputMin = new int[fileNames.Count, 5];
            var outputMax = new int[fileNames.Count, 5];
            var outputMedian = new int[fileNames.Count, 5];


            for (int i = 0; i < fileNames.Count; ++i)
            {
                for (int j = 0; j < 5; j++)
                {
                    outputMin[i, j] = Int32.MaxValue;
                    outputMax[i, j] = Int32.MinValue;
                }
            }

            for (int i = 0; i < fileNames.Count;  ++i)
            {
                string s = fileNames[i];
                var qapDataReader = new QapDataFileReader();
                var data = qapDataReader.ReadData(@"../AlgBattle/Data/BaseData/" + s + ".dat");
                var solution = qapDataReader.ReadSolution(@"../AlgBattle/Data/BaseData/" + s + ".sln");
                var algorithms = new List<QapSolver> { new QapRandomSolver(data), new QapHeuristicSolver(data), new QapGreedyLocalSolver(data), new QapGreedyLocalSolver(data), new QapSteepestLocalSolver(data) };

                Stopwatch sw = new Stopwatch();
                int mediumRate = 0;

                for (int a = 0; a < algorithms.Count; ++a)
                {
                    var algorithm = algorithms[a];
                    var tempList = new List<int>();
                    for (int j = 0; j < reps; j++)
                    {
                        var randomSolver = new QapRandomSolver(data);
                        sw.Start();
                        var randomSolution = randomSolver.GetSolution();
                        sw.Stop();
                        int rate = bench.RateSolution(randomSolution, data);
                        mediumRate += rate;
                        if (rate > outputMax[i, a])
                        {
                            outputMax[i, a] = rate;
                        }
                        if (rate < outputMin[i, a])
                        {
                            outputMin[i, a] = rate;
                        }
                        tempList.Add(rate);
                    }
                    outputMedian[i, a] = this.GetMedian(tempList);
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
                    for (int j = 0; j < fileNames.Count; ++j)
                    {
                        file.Write(outputTime[j, i] + ";");
                    }
                    file.Write("\n");
                }
            }

            using (StreamWriter file = File.AppendText(outputNameScore))
            {
                for (int i = 0; i < 5; ++i)
                {
                    for (int j = 0; j < fileNames.Count; ++j)
                    {
                        file.Write(outputScore[j, i] + ";");
                    }
                    file.Write("\n");
                }
            }

            using (StreamWriter file = File.AppendText(outputNameMin))
            {
                for (int i = 0; i < 5; ++i)
                {
                    for (int j = 0; j < fileNames.Count; ++j)
                    {
                        file.Write(outputMin[j, i] + ";");
                    }
                    file.Write("\n");
                }
            }

            using (StreamWriter file = File.AppendText(outputNameMax))
            {
                for (int i = 0; i < 5; ++i)
                {
                    for (int j = 0; j < fileNames.Count; ++j)
                    {
                        file.Write(outputMax[j, i] + ";");
                    }
                    file.Write("\n");
                }
            }

            using (StreamWriter file = File.AppendText(outputNameMedian))
            {
                for (int i = 0; i < 5; ++i)
                {
                    for (int j = 0; j < fileNames.Count; ++j)
                    {
                        file.Write(outputMedian[j, i] + ";");
                    }
                    file.Write("\n");
                }
            }
        }
    }
}
