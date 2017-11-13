using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlgBattle.DataReaders;

namespace AlgBattle.Solvers
{
    public class QapSimpleGreedySolver : QapSolver
    {
        public QapSimpleGreedySolver(QapData data) : base(data)
        {
        }

        public override int[] GetSolution()
        {
            int dimension_ = this.Data.Size;
            var distancesPotential_ = new List<int>();
            var flowPotential_ = new List<int>();

            // Calculate distances and flow potential
            for (int i = 0; i < dimension_; ++i)
            {
                int potential = 0;
                for (int j = 0; j < dimension_; ++j)
                {
                    if (i != j)
                    {
                        potential += this.Data.Distances[i][j];
                    }
                }
                distancesPotential_.Add(potential);
            }

            for (int i = 0; i < dimension_; ++i)
            {
                int potential = 0;
                for (int j = 0; j < dimension_; ++j)
                {
                    if (i != j)
                    {
                        potential += this.Data.Flows[i][j];
                    }
                }
                flowPotential_.Add(potential);
            }

            // Compute the solution
            int[] solution_ = Enumerable.Repeat(-1, Data.Size).ToArray();
            for (int j = 0; j < dimension_; ++j)
            {
                int maxPos = 0;
                int minPos = 0;
                for (int i = 0; i < dimension_; ++i)
                {
                    // Maximum distance potential
                    if (distancesPotential_[i] > distancesPotential_[maxPos])
                    {
                        maxPos = i;
                    }
                    // Minimum flow potential
                    if (flowPotential_[i] < flowPotential_[minPos])
                    {
                        minPos = i;
                    }
                }
                solution_[maxPos] = minPos;
                distancesPotential_[maxPos] = -1;
                flowPotential_[minPos] = Int32.MaxValue;
            }
            return solution_;
        }

        public override int GetSwapCounter()
        {
            throw new NotImplementedException();
        }
    }
}
