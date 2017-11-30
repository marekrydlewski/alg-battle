using AlgBattle.DataReaders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgBattle.Solvers
{
    public class QapAnnealingSolver : QapSolver
    {
        public QapAnnealingSolver(QapData data) : base(data)
        {
        }

        public override int[] GetSolution()
        {
            throw new NotImplementedException();
        }

        public override int GetSwapCounter()
        {
            throw new NotImplementedException();
        }
    }
}
