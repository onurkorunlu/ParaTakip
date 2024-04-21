using ParaTakip.Business.Interfaces;
using ParaTakip.Core;
using ParaTakip.Entities;
using ParaTakip.Model.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ParaTakip.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class EmailTemplateController : ParaTakipController
    {

        [HttpGet]
        public ActionResult<List<EmailTemplate>> Get()
        {
            try
            {
                return Ok(AppServiceProvider.Instance.Get<IEmailTemplateService>().GetAll());
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
        public ActionResult<EmailTemplate> Add(AddEmailTemplateRequestModel model)
        {
            try
            {
                CheckModelState(model);

                EmailTemplate addModel = new EmailTemplate
                {
                    //Fill Here
                };

                return Ok(AppServiceProvider.Instance.Get<IEmailTemplateService>().Create(addModel));
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
        public ActionResult<EmailTemplate> Update(UpdateEmailTemplateRequestModel model)
        {
            try
            {
                CheckModelState(model);

                var updateModel = AppServiceProvider.Instance.Get<IEmailTemplateService>().GetById(model.Id);
                if (updateModel != null)
                {
                    //Fill Here
                    AppServiceProvider.Instance.Get<IEmailTemplateService>().Update(updateModel);
                }
                else
                {
                    throw new AppException(ReturnMessages.ITEM_NOT_FOUND, model);
                }

                return Ok(updateModel);
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
        public ActionResult<EmailTemplate> Delete([FromQuery] string Id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Id))
                {
                    throw new AppException(ReturnMessages.INVALID_PARAMETER, Id, "Id");
                }

                var deleteModel = AppServiceProvider.Instance.Get<IEmailTemplateService>().GetById(Id);
                if (deleteModel != null)
                {
                    AppServiceProvider.Instance.Get<IEmailTemplateService>().DeleteById(Id);
                }
                else
                {
                    throw new AppException(ReturnMessages.ITEM_NOT_FOUND, Id);
                }

                return Ok(deleteModel);
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