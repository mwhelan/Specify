using System;
using System.Text;

namespace ApiTemplate.Api.Domain.Utils
{
    public static class ExceptionExtensions
    {
        public static string UnwrapInnerExceptionMessages(this Exception exception)
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(exception.Message))
            {
                sb.AppendLine(exception.Message);
            }

            if (exception.InnerException != null)
            {
                sb = GetInnerExceptionMessage(sb, exception.InnerException);
            }

            return sb.ToString();
        }

        private static StringBuilder GetInnerExceptionMessage(StringBuilder sb, Exception exception)
        {
            if (!string.IsNullOrWhiteSpace(exception.Message))
            {
                sb.AppendLine(exception.Message);
            }

            if (exception.InnerException != null)
            {
                sb = GetInnerExceptionMessage(sb, exception.InnerException);
            }

            return sb;
        }
    }
}