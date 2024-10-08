using System;
using System.IO;
using System.Linq;

public class Program
{
    public const int MAX = 101;
    public const int INF = 10000000;
    public static int[,] g = new int[MAX, MAX];
    public static int n;

    public static void Main()
    {
        try
        {
            ReadInput();
            int result = FindMaximumShortestDistance();
            WriteOutput(result);
            Console.WriteLine($"Calculation complete. Result: {result}");
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"File error: {ex.Message}");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Input format error: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Argument error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    static void ReadInput()
    {
        string inputPath = Path.Combine(GetProjectDirectory(), "lab3", "INPUT.txt");

        if (!File.Exists(inputPath))
        {
            throw new FileNotFoundException($"Input file not found at: {inputPath}");
        }

        string[] lines = File.ReadAllLines(inputPath);
        if (lines.Length < 2)
        {
            throw new FormatException("Input file is missing required data.");
        }

        if (!int.TryParse(lines[0], out n) || n <= 0 || n > 100)
        {
            throw new FormatException("Number of vertices must be a positive integer not exceeding 100.");
        }

        for (int i = 0; i < n; i++)
        {
            if (i + 1 >= lines.Length)
            {
                throw new FormatException($"Input file has insufficient rows for a {n}x{n} matrix.");
            }

            int[] row = lines[i + 1].Split().Select(s => {
                if (!int.TryParse(s, out int value))
                {
                    throw new FormatException($"Matrix contains invalid number at row {i + 1}.");
                }
                return value;
            }).ToArray();

            if (row.Length != n)
            {
                throw new FormatException($"Row {i + 1} has {row.Length} elements, expected {n}.");
            }

            for (int j = 0; j < n; j++)
            {
                g[i, j] = row[j] < 0 ? INF : row[j];
            }
        }
    }

    static void WriteOutput(int result)
    {
        string outputPath = Path.Combine(GetProjectDirectory(), "lab3", "OUTPUT.txt");

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

    static string GetProjectDirectory()
    {
        string currentDirectory = Directory.GetCurrentDirectory();

        while (!Directory.Exists(Path.Combine(currentDirectory, "lab3")))
        {
            currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
            if (currentDirectory == null)
            {
                throw new DirectoryNotFoundException("Could not find the 'lab3' directory.");
            }
        }

        return currentDirectory;
    }

    static void Floyd()
    {
        for (int k = 0; k < n; k++)
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (g[i, k] + g[k, j] < g[i, j])
                        g[i, j] = g[i, k] + g[k, j];
    }

    public static int FindMaximumShortestDistance()
    {
        Floyd();
        int maxDistance = 0;
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                if (g[i, j] > maxDistance && g[i, j] < INF)
                    maxDistance = g[i, j];
        return maxDistance;
    }
}
