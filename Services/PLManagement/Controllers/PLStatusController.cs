using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using PLManagement.Interfaces.services;
using TMS.Models;

namespace PLManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class PLStatusController : ControllerBase
    {
        private readonly IPLStatusService _service;
        public PLStatusController(IPLStatusService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Plstatus>> GetPLStatuses()
        {
            var pLStatuses = await _service.GetPLStatuses();
            if (pLStatuses == null)
            {
                return NotFound();
            }
            return Ok(pLStatuses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Plstatus>> GetPLStatusById(int id)
        {
            var pLStatus = await _service.GetPLStatusById(id);
            if (pLStatus == null)
            {
                return NotFound();
            }
            return Ok(pLStatus);
        }
    }
}
