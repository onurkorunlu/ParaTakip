using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ParaTakip.Core
{
    public class ParaTakipController : ControllerBase
    {
        public ParaTakipController()
        {
        }

        public bool IsAuthenticated
        {
            get
            {
                if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
                {
                    return true;
                }
                return false;
            }
        }
        public string AuthenticatedUserId
        {
            get
            {
                if (IsAuthenticated)
                {
                    return User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                }
                return "";
            }
        }
        public string AuthenticatedUserName
        {
            get
            {
                if (IsAuthenticated)
                {
                    return User.Claims.First(x => x.Type == ClaimTypes.Name).Value;
                }
                return "";
            }
        }

        protected void CheckModelState(object? model = null)
        {
            if (!ModelState.IsValid && ModelState.Values.Any())
            {
                var invalidFields = ModelState.Values.Where(x => x.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid).ToList();

                string errors = string.Empty;

                foreach (var key in ModelState.Keys)
                {

                    var field = ModelState[key];
                    if (field.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    {
                        errors += errors != string.Empty ? "</br>" : string.Empty;
                        errors += field.Errors[0].ErrorMessage;
                    }
                }

                throw new AppException(ReturnMessages.MODEL_VALIDATION_ERROR, model, errors);
            }
        }

        protected string GetAuthenticatedUserId()
        {
            if (this.IsAuthenticated)
            {
                return User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            }

            return null;
        }

        #region SuccessResult

        protected JsonResult PrepareSuccessJsonResult(object? data = null, bool showNotification = false)
        {
            return MyJsonResult.Success(data, showNotification);
        }

        protected JsonResult PrepareSuccessJsonResult(string successMessage, object? data = null, bool showNotification = false)
        {
            return MyJsonResult.Success(successMessage, null, showNotification);
        }

        protected JsonResult PrepareSuccessJsonResult(KeyValuePair<string, string> returnMessage, object? data = null, bool showNotification = false)
        {
            return MyJsonResult.Success(returnMessage.Value, data, showNotification);
        }

        #endregion

        #region ErrorResult
        protected JsonResult PrepareErrorJsonResult(string errorMessage, object? data = null, bool showNotification = true)
        {
            return MyJsonResult.Error(errorMessage, data, showNotification);
        }

        protected JsonResult PrepareErrorJsonResult(string returnMessage, string? customMessage, object? data = null)
        {
            return MyJsonResult.Error(returnMessage, data, customMessage, true);
        }

        protected JsonResult PrepareErrorJsonResult(AppException definedException, bool showNotification = false, object? data = null)
        {
            if (definedException.ErrorCode == ReturnMessages.MODEL_VALIDATION_ERROR.GetHashCode())
            {
                return MyJsonResult.Error(definedException.Message, data, string.Empty, showNotification);
            }
            return MyJsonResult.Error(definedException.Message, data, string.Empty, showNotification);
        }

        protected string GetUserName(ApplicationContext Context)
        {
            if (Context != null && Context.Current != null && Context.Current.HttpContext != null && Context.Current.HttpContext.User != null && Context.Current.HttpContext.User.Identity != null && Context.Current.HttpContext.User.Identity.IsAuthenticated)
            {
                return Context.Current.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Name).Value;
            }

            return "BATCH";
        }


        #endregion
    }
}
