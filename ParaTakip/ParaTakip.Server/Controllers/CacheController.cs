using Amazon.Runtime.Internal.Util;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ParaTakip.Business.Caches;
using ParaTakip.Core;
using System.Collections;
using System.Reflection;
using static ParaTakip.Entities.ExchangeRate;

namespace ParaTakip.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CacheController : ParaTakipController
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [HttpGet]
        public ActionResult<List<string>> Get()
        {
            try
            {
                return Ok(AppServiceProvider.Instance.Get<IRegisteredCache>().List());
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
        public ActionResult Reload(string cacheName)
        {
            try
            {
                AppServiceProvider.Instance.Get<IMemoryCache>().Remove(cacheName);
                AppServiceProvider.Instance.Get<IRegisteredCache>().Remove(cacheName);

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
        public ActionResult<List<string>> ReloadAll()
        {
            try
            {
                foreach (var cache in AppServiceProvider.Instance.Get<IRegisteredCache>().List())
                {
                    AppServiceProvider.Instance.Get<IMemoryCache>().Remove(cache);
                    AppServiceProvider.Instance.Get<IRegisteredCache>().Remove(cache);
                }

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
