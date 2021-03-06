﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MedicalExaminer.API.Models;
using MedicalExaminer.Common.Reporting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace MedicalExaminer.API
{
    public class ReportRUMiddleware
    {
        /// <summary>
        /// Name of the header
        /// </summary>
        private const string ResponseHeader = "X-Report-RUs";

        private const string ResponseTotalHeader = "X-Report-Total-RUs";

        /// <summary>
        /// Handle to the next Middleware in the pipeline
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initialise a new instance of <see cref="ReportRUMiddleware"/>.
        /// </summary>
        /// <param name="next">Next delegate.</param>
        public ReportRUMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke Async called by MVC.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Task.</returns>
        public Task InvokeAsync(HttpContext context)
        {
            var requestChargeService = context.RequestServices.GetRequiredService<RequestChargeService>();

            context.Response.OnStarting(() =>
            {
                var requestChargeSettings = context.RequestServices.GetRequiredService<IOptions<RequestChargeSettings>>();

                if (requestChargeSettings?.Value != null && requestChargeSettings.Value.ShowFullBreakdownInHeader)
                {
                    var i = 0;
                    foreach (var request in requestChargeService.RequestCharges)
                    {
                        var requestString = JsonConvert.SerializeObject(request);

                        context.Response.Headers[$"{ResponseHeader}-{i}"] = requestString;
                        i++;

                        if (i >= 10)
                        {
                            context.Response.Headers[$"{ResponseHeader}-{i}"] = "Too many headers. Stopped.";
                            break;
                        }
                    }
                }

                context.Response.Headers[ResponseTotalHeader] = requestChargeService.RequestCharges.Sum(s => s.Charge).ToString();
                return Task.CompletedTask;
            });

            // Call the next delegate/middleware in the pipeline.
            return _next(context);
        }
    }
}
