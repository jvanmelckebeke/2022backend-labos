using labo5_sneakers.Models;

namespace labo5_sneakers.Services.StockRules;

public class PumaStockRule : IStockRule
{
    public string RuleName => "puma rule";

    public bool IsApplicable(Sneaker sneaker)
    {
        return sneaker.Brand?.Name == "PUMA" && sneaker.Stock < 5;
    }

    public void DoAction(Sneaker sneaker)
    {
        Console.WriteLine("SENDING MAIL TO PUMA SALES");
    }
}