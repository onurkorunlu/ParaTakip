using ParaTakip.Business.Interfaces;
using ParaTakip.Core;
using ParaTakip.Entities;
using ParaTakip.Model.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParaTakip.Model.ResponseModel;

namespace ParaTakip.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class AppUserController : ParaTakipController
    {
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<LoginResultModel> Login(LoginRequestModel model)
        {
            try
            {
                this.CheckModelState();
                LoginServiceRequestModel requestModel = new LoginServiceRequestModel
                {
                    HttpContext = this.HttpContext,
                    Password = model.Password,
                    Username = model.Username
                };

                var loginResult = AppServiceProvider.Instance.Get<IAppUserService>().TokenBasedLogin(requestModel);

                return Ok(loginResult);
            }
            catch (AppException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                var e = new AppException(ReturnMessages.GENERIC_ERROR, ex);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<RegisterResultModel> Register(RegisterRequestModel model)
        {
            try
            {
                this.CheckModelState();
                RegisterServiceRequestModel requestModel = new RegisterServiceRequestModel
                {
                    HttpContext = this.HttpContext,
                    Password = model.Password,
                    Username = model.Username,
                    EmailAddress = model.EmailAddress
                };

                var registerResult = AppServiceProvider.Instance.Get<IAppUserService>().Register(requestModel);

                return Ok(registerResult);
            }
            catch (AppException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                var e = new AppException(ReturnMessages.GENERIC_ERROR, ex);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<AppUser> Add(AddAppUserRequestModel model)
        {
            try
            {
                CheckModelState(model);

                AppUser addModel = new AppUser
                {
                    //Fill Here
                };

                return Ok(AppServiceProvider.Instance.Get<IAppUserService>().Create(addModel));
            }
            catch (AppException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                var e = new AppException(ReturnMessages.GENERIC_ERROR, ex);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<AppUser> Update(UpdateAppUserRequestModel model)
        {
            try
            {
                CheckModelState(model);

                var updateModel = AppServiceProvider.Instance.Get<IAppUserService>().GetById(model.Id);
                if (updateModel != null)
                {
                    //Fill Here
                    AppServiceProvider.Instance.Get<IAppUserService>().Update(updateModel);
                }
                else
                {
                    throw new AppException(ReturnMessages.ITEM_NOT_FOUND, model);
                }

                return Ok(updateModel);
            }
            catch (AppException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                var e = new AppException(ReturnMessages.GENERIC_ERROR, ex);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public ActionResult<AppUser> Delete([FromQuery] string Id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Id))
                {
                    throw new AppException(ReturnMessages.INVALID_PARAMETER, Id, "Id");
                }

                var deleteModel = AppServiceProvider.Instance.Get<IAppUserService>().GetById(Id);
                if (deleteModel != null)
                {
                    AppServiceProvider.Instance.Get<IAppUserService>().DeleteById(Id);
                }
                else
                {
                    throw new AppException(ReturnMessages.ITEM_NOT_FOUND, Id);
                }

                return Ok(deleteModel);
            }
            catch (AppException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                var e = new AppException(ReturnMessages.GENERIC_ERROR, ex);
                return BadRequest(e.Message);
            }
        }

    }
}