using labo5_sneakers.Models;

namespace labo5_sneakers.DTO;

public class OrderResponse
{
    public Order Order { get; set; }

    public string Status { get; set; }
}