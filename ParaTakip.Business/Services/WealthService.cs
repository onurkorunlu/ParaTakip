using MongoDB.Bson;
using ParaTakip.Business.Base;
using ParaTakip.Business.Interfaces;
using ParaTakip.DataAccess.Interfaces;
using ParaTakip.Entities;

namespace ParaTakip.Business.Services
{
    public class WealthService : BaseService<Wealth, IWealthDataAccess>, IWealthService
    {
        public Wealth? GetByUserId(string authenticatedUserId)
        {
            return FilterBy(x => x.AppUserId == ObjectId.Parse(authenticatedUserId)).FirstOrDefault();
        }
    }
}
