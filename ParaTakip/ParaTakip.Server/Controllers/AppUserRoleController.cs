using ParaTakip.Business.Interfaces;
using ParaTakip.Core;
using ParaTakip.Entities;
using ParaTakip.Model.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ParaTakip.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class AppUserRoleController : ParaTakipController
    {

      [HttpGet]
        public ActionResult<List<AppUserRole>> Get()
        {
            try
            {
                return Ok(AppServiceProvider.Instance.Get<IAppUserRoleService>().GetAll());
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
        public ActionResult<AppUserRole> Add(AddAppUserRoleRequestModel model)
        {
            try
            {
                CheckModelState(model);

                AppUserRole addModel = new AppUserRole
                {
                    //Fill Here
                };

                return Ok(AppServiceProvider.Instance.Get<IAppUserRoleService>().Create(addModel));
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
        public ActionResult<AppUserRole> Update(UpdateAppUserRoleRequestModel model)
        {
            try
            {
                CheckModelState(model);

                var updateModel = AppServiceProvider.Instance.Get<IAppUserRoleService>().GetById(model.Id);
                if (updateModel != null)
                {
                    //Fill Here
                    AppServiceProvider.Instance.Get<IAppUserRoleService>().Update(updateModel);
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
        public ActionResult<AppUserRole> Delete([FromQuery]string Id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Id))
                {
                    throw new AppException(ReturnMessages.INVALID_PARAMETER, Id, "Id");
                }

                var deleteModel = AppServiceProvider.Instance.Get<IAppUserRoleService>().GetById(Id);
                if (deleteModel != null)
                {
                    AppServiceProvider.Instance.Get<IAppUserRoleService>().DeleteById(Id);
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