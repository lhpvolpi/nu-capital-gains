namespace NuCapitalGains.Core.Calculator.Entities;

public class OperationResult
{
    public OperationResult(decimal tax = 0m)
        => this.Tax = tax;

    public decimal Tax { get; set; }
}