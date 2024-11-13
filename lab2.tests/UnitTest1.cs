using Xunit;
using Xunit.Abstractions;

namespace lab2.tests
{
    public class ProgramTests
    {
        private readonly ITestOutputHelper output;
        LabSecond labSecond = new LabSecond();

        public ProgramTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestFindMaximumSumSubmatrix()
        {
            // Arrange
            int[,] matrix = new int[,]
            {
                { -7, 8, -1, 0, -2 },
                { 2, -9, 2, 4, -6 },
                { -7, 0, 6, 8, 1 },
                { 4, -8, -1, 0, -6 }
            };

            output.WriteLine("Matrix used for testing:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    output.WriteLine(matrix[i, j].ToString() + " ");
                }
            }

            // Act
            int result = labSecond.FindMaximumSumSubmatrix(matrix, 4, 5);
            output.WriteLine($"Result of FindMaximumSumSubmatrix: {result}");

            // Assert
            Xunit.Assert.Equal(20, result);
        }

        [Fact]
        public void TestFindMaximumSumSubmatrix_AllNegative()
        {
            // Arrange
            int[,] matrix = new int[,]
            {
                { -1, -2, -3 },
                { -4, -5, -6 },
                { -7, -8, -9 }
            };

            output.WriteLine("Matrix with all negative values used for testing:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    output.WriteLine(matrix[i, j].ToString() + " ");
                }
            }

            // Act
            int result = labSecond.FindMaximumSumSubmatrix(matrix, 3, 3);
            output.WriteLine($"Result of FindMaximumSumSubmatrix with all negative values: {result}");

            // Assert
            Assert.Equal(-1, result);
        }

        [Fact]
        public void TestKadaneAlgorithm()
        {
            // Arrange
            int[] arr = new int[] { -2, -3, 4, -1, -2, 1, 5, -3 };
            output.WriteLine("Array used for KadaneAlgorithm:");
            output.WriteLine(string.Join(", ", arr));

            // Act
            int result = labSecond.KadaneAlgorithm(arr);
            output.WriteLine($"Result of KadaneAlgorithm: {result}");

            // Assert
            Xunit.Assert.Equal(7, result);
        }

        [Fact]
        public void TestKadaneAlgorithm_AllNegative()
        {
            // Arrange
            int[] arr = new int[] { -1, -2, -3, -4, -5 };
            output.WriteLine("Array with all negative values used for KadaneAlgorithm:");
            output.WriteLine(string.Join(", ", arr));

            // Act
            int result = labSecond.KadaneAlgorithm(arr);
            output.WriteLine($"Result of KadaneAlgorithm with all negative values: {result}");

            // Assert
            Assert.Equal(-1, result);
        }

        [Fact]
        public void TestFileOperations()
        {
            // Arrange
            string tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDir);
            string labDir = Path.Combine(tempDir, "lab2");
            Directory.CreateDirectory(labDir);
            string inputPath = Path.Combine(labDir, "INPUT.txt");
            string outputPath = Path.Combine(labDir, "OUTPUT.txt");
            string originalDirectory = Directory.GetCurrentDirectory();

            try
            {
                // Act
                File.WriteAllText(inputPath, "3 4\n-1 -2 -3 -4\n-5 -6 -7 -8\n-9 -10 -11 -12");
                output.WriteLine($"Created INPUT.txt at {inputPath}");
                Directory.SetCurrentDirectory(tempDir);
                output.WriteLine($"Changed current directory to {tempDir}");
                Program.Main();
                output.WriteLine("Executed Program.Main()");

                // Assert
                output.WriteLine($"Checking for OUTPUT.txt at {outputPath}");
                Xunit.Assert.True(File.Exists(outputPath), $"OUTPUT.txt does not exist at {outputPath}");
                string result = File.ReadAllText(outputPath);
                output.WriteLine($"Content of OUTPUT.txt: {result}");
                Xunit.Assert.Equal("-1", result);
            }
            finally
            {
                Directory.SetCurrentDirectory(originalDirectory);
                output.WriteLine($"Restored current directory to {originalDirectory}");
                // Cleanup
                Directory.Delete(tempDir, true);
                output.WriteLine($"Deleted temporary directory {tempDir}");
            }
        }
    }
}
