using CTracker.DAL;
using CTracker.DAL.Entities;

namespace CTracker.Repositories;

public class TradeRepository : Repository<Trade>, ITradeRepository
{
    public TradeRepository(CTrackerContext context)
    : base(context)
    {
    }

    public bool Any(string externalId)
    {
        return DbSet.Any(t => t.ExternalId == externalId);
    }
}