using schedule_2.Validators;

namespace schedule_2;

public static class Scheduler
{
    private static DateTime CalculateDailyScheduleSeries(ScheduleConfiguration conf)
        => conf.DailyFrequency!.OccursOnceAt != null 
            ? CalculateDailyOnceScheduleDetails(conf).NextDate 
            : CalculateDailyRecurringScheduleDetails(conf).NextDate;
    public static List<DateTime> DailyScheduleSeries(ScheduleConfiguration config, int count)
        => Enumerable.Range(0, count)
            .Select(_ =>
            {
                var result = CalculateDailyScheduleSeries(config);
                config.CurrentDate = result;
                return result;
            }).ToList();
    
    public static ScheduleDetails CalculateDetails(ScheduleConfiguration configuration)
    {
        if (!configuration.Configuration.Enabled)
            return new ScheduleDetails(DateTime.MinValue, "Schedule was canceled");
        
        if (configuration.Configuration.ScheduleType == ScheduleType.Once)
            return OnceScheduleDetails(configuration);
        
        if (!LimitsValidator.ValidateLimits(configuration.Limits))
            return new ScheduleDetails(DateTime.MinValue, "Schedule can't be executed");
        
        if (configuration.Limits!.EndDate != null && configuration.Limits.EndDate < configuration.CurrentDate)
            return new ScheduleDetails(DateTime.MinValue, "Schedule can't be executed");
        
        return configuration.Configuration.Occurs switch
        {
            Occurs.Daily => RecurringDailyScheduleDetails(configuration),
            Occurs.Weekly => RecurringWeeklyScheduleDetails(configuration),
            _ => RecurringMonthlyScheduleDetails(configuration)
        };
    }
    private static ScheduleDetails RecurringMonthlyScheduleDetails(ScheduleConfiguration configuration)
    {
        MonthlyValidator.ValidateMonthConfiguration(configuration.MonthlyConfiguration);
        if (configuration.MonthlyConfiguration!.MonthDay != null)
        {
            return configuration.DailyFrequency!.OccursOnceAt != null 
                ? CalculateMonthlyDayOnceScheduleDetails(configuration) 
                : CalculateMonthlyDayRecurringScheduleDetails(configuration);
        }
        return configuration.DailyFrequency!.OccursOnceAt != null 
            ? CalculateMonthlyTheOnceScheduleDetails(configuration) 
            : CalculateMonthlyTheRecurringScheduleDetails(configuration);
    }
    private static ScheduleDetails CalculateMonthlyDayRecurringScheduleDetails(ScheduleConfiguration configuration)
    {
        var count = configuration.DailyFrequency!.OccursEvery;
        var startingDate = Helpers.GetExactRunningDate(configuration);
        var startingTime = configuration.DailyFrequency.StartingTime;
        var endingTime = configuration.DailyFrequency.EndingTime;

        startingDate = new DateTime(startingDate.Year, startingDate.Month, startingDate.Day,
                startingTime.Hours, startingTime.Minutes, startingTime.Seconds);
        var currentDate = configuration.CurrentDate;
        var months = configuration.MonthlyConfiguration.EveryAfterMonths;
        while (true)
        {
            if (startingDate > currentDate)
                return new ScheduleDetails(startingDate, $"Next schedule will execute on {startingDate}");
            if (startingDate.Date == currentDate.Date)
            {
                if (currentDate.TimeOfDay < startingTime)
                    return new ScheduleDetails(startingDate, $"Next schedule will execute on {startingDate}");
                if (currentDate.TimeOfDay >= startingTime && currentDate.TimeOfDay < endingTime)
                {
                    return configuration.DailyFrequency.IntervalType switch
                    {
                        IntervalType.Hours => new ScheduleDetails(startingDate.AddHours(count),
                                $"Next schedule will execute on {startingDate.AddHours(count)}"),
                        IntervalType.Minutes => new ScheduleDetails(startingDate.AddMinutes(count),
                                $"Next schedule will execute on {startingDate.AddMinutes(count)}"),
                        _ => new ScheduleDetails(startingDate.AddSeconds(count),
                                $"Next schedule will execute on {startingDate.AddSeconds(count)}")
                    };
                }
                return new ScheduleDetails(startingDate.AddMonths(months),
                        $"Next schedule will execute on {startingDate.AddMonths(months)}");
            }
            startingDate = startingDate.AddMonths(months);
        }
    }
    private static ScheduleDetails CalculateMonthlyDayOnceScheduleDetails(ScheduleConfiguration configuration)
    {
        var startingDate = Helpers.GetExactRunningDate(configuration);
        var occuringTime = (TimeSpan)configuration.DailyFrequency.OccursOnceAt;

        startingDate = new DateTime(startingDate.Year, startingDate.Month, startingDate.Day,
            occuringTime.Hours, occuringTime.Minutes, occuringTime.Seconds);

        var currentDate = configuration.CurrentDate;
        var months = configuration.MonthlyConfiguration.EveryAfterMonths;

        while (true)
        {
            if (startingDate > currentDate)
                return new ScheduleDetails(startingDate, $"Next schedule will execute on {startingDate}");
            if (startingDate.Date == currentDate.Date)
            {
                if (currentDate.TimeOfDay < occuringTime)
                    return new ScheduleDetails(startingDate, $"Next schedule will execute on {startingDate}");
                if (currentDate.TimeOfDay >= occuringTime)
                    return new ScheduleDetails(startingDate.AddMonths(months),
                        $"Next schedule will execute on {startingDate.AddMonths(months)}");
            }
            startingDate = startingDate.AddMonths(months);
        }
    }
    private static ScheduleDetails RecurringWeeklyScheduleDetails(ScheduleConfiguration configuration)
    {
        var date = CalculateNextScheduleDate(configuration.Limits!.StartDate, configuration.CurrentDate);
        return new ScheduleDetails(date,$"Next schedule will execute on {date.Date}");
        
        DateTime CalculateNextScheduleDate(DateTime startDate, DateTime currentDate)
        {
            if (currentDate.DayOfWeek == startDate.DayOfWeek)
                return currentDate.AddDays(7);
            if (currentDate.DayOfWeek < startDate.DayOfWeek)
            {
                var daysToAdd = (int)startDate.DayOfWeek - (int)currentDate.DayOfWeek;
                return currentDate.AddDays(daysToAdd);
            }
            else
            {
                var daysToAdd = 7 - (int)currentDate.DayOfWeek + (int)startDate.DayOfWeek;
                return currentDate.AddDays(daysToAdd);
            }
        }
    }
    private static ScheduleDetails RecurringDailyScheduleDetails(ScheduleConfiguration configuration)
    {
        return configuration.DailyFrequency!.OccursOnceAt != null 
            ? CalculateDailyOnceScheduleDetails(configuration) 
            : CalculateDailyRecurringScheduleDetails(configuration);
    }
    private static ScheduleDetails CalculateDailyRecurringScheduleDetails(ScheduleConfiguration configuration)
    {
        DailyRepetitivevalidator.ValidateCount(configuration.DailyFrequency);
        var currentDateTime = configuration.CurrentDate;
        var startingTime = configuration.DailyFrequency.StartingTime;
        var endingTime = configuration.DailyFrequency.EndingTime;
        var date = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day,
            startingTime.Hours, startingTime.Minutes, startingTime.Seconds);
        switch (configuration.DailyFrequency!.IntervalType)
        {
            case IntervalType.Hours:
            {
                var hours = configuration.DailyFrequency.OccursEvery;
                if (currentDateTime.TimeOfDay < startingTime)
                    return new ScheduleDetails(date,
                        $"Next schedule will execute on {date}");
                if (currentDateTime.TimeOfDay >= startingTime && currentDateTime.TimeOfDay < endingTime)
                    return new ScheduleDetails(currentDateTime.AddHours(hours),
                        $"Next schedule will execute on {currentDateTime.AddHours(hours)}");
                if (currentDateTime.TimeOfDay >= endingTime)
                    return new ScheduleDetails(date.AddDays(hours),
                        $"Next schedule will execute on {date.AddDays(1)}");
                break;
            }
            case IntervalType.Minutes:
            {
                var minutes = configuration.DailyFrequency.OccursEvery;
                if (currentDateTime.TimeOfDay < startingTime)
                    return new ScheduleDetails(date,
                        $"Next schedule will execute on {date}");
                if (currentDateTime.TimeOfDay >= startingTime && currentDateTime.TimeOfDay < endingTime)
                    return new ScheduleDetails(currentDateTime.AddMinutes(minutes),
                        $"Next schedule will execute on {currentDateTime.AddMinutes(minutes)}");
                if (currentDateTime.TimeOfDay >= endingTime)
                    return new ScheduleDetails(date.AddDays(1),
                        $"Next schedule will execute on {date.AddDays(1)}");
                break;
            }
            case IntervalType.Seconds:
            {
                var seconds = configuration.DailyFrequency.OccursEvery;
                if (currentDateTime.TimeOfDay < startingTime)
                    return new ScheduleDetails(date,
                        $"Next schedule will execute on {date}");
                if (currentDateTime.TimeOfDay >= startingTime && currentDateTime.TimeOfDay < endingTime)
                    return new ScheduleDetails(currentDateTime.AddSeconds(seconds),
                        $"Next schedule will execute on {currentDateTime.AddSeconds(seconds)}");
                if (currentDateTime.TimeOfDay >= endingTime)
                    return new ScheduleDetails(date.AddDays(1),
                        $"Next schedule will execute on {date.AddDays(1)}");
                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }

        return null;
    }
    private static ScheduleDetails CalculateDailyOnceScheduleDetails(ScheduleConfiguration configuration)
    {
        var currentDateTime = configuration.CurrentDate;
        var occuringTime = (TimeSpan)configuration.DailyFrequency.OccursOnceAt;
        var date = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day,
            occuringTime.Hours, occuringTime.Minutes, occuringTime.Seconds);

        if (currentDateTime.TimeOfDay >= occuringTime)
            return new ScheduleDetails(date.AddDays(1),
                $"Next schedule will execute on {date.AddDays(1)}");
        return new ScheduleDetails(date, $"Next schedule will execute on {date}");
    }
    private static ScheduleDetails OnceScheduleDetails(ScheduleConfiguration configuration)
    {
        if (configuration.Configuration.ExecutionDate < configuration.CurrentDate)
            return new ScheduleDetails(DateTime.MinValue, "Schedule can't be executed");
        return new ScheduleDetails(configuration.Configuration.ExecutionDate,
            $"Next schedule will execute on {configuration.Configuration.ExecutionDate}");
    }
    private static ScheduleDetails CalculateMonthlyTheOnceScheduleDetails(ScheduleConfiguration configuration)
    {
        var currentDate = configuration.CurrentDate;
        var months = configuration.MonthlyConfiguration!.EveryAfterMonths;
        var day = configuration.MonthlyConfiguration.Day;
        var position = configuration.MonthlyConfiguration.Position;
        var startingDate = Helpers.GetExactStartDate(configuration);

        var occuringTime = (TimeSpan)configuration.DailyFrequency!.OccursOnceAt!;
        startingDate = new DateTime(startingDate.Year, startingDate.Month, startingDate.Day,
                    occuringTime.Hours, occuringTime.Minutes, occuringTime.Seconds);
        while (true)
        {
            if (startingDate.Month == currentDate.Month && startingDate.Year == currentDate.Year)
            {
                var a = Helpers.FindNthOccurrenceOfDay(startingDate, day, position);
                if (a.Day == currentDate.Day)
                {
                    if (currentDate.TimeOfDay < occuringTime)
                        return new ScheduleDetails(a, $"Next schedule will execute on {a}");
                    if (currentDate.TimeOfDay >= occuringTime)
                    {
                        var newMonth = startingDate.AddMonths(configuration.MonthlyConfiguration.EveryAfterMonths);
                        var d = Helpers.FindNthOccurrenceOfDay(newMonth, day, position);
                        return new ScheduleDetails(d, $"Next schedule will execute on {d}");
                    }
                }

                if (currentDate.Day < a.Day)
                {
                    var d = Helpers.FindNthOccurrenceOfDay(startingDate, day, position);
                    return new ScheduleDetails(d, $"Next schedule will execute on {d}");
                }

                var nextMonth = startingDate.AddMonths(configuration.MonthlyConfiguration.EveryAfterMonths);
                var date = Helpers.FindNthOccurrenceOfDay(nextMonth, day, position);
                return new ScheduleDetails(date, $"Next schedule will execute on {date}");
            }
            if (startingDate > currentDate)
            {
                var date = Helpers.FindNthOccurrenceOfDay(startingDate, day, position);
                return new ScheduleDetails(date, $"Next schedule will execute on {date}");
            }
            startingDate = startingDate.AddMonths(months);
        }
    }
    private static ScheduleDetails CalculateMonthlyTheRecurringScheduleDetails(ScheduleConfiguration configuration)
    {
        MonthlyValidator.ValidateMonthConfiguration(configuration.MonthlyConfiguration);
        var count = configuration.DailyFrequency!.OccursEvery;
        var startingDate = Helpers.GetExactStartDate(configuration);
        var startingTime = configuration.DailyFrequency.StartingTime;
        var endingTime = configuration.DailyFrequency.EndingTime;
        var currentDate = configuration.CurrentDate;
        var day = configuration.MonthlyConfiguration!.Day;
        var position = configuration.MonthlyConfiguration!.Position;
                
        startingDate = new DateTime(startingDate.Year, startingDate.Month, startingDate.Day,
            startingTime.Hours, startingTime.Minutes, startingTime.Seconds);
        var months = configuration.MonthlyConfiguration!.EveryAfterMonths;
         while (true)
        {
            if (startingDate > currentDate)
            {
                var date = Helpers.FindNthOccurrenceOfDay(startingDate, day, position);
                return new ScheduleDetails(date, $"Next schedule will execute on {date}");
            }
            if (startingDate.Month == currentDate.Month && startingDate.Year == currentDate.Year)
            {
                var a = Helpers.FindNthOccurrenceOfDay(startingDate, day, position);
                if (a.Day == currentDate.Day)
                {
                    if (currentDate.TimeOfDay < startingTime)
                        return  new ScheduleDetails(a, $"Next schedule will execute on {a}");
                    if (currentDate.TimeOfDay >= startingTime && currentDate.TimeOfDay < endingTime)
                    {
                        return configuration.DailyFrequency.IntervalType switch
                            {
                                IntervalType.Hours => new ScheduleDetails(a.AddHours(count),
                                    $"Next schedule will execute on {a.AddHours(count)}"),
                                IntervalType.Minutes => new ScheduleDetails(a.AddMinutes(count),
                                    $"Next schedule will execute on {a.AddMinutes(count)}"),
                                _ => new ScheduleDetails(a.AddSeconds(count),
                                    $"Next schedule will execute on {a.AddSeconds(count)}")
                            };
                    }
                }
                if (currentDate.Day < a.Day)
                {
                    var d = Helpers.FindNthOccurrenceOfDay(startingDate, day, position);
                    return  new ScheduleDetails(d, $"Next schedule will execute on {d}");
                }
                var nextMonth = startingDate.AddMonths(months); 
                var date = Helpers.FindNthOccurrenceOfDay(nextMonth, day, position);
                return  new ScheduleDetails(date, $"Next schedule will execute on {date}");
            }
            startingDate = startingDate.AddMonths(months);
        }
    }
}