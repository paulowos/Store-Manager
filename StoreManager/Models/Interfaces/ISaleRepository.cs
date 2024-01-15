using StoreManager.Models.Dto;

namespace StoreManager.Models.Interfaces;

public interface ISaleRepository
{
    public IEnumerable<SaleProductDto>? GetById(int id);
    public IEnumerable<SaleProductDto> GetAll();
    public SaleOutputDto Add(IEnumerable<SaleInputDto> products);
}