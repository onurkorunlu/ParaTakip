using ParaTakip.Common;
using ParaTakip.PythonIntegrator;

namespace ParaTakip.Business.Caches
{
    public class FundRefundCache : CacheBaseSingleLoad<decimal>
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

        public override decimal GetCacheValue(string code)
        {
            try
            {
                if (!Instance.Values.ContainsKey(code))
                {
                    Instance.Values.Add(code, Tefas.GetFundPrice(code));
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                return 0;
            }
            
            return Instance.Values[code];
        }
    }
}
