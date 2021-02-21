using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Task_1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder => loggingBuilder
                .AddSerilog(
                    new LoggerConfiguration()
                        .WriteTo.Console()
                        .WriteTo.File("app.log")
                        .CreateLogger()
                ));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var logger = context.RequestServices.GetService<ILogger<Startup>>();
                    logger.LogInformation("Main page is got");
                    await context.Response.WriteAsync("\"Prime numbers\" by st. Dovhal Vladyslav");
                });
                endpoints.MapGet("/primes/{number:int}", async context =>
                {
                    var number = int.Parse((string)context.Request.RouteValues["number"]);
                    var logger = context.RequestServices.GetService<ILogger<Startup>>();
                    logger.LogInformation("The number is checked for simplicity");
                    if (number > 1 && await Helper.IsPrime(number))
                    {
                        logger.LogInformation($"The number {number} is prime");
                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                    }
                    else
                    {
                        logger.LogInformation($"The number {number} is not prime");
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    }
                });
                endpoints.MapGet("/primes", async context =>
                {
                    var logger = context.RequestServices.GetService<ILogger<Startup>>();
                    logger.LogInformation("The range is checked for simplicity");
                    if (!context.Request.Query.ContainsKey("from") ||
                            !context.Request.Query.ContainsKey("to") ||
                             !int.TryParse(context.Request.Query["from"].FirstOrDefault(), out var primesFrom) ||
                             !int.TryParse(context.Request.Query["to"].FirstOrDefault(), out var primesTo)
                    )
                    {
                        logger.LogInformation("Not enough correct parameters");
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return;
                    }

                    var json = JsonSerializer.Serialize(await Helper.PrimeRange(primesFrom, primesTo));
                    logger.LogInformation("Search for prime numbers in the range is over");
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    await context.Response.WriteAsync(json);

                });
            });

        }
    }
}
