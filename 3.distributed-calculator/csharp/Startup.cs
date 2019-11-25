// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
// ------------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Subtract
{
    public class Startup : App
    {
        public static void Main(string[] args) => Run<Startup>(args);

        protected override void Configure(IEndpointRouteBuilder app)
        {
            app.MapPost("/subtract", async ctx =>
            {
                var operands = await ctx.ReadJsonAsync<Operands>();

                var result = decimal.Parse(operands.OperandOne) - decimal.Parse(operands.OperandTwo);

                await ctx.WriteJsonAsync(result);
            });
        }

        private class Operands
        {
            public string OperandOne { get; set; }

            public string OperandTwo { get; set; }
        }
    }
}
