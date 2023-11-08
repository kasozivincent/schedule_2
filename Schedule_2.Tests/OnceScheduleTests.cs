using schedule_2;

namespace Schedule_2.Tests;

[TestFixture]
public class OnceScheduleTests
{
    [Test]
    public void CalculateDetails_OnceScheduleEnabled_ReturnsValidDetails()
    {
        var schedule = new ScheduleConfiguration
        {
            CurrentDate = new DateTime(2023, 10, 1),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Once,
                ExecutionDate = new DateTime(2023, 10, 5)
            },
            DailyFrequency = null,
            WeeklyConfiguration = null,
            Limits = null,
        };

        var details = Scheduler.CalculateDetails(schedule);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(new DateTime(2023, 10, 5)));
            Assert.That(details.Description, Is.EqualTo("Next schedule will execute on 10/5/2023 12:00:00 AM"));
        });
    }
    
    [Test]
    public void CalculateDetails_OnceScheduleDisabled_ReturnsCancellationDetails()
    {
        var schedule = new ScheduleConfiguration
        {
            CurrentDate = new DateTime(2023, 10, 1),
            Configuration = new Configuration
            {
                Enabled = false,
                ScheduleType = ScheduleType.Once,
                ExecutionDate = new DateTime(2023, 10, 5)
            },
            DailyFrequency = null,
            WeeklyConfiguration = null,
            Limits = null,
        };

        var details = Scheduler.CalculateDetails(schedule);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(DateTime.MinValue));
            Assert.That(details.Description, Is.EqualTo("Schedule was canceled"));
        });
    }
    
    [Test]
    public void CalculateDetails_OnceScheduleExpired_ReturnsErrorDetails()
    {
        var schedule = new ScheduleConfiguration
        {
            CurrentDate = new DateTime(2023, 10, 1),
            Configuration = new Configuration
            {
                Enabled = true,
                ScheduleType = ScheduleType.Once,
                ExecutionDate = new DateTime(2020, 10, 5)
            },
            DailyFrequency = null,
            WeeklyConfiguration = null,
            Limits = null,
        };

        var details = Scheduler.CalculateDetails(schedule);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(DateTime.MinValue));
            Assert.That(details.Description, Is.EqualTo("Schedule can't be executed"));
        });
    }
    
    [Test]
    public void CalculateDetails_OnceScheduleDisabled_Expired_ReturnsErrorDetails()
    {
        var schedule = new ScheduleConfiguration
        {
            CurrentDate = new DateTime(2023, 10, 1),
            Configuration = new Configuration
            {
                Enabled = false,
                ScheduleType = ScheduleType.Once,
                ExecutionDate = new DateTime(2020, 10, 5)
            },
            DailyFrequency = null,
            WeeklyConfiguration = null,
            Limits = null,
        };

        var details = Scheduler.CalculateDetails(schedule);
        Assert.Multiple(() =>
        {
            Assert.That(details.NextDate, Is.EqualTo(DateTime.MinValue));
            Assert.That(details.Description, Is.EqualTo("Schedule was canceled"));
        });
    }
}