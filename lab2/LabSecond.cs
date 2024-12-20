﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class LabSecond
    {
        public  (int n, int m, int[,] matrix) ReadInput(string inputPath)
        {
            //string inputPath = Path.Combine(GetProjectDirectory(), "lab2", "INPUT.txt");

            if (!File.Exists(inputPath))
            {
                throw new FileNotFoundException($"Input file not found at: {inputPath}");
            }

            string[] lines = File.ReadAllLines(inputPath);
            if (lines.Length < 2)
            {
                throw new FormatException("Input file is missing required matrix dimensions or data.");
            }

            string[] dimensions = lines[0].Split();
            if (dimensions.Length != 2)
            {
                throw new FormatException("Matrix dimensions must be exactly two integers.");
            }

            if (!int.TryParse(dimensions[0], out int n) || !int.TryParse(dimensions[1], out int m))
            {
                throw new FormatException("Matrix dimensions must be valid integers.");
            }

            int[,] matrix = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                if (i + 1 >= lines.Length)
                {
                    throw new FormatException($"Input file has insufficient rows for a {n}x{m} matrix.");
                }

                int[] row = lines[i + 1].Split().Select(s =>
                {
                    if (!int.TryParse(s, out int value))
                    {
                        throw new FormatException($"Matrix contains invalid number at row {i + 1}.");
                    }
                    return value;
                }).ToArray();

                if (row.Length != m)
                {
                    throw new FormatException($"Row {i + 1} has {row.Length} elements, expected {m}.");
                }

                for (int j = 0; j < m; j++)
                {
                    matrix[i, j] = row[j];
                }
            }
            return (n, m, matrix);
        }

        public void WriteOutput(int result, string outputPath)
        {
            //string outputPath = Path.Combine(GetProjectDirectory(), "lab2", "OUTPUT.txt");

            try
            {
                File.WriteAllText(outputPath, result.ToString());
                Console.WriteLine($"Output written to: {outputPath}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Failed to write output: {ex.Message}");
            }
        }

        public string GetProjectDirectory()
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            while (!Directory.Exists(Path.Combine(currentDirectory, "lab2")))
            {
                currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
                if (currentDirectory == null)
                {
                    throw new DirectoryNotFoundException("Could not find the 'lab2' directory.");
                }
            }

            return currentDirectory;
        }

        public int FindMaximumSumSubmatrix(int[,] matrix, int n, int m)
        {
            int maxSum = int.MinValue;

            for (int left = 0; left < m; left++)
            {
                int[] temp = new int[n];
                for (int right = left; right < m; right++)
                {
                    for (int i = 0; i < n; i++)
                    {
                        temp[i] += matrix[i, right];
                    }

                    int currentSum = KadaneAlgorithm(temp);
                    maxSum = Math.Max(maxSum, currentSum);
                }
            }

            return maxSum;
        }

        public int KadaneAlgorithm(int[] arr)
        {
            if (arr == null || arr.Length == 0)
            {
                throw new ArgumentException("Input array for Kadane's algorithm cannot be null or empty.");
            }

            int maxSoFar = arr[0];
            int maxEndingHere = arr[0];

            for (int i = 1; i < arr.Length; i++)
            {
                maxEndingHere = Math.Max(arr[i], maxEndingHere + arr[i]);
                maxSoFar = Math.Max(maxSoFar, maxEndingHere);
            }

            return maxSoFar;
        }
    }
}
