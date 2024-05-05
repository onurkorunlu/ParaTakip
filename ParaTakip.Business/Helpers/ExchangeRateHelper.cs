using HtmlAgilityPack;
using ParaTakip.Common;
using static ParaTakip.Entities.ExchangeRate;

namespace ParaTakip.Business.Helpers
{
    public class ExchangeRateHelper
    {
        public static List<ExchangeRateInfo> Get()
        {
            List<ExchangeRateInfo> response = new List<ExchangeRateInfo>();

            response.AddRange(GetCurrencies());
            response.AddRange(GetGoldPrices());

            return response;
        }

        private static List<ExchangeRateInfo> GetCurrencies()
        {
            List<ExchangeRateInfo> response = new List<ExchangeRateInfo>();

            string url = $"https://kur.doviz.com/";
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(url);

            var currencies = htmlDoc.GetElementbyId("currencies").SelectNodes("//tbody/tr/td");
            for (int i = 0; i < 60; i++)
            {
                if (currencies[i].InnerText.Split("\n").Length < 6)
                {
                    continue;
                }

                response.Add(new ExchangeRateInfo
                {
                    CurrencyCode = currencies[i].InnerText.Split("\n")[4].Trim(),
                    Name = currencies[i].InnerText.Split("\n")[5].Trim(),
                    Buying = decimal.Parse(currencies[i + 1].InnerText),
                    Selling = decimal.Parse(currencies[i + 2].InnerText),
                    MaxValue = decimal.Parse(currencies[i + 3].InnerText),
                    MinValue = decimal.Parse(currencies[i + 4].InnerText),
                    Change = decimal.Parse(currencies[i + 5].InnerText.Trim().Substring(1)),
                    Time = currencies[i + 6].InnerText.Trim()
                });

                i += 6;
            }

            return response;
        }

        private static List<ExchangeRateInfo> GetGoldPrices()
        {
            List<ExchangeRateInfo> response = new List<ExchangeRateInfo>();

            string url = $"https://altin.doviz.com/";
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(url);

            var currencies = htmlDoc.GetElementbyId("golds").SelectNodes("//td");
            for (int i = 5; i < 85; i++)
            {
                var namefields = currencies[i].InnerText.Split("\n");
                if (namefields.Length < 6)
                {
                    continue;
                }

                string name = namefields[4].Trim();

                response.Add(new ExchangeRateInfo
                {
                    CurrencyCode = GetGoldCurrency(name.Split(" ")),
                    Name = currencies[i].InnerText.Split("\n")[4].Trim(),
                    Buying = decimal.Parse(currencies[i + 1].InnerText),
                    Selling = decimal.Parse(currencies[i + 2].InnerText),
                    Change = decimal.Parse(currencies[i + 3].InnerText.Trim().Substring(1)),
                    Time = currencies[i + 4].InnerText.Trim()
                });

                i += 4;
            }

            return response;
        }

        public static string GetGoldCurrency(string[] strings)
        {
            string code = string.Empty;
            foreach (var item in strings)
            {
                if (!string.IsNullOrEmpty(code))
                {
                    code += "_";
                }

                code += item.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("tr-TR"));
            }

            return code;
        }

    }
}
