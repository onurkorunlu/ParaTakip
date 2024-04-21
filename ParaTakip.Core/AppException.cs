using Newtonsoft.Json;

namespace ParaTakip.Core
{
    public class AppException : Exception
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int ErrorCode { get; set; }

        public AppException(string message, object? model = null) : base(message)
        {
            ErrorCode = message.GetHashCode();
            LogException(this);
        }

        public AppException(string message, params string[] args) : base(string.Format(message, args))
        {
            ErrorCode = message.GetHashCode();
            LogException(this);
        }


        public AppException(string message, object? model = null, params string[] args) : base(string.Format(message, args))
        {
            ErrorCode = message.GetHashCode();
            LogException(this, model);
        }

        public AppException(string message, Exception innerException, object? model = null, params string[] args) : base(string.Format(message, args), innerException)
        {
            ErrorCode = message.GetHashCode();
            LogException(this, model);
        }

        public void LogException(AppException ex, object? model = null)
        {
            if (model != null)
            {
                Log.Error(string.Format("Input : {0}", JsonConvert.SerializeObject(model)));
            }
            Log.Error(ex.Message, ex);
        }
    }
   
}
