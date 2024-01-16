using System;
using StoreManager.Models;
using StoreManager.Models.Dto;

namespace StoreManager.Test.Models;

[Trait("Category", "Models")]
public class SaleTest
{
    [Fact(DisplayName = "Sale Model")]
    public void SaleModel()
    {
        var sale = new Sale
        {
            Id = 1,
            Date = DateTime.Now
        };

        sale.Id.Should().Be(1);
        sale.Date.Should().BeCloseTo(DateTime.Now, new TimeSpan(0, 0, 1));
    }

    [Fact(DisplayName = "SaleInputDto Model")]
    public void SaleInputDtoModel()
    {
        var saleInputDto = new SaleInputDto
        {
            ProductId = 1,
            Quantity = 10
        };

        saleInputDto.ProductId.Should().Be(1);
        saleInputDto.Quantity.Should().Be(10);
    }

    [Fact(DisplayName = "SaleOutputDto Model")]
    public void SaleOutputDtoModel()
    {
        var itemsSold = new SaleInputDto[]
        {
            new()
            {
                ProductId = 1,
                Quantity = 10
            }
        };
        var saleOutputDto = new SaleOutputDto
        {
            Id = 1,
            Date = DateTime.Now,
            ItemsSold = itemsSold
        };

        saleOutputDto.Id.Should().Be(1);
        saleOutputDto.Date.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        saleOutputDto.ItemsSold.Should().BeEquivalentTo(itemsSold);
    }
}