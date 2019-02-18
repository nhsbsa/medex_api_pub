﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Medical_Examiner_API.Loggers;



namespace Medical_Examiner_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DJPTestController : BaseController
    {
        
       


        public DJPTestController(IMELogger logger) : base(logger)
        {
        }


        // GET: api/DJPTest
        [HttpGet]
        public override IEnumerable<string> Get()
        {
            return new string[] { "DJPvalue1", "DJPvalue2" };
        }

        // GET: api/DJPTest/5
        [HttpGet("{id}", Name = "Get")]
        public override string Get(int id)
        {
            return "DJP value";
        }

        // POST: api/DJPTest
        [HttpPost]
        public override void Post([FromBody] string value)
        {
        }

        // PUT: api/DJPTest/5
        [HttpPut("{id}")]
        public override void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public override void Delete(int id)
        {
        }
    }
}