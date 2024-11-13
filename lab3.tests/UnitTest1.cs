using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using lab3;

namespace lab3.tests
{
    public class ProgramTests
    {
        private readonly ITestOutputHelper output;
        private readonly int[,]? originalG;
        private readonly int originalN;
        private const int TEST_MAX = 101; 
        LabThird labThird = new LabThird();
        public ProgramTests(ITestOutputHelper output)
        {
            this.output = output;
            originalG = labThird.g?.Clone() as int[,];
            originalN = labThird.n;
        }

        [Fact]
        public void TestFindMaximumShortestDistance()
        {
            // Arrange
            labThird.n = 4;
            labThird.g = new int[TEST_MAX, TEST_MAX]; 
            int[,] testMatrix = {
                { 0, 5, 9, LabThird.INF },
                { LabThird.INF, 0, 2, 8 },
                { LabThird.INF, LabThird.INF, 0, 7 },
                { 4, LabThird.INF, LabThird.INF, 0 }
            };

            // Copy test matrix to Program.g
            for (int i = 0; i < labThird.n; i++)
                for (int j = 0; j < labThird.n; j++)
                    labThird.g[i, j] = testMatrix[i, j];

            output.WriteLine("Adjacency matrix used for testing:");
            PrintMatrix();

            // Act
            int result = labThird.FindMaximumShortestDistance();
            output.WriteLine($"Result of FindMaximumShortestDistance: {result}");

            // Assert
            Assert.Equal(16, result);

            // Restore original values
            RestoreOriginalValues();
        }

        [Fact]
        public void TestFindMaximumShortestDistance_SingleNode()
        {
            // Arrange
            labThird.n = 1;
            labThird.g = new int[TEST_MAX, TEST_MAX]; 
            labThird.g[0, 0] = 0;

            output.WriteLine("Single-node matrix used for testing.");

            // Act
            int result = labThird.FindMaximumShortestDistance();
            output.WriteLine($"Result of FindMaximumShortestDistance: {result}");

            // Assert
            Assert.Equal(0, result);

            RestoreOriginalValues();
        }

        [Fact]
        public void TestFindMaximumShortestDistance_NoPath()
        {
            // Arrange
            labThird.n = 3;
            labThird.g = new int[TEST_MAX, TEST_MAX]; 

            for (int i = 0; i < labThird.n; i++)
                for (int j = 0; j < labThird.n; j++)
                    labThird.g[i, j] = i == j ? 0 : LabThird.INF;

            output.WriteLine("Matrix with no paths between nodes.");
            PrintMatrix();

            // Act
            int result = labThird.FindMaximumShortestDistance();
            output.WriteLine($"Result of FindMaximumShortestDistance: {result}");

            // Assert
            Assert.Equal(0, result);

            RestoreOriginalValues();
        }


        [Fact]
        public void TestFileOperations()
        {
            var savedG = labThird.g?.Clone() as int[,];
            var savedN = labThird.n;

            // Arrange
            string tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDir);
            string labDir = Path.Combine(tempDir, "lab3");
            Directory.CreateDirectory(labDir);
            string inputPath = Path.Combine(labDir, "INPUT.txt");
            string outputPath = Path.Combine(labDir, "OUTPUT.txt");
            string originalDirectory = Directory.GetCurrentDirectory();

            try
            {
                // Create test input file
                File.WriteAllText(inputPath, "4\n0 5 9 -1\n-1 0 2 8\n-1 -1 0 7\n4 -1 -1 0");
                output.WriteLine($"Created INPUT.txt at {inputPath}");

                // Change directory and save current directory
                Environment.CurrentDirectory = tempDir;
                output.WriteLine($"Changed current directory to {tempDir}");

                Program3.Main();
                output.WriteLine("Executed Program.Main()");

                Assert.True(File.Exists(outputPath), $"OUTPUT.txt does not exist at {outputPath}");
                string result = File.ReadAllText(outputPath);
                output.WriteLine($"Content of OUTPUT.txt: {result}");
                Assert.Equal("16", result);
            }
            finally
            {
                labThird.g = savedG;
                labThird.n = savedN;

                Environment.CurrentDirectory = originalDirectory;
                output.WriteLine($"Restored current directory to {originalDirectory}");
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                    output.WriteLine($"Deleted temporary directory {tempDir}");
                }
            }
        }

        private void PrintMatrix()
        {
            for (int i = 0; i < labThird.n; i++)
            {
                string row = "";
                for (int j = 0; j < labThird.n; j++)
                {
                    row += (labThird.g[i, j] == LabThird.INF ? "INF" : labThird.g[i, j].ToString()) + " ";
                }
                output.WriteLine(row.Trim());
            }
        }

        private void RestoreOriginalValues()
        {
            if (originalG != null)
            {
                labThird.g = originalG.Clone() as int[,];
            }
            labThird.n = originalN;
        }
    }
}