using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParaTakip.Model.Api
{
    public class FundInfo
    {
        public decimal Price { get; set; }
        public decimal DailyReturn { get; set; }
        public long Shares { get; set; }
        public decimal FundTotalValue { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
    }
}
