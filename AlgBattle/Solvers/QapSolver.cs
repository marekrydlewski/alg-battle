using System;
using System.Linq;
using AlgBattle.DataReaders;

namespace AlgBattle.Solvers
{
    public abstract class QapSolver
    {
        public QapData Data { get; set; }

        public int[] GetRandomInitSolution()
        {
            var init = Enumerable.Range(0, Data.Size).ToArray();
            RandomGenerator.Shuffle(init);
            return init;
        }

        public int[] GetGreedyInitSolution()
        {
            throw new NotImplementedException();
        }

    }
}
