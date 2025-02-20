namespace CoffeePeek.Contract.Dtos.Schedule;

public class ScheduleDto
{
    public int ScheduleId { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan? OpeningTime { get; set; }
    public TimeSpan? ClosingTime { get; set; }
    public bool IsOpen24Hours { get; set; }
}