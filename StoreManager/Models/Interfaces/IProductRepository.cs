using StoreManager.Models.Dto;

namespace StoreManager.Models.Interfaces;

public interface IProductRepository
{
    public Product? GetById(int id);
    public IEnumerable<Product> GetAll();
    public Product Add(ProductDto productDto);
}