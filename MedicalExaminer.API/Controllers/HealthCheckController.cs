﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MedicalExaminer.API.Controllers
{
    /// <summary>
    /// Health Check Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("/v{api-version:apiVersion}/health_check")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        /// <summary>
        /// Endpoint to health check the API
        /// </summary>
        /// <returns> Ok </returns>
        [HttpGet]
        public async Task<ActionResult> Check()
        {
            return Ok();
        }
    }
}
