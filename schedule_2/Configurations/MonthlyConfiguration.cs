namespace schedule_2;

public enum Position
{
    First = 1,
    Second = 2,
    Third = 3,
    Fourth = 4,
    Last = -1
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