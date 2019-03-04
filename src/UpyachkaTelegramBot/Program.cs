using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace UpyachkaTelegramBot
{
    class Program
    {
        static ITelegramBotClient botClient;

        static void Main(string[] args)
        {
            // botClient = new TelegramBotClient("759867650:AAEUUhkBknllkxGMj4ZFpRJT36Gh3AVhE3A");

            // botClient.OnMessage += (sender, e) =>
            // {
            //     botClient.SendPhotoAsync(
            //         chatId: e.Message.Chat,
            //         photo: "https://upyachka.io/images/11/1549955910_2fb7546a87829d11ab59c1ee5239eb8c_20190227152408.jpg",
            //         caption: "Че"
            //         ).GetAwaiter().GetResult();
            // };

            // botClient.StartReceiving();

            var host = new WebHostBuilder()
                .UseKestrel()
                .Configure(app => app.Map("/echo", EchoHandler))
                .Build();

            host.Run();
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
