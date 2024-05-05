using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParaTakip.Entities
{
    public class ExchangeRate : MongoBaseEntity
    {
        private DateTime _Date { get; set; }

        public DateTime Date
        {
            get
            {
                return _Date.ToLocalTime();
            }
            set
            {
                _Date = value.ToUniversalTime();
            }
        }

        public Dictionary<string, ExchangeRateInfo> CurrencyDictionary { get; set; } = new Dictionary<string, ExchangeRateInfo>();

        public class ExchangeRateInfo
        {
            public string CurrencyCode { get; set; }
            public string Name { get; set; }
            public decimal Buying { get; set; }
            public decimal Selling { get; set; }
            public decimal MinValue { get; set; }
            public decimal MaxValue { get; set; }
            public decimal Change { get; set; }
            public string Time { get; set; }
        }
    }


}
