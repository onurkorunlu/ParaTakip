using ParaTakip.Business.Base;
using ParaTakip.DataAccess.Interfaces;
using ParaTakip.Entities;
using ParaTakip.Model.ResponseModel;

namespace ParaTakip.Business.Interfaces
{
    public interface IDebtService : IBaseService<Debt, IDebtDataAccess>
    {
        Debt? GetByUserId(string authenticatedUserId);
        List<GetEventsResponseModel> GetDebtEvents(string authenticatedUserId, DateTime dateTime);
        Debt? Update(List<DebtInfo> model, string authenticatedUserId);
    }
}
