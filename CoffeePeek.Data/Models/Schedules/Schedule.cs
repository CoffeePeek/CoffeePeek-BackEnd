namespace CoffeePeek.Data.Models.Schedules;

public class Schedule : BaseModel
{
    public int CoffeeShopId { get; set; }

    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan? OpeningTime { get; set; }
    public TimeSpan? ClosingTime { get; set; }
    public bool IsOpen24Hours { get; set; }
    
    public Shop.Shop Shop { get; set; }
}