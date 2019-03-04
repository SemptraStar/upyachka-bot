using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace UpyachkaTelegramBot
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

        }

        public void Configure(IApplicationBuilder app)
        {
            app.Map("/echo", EchoHandler);
        }

        private static void EchoHandler(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                context.Response.ContentType = context.Request.ContentType;

                await context.Response.WriteAsync(
                    JsonConvert.SerializeObject(new
                    {
                        StatusCode = context.Response.StatusCode.ToString(),
                        PathBase = context.Request.PathBase.Value.Trim('/'),
                        Path = context.Request.Path.Value.Trim('/'),
                        Method = context.Request.Method,
                        Scheme = context.Request.Scheme,
                        ContentType = context.Request.ContentType,
                        ContentLength = (long?)context.Request.ContentLength,
                        Content = new StreamReader(context.Request.Body).ReadToEnd(),
                        QueryString = context.Request.QueryString.ToString(),
                        Query = context.Request.Query
                            .ToDictionary(
                                item => item.Key,
                                item => item.Value,
                                StringComparer.OrdinalIgnoreCase)
                    })
                );
            });
        }
    }
}