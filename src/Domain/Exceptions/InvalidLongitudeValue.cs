namespace Domain.Exceptions;

public class InvalidLongitudeValue : Exception
{
    public InvalidLongitudeValue(double value)
        : base($"Invalid value for longitude given \"{value}\", must be between -180 and 180")
    {
    }
}