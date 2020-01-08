using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW11.Services
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate requestDelegate;

        public AuthMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                string url = context.Request.GetDisplayUrl();
                if (url.ToLower().Contains("user"))
                {
                    string browserType = context.Request.Headers["auth-token"].ToString();
                    if (browserType.ToLower() != "")
                    {
                        await requestDelegate(context);
                    }
                    else
                    {
                        context.Response.StatusCode = 401; //UnAuthorized
                        await context.Response.WriteAsync("Invalid token");
                        return;
                    }
                }
                else
                {
                    await requestDelegate(context);
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = ex.HResult;
                await context.Response.WriteAsync(ex.Message);
                return;
            }
        }
    }
}
