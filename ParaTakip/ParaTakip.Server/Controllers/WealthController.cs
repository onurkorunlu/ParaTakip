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
    public class WealthController : ParaTakipController
    {
        [HttpGet]
        public ActionResult<Wealth> Get()
        {
            try
            {
                return Ok(AppServiceProvider.Instance.Get<IWealthService>().GetByUserId(this.AuthenticatedUserId) ?? new Wealth());
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
        public ActionResult<Wealth> Update(UpdateWealthRequestModel model)
        {
            try
            {
                CheckModelState(model);

                var userWealth = AppServiceProvider.Instance.Get<IWealthService>().GetByUserId(this.AuthenticatedUserId);
                if (userWealth == null)
                {
                    throw new AppException(ReturnMessages.WEALTH_NOT_FOUND);
                }
                userWealth.Values[model.WealthType] = model.WealthValues;

                return Ok(AppServiceProvider.Instance.Get<IWealthService>().Update(userWealth));
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