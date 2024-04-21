using MongoDB.Driver;
using ParaTakip.DataAccess.Base;
using ParaTakip.DataAccess.Interfaces;
using ParaTakip.Entities;

namespace ParaTakip.DataAccess.Services
{
    public class ExchangeRateDataAccess : BaseDataAcccess<ExchangeRate>, IExchangeRateDataAccess
    {
        public ExchangeRate? GetLast()
        {
            FilterDefinitionBuilder<ExchangeRate> filterBuilder = new FilterDefinitionBuilder<ExchangeRate>();
            return this.Find(filterBuilder.Empty).OrderByDescending(x=>x.Date).FirstOrDefault();
        }
    }
}
