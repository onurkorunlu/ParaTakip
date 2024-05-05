using HtmlAgilityPack;
using ParaTakip.Model.Api;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ParaTakip.Common;

namespace ParaTakip.Business.Helpers
{
    public static class FundTradeHelper
    {
       public static FundInfo  GetFundPrice(string fundCode)
        {
            FundInfo response = new FundInfo();

            using (var client = new HttpClient())
            {
                string url = "https://www.tefas.gov.tr/FonAnaliz.aspx?FonKod="+ fundCode;
                using HttpResponseMessage httpResponse = client.GetAsync(url).Result;
                httpResponse.EnsureSuccessStatusCode();
                string responseBody = httpResponse.Content.ReadAsStringAsync().Result;
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(responseBody.ToString());

                decimal price = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//div/ul/li/span")[0].InnerText);
                decimal dailyReturn = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//div/ul/li/span")[1].InnerText.Replace("%", ""));
                long shares = long.Parse(htmlDoc.DocumentNode.SelectNodes("//div/ul/li/span")[2].InnerText.Replace(".", ""));
                decimal fundTotalValue = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//div/ul/li/span")[3].InnerText);
                string category = htmlDoc.DocumentNode.SelectNodes("//div/ul/li/span")[4].InnerText;
                string name = htmlDoc.DocumentNode.SelectNodes("//div/div/h2/span")[0].InnerText;

                response = new FundInfo()
                {
                    Category = category,
                    DailyReturn = dailyReturn,
                    FundTotalValue = fundTotalValue,
                    Name = name,
                    Price = price,
                    Shares = shares
                };
            }

            return response;
        }
    }
}
