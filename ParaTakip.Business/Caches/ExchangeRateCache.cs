using Amazon.Runtime.Internal.Transform;
using ParaTakip.Business.Helpers;
using ParaTakip.Business.Interfaces;
using ParaTakip.Common;
using ParaTakip.Core;
using ParaTakip.Entities;
using System.IO;
using System.Reflection;
using static ParaTakip.Entities.ExchangeRate;

namespace ParaTakip.Business.Caches
{
    public class ExchangeRateCache : CacheBase<ExchangeRateInfo>
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

        public void Reset()
        {
            Instance.Reset();
        }

        protected override Dictionary<string, ExchangeRateInfo> GetPrepareCacheDictionary()
        {
            Dictionary<string, ExchangeRateInfo> dictonary = [];

            ExchangeRate? rates = AppServiceProvider.Instance.Get<IExchangeRateService>().GetLast();
            if (rates == null)
            {
                return dictonary;
            }

            dictonary = rates.CurrencyDictionary;

            return dictonary;
        }
    }
}
