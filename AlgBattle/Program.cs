using AlgBattle.DataReaders;
using System;
using System.Diagnostics;
using System.Linq;
using AlgBattle.Benchmarks;
using AlgBattle.Solvers;

namespace AlgBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AlgBattle - algorithms comparison in QAP problem");
            var qapDataReader = new QapDataFileReader();
            var data = qapDataReader.ReadData(@"Data/BaseData/bur26b.dat");
            var solution = qapDataReader.ReadSolution(@"Data/BaseData/bur26b.sln");
            var bench = new QapSolutionBenchmark();
            LocalOptimumValidator validator = new LocalOptimumValidator();
            //caution: some data files have got only instances without solutions
            //var fullData = qapDataReader.LoadDirectory(@"Data/BaseData");
            //var initializedData = fullData.ToList();
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
        }
    }
}