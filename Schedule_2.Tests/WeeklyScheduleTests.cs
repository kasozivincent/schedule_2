using schedule_2;

namespace Schedule_2.Tests;

[TestFixture]
public class WeeklyScheduleTests
{
    [Test]
    public void CalculateDetails_RecurringWeeklyScheduleEnabled_OccurOnce_InvalidLimits()
    {
    }
    
    
    [Test]
    public void CalculateDetails_RecurringWeeklyScheduleEnabled_OccurOnce_Before_Time()
    {
        var configuration = new ScheduleConfiguration
        {
            Limits = new Limits
            {
                StartDate = new DateTime(2023, 1, 1),
                EndDate = new DateTime(2023, 10, 1)
            },
            CurrentDate = new DateTime(2023, 11, 5, 2, 0, 0),
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
            WeeklyConfiguration = new WeeklyConfiguration
            {
                EveryAfterWeeks = 1,
                WeekDays = WeekDay.Monday | WeekDay.Friday
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
    public void CalculateDetails_RecurringWeeklyScheduleDisabled_OccurOnce_Before_Time()
    {
        
    }
    
    [Test]
    public void CalculateDetails_RecurringWeeklyScheduleEnabled_OccurOnce_Exact_Time()
    {
        
    }
    
    [Test]
    public void CalculateDetails_RecurringWeeklyScheduleDisabled_OccurOnce_Exact_Time()
    {
        
    }

    [Test]
    public void CalculateDetails_RecurringWeeklyScheduleEnabled_OccurOnce_After_Time()
    {
        
    }

    [Test]
    public void CalculateDetails_RecurringWeeklyScheduleDisabled_OccurOnce_After_Time()
    {
    }









}