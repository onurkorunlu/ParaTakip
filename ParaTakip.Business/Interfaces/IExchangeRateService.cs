using ParaTakip.Business.Base;
using ParaTakip.DataAccess.Interfaces;
using ParaTakip.Entities;

namespace ParaTakip.Business.Interfaces
{
    public interface IExchangeRateService : IBaseService<ExchangeRate, IExchangeRateDataAccess>
    {
        ExchangeRate? GetLast();
        ExchangeRate? GetByDate(DateTime today);
        Task Load(DateTime? date);
        void FactoryDelete(string recordId);
    }
}
