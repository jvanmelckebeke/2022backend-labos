using labo5_sneakers.DTO;
using labo5_sneakers.Models;

namespace labo5_sneakers.Services;

public interface ISneakerService
{
    Task SetupData();

    Task<List<Brand>> GetBrands();

    Task<List<Occasion>> GetOccasions();

    Task<List<Sneaker>> GetSneakers();

    Task<Sneaker> AddSneaker(Sneaker sneaker);

    Task<OrderResponse?> AddOrder(Order? order);

    Task<Sneaker?> GetSneakerBySneakerId(string sneakerId);
}