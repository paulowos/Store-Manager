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
}