using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ParaTakip.Entities
{
    public class MongoBaseEntity
    {
        [BsonId]
        public ObjectId RecordId { get; set; } = ObjectId.GenerateNewId();

        public string StringRecordId
        {
            get { return RecordId.ToString(); }
        }

        private DateTime _RecordCreateDate {  get; set; }

        [Required]
        [DisplayName("Kayıt Tarihi")]
        public DateTime RecordCreateDate
        {
            get
            {
                return _RecordCreateDate.ToLocalTime();
            }
            set => _RecordCreateDate = value.ToUniversalTime();
        }

        private DateTime _RecordUpdateDate {  get; set; }
        
        [DisplayName("Güncelleme Tarihi")]
        public DateTime RecordUpdateDate
        {
            get
            {
                return _RecordUpdateDate.ToLocalTime();
            }
            set => _RecordUpdateDate = value;
        }

        [Required]
        [DisplayName("Kayıt Statüsü")]
        public bool RecordStatus { get; set; } = true;

        public string? RecordCreateUsername { get; set; }

        public string? RecordUpdateUsername { get; set; }
    }
}
