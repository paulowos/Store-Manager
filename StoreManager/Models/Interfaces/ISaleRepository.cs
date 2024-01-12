using StoreManager.Models.Dto;

namespace StoreManager.Models.Interfaces;

public interface ISaleRepository
{
    public IEnumerable<SaleDto>? GetById(int id);
    public IEnumerable<SaleDto> GetAll();
}