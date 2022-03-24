using System.Diagnostics;
using labo5_sneakers.Models;

namespace labo5_sneakers.Services.StockRules;

public class PumaStockRule : IStockRule
{
    public string RuleName => "puma rule";

    public bool IsApplicable(Sneaker sneaker)
    {
        return sneaker.Brand?.Name == "PUMA" && sneaker.Stock < 5;
    }

    public async Task DoAction(Sneaker sneaker)
    {
        var line =
            $"[{DateTime.Now.ToString("s")}] sending sales@puma.com for sneaker {sneaker.SneakerId}, current stock: {sneaker.Stock}";
        await File.AppendAllLinesAsync("/tmp/saleslog.log", new[] {line});

        Console.WriteLine("SENDING MAIL TO PUMA SALES");
    }
}