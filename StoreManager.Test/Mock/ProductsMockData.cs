using System.Collections.Generic;
using StoreManager.Models;
using StoreManager.Models.Dto;

namespace StoreManager.Test.Mock;

public static class ProductsMockData
{
    public static Product GetProduct(int id)
    {
        return new Product
        {
            Id = id,
            Name = $"Product {id}"
        };
    }

    public static IEnumerable<Product> GetProducts()
    {
        return new List<Product>
        {
            GetProduct(1),
            GetProduct(2)
        };
    }

    public static ProductDto GetProductDto(int id)
    {
        return new ProductDto
        {
            Name = $"Product {id}"
        };
    }
}