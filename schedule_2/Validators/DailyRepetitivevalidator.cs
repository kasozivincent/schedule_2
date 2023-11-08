namespace schedule_2.Validators;

public static class DailyRepetitivevalidator
{
    public static void ValidateCount(DailyFrequency frequency)
    {
        if (frequency is null) 
            throw new ArgumentNullException(nameof(frequency));
        if (frequency.OccursEvery <= 0) 
            throw new ArgumentException("Count can't be non positive");
        if (frequency.StartingTime >= frequency.EndingTime) 
            throw new ArgumentException("Invalid Interval bounds");
    }
}