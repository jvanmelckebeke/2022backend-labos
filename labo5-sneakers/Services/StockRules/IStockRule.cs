using labo5_sneakers.Models;

namespace labo5_sneakers.Services.StockRules;

public interface IStockRule
{
    String RuleName { get; }
    bool IsApplicable(Sneaker sneaker);

    void DoAction(Sneaker sneaker);
}