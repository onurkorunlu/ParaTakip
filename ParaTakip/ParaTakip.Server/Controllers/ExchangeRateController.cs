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
    [Route("[controller]/[action]")]
    //[Authorize]
    public class ExchangeRateController : ParaTakipController
    {

        [HttpGet]
        public ActionResult<Dictionary<string, CurrencyInfo>> Get()
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
        public ActionResult<Dictionary<string, CurrencyInfo>> Load(DateTime? date=null)
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
    }
}