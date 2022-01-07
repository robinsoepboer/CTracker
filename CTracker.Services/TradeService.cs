using System.Globalization;
using CTracker.DAL.Entities;
using CTracker.Models.Bitvavo;
using CTracker.Repositories;

namespace CTracker.Services;

public class TradeService : ITradeService
{
    private IBitvavoService _bitvavoService;
    private readonly ITradeRepository _tradeRepository;
    
    public TradeService(
        IBitvavoService bitvavoService, 
        ITradeRepository tradeRepository)
    {
        _bitvavoService = bitvavoService;
        _tradeRepository = tradeRepository;
    }

    public async Task Sync(Portfolio portfolio)
    {
        if (portfolio.Assets == null || !portfolio.Assets.Any())
            return;
        
        foreach (var coin in portfolio.Assets.Select(t => t.Coin))
        {
            if (coin?.Symbol == null)
                continue;

            var tradeResponses = await _bitvavoService.Trades(coin.Symbol);

            if (tradeResponses == null)
                continue;
            
            foreach (var tradeResponse in tradeResponses)
            {
                var externalId = $"bitvavo|{tradeResponse.id}";
                
                if (_tradeRepository.Any(externalId))
                    continue;
                
                var price = decimal.Parse(tradeResponse.price, NumberStyles.Number, new CultureInfo("en-US"));
                var amount = decimal.Parse(tradeResponse.amount, NumberStyles.Number, new CultureInfo("en-US"));
                var fee = decimal.Parse(tradeResponse.fee, NumberStyles.Number, new CultureInfo("en-US"));
            
                var trade = new Trade
                {
                    Amount = amount,
                    Price = price,
                    Fee = fee,
                    CoinId = coin.Id,
                    ExternalId = externalId,
                    PortfolioId = portfolio.Id,
                    Created = DateTime.Now,
                    LastModified = DateTime.Now,
                    IsDeleted = false
                };
            
                _tradeRepository.Insert(trade);
            }
            _tradeRepository.Commit();
        }
        
        
    }
}