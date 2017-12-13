using AlgBattle.DataReaders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AlgBattle.Benchmarks;
using AlgBattle.Solvers;
using AlgBattle.Utils;
using System.IO;

namespace AlgBattle
{
    class Program
    {
        static void Main(string[] args)
        {

            /*//test annealing 
            Console.WriteLine("AlgBattle - algorithms comparison in QAP problem");
            var qapDataReader = new QapDataFileReader();
            var data = qapDataReader.ReadData(@"Data/BaseData/tai60a.dat");
            var solution = qapDataReader.ReadSolution(@"Data/BaseData/tai60a.sln");
            var bench = new QapSolutionBenchmark();
            LocalOptimumValidator validator = new LocalOptimumValidator();
            Console.WriteLine($"Optimal::: {string.Join(" ", solution.Solution)}");
            Console.WriteLine($"Fitness:::: {solution.Score}");
            Console.WriteLine($"Fitness Ours:::: {bench.RateSolutionIndexedFromZero(solution.Solution.ToArray(), data)}");
            Console.WriteLine($"Is local optimum::: {validator.CheckLocalOptimum(solution.Solution.ToArray(), data, true)}");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var randomSolver = new QapRandomSolver(data);
            var randomSolution = randomSolver.GetSolution();
            Console.WriteLine($"Random:::: {string.Join(" ", randomSolution)}");
            Console.WriteLine($"Fitness:::: {bench.RateSolution(randomSolution, data)}");
            Console.WriteLine($"Is local optimum::: {validator.CheckLocalOptimum(randomSolution, data, false)}");

            var annealingSolver = new QapAnnealingSolver(data);
            var annealingSolution = annealingSolver.GetSolution();
            Console.WriteLine($"Annealing:: {string.Join(" ", annealingSolution)}");
            Console.WriteLine($"Fitness:::: {bench.RateSolution(annealingSolution, data)}");
            Console.WriteLine($"Is local optimum::: {validator.CheckLocalOptimum(annealingSolution, data, false)}");

            var tabuSolver = new QapTabuSolver(data);
            var tabuSolution = tabuSolver.GetSolution();
            Console.WriteLine($"Tabu:: {string.Join(" ", tabuSolution)}");
            Console.WriteLine($"Fitness:::: {bench.RateSolution(tabuSolution, data)}");
            Console.WriteLine($"Is local optimum::: {validator.CheckLocalOptimum(tabuSolution, data, false)}");

            var heuristicSolver = new QapHeuristicSolver(data);
            var heuristicSolution = heuristicSolver.GetSolution();
            Console.WriteLine($"Heurstic:: {string.Join(" ", heuristicSolution)}");
            Console.WriteLine($"Fitness:::: {bench.RateSolution(heuristicSolution, data)}");
            Console.WriteLine($"Is local optimum::: {validator.CheckLocalOptimum(heuristicSolution, data, false)}");


            var steepestSolver = new QapSteepestLocalSolver(data);
            var steepestSolution = steepestSolver.GetSolution();
            Console.WriteLine($"Steepest:: {string.Join(" ", steepestSolution)}");
            Console.WriteLine($"Fitness:::: {bench.RateSolution(steepestSolution, data)}");
            Console.WriteLine($"Is local optimum::: {validator.CheckLocalOptimum(steepestSolution, data, false)}");

            var greedySolver = new QapGreedyLocalSolver(data);
            var greedySolution = greedySolver.GetSolution();
            Console.WriteLine($"Greedy:: {string.Join(" ", greedySolution)}");
            Console.WriteLine($"Fitness:::: {bench.RateSolution(greedySolution, data)}");
            Console.WriteLine($"Is local optimum::: {validator.CheckLocalOptimum(greedySolution, data, false)}");

            sw.Stop();
            Console.WriteLine($"Elapsed medium time: {sw.Elapsed }");
            Console.ReadLine();
            */
            //instancje do analizy
            // chr25a - duze roznice random vs reszta, naiwne niezle
            IList<string> taiNames = new List<string> { "tai15b", "tai20b","tai25b", "tai30b", "tai35b", "tai40b", "tai50b", "tai60b", "tai80b" };
            var test = new TestExecutioner();
            var timerAll = new Stopwatch();
            timerAll.Start();
            test.RunTest(20, "taiOutput", taiNames);
            timerAll.Stop();
            Console.WriteLine(timerAll.Elapsed.Milliseconds);


            //////////////optimal list
            /*IList<string> taiNames = new List<string> { "tai15b", "tai20b", "tai25b", "tai30b", "tai35b", "tai40b", "tai50b", "tai60b", "tai80b" };
            List<ulong> list = new List<ulong>();
            foreach (string name in taiNames)
            {
                var qapDataReader = new QapDataFileReader();
                var data = qapDataReader.ReadData(@"../AlgBattle/Data/BaseData/" + name + ".dat");
                var optimalSolution = qapDataReader.ReadSolution(@"../AlgBattle/Data/BaseData/" + name + ".sln");
                list.Add((ulong)optimalSolution.Score);
            }
            using (StreamWriter file = File.AppendText("optimal_solutions"))
            {
                foreach (ulong line in list)
                {
                    file.WriteLine(line + ';');
                }
            }*/

            ////////// test first vs last result "tai15b", "tai20b", "tai25b", "tai30b", "tai35b",, "chr20b", "els19", "esc16a", "esc16j"
            /*IList<string> taiNames = new List<string> { "bur26f", "tai15b", "chr12b", "chr20a", "esc16j"};
            foreach (string name in taiNames){
                FirstVsLastResultTester test = new FirstVsLastResultTester(name, 300);
                test.run();
            }*/

            ////////// test repeating
            /*IList<string> taiNames2 = new List<string> { "tai15a", "tai20b", "chr12a" };
            foreach (string name in taiNames2)
            {
                RepeatingTest test = new RepeatingTest(name, 200);
                test.run();
            }*/

            //IList<string> taiNames = new List<string> { "tai30b", "chr22b", "lipa50b"};
            //for(int i = 0; i <10; i++)
            //{
            //    foreach (string name in taiNames)
            //    {
            //        EfficiencyMeter test = new EfficiencyMeter();
            //        test.MaxRepetitionsWithoutImprove = 150;
            //        test.InstanceName = name;
            //        test.run();
            //    }
            //}

            /*IList<string> taiNames = new List<string> { "tai30b", "chr22b", "lipa50b" };
            foreach (string name in taiNames)
            {
                EfficiencyMeter test = new EfficiencyMeter();
                test.MaxRepetitionsWithoutImprove = 150;
                test.InstanceName = name;
                test.Repetitions = 10;
                test.run();
            }     */
        }
    }
}