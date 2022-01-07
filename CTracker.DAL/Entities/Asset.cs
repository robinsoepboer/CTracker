using System.ComponentModel.DataAnnotations.Schema;

namespace CTracker.DAL.Entities;

public class Asset : Entity
{
    
    [ForeignKey(nameof(PortfolioId))]
    [InverseProperty(nameof(Entities.Portfolio.Assets))]
    public virtual Portfolio? Portfolio { get; set; }
    public int PortfolioId { get; set; }

    [ForeignKey(nameof(CoinId))]
    [InverseProperty(nameof(Entities.Coin.Assets))]
    public virtual Coin? Coin { get; set; }
    public int CoinId { get; set; }

    [InverseProperty(nameof(AssetHistory.Asset))]
    public virtual ICollection<AssetHistory>? History { get; set; }
}