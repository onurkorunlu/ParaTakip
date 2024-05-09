using ParaTakip.Business.Interfaces;
using ParaTakip.Core;
using ParaTakip.Entities;
using ParaTakip.Model.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ParaTakip.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class DebtController : ParaTakipController
    {

      [HttpGet]
        public ActionResult<Debt> Get()
        {
            try
            {
                return Ok(AppServiceProvider.Instance.Get<IDebtService>().GetByUserId(this.AuthenticatedUserId));
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
        public ActionResult<Debt> Update([FromBody]List<DebtInfo> model)
        {
            try
            {
                return Ok(AppServiceProvider.Instance.Get<IDebtService>().Update(model, this.AuthenticatedUserId));
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