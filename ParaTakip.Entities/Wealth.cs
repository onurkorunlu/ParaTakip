using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using ParaTakip.Entities.Enums;
using MongoDB.Bson.Serialization.Options;

namespace ParaTakip.Entities
{
    public class Wealth : MongoBaseEntity
    {
        public ObjectId AppUserId { get; set; }

        public string StringAppUserId
        {
            get { return AppUserId.ToString(); }
        }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<WealthType, List<BaseWealthValue>> Values { get; set; } = new Dictionary<WealthType, List<BaseWealthValue>>
        {
            {WealthType.FOREIGN_EXCHANGE_AND_PRECIOUS_METALS, new List<BaseWealthValue>() },
            {WealthType.STOCK_TRADING, new List<BaseWealthValue>() },
            {WealthType.FUND_TRADING, new List<BaseWealthValue>() }
        };

        public class ForeignExchangeAndPreciousMetals : BaseWealthValue
        {
            public decimal Amount { get; set; }
            public decimal Buying { get; set; }
            public string Currency { get; set; }
            public DateTime Date { get; set; }
        }

        public class StockTrading  : BaseWealthValue
        {

        }

        public class FundTrading: BaseWealthValue
        {

        }

        [BsonDiscriminator(RootClass = true)]
        [BsonKnownTypes(typeof(ForeignExchangeAndPreciousMetals), typeof(StockTrading), typeof(FundTrading))]
        public class BaseWealthValue
        {

        }
    }
}
