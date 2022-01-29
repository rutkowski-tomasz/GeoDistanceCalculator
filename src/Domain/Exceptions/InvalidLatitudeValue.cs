namespace Domain.Exceptions;

public class InvalidLatitudeValue : Exception
{
    public InvalidLatitudeValue(double value)
        : base($"Invalid value for latitude given \"{value}\", must be between -90 and 90")
    {
    }
}