using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace AlgBattle.DataReaders
{
    public class QAPDataFileReader
    {
        public QAPData ReadData(string filePath)
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

                    return new QAPData { Distances = qapDataDistance, Flows = qapDataFlow };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine( $"QAPDataFileReader: The file could not be read: {filePath}");
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public QAPSolution ReadSolution(string filePath)
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

                    return new QAPSolution { Size = matrixSize, Solution = solution, Score = score };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"QAPDataFileReader: The file could not be read: {filePath}");
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public IEnumerable<QAPEnsemble> LoadDirectory(string dirPath)
        {
            var dataFiles = Directory.GetFiles(dirPath, "*.dat");
            foreach (var file in dataFiles)
            {
                var solutionFile = file.Remove(file.Length - 4) + ".sln";
                var data = ReadData($@"{file}");
                var solution = ReadSolution($@"{solutionFile}");
                var ensemble = new QAPEnsemble {Data = data, Solution = solution};
                yield return ensemble;
            }
        }
    }
}
