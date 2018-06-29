using Npgsql;
using System;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StagingWizard.Attributes
{
    //[AttributeUsage(AttributeTargets.Method)]
    public class AuthorizationAttribute: ActionFilterAttribute, IActionFilter
    {
        public AuthorizationAttribute() { }

        private static string connectionString = "server=127.0.0.1;userid=postgres;password=1;database=stagingdb;";

        private static IDbConnection OpenConnection(string connStr)
        {
            var conn = new NpgsqlConnection(connStr);
            conn.Open();
            return conn;
        }
        
        private static bool CheckToken(string token)
        {
            using (var conn = OpenConnection(connectionString))
            {
                var reader = conn.ExecuteReader("SELECT * FROM users WHERE token = @Token", new { token });
                while (reader.Read())
                {
                    return true;
                }
                return false;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            string token = context.HttpContext.Request.Headers["token"];
            if (CheckToken(token))
                context.HttpContext.Response.StatusCode = 200;
            else
                context.HttpContext.Response.StatusCode = 401;
        }
    }
}
