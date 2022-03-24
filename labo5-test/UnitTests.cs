using System.Collections.Generic;
using System.Threading.Tasks;
using labo5_sneakers.DTO;
using labo5_sneakers.Models;
using labo5_sneakers.Services;
using labo5_test.Helpers;
using Xunit;

namespace labo5_test;

public class UnitTests
{
    public ISneakerService _sneakerService;

    public UnitTests()
    {
        _sneakerService = Helper.CreateSneakerService();
    }

    [Fact]
    public async Task Should_Reduce_Stock_On_Order()
    {
        Sneaker sneaker = new()
        {
            SneakerId = "a51cffc3-55e4-4762-bee3-6a36e0456c8d",
            Name = "PUMA",
            Price = new decimal(38.45),
            Stock = 6,
            Brand = new Brand()
            {
                BrandId = "62334349301e056f50c7fcc0",
                Name = "SCOTT"
            },
            Occasions = new List<Occasion>
            {
                new()
                {
                    OccasionId = "62334349301e056f50c7fcc1",
                    Description = "Sports"
                },
                new()
                {
                    OccasionId = "62334349301e056f50c7fcc2",
                    Description = "Casual"
                }
            }
        };

        await _sneakerService.AddSneaker(sneaker);

        Order order = new Order()
        {
            Email = "test@test.be",
            NumberOfItems = 2,
            SneakerId = "a51cffc3-55e4-4762-bee3-6a36e0456c8d"
        };

        OrderResponse? orderResponse = await _sneakerService.AddOrder(order);

        Assert.NotNull(orderResponse);
        Assert.Equal("Order Success", orderResponse.Status);

        Sneaker? sneakerResponse = await _sneakerService.GetSneakerBySneakerId(sneaker.SneakerId);

        Assert.NotNull(sneakerResponse);
        Assert.Equal(4, sneakerResponse!.Stock);
    }

    [Fact]
    public async Task Should_Place_Order()
    {
        Order order = new Order()
        {
            Email = "test@test.be",
            NumberOfItems = 2,
            SneakerId = "0123-456"
        };

        OrderResponse? orderResponse = await _sneakerService.AddOrder(order);

        Assert.NotNull(orderResponse);
        Assert.Equal("Order Success", orderResponse.Status);
    }

    [Fact]
    public async Task Should_OutOfStock_Order()
    {
        Order order = new Order()
        {
            Email = "test@test.be",
            NumberOfItems = 2999,
            SneakerId = "0123-456"
        };

        OrderResponse? orderResponse = await _sneakerService.AddOrder(order);
        Assert.NotNull(orderResponse);
        Assert.Equal("Out of stock", orderResponse.Status);
    }

    [Fact]
    public async Task Should_404_Order()
    {
        Order order = new Order()
        {
            Email = "test@test.be",
            NumberOfItems = 2,
            SneakerId = "0123-4569999"
        };
        OrderResponse? orderResponse = await _sneakerService.AddOrder(order);
        Assert.Null(orderResponse);
    }

    [Fact]
    public Task Should_Fire_PumaNotification()
    {
        IStockNotificationService service = new StockNotificationService();

        Dictionary<string, bool> notifsFired = service.CheckStockNotificationRules(new Sneaker()
        {
            Brand = new Brand()
            {
                BrandId = "999",
                Name = "PUMA"
            },
            Name = "doesnt matter",
            Occasions = new List<Occasion>(),
            Price = 100,
            SneakerId = "0",
            Stock = 4
        });

        Assert.True(notifsFired["puma rule"]);
        return Task.CompletedTask;
    }

    [Fact]
    public Task Should_Fire_ConverseNotification()
    {
        IStockNotificationService service = new StockNotificationService();

        Dictionary<string, bool> notifsFired = service.CheckStockNotificationRules(new Sneaker()
        {
            Brand = new Brand()
            {
                BrandId = "999",
                Name = "CONVERSE"
            },
            Name = "doesnt matter",
            Occasions = new List<Occasion>(),
            Price = 100,
            SneakerId = "0",
            Stock = 4
        });

        Assert.True(notifsFired["converse rule"]);
        return Task.CompletedTask;
    }

    [Fact]
    public Task Should_Not_Fire_Notification()
    {
        IStockNotificationService service = new StockNotificationService();

        Dictionary<string, bool> notifsFired = service.CheckStockNotificationRules(new Sneaker()
        {
            Brand = new Brand()
            {
                BrandId = "999",
                Name = "dddd"
            },
            Name = "doesnt matter",
            Occasions = new List<Occasion>(),
            Price = 100,
            SneakerId = "0",
            Stock = 4
        });

        Assert.All(notifsFired.Values, b => Assert.False(b));
        return Task.CompletedTask;
    }
}