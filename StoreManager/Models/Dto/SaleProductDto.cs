namespace StoreManager.Models.Dto;

public class SaleProductDto
{
    public int SaleId { get; init; }
    public DateTime Date { get; init; }
    public int ProductId { get; init; }
    public int Quantity { get; init; }
}