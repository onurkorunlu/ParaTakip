using MongoDB.Bson;
using ParaTakip.Entities;
using ParaTakip.Entities.Enums;
using System.ComponentModel;

namespace ParaTakip.Entities
{
    public class AppUser : MongoBaseEntity
    {
        [DisplayName("Kullanıcı")]
        public string Username { get; set; }

        [DisplayName("E-Posta Adresi")]
        public string EmailAddress { get; set; }


        [DisplayName("Şifre")]
        public string Password { get; set; }


        [DisplayName("Kullanıcı Rolü")]
        public ObjectId RoleId { get; set; }

        public Dictionary<PreferencesType, object> Preferences { get; set; } = new Dictionary<PreferencesType, object>();

        public List<CreditCard> CreditCards { get; set; } = new List<CreditCard>();

        public class CreditCard: MongoBaseEntity
        {
            public string MaskedCardNumber { get; set; }
            public string BankName { get; set; }
            public short StatementDay { get; set; }
            public short LastPaymentDay { get; set;}
        }

    }


}
