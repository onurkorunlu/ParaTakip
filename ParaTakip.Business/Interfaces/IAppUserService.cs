using Microsoft.AspNetCore.Http;
using ParaTakip.Business.Base;
using ParaTakip.DataAccess.Interfaces;
using ParaTakip.Entities;
using ParaTakip.Model.RequestModel;
using ParaTakip.Model.ResponseModel;

namespace ParaTakip.Business.Interfaces
{
    public interface IAppUserService : IBaseService<AppUser, IAppUserDataAccess>
    {
        LoginResultModel Login(LoginServiceRequestModel loginModel);
        void BatchLogin(HttpContext httpContext);
        LoginResultModel TokenBasedLogin(LoginServiceRequestModel model);
        AppUser? GetByUserName(string userName);
        RegisterResultModel Register(RegisterServiceRequestModel requestModel);
        List<GetEventsResponseModel> GetCreditCardEvents(string authenticatedUserId, DateTime date);
    }
}
