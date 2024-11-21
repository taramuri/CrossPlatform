using lab1;
using lab2;
using lab3;

namespace LabLibrary
{
    public class LabRunner
    {
        private readonly string _lab;
        private readonly string _solutionRoot;
        private readonly LabFirst _labFirst;
        private readonly LabSecond _labSecond;
        private readonly LabThird _labThird;

        public LabRunner(string lab)
        {
            _lab = lab;
            _solutionRoot = GetSolutionRootPath();
            _labFirst = new LabFirst();
            _labSecond = new LabSecond();
            _labThird = new LabThird();
        }

        private string GetSolutionRootPath()
        {
            string currentDir = Directory.GetCurrentDirectory();
            while (!string.IsNullOrEmpty(currentDir))
            {
                if (Directory.GetFiles(currentDir, "*.sln").Any())
                    return currentDir;
                var parent = Directory.GetParent(currentDir);
                if (parent == null)
                    break;
                currentDir = parent.FullName;
            }
            throw new DirectoryNotFoundException("Could not find solution root directory");
        }

        public string ProcessData(string inputData)
        {
            try
            {
                switch (_lab.ToLower())
                {
                    case "lab1":
                        return ProcessLab1(inputData);
                    case "lab2":
                        return ProcessLab2(inputData);
                    case "lab3":
                        return ProcessLab3(inputData);
                    default:
                        throw new ArgumentException($"Непідтримувана лабораторна робота: {_lab}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Помилка обробки даних: {ex.Message}");
            }
        }

        private string ProcessLab1(string inputData)
        {
            if (!int.TryParse(inputData, out int guestCount) || guestCount < 1 || guestCount > 100)
            {
                throw new ArgumentException("Введіть коректне число від 1 до 100");
            }

            long result = _labFirst.CalculateDerangements(guestCount);
            return result.ToString();
        }

        private string ProcessLab2(string inputData)
        {
            try
            {
                var lines = inputData.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var dimensions = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (dimensions.Length != 2 ||
                    !int.TryParse(dimensions[0], out int n) ||
                    !int.TryParse(dimensions[1], out int m))
                {
                    throw new ArgumentException("Invalid matrix dimensions format");
                }

                if (n <= 0 || m <= 0)
                {
                    throw new ArgumentException("Matrix dimensions must be positive integers.");
                }

                int[,] matrix = new int[n, m];
                for (int i = 0; i < n; i++)
                {
                    var row = lines[i + 1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (row.Length != m)
                    {
                        throw new ArgumentException($"Invalid number of elements in row {i + 1}");
                    }

                    for (int j = 0; j < m; j++)
                    {
                        if (!int.TryParse(row[j], out matrix[i, j]))
                        {
                            throw new ArgumentException($"Invalid number format at position [{i},{j}]");
                        }
                    }
                }

                int result = _labSecond.FindMaximumSumSubmatrix(matrix, n, m);
                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error processing Lab2 data: {ex.Message}");
            }
        }

        private string ProcessLab3(string inputData)
        {
            try
            {
                var lines = inputData.Split('\n', StringSplitOptions.RemoveEmptyEntries);

                if (!int.TryParse(lines[0], out int n))
                {
                    throw new ArgumentException("Invalid format for n or m");
                }

                if (n < 1)
                {
                    throw new ArgumentException("n must be positive integers");
                }

                string tempFile = Path.GetTempFileName();
                try
                {
                    File.WriteAllText(tempFile, inputData);
                    _labThird.ReadInput(tempFile);
                    int result = _labThird.FindMaximumShortestDistance();
                    return result.ToString();
                }
                finally
                {
                    if (File.Exists(tempFile))
                    {
                        File.Delete(tempFile);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error processing Lab3 data: {ex.Message}");
            }
        }
    }
}
