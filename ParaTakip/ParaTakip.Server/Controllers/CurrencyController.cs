using Microsoft.AspNetCore.Mvc;
using ParaTakip.Business.Caches;
using ParaTakip.Common;
using ParaTakip.Core;

namespace ParaTakip.Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    //[Authorize]
    public class CurrencyController : ParaTakipController
    {

        [HttpGet]
        public ActionResult<Dictionary<string, Currency>> Get()
        {
            try
            {
                return Ok(CurrencyCache.Instance.Values);
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
