using System;
using StoreManager.Models;
using StoreManager.Models.Dto;

namespace StoreManager.Test;

[Trait("Category", "Models")]
public class ModelsTest
{
    [Fact(DisplayName = "Product Model")]
    public void ProductModel()
    {
        var product = new Product
        {
            Id = 1,
            Name = "Test"
        };

        product.Id.Should().Be(1);
        product.Name.Should().Be("Test");
    }

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


    [Fact(DisplayName = "SaleProduct Model")]
    public void SaleProductModel()
    {
        var saleProduct = new SaleProduct
        {
            ProductId = 1,
            SaleId = 1,
            Quantity = 1,
            Product = new Product { Id = 1, Name = "Test" },
            Sale = new Sale { Id = 1, Date = default }
        };

        saleProduct.ProductId.Should().Be(1);
        saleProduct.SaleId.Should().Be(1);
        saleProduct.Quantity.Should().Be(1);
        saleProduct.Product.Id.Should().Be(1);
        saleProduct.Product.Name.Should().Be("Test");
        saleProduct.Sale.Id.Should().Be(1);
        saleProduct.Sale.Date.Should().BeCloseTo(default, new TimeSpan(0, 0, 1));
    }

    [Fact(DisplayName = "ErrorMessage Model")]
    public void ErrorMessageModel()
    {
        var errorMessage = new ErrorMessage("Test");

        errorMessage.Message.Should().Be("Test");
    }

    [Fact(DisplayName = "SaleDto Model")]
    public void SaleDtoModel()
    {
        var saleDto = new SaleDto
        {
            SaleId = 1,
            Date = DateTime.Now,
            ProductId = 1,
            Quantity = 1
        };

        saleDto.SaleId.Should().Be(1);
        saleDto.Date.Should().BeCloseTo(DateTime.Now, new TimeSpan(0, 0, 1));
        saleDto.ProductId.Should().Be(1);
        saleDto.Quantity.Should().Be(1);
    }
}