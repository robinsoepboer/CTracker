namespace CTracker.Models.Bitvavo;

public class TradeResponse
{
    public string id { get; set; }
    public string orderId { get; set; }
    public long timestamp { get; set; }
    public string market { get; set; }
    public string side { get; set; }
    public string amount { get; set; }
    public string price { get; set; }
    public bool taker { get; set; }
    public string fee { get; set; }
    public string feeCurrency { get; set; }
    public bool settled { get; set; }
}