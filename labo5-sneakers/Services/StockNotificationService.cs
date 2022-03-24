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

    public Dictionary<string, bool> CheckStockNotificationRules(Sneaker sneaker)
    {
        Dictionary<string, bool> notifsFired = new Dictionary<string, bool>();
        foreach (IStockRule rule in _stockRules)
        {
            var applicable = rule.IsApplicable(sneaker);
            notifsFired.Add(rule.RuleName, applicable);
            if (applicable)
            {
                rule.DoAction(sneaker);
                Console.WriteLine($"done action for {rule.RuleName}");
            }
        }

        return notifsFired;
    }
}