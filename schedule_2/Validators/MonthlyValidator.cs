namespace schedule_2.Validators;

public static class MonthlyValidator
{
    public static void ValidateMonthConfiguration(MonthlyConfiguration? configuration)
    {
        if (configuration is null) 
            throw new ArgumentNullException(nameof(configuration));
        if (configuration.EveryAfterMonths <= 0) 
            throw new ArgumentException("Count can't be non positive");
        if (configuration.MonthDay is < 1 or > 31) 
            throw new ArgumentException("Invalid Date");
    }
}