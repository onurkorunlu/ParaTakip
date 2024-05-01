using HtmlAgilityPack;
using ParaTakip.Common;
using ParaTakip.Model.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParaTakip.Business.Helpers
{
    public class StockTradeHelper
    {
        public static StockInfo GetStockPrice(string stockCode)
        {
            StockInfo response = new StockInfo();

            using (var client = new HttpClient())
            {
                string url = $"https://www.getmidas.com/canli-borsa/{stockCode}-hisse/";
                HtmlWeb web = new HtmlWeb();
                var htmlDoc = web.Load(url);
                string name = htmlDoc.DocumentNode.SelectNodes("//div/div/h1")[0].InnerText;
                decimal price = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//p[contains(@class, 'val')]")[0].InnerText.Substring(1).ToUniDecimal());
                decimal volume = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//p[contains(@class, 'val')]")[1].InnerText.Substring(1).ToUniDecimal());
                decimal dailyDifRatio = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//span[contains(@class, 'val')]")[2].InnerText.ToDigit().ToUniDecimal());
                decimal dailyDifValue = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//span[contains(@class, 'val')]")[13].InnerText.Substring(1).ToUniDecimal());
                decimal dailyMinValue = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//span[contains(@class, 'val')]")[4].InnerText.Substring(1).ToUniDecimal());
                decimal dailyMaxValue = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//span[contains(@class, 'val')]")[15].InnerText.Substring(1).ToUniDecimal());
                decimal weeklyMinValue = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//span[contains(@class, 'val')]")[16].InnerText.Substring(1).ToUniDecimal());
                decimal weeklyMaxValue = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//span[contains(@class, 'val')]")[5].InnerText.Substring(1).ToUniDecimal());
                decimal monthlyMinValue = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//span[contains(@class, 'val')]")[17].InnerText.Substring(1).ToUniDecimal());
                decimal monthlyMaxValue = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//span[contains(@class, 'val')]")[6].InnerText.Substring(1).ToUniDecimal());
                decimal startValue = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//span[contains(@class, 'val')]")[11].InnerText.Substring(1).ToUniDecimal());

                response = new StockInfo()
                {
                    Name = name,
                    Price = price,
                    DailyMaxValue = dailyMaxValue,
                    DailyMinValue = dailyMinValue,  
                    Difference = dailyDifValue,
                    DifferenceRatio = dailyDifRatio,
                    WeeklyMinValue=weeklyMinValue,
                    WeeklyMaxValue = weeklyMaxValue,
                    MonthlyMinValue = monthlyMinValue,
                    MonthlyMaxValue = monthlyMaxValue,
                    StartValue = startValue,
                    Volume = volume,
                };
            }

            return response;
        }
    }
}
