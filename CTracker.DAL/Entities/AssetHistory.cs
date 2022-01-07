using System.ComponentModel.DataAnnotations.Schema;

namespace CTracker.DAL.Entities;

public class AssetHistory : Entity
{
    [ForeignKey(nameof(AssetId))]
    [InverseProperty(nameof(Entities.Asset.History))]
    public virtual Asset? Asset { get; set; }
    public int AssetId { get; set; }

    public decimal AverageCost { get; set; }
    public decimal Investment { get; set; }
    public decimal CurrentValue { get; set; }
    public decimal Price { get; set; }
    public decimal Unrealized { get; set; }
    public decimal UnrealizedPercentage { get; set; }
}