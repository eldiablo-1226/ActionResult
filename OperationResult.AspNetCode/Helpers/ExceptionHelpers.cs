namespace OperationResult.AspNetCode.Helpers
{
    using System.Text;
    using System;
    
    public static class ExceptionHelpers
    {
        public static string GetMessages(Exception? exception) => exception == null ? "Exception is NULL" : GetErrorMessage(exception);

        private static string GetErrorMessage(Exception exception)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat(exception.Message);
            
            if (exception.InnerException != null)
                stringBuilder.AppendLine(GetErrorMessage(exception.InnerException));
            
            return stringBuilder.ToString();
        }
    }
}