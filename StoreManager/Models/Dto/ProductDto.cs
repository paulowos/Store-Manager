using System.ComponentModel.DataAnnotations;

namespace StoreManager.Models.Dto;

public class ProductDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Name length must be between 5 and 50 characters")]
    public string Name { get; init; } = null!;
}