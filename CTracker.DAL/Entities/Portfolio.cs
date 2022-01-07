using System.ComponentModel.DataAnnotations.Schema;

namespace CTracker.DAL.Entities;

public class Portfolio : Entity
{
    public string? Name { get; set; }

    [InverseProperty(nameof(Asset.Portfolio))]
    public virtual ICollection<Asset>? Assets { get; set; }
    [InverseProperty(nameof(Trade.Portfolio))]
    public virtual ICollection<Trade>? Trades { get; set; }
}