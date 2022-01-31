using FluentValidation;

namespace Application.GeoDistance.Calculate;

public class CalculateGeoDistanceRequestValidator : AbstractValidator<CalculateGeoDistanceRequest>
{
    public CalculateGeoDistanceRequestValidator()
    {
        RuleFor(x => x.Unit).IsInEnum();
        RuleFor(x => x.Method).IsInEnum();
        
        RuleFor(x => x.InitialLatitude).InclusiveBetween(-90, 90);
        RuleFor(x => x.InitialLongitude).InclusiveBetween(-180, 180);
        
        RuleFor(x => x.TargetLatitude).InclusiveBetween(-90, 90);
        RuleFor(x => x.TargetLongitude).InclusiveBetween(-180, 180);
    }
}