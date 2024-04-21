using Microsoft.AspNetCore.Mvc;
using ParaTakip.Business.Caches;
using ParaTakip.Core;
using ParaTakip.PythonIntegrator;
using static ParaTakip.Entities.ExchangeRate;

namespace ParaTakip.Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FundTrading : ParaTakipController
    {

        [HttpGet]
        public ActionResult<decimal> GetFundValue(string fundCode)
        {
            try
            {
                return Ok(FundRefundCache.Instance.GetCacheValue(fundCode));
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
