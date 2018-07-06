using Microsoft.AspNetCore.Mvc.Filters;
using StagingWizard.DataLayer;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace StagingWizard.Attributes
{
    public class AuthorizationAttribute : ActionFilterAttribute, IActionFilter
    {
        private IConfiguration Configuration { get; } 
        public UserRepository userRepository;

        public AuthorizationAttribute()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            userRepository = new UserRepository(GetConnectionString());
        }

        private string GetConnectionString()
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            return connectionString;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            string token = context.HttpContext.Request.Headers["token"];

            if (userRepository.CheckToken(token))
                context.HttpContext.Response.StatusCode = 200;
            else
                context.HttpContext.Response.StatusCode = 401;
        }
    }
}
