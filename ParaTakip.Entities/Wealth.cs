using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using ParaTakip.Entities.Enums;

namespace ParaTakip.Entities
{
    public class Wealth : MongoBaseEntity
    {
        public ObjectId AppUserId { get; set; }

        public string StringAppUserId
        {
            get { return AppUserId.ToString(); }
        }

        public Dictionary<WealthType, object> Values { get; set; } = new Dictionary<WealthType, object>()
        {
            {WealthType.FOREIGN_EXCHANGE_AND_PRECIOUS_METALS, new List<ForeignExchangeAndPreciousMetals>()},
            {WealthType.STOCK_TRADING, new List<StockTrading>()},
            {WealthType.FUND_TRADING, new List<FundTrading>()}
        };

        public class ForeignExchangeAndPreciousMetals 
        {

        }

        public class StockTrading
        {

        }

        public class FundTrading
        {

        }
    }
}
