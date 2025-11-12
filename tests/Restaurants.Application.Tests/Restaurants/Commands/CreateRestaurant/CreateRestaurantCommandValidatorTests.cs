using Xunit;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            // arrange
            var command = new CreateRestaurantCommand
            {
                Name = "Valid Restaurant",
                Description = "A valid description for the restaurant.",
                Category = "Italian",
                ContactEmail = "test@test.com",
                PostalCode = "12-345",
            };

            var validator = new CreateRestaurantCommandValidator();

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validator_ForNotValidCommand_ShouldHaveValidationErrors()
        {
            // arrange
            var command = new CreateRestaurantCommand
            {
                Name = "Va",
                Description = "",
                Category = "Ita",
                ContactEmail = "@test.com",
                PostalCode = "12345",
            };

            var validator = new CreateRestaurantCommandValidator();

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(e => e.Name);
            result.ShouldHaveValidationErrorFor(e => e.Description);
            result.ShouldHaveValidationErrorFor(e => e.Category);
            result.ShouldHaveValidationErrorFor(e => e.ContactEmail);
            result.ShouldHaveValidationErrorFor(e => e.PostalCode);
        }

        [Theory()]
        [InlineData("Italian")]
        [InlineData("Indian")]
        [InlineData("Mexican")]
        [InlineData("American")]
        [InlineData("Japanese")]
        public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
        {
            // arrange

            var command = new CreateRestaurantCommand { Category = category };
            var validator = new CreateRestaurantCommandValidator();

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldNotHaveValidationErrorFor(e => e.Category);
        }

        [Theory()]
        [InlineData("10220")]
        [InlineData("102-20")]
        [InlineData("10 220")]
        [InlineData("10-2 20")]
        public void Validator_ForNotValidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty(string postalCode)
        {
            // arrange

            var command = new CreateRestaurantCommand { PostalCode = postalCode };
            var validator = new CreateRestaurantCommandValidator();

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(e => e.PostalCode);
        }
    }
}