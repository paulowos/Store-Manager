namespace StoreManager.Model;

public class SaleProduct
{
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int SaleId { get; set; }
    public Sale Sale { get; set; } = null!;
    public int Quantity { get; set; }
}