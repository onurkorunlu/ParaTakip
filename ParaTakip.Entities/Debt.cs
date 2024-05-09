using MongoDB.Bson;

namespace ParaTakip.Entities
{
    public class Debt : MongoBaseEntity
    {
        public ObjectId AppUserId { get; set; }

        public string StringAppUserId
        {
            get { return AppUserId.ToString(); }
        }

        public List<DebtInfo> Values { get; set; } = [];
    }

    public class DebtInfo : MongoBaseEntity
    {
        public DebtType DebtType { get; set; }

        public decimal TotalAmount { get; set; }

        private DateTime _date { get; set; }
        public DateTime Date
        {
            get
            {
                return _date.ToLocalTime();
            }
            set => _date = value.ToUniversalTime();
        }

        public string BankName { get; set; }
        public short InstallmentCount { get; set; }
    }

    public enum DebtType
    {
        None = -1,
        Loan
    }
}
