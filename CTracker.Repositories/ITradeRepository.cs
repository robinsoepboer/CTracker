using CTracker.DAL.Entities;

namespace CTracker.Repositories;

public interface ITradeRepository : IRepository<Trade>
{
    bool Any(string externalId);
}