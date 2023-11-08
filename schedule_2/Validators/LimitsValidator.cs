namespace schedule_2.Validators;

public static class LimitsValidator
{
    public static bool ValidateLimits(Limits? limits)
    {
        if (limits == null)
            return false;
        if (limits.EndDate is null) 
            return true;
        return !(limits.StartDate >= limits.EndDate);
    }
}