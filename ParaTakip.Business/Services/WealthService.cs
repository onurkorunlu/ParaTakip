using MongoDB.Bson;
using ParaTakip.Business.Base;
using ParaTakip.Business.Interfaces;
using ParaTakip.Core;
using ParaTakip.DataAccess.Interfaces;
using ParaTakip.Entities;
using ParaTakip.Model.RequestModel;
using static ParaTakip.Entities.Wealth;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch.Converters;

namespace ParaTakip.Business.Services
{
    public class WealthService : BaseService<Wealth, IWealthDataAccess>, IWealthService
    {
        public Wealth? GetByUserId(string authenticatedUserId)
        {
            return FilterBy(x => x.AppUserId == ObjectId.Parse(authenticatedUserId)).FirstOrDefault();
        }

        public Wealth Update(UpdateWealthRequestModel model, string authenticatedUserId)
        {
            Wealth response;

            List<BaseWealthValue> baseWealthValue = new List<BaseWealthValue>();
            switch (model.WealthType)
            {
                case Entities.Enums.WealthType.FOREIGN_EXCHANGE_AND_PRECIOUS_METALS:
                    baseWealthValue.AddRange(CastForeignExchangeAndPreciousMetals(model.WealthValues));
                    break;
                case Entities.Enums.WealthType.STOCK_TRADING:
                    break;
                case Entities.Enums.WealthType.FUND_TRADING:
                    break;
                default:
                    break;
            }

            var userWealth = AppServiceProvider.Instance.Get<IWealthService>().GetByUserId(authenticatedUserId);
            if (userWealth == null)
            {
                userWealth = new Wealth()
                {
                    AppUserId = MongoDB.Bson.ObjectId.Parse(authenticatedUserId)
                };

                userWealth.Values[model.WealthType] = baseWealthValue;
                response = this.Create(userWealth);
            }
            else
            {
                userWealth.Values[model.WealthType] = baseWealthValue;
                response = this.Update(userWealth);
            }


            return response;
        }

        private IEnumerable<ForeignExchangeAndPreciousMetals> CastForeignExchangeAndPreciousMetals(List<object> values)
        {
            List<ForeignExchangeAndPreciousMetals> result = new List<ForeignExchangeAndPreciousMetals>();
            foreach (var val in values)
            {
                result.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<Wealth.ForeignExchangeAndPreciousMetals>(val.ToString()));
            }

            return result;
        }
    }
}
