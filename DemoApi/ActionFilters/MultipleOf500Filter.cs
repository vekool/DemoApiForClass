using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoApi.ActionFilters
{
    public class MultipleOf500Filter : Attribute{
        public void OnActionExecuted(ActionExecutedContext context)
        {
          //None
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Form.TryGetValue("Esalary", out var salary) == true)

            {
                int s = int.Parse(salary);
                if (s % 500 != 0)
                {
                    context.Result = new BadRequestObjectResult("Salary must be multiple of 500");
                }

            }
            else
            {
                context.Result = new BadRequestObjectResult("Salary not provided");
            }
        }
    }
}
