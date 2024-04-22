using ParaTakip.Business.Interfaces;
using ParaTakip.Core;
using ParaTakip.Entities;
using ParaTakip.Model.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using static ParaTakip.Entities.Wealth;

namespace ParaTakip.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class WealthController : ParaTakipController
    {
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var result = AppServiceProvider.Instance.Get<IWealthService>().GetByUserId(this.AuthenticatedUserId);
                return Ok(result ?? new Wealth());
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
                return Ok(AppServiceProvider.Instance.Get<IWealthService>().Update(model, this.AuthenticatedUserId));
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