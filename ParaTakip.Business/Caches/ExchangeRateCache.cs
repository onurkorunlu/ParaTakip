using ParaTakip.Business.Interfaces;
using ParaTakip.Common;
using ParaTakip.Core;
using ParaTakip.Entities;
using System.IO;
using System.Reflection;
using static ParaTakip.Entities.ExchangeRate;

namespace ParaTakip.Business.Caches
{
    public class ExchangeRateCache : CacheBase<CurrencyInfo>
    {
        public ExchangeRateCache(): base("ExchangeRateCache")
        {
        }

        public new static ExchangeRateCache Instance
        {
            get
            {
               return SingletonProvider<ExchangeRateCache>.Instance;
            }
        }

        protected override Dictionary<string, CurrencyInfo> GetPrepareCacheDictionary()
        {
            Dictionary<string,CurrencyInfo> dictonary = [];

            ExchangeRate? rates = AppServiceProvider.Instance.Get<IExchangeRateService>().GetLast();
            if (rates == null)
            {
                return dictonary;
            }

            if (rates.Date != DateTime.Now.Date)
            {
                
            }

            foreach (var rate in rates.CurrencyDictionary)
            {
                rate.Value.Name = CurrencyCache.Instance.Values[rate.Key].NAME;
                rate.Value.CurrencyCodeDigit = CurrencyCache.Instance.Values[rate.Key].CURRENCY_CODE;
                dictonary.Add(rate.Key, rate.Value);
            }

            return dictonary;
        }
    }
}
