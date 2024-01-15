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

public class SaleControllerTest
{
    [Fact(DisplayName = "GetAll returns Sales")]
    public void GetSalesSuccessful()
    {
        // Arrange
        var mockRepo = new Mock<ISaleRepository>();
        mockRepo.Setup(repo => repo.GetAll())
            .Returns(SalesProductsMockData.GetSalesProductsDto(1));
        var controller = new SaleController(mockRepo.Object);

        // Act
        var result = controller.GetAll();
        var response = result.Result.As<OkObjectResult>();

        // Assert
        response.StatusCode.Should().Be((int)HttpStatusCode.OK);
        response.Value.Should().BeEquivalentTo(SalesProductsMockData.GetSalesProductsDto(1));
    }

    [Fact(DisplayName = "GetAll when no Sales exist returns empty list")]
    public void GetSalesEmpty()
    {
        // Arrange
        var mockRepo = new Mock<ISaleRepository>();
        mockRepo.Setup(repo => repo.GetAll())
            .Returns(new List<SaleProductDto>());
        var controller = new SaleController(mockRepo.Object);

        // Act
        var result = controller.GetAll();
        var response = result.Result.As<OkObjectResult>();

        // Assert
        response.StatusCode.Should().Be((int)HttpStatusCode.OK);
        response.Value.Should().BeEquivalentTo(Array.Empty<SaleProductDto>());
    }

    [Theory(DisplayName = "GetById when Sale exists returns Sale")]
    [InlineData(1)]
    [InlineData(2)]
    public void GetSaleByIdSuccessful(int id)
    {
        // Arrange
        var mockRepo = new Mock<ISaleRepository>();
        mockRepo.Setup(repo => repo.GetById(It.Is<int>(i => i == id)))
            .Returns(SalesProductsMockData.GetSalesProductsDto(id));
        var controller = new SaleController(mockRepo.Object);

        // Act
        var result = controller.GetById(id);
        var response = result.Result.As<OkObjectResult>();

        // Assert
        response.StatusCode.Should().Be((int)HttpStatusCode.OK);
        response.Value.Should().BeEquivalentTo(SalesProductsMockData.GetSalesProductsDto(id));
    }

    [Fact(DisplayName = "GetById when Sale does not exist returns NotFound")]
    public void GetSaleByIdNotFound()
    {
        // Arrange
        var mockRepo = new Mock<ISaleRepository>();
        mockRepo.Setup(repo => repo.GetById(It.IsAny<int>()))
            .Returns((IEnumerable<SaleProductDto>?)null);
        var controller = new SaleController(mockRepo.Object);

        // Act
        var result = controller.GetById(1);
        var response = result.Result.As<NotFoundObjectResult>();

        // Assert
        response.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        response.Value.Should().BeEquivalentTo(new ErrorMessage("Sale not found"));
    }
}