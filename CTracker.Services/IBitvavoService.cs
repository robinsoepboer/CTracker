using CTracker.Models.Bitvavo;

namespace CTracker.Services;

public interface IBitvavoService
{
    Task<List<AccountResponseItem>?> Balance();
    Task<List<TradeResponse>?> Trades(string coinSymbol);
    Task<TickerResponse?> Ticker(string coinSymbol);
    Task<TimeResponse?> Time();
}