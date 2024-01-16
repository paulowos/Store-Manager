using StoreManager.Context;
using StoreManager.Models;
using StoreManager.Models.Dto;
using StoreManager.Models.Interfaces;

namespace StoreManager.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly StoreManagerContext _context;

    public ProductRepository(StoreManagerContext context)
    {
        _context = context;
    }

    public Product? GetById(int id)
    {
        var product = _context.Products.Find(id);
        return product;
    }

    public IEnumerable<Product> GetAll()
    {
        var products = _context.Products.OrderBy(p => p.Id).ToList();
        return products;
    }

    public Product Add(ProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name
        };
        _context.Products.Add(product);
        _context.SaveChanges();
        return product;
    }

    public Product Update(int id, ProductDto productDto)
    {
        var product = _context.Products.Find(id);
        if (product == null) throw new ArgumentException("Product not found");
        product.Name = productDto.Name;
        _context.SaveChanges();
        return product;
    }

    public void Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null) throw new ArgumentException("Product not found");
        _context.Products.Remove(product);
        _context.SaveChanges();
    }
}