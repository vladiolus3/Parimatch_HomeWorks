using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;
using System.Threading.Tasks;

namespace Task_1.Filters
{
#pragma warning disable CS1591 
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var json = JsonSerializer.Serialize(new
            {
                Code = new Random().Next(10, 100),
                context.Exception.Message
            });

            context.Result = new ContentResult
            {
                Content = json
            };

            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
#pragma warning restore CS1591
}
