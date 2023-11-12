using schedule_2;

namespace Schedule_2.Tests;

[TestFixture]
public class MonthlyScheduleTheOccurOnceTests
{
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurOnce_InvalidLimits()
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
                EveryAfterMonths = 1,
                Position = Position.First,
                Day = Day.Monday
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
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurOnce_InvalidCount()
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
                EveryAfterMonths = -1,
                Position = Position.First,
                Day = Day.Monday
            }
        };

        Assert.That(() => Scheduler.CalculateDetails(configuration), 
            Throws.ArgumentException.With.Message.EqualTo("Count can't be non positive"));
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurOnce_ExpiredDate()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 12, 5, 1, 0, 0),
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
                EveryAfterMonths = 1,
                Position = Position.First,
                Day = Day.Monday
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
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurOnce_StartDate_Before_SelectedDate()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
                EndDate = new DateTime(2024, 10, 1)
            },
            CurrentDate = new DateTime(2023, 11, 5, 1, 0, 0),
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
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurOnce_StartDate_Equal_SelectedDate()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 6),
                EndDate = new DateTime(2024, 10, 1)
            },
            CurrentDate = new DateTime(2023, 11, 5, 1, 0, 0),
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
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurOnce_InValidCurrentDate()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2025, 5, 5, 2, 0, 0),
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
                EveryAfterMonths = 1,
                Position = Position.First,
                Day = Day.Monday
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
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurOnce_Before_Time()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
                EndDate = new DateTime(2024, 10, 1)
            },
            CurrentDate = new DateTime(2023, 11, 13, 1, 0, 0),
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
                EveryAfterMonths = 1,
                Position = Position.Second,
                Day = Day.Monday
            }
        };
        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2023, 11, 13, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleDisabled_The_OccurOnce_Before_Time()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
                EndDate = new DateTime(2024, 10, 1)
            },
            CurrentDate = new DateTime(2020, 11, 13, 1, 0, 0),
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
                EveryAfterMonths = 1,
                Position = Position.Second,
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
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurOnce_Exact_Time()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
                EndDate = new DateTime(2024, 10, 1)
            },
            CurrentDate = new DateTime(2023, 11, 13, 2, 0, 0),
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
                EveryAfterMonths = 1,
                Position = Position.Second,
                Day = Day.Monday
            }
        };
        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2023, 12, 11, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleDisabled_The_OccurOnce_Exact_Time()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
                EndDate = new DateTime(2024, 10, 1)
            },
            CurrentDate = new DateTime(2023, 11, 13, 2, 0, 0),
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
                EveryAfterMonths = 1,
                Position = Position.Second,
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
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurOnce_After_Time()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
                EndDate = new DateTime(2024, 10, 1)
            },
            CurrentDate = new DateTime(2023, 11, 13, 3, 0, 0),
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
                EveryAfterMonths = 1,
                Position = Position.Second,
                Day = Day.Monday
            }
        };
        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2023, 12, 11, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleDisabled_The_OccurOnce_After_Time()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
                EndDate = new DateTime(2024, 10, 1)
            },
            CurrentDate = new DateTime(2020, 11, 13, 2, 0, 0),
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
                EveryAfterMonths = 1,
                Position = Position.Second,
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
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurOnce_NoEndDate_Exact_Time()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
            },
            CurrentDate = new DateTime(2023, 11, 13, 2, 0, 0),
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
                EveryAfterMonths = 1,
                Position = Position.Second,
                Day = Day.Monday
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2023, 12, 11, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurOnce_NoEndDate_Before_Time()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
            },
            CurrentDate = new DateTime(2023, 11, 13, 1, 0, 0),
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
                EveryAfterMonths = 1,
                Position = Position.Second,
                Day = Day.Monday
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2023, 11, 13, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurOnce_NoEndDate_After_Time()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
            },
            CurrentDate = new DateTime(2023, 11, 13, 3, 0, 0),
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
                EveryAfterMonths = 1,
                Position = Position.Second,
                Day = Day.Monday
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2023, 12, 11, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurOnce_FirstMonday_CurrentDate_Before()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
                EndDate = new DateTime(2024, 11, 1)
            },
            CurrentDate = new DateTime(2023, 11, 4, 3, 0, 0),
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
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurOnce_FirstMonday_CurrentDate_After()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
                EndDate = new DateTime(2024, 11, 1)
            },
            CurrentDate = new DateTime(2023, 11, 7, 3, 0, 0),
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
                EveryAfterMonths = 1,
                Position = Position.First,
                Day = Day.Monday
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2023, 12, 4, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurOnce_FirstTuesday_CurrentDate_Before()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
                EndDate = new DateTime(2024, 11, 1)
            },
            CurrentDate = new DateTime(2023, 11, 1, 3, 0, 0),
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
                EveryAfterMonths = 1,
                Position = Position.First,
                Day = Day.Tuesday
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2023, 11, 7, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurOnce_FirstTuesday_CurrentDate_After()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
                EndDate = new DateTime(2024, 11, 1)
            },
            CurrentDate = new DateTime(2023, 11, 12, 3, 0, 0),
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
                EveryAfterMonths = 1,
                Position = Position.First,
                Day = Day.Tuesday
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2023, 12, 5, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurOnce_FirstWednesday_CurrentDate_Before()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 10, 1),
                EndDate = new DateTime(2024, 11, 1)
            },
            CurrentDate = new DateTime(2023, 10, 1, 3, 0, 0),
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
                EveryAfterMonths = 1,
                Position = Position.First,
                Day = Day.Wednesday
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2023, 10, 4, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurOnce_FirstWednesday_CurrentDate_After()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
                EndDate = new DateTime(2024, 11, 1)
            },
            CurrentDate = new DateTime(2023, 11, 7, 3, 0, 0),
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
                EveryAfterMonths = 2,
                Position = Position.First,
                Day = Day.Wednesday
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2024, 1, 3, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurOnce_FirstWeekDay()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 7, 1),
                EndDate = new DateTime(2024, 11, 1)
            },
            CurrentDate = new DateTime(2023, 7, 2, 3, 0, 0),
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
                EveryAfterMonths = 1,
                Position = Position.First,
                Day = Day.WeekDay
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurOnce_SecondWeekDay()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
                EndDate = new DateTime(2024, 11, 1)
            },
            CurrentDate = new DateTime(2023, 11, 1, 3, 0, 0),
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
                EveryAfterMonths = 1,
                Position = Position.Second,
                Day = Day.WeekDay
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2023, 11, 2, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
    [Test]
    public void CalculateDetails_RecurringMonthlyScheduleEnabled_The_OccurOnce_FirstWeekEndDay()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 11, 1),
                EndDate = new DateTime(2024, 11, 1)
            },
            CurrentDate = new DateTime(2023, 11, 5, 3, 0, 0),
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
                EveryAfterMonths = 3,
                Position = Position.First,
                Day = Day.WeekendDay
            }
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2024, 2, 3, 2, 0, 0)));
            Assert.That(details.Description, Is.EqualTo($"Next schedule will execute on {details.NextDate}"));
        });
    }
    
}