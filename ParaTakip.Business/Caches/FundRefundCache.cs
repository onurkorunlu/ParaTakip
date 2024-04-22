using ParaTakip.Business.Helpers;
using ParaTakip.Common;
using ParaTakip.Model.Api;

namespace ParaTakip.Business.Caches
{
    public class FundRefundCache : CacheBaseSingleLoad<FundInfo>
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public FundRefundCache() : base("FundRefundCache")
        {
        }

        public new static FundRefundCache Instance
        {
            get
            {
                return SingletonProvider<FundRefundCache>.Instance;
            }
        }

        public override FundInfo GetCacheValue(string code)
        {
            try
            {
                if(!Instance.Values.ContainsKey(code))
                {
                    Instance.Values.Add(code, FundTradeHelper.GetFundPrice(code));
                }
                else if (Instance.Values[code].Price==0)
                {
                    Instance.Values[code] = FundTradeHelper.GetFundPrice(code);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new FundInfo();
            }
            
            return Instance.Values[code];
        }
    }
}
