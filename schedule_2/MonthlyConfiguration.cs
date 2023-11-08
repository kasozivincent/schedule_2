namespace schedule_2;

public enum Position
{
    First,
    Second,
    Third,
    Fourth,
    Last
}

public enum Day
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday,
    Day,
    WeekDay,
    WeekendDay
}

public class MonthlyConfiguration
{
    public int? MonthDay { get; set; }
    public int EveryAfterMonths { get; set; }
    public Position Position { get; set; }
    public Day Day { get; set; }
}