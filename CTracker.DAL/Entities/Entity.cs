using System.ComponentModel.DataAnnotations;

namespace CTracker.DAL.Entities;

public class Entity
{
    [Key]
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastModified { get; set; }
}