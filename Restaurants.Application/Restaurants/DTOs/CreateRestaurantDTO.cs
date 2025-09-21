using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace Restaurants.Application.Restaurants.DTOs;

public class CreateRestaurantDTO
{
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    [Required(ErrorMessage = "Enter a valid category")]
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }

    public string? City { get; set; }
    public string? Street { get; set; }
    [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Postal code must be in the format (XX-XXX)")]
    public string? PostalCode { get; set; }

    [EmailAddress(ErrorMessage = "Please provide a valid email address")]
    public string? ContactEmail { get; set; }
    [Phone(ErrorMessage = "Please provide a valid phone address")]
    public string? ContactNumber { get; set; }
}
