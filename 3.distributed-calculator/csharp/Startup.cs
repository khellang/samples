// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
// ------------------------------------------------------------

using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using static System.Text.Json.JsonSerializer;

namespace Subtract
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapPost("/subtract", async ctx =>
                {
                    var operands = await DeserializeAsync<Operands>(ctx.Request.Body);

                    var result = decimal.Parse(operands.OperandOne) - decimal.Parse(operands.OperandTwo);

                    ctx.Response.ContentType = "application/json";

                    await ctx.Response.WriteAsync(result.ToString(CultureInfo.InvariantCulture));
                });
            });
        }

        public static void Main(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(x => x.UseStartup<Startup>())
                .Build()
                .Run();
    }

    public class Operands
    {
        public string OperandOne { get; set; }

        public string OperandTwo { get; set; }
    }
}
