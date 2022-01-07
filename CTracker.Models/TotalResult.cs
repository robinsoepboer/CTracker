namespace CTracker.Models;

public class TotalResult
{
    public decimal Investment { get; set; }
    public decimal CurrentValue { get; set; }
    public decimal Unrealized { get; set; }
    public decimal UnrealizedPercentage{ get; set; }
    public decimal AverageCost{ get; set; }
}