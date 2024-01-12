namespace StoreManager.Models.Dto;

public class SaleDto
{
    public int SaleId { get; set; }
    public DateTime Date { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}