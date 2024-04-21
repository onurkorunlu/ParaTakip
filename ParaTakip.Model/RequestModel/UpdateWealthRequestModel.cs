using ParaTakip.Entities.Enums;

namespace ParaTakip.Model.RequestModel
{
    public class UpdateWealthRequestModel
    {
        public WealthType WealthType { get; set; }
        public List<object> WealthValues { get; set; }
    }
}
