using ParaTakip.Business.Base;
using ParaTakip.DataAccess.Interfaces;
using ParaTakip.Entities;

namespace ParaTakip.Business.Interfaces
{
    public interface IEmailTemplateService : IBaseService<EmailTemplate, IEmailTemplateDataAccess>
    {
    }
}
