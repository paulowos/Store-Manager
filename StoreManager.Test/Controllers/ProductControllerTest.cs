using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StoreManager.Controllers;
using StoreManager.Models;
using StoreManager.Models.Dto;
using StoreManager.Models.Interfaces;
using StoreManager.Test.Mock;

namespace StoreManager.Test.Controllers;

public class ProductControllerTest
{
    [Fact(DisplayName = "GetAll returns Products")]
    public void GetProductsSuccessful()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(repo => repo.GetAll())
            .Returns(ProductsMockData.GetProducts());
        var controller = new ProductController(mockRepo.Object);

        // Act
        var result = controller.GetAll();
        var response = result.Result.As<OkObjectResult>();

        // Assert
        response.StatusCode.Should().Be((int)HttpStatusCode.OK);
        response.Value.Should().BeEquivalentTo(ProductsMockData.GetProducts());
    }

    [Fact(DisplayName = "GetAll when no Products exist returns empty list")]
    public void GetProductsEmpty()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(repo => repo.GetAll())
            .Returns(new List<Product>());
        var controller = new ProductController(mockRepo.Object);

        // Act
        var result = controller.GetAll();
        var response = result.Result.As<OkObjectResult>();

        // Assert
        response.StatusCode.Should().Be((int)HttpStatusCode.OK);
        response.Value.Should().BeEquivalentTo(Array.Empty<Product>());
    }

    [Theory(DisplayName = "GetById when Product exists returns Product")]
    [InlineData(1)]
    [InlineData(2)]
    public void GetProductByIdSuccessful(int id)
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(repo => repo.GetById(It.Is<int>(i => i == id)))
            .Returns(ProductsMockData.GetProduct(id));
        var controller = new ProductController(mockRepo.Object);

        // Act
        var result = controller.GetById(id);
        var response = result.Result.As<OkObjectResult>();

        // Assert
        response.StatusCode.Should().Be((int)HttpStatusCode.OK);
        response.Value.Should().BeEquivalentTo(ProductsMockData.GetProduct(id));
    }

    [Fact(DisplayName = "GetById when Product does not exist returns NotFound")]
    public void GetProductByIdUnsuccessful()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(repo => repo.GetById(It.IsAny<int>()))
            .Returns((Product?)null);
        var controller = new ProductController(mockRepo.Object);

        // Act
        var result = controller.GetById(1);
        var response = result.Result.As<NotFoundObjectResult>();

        // Assert
        response.Should().BeOfType<NotFoundObjectResult>();
        response.Value.Should().BeEquivalentTo(new ErrorMessage("Product not found"));
    }

    [Fact(DisplayName = "Create when Product is valid returns CreatedAtRoute")]
    public void CreateProductSuccessful()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(repo => repo.Add(It.IsAny<ProductDto>()))
            .Returns(ProductsMockData.GetProduct(1));
        var controller = new ProductController(mockRepo.Object);

        // Act
        var result = controller.Add(ProductsMockData.GetProductDto(1));
        var response = result.Result.As<CreatedAtActionResult>();

        // Assert
        response.StatusCode.Should().Be((int)HttpStatusCode.Created);
        response.Value.Should().BeEquivalentTo(ProductsMockData.GetProduct(1));
    }

    [Fact(DisplayName = "Update when successful returns Ok")]
    public void UpdateProductSuccessful()
    {
        // Arrange
        const int id = 1;
        var mockRepo = new Mock<IProductRepository>();
        mockRepo
            .Setup(repo => repo
                .Update(It.IsAny<int>(), It.IsAny<ProductDto>()))
            .Returns(ProductsMockData.GetProduct(id));
        var controller = new ProductController(mockRepo.Object);

        // Act
        var result = controller.Update(id, ProductsMockData.GetProductDto(id));
        var response = result.Result.As<OkObjectResult>();

        // Assert
        response.StatusCode.Should().Be((int)HttpStatusCode.OK);
        response.Value.Should().BeEquivalentTo(ProductsMockData.GetProduct(id));
    }

    [Fact(DisplayName = "Update when Product does not exist returns NotFound")]
    public void UpdateProductUnsuccessful()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        mockRepo
            .Setup(repo => repo
                .Update(It.IsAny<int>(), It.IsAny<ProductDto>()))
            .Throws(() => new ArgumentException("Product not found"));
        var controller = new ProductController(mockRepo.Object);

        // Act
        var result = controller.Update(1, ProductsMockData.GetProductDto(1));
        var response = result.Result.As<NotFoundObjectResult>();

        // Assert
        response.Should().BeOfType<NotFoundObjectResult>();
        response.Value.Should().BeEquivalentTo(new ErrorMessage("Product not found"));
    }

    [Fact(DisplayName = "Delete when successful returns NoContent")]
    public void DeleteProductSuccessful()
    {
        // Arrange
        const int id = 1;
        var mockRepo = new Mock<IProductRepository>();
        mockRepo
            .Setup(repo => repo
                .Delete(It.IsAny<int>()));
        var controller = new ProductController(mockRepo.Object);

        // Act
        var result = controller.Delete(id);
        var response = result.As<NoContentResult>();

        // Assert
        response.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
    }

    [Fact(DisplayName = "Delete when Product does not exist returns NotFound")]
    public void DeleteProductUnsuccessful()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        mockRepo
            .Setup(repo => repo
                .Delete(It.IsAny<int>()))
            .Throws(() => new ArgumentException("Product not found"));
        var controller = new ProductController(mockRepo.Object);

        // Act
        var result = controller.Delete(1);
        var response = result.As<NotFoundObjectResult>();

        // Assert
        response.Should().BeOfType<NotFoundObjectResult>();
        response.Value.Should().BeEquivalentTo(new ErrorMessage("Product not found"));
    }

    [Fact(DisplayName = "Search when successful returns Products")]
    public void SearchProductSuccessful()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(repo => repo.Search(It.IsAny<string>()))
            .Returns(ProductsMockData.GetProducts());
        var controller = new ProductController(mockRepo.Object);

        // Act
        var result = controller.Search("Product");
        var response = result.Result.As<OkObjectResult>();

        // Assert
        response.StatusCode.Should().Be((int)HttpStatusCode.OK);
        response.Value.Should().BeEquivalentTo(ProductsMockData.GetProducts());
    }

    [Fact(DisplayName = "Search when no Products exist returns empty list")]
    public void SearchProductEmpty()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(repo => repo.Search(It.IsAny<string>()))
            .Returns(new List<Product>());
        var controller = new ProductController(mockRepo.Object);

        // Act
        var result = controller.Search("Product");
        var response = result.Result.As<OkObjectResult>();

        // Assert
        response.StatusCode.Should().Be((int)HttpStatusCode.OK);
        response.Value.Should().BeEquivalentTo(Array.Empty<Product>());
    }
}