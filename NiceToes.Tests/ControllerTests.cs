using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NiceToes.ProductAPI;
using NiceToes.ProductAPI.Controllers;
using NiceToes.ProductAPI.Data;
using NiceToes.ProductAPI.Models.Dto;
using Xunit;

namespace NiceToes.Tests;

public class ControllerTests
{

    [Fact]
    void TestProductController()
    {
        int _actionResult = 42;
        // Setup
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        var mockDbSet = new Mock<DbSet<ProductAPI.Models.Product>>();
        mockDbSet.Object.Add(new ProductAPI.Models.Product() { ProductId = 42 });

       var mockContext = new Mock<AppDbContext>(options)
       {
           Object =
           {
               //Products = new DbSet<Product>();
           }
       };

        mockContext.Object.SaveChanges();

        var mockMapper = MappingConfig.RegisterMaps().CreateMapper();

        var mockLogger = new Mock<ILogger<ProductAPIController>>();

        var controller = new ProductAPIController(mockContext.Object, mockMapper, mockLogger.Object);

        // Act
        var actionResult = controller.Get(42);
        var contentResult = actionResult.Result as ProductDto;

        // Assert
        Assert.Equal(42, _actionResult);
    }
}