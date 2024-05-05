using Microsoft.AspNetCore.Mvc;
using ParaTakip.Business.Caches;
using ParaTakip.Core;
using ParaTakip.Model.Api;

namespace ParaTakip.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StockTradingController : ParaTakipController
    {
        [HttpGet]
        public ActionResult<Dictionary<string, StockInfo>> Get()
        {
            try
            {
                return Ok(StockInfoCache.Instance.Values);
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
        public ActionResult<StockInfo> GetStockValue(string stockCode)
        {
            try
            {
                return Ok(StockInfoCache.Instance.GetCacheValue(stockCode));
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
                StockInfoCache.Instance.Reset();
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
