using System;
using System.IO;
using System.Linq;

public class Program
{
    public static void Main()
    {
        try
        {
            var (n, m, matrix) = ReadInput();
            int result = FindMaximumSumSubmatrix(matrix, n, m);
            WriteOutput(result);
            Console.WriteLine($"Calculation complete. Result: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static (int n, int m, int[,] matrix) ReadInput()
    {
        string inputPath = Path.Combine(GetProjectDirectory(), "lab2", "INPUT.txt");
        if (!File.Exists(inputPath))
        {
            throw new FileNotFoundException($"Input file not found at: {inputPath}");
        }
        string[] lines = File.ReadAllLines(inputPath);
        string[] dimensions = lines[0].Split();
        int n = int.Parse(dimensions[0]);
        int m = int.Parse(dimensions[1]);

        int[,] matrix = new int[n, m];
        for (int i = 0; i < n; i++)
        {
            int[] row = lines[i + 1].Split().Select(int.Parse).ToArray();
            for (int j = 0; j < m; j++)
            {
                matrix[i, j] = row[j];
            }
        }
        return (n, m, matrix);
    }


    static void WriteOutput(int result)
    {
        string outputPath = Path.Combine(GetProjectDirectory(), "lab2", "OUTPUT.txt");
        File.WriteAllText(outputPath, result.ToString());
        Console.WriteLine($"Output written to: {outputPath}");
    }

    static string GetProjectDirectory()
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

    static int FindMaximumSumSubmatrix(int[,] matrix, int n, int m)
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

    static int KadaneAlgorithm(int[] arr)
    {
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