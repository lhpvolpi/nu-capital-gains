using NuCapitalGains.Core.Calculator.Entities;
using NuCapitalGains.Core.Calculator.Services;

namespace NuCapitalGains.Infra.Services;

public class CalculatorService : ICalculatorService
{
    private const decimal TaxRate = 0.2m;
    private const decimal ExemptionLimit = 20000m;

    private int totalShares = 0;
    private decimal weightedAveragePrice = 0m;
    private decimal accumulatedLoss = 0m;

    public CalculatorService() { }

    /// <summary>
    /// Processes an operation (buy or sell) and returns the tax amount (if applicable).
    /// </summary>
    /// <param name="operation">The operation to process.</param>
    /// <returns>An OperationResult containing the calculated tax.</returns>
    public OperationResult ProcessOperation(Operation operation)
    {
        try
        {
            switch (operation.OperationType.ToLower())
            {
                case "buy":
                    this.Buy(operation.Quantity, operation.UnitCost);
                    return new OperationResult();

                case "sell":
                    var tax = this.Sell(operation.Quantity, operation.UnitCost);
                    return new OperationResult(tax);

                default:
                    throw new InvalidOperationException("Invalid operation type");
            }
        }
        catch
        {
            throw;
        }
    }

    ///<summary>
    /// Resets the state of the CalculatorService by setting the total shares,
    /// weighted average price, and accumulated loss to their initial values.
    /// </summary>
    public void Reset()
    {
        this.totalShares = 0;
        this.weightedAveragePrice = 0m;
        this.accumulatedLoss = 0m;
    }

    /// <summary>
    /// Processes a buy operation, updating the total shares and weighted average price.
    /// </summary>
    /// <param name="quantity">Number of shares bought.</param>
    /// <param name="price">Price per share.</param>
    private void Buy(int quantity, decimal price)
    {
        if (this.totalShares == 0)
            this.weightedAveragePrice = price;
        else
            this.weightedAveragePrice = ((this.totalShares * this.weightedAveragePrice) + (quantity * price)) / (this.totalShares + quantity);

        this.totalShares += quantity;
    }

    /// <summary>
    /// Processes a sell operation, calculating the capital gains and tax due.
    /// </summary>
    /// <param name="quantity">Number of shares sold.</param>
    /// <param name="price">Selling price per share.</param>
    /// <returns>The amount of tax due (if applicable).</returns>
    private decimal Sell(int quantity, decimal price)
    {
        decimal totalRevenue = quantity * price;
        decimal totalCost = quantity * this.weightedAveragePrice;
        decimal profit = totalRevenue - totalCost;
        decimal taxDue = 0m;

        this.totalShares -= quantity;

        if (profit < 0)
        {
            this.accumulatedLoss += Math.Abs(profit);
        }
        else
        {
            if (this.accumulatedLoss > 0)
            {
                decimal deductible = Math.Min(this.accumulatedLoss, profit);
                profit -= deductible;
                this.accumulatedLoss -= deductible;
            }

            if (profit > 0 && totalRevenue > ExemptionLimit)
                taxDue = profit * TaxRate;
        }

        return Math.Round(taxDue, 2);
    }
}

