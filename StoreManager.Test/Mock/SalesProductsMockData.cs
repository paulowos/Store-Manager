using System.Collections.Generic;
using StoreManager.Models;
using StoreManager.Models.Dto;

namespace StoreManager.Test.Mock;

public static class SalesProductsMockData
{
    public static SaleProduct GetSaleProduct(int id)
    {
        return new SaleProduct
        {
            ProductId = id,
            SaleId = id,
            Quantity = id * 2
        };
    }

    public static IEnumerable<SaleProductDto> GetSalesProductsDto(int id)
    {
        return new List<SaleProductDto>
        {
            new()
            {
                SaleId = id,
                Date = default,
                ProductId = id,
                Quantity = id * 2
            }
        };
    }
}