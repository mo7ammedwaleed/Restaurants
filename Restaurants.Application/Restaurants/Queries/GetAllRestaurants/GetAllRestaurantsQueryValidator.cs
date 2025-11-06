using FluentValidation;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private int[] allowedMaxPageSize = [5,10,15,30];
    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .Must(pageSize => allowedMaxPageSize.Contains(pageSize))
            .WithMessage($"PageSize must be in  {string.Join(", ", allowedMaxPageSize)}");


    }

}
