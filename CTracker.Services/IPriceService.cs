using CTracker.Models;

namespace CTracker.Services;

public interface IPriceService
{
    Task<Coin> Get();
}