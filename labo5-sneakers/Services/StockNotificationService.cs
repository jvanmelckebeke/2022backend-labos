using labo5_sneakers.Models;
using labo5_sneakers.Repositories;
using labo5_sneakers.Services.StockRules;

namespace labo5_sneakers.Services;

public class StockNotificationService : IStockNotificationService
{
    private readonly List<IStockRule> _stockRules = new()
    {
        new PumaStockRule(), new ConverseStockRule()
    };

    public void CheckStockNotificationRules(Sneaker sneaker)
    {
        foreach (IStockRule rule in _stockRules)
        {
            if (rule.IsApplicable(sneaker))
            {
                rule.DoAction(sneaker);
                Console.WriteLine($"done action for {rule.RuleName}");
            }
        }
    }
}