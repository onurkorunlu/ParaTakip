// See https://aka.ms/new-console-template for more information


using HtmlAgilityPack;
using ParaTakip.Common;
using System.Xml;

string stockCode = "KCHOL";

string url = "https://www.getmidas.com/canli-borsa/kchol-hisse/";
HtmlWeb web = new HtmlWeb();
var htmlDoc = web.Load(url);

string name = htmlDoc.DocumentNode.SelectNodes("//div/div/h1")[0].InnerText;
decimal price = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//p[contains(@class, 'val')]")[0].InnerText.Substring(1));
decimal volume = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//p[contains(@class, 'val')]")[1].InnerText.Substring(1));
decimal dailyDifRatio = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//span[contains(@class, 'val')]")[2].InnerText.ToDigit());
decimal dailyDifValue = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//span[contains(@class, 'val')]")[13].InnerText.Substring(1));
decimal dailyMinValue = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//span[contains(@class, 'val')]")[4].InnerText.Substring(1));
decimal dailyMaxValue = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//span[contains(@class, 'val')]")[15].InnerText.Substring(1));
decimal weeklyMinValue = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//span[contains(@class, 'val')]")[16].InnerText.Substring(1));
decimal weeklyMaxValue = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//span[contains(@class, 'val')]")[5].InnerText.Substring(1));
decimal monthlyMinValue = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//span[contains(@class, 'val')]")[17].InnerText.Substring(1));
decimal monthlyMaxValue = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//span[contains(@class, 'val')]")[6].InnerText.Substring(1));
decimal startValue = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//span[contains(@class, 'val')]")[11].InnerText.Substring(1));

