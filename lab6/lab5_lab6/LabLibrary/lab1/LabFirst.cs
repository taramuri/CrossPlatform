using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public class LabFirst
    {
        public int ReadInput(string inputPath)
        {
            //string inputPath = Path.Combine(GetProjectDirectory(), "lab1", "INPUT.txt");
            if (!File.Exists(inputPath))
            {
                throw new FileNotFoundException($"Input file not found at: {inputPath}");
            }
            string input = File.ReadAllText(inputPath);
            return int.Parse(input);

        }

        public void WriteOutput(long result, string outputPath)
        {
            //string outputPath = Path.Combine(GetProjectDirectory(), "lab1", "OUTPUT.txt");
            File.WriteAllText(outputPath, result.ToString());
            Console.WriteLine($"Output written to: {outputPath}");
        }

        public string GetProjectDirectory()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            while (!Directory.Exists(Path.Combine(currentDirectory, "lab1")))
            {
                currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
                if (currentDirectory == null)
                {
                    throw new DirectoryNotFoundException("Could not find the 'lab1' directory.");
                }
            }
            return currentDirectory;
        }

        public long CalculateDerangements(int n)
        {
            if (n == 0) return 1;
            if (n == 1) return 0;
            if (n < 0) throw new ArgumentException("Input must be non-negative.");


            long[] dp = new long[n + 1];
            dp[0] = 1;
            dp[1] = 0;

            for (int i = 2; i <= n; i++)
            {
                dp[i] = (i - 1) * (dp[i - 1] + dp[i - 2]);
            }

            return dp[n];
        }
    }
}
