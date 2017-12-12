using AlgBattle.DataReaders;
using AlgBattle.Solvers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace AlgBattle.Benchmarks
{
    public class RepeatingTest
    {
        public String InstanceName { get; set; }
        public int RepetitionsNo { get; set; }
        public QapData Data { get; set; }

        public RepeatingTest(String instanceName, int repetitionsNo)
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
            runSolver(sSolver, data, benchmark, optimalSolution, "repeating_steepest_" + outputFileName);
            QapGreedyLocalSolver gSolver = new QapGreedyLocalSolver(data);
            runSolver(gSolver, data, benchmark, optimalSolution, "repeating_greedy_" + outputFileName);
            QapAnnealingSolver aSolver = new QapAnnealingSolver(data);
            runSolver(aSolver, data, benchmark, optimalSolution, "repeating_annealing_" + outputFileName);
            QapTabuSolver tSolver = new QapTabuSolver(data);
            runSolver(tSolver, data, benchmark, optimalSolution, "repeating_tabu_" + outputFileName);

        }

        private void runSolver(QapSolver solver, QapData data, QapSolutionBenchmark benchmark, QapSolution optimalSolution, String outputFileName)
        {
            List<long> solutionsScore = new List<long>();
            ulong bestSolutionScore = 0;
            //LocalOptimumValidator validator = new LocalOptimumValidator();
            List<ulong[]> list = new List<ulong[]>();
            for (int i = 0; i < RepetitionsNo; i++)
            {   
                var lastSolution = solver.GetSolution();                  
                var lastSolutionScore = benchmark.RateSolution(lastSolution, data);
                if ((bestSolutionScore == 0) || (bestSolutionScore > lastSolutionScore))
                {
                    bestSolutionScore = lastSolutionScore;
                }
                solutionsScore.Add(((long)lastSolutionScore));

                list.Add(new ulong[] { Convert.ToUInt64(optimalSolution.Score), lastSolutionScore,  (ulong)solutionsScore.Average(), bestSolutionScore});
            }

            using (StreamWriter file = File.AppendText(outputFileName))
            {
                foreach (ulong[] line in list)
                {
                    file.WriteLine(line[0].ToString() + ',' + line[1].ToString() + ',' + line[2].ToString() + ',' + line[3].ToString() + ';');
                }
            }
        }
    }
}
