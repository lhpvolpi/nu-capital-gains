namespace NuCapitalGains.Core.Calculator.Entities;

public class Operation
{
    public Operation() { }

    public Operation(
        string operationType,
        int quantity,
        decimal unitCost)
    {
        this.OperationType = operationType;
        this.Quantity = quantity;
        this.UnitCost = unitCost;
    }

    [JsonPropertyName("operation")]
    public string OperationType { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("unit-cost")]
    public decimal UnitCost { get; set; }
}

