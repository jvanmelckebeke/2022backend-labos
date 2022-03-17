using labo5_sneakers.Models;
using labo5_sneakers.Repositories;

namespace labo5_sneakers.Services;

public interface ISneakerService
{
    Task SetupData();

    Task<List<Brand>> GetBrands();

    Task<List<Occasion>> GetOccasions();

    Task<List<Sneaker>> GetSneakers();

    Task<Sneaker> AddSneaker(Sneaker sneaker);

    Task<Sneaker> GetSneakerBySneakerId(string sneakerId);
}

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
}