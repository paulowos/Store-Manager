using System;
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
        var context = _databaseContext.GetContext(nameof(ProductRepositoryTest) + nameof(GetByIdUnsuccessful));
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
        var context = _databaseContext.GetContext(nameof(ProductRepositoryTest) + nameof(GetAllSuccessful));
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
        var context = _databaseContext.GetContext(nameof(ProductRepositoryTest) + nameof(GetAllEmpty));
        var repository = new ProductRepository(context);

        // Act
        var result = repository.GetAll();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact(DisplayName = "Create Product successful returns Product")]
    public void CreateSuccessful()
    {
        // Arrange
        var context = _databaseContext.GetContext(nameof(ProductRepositoryTest) + nameof(CreateSuccessful));
        var repository = new ProductRepository(context);

        // Act
        var result = repository.Add(ProductsMockData.GetProductDto(1));

        // Assert
        result.Should().BeEquivalentTo(ProductsMockData.GetProduct(1));
    }

    [Fact(DisplayName = "Update Product successful returns Product")]
    public void UpdateSuccessful()
    {
        // Arrange
        const int id = 1;
        var context = _databaseContext.GetContext(nameof(ProductRepositoryTest) + nameof(UpdateSuccessful));
        context.Products.Add(ProductsMockData.GetProduct(id));
        context.SaveChanges();
        var repository = new ProductRepository(context);

        // Act
        var result = repository.Update(id, ProductsMockData.GetProductDto(id));

        // Assert
        result.Should().BeEquivalentTo(ProductsMockData.GetProduct(id));
    }

    [Fact(DisplayName = "Update Product when Product does not exist throws Exception")]
    public void UpdateUnsuccessful()
    {
        // Arrange
        const int id = 1;
        var context = _databaseContext.GetContext(nameof(ProductRepositoryTest) + nameof(UpdateUnsuccessful));
        var repository = new ProductRepository(context);

        // Act
        Action act = () => repository.Update(id, ProductsMockData.GetProductDto(id));

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Product not found");
    }

    [Fact(DisplayName = "Delete Product successful")]
    public void DeleteSuccessful()
    {
        // Arrange
        const int id = 1;
        var context = _databaseContext.GetContext(nameof(ProductRepositoryTest) + nameof(DeleteSuccessful));
        context.Products.Add(ProductsMockData.GetProduct(id));
        context.SaveChanges();
        var repository = new ProductRepository(context);

        // Act
        repository.Delete(id);

        // Assert
        context.Products.Should().BeEmpty();
    }

    [Fact(DisplayName = "Delete Product when Product does not exist throws Exception")]
    public void DeleteUnsuccessful()
    {
        // Arrange
        const int id = 1;
        var context = _databaseContext.GetContext(nameof(ProductRepositoryTest) + nameof(DeleteUnsuccessful));
        var repository = new ProductRepository(context);

        // Act
        var act = () => repository.Delete(id);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Product not found");
    }
}