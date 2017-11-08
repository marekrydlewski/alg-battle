using AlgBattle.DataReaders;
using AlgBattle.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AlgBattle.Benchmarks
{
    public class DeltaSolutionBenchmark
    {
        public int[,] DeltaTable { get; set; }

        public QapData Data { get; set; }

        public QapSolution ActualBestSolution { get; set; }
        
        public int SwapCounter { get; set; }

        public DeltaSolutionBenchmark(QapData data, QapSolution solution)
        {
            QapSolutionBenchmark bench = new QapSolutionBenchmark();
            ActualBestSolution = solution;
            Data = data;
            SwapCounter = 0;
            ActualBestSolution.Score = bench.RateSolution(solution.Solution.ToArray(), data);
            CalcDeltaTable();
        }

        public void ChangeSolution(int p, int q)
        {
            SwapCounter++;            
            ConsoleSpiner.Print(ActualBestSolution.Score.ToString());

            int piP = ActualBestSolution.Solution[p];
            int piQ = ActualBestSolution.Solution[q];

            ActualBestSolution.Score += DeltaTable[p, q];
            SwapValuesInSolution(p, q);
           // DeltaTable[p, q] *= -1;

            int tableSize = Data.Distances.Count();
            for (int i = 0; i < tableSize; i++){
                int piI = ActualBestSolution.Solution[i];
                for (int j = 0; j < tableSize; j++)
                {
                    int piJ = ActualBestSolution.Solution[j];
                    /*if (i!=q && i!=p && j!=p && j!= q)
                    {                        
                        DeltaTable[i, j] = 
                            DeltaTable[i, j] + 
                            (Data.Distances[p][i] - Data.Distances[p][j] + Data.Distances[q][j] - Data.Distances[q][i])*
                            (Data.Flows[piP][piJ] - Data.Flows[piP][piI] + Data.Flows[piQ][piI] - Data.Flows[piQ][piJ])+
                            (Data.Distances[i][p] - Data.Distances[j][p] + Data.Distances[j][q] - Data.Distances[i][q])*
                            (Data.Flows[piJ][piP] - Data.Flows[piI][piP] + Data.Flows[piI][piQ] - Data.Flows[piJ][piQ]);
                    }else*/
                    //if ((i == p && j != q)||(i != p && j == q)||(i == q && j != p)|| (i != q && j == p)||(i==q && j==p))
                    //{                        
                        CalcDelta(tableSize, i, j, piJ, piI);
                    //}
                }
            }
        }

        public bool CheckIfSolutionChangeIsBetter(int swapX, int swapY)
        {
            if (DeltaTable[swapX, swapY]<0) {
                return true;
            }
            else{
                return false;
            }
        }

        public int RateSolutionChange(int swapX, int swapY)
        {
            return ActualBestSolution.Score + DeltaTable[swapX, swapY];
        }     

        private void CalcDelta(int tableSize, int i, int j, int piJ, int piI)
        {
            int partSum = 0;
            for (int g = 0; g < tableSize; g++)
            {
                if (g == i || g == j) continue;
                int piG = ActualBestSolution.Solution[g];
                partSum +=
                    (Data.Distances[g][i] - Data.Distances[g][j]) * (Data.Flows[piG][piJ] - Data.Flows[piG][piI]) +
                    (Data.Distances[i][g] - Data.Distances[j][g]) * (Data.Flows[piJ][piG] - Data.Flows[piI][piG]);
            }
            DeltaTable[i, j] =
                (Data.Distances[i][i] - Data.Distances[j][j]) * (Data.Flows[piJ][piJ] - Data.Flows[piI][piI]) +
                (Data.Distances[i][j] - Data.Distances[j][i]) * (Data.Flows[piJ][piI] - Data.Flows[piI][piJ]) +
                partSum;
        }

        private void CalcDeltaTable()
        {
            int tableSize = Data.Distances.Count();
            
            DeltaTable = new int[tableSize, tableSize];
            for (int i = 0; i < tableSize; i++) {
                int piI = ActualBestSolution.Solution[i];
                for (int j = 0; j < tableSize; j++)
                {
                    int piJ = ActualBestSolution.Solution[j];
                    CalcDelta(tableSize, i, j, piJ, piI);
                }
            }
        }

        private void SwapValuesInSolution(int swapX, int swapY)
        {
            int temp = ActualBestSolution.Solution[swapY];
            ActualBestSolution.Solution[swapY] = ActualBestSolution.Solution[swapX];
            ActualBestSolution.Solution[swapX] = temp;
        }
    }
}
