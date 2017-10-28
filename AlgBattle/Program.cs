using AlgBattle.DataReaders;
using System;
using System.Diagnostics;
using System.Linq;
using AlgBattle.Solvers;

namespace AlgBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AlgBattle - algorithms comparison in QAP problem");
            var qapDataReader = new QapDataFileReader();
            var data = qapDataReader.ReadData(@"Data/BaseData/bur26a.dat");
            var solution = qapDataReader.ReadSolution(@"Data/BaseData/bur26a.sln");
            //caution: some data files have got only instances without solutions
            //var fullData = qapDataReader.LoadDirectory(@"Data/BaseData");
            //var initializedData = fullData.ToList();
            Console.WriteLine($"Optimal::: {string.Join(" ", solution.Solution)}");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var randomSolver = new QapRandomSolver(data);
            var randomSolution = randomSolver.GetSolution();
            Console.WriteLine($"Random:::: {string.Join(" ", randomSolution)}");
            var heuristicSolver = new QapHeuristicSolver(data);
            var heuristicSolution = heuristicSolver.GetSolution();
            Console.WriteLine($"Heurstic:: {string.Join(" ", heuristicSolution)}");
            sw.Stop();
            Console.WriteLine($"Elapsed medium time: {sw.Elapsed }");
            Console.ReadLine();
        }
    }
}