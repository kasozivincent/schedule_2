using schedule_2;

namespace Schedule_2.Tests;

[TestFixture]
public class MonthlyScheduleTests
{
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_Day_OccurOnce_InvalidLimits()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 1, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Daily
            },
            DailyFrequency = new DailyFrequency
            {
                OccursOnceAt = new TimeSpan(2, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 4,
                EveryAfterMonths = 1,
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        { 
            Assert.That(details.NextDate, Is.EqualTo(DateTime.MinValue));
            Assert.That(details.Description, Is.EqualTo("Schedule can't be executed"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_Day_OccurOnce_InvalidCount()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 1, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursOnceAt = new TimeSpan(2, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 4,
                EveryAfterMonths = -1,
            }
        };

        Assert.That(() => Scheduler.CalculateDetails(configuration), 
            Throws.ArgumentException.With.Message.EqualTo("Count can't be non positive"));
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_Day_OccurOnce_InvalidDate()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 1, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursOnceAt = new TimeSpan(2, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 32,
                EveryAfterMonths = 1,
            }
        };

        Assert.That(() => Scheduler.CalculateDetails(configuration), 
            Throws.ArgumentException.With.Message.EqualTo("Invalid Date"));
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_Day_OccurOnce_MonthDayBeforeStartDateDay()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 8),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 1, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursOnceAt = new TimeSpan(2, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 4,
                EveryAfterMonths = 2,
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2020, 6, 4, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleDisabled_Day_OccurOnce_MonthDayBeforeStartDateDay()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 8),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 1, 0, 0),
            Configuration = new Configuration
            {
                Enabled = false,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursOnceAt = new TimeSpan(2, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 4,
                EveryAfterMonths = 2,
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
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_Day_OccurOnce_MonthDayEqual_To_StartDateDay()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 8),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 1, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursOnceAt = new TimeSpan(2, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 8,
                EveryAfterMonths = 2,
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleDisabled_Day_OccurOnce_MonthDayEqual_To_StartDateDay()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 8),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 1, 0, 0),
            Configuration = new Configuration
            {
                Enabled = false,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursOnceAt = new TimeSpan(2, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 8,
                EveryAfterMonths = 2,
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
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_Day_OccurOnce_MonthDayAfter_StartDateDay()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 8),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 1, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursOnceAt = new TimeSpan(2, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 10,
                EveryAfterMonths = 2,
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2020, 5, 10, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_Day_OccurOnce_ExactCurrentDate_BeforeTime()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 8),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 8, 1, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursOnceAt = new TimeSpan(2, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 8,
                EveryAfterMonths = 2,
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
   
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_Day_OccurOnce_ExactCurrentDate_ExactTime()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 8),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 8, 2, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursOnceAt = new TimeSpan(2, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 8,
                EveryAfterMonths = 2,
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2020, 7, 8, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_Day_OccurOnce_ExactCurrentDate_AfterTime()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 8),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 8, 3, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursOnceAt = new TimeSpan(2, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 8,
                EveryAfterMonths = 2,
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2020, 7, 8, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleDisabled_Day_OccurOnce_MonthDayAfter_StartDateDay()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 8),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 1, 0, 0),
            Configuration = new Configuration
            {
                Enabled = false,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Monthly
            },
            DailyFrequency = new DailyFrequency
            {
                OccursOnceAt = new TimeSpan(2, 0, 0),
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 10,
                EveryAfterMonths = 2,
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
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_Day_OccurRepetitive_InvalidCount()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 1, 0, 0),
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
                EndingTime = new TimeSpan(4, 0, 0)
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 4,
                EveryAfterMonths = -1,
            }
        };

        Assert.That(() => Scheduler.CalculateDetails(configuration), 
            Throws.ArgumentException.With.Message.EqualTo("Count can't be non positive"));
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_Day_OccurRepetitive_InvalidLimits()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 1, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Daily
            },
            DailyFrequency = new DailyFrequency
            {
                OccursEvery = 1,
                IntervalType = IntervalType.Hours,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0)
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 4,
                EveryAfterMonths = 1,
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        { 
            Assert.That(details.NextDate, Is.EqualTo(DateTime.MinValue));
            Assert.That(details.Description, Is.EqualTo("Schedule can't be executed"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_Day_OccurRepetitive_InvalidDate()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 1, 0, 0),
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
                EndingTime = new TimeSpan(4, 0, 0)
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 32,
                EveryAfterMonths = 1,
            }
        };

        Assert.That(() => Scheduler.CalculateDetails(configuration), 
            Throws.ArgumentException.With.Message.EqualTo("Invalid Date"));
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_Day_OccurRepetitive_ExactCurrentDate_BeforeTime()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 8),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 8, 1, 0, 0),
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
                EndingTime = new TimeSpan(4, 0, 0)
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 8,
                EveryAfterMonths = 2,
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_Day_OccurRepetitive_ExactCurrentDate_ExactTime()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 8),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 8, 2, 0, 0),
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
                EndingTime = new TimeSpan(4, 0, 0)
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 8,
                EveryAfterMonths = 2,
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2020, 5, 8, 3, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_Day_OccurRepetitive_ExactCurrentDate_AfterTime()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 8),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 8, 5, 0, 0),
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
                EndingTime = new TimeSpan(4, 0, 0)
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 8,
                EveryAfterMonths = 2,
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2020, 7, 8, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_Day_OccurRepetitive_ExactCurrentDate_Equal_To_EndTime()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 8),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 8, 4, 0, 0),
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
                EndingTime = new TimeSpan(4, 0, 0)
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 8,
                EveryAfterMonths = 2,
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2020, 7, 8, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }

    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_Day_OccurRepetitive_MonthDayBeforeStartDateDay()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 8),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 1, 0, 0),
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
                EndingTime = new TimeSpan(4, 0, 0)
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 4,
                EveryAfterMonths = 2,
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2020, 6, 4, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleDisabled_Day_OccurRepetitive_MonthDayBeforeStartDateDay()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 8),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 1, 0, 0),
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
                EndingTime = new TimeSpan(4, 0, 0)
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 4,
                EveryAfterMonths = 2,
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
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_Day_OccurRepetitive_MonthDayEqual_To_StartDateDay()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 8),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 1, 0, 0),
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
                EndingTime = new TimeSpan(4, 0, 0)
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 4,
                EveryAfterMonths = 2,
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2020, 6, 4, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleDisabled_Day_OccurRepetitive_MonthDayEqual_To_StartDateDay()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 8),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 1, 0, 0),
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
                EndingTime = new TimeSpan(4, 0, 0)
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 4,
                EveryAfterMonths = 2,
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
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_Day_OccurRepetitive_MonthDayAfter_StartDateDay()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 8),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 1, 0, 0),
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
                EndingTime = new TimeSpan(4, 0, 0)
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 10,
                EveryAfterMonths = 2,
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2020, 5, 10, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleDisabled_Day_OccurRepetitive_MonthDayAfter_StartDateDay()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 8),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 1, 0, 0),
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
                EndingTime = new TimeSpan(4, 0, 0)
            },
            MonthlyConfiguration = new MonthlyConfiguration
            {
                MonthDay = 10,
                EveryAfterMonths = 2,
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(DateTime.MinValue));
            Assert.That(details.Description, Is.EqualTo("Schedule was canceled"));
        });
    }
}