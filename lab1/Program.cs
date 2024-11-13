using lab1;
using System;
using System.IO;

public class Program1
{
    public static void Main()
    {
        LabFirst labFirst = new LabFirst();
        try
        {
            string inputPath = Path.Combine(labFirst.GetProjectDirectory(), "lab1", "INPUT.txt");            
            string outputPath = Path.Combine(labFirst.GetProjectDirectory(), "lab1", "OUTPUT.txt");

            int n = labFirst.ReadInput(inputPath);
            long result = labFirst.CalculateDerangements(n);
            labFirst.WriteOutput(result, outputPath);
            Console.WriteLine($"Calculation complete. Result: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

    }    
   
}


