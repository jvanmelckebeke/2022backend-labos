using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
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
    public async Task Should_Place_Order()
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

        Order orderResponse = await _sneakerService.AddOrder(order);

        Assert.NotNull(orderResponse);

        Sneaker sneakerResponse = await _sneakerService.GetSneakerBySneakerId(sneaker.SneakerId);

        Assert.NotNull(sneakerResponse);
        Assert.Equal(4, sneakerResponse.Stock);
    }
}