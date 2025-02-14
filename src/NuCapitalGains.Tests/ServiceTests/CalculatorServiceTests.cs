using NuCapitalGains.Core.Calculator.Entities;
using NuCapitalGains.Infra.Services;

namespace NuCapitalGains.Tests.ServiceTests;

public class CalculatorServiceTests
{
    public CalculatorServiceTests() { }

    [Fact]
    public void ProcessOperation_UseCase01()
    {
        // arrange
        var operations = new List<Operation>
        {
            new("buy", 100, 10),
            new("sell", 50, 15),
            new("sell", 50, 15)
        };

        var expectedResults = new List<OperationResult>
        {
            new(0m),
            new(0m),
            new(0m)
        };

        var service = new CalculatorService();
        var results = new List<OperationResult>();

        // act
        foreach (var item in operations)
            results.Add(service.ProcessOperation(item));

        // assert
        Assert.Equal(
            expectedResults.Select(i => i.Tax),
            results.Select(i => i.Tax));
    }

    [Fact]
    public void ProcessOperation_UseCase02()
    {
        // arrange
        var operations = new List<Operation>
        {
            new("buy", 10000, 10),
            new("sell", 5000, 20),
            new("sell", 5000, 5)
        };

        var expectedResults = new List<OperationResult>
        {
            new(0m),
            new(10000),
            new(0m)
        };

        var service = new CalculatorService();
        var results = new List<OperationResult>();

        // act
        foreach (var item in operations)
            results.Add(service.ProcessOperation(item));

        // assert
        Assert.Equal(
            expectedResults.Select(i => i.Tax),
            results.Select(i => i.Tax));
    }

    [Fact]
    public void ProcessOperation_UseCase03()
    {
        // arrange
        var operations = new List<Operation>
        {
            new("buy", 10000, 10),
            new("sell", 5000, 5),
            new("sell", 3000, 20)
        };

        var expectedResults = new List<OperationResult>
        {
            new(0m),
            new(0m),
            new(1000)
        };

        var service = new CalculatorService();
        var results = new List<OperationResult>();

        // act
        foreach (var item in operations)
            results.Add(service.ProcessOperation(item));

        // assert
        Assert.Equal(
            expectedResults.Select(i => i.Tax),
            results.Select(i => i.Tax));
    }

    [Fact]
    public void ProcessOperation_UseCase04()
    {
        // arrange
        var operations = new List<Operation>
        {
            new("buy", 10000, 10),
            new("buy", 5000, 25),
            new("sell", 10000, 15)
        };

        var expectedResults = new List<OperationResult>
        {
            new(0m),
            new(0m),
            new(0m)
        };

        var service = new CalculatorService();
        var results = new List<OperationResult>();

        // act
        foreach (var item in operations)
            results.Add(service.ProcessOperation(item));

        // assert
        Assert.Equal(
            expectedResults.Select(i => i.Tax),
            results.Select(i => i.Tax));
    }

    [Fact]
    public void ProcessOperation_UseCase05()
    {
        // arrange
        var operations = new List<Operation>
        {
            new("buy", 10000, 10),
            new("buy", 5000, 25),
            new("sell", 10000, 15),
            new("sell", 5000, 25)
        };

        var expectedResults = new List<OperationResult>
        {
            new(0m),
            new(0m),
            new(0m),
            new(10000)
        };

        var service = new CalculatorService();
        var results = new List<OperationResult>();

        // act
        foreach (var item in operations)
            results.Add(service.ProcessOperation(item));

        // assert
        Assert.Equal(
            expectedResults.Select(i => i.Tax),
            results.Select(i => i.Tax));
    }

    [Fact]
    public void ProcessOperation_UseCase06()
    {
        // arrange
        var operations = new List<Operation>
        {
            new("buy", 10000, 10),
            new("sell", 5000, 2),
            new("sell", 2000, 20),
            new("sell", 2000, 20),
            new("sell", 1000, 25)
        };

        var expectedResults = new List<OperationResult>
        {
            new(0m),
            new(0m),
            new(0m),
            new(0m),
            new(3000)
        };

        var service = new CalculatorService();
        var results = new List<OperationResult>();

        // act
        foreach (var item in operations)
            results.Add(service.ProcessOperation(item));

        // assert
        Assert.Equal(
            expectedResults.Select(i => i.Tax),
            results.Select(i => i.Tax));
    }

    [Fact]
    public void ProcessOperation_UseCase07()
    {
        // arrange
        var operations = new List<Operation>
        {
            new("buy", 10000, 10),
            new("sell", 5000, 2),
            new("sell", 2000, 20),
            new("sell", 2000, 20),
            new("sell", 1000, 25),
            new("buy", 10000, 20),
            new("sell", 5000, 15),
            new("sell", 4350, 30),
            new("sell", 650, 30)
        };

        var expectedResults = new List<OperationResult>
        {
            new(0m),
            new(0m),
            new(0m),
            new(0m),
            new(3000),
            new(0m),
            new(0m),
            new(3700),
            new(0m)
        };

        var service = new CalculatorService();
        var results = new List<OperationResult>();

        // act
        foreach (var item in operations)
            results.Add(service.ProcessOperation(item));

        // assert
        Assert.Equal(
            expectedResults.Select(i => i.Tax),
            results.Select(i => i.Tax));
    }

    [Fact]
    public void ProcessOperation_UseCase08()
    {
        // arrange
        var operations = new List<Operation>
        {
            new("buy", 10000, 10),
            new("sell", 10000, 50),
            new("buy", 10000, 20),
            new("sell", 10000, 50)
        };

        var expectedResults = new List<OperationResult>
        {
            new(0m),
            new(80000),
            new(0m),
            new(60000)
        };

        var service = new CalculatorService();
        var results = new List<OperationResult>();

        // act
        foreach (var item in operations)
            results.Add(service.ProcessOperation(item));

        // assert
        Assert.Equal(
            expectedResults.Select(i => i.Tax),
            results.Select(i => i.Tax));
    }

    [Fact]
    public void ProcessOperation_ShouldThrowException_WhenOperationTypeIsInvalid()
    {
        // arrange
        var invalidOperation = new Operation("invalid", 100, 10);
        var service = new CalculatorService();

        // act and assert
        var exception = Assert.Throws<InvalidOperationException>(() => service.ProcessOperation(invalidOperation));

        Assert.Equal("Invalid operation type", exception.Message);
    }
}
