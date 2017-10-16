using AlgBattle.DataReaders;
using System;
using System.Diagnostics;
using System.Linq;

namespace AlgBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AlgBattle - algorithms comparison in QAP problem");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var qapDataReader = new QAPDataFileReader();
            var data = qapDataReader.ReadData(@"Data/BaseData/bur26a.dat");
            var solution = qapDataReader.ReadSolution(@"Data/BaseData/bur26a.sln");
            //caution: some data files have got only instances without solutions
            var fullData = qapDataReader.LoadDirectory(@"Data/BaseData");
            var initializedData = fullData.ToList();
            sw.Stop();
            Console.WriteLine($"Elapsed medium time: {sw.Elapsed }");
        }
    }
}