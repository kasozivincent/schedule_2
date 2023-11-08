namespace schedule_2;

public class DailyFrequency
{
    public TimeSpan? OccursOnceAt { get; set; }
    public int OccursEvery { get; set; }
    public IntervalType IntervalType { get; set; }
    public TimeSpan StartingTime { get; set; }
    public TimeSpan EndingTime { get; set; }
}