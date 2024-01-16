using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StoreManager.Controllers;
using StoreManager.Models;
using StoreManager.Models.Dto;
using StoreManager.Models.Interfaces;
using StoreManager.Test.Mock;

namespace StoreManager.Test.Controllers;

[Trait("Category", "Controllers")]
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

    [Fact(DisplayName = "Add returns CreatedAtRoute")]
    public void AddSuccessful()
    {
        // Arrange
        var saleInputDto = SalesMockData.GetSaleInputDto(1, 1);
        var saleInputDtos = saleInputDto as SaleInputDto[] ?? saleInputDto.ToArray();
        var saleOutputDto = SalesMockData.GetSaleOutputDto(1, saleInputDtos);
        var mockRepo = new Mock<ISaleRepository>();
        mockRepo.Setup(repo => repo.Add(It.IsAny<IEnumerable<SaleInputDto>>()))
            .Returns(saleOutputDto);
        var controller = new SaleController(mockRepo.Object);

        // Act
        var result = controller.Add(SalesMockData.GetSaleInputDto(1, 1));
        var response = result.Result.As<CreatedAtActionResult>();
        var value = response.Value.As<SaleOutputDto>();

        // Assert
        response.StatusCode.Should().Be((int)HttpStatusCode.Created);
        response.ActionName.Should().Be(nameof(controller.GetById));
        response.RouteValues.Should().BeEquivalentTo(new Dictionary<string, int> { { "id", 1 } });
        value.Id.Should().Be(saleOutputDto.Id);
        value.Date.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        value.ItemsSold.Should().BeEquivalentTo(saleInputDtos);
    }

    [Fact(DisplayName = "Add when Products do not exist returns BadRequest")]
    public void AddProductsNotFound()
    {
        // Arrange
        var mockRepo = new Mock<ISaleRepository>();
        mockRepo.Setup(repo => repo.Add(It.IsAny<IEnumerable<SaleInputDto>>()))
            .Throws<ArgumentException>(() => throw new ArgumentException("Product with id 1 does not exist"));
        var controller = new SaleController(mockRepo.Object);

        // Act
        var result = controller.Add(SalesMockData.GetSaleInputDto(1, 1));
        var response = result.Result.As<BadRequestObjectResult>();

        // Assert
        response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        response.Value.Should().BeEquivalentTo(new ErrorMessage("Product with id 1 does not exist"));
    }

    [Fact(DisplayName = "Delete returns NoContent")]
    public void DeleteSuccessful()
    {
        // Arrange
        var mockRepo = new Mock<ISaleRepository>();
        mockRepo.Setup(repo => repo.Delete(It.IsAny<int>()));
        var controller = new SaleController(mockRepo.Object);

        // Act
        var result = controller.Delete(1);
        var response = result.As<NoContentResult>();

        // Assert
        response.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
    }

    [Fact(DisplayName = "Delete when Sale does not exist returns BadRequest")]
    public void DeleteNotFound()
    {
        // Arrange
        var mockRepo = new Mock<ISaleRepository>();
        mockRepo.Setup(repo => repo.Delete(It.IsAny<int>()))
            .Throws<ArgumentException>(() => throw new ArgumentException("Sale with id 1 does not exist"));
        var controller = new SaleController(mockRepo.Object);

        // Act
        var result = controller.Delete(1);
        var response = result.As<BadRequestObjectResult>();

        // Assert
        response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        response.Value.Should().BeEquivalentTo(new ErrorMessage("Sale with id 1 does not exist"));
    }
}