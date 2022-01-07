using CTracker.DAL.Entities;
using CTracker.Repositories;
using CTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CTracker.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SyncController : ControllerBase
{
    private readonly IRepository<Portfolio> _portfolioRepository;
    private ITradeService _tradeService;
    
    
    public SyncController(
        IRepository<Portfolio> portfolioRepository,
        ITradeService tradeService)
    {
        _portfolioRepository = portfolioRepository;
        _tradeService = tradeService;
    }

    [HttpGet]
    public async Task<bool> Get()
    {
        var portfolio = _portfolioRepository
            .All()
            .Include(t => t.Assets!)
            .ThenInclude(t => t.Coin)
            .First(t => t.Name == "Test");

        if (portfolio.Assets == null || !portfolio.Assets.Any())
            return false;

        await _tradeService.Sync(portfolio);

        return true;
    }
}