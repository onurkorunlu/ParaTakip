using ParaTakip.Business.Base;
using ParaTakip.Business.Interfaces;
using ParaTakip.Common;
using ParaTakip.Core;
using ParaTakip.DataAccess.Interfaces;
using ParaTakip.Entities;
using System.Globalization;
using System.Reflection.Emit;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static ParaTakip.Entities.ExchangeRate;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ParaTakip.Business.Services
{
    public class ExchangeRateService : BaseService<ExchangeRate, IExchangeRateDataAccess>, IExchangeRateService
    {
        public ExchangeRate? GetLast()
        {
            return AppServiceProvider.Instance.Get<IExchangeRateDataAccess>().GetLast();
        }

        public ExchangeRate? GetByDate(DateTime today)
        {
            return this.FilterBy(x => x.Date == today)?.FirstOrDefault();
        }

        public async Task Load(DateTime? date = null)
        {
            date ??= DateTime.Now;

            string URLString = $"https://www.tcmb.gov.tr/reeskontkur/{date.Value:yyyyMM}/{date.Value:ddMMyyyy}-{date.Value:HH}00.xml";


            XmlDocument doc1 = new XmlDocument();
            doc1.Load(URLString);
            XmlElement root = doc1.DocumentElement;
            string dataDate = root["baslik_bilgi"]["zaman_etiketi"].InnerText;
            string saat = root["doviz_kur_liste"].Attributes["saat"].Value;
            date = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, int.Parse(saat.Split(':').First()), int.Parse(saat.Split(':').Last()), 0);

            ExchangeRate exchangeRate = new ExchangeRate()
            {
                Date = date.Value.Date,
                CurrencyDictionary = new Dictionary<string, CurrencyInfo>()
            };

            Dictionary<string, CurrencyInfo> currencyList = new Dictionary<string, CurrencyInfo>();

            var dataCurrencyList = root["doviz_kur_liste"];
            foreach (XmlNode node in dataCurrencyList)
            {
                string Unit = node["birim"].InnerText;
                string CurrencyCode = node["doviz_cinsi"].InnerText;
                string buying = node["alis"].InnerText;
                buying = buying.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);

                CurrencyInfo currencyInfo = new CurrencyInfo
                {
                    Unit = int.Parse(Unit),
                    CurrencyCode = CurrencyCode,
                    Buying = decimal.Parse(buying),
                    LastUpdateDate = date.Value
                };

                exchangeRate.CurrencyDictionary.TryAdd(currencyInfo.CurrencyCode, currencyInfo);
            }

            var currentRate = AppServiceProvider.Instance.Get<IExchangeRateService>().GetByDate(date.Value.Date);
            if (currentRate != null)
            {
                currentRate.CurrencyDictionary = exchangeRate.CurrencyDictionary;
                AppServiceProvider.Instance.Get<IExchangeRateService>().Update(currentRate);
            }
            else
            {
                AppServiceProvider.Instance.Get<IExchangeRateService>().Create(exchangeRate);
            }
        }

        public void FactoryDelete(string recordId)
        {
            this.FactoryDeleteById(recordId);
        }
    }
}
