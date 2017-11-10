using System;
using System.Collections.Generic;
using System.Text;

namespace AlgBattle.DataReaders
{
    class QapTestResult
    {
        public string AlgName { get; set; } //GSRH
        public int InstanceSize { get; set; } //GSRH
        public int TestsCount { get; set; } //GSRH
        public int TestTime { get; set; } //GSR H?
        public int AverageTime { get; set; } //GSRH 

        public int OptimalSolutionScore { get; set; } //GSRH
        public int BestSolutionScore { get; set; }  //GSRH
        public int AverageSolutionScore {get; set;} //GSRH
        public float StandardDeviationSolutionScore { get; set; } //GSRH
        public int WorstSolutionScore { get; set; } //GSRH
        
        public int AverageSwapCounter { get; set; } //GS
        public float StandardDeviationSwapCounter { get; set; } //GS        
    }
}
