using ParaTakip.Business.Interfaces;
using ParaTakip.Core;
using ParaTakip.Entities;
using ParaTakip.Model.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using static ParaTakip.Entities.Wealth;
using static ParaTakip.Entities.AppUser;
using ParaTakip.Common;

namespace ParaTakip.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class CreditCardController : ParaTakipController
    {
        ApplicationContext Context;
        public CreditCardController()
        {
            Context = (ApplicationContext)AppServiceProvider.Instance.Get<IApplicationContext>();
        }

        [HttpGet]
        public ActionResult<List<CreditCard>> Get()
        {
            try
            {
                var result = AppServiceProvider.Instance.Get<IAppUserService>().GetById(this.AuthenticatedUserId);
                return Ok(result.CreditCards ?? new List<AppUser.CreditCard>());
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
        public ActionResult<CreditCard> Add(AddCreditCardRequestModel model)
        {
            try
            {
                CheckModelState(model);
                var appUser = AppServiceProvider.Instance.Get<IAppUserService>().GetById(this.AuthenticatedUserId);

                if (appUser.CreditCards.Any(x => x.MaskedCardNumber == model.MaskedCardNumber.ToMaskedCardNumber()))
                {
                    throw new AppException(ReturnMessages.CREDIT_CARD_NUMBER_ALREADY_EXISTS);
                }

                var newCreditCard = new AppUser.CreditCard()
                {
                    MaskedCardNumber = model.MaskedCardNumber.ToMaskedCardNumber(),
                    LastPaymentDay = model.LastPaymentDay,
                    StatementDay = model.StatementDay,
                    RecordCreateDate = DateTime.Now,
                    RecordCreateUsername = GetUserName(Context)
                };

                appUser.CreditCards.Add(newCreditCard);
                AppServiceProvider.Instance.Get<IAppUserService>().Update(appUser);

                return Ok(newCreditCard);
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

        [HttpDelete]
        public ActionResult Delete([FromQuery] string id)
        {
            try
            {
                var appUser = AppServiceProvider.Instance.Get<IAppUserService>().GetById(this.AuthenticatedUserId);
                appUser.CreditCards.RemoveAll(x => x.StringRecordId == id);
                AppServiceProvider.Instance.Get<IAppUserService>().Update(appUser);
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