using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StoreManager.Controllers;
using StoreManager.Models;
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
}