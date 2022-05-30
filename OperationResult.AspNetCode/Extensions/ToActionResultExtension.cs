namespace OperationResult.AspNetCode.Extensions
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    // ReSharper disable RedundantCast
    
    public static class ToActionResultExtension
    {
        public static async Task<IActionResult> ToActionResultAsync<T>(this Task<Core.OperationResult<T>> taskResult)
        {
            var result = await taskResult;
            return result.Ok
                ? (IActionResult)new OkObjectResult(result)
                : (IActionResult)new BadRequestObjectResult(result);
        }
        
        public static IActionResult ToActionResult<T>(this Core.OperationResult<T> result)
        {
            return result.Ok
                ? (IActionResult)new OkObjectResult(result)
                : (IActionResult)new BadRequestObjectResult(result);
        }
    }
}