using System.ComponentModel.DataAnnotations;

namespace StoreManager.Models.Dto;

public class SaleInputDto
{
    [Required(ErrorMessage = "Product Id is required")]
    public int ProductId { get; init; }

    [Required(ErrorMessage = "Quantity is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int Quantity { get; init; }
}