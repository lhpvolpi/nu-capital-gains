using NuCapitalGains.Core.Calculator.Entities;
using NuCapitalGains.Core.Calculator.Services;
using NuCapitalGains.Infra.Services;

namespace NuCapitalGains.Application;

public static class Program
{
    private static ICalculatorService service;

    public static void Main()
    {
        try
        {
            var serviceProvider = GetServiceProvider();
            service = serviceProvider.GetRequiredService<ICalculatorService>();

            using StreamReader reader = new(Console.OpenStandardInput());
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var result = Process(line);
                Console.WriteLine(result);

                service.Reset();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public static string Process(string input)
    {
        var operations = JsonSerializer.Deserialize<List<Operation>>(input);
        var results = new List<OperationResult>();

        foreach (var item in operations)
            results.Add(service.ProcessOperation(item));

        return JsonSerializer.Serialize(results, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    }

    private static ServiceProvider GetServiceProvider()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.ConfigureServices();

        return serviceCollection.BuildServiceProvider();
    }

    private static void ConfigureServices(this IServiceCollection services)
        => services.AddScoped<ICalculatorService, CalculatorService>();
}