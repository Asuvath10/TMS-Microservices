using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PLManagement.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PLManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class PLController : ControllerBase
    {
        private readonly IPLService _service;
        public PLController(IPLService service)
        {
            _service = service;
        }

        // GET: api/<PLController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.GetAllPLservice());
        }

        // GET api/<PLController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PLController>
        [HttpPost]
        public string Post([FromBody] string value)
        {
            return "Created";
        }

        // PUT api/<PLController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PLController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
