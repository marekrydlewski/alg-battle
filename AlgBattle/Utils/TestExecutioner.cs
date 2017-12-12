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
    public static class Extend
    {
        public static double StandardDeviation(this IEnumerable<int> values)
        {
            double avg = values.Average();
            return Math.Sqrt(values.Average(v => Math.Pow(v - avg, 2)));
        }
    }

    public class TestExecutioner
    {
        private static readonly IList<string> FileNames = new List<string> { "tai12a", "tai15a", "tai18a", "tai20a", "tai22a", "tai25a" };

        private ulong GetMedian(List<ulong> numbers)
        {
            int numberCount = numbers.Count();
            int halfIndex = numbers.Count() / 2;
            var sortedNumbers = numbers.OrderBy(n => n);
            ulong median;
            if ((numberCount % 2) == 0)
            {
                median = sortedNumbers.ElementAt(halfIndex) +
                           sortedNumbers.ElementAt((halfIndex - 1) / 2);
            }
            else
            {
                median = sortedNumbers.ElementAt(halfIndex);
            }
            return median;
        }

        private QapSolver GetAlgorithm(int a, QapData data)
        {
            switch (a)
            {
                case 0:
                    return new QapSimpleGreedySolver(data);
                case 1:
                    return new QapRandomSolver(data);
                case 2:
                    return new QapGreedyLocalSolver(data);
                case 3:
                    return new QapSteepestLocalSolver(data);
                case 4:
                    return new QapAnnealingSolver(data);
                case 5:
                    return new QapTabuSolver(data);
            }
            return null;
        }
   
        public void RunTest(int reps =  5, string outputName = "output", IList<string> fileNames = null)
        {
            fileNames = fileNames ?? FileNames;
            var outputNameTime = outputName +  "_time.csv";
            var outputNameScore = outputName + "_score.csv";
            var outputNameMin = outputName + "_score_min.csv";
            var outputNameMax = outputName + "_score_max.csv";
            var outputNameMedian = outputName + "_score_median.csv";
            var outputNameStd = outputName + "_score_std.csv";
            var outputNameSimilarity = outputName + "_score_similarity.csv";

            var outputNameSteps = outputName + "_steps_gs.csv";
            var outputNameCheckedElems = outputName + "_checked_elems_gs.csv";

            var bench = new QapSolutionBenchmark();
            var outputScore = new ulong [fileNames.Count, 6];
            var outputTime = new double [fileNames.Count, 6];

            var outputMin = new ulong[fileNames.Count, 6];
            var outputMax = new ulong[fileNames.Count, 6];
            var outputMedian = new ulong[fileNames.Count, 6];
            var outputStd = new int[fileNames.Count, 6];
            var outputSimilarity = new double[fileNames.Count, 6];

            var outputSteps = new int[fileNames.Count, 4];
            var outputCheckedElems = new int[fileNames.Count, 4];


            for (int i = 0; i < fileNames.Count; ++i)
            {
                for (int j = 0; j < 6; j++)
                {
                    outputMin[i, j] = UInt64.MaxValue;
                    outputMax[i, j] = 0;
                }
            }

            for (int i = 0; i < fileNames.Count;  ++i)
            {
                string s = fileNames[i];
                var qapDataReader = new QapDataFileReader();
                var data = qapDataReader.ReadData(@"../AlgBattle/Data/BaseData/" + s + ".dat");
                var solution = qapDataReader.ReadSolution(@"../AlgBattle/Data/BaseData/" + s + ".sln");
                
                Stopwatch sw = new Stopwatch();
                
                ulong mediumRate = 0;
                double mediumSim = 0;

                for (int a = 0; a < 6; ++a)
                {
                    var algorithm = GetAlgorithm(a, data);
                    Console.WriteLine("File: " + s + "Alg num: " + a);
                    var tempList = new List<ulong>();
                    for (int j = 0; j < reps; j++)
                    {
                        sw.Start();
                        var sol = algorithm.GetSolution();
                        sw.Stop();
                        ulong rate = bench.RateSolution(sol, data);
                        var sim = bench.RateSimilarity(sol, solution.Solution.ToArray());
                        mediumRate += Convert.ToUInt64(rate);
                        mediumSim += sim;
                        if (rate > outputMax[i, a])
                        {
                            outputMax[i, a] = rate;
                        }
                        if (rate < outputMin[i, a])
                        {
                            outputMin[i, a] = rate;
                        }
                        tempList.Add(rate);

                        if (a >= 2) //GS
                        {
                            outputCheckedElems[i, a - 2] = algorithm.CheckedElems;
                            outputSteps[i, a - 2] = algorithm.Steps;
                        }
                    }
                    outputMedian[i, a] = this.GetMedian(tempList);
                    outputStd[i, a] = Convert.ToInt32(tempList.Select(x => Convert.ToInt32(x)).ToList().StandardDeviation());
                    mediumRate /= Convert.ToUInt64(reps);
                    mediumSim /= reps;
                    var mediumTime = sw.Elapsed.TotalMilliseconds / reps;
                    sw.Reset();

                    //save
                    outputScore[i, a] = mediumRate;
                    outputTime[i, a] = mediumTime;
                    outputSimilarity[i, a] = mediumSim;
                }
            }

            using (StreamWriter file = File.AppendText(outputNameTime))
            {
                for (int i = 0; i < 6; ++i)
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
                for (int i = 0; i < 6; ++i)
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
                for (int i = 0; i < 6; ++i)
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
                for (int i = 0; i < 6; ++i)
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
                for (int i = 0; i < 6; ++i)
                {
                    for (int j = 0; j < fileNames.Count; ++j)
                    {
                        file.Write(outputMedian[j, i] + ";");
                    }
                    file.Write("\n");
                }
            }

            using (StreamWriter file = File.AppendText(outputNameStd))
            {
                for (int i = 0; i < 6; ++i)
                {
                    for (int j = 0; j < fileNames.Count; ++j)
                    {
                        file.Write(outputStd[j, i] + ";");
                    }
                    file.Write("\n");
                }
            }

            using (StreamWriter file = File.AppendText(outputNameSimilarity))
            {
                for (int i = 0; i < 6; ++i)
                {
                    for (int j = 0; j < fileNames.Count; ++j)
                    {
                        file.Write(outputSimilarity[j, i] + ";");
                    }
                    file.Write("\n");
                }
            }


            using (StreamWriter file = File.AppendText(outputNameCheckedElems))
            {
                for (int i = 0; i < 4; ++i)
                {
                    for (int j = 0; j < fileNames.Count; ++j)
                    {
                        file.Write(outputCheckedElems[j, i] + ";");
                    }
                    file.Write("\n");
                }
            }

            using (StreamWriter file = File.AppendText(outputNameSteps))
            {
                for (int i = 0; i < 4; ++i)
                {
                    for (int j = 0; j < fileNames.Count; ++j)
                    {
                        file.Write(outputSteps[j, i] + ";");
                    }
                    file.Write("\n");
                }
            }
        }
    }
}
