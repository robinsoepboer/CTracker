using System.Globalization;
using CTracker.DAL.Entities;
using CTracker.Repositories;
using CTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CTracker.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CreateAssetHistoryController : ControllerBase
{
    private readonly IRepository<Portfolio> _portfolioRepository;
    private readonly IRepository<AssetHistory> _assetHistoryRepository;
    private readonly IBitvavoService _bitvavoService;

    
    
    public CreateAssetHistoryController(
        IRepository<Portfolio> portfolioRepository,
        IRepository<AssetHistory> assetHistoryRepository,
        IBitvavoService bitvavoService)
    {
        _portfolioRepository = portfolioRepository;
        _assetHistoryRepository = assetHistoryRepository;
        _bitvavoService = bitvavoService;
    }

    [HttpGet]
    public async Task<bool> Get()
    {
        var portfolio = _portfolioRepository
            .All()
            .Include(t => t.Assets!)
            .ThenInclude(t => t.Coin)
            .Include(t => t.Trades!)
            .ThenInclude(t => t.Coin)
            .First(t => t.Name == "Test");

        foreach (var asset in portfolio.Assets!)
        {
            var response = await _bitvavoService.Ticker(asset.Coin!.Symbol!);
            
            if(response == null)
                continue;
                
            var currentPrice = decimal.Parse(response.price, NumberStyles.Number, new CultureInfo("en-US"));
            
            var trades = portfolio.Trades!.Where(t => t.CoinId == asset.CoinId);
            
            var investment = 0.00m;
            var currentValue = 0.00m;
            var totalAmount = 0.00m;
            
            foreach (var trade in trades)
            {
                totalAmount += trade.Amount;
                investment += (trade.Price * trade.Amount);
                currentValue += (currentPrice * trade.Amount);
            }

            var assetHistory = new AssetHistory
            {
                AssetId = asset.Id,
                Price = Math.Round(currentPrice, 2),
                Investment = Math.Round(investment, 2),
                CurrentValue = Math.Round(currentValue, 2),
                AverageCost = Math.Round(investment / totalAmount, 2),
                Unrealized = Math.Round(currentValue - investment, 2),
                UnrealizedPercentage = Math.Round(((currentValue / investment) * 100) - 100, 2),
                Created = DateTime.Now,
                LastModified = DateTime.Now,
            };
            
            _assetHistoryRepository.Insert(assetHistory);
            _assetHistoryRepository.Commit();
        }

        return true;
    }
}