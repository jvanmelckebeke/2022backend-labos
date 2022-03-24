using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using GreenDonut;
using labo5_sneakers.DTO;
using labo5_sneakers.Models;
using labo5_test.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace labo5_test;

public class IntegrationTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly HttpClient _client;

    Sneaker _sneakerValid = new()
    {
        SneakerId = "a51cffc3-55e4-4762-bee3-6a36e0456c8c",
        Name = "Pursuit",
        Price = new decimal(37.8),
        Stock = 500,
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

    Sneaker _sneakerInvalid = new()
    {
        Name = "Pursuit",
        Price = new decimal(-37.8),
        Stock = 500,
        Brand = new Brand()
        {
            BrandId = "62334349301e056f50c7fcc0",
            Name = "SCOTT"
        },
        Occasions = new List<Occasion>()
    };

    public IntegrationTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;


        var application = Helper.CreateApi();
        _client = application.CreateClient();

        _client.GetAsync("/setup");
    }

    [Fact]
    public async Task Should_Return_Brands()
    {
        var result = await _client.GetAsync("/brands");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var brands = await result.Content.ReadFromJsonAsync<List<Brand>>();
        Assert.NotNull(brands);
        Assert.NotEmpty(brands);
    }


    [Fact]
    public async Task Should_Return_Occasions()
    {
        var result = await _client.GetAsync("/occasions");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var occasions = await result.Content.ReadFromJsonAsync<List<Occasion>>();
        Assert.NotNull(occasions);
        Assert.NotEmpty(occasions);
    }

    [Fact]
    public async Task Should_Return_Sneakers()
    {
        var result = await _client.GetAsync("/sneakers");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var occasions = await result.Content.ReadFromJsonAsync<List<Sneaker>>();
        Assert.NotNull(occasions);
    }

    [Fact]
    public async Task Should_Create_Sneaker_Valid()
    {
        HttpContent body = JsonContent.Create(_sneakerValid);

        var result = await _client.PostAsync("/sneakers", body);
        result.StatusCode.Should().Be(HttpStatusCode.Created);

        var created = await result.Content.ReadFromJsonAsync<Sneaker>();

        Assert.NotNull(created);
        Assert.Equal(created.Price, new decimal(37.8));

        _testOutputHelper.WriteLine(result.ToString());
        _testOutputHelper.WriteLine(created.ToString());
    }


    [Fact]
    public async Task Should_Create_Sneaker_Invalid()
    {
        HttpContent body = JsonContent.Create(_sneakerInvalid);

        var result = await _client.PostAsync("/sneakers", body);
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var resultBody = await result.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(result.ToString());
        _testOutputHelper.WriteLine(resultBody);
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

        HttpContent body = JsonContent.Create(order);


        var result = await _client.PostAsync("/orders", body);
        var resultBody = await result.Content.ReadAsStringAsync();
        var created = await result.Content.ReadFromJsonAsync<OrderResponse>();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        Assert.Equal("Order Success", created.Status);
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

        HttpContent body = JsonContent.Create(order);


        var result = await _client.PostAsync("/orders", body);
        var resultBody = await result.Content.ReadAsStringAsync();
        var created = await result.Content.ReadFromJsonAsync<OrderResponse>();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        Assert.Equal("Out of stock", created.Status);
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

        HttpContent body = JsonContent.Create(order);

        var result = await _client.PostAsync("/orders", body);
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}