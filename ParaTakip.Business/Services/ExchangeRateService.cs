using ParaTakip.Business.Base;
using ParaTakip.Business.Helpers;
using ParaTakip.Business.Interfaces;
using ParaTakip.Core;
using ParaTakip.DataAccess.Interfaces;
using ParaTakip.Entities;

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

            var updatedRates = ExchangeRateHelper.Get();
            ExchangeRate exchangeRate = new ExchangeRate()
            {
                Date = date.GetValueOrDefault(),
                CurrencyDictionary = updatedRates.ToDictionary(x => x.CurrencyCode)
            };

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
