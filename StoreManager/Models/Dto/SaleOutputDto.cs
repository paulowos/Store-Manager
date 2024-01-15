namespace StoreManager.Models.Dto;

public class SaleOutputDto
{
    public int Id { get; init; }
    public DateTime Date { get; init; }
    public IEnumerable<SaleInputDto> ItemsSold { get; init; } = null!;
}