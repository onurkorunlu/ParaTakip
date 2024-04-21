using ParaTakip.Business.Base;
using ParaTakip.Business.Helpers;
using ParaTakip.Business.Interfaces;
using ParaTakip.Common;
using ParaTakip.Core;
using ParaTakip.DataAccess.Interfaces;
using ParaTakip.Entities;

namespace ParaTakip.Business.Services
{
    public class AppUserRoleService : BaseService<AppUserRole, IAppUserRoleDataAccess>, IAppUserRoleService
    {
        public AppUserRole? GetByRoleName(string name)
        {
            return FilterBy(x=>x.RoleName == name).FirstOrDefault();
        }
    }
}
