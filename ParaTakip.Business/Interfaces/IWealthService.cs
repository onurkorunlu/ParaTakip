using ParaTakip.Business.Base;
using ParaTakip.DataAccess.Interfaces;
using ParaTakip.Entities;
using ParaTakip.Model.RequestModel;

namespace ParaTakip.Business.Interfaces
{
    public interface IWealthService : IBaseService<Wealth, IWealthDataAccess>
    {
        Wealth? GetByUserId(string authenticatedUserId);

        Wealth Update(UpdateWealthRequestModel model, string authenticatedUserId);
    }
}
