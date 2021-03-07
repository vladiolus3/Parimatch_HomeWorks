using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using Microsoft.OpenApi.Models;
using Serilog;
using Task_1.Clients;
using Task_1.Filters;
using Task_1.Options;
using Task_1.Services;

namespace Task_1
{
#pragma warning disable 1591
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add logger
            services.AddLogging(loggingBuilder => loggingBuilder
                .AddSerilog(
                    new LoggerConfiguration()
                        .WriteTo.Console()
                        .WriteTo.File("app.log")
                        .CreateLogger()
                ));

            // Add exception filter
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
            });

            // Add options
            services
                .Configure<CacheOptions>(Configuration.GetSection("Cache"))
                .Configure<NbuClientOptions>(Configuration.GetSection("Client"))
                .Configure<RatesOptions>(Configuration.GetSection("Rates"));

            // Add application services
            services.AddScoped<IRatesService, RatesService>();

            // Add NbuClient as Transient
            services.AddHttpClient<IRatesProviderClient, NbuClient>()
                .ConfigureHttpClient(client => client.Timeout = TimeSpan.FromSeconds(10));

            // Add CacheHostedService as Singleton
            services.AddHostedService<CacheHostedService>();

            // Add batch of Swashbuckle Swagger services
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "DI Demo App API", Version = "v1"});
                var filePath = Path.Combine(AppContext.BaseDirectory, "Task_1.xml");
                if (File.Exists(filePath))
                {
                    c.IncludeXmlComments(filePath);
                }
            });

            // Add batch of framework services
            services.AddMemoryCache();
            services.AddControllers();
        }

        // This method gets called by the runtime.
        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DI Demo App API v1"));
            }

            //middleware for logging request`s and response`s body
            app.Use(async (context, next) =>
            {
                var logger = context.RequestServices.GetService<ILogger<Startup>>();
                var recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();

                var body = await ObtainBodyService.ObtainRequestBody(context.Request);
                if (string.IsNullOrEmpty(body))
                    body = "The request`s body is empty.";

                logger.LogInformation("Body:\t" + body);
                var originalBodyStream = context.Response.Body;
                await using var responseBody = recyclableMemoryStreamManager.GetStream();
                context.Response.Body = responseBody;

                await next.Invoke();

                body = await ObtainBodyService.ObtainResponseBody(context);
                if (string.IsNullOrEmpty(body))
                    body = "The response`s body is empty.";

                logger.LogInformation("Body:\t" + body);

                await responseBody.CopyToAsync(originalBodyStream);
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
#pragma warning restore 1591
}
