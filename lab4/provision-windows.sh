# Update Windows (optional, can be lengthy)
# Install .NET SDK 8.0 using Chocolatey
choco install dotnet-sdk -y

# Add BaGet source using the provided environment variable
dotnet nuget add source "$env:BAGET_URL" -n Baget

# Install the Lab4 NuGet tool
dotnet tool install --global lab4 --version 1.0.0

# Navigate to the project directory and run Lab4
cd C:\project
dotnet run --project LAB4 -- help

Write-Host "Windows environment setup complete and Lab4 executed."