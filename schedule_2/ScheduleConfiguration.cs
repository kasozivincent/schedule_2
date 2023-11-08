namespace schedule_2;

public class ScheduleConfiguration
{
    public DateTime CurrentDate { get; set; }
    public Configuration Configuration { get; set; }
    public DailyFrequency? DailyFrequency { get; set; }
    public WeeklyConfiguration? WeeklyConfiguration { get; set; }
    public Limits? Limits { get; set; } 
    
    public MonthlyConfiguration? MonthlyConfiguration { get; set; }
    
}