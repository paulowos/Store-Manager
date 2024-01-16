using System;
using System.Collections.Generic;
using StoreManager.Models;
using StoreManager.Models.Dto;

namespace StoreManager.Test.Mock;

public static class SalesMockData
{
    public static Sale GetSale(int id)
    {
        return new Sale
        {
            Id = id,
            Date = default
        };
    }

    public static SaleOutputDto GetSaleOutputDto(int id, IEnumerable<SaleInputDto> products)
    {
        return new SaleOutputDto
        {
            Id = id,
            Date = DateTime.Now,
            ItemsSold = products
        };
    }

    public static IEnumerable<SaleInputDto> GetSaleInputDto(int id, int quantity)
    {
        return new List<SaleInputDto>
        {
            new()
            {
                ProductId = id,
                Quantity = quantity
            }
        };
    }
}