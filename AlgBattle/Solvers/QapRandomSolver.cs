using System;
using System.Collections.Generic;
using System.Text;

namespace AlgBattle.Solvers
{
    public class QapRandomSolver : QapSolver
    {
        public override int[] GetSolution()
        {
            return this.GetRandomInitSolution();
        }
    }
}
