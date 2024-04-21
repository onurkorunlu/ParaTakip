using Microsoft.AspNetCore.Mvc;

namespace ParaTakip.Core
{
    public class MyJsonResult
    {
        public static JsonResult Success(bool showNotification = false)
        {
            return new JsonResult(new { IsSuccess = true, ErrorCode = ReturnMessages.SUCCESSFUL.GetHashCode(), Message = ReturnMessages.SUCCESSFUL, ShowNotification = showNotification });
        }
        public static JsonResult Success(string? message, object? data = null, bool showNotification = false)
        {
            return new JsonResult(new { IsSuccess = true, ErrorCode = ReturnMessages.SUCCESSFUL.GetHashCode(), Message = string.IsNullOrWhiteSpace(message) ? ReturnMessages.SUCCESSFUL : message, Data = data, ShowNotification = showNotification });
        }
        public static JsonResult Success(object? data = null, bool showNotification = false)
        {
            return new JsonResult(new { IsSuccess = true, ErrorCode = ReturnMessages.SUCCESSFUL.GetHashCode(), Message = ReturnMessages.SUCCESSFUL, Data = data, ShowNotification = showNotification });
        }

        public static JsonResult Error(bool showNotification = false)
        {
            return new JsonResult(new { IsSuccess = false, ErrorCode = ReturnMessages.GENERIC_ERROR.GetHashCode(), Message = ReturnMessages.GENERIC_ERROR, ShowNotification = showNotification });
        }

        public static JsonResult Error(string customMessage, object? data = null, bool showNotification = false)
        {
            return new JsonResult(new { IsSuccess = false, ErrorCode = ReturnMessages.GENERIC_ERROR.GetHashCode(), Message = customMessage, Data = data, ShowNotification = showNotification });
        }

        public static JsonResult Error(string message, object? data = null, string? customMessage = null, bool showNotification = false)
        {
            return new JsonResult(new { IsSuccess = false, ErrorCode = message.GetHashCode(), Message = string.IsNullOrWhiteSpace(customMessage) ? message : customMessage, Data = data, ShowNotification = showNotification });
        }
    }

}
