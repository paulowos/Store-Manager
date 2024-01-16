using System;
using System.Collections.Generic;
using StoreManager.Models.Dto;
using StoreManager.Repositories;
using StoreManager.Test.Context;
using StoreManager.Test.Mock;

namespace StoreManager.Test.Repositories;

public class SaleRepositoryTest : IClassFixture<DatabaseContext>
{
    private readonly DatabaseContext _databaseContext;

    public SaleRepositoryTest(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    [Fact(DisplayName = "GetById when Sale exists returns SaleDto")]
    public void GetByIdSuccessful()
    {
        // Arrange
        const int id = 1;
        var context = _databaseContext.GetContext(nameof(SaleRepositoryTest) + nameof(GetByIdSuccessful));
        context.Sales.Add(SalesMockData.GetSale(id));
        context.Products.Add(ProductsMockData.GetProduct(id));
        context.SalesProducts.Add(SalesProductsMockData.GetSaleProduct(id));
        context.SaveChanges();
        var repository = new SaleRepository(context);

        // Act
        var result = repository.GetById(id);

        // Assert
        result.Should().BeEquivalentTo(SalesProductsMockData.GetSalesProductsDto(id));
    }

    [Fact(DisplayName = "GetById when Sale does not exist returns null")]
    public void GetByIdUnsuccessful()
    {
        // Arrange
        var context = _databaseContext.GetContext(nameof(SaleRepositoryTest) + nameof(GetByIdUnsuccessful));
        var repository = new SaleRepository(context);

        // Act
        var result = repository.GetById(1);

        // Assert
        result.Should().BeNull();
    }

    [Fact(DisplayName = "GetAll when Sales exist returns SaleDtos")]
    public void GetAllSuccessful()
    {
        // Arrange
        const int id = 1;
        var context = _databaseContext.GetContext(nameof(SaleRepositoryTest) + nameof(GetAllSuccessful));
        context.Sales.Add(SalesMockData.GetSale(id));
        context.Products.Add(ProductsMockData.GetProduct(id));
        context.SalesProducts.Add(SalesProductsMockData.GetSaleProduct(id));
        context.SaveChanges();
        var repository = new SaleRepository(context);

        // Act
        var result = repository.GetAll();

        // Assert
        result.Should().BeEquivalentTo(SalesProductsMockData.GetSalesProductsDto(id));
    }

    [Fact(DisplayName = "GetAll when Sales do not exist returns empty list")]
    public void GetAllUnsuccessful()
    {
        // Arrange
        var context = _databaseContext.GetContext(nameof(SaleRepositoryTest) + nameof(GetAllUnsuccessful));
        var repository = new SaleRepository(context);

        // Act
        var result = repository.GetAll();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact(DisplayName = "Add when SaleInputDtos are valid returns SaleOutputDto")]
    public void AddSuccessful()
    {
        // Arrange
        const int id = 1;
        var context = _databaseContext.GetContext(nameof(SaleRepositoryTest) + nameof(AddSuccessful));
        context.Products.Add(ProductsMockData.GetProduct(id));
        context.SaveChanges();
        var repository = new SaleRepository(context);
        var saleInputDtos = new List<SaleInputDto>
        {
            new()
            {
                ProductId = id,
                Quantity = 1
            }
        };

        // Act
        var result = repository.Add(saleInputDtos);

        // Assert
        var expected = SalesMockData.GetSaleOutputDto(id, saleInputDtos);
        result.Id.Should().Be(expected.Id);
        result.Date.Should().BeCloseTo(expected.Date, TimeSpan.FromSeconds(1));
        result.ItemsSold.Should().BeEquivalentTo(expected.ItemsSold);
    }

    [Fact(DisplayName = "Add when SaleInputDtos are invalid throws ArgumentException")]
    public void AddUnsuccessful()
    {
        // Arrange
        const int id = 1;
        var context = _databaseContext.GetContext(nameof(SaleRepositoryTest) + nameof(AddUnsuccessful));
        var repository = new SaleRepository(context);
        var saleInputDtos = new List<SaleInputDto>
        {
            new()
            {
                ProductId = id,
                Quantity = 10
            }
        };

        // Act
        Action act = () => repository.Add(saleInputDtos);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Product with id 1 does not exist");
    }

    [Fact(DisplayName = "Delete when Sale exists deletes Sale")]
    public void DeleteSuccessful()
    {
        // Arrange
        const int id = 1;
        var context = _databaseContext.GetContext(nameof(SaleRepositoryTest) + nameof(DeleteSuccessful));
        context.Sales.Add(SalesMockData.GetSale(id));
        context.Products.Add(ProductsMockData.GetProduct(id));
        context.SalesProducts.Add(SalesProductsMockData.GetSaleProduct(id));
        context.SaveChanges();
        var repository = new SaleRepository(context);

        // Act
        repository.Delete(id);

        // Assert
        context.Sales.Should().BeEmpty();
        context.SalesProducts.Should().BeEmpty();
    }

    [Fact(DisplayName = "Delete when Sale does not exist throws ArgumentException")]
    public void DeleteUnsuccessful()
    {
        // Arrange
        var context = _databaseContext.GetContext(nameof(SaleRepositoryTest) + nameof(DeleteUnsuccessful));
        var repository = new SaleRepository(context);

        // Act
        var act = () => repository.Delete(1);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Sale with id 1 does not exist");
    }
}