namespace schedule_2;

public class Configuration
{
    public bool Enabled { get; set; }
    public ScheduleType ScheduleType { get; set; }
    public DateTime ExecutionDate { get; set; }
    public Occurs Occurs { get; set; }
}