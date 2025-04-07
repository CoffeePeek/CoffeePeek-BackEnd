using CoffeePeek.Data.Entities;

namespace CoffeePeek.Data.Models.Schedules;

public class Schedule : BaseEntity
{
    public int ShopId { get; set; }

    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan? OpeningTime { get; set; }
    public TimeSpan? ClosingTime { get; set; }
    public bool IsOpen24Hours { get; set; }
    
    public Entities.Shop.Shop Shop { get; set; }
}