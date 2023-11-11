using schedule_2.Validators;

namespace schedule_2;

public static class Scheduler
{
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
            if (configuration.DailyFrequency!.OccursOnceAt != null)
            {
                var startingDate = GetExactRunningDate();
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
                            return  new ScheduleDetails(startingDate, $"Next schedule will execute on {startingDate}");
                        if(currentDate.TimeOfDay >= occuringTime)
                            return  new ScheduleDetails(startingDate.AddMonths(months), $"Next schedule will execute on {startingDate.AddMonths(months)}");
                    }
                    startingDate = startingDate.AddMonths(months);
                }
            }
            else
            {
                var count = configuration.DailyFrequency.OccursEvery;
                var startingDate = GetExactRunningDate();
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
                            return  new ScheduleDetails(startingDate, $"Next schedule will execute on {startingDate}");
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
                        return  new ScheduleDetails(startingDate.AddMonths(months), $"Next schedule will execute on {startingDate.AddMonths(months)}");
                    }
                    startingDate = startingDate.AddMonths(months);
                }
            }
        }
        {
            var currentDate = configuration.CurrentDate;
            var months = configuration.MonthlyConfiguration.EveryAfterMonths;
            var day = configuration.MonthlyConfiguration.Day;
            var position = configuration.MonthlyConfiguration.Position;
            var startingDate = GetExactStartDate();
            var occuringTime = (TimeSpan)configuration.DailyFrequency!.OccursOnceAt!;
            startingDate = new DateTime(startingDate.Year, startingDate.Month, startingDate.Day,
                occuringTime.Hours, occuringTime.Minutes, occuringTime.Seconds);

            if (configuration.DailyFrequency!.OccursOnceAt != null)
            {
                while (true)
                {
                    if (startingDate > currentDate)
                    {
                        var date = FindNthOccurrenceOfDay(startingDate, day, position);
                        return new ScheduleDetails(date, $"Next schedule will execute on {date}");
                    }
                    if (startingDate.Month == currentDate.Month && startingDate.Year == currentDate.Year)
                    {
                        var a = FindNthOccurrenceOfDay(startingDate, day, position);
                        if (a.Day == currentDate.Day)
                        {
                            if (currentDate.TimeOfDay < occuringTime)
                                return  new ScheduleDetails(startingDate, $"Next schedule will execute on {startingDate}");
                            if (currentDate.TimeOfDay >= occuringTime)
                            {
                                var newMonth =
                                    startingDate.AddMonths(configuration.MonthlyConfiguration.EveryAfterMonths); 
                                var d = FindNthOccurrenceOfDay(newMonth, day, position);
                                return  new ScheduleDetails(d, $"Next schedule will execute on {d}");
                            }
                        }

                        if (currentDate.Day < a.Day)
                        {
                            var d = FindNthOccurrenceOfDay(startingDate, day, position);
                            return  new ScheduleDetails(d, $"Next schedule will execute on {d}");
                        }
                        var nextMonth =
                            startingDate.AddMonths(configuration.MonthlyConfiguration.EveryAfterMonths); 
                        var date = FindNthOccurrenceOfDay(nextMonth, day, position);
                        return  new ScheduleDetails(date, $"Next schedule will execute on {date}");
                    }
                    startingDate = startingDate.AddMonths(months);
                }
            }
            
            var count = configuration.DailyFrequency.OccursEvery;
            var startingTime = configuration.DailyFrequency.StartingTime;
            var endingTime = configuration.DailyFrequency.EndingTime;
                
            startingDate = new DateTime(startingDate.Year, startingDate.Month, startingDate.Day,
                startingTime.Hours, startingTime.Minutes, startingTime.Seconds);
            
            while (true)
            {
                if (startingDate > currentDate)
                {
                    var date = FindNthOccurrenceOfDay(startingDate, day, position);
                    return new ScheduleDetails(date, $"Next schedule will execute on {date}");
                }
                if (startingDate.Date == currentDate.Date)
                {
                    if (currentDate.TimeOfDay < startingTime)
                        return  new ScheduleDetails(startingDate, $"Next schedule will execute on {startingDate}");
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
                    return  new ScheduleDetails(startingDate.AddMonths(months), $"Next schedule will execute on {startingDate.AddMonths(months)}");
                }
                startingDate = startingDate.AddMonths(months);
            }
        }

        DateTime GetExactStartDate()
        {
            var startingDate = configuration.Limits!.StartDate;
            var day = configuration.MonthlyConfiguration!.Day;
            var date = GetDateOfDay(startingDate, day);
            return date >= startingDate ?  startingDate
                : startingDate.AddMonths(configuration.MonthlyConfiguration.EveryAfterMonths);
        }

        DateTime GetExactRunningDate()
        {
           // var currentDateTime = configuration.CurrentDate.TimeOfDay;
            var startingDate = configuration.Limits!.StartDate;
            var startingDateDay = configuration.Limits!.StartDate.Day;
            var runDate = (int)configuration.MonthlyConfiguration.MonthDay;

            if (startingDateDay == runDate)
                return startingDate;
            if (runDate > startingDateDay)
                return  new DateTime(startingDate.Year, startingDate.Month, runDate);
            var b = startingDate.AddMonths(1);
            return new DateTime(b.Year, b.Month, runDate);
        }

        DateTime GetDateOfDay(DateTime startDate, Day dayOfWeek)
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
            var positionIndex = GetPositionIndex();
            var daysToAdd = ((dayOfWeekValue - (int)firstDayOfMonth.DayOfWeek + 7) % 7) + (7 * (positionIndex - 1));
            var dateOfDay = firstDayOfMonth.AddDays(daysToAdd);
            return dateOfDay;
        }
        
        DayOfWeek GetFirstDay(DateTime date)
        {
            var firstDay = new DateTime(date.Year, date.Month, 1);
            return firstDay.DayOfWeek;
        }
        
        DayOfWeek GetFirstWeekDay(DateTime date)
        {
            var firstDay = new DateTime(date.Year, date.Month, 1);
            while (true)
            {
                if (firstDay.DayOfWeek != DayOfWeek.Saturday && firstDay.DayOfWeek != DayOfWeek.Sunday)
                    return firstDay.DayOfWeek;
                firstDay = firstDay.AddDays(1);
            }
        }
        
        DayOfWeek GetFirstWeekEnd(DateTime date)
         {
            var firstDay = new DateTime(date.Year, date.Month, 1);
            while (true)
            {
                if (firstDay.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
                    return firstDay.DayOfWeek;
                firstDay = firstDay.AddDays(1);
            }
         }
        
        int GetPositionIndex()
        {
            var position = configuration.MonthlyConfiguration!.Position;
            return position switch
            {
                Position.First => 1,
                Position.Second => 2,
                Position.Third => 3,
                Position.Fourth => 4,
                _ => -1
            };
        }
     
        DateTime FindNthOccurrenceOfDay(DateTime date, Day dayOfWeek, Position occurrence)
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
      
        DateTime FindLastOccurrenceOfDay(DateTime date, Day dayOfWeek)
        {
          var lastDayOfMonth = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
          while (lastDayOfMonth.DayOfWeek.ToString() != dayOfWeek.ToString())
          {
              lastDayOfMonth = lastDayOfMonth.AddDays(-1);
          }
          return lastDayOfMonth;
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
        if (configuration.DailyFrequency!.OccursOnceAt != null)
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
        else
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
    }
    private static ScheduleDetails OnceScheduleDetails(ScheduleConfiguration configuration)
    {
        if (configuration.Configuration.ExecutionDate < configuration.CurrentDate)
            return new ScheduleDetails(DateTime.MinValue, "Schedule can't be executed");
        return new ScheduleDetails(configuration.Configuration.ExecutionDate,
            $"Next schedule will execute on {configuration.Configuration.ExecutionDate}");
    }
}