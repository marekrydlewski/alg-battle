using System;
using System.Collections.Generic;
using System.Text;

namespace AlgBattle.Solvers
{
    public class QapHeuristicSolver : QapSolver
    {
        public override int[] GetSolution()
        {
            return GetGreedyInitSolution();
        }
    }
}
