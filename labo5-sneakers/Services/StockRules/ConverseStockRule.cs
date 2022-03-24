using labo5_sneakers.Models;

namespace labo5_sneakers.Services.StockRules;

public class ConverseStockRule : IStockRule
{
    public string RuleName => "converse rule";

    public bool IsApplicable(Sneaker sneaker)
    {
        return sneaker.Brand?.Name == "CONVERSE" && sneaker.Stock < 50;
    }

    public void DoAction(Sneaker sneaker)
    {
        Console.WriteLine($"writing stock file to CONVERSE brand for sneaker {sneaker.SneakerId}");
    }
}