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

        public Dictionary<string, CurrencyInfo> CurrencyDictionary { get; set; } = new Dictionary<string, CurrencyInfo>();

        public class CurrencyInfo
        {
            public string? Name { get; set; }
            public string? CurrencyCode { get; set; }
            public string? CurrencyCodeDigit { get; set; }
            public decimal Unit { get; set; }
            public decimal Buying { get; set; }

            public DateTime LastUpdateDate { get; set; }
        }
    }


}
