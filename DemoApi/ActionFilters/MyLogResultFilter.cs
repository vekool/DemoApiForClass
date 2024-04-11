using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace DemoApi.ActionFilters
{
    /// <summary>
    /// MyLogResultFilter - for logging a method
    /// </summary>
    public class MyLogResultFilter : IResultFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            //when the method ends - this code will be executed
           
            Debug.WriteLine(context.ActionDescriptor.DisplayName + " has ended");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            //when the actionmethod starts - this code will be executed
            Debug.WriteLine(context.ActionDescriptor.DisplayName + " Was run");
        }
    }
}
