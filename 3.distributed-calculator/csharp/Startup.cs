// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
// ------------------------------------------------------------

using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using static System.Text.Json.JsonSerializer;

namespace Subtract
{
    public class Startup
    {
        public static void Main(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(x => x.UseStartup<Startup>())
                .Build()
                .Run();

        public void Configure(IApplicationBuilder app) =>
            app.UseRouting()
               .UseEndpoints(x => x.MapPost("/subtract", Subtract));

        private static async Task Subtract(HttpContext context)
        {
            var operands = await DeserializeAsync<Operands>(context.Request.Body);

            var result = decimal.Parse(operands.OperandOne) - decimal.Parse(operands.OperandTwo);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(result.ToString(CultureInfo.InvariantCulture));
        }
    }

    public class Operands
    {
        public string OperandOne { get; set; }

        public string OperandTwo { get; set; }
    }
}
