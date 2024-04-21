using Newtonsoft.Json;

namespace ParaTakip.Common
{
    public class Currency
    {
        public string ALPHA_CURRENCY_CODE { get; set; }
        public string CURRENCY_CODE { get; set; }
        public string NAME { get; set; }

        public static Dictionary<string, Currency>? GetCurrencyList()
        {
            var json = File.ReadAllText("currency.json");
            return JsonConvert.DeserializeObject<Dictionary<string, Currency>>(json);
        }
    }

    public class CurrencyCache
    {
        public Dictionary<string, Currency> Values;

        public CurrencyCache()
        {

            Values = Currency.GetCurrencyList() ?? new Dictionary<string, Currency>();
        }

        public new static CurrencyCache Instance
        {
            get
            {
                return SingletonProvider<CurrencyCache>.Instance;
            }
        }
    }
}
