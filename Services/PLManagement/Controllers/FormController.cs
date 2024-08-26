using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using PLManagement.Interfaces.services;
using System;
using System.Security.Cryptography.Xml;
using TMS.Models;


namespace PLManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IFormService _service;
        public FormController(IFormService service)
        {
            _service = service;
        }

        [HttpGet("Forms")]
        public async Task<ActionResult<IEnumerable<Form>>> GetAllForms()
        {
            var Forms = await _service.GetAllForms();
            return Ok(Forms);
        }

        [HttpGet("GetallFormsByPLId/{PLId}")]
        public async Task<ActionResult<IEnumerable<Form>>> GetAllFormsByPLId(int PLId)
        {
            var Forms = await _service.GetAllFormsByPLId(PLId);
            return Ok(Forms);
        }

        [HttpGet("GetForm/{id}")]
        public async Task<ActionResult<Form>> GetFormById(int id)
        {
            var Form = await _service.GetFormById(id);
            if (Form == null)
            {
                return NotFound();
            }
            return Ok(Form);
        }

        [HttpPost("CreateForm")]
        public async Task<IActionResult> Post([FromBody] Form Form)
        {
            if (Form == null) { return BadRequest("Request is null"); }
            int CreatedFormId = await _service.CreateForm(Form);
            return Ok(CreatedFormId);
        }

        [HttpPut("UpdateForm")]
        public async Task<IActionResult> UpdateForm([FromBody] Form Form)
        {
            if (Form == null)
            {
                return BadRequest();
            }

            int formId = await _service.UpdateForm(Form);
            return Ok(formId);

        }

        [HttpDelete("DeleteForm/{id}")]
        public async Task<IActionResult> DeleteForm(int id)
        {
            var result = await _service.DeleteForm(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok("Form Deleted Successfully");
        }
    }
}
