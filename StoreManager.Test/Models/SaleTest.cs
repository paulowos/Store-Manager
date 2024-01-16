using System;
using StoreManager.Models;
using StoreManager.Models.Dto;
using StoreManager.Test.Helpers;

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

    [Fact(DisplayName = "SaleInputDto Model - Empty ProductId")]
    public void SaleInputDtoEmptyProductId()
    {
        var saleInputDto = new SaleInputDto
        {
            ProductId = 0,
            Quantity = 10
        };

        saleInputDto.ProductId.Should().Be(0);
        saleInputDto.Quantity.Should().Be(10);
        var result = ValidateModel.Validate(saleInputDto);
        result.IsValid.Should().BeFalse();
        result.ValidationResults.Should()
            .Contain(x => x.ErrorMessage == "Product Id is required");
    }

    [Fact(DisplayName = "SaleInputDto Model - Empty Quantity")]
    public void SaleInputDtoEmptyQuantity()
    {
        var saleInputDto = new SaleInputDto
        {
            ProductId = 1,
            Quantity = 0
        };

        saleInputDto.ProductId.Should().Be(1);
        saleInputDto.Quantity.Should().Be(0);
        var result = ValidateModel.Validate(saleInputDto);
        result.IsValid.Should().BeFalse();
        result.ValidationResults.Should()
            .Contain(x => x.ErrorMessage == "Quantity must be greater than 0");
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