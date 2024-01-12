using Microsoft.EntityFrameworkCore;
using StoreManager.Context;
using StoreManager.Repositories;
using StoreManager.Test.Context;
using StoreManager.Test.Mock;

namespace StoreManager.Test.Repositories;

[Trait("Category", "Repositories")]
public class ProductRepositoryTest : IClassFixture<DatabaseContext>
{
    private readonly DatabaseContext _databaseContext;

    public ProductRepositoryTest(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    [Fact(DisplayName = "GetById when Product exists returns Product")]
    public void GetByIdSuccessful()
    {
        // Arrange
        var context = _databaseContext.GetContext(nameof(ProductRepositoryTest) + nameof(GetByIdSuccessful));
        context.Products.Add(ProductsMockData.GetProduct(1));
        context.SaveChanges();
        var repository = new ProductRepository(context);

        // Act
        var result = repository.GetById(1);

        // Assert
        result.Should().BeEquivalentTo(ProductsMockData.GetProduct(1));
    }

    [Fact(DisplayName = "GetById when Product does not exist returns null")]
    public void GetByIdUnsuccessful()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<StoreManagerContext>()
            .UseInMemoryDatabase(nameof(ProductRepositoryTest) + nameof(GetByIdUnsuccessful))
            .Options;
        var context = new StoreManagerContext(options);
        var repository = new ProductRepository(context);

        // Act
        var result = repository.GetById(1);

        // Assert
        result.Should().BeNull();
    }

    [Fact(DisplayName = "GetAll when Products exist returns Products")]
    public void GetAllSuccessful()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<StoreManagerContext>()
            .UseInMemoryDatabase(nameof(ProductRepositoryTest) + nameof(GetAllSuccessful))
            .Options;
        var context = new StoreManagerContext(options);
        context.Products.Add(ProductsMockData.GetProduct(1));
        context.Products.Add(ProductsMockData.GetProduct(2));
        context.SaveChanges();
        var repository = new ProductRepository(context);

        // Act
        var result = repository.GetAll();

        // Assert
        result.Should().BeEquivalentTo(ProductsMockData.GetProducts());
    }

    [Fact(DisplayName = "GetAll when no Products exist returns empty list")]
    public void GetAllEmpty()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<StoreManagerContext>()
            .UseInMemoryDatabase(nameof(ProductRepositoryTest) + nameof(GetAllEmpty))
            .Options;
        var context = new StoreManagerContext(options);
        var repository = new ProductRepository(context);

        // Act
        var result = repository.GetAll();

        // Assert
        result.Should().BeEmpty();
    }
}