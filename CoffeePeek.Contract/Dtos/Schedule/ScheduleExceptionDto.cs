using CoffeePeek.Contract.Enums;

namespace CoffeePeek.Contract.Dtos.Schedule;

public class ScheduleExceptionDto
{
    public DateTime ExceptionStartDate { get; set; }
    public DateTime ExceptionEndDate { get; set; }
    public ScheduleExceptionType OverrideScheduleType { get; set; }
    public TimeSpan? SpecialOpeningTime { get; set; }
    public TimeSpan? SpecialClosingTime { get; set; }
    public bool IsSpecialOpen24Hours { get; set; }
    public string? ExceptionReason { get; set; }
}