using schedule_2;

namespace Schedule_2.Tests;

[TestFixture]
public class DailyOnceTests
{
   
    [Test]
    public void SeriesDailyOnce_CurrentDateTime_BeforeExecutionTime()
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
                Occurs = Occurs.Daily
            },
            DailyFrequency = new DailyFrequency
            {
                OccursOnceAt = new TimeSpan(2, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 6);
        Assert.That(details, Has.Count.EqualTo(6));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 5, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 9, 2, 0, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 10, 2, 0, 0)));
        });
    }

    [Test]
    public void SeriesDailyOnce_CurrentDateTime_BeforeExecutionTime_NoEndDate()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
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
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 6);
        Assert.That(details, Has.Count.EqualTo(6));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 5, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 9, 2, 0, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 10, 2, 0, 0)));
        });
    }
    
    [Test]
    public void SeriesDailyOnce_CurrentDateTime_Equal_To_ExecutionTime()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 2, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Daily
            },
            DailyFrequency = new DailyFrequency
            {
                OccursOnceAt = new TimeSpan(2, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 6);
        Assert.That(details, Has.Count.EqualTo(6));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 9, 2, 0, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 10, 2, 0, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 11, 2, 0, 0)));
        });
    }
    
    [Test]
    public void SeriesDailyOnce_CurrentDateTime_Equal_To_ExecutionTime_NoEndDate()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
            },
            CurrentDate = new DateTime(2020, 5, 5, 2, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Daily
            },
            DailyFrequency = new DailyFrequency
            {
                OccursOnceAt = new TimeSpan(2, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 6);
        Assert.That(details, Has.Count.EqualTo(6));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 9, 2, 0, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 10, 2, 0, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 11, 2, 0, 0)));
        });
    }
    
    [Test]
    public void SeriesDailyOnce_CurrentDateTime_AfterExecutionTime()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 3, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Daily
            },
            DailyFrequency = new DailyFrequency
            {
                OccursOnceAt = new TimeSpan(2, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 6);
        Assert.That(details, Has.Count.EqualTo(6));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 9, 2, 0, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 10, 2, 0, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 11, 2, 0, 0)));
        });
    }
    
    [Test]
    public void SeriesDailyOnce_CurrentDateTime_AfterExecutionTime_NoEndDate()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
            },
            CurrentDate = new DateTime(2020, 5, 5, 3, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Daily
            },
            DailyFrequency = new DailyFrequency
            {
                OccursOnceAt = new TimeSpan(2, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 6);
        Assert.That(details, Has.Count.EqualTo(6));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 9, 2, 0, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 10, 2, 0, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 11, 2, 0, 0)));
        });
    }
    
    [Test]
    [Category("Invalid-parameters")]
    public void CalculateDetails_RecurringDailyScheduleEnabled_OccurOnce_InvalidLimits()
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
    [Category("Disabled")]
    public void CalculateDetails_RecurringDailyScheduleDisabled_OccurOnce_Before_Time()
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
                Enabled = false,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Daily
            },
            DailyFrequency = new DailyFrequency
            {
                OccursOnceAt = new TimeSpan(2, 0, 0),
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
    [Category("Invalid-parameters")]
    public void CalculateDetails_RecurringDailyScheduleEnabled_OccurOnce_InValidCurrentDate()
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
                Occurs = Occurs.Daily
            },
            DailyFrequency = new DailyFrequency
            {
                OccursOnceAt = new TimeSpan(2, 0, 0),
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
    [Category("Disabled")]
    public void CalculateDetails_RecurringDailyScheduleDisabled_OccurOnce_Exact_Time()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 2, 0, 0),
            Configuration = new Configuration
            {
                Enabled = false,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Daily
            },
            DailyFrequency = new DailyFrequency
            {
                OccursOnceAt = new TimeSpan(2, 0, 0),
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
    [Category("Disabled")]
    public void CalculateDetails_RecurringDailyScheduleDisabled_OccurOnce_NoEndDate_Exact_Time()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
            },
            CurrentDate = new DateTime(2020, 5, 5, 2, 0, 0),
            Configuration = new Configuration
            {
                Enabled = false,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Daily
            },
            DailyFrequency = new DailyFrequency
            {
                OccursOnceAt = new TimeSpan(2, 0, 0),
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
    [Category("Disabled")]
    public void CalculateDetails_RecurringDailyScheduleDisabled_OccurOnce_After_Time()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 3, 0, 0),
            Configuration = new Configuration
            {
                Enabled = false,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Daily
            },
            DailyFrequency = new DailyFrequency
            {
                OccursOnceAt = new TimeSpan(2, 0, 0),
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
    [Category("Disabled")]
    public void CalculateDetails_RecurringDailyScheduleDisabled_OccurOnce_NoEndDate_After_Time()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
            },
            CurrentDate = new DateTime(2020, 5, 5, 3, 0, 0),
            Configuration = new Configuration
            {
                Enabled = false,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Daily
            },
            DailyFrequency = new DailyFrequency
            {
                OccursOnceAt = new TimeSpan(2, 0, 0),
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