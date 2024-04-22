using Microsoft.AspNetCore.Mvc;
using ParaTakip.Business.Caches;
using ParaTakip.Core;

namespace ParaTakip.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FundTrading : ParaTakipController
    {
        [HttpGet]
        public ActionResult<Dictionary<string, decimal>> Get()
        {
            try
            {
                return Ok(FundRefundCache.Instance.Values);
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
