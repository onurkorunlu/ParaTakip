using ParaTakip.Entities.Enums;

namespace ParaTakip.Model.RequestModel
{
    public class AddCreditCardRequestModel
    {
        public string MaskedCardNumber { get; set; }
        public short StatementDay { get; set; }
        public short LastPaymentDay { get; set; }
    }
}
