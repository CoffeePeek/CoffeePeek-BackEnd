using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeePeek.Data.Models;

public class BaseModel
{
    public int Id { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}