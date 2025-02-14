using NuCapitalGains.Core.Calculator.Entities;

namespace NuCapitalGains.Core.Calculator.Services;

public interface ICalculatorService
{
    OperationResult ProcessOperation(Operation operation);

    void Reset();
}