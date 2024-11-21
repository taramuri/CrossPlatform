using lab1;

public partial class Program1
{
    public static string Lab1(string[] args)
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
            return result.ToString(); 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return $"Error: {ex.Message}"; 
        }
    }
}