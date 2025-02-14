namespace NuCapitalGains.Tests.IntegrationTests;

public class ProgramIntegrationTests
{
    public ProgramIntegrationTests() { }

    [Fact]
    public void Program_Integration_Should_Process_Buy_Operation_And_Return_Zero_Tax()
    {
        // Arrange
        var projectPath = Path.GetFullPath("../../../../NuCapitalGains.Application/NuCapitalGains.Application.csproj");
        var simulatedInput = "[{\"operation\":\"buy\",\"quantity\":100,\"unit-cost\":10.00}]";

        var processStartInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"run --project \"{projectPath}\"",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process { StartInfo = processStartInfo };

        // Act
        process.Start();
        process.StandardInput.WriteLine(simulatedInput);
        process.StandardInput.Close();
        var act = process.StandardOutput.ReadToEnd()?.Trim();
        process.WaitForExit();

        // assert
        Assert.Equal("[{\"tax\":0}]", act);
    }
}
