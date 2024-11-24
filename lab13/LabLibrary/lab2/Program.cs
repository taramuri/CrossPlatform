using lab2;
using System;
using System.IO;
using System.Linq;

public partial class Program2
{
    public static string Lab2(string[] args)
    {
        try
        {
            LabSecond labSecond = new LabSecond();
            try
            {
                string inputPath = Path.Combine(labSecond.GetProjectDirectory(), "lab2", "INPUT.txt");
                string outputPath = Path.Combine(labSecond.GetProjectDirectory(), "lab2", "OUTPUT.txt");

                var (n, m, matrix) = labSecond.ReadInput(inputPath);
                if (n <= 0 || m <= 0)
                {
                    throw new ArgumentException("Matrix dimensions must be positive integers.");
                }
                if (matrix == null || matrix.Length == 0)
                {
                    throw new ArgumentException("Matrix is empty or not initialized properly.");
                }

                int result = labSecond.FindMaximumSumSubmatrix(matrix, n, m);
                labSecond.WriteOutput(result, outputPath);
                Console.WriteLine($"Calculation complete. Result: {result}");
                return result.ToString(); 
            }
            catch (FileNotFoundException ex)
            {
                string errorMessage = $"File error: {ex.Message}";
                Console.WriteLine(errorMessage);
                return errorMessage; 
            }
        }
        catch (Exception ex)
        {
            string errorMessage = $"An unexpected error occurred: {ex.Message}";
            Console.WriteLine(errorMessage);
            return errorMessage; 
        }
    }
}