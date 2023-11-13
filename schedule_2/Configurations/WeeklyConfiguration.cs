namespace schedule_2;

[Flags]
public enum WeekDay
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday
}

public class WeeklyConfiguration
{
    public int EveryAfterWeeks { get; set; }
    public WeekDay WeekDays { get; set; } 
}