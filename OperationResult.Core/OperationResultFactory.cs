using OperationResult.Core.Extensions;

namespace OperationResult.Core
{
    // ReSharper disable MemberCanBePrivate.Global

    public static class OperationResultFactory
    {
        /// <summary>
        /// Returns as factory method OperationResult with result and 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="result"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static OperationResult<TResult> CreateResult<TResult>(
            TResult result, 
            Exception? exception = null)
        {
            var operation = new OperationResult<TResult>
            {
                Result = result,
                Exception = exception
            };
            return operation;
        }

        /// <summary>
        /// Returns as factory method OperationResult
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public static OperationResult<TResult> CreateResult<TResult>() => CreateResult(default(TResult)!);
        
        /// <summary>
        /// Returns Error OperationResult 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public static OperationResult<TResult> CreateResult<TResult>(string message)
        {
            var result = CreateResult<TResult>();
            result.AddError(message);
            return result;
        }
    }
}