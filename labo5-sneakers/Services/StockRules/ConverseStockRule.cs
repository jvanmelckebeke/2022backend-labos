using System.Diagnostics;
using labo5_sneakers.Models;

namespace labo5_sneakers.Services.StockRules;

public class ConverseStockRule : IStockRule
{
    public string RuleName => "converse rule";

    public bool IsApplicable(Sneaker sneaker)
    {
        return sneaker.Brand?.Name == "CONVERSE" && sneaker.Stock < 50;
    }

    public async Task DoAction(Sneaker sneaker)
    {
        
        var line = $"[{DateTime.Now.ToString("s")}] CONVERSE stock notification for sneaker {sneaker.SneakerId}, current stock: {sneaker.Stock}";
        await File.AppendAllLinesAsync("/tmp/saleslog.log", new[] {line});
        
        Console.WriteLine($"writing stock file to CONVERSE brand for sneaker {sneaker.SneakerId}");
    }
}