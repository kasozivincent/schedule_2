namespace schedule_2;

public static class Helpers
{
    public static DateTime GetExactStartDate(ScheduleConfiguration configuration)
    {
        var startingDate = configuration.Limits!.StartDate;
        var day = configuration.MonthlyConfiguration!.Day;
        var date = GetDateOfDay(startingDate, day, configuration.MonthlyConfiguration.Position);
        return date >= startingDate ?  startingDate
            : startingDate.AddMonths(configuration.MonthlyConfiguration.EveryAfterMonths);
    }
    private static DateTime GetDateOfDay(DateTime startDate, Day dayOfWeek, Position position)
    {
        var firstDayOfMonth = new DateTime(startDate.Year, startDate.Month, 1);
        var day = dayOfWeek switch
        {
            Day.WeekendDay => GetFirstWeekEnd(firstDayOfMonth),
            Day.WeekDay => GetFirstWeekDay(firstDayOfMonth),
            Day.Monday => DayOfWeek.Monday,
            Day.Tuesday => DayOfWeek.Tuesday,
            Day.Wednesday => DayOfWeek.Wednesday,
            Day.Thursday => DayOfWeek.Thursday,
            Day.Friday => DayOfWeek.Friday,
            Day.Saturday => DayOfWeek.Saturday,
            Day.Sunday => DayOfWeek.Sunday,
            Day.Day => GetFirstDay(firstDayOfMonth),
            _ => throw new ArgumentOutOfRangeException(nameof(dayOfWeek), dayOfWeek, null)
        };

        var dayOfWeekValue = (int)day;
        var positionIndex = (int)position;
        var daysToAdd = ((dayOfWeekValue - (int)firstDayOfMonth.DayOfWeek + 7) % 7) + (7 * (positionIndex - 1));
        var dateOfDay = firstDayOfMonth.AddDays(daysToAdd);
        return dateOfDay;
    }
    private static DateTime FindLastOccurrenceOfDay(DateTime date, Day dayOfWeek)
    {
        var lastDayOfMonth = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        while (lastDayOfMonth.DayOfWeek.ToString() != dayOfWeek.ToString())
        {
            lastDayOfMonth = lastDayOfMonth.AddDays(-1);
        }
        return lastDayOfMonth;
    }
    private static DayOfWeek GetFirstDay(DateTime date)
    {
        var firstDay = new DateTime(date.Year, date.Month, 1);
        return firstDay.DayOfWeek;
    }
    private static DayOfWeek GetFirstWeekDay(DateTime date)
    {
        var firstDay = new DateTime(date.Year, date.Month, 1);
        while (true)
        {
            if (firstDay.DayOfWeek != DayOfWeek.Saturday && firstDay.DayOfWeek != DayOfWeek.Sunday)
                return firstDay.DayOfWeek;
            firstDay = firstDay.AddDays(1);
        }
    }
    private static DayOfWeek GetFirstWeekEnd(DateTime date)
    {
        var firstDay = new DateTime(date.Year, date.Month, 1);
        while (true)
        {
            if (firstDay.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
                return firstDay.DayOfWeek;
            firstDay = firstDay.AddDays(1);
        }
    }
    public static DateTime FindNthOccurrenceOfDay(DateTime date, Day dayOfWeek, Position occurrence)
    {
        var firstDayOfMonth = new DateTime(date.Year, date.Month, 1, date.Hour, date.Minute, date.Second);
        var day = dayOfWeek switch
        {
            Day.WeekendDay => GetFirstWeekEnd(firstDayOfMonth),
            Day.WeekDay => GetFirstWeekDay(firstDayOfMonth),
            Day.Monday => DayOfWeek.Monday,
            Day.Tuesday => DayOfWeek.Tuesday,
            Day.Wednesday => DayOfWeek.Wednesday,
            Day.Thursday => DayOfWeek.Thursday,
            Day.Friday => DayOfWeek.Friday,
            Day.Saturday => DayOfWeek.Saturday,
            Day.Sunday => DayOfWeek.Sunday,
            Day.Day => GetFirstDay(firstDayOfMonth),
            _ => throw new ArgumentOutOfRangeException(nameof(dayOfWeek), dayOfWeek, null)
        };
        var daysToAdd = ((int)day - (int)firstDayOfMonth.DayOfWeek + 7) % 7;

        var nthOccurrence = occurrence switch
        {
            Position.First => firstDayOfMonth.AddDays(daysToAdd),
            Position.Second => firstDayOfMonth.AddDays(daysToAdd + 7),
            Position.Third => firstDayOfMonth.AddDays(daysToAdd + 14),
            Position.Fourth => firstDayOfMonth.AddDays(daysToAdd + 21),
            Position.Last => FindLastOccurrenceOfDay(date, dayOfWeek),
            _ => throw new ArgumentException("Invalid occurrence value.")
        };
        return nthOccurrence;
    }
    public static DateTime GetExactRunningDate(ScheduleConfiguration configuration)
    {
        var startingDate = configuration.Limits!.StartDate;
        var startingDateDay = configuration.Limits!.StartDate.Day;
        var runDate = (int)configuration.MonthlyConfiguration!.MonthDay;

        if (startingDateDay == runDate)
            return startingDate;
        if (runDate > startingDateDay)
            return new DateTime(startingDate.Year, startingDate.Month, runDate);
        var b = startingDate.AddMonths(1);
        return new DateTime(b.Year, b.Month, runDate);
    }
}