// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
// ------------------------------------------------------------

using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;

namespace Subtract
{
    public abstract class App
    {
        protected static void Run<TStartup>(string[] args) where TStartup : class
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(x => x.UseStartup<TStartup>())
                .Build()
                .Run();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting().UseEndpoints(Configure);
        }

        protected abstract void Configure(IEndpointRouteBuilder app);
    }

    public static class BoilerplateExtensions
    {
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
        };

        public static ValueTask<T> ReadJsonAsync<T>(this HttpContext context)
        {
            return JsonSerializer.DeserializeAsync<T>(context.Request.Body, JsonOptions, context.RequestAborted);
        }

        public static Task WriteJsonAsync<T>(this HttpContext context, T value)
        {
            context.Response.ContentType = "application/json";
            return JsonSerializer.SerializeAsync(context.Response.Body, value, JsonOptions, context.RequestAborted);
        }
    }
}
