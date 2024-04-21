using ParaTakip.Business.Base;
using ParaTakip.DataAccess.Interfaces;
using ParaTakip.Entities;

namespace ParaTakip.Business.Interfaces
{
    public interface IWealthService : IBaseService<Wealth, IWealthDataAccess>
    {
        Wealth? GetByUserId(string authenticatedUserId);
    }
}
