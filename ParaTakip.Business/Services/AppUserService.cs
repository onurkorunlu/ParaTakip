using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using ParaTakip.Business.Base;
using ParaTakip.Business.Helpers;
using ParaTakip.Business.Interfaces;
using ParaTakip.Common;
using ParaTakip.Core;
using ParaTakip.DataAccess.Interfaces;
using ParaTakip.Entities;
using ParaTakip.Model.RequestModel;
using ParaTakip.Model.ResponseModel;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using ParaTakip.Entities.Enums;

namespace ParaTakip.Business.Services
{
    public class AppUserService : BaseService<AppUser, IAppUserDataAccess>, IAppUserService
    {
        public AppUser? GetByUserName(string userName)
        {
            return FilterBy(x => x.Username == userName).FirstOrDefault();
        }

        public AppUser? GetByEmailAddress(string emailAddress)
        {
            return FilterBy(x => x.EmailAddress == emailAddress).FirstOrDefault();
        }

        public LoginResultModel Login(LoginServiceRequestModel model)
        {
            LoginResultModel result;

            try
            {

                ValidateLoginModel(model, out AppUser user, out AppUserRole userRole);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.StringRecordId),
                    new Claim(ClaimTypes.Role, userRole.RoleName),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                    IsPersistent = true,
                    RedirectUri = model.ReturnUrl
                };

                model.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties)
                    .Wait();
            }
            catch (Exception ex)
            {
                result = new LoginResultModel
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    ReturnUrl = model.ReturnUrl,
                };

                return result;
            }

            result = new LoginResultModel { IsSuccess = true, ReturnUrl = model.ReturnUrl };
            return result;
        }

        public void BatchLogin(HttpContext HttpContext)
        {
            try
            {
                var batchUser = AppServiceProvider.Instance.Get<IAppUserService>().GetByUserName("BATCH_USER");
                if (batchUser == null)
                {
                    throw new AppException(ReturnMessages.USER_NOT_FOUND);
                }

                var batchUserRole = AppServiceProvider.Instance.Get<IAppUserRoleService>().GetById(batchUser.RoleId.ToString());
                if (batchUserRole == null)
                {
                    throw new AppException(ReturnMessages.USER_ROLE_NOT_FOUND);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, batchUser.Username),
                    new Claim(ClaimTypes.NameIdentifier, batchUser.StringRecordId),
                    new Claim(ClaimTypes.Role, batchUserRole.RoleName),
                };

                var userIdentity = new ClaimsIdentity(claims, "BATCH_USER_AUTH");
                HttpContext.User = new ClaimsPrincipal(userIdentity);
            }
            catch (Exception ex)
            {
                new AppException(ReturnMessages.BATCH_LOGIN_ERROR, ex.Message);
            }

        }


        public LoginResultModel TokenBasedLogin(LoginServiceRequestModel loginModel)
        {
            LoginResultModel result = new();

            try
            {
                if (!ConfigurationCache.Instance.TryGet("JwtSecret", out string JwtSecret))
                {
                    throw new AppException(ReturnMessages.JWT_SECRET_KEY_NOT_FOUND_IN_CONFIGURATION);
                }

                ValidateLoginModel(loginModel, out AppUser user, out AppUserRole userRole);

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(JwtSecret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.StringRecordId),
                    new Claim(ClaimTypes.Role, userRole.RoleName)
                    }),
                    Expires = DateTime.UtcNow.AddDays(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                result.Token = tokenHandler.WriteToken(token);
                user.Password = string.Empty;
                result.AppUser = user;
                result.AppUserRole = userRole;
                result.IsSuccess = true;
                result.ExpireDate = tokenDescriptor.Expires.Value;
                result.ReturnUrl = loginModel.ReturnUrl;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result = new LoginResultModel
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    ReturnUrl = loginModel.ReturnUrl,
                };

                return result;
            }

            return result;
        }

        private void ValidateLoginModel(LoginServiceRequestModel loginModel, out AppUser user, out AppUserRole userRole)
        {
            if (string.IsNullOrWhiteSpace(loginModel.Username) || string.IsNullOrWhiteSpace(loginModel.Password))
            {
                throw new AppException(ReturnMessages.INVALID_PARAMETER, "LoginRequestModel");
            }

            user = GetByUserName(loginModel?.Username);

            if (user == null)
            {
                throw new AppException(ReturnMessages.USER_NOT_FOUND);
            }

            if (loginModel.Password.GenerateSHA256Hash().ToLower() != user.Password.ToLower())
            {
                throw new AppException(ReturnMessages.INVALID_USERNAME_OR_PASSWORD);
            }

            userRole = AppServiceProvider.Instance.Get<IAppUserRoleService>().GetById(user.RoleId.ToString());
            if (userRole == null)
            {
                throw new AppException(ReturnMessages.USER_ROLE_NOT_FOUND);
            }

            string userRoleName = userRole.RoleName;
            if (loginModel.LoginAllowsRoles != null && !loginModel.LoginAllowsRoles.Any(x => x == userRoleName))
            {
                throw new AppException(ReturnMessages.USER_ROLE_IS_UNAUTHORIZED);
            }
        }

        private void ValidateRegisterModel(RegisterServiceRequestModel registerModel, out AppUserRole userRole)
        {
            if (string.IsNullOrWhiteSpace(registerModel.Username) || string.IsNullOrWhiteSpace(registerModel.Password) || string.IsNullOrWhiteSpace(registerModel.EmailAddress))
            {
                throw new AppException(ReturnMessages.INVALID_PARAMETER, "RegisterServiceRequestModel");
            }

            AppUser? user = GetByUserName(registerModel.Username);

            if (user != null)
            {
                throw new AppException(ReturnMessages.USERNAME_ALREADY_EXISTS);
            }

            user = GetByEmailAddress(registerModel.EmailAddress);


            if (user != null)
            {
                throw new AppException(ReturnMessages.EMAILADDRESS_ALREADY_EXISTS);
            }

            userRole = AppServiceProvider.Instance.Get<IAppUserRoleService>().GetByRoleName(BaseUserRoles.Kullanici.Value) ?? throw new AppException(ReturnMessages.USER_ROLE_NOT_FOUND);
        }


        public RegisterResultModel Register(RegisterServiceRequestModel model)
        {
            RegisterResultModel result;

            try
            {

                ValidateRegisterModel(model, out AppUserRole userRole);

                AppUser newUser = new AppUser()
                {
                    Username = model.Username,
                    EmailAddress = model.EmailAddress,
                    Password = model.Password.GenerateSHA256Hash(),
                    RoleId = userRole.RecordId,
                    Preferences = SetDefaultPreferences()
                };


                newUser = this.Create(newUser);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, newUser.Username),
                    new Claim(ClaimTypes.NameIdentifier, newUser.StringRecordId),
                    new Claim(ClaimTypes.Role, userRole.RoleName),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                    IsPersistent = true,
                };

                model.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties)
                    .Wait();
            }
            catch (Exception ex)
            {
                result = new RegisterResultModel
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };

                return result;
            }

            result = new RegisterResultModel { IsSuccess = true };
            return result;
        }

        private Dictionary<PreferencesType, object> SetDefaultPreferences()
        {
            return new Dictionary<PreferencesType, object>();
        }

        public List<GetEventsResponseModel> GetCreditCardEvents(string authenticatedUserId, DateTime date)
        {
            List<GetEventsResponseModel> result = new List<GetEventsResponseModel>();
            var user = this.GetById(authenticatedUserId);
            foreach (var creditCard in user.CreditCards)
            {
                result.Add(new GetEventsResponseModel
                {
                    EventType = EventType.CreditCardStatement,
                    StartDate = new DateTime(date.Year, date.Month, creditCard.StatementDay),
                    EndDate = new DateTime(date.Year, date.Month, creditCard.StatementDay),
                    AllDay = true,
                    EventData = creditCard,
                    Title = $"{creditCard.BankName} - {creditCard.MaskedCardNumber} nolu kredi kartýnýn ekstre kesim tarihi."
                });

                result.Add(new GetEventsResponseModel
                {
                    EventType = EventType.CreditCardStatementLastPayment,
                    StartDate = new DateTime(date.Year, date.Month, creditCard.LastPaymentDay),
                    EndDate = new DateTime(date.Year, date.Month, creditCard.LastPaymentDay),
                    AllDay = true,
                    EventData = creditCard,
                    Title = $"{creditCard.BankName} - {creditCard.MaskedCardNumber} nolu kredi kartýnýn ekstre son ödeme tarihi."
                });
            }

            return result;
        }
    }
}
