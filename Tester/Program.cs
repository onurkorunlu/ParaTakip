// See https://aka.ms/new-console-template for more information


using HtmlAgilityPack;
using Microsoft.Extensions.Primitives;
using ParaTakip.Common;
using System.Xml;
using static ParaTakip.Entities.ExchangeRate;


List<ExchangeRateInfo> response = new List<ExchangeRateInfo>();

string url = $"https://altin.doviz.com/";
HtmlWeb web = new HtmlWeb();
var htmlDoc = web.Load(url);

var currencies = htmlDoc.GetElementbyId("golds").SelectNodes("//td");
for (int i = 5; i < 60; i++)
{
    var namefields = currencies[i].InnerText.Split("\n");
    if (namefields.Length < 6)
    {
        continue;
    }

    string name = namefields[4].Trim();

    response.Add(new ExchangeRateInfo
    {
        CurrencyCode = string.Join("",name.Split(" ").Select(x=>x[0])),
        Name = currencies[i].InnerText.Split("\n")[4].Trim(),
        Buying = decimal.Parse(currencies[i + 1].InnerText.ToUniDecimal()),
        Selling = decimal.Parse(currencies[i + 2].InnerText.ToUniDecimal()),
        Change = decimal.Parse(currencies[i + 5].InnerText.Trim().Substring(1).ToUniDecimal()),
        Time = currencies[i + 6].InnerText.Trim()
    });

    i += 4;
}
