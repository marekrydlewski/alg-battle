using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace AlgBattle.DataReaders
{
    public class QapDataFileReader
    {
        public bool WarningsEnabled { get; set; } = false;

        public QapData ReadData(string filePath)
        {
            try
            {
                using (var sr = new StreamReader(File.OpenRead(filePath)))
                {
                    string data = sr.ReadToEnd();
                    var splitted = data.Split((char[])null, StringSplitOptions.RemoveEmptyEntries)
                        .ToList()
                        .Select(x => Convert.ToInt32(x))
                        .ToList();

                    int matrixSize = splitted[0];
                    var qapDataFlow = new int[matrixSize][];
                    var qapDataDistance = new int[matrixSize][];

                    var chunked = splitted.Skip(1).Chunk(matrixSize).ToList();

                    for (int i = 0; i < matrixSize; ++i)
                    {
                        qapDataFlow[i] = chunked[i].ToArray();
                    }

                    for (int i = matrixSize; i < 2 * matrixSize; ++i)
                    {
                        qapDataDistance[i - matrixSize] = chunked[i].ToArray();
                    }

                    return new QapData { Distances = qapDataDistance, Flows = qapDataFlow, Size = qapDataFlow.Count() };
                }
            }
            catch (Exception e)
            {
                if (WarningsEnabled)
                {
                    Console.WriteLine($"QapDataFileReader: The file could not be read: {filePath}");
                    Console.WriteLine(e.Message);
                }
            }
            return null;
        }

        public QapSolution ReadSolution(string filePath)
        {
            try
            {
                using (var sr = new StreamReader(File.OpenRead(filePath)))
                {
                    string data = sr.ReadToEnd();
                    var splitted = data.Split((char[])null, StringSplitOptions.RemoveEmptyEntries)
                        .ToList()
                        .Select(x => Convert.ToInt32(x))
                        .ToList();

                    int matrixSize = splitted[0];
                    int score = splitted[1];
                    var solution = splitted.Skip(2).ToList();

                    return new QapSolution { Size = matrixSize, Solution = solution, Score = score };
                }
            }
            catch (Exception e)
            {
                if (WarningsEnabled)
                {
                    Console.WriteLine($"QapDataFileReader: The file could not be read: {filePath}");
                    Console.WriteLine(e.Message);
                }
            }
            return null;
        }

        public IEnumerable<QapEnsemble> LoadDirectory(string dirPath)
        {
            var dataFiles = Directory.GetFiles(dirPath, "*.dat");
            foreach (var file in dataFiles)
            {
                var solutionFile = file.Remove(file.Length - 4) + ".sln";
                var data = ReadData($@"{file}");
                var solution = ReadSolution($@"{solutionFile}");
                var ensemble = new QapEnsemble {Data = data, Solution = solution};
                yield return ensemble;
            }
        }
    }
}
