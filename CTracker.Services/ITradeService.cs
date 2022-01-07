using CTracker.DAL.Entities;

namespace CTracker.Services;

public interface ITradeService
{
    Task Sync(Portfolio portfolio);
}