using Microsoft.AspNetCore.Mvc.Filters;
using StagingWizard.DataLayer;
using StagingWizard.UIContracts;

namespace StagingWizard.Attributes
{
    public class AuthorizationAttribute : ActionFilterAttribute, IActionFilter
    {
        public AuthorizationAttribute() { }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            string token = context.HttpContext.Request.Headers["token"];

            UserRepository userRepository = new UserRepository("server=127.0.0.1;userid=postgres;password=1;database=stagingdb;");
            if (userRepository.CheckToken(token))
                context.HttpContext.Response.StatusCode = 200;
            else
                context.HttpContext.Response.StatusCode = 401;
        }
    }
}
