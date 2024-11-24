using lab3;
using System;
using System.IO;
using System.Linq;

public class Program3
{
    public static string Lab3(string[] args)
    {
        LabThird labThird = new LabThird();
        try
        {
            string inputPath = Path.Combine(labThird.GetProjectDirectory(), "lab3", "INPUT.txt");
            string outputPath = Path.Combine(labThird.GetProjectDirectory(), "lab3", "OUTPUT.txt");

            labThird.ReadInput(inputPath);
            int result = labThird.FindMaximumShortestDistance();
            labThird.WriteOutput(result, outputPath);
            return($"Calculation complete. Result: {result}");
        }
        catch (Exception ex)
        {
            return($"An unexpected error occurred: {ex.Message}");
        }
    }    
}