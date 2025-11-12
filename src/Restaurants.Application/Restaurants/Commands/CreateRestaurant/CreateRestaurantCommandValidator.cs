using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> validCategories = [ "Italian","Indian","Mexican","American","Japanese"];
    public CreateRestaurantCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100);

        RuleFor(dto => dto.Description)
            .NotEmpty()
            .WithMessage("Description is required.");

        RuleFor(dto => dto.Category)
            .Must(validCategories.Contains)
            .WithMessage($"Category must be one of the following: {string.Join(", ", validCategories)}");

        //RuleFor(dto => dto.Category)
        //    .Custom((value, context) =>
        //    {
        //        var isvalidCategory = validCategories.Contains(value);
        //        if (!isvalidCategory)
        //        {
        //            context.AddFailure("Category", $"Category must be one of the following: {string.Join(", ", validCategories)}");
        //        }
        //    });
        //RuleFor(dto => dto.Category)
        //    .NotEmpty()
        //    .WithMessage("Category is required.");

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress()
            .WithMessage("Please provide a valid email address");

        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Postal code must be in the format (XX-XXX)");

    }
}
