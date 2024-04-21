using ParaTakip.Business.Base;
using ParaTakip.Business.Helpers;
using ParaTakip.Business.Interfaces;
using ParaTakip.Common;
using ParaTakip.Core;
using ParaTakip.DataAccess.Interfaces;
using ParaTakip.Entities;

namespace ParaTakip.Business.Services
{
    public class EmailTemplateService : BaseService<EmailTemplate, IEmailTemplateDataAccess>, IEmailTemplateService
    {
    }
}
