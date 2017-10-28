using System;
using System.Collections.Generic;
using System.Text;
using AlgBattle.DataReaders;

namespace AlgBattle.Solvers
{
    public class QapRandomSolver : QapSolver
    {
        public QapRandomSolver(QapData data) : base(data)
        {
        }

        public override int[] GetSolution()
        {
            return this.GetRandomInitSolution();
        }
    }
}
