using lab3;
using System;
using System.IO;
using System.Linq;

public class Program3
{    
    public static void Main()
    {
        LabThird labThird = new LabThird();
        try
        {
            string inputPath = Path.Combine(labThird.GetProjectDirectory(), "lab3", "INPUT.txt");
            string outputPath = Path.Combine(labThird.GetProjectDirectory(), "lab3", "OUTPUT.txt");

            labThird.ReadInput(inputPath);
            int result = labThird.FindMaximumShortestDistance();
            labThird.WriteOutput(result, outputPath);
            Console.WriteLine($"Calculation complete. Result: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }    
}