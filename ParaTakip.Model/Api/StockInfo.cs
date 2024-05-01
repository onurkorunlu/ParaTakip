using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParaTakip.Model.Api
{
    public class StockInfo
    {
        public decimal Price { get; set; }
        public decimal DifferenceRatio { get; set; }
        public decimal Difference { get; set; }
        public decimal Volume { get; set; }
        public string Name { get; set; }
        public decimal StartValue { get; set; }
        public decimal DailyMinValue { get; set; }
        public decimal DailyMaxValue { get; set; }
        public decimal WeeklyMinValue { get; set; }
        public decimal WeeklyMaxValue { get; set; }
        public decimal MonthlyMinValue { get; set;}
        public decimal MonthlyMaxValue { get; set;} 

    }
}
