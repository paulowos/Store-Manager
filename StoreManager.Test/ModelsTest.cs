using System;
using StoreManager.Models;

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
            Quantity = 1
        };

        saleProduct.ProductId.Should().Be(1);
        saleProduct.SaleId.Should().Be(1);
        saleProduct.Quantity.Should().Be(1);
    }

    [Fact(DisplayName = "ErrorMessage Model")]
    public void ErrorMessageModel()
    {
        var errorMessage = new ErrorMessage("Test");

        errorMessage.Message.Should().Be("Test");
    }
}