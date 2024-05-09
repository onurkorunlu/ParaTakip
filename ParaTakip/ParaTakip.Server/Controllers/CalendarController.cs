using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParaTakip.Business.Interfaces;
using ParaTakip.Core;
using ParaTakip.Model.ResponseModel;

namespace ParaTakip.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class CalendarController : ParaTakipController
    {
        [HttpGet]
        public IActionResult GetEvents(string yearAndMonth)
        {
            DateTime dateTime = new DateTime(int.Parse(yearAndMonth.Split("-")[0]), int.Parse(yearAndMonth.Split("-")[1]), 1);

            List<GetEventsResponseModel> events =
            [
                .. AppServiceProvider.Instance.Get<IAppUserService>().GetCreditCardEvents(this.AuthenticatedUserId, dateTime),
            ];

            return Ok(events);
        }
    }
}
