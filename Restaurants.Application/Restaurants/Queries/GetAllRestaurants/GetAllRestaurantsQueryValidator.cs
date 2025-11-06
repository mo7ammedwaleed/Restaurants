using FluentValidation;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private int[] allowedMaxPageSize = [5,10,15,30];
    private string[] allowedSortByColumnNames = ["Name", "Description", "Category"];
    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .Must(pageSize => allowedMaxPageSize.Contains(pageSize))
            .WithMessage($"PageSize must be in  {string.Join(", ", allowedMaxPageSize)}");

        RuleFor(x => x.SortBy)
            .Must(value => allowedSortByColumnNames.Contains(value))
            .When(x => x.SortBy != null)
            .WithMessage($"Sort by is optinal, or must be in  {string.Join(", ", allowedSortByColumnNames)}");
    }

}
