using schedule_2;

namespace Schedule_2.Tests;

[TestFixture]
public class DailyRecurringTests
{
    [Test]
    [Category("Hour")]
    public void SeriesDailyRecurring_CurrentDateTime_BeforeStartingTime_Hours()
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
                OccursEvery = 1,
                IntervalType = IntervalType.Hours,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 5, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 5, 3, 0, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 5, 4, 0, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 6, 3, 0, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 6, 4, 0, 0)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 7, 3, 0, 0)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 7, 4, 0, 0)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0)));
        });
    }
    
    [Test]
    [Category("Hour-NoEndDate")]
    public void SeriesDailyRecurring_CurrentDateTime_BeforeStartingTime_Hours_NoEndDate()
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
                OccursEvery = 1,
                IntervalType = IntervalType.Hours,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 5, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 5, 3, 0, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 5, 4, 0, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 6, 3, 0, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 6, 4, 0, 0)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 7, 3, 0, 0)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 7, 4, 0, 0)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0)));
        });
    }
  
    [Test]
    [Category("Minute")]
    public void SeriesDailyRecurring_CurrentDateTime_BeforeStartingTime_Minutes()
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
                OccursEvery = 30,
                IntervalType = IntervalType.Minutes,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 5, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 5, 2, 30, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 5, 3, 0, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 5, 3, 30, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 5, 4, 0, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 6, 2, 30, 0)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 6, 3, 0, 0)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 6, 3, 30, 0)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 6, 4, 0, 0)));
        });
    }
    
    [Test]
    [Category("Minute-NoEndDate")]
    public void SeriesDailyRecurring_CurrentDateTime_BeforeStartingTime_Minutes_NoEndDate()
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
                OccursEvery = 30,
                IntervalType = IntervalType.Minutes,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 5, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 5, 2, 30, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 5, 3, 0, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 5, 3, 30, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 5, 4, 0, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 6, 2, 30, 0)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 6, 3, 0, 0)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 6, 3, 30, 0)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 6, 4, 0, 0)));
        });
    }
    
    [Test]
    [Category("Second")]
    public void SeriesDailyRecurring_CurrentDateTime_BeforeStartingTime_Seconds()
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
                OccursEvery = 30,
                IntervalType = IntervalType.Seconds,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 5, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 5, 2, 0, 30)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 5, 2, 1, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 5, 2, 1, 30)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 5, 2, 2, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 5, 2, 2, 30)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 5, 2, 3, 0)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 5, 2, 3, 30)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 5, 2, 4, 0)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 5, 2, 4, 30)));
        });
    }
    
    [Test]
    [Category("Second-NoEndDate")]
    public void SeriesDailyRecurring_CurrentDateTime_BeforeStartingTime_Seconds_NoEndDate()
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
                OccursEvery = 30,
                IntervalType = IntervalType.Seconds,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 5, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 5, 2, 0, 30)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 5, 2, 1, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 5, 2, 1, 30)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 5, 2, 2, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 5, 2, 2, 30)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 5, 2, 3, 0)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 5, 2, 3, 30)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 5, 2, 4, 0)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 5, 2, 4, 30)));
        });
    }
    
    [Test]
    [Category("Hour")]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_StartingTime_Hours()
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
                OccursEvery = 1,
                IntervalType = IntervalType.Hours,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 5, 3, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 5, 4, 0, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 6, 3, 0, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 6, 4, 0, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 7, 3, 0, 0)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 7, 4, 0, 0)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 8, 3, 0, 0)));
        });
    }
    
    [Test]
    [Category("Hour-NoEndDate")]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_StartingTime_Hours_NoEndDate()
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
                OccursEvery = 1,
                IntervalType = IntervalType.Hours,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 5, 3, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 5, 4, 0, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 6, 3, 0, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 6, 4, 0, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 7, 3, 0, 0)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 7, 4, 0, 0)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 8, 3, 0, 0)));
        });
    }
    
    [Test]
    [Category("Minute")]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_StartingTime_Minutes()
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
                OccursEvery = 30,
                IntervalType = IntervalType.Minutes,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 5, 2, 30, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 5, 3, 0, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 5, 3, 30, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 5, 4, 0, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 6, 2, 30, 0)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 6, 3, 0, 0)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 6, 3, 30, 0)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 6, 4, 0, 0)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0)));
        });
    }
    
    [Test]
    [Category("Minute-NoEndDate")]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_StartingTime_Minutes_NoEndDate()
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
                OccursEvery = 30,
                IntervalType = IntervalType.Minutes,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 5, 2, 30, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 5, 3, 0, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 5, 3, 30, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 5, 4, 0, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 6, 2, 30, 0)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 6, 3, 0, 0)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 6, 3, 30, 0)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 6, 4, 0, 0)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0)));
        });
    }
    
    [Test]
    [Category("Second")]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_StartingTime_Seconds()
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
                OccursEvery = 30,
                IntervalType = IntervalType.Seconds,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 5, 2, 0, 30)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 5, 2, 1, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 5, 2, 1, 30)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 5, 2, 2, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 5, 2, 2, 30)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 5, 2, 3, 0)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 5, 2, 3, 30)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 5, 2, 4, 0)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 5, 2, 4, 30)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 5, 2, 5, 0)));
        });
    }
    
    [Test]
    [Category("Second-NoEndDate")]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_StartingTime_Seconds_NoEndDate()
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
                OccursEvery = 30,
                IntervalType = IntervalType.Seconds,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 5, 2, 0, 30)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 5, 2, 1, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 5, 2, 1, 30)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 5, 2, 2, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 5, 2, 2, 30)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 5, 2, 3, 0)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 5, 2, 3, 30)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 5, 2, 4, 0)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 5, 2, 4, 30)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 5, 2, 5, 0)));
        });
    }
    
    [Test]
    [Category("Hour")]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_EndingTime_Hours()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 4, 0, 0),
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
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 6, 3, 0, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 6, 4, 0, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 7, 3, 0, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 7, 4, 0, 0)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 8, 3, 0, 0)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 8, 4, 0, 0)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 9, 2, 0, 0)));
        });
    }
    
    [Test]
    [Category("Hour-NoEndDate")]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_EndingTime_Hours_NoEndDate()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
            },
            CurrentDate = new DateTime(2020, 5, 5, 4, 0, 0),
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
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 6, 3, 0, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 6, 4, 0, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 7, 3, 0, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 7, 4, 0, 0)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 8, 3, 0, 0)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 8, 4, 0, 0)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 9, 2, 0, 0)));
            
        });
    }
    
     [Test]
     [Category("Minute")]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_EndingTime_Minutes()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 4, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Daily
            },
            DailyFrequency = new DailyFrequency
            {
                OccursEvery = 30,
                IntervalType = IntervalType.Minutes,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 6, 2, 30, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 6, 3, 0, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 6, 3, 30, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 6, 4, 0, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 7, 2, 30, 0)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 7, 3, 0, 0)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 7, 3, 30, 0)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 7, 4, 0, 0)));
        });
    }
    
    [Test]
    [Category("Minute-NoEndDate")]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_EndingTime_Minutes_NoEndDate()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
            },
            CurrentDate = new DateTime(2020, 5, 5, 4, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Daily
            },
            DailyFrequency = new DailyFrequency
            {
                OccursEvery = 30,
                IntervalType = IntervalType.Minutes,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 6, 2, 30, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 6, 3, 0, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 6, 3, 30, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 6, 4, 0, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 7, 2, 30, 0)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 7, 3, 0, 0)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 7, 3, 30, 0)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 7, 4, 0, 0)));
        });
    }
    
    [Test]
    [Category("Second")]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_EndingTime_Seconds()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 4, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Daily
            },
            DailyFrequency = new DailyFrequency
            {
                OccursEvery = 30,
                IntervalType = IntervalType.Seconds,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 30)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 6, 2, 1, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 6, 2, 1, 30)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 6, 2, 2, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 6, 2, 2, 30)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 6, 2, 3, 0)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 6, 2, 3, 30)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 6, 2, 4, 0)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 6, 2, 4, 30)));
        });
    }
    
    [Test]
    [Category("Second-NoEndDate")]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_EndingTime_Seconds_NoEndDate()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
            },
            CurrentDate = new DateTime(2020, 5, 5, 4, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Daily
            },
            DailyFrequency = new DailyFrequency
            {
                OccursEvery = 30,
                IntervalType = IntervalType.Seconds,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 30)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 6, 2, 1, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 6, 2, 1, 30)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 6, 2, 2, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 6, 2, 2, 30)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 6, 2, 3, 0)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 6, 2, 3, 30)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 6, 2, 4, 0)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 6, 2, 4, 30)));
        });
    }
    
    [Test]
    [Category("Hour")]
    public void SeriesDailyRecurring_CurrentDateTime_AfterEndingTime_Hours()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 5, 0, 0),
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
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 6, 3, 0, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 6, 4, 0, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 7, 3, 0, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 7, 4, 0, 0)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 8, 3, 0, 0)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 8, 4, 0, 0)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 9, 2, 0, 0)));
        });
    }
    
    [Test]
    [Category("Hour-NoEndDate")]
    public void SeriesDailyRecurring_CurrentDateTime_AfterEndingTime_Hours_NoEndDate()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
            },
            CurrentDate = new DateTime(2020, 5, 5, 5, 0, 0),
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
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 6, 3, 0, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 6, 4, 0, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 7, 3, 0, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 7, 4, 0, 0)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 8, 3, 0, 0)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 8, 4, 0, 0)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 9, 2, 0, 0)));
            
        });
    }
    
    [Test]
    [Category("Minute")]
    public void SeriesDailyRecurring_CurrentDateTime_AfterEndingTime_Minutes()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 5, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Daily
            },
            DailyFrequency = new DailyFrequency
            {
                OccursEvery = 30,
                IntervalType = IntervalType.Minutes,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 6, 2, 30, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 6, 3, 0, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 6, 3, 30, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 6, 4, 0, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 7, 2, 30, 0)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 7, 3, 0, 0)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 7, 3, 30, 0)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 7, 4, 0, 0)));
        });
    }
    
    [Test]
    [Category("Minute-NoEndDate")]
    public void SeriesDailyRecurring_CurrentDateTime_AfterEndingTime_Minutes_NoEndDate()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
            },
            CurrentDate = new DateTime(2020, 5, 5, 5, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Daily
            },
            DailyFrequency = new DailyFrequency
            {
                OccursEvery = 30,
                IntervalType = IntervalType.Minutes,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 6, 2, 30, 0)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 6, 3, 0, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 6, 3, 30, 0)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 6, 4, 0, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 7, 2, 30, 0)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 7, 3, 0, 0)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 7, 3, 30, 0)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 7, 4, 0, 0)));
        });
    }
    
    [Test]
    [Category("Second")]
    public void SeriesDailyRecurring_CurrentDateTime_AfterEndingTime_Seconds()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 5, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Daily
            },
            DailyFrequency = new DailyFrequency
            {
                OccursEvery = 30,
                IntervalType = IntervalType.Seconds,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 30)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 6, 2, 1, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 6, 2, 1, 30)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 6, 2, 2, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 6, 2, 2, 30)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 6, 2, 3, 0)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 6, 2, 3, 30)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 6, 2, 4, 0)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 6, 2, 4, 30)));
        });
    }
    
    [Test]
    [Category("Second-NoEndDate")]
    public void SeriesDailyRecurring_CurrentDateTime_AfterEndingTime_Seconds_NoEndDate()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
            },
            CurrentDate = new DateTime(2020, 5, 5, 5, 0, 0),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Daily
            },
            DailyFrequency = new DailyFrequency
            {
                OccursEvery = 30,
                IntervalType = IntervalType.Seconds,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        var details = Scheduler.DailyScheduleSeries(configuration, 10);
        Assert.That(details, Has.Count.EqualTo(10));
        Assert.That(details, Is.Ordered.Ascending);
        Assert.Multiple(() =>
        {
            Assert.That(details[0], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0)));
            Assert.That(details[1], Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 30)));
            Assert.That(details[2], Is.EqualTo(new DateTime(2020, 5, 6, 2, 1, 0)));
            Assert.That(details[3], Is.EqualTo(new DateTime(2020, 5, 6, 2, 1, 30)));
            Assert.That(details[4], Is.EqualTo(new DateTime(2020, 5, 6, 2, 2, 0)));
            Assert.That(details[5], Is.EqualTo(new DateTime(2020, 5, 6, 2, 2, 30)));
            Assert.That(details[6], Is.EqualTo(new DateTime(2020, 5, 6, 2, 3, 0)));
            Assert.That(details[7], Is.EqualTo(new DateTime(2020, 5, 6, 2, 3, 30)));
            Assert.That(details[8], Is.EqualTo(new DateTime(2020, 5, 6, 2, 4, 0)));
            Assert.That(details[9], Is.EqualTo(new DateTime(2020, 5, 6, 2, 4, 30)));
        });
    }
    
    
    [Test]
    [Category("Disabled")]
    public void CalculateDetails_RecurringDailyScheduleDisabled_OccurRepetitive_BeforeStartTime_Hours()
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
                OccursEvery = 1,
                IntervalType = IntervalType.Hours,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            },
            WeeklyConfiguration = null
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
    public void CalculateDetails_RecurringDailyScheduleEnabled_OccurRepetitive_InValidCount()
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
                OccursEvery = 0,
                IntervalType = IntervalType.Hours,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            },
        };
        
        Assert.That(() => Scheduler.CalculateDetails(configuration), 
            Throws.ArgumentException.With.Message.EqualTo("Count can't be non positive"));
    }
    
    [Test]
    [Category("Invalid-parameters")]
    public void CalculateDetails_RecurringDailyScheduleEnabled_OccurRepetitive_InValidIntervalBounds()
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
                OccursEvery = 1,
                IntervalType = IntervalType.Hours,
                StartingTime = new TimeSpan(5, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            }
        };
        
        Assert.That(() => Scheduler.CalculateDetails(configuration), 
            Throws.ArgumentException.With.Message.EqualTo("Invalid Interval bounds"));
    }
    
    [Test]
    [Category("Disabled")]
    public void CalculateDetails_RecurringDailyScheduleDisabled_OccurRepetitive_AtStartTime_Hours()
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
                OccursEvery = 1,
                IntervalType = IntervalType.Hours,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            },
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
    public void CalculateDetails_RecurringDailyScheduleDisabled_OccurRepetitive_InInterval_Hours()
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
                OccursEvery = 1,
                IntervalType = IntervalType.Hours,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            },
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
    public void CalculateDetails_RecurringDailyScheduleDisabled_OccurRepetitive_AtEndTime_Hours()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 10, 1)
            },
            CurrentDate = new DateTime(2020, 5, 5, 4, 0, 0),
            Configuration = new Configuration
            {
                Enabled = false,
                ScheduleType = ScheduleType.Recurring,
                Occurs = Occurs.Daily
            },
            DailyFrequency = new DailyFrequency
            {
                OccursEvery = 1,
                IntervalType = IntervalType.Hours,
                StartingTime = new TimeSpan(2, 0, 0),
                EndingTime = new TimeSpan(4, 0, 0),
            },
        };

        var details = Scheduler.CalculateDetails(configuration);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(DateTime.MinValue));
            Assert.That(details.Description, Is.EqualTo("Schedule was canceled"));
        });
    }
    
}