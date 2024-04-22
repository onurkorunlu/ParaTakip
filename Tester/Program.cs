// See https://aka.ms/new-console-template for more information


using HtmlAgilityPack;
using System.Xml;

using (var client = new HttpClient())
{
    string url = "https://www.tefas.gov.tr/FonAnaliz.aspx?FonKod=DLY";
    using HttpResponseMessage httpResponse = client.GetAsync(url).Result;
    httpResponse.EnsureSuccessStatusCode();
    string responseBody = httpResponse.Content.ReadAsStringAsync().Result;
    // Above three lines can be replaced with new helper method below
    // string responseBody = await client.GetStringAsync(uri);

    var htmlDoc = new HtmlDocument();
    htmlDoc.LoadHtml(responseBody.ToString());

    var price = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//div/ul/li/span")[0].InnerText);
    var dailyReturn = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//div/ul/li/span")[1].InnerText.Replace("%",""));
    var shares = long.Parse(htmlDoc.DocumentNode.SelectNodes("//div/ul/li/span")[2].InnerText.Replace(".",""));
    var FundTotalValue = decimal.Parse(htmlDoc.DocumentNode.SelectNodes("//div/ul/li/span")[3].InnerText);
    var category = htmlDoc.DocumentNode.SelectNodes("//div/ul/li/span")[4].InnerText;
    var name = htmlDoc.DocumentNode.SelectNodes("//div/div/h2/span")[0].InnerText;

    
}