using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CTracker.DAL.Entities;

public class Trade : Entity
{
    [Precision(18, 10)]
    public decimal Amount { get; set; }
    [Precision(18, 10)]
    public decimal Price { get; set; }
    [Precision(18, 10)]
    public decimal Fee { get; set; }

    [Required]
    public string ExternalId { get; set; } = null!;

    public int CoinId { get; set; }
    public virtual Coin? Coin { get; set; }
    
    [ForeignKey(nameof(PortfolioId))]
    [InverseProperty(nameof(Entities.Portfolio.Trades))]
    public virtual Portfolio? Portfolio { get; set; }
    public int PortfolioId { get; set; }
}