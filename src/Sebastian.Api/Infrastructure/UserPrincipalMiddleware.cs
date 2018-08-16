using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Sebastian.Api.Infrastructure
{
    public class UserPrincipalMiddleware
    {
        private readonly RequestDelegate _next;

        public UserPrincipalMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine("User Principal Middleware");
            await _next(context);
        }
    }
}