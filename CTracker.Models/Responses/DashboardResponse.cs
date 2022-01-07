using CTracker.Models.Bitvavo;

namespace CTracker.Models.Responses;

public class DashboardResponse
{
    public Coin Ethereum { get; set; }
    public TotalResult TotalResult { get; set; }
    
    public AccountResponseItem Bitvavo { get; set; }
    public List<TradeResponse> Trades { get; set; }
}