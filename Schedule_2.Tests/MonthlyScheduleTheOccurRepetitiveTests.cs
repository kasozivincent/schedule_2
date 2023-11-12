using schedule_2;

namespace Schedule_2.Tests;

[TestFixture]
public class MonthlyScheduleTheOccurRepetitiveTests
{
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurRepetitive_ExactDate_Equal_StartTime_Hours()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
                EndDate = new DateTime(2024, 11, 1)
            },
            CurrentDate = new DateTime(2023, 11, 6, 2, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursEvery = 1,
                IntervalType = IntervalType.Hours,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                EveryAfterMonths = 1,
                Position = Position.First,
                Day = Day.Monday
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2023, 11, 6, 3, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurRepetitive_ExactDate_NoEndDate_Equal_StartTimeHours()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
            },
            CurrentDate = new DateTime(2023, 11, 6, 2, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursEvery = 1,
                IntervalType = IntervalType.Hours,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                EveryAfterMonths = 1,
                Position = Position.First,
                Day = Day.Monday
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2023, 11, 6, 3, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleDisabled_The_OccurRepetitive_ExactDate_NoEndDate_Equal_StartTimeHours()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
            },
            CurrentDate = new DateTime(2023, 11, 6, 2, 0, 0),
            Configuration = new Configuration
            {
                Enabled = false,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursEvery = 1,
                IntervalType = IntervalType.Hours,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                EveryAfterMonths = 1,
                Position = Position.First,
                Day = Day.Monday
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(DateTime.MinValue));
            Assert.That(details.Description, Is.EqualTo("Schedule was canceled"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurRepetitive_ExactDate_BeforeStartTime_Hours()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
                EndDate = new DateTime(2024, 11, 1)
            },
            CurrentDate = new DateTime(2023, 11, 6, 1, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursEvery = 1,
                IntervalType = IntervalType.Hours,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                EveryAfterMonths = 1,
                Position = Position.First,
                Day = Day.Monday
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2023, 11, 6, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurRepetitive_ExactDate_NoEndDate_BeforeStartTime_Hours()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
            },
            CurrentDate = new DateTime(2023, 11, 6, 1, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursEvery = 1,
                IntervalType = IntervalType.Hours,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                EveryAfterMonths = 1,
                Position = Position.First,
                Day = Day.Monday
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2023, 11, 6, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleDisabled_The_OccurRepetitive_ExactDate_NoEndDate_BeforeStartTime_Hours()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
            },
            CurrentDate = new DateTime(2023, 11, 6, 1, 0, 0),
            Configuration = new Configuration
            {
                Enabled = false,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursEvery = 1,
                IntervalType = IntervalType.Hours,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                EveryAfterMonths = 1,
                Position = Position.First,
                Day = Day.Monday
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(DateTime.MinValue));
            Assert.That(details.Description, Is.EqualTo("Schedule was canceled"));
        });
    }

    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurRepetitive_ExactDate_BeforeStartTime_Minutes()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
                EndDate = new DateTime(2024, 11, 1)
            },
            CurrentDate = new DateTime(2023, 11, 6, 1, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursEvery = 1,
                IntervalType = IntervalType.Minutes,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                EveryAfterMonths = 1,
                Position = Position.First,
                Day = Day.Monday
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2023, 11, 6, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurRepetitive_ExactDate_BeforeStartTime_Seconds()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
                EndDate = new DateTime(2024, 11, 1)
            },
            CurrentDate = new DateTime(2023, 11, 6, 1, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursEvery = 1,
                IntervalType = IntervalType.Seconds,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                EveryAfterMonths = 1,
                Position = Position.First,
                Day = Day.Monday
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2023, 11, 6, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }

    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurRepetitive_ExactDate_Equal_StartTime_Minutes()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
                EndDate = new DateTime(2024, 11, 1)
            },
            CurrentDate = new DateTime(2023, 11, 6, 2, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursEvery = 30,
                IntervalType = IntervalType.Minutes,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                EveryAfterMonths = 1,
                Position = Position.First,
                Day = Day.Monday
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2023, 11, 6, 2, 30, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurRepetitive_ExactDate_Equal_StartTime_Seconds()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
                EndDate = new DateTime(2024, 11, 1)
            },
            CurrentDate = new DateTime(2023, 11, 6, 2, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursEvery = 40,
                IntervalType = IntervalType.Seconds,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                EveryAfterMonths = 1,
                Position = Position.First,
                Day = Day.Monday
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2023, 11, 6, 2, 0, 40)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurRepetitive_ExactDate_AfterEndTime_Hours()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
                EndDate = new DateTime(2024, 11, 1)
            },
            CurrentDate = new DateTime(2023, 11, 6, 5, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursEvery = 2,
                IntervalType = IntervalType.Hours,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                EveryAfterMonths = 2,
                Position = Position.First,
                Day = Day.Monday
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2024, 1, 1, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurRepetitive_ExactDate_AfterEndTime_Minutes()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
                EndDate = new DateTime(2024, 11, 1)
            },
            CurrentDate = new DateTime(2023, 11, 6, 5, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursEvery = 2,
                IntervalType = IntervalType.Minutes,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                EveryAfterMonths = 2,
                Position = Position.First,
                Day = Day.Monday
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2024, 1, 1, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurRepetitive_ExactDate_AfterEndTime_Seconds()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
                EndDate = new DateTime(2024, 11, 1)
            },
            CurrentDate = new DateTime(2023, 11, 6, 5, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursEvery = 2,
                IntervalType = IntervalType.Seconds,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                EveryAfterMonths = 2,
                Position = Position.First,
                Day = Day.Monday
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2024, 1, 1, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    
    
}