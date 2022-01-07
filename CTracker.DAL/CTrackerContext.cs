using CTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CTracker.DAL;

public class CTrackerContext : DbContext
{
    public CTrackerContext()
    {
    }

    public CTrackerContext(DbContextOptions<CTrackerContext> options)
        : base(options)
    {
    }

    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<Coin> Coins { get; set; }
    public DbSet<Trade> Trade { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<AssetHistory> AssetHistories { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer($"Server=.\\SQLEXPRESS;Database=CTracker;Trusted_Connection=True;");
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Portfolio>().HasData(
            new Portfolio()
            {
                Id = 1,
                Name = "Test",
                Created = DateTime.Now,
                LastModified = DateTime.Now
            }
        );
        
        modelBuilder.Entity<Coin>().HasData(
            new Coin()
            {
                Id = 1,
                Name = "Bitcoin",
                Symbol = "BTC",
                Created = DateTime.Now,
                LastModified = DateTime.Now
            },
            new Coin()
            {
                Id = 2,
                Name = "Ethereum",
                Symbol = "ETH",
                Created = DateTime.Now,
                LastModified = DateTime.Now
            },
            new Coin()
            {
                Id = 3,
                Name = "Cardano",
                Symbol = "ADA",
                Created = DateTime.Now,
                LastModified = DateTime.Now
            },
            new Coin()
            {
                Id = 4,
                Name = "USD Coin",
                Symbol = "USDC",
                Created = DateTime.Now,
                LastModified = DateTime.Now
            }
        );
        
        modelBuilder.Entity<Asset>().HasData(
            new Asset()
            {
                Id = 1,
                CoinId = 1,
                PortfolioId = 1,
                Created = DateTime.Now,
                LastModified = DateTime.Now
            },
            new Asset()
            {
                Id = 2,
                CoinId = 2,
                PortfolioId = 1,
                Created = DateTime.Now,
                LastModified = DateTime.Now
            },
            new Asset()
            {
                Id = 3,
                CoinId = 3,
                PortfolioId = 1,
                Created = DateTime.Now,
                LastModified = DateTime.Now
            },
            new Asset()
            {
                Id = 4,
                CoinId = 4,
                PortfolioId = 1,
                Created = DateTime.Now,
                LastModified = DateTime.Now
            }
        );
    }
}