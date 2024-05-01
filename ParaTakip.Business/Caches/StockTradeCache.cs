using ParaTakip.Business.Helpers;
using ParaTakip.Common;
using ParaTakip.Model.Api;

namespace ParaTakip.Business.Caches
{
    public class StockInfoCache : CacheBaseSingleLoad<StockInfo>
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public StockInfoCache() : base("FundRefundCache")
        {
        }

        public new static StockInfoCache Instance
        {
            get
            {
                return SingletonProvider<StockInfoCache>.Instance;
            }
        }

        public override StockInfo GetCacheValue(string code)
        {
            try
            {
                if(!Instance.Values.ContainsKey(code))
                {
                    Instance.Values.Add(code, StockTradeHelper.GetStockPrice(code));
                }
                else if (Instance.Values[code].Price==0)
                {
                    Instance.Values[code] = StockTradeHelper.GetStockPrice(code);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new StockInfo();
            }
            
            return Instance.Values[code];
        }
    }
}
