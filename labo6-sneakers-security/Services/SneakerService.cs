using labo5_sneakers.Models;
using labo5_sneakers.Repositories;

namespace labo5_sneakers.Services;

public class SneakerService : ISneakerService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IOccasionRepository _occasionRepository;
    private readonly ISneakerRepository _sneakerRepository;
    private readonly IOrderRepository _orderRepository;

    public SneakerService(IBrandRepository brandRepository, IOccasionRepository occasionRepository,
        ISneakerRepository sneakerRepository, IOrderRepository orderRepository)
    {
        _brandRepository = brandRepository;
        _occasionRepository = occasionRepository;
        _sneakerRepository = sneakerRepository;
        _orderRepository = orderRepository;
    }


    public async Task SetupData()
    {
        try
        {
            if (!(await _brandRepository.GetBrands()).Any())
                await _brandRepository.AddBrands(new List<Brand>()
                {
                    new() {Name = "ASICS"}, new() {Name = "CONVERSE"}, new() {Name = "JORDAN"},
                    new() {Name = "PUMA"}, new() {Name = "SCOTT"}
                });

            if (!(await _occasionRepository.GetOccasions()).Any())
                await _occasionRepository.AddOccasions(new List<Occasion>()
                {
                    new() {Description = "Sports"}, new() {Description = "Casual"},
                    new() {Description = "Skate"}, new() {Description = "Diner"}
                });
            if (!(await _sneakerRepository.GetSneakers()).Any())
            {
                var brands = await _brandRepository.GetBrands();
                var occasions = await _occasionRepository.GetOccasions();

                var sneakers = new List<Sneaker>(){new()
                {
                    Brand = brands[4], Name = "Pursuit", Occasions = occasions.GetRange(0, 2),
                    Price = new decimal(37.8), Stock = 50
                },
                    new()
                    {
                        Brand = brands[3], Name = "Smash v2", Occasions = occasions.GetRange(0, 3),
                        Price = new decimal(38.45), Stock = 5
                    }
                };

                await _sneakerRepository.AddSneakers(sneakers);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<List<Brand>> GetBrands()
    {
        return await _brandRepository.GetBrands();
    }

    public async Task<List<Occasion>> GetOccasions()
    {
        return await _occasionRepository.GetOccasions();
    }

    public async Task<List<Sneaker>> GetSneakers()
    {
        return await _sneakerRepository.GetSneakers();
    }

    public async Task<Sneaker> AddSneaker(Sneaker sneaker)
    {
        return await _sneakerRepository.AddSneaker(sneaker);
    }

    public async Task<Sneaker> GetSneakerBySneakerId(string sneakerId)
    {
        return await _sneakerRepository.GetSneakerBySneakerId(sneakerId);
    }

    public async Task<Order> AddOrder(Order order)
    {
        await _orderRepository.AddOrder(order);

        var sneaker = await GetSneakerBySneakerId(order.SneakerId);
        
        sneaker.Stock -= order.NumberOfItems;

        await _sneakerRepository.UpdateSneaker(sneaker);

        return order;
    }
}