using System;
using System.Collections.Generic;
using System.Text;
using AlgBattle.DataReaders;

namespace AlgBattle.Solvers
{
    public class QapHeuristicSolver : QapSolver
    {
        public QapHeuristicSolver(QapData data) : base(data)
        {
        }

        public override int[] GetSolution()
        {
            return GetGreedyInitSolution();
        }

        public override int GetSwapCounter()
        {
            throw new NotImplementedException();
        }
    }
}
