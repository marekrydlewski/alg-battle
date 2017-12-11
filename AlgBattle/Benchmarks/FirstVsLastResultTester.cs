using AlgBattle.DataReaders;
using AlgBattle.Solvers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AlgBattle.Benchmarks
{
    public class FirstVsLastResultTester
    {
        public String InstanceName { get; set; }
        public int RepetitionsNo { get; set; }
        public QapData Data { get; set; }

        public FirstVsLastResultTester(String instanceName, int repetitionsNo)
        {
            InstanceName = instanceName;
            RepetitionsNo = repetitionsNo;
        }

        public void run()
        {
            string outputFileName = InstanceName + ".csv";
            var qapDataReader = new QapDataFileReader();
            QapSolutionBenchmark benchmark = new QapSolutionBenchmark();
            var data = qapDataReader.ReadData(@"../AlgBattle/Data/BaseData/" + InstanceName + ".dat");
            var optimalSolution = qapDataReader.ReadSolution(@"../AlgBattle/Data/BaseData/" + InstanceName + ".sln");
            QapSteepestLocalSolver sSolver = new QapSteepestLocalSolver(data);
            runSolver(sSolver, data, benchmark, optimalSolution, "firstVsLastsResult_steepest_" + outputFileName);
            QapGreedyLocalSolver gSolver = new QapGreedyLocalSolver(data);
            runSolver(gSolver, data, benchmark, optimalSolution, "firstVsLastsResult_greedy_" + outputFileName);
            QapAnnealingSolver aSolver = new QapAnnealingSolver(data);
            runSolver(aSolver, data, benchmark, optimalSolution, "firstVsLastsResult_annealing_" + outputFileName);
            QapTabuSolver tSolver = new QapTabuSolver(data);
            runSolver(tSolver, data, benchmark, optimalSolution, "firstVsLastsResult_tabu_" + outputFileName);
        }

        private void runSolver(QapSolver solver, QapData data, QapSolutionBenchmark benchmark, QapSolution optimalSolution, String outputFileName)
        {
            //LocalOptimumValidator validator = new LocalOptimumValidator();
            List<ulong[]> list = new List<ulong[]>();
            for (int i = 0; i < RepetitionsNo; i++)
            {

                var lastSolution = solver.GetSolution();
                //Console.WriteLine(validator.CheckLocalOptimum(lastSolution, data, false));
                var firstSolution = solver.FirstSolution;
                var lastSolutionScore = benchmark.RateSolution(lastSolution, data);
                var firstSolutionScore = benchmark.RateSolution(firstSolution, data);

                list.Add(new ulong[] { Convert.ToUInt64(optimalSolution.Score), firstSolutionScore, lastSolutionScore });
            }

            using (StreamWriter file = File.AppendText(outputFileName))
            {
                foreach (ulong[] line in list)
                {                    
                    file.WriteLine(line[0].ToString() + ',' + line[1].ToString() + ',' + line[2].ToString() + ';');
                }
            }
        }
    }
}
