using labo5_sneakers.Models;

namespace labo5_sneakers.Services;

public interface IStockNotificationService
{
    Dictionary<string, bool> CheckStockNotificationRules(Sneaker sneaker);
}