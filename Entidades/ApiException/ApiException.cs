using System.Globalization;

namespace Entidades.ApiException
{
    public class ApiException : Exception
    {
        public int ErrorCode { get; set; }

        public ApiException() : base() { }

        public ApiException(string message) : base(message) { }

        public ApiException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public ApiException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}