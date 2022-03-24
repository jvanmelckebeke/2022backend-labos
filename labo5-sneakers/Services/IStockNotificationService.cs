using labo5_sneakers.Models;

namespace labo5_sneakers.Services;

public interface IStockNotificationService
{
    void CheckStockNotificationRules(Sneaker sneaker);
}