using ParaTakip.Entities.Enums;

namespace ParaTakip.Model.RequestModel
{
    public class AddWealthRequestModel
    {
        public WealthType WealthType { get; set; }
        public List<object> WealthValues { get;set;}
    }
}
