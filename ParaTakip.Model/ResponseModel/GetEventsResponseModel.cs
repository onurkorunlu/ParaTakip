using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParaTakip.Model.ResponseModel
{
    public class GetEventsResponseModel
    {
        public EventType EventType { get; set; }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool AllDay { get; set; } = true;

        public object? EventData { get; set; }

    }

    public enum EventType
    {
        CreditCardStatement,
        CreditCardStatementLastPayment,
    }
}
