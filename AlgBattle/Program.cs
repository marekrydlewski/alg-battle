using AlgBattle.DataReaders;
using System;

namespace AlgBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AlgBattle - algorithms comparison in QAP problem");
            var qapDataReader = new QAPDataFileReader();
            var data = qapDataReader.ReadData(@"Data/BaseData/bur26a.dat");
            var solution = qapDataReader.ReadSolution(@"Data/BaseData/bur26a.sln");
        }
    }
}