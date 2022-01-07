using CTracker.Models;
using CTracker.Models.Responses;
using CTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace CTracker.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IPriceService _priceService;
    private readonly IBitvavoService _bitvavoService;
    
    public DashboardController(
        IPriceService priceService,
        IBitvavoService bitvavoService
        )
    {
        _priceService = priceService;
        _bitvavoService = bitvavoService;
    }

    [HttpGet]
    public async Task<DashboardResponse> Get()
    {
        var response = await _priceService.Get();
        //var balanceResponse = await _bitvavoService.Balance();
        var tradesResponse = await _bitvavoService.Trades("ETH");
        
        var viewmodel = new DashboardResponse
        {
            Ethereum = new Coin { Symbol = "ETH", Price = response.Price },
            TotalResult = new TotalResult(),
            //Bitvavo = balanceResponse?.FirstOrDefault(t => t.symbol == "ETH"),
            Trades = tradesResponse
        };

        var totalAmount = 0.00m;
        foreach (var trade in tradesResponse)
        {
            var price = decimal.Parse(trade.price.Replace(".", ","));
            var amount = decimal.Parse(trade.amount.Replace(".", ","));
            totalAmount += amount;
            
            viewmodel.TotalResult.Investment += (price * amount);
            viewmodel.TotalResult.CurrentValue += (viewmodel.Ethereum.Price * amount);
            
        }

        viewmodel.TotalResult.AverageCost = Math.Round(viewmodel.TotalResult.Investment / totalAmount, 2);
        viewmodel.TotalResult.Investment = Math.Round(viewmodel.TotalResult.Investment, 2);
        viewmodel.TotalResult.CurrentValue = Math.Round(viewmodel.TotalResult.CurrentValue, 2);
        viewmodel.TotalResult.Unrealized = Math.Round(viewmodel.TotalResult.CurrentValue - viewmodel.TotalResult.Investment, 2);
        viewmodel.TotalResult.UnrealizedPercentage = Math.Round(((viewmodel.TotalResult.CurrentValue / viewmodel.TotalResult.Investment) * 100) - 100, 2);
        

        return viewmodel;
    }
}