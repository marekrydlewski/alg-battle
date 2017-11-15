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
            //instancje do analizy
            // chr25a - duze roznice random vs reszta, naiwne niezle
            IList<string> taiNames = new List<string> { /*"tai15b", "tai20b","tai25b", "tai30b", "tai35b", "tai40b",*/ "tai50b", "tai60b", "tai80b", /*"tai150b"*/ };
            var test = new TestExecutioner();
            var timerAll = new Stopwatch();
            timerAll.Start();
            test.RunTest(20, "taiOutput", taiNames);
            timerAll.Stop();
            Console.WriteLine(timerAll.Elapsed.Milliseconds);

            ////////// test repeating "tai15b", "tai20b", "tai25b", "tai30b", "tai35b",, "chr20b", "els19", "esc16a", "esc16j"
            /*IList<string> taiNames = new List<string> { "esc16j", "chr12a", "tai15b", "tai20b", "els19", "esc16a", "tai15a" };
            foreach (string name in taiNames)
            {
                RepeatingTest test = new RepeatingTest(name, 300);
                test.run();
            }*/

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
            /*IList<string> taiNames = new List<string> { "esc16j", "chr12a"};
            foreach (string name in taiNames){
                FirstVsLastResultTester test = new FirstVsLastResultTester(name, 300);
                test.run();
            }*/            
        }
    }
}