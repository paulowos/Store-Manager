using StoreManager.Models;
using StoreManager.Models.Dto;
using StoreManager.Test.Helpers;

namespace StoreManager.Test;

[Trait("Category", "Models")]
public class ProductTest
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

    [Fact(DisplayName = "ProductDto Model")]
    public void ProductDto()
    {
        var productDto = new ProductDto
        {
            Name = "name test"
        };

        productDto.Name.Should().Be("name test");
        var result = ValidateModel.Validate(productDto);
        result.IsValid.Should().BeTrue();
        result.ValidationResults.Should().BeEmpty();
    }

    [Fact(DisplayName = "ProductDto Model - Empty Name")]
    public void ProductDtoEmptyName()
    {
        var productDto = new ProductDto
        {
            Name = ""
        };

        productDto.Name.Should().BeEmpty();
        var result = ValidateModel.Validate(productDto);
        result.IsValid.Should().BeFalse();
        result.ValidationResults.Should()
            .Contain(x => x.ErrorMessage == "Name is required");
    }

    [Fact(DisplayName = "ProductDto Model - Name Length")]
    public void ProductDtoNameLength()
    {
        var productDto = new ProductDto
        {
            Name = "1234"
        };

        productDto.Name.Should().HaveLength(4);
        var result = ValidateModel.Validate(productDto);
        result.IsValid.Should().BeFalse();
        result.ValidationResults.Should()
            .Contain(x => x.ErrorMessage == "Name length must be between 5 and 50 characters");
    }
}