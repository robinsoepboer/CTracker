using System.ComponentModel.DataAnnotations.Schema;

namespace CTracker.DAL.Entities;

public class Coin : Entity
{
    public string? Symbol { get; set; }
    public string? Name { get; set; }
    
    [InverseProperty(nameof(Asset.Coin))]
    public virtual ICollection<Asset> Assets { get; set; }
}