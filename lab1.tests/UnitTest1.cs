using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace lab1.tests
{
    public class Tests
    {
        private readonly ITestOutputHelper output;
        LabFirst labFirst = new LabFirst();

        public Tests(ITestOutputHelper output)
        {
            this.output = output;
        }

        public static IEnumerable<object[]> DerangementTestData()
        {
            yield return new object[] { 0, 1 };
            yield return new object[] { 1, 0 };
            yield return new object[] { 2, 1 };
            yield return new object[] { 5, 44 };
            yield return new object[] { 20, 895014631192902121 };
        }

        [Xunit.Theory]
        [MemberData(nameof(DerangementTestData))]
        public void TestCalculateDerangements(int input, long expected)
        {
            // Act
            long result = labFirst.CalculateDerangements(input);

            // Assert
            Xunit.Assert.Equal(expected, result);
            Console.WriteLine($"Input: {input}, Result: {result}");
        }

        [Fact]
        public void TestNegativeInput()
        {
            int input = -1;
            Xunit.Assert.Throws<ArgumentException>(() => labFirst.CalculateDerangements(input));
        }

        [Fact]
        public void TestFileOperations()
        {
            // Arrange
            string tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDir);
            string labDir = Path.Combine(tempDir, "lab1");
            Directory.CreateDirectory(labDir);
            string inputPath = Path.Combine(labDir, "INPUT.txt");
            string outputPath = Path.Combine(labDir, "OUTPUT.txt");
            string originalDirectory = Directory.GetCurrentDirectory();

            try
            {
                // Act
                File.WriteAllText(inputPath, "5");
                output.WriteLine($"Created INPUT.txt at {inputPath}");

                Directory.SetCurrentDirectory(tempDir);
                output.WriteLine($"Changed current directory to {tempDir}");

                Program1.Main();
                output.WriteLine("Executed Program.Main()");

                // Assert
                output.WriteLine($"Checking for OUTPUT.txt at {outputPath}");
                Xunit.Assert.True(File.Exists(outputPath), $"OUTPUT.txt does not exist at {outputPath}");
                string result = File.ReadAllText(outputPath);
                output.WriteLine($"Content of OUTPUT.txt: {result}");
                Xunit.Assert.Equal("44", result);
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