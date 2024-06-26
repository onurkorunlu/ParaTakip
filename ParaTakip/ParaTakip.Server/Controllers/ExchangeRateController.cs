using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParaTakip.Business.Caches;
using ParaTakip.Business.Interfaces;
using ParaTakip.Common;
using ParaTakip.Core;
using static ParaTakip.Entities.ExchangeRate;

namespace ParaTakip.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    //[Authorize]
    public class ExchangeRateController : ParaTakipController
    {

        [HttpGet]
        public ActionResult<Dictionary<string, ExchangeRateInfo>> Get()
        {
            try
            {
                return Ok(ExchangeRateCache.Instance.Values);
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

        [HttpGet]
        public ActionResult<Dictionary<string, ExchangeRateInfo>> Load(DateTime? date=null)
        {
            try
            {

                AppServiceProvider.Instance.Get<IExchangeRateService>().Load(date);
                return Ok();
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

        [HttpGet]
        public ActionResult Clear()
        {
            try
            {
                ExchangeRateCache.Instance.Reset();
                return Ok("Cache reloaded.");
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