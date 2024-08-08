using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PLManagement.Interfaces;
using PLManagement.Models;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProposalLetter>>> GetAllProposalLetters()
        {
            var proposalLetters = await _service.GetAllPLservice();
            return Ok(proposalLetters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProposalLetter>> GetProposalLetterById(int id)
        {
            var proposalLetter = await _service.GetPLById(id);
            if (proposalLetter == null)
            {
                return NotFound();
            }
            return Ok(proposalLetter);
        }

        [HttpPost]
        public async Task<ActionResult<ProposalLetter>> Post([FromBody] ProposalLetter proposalLetter)
        {
            if (proposalLetter == null) { return BadRequest("Request is null"); }
            var CreatedProposalLetter = await _service.CreateProposalLetter(proposalLetter);
            return Ok(CreatedProposalLetter);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProposalLetter(int id, ProposalLetter proposalLetter)
        {
            if (id != proposalLetter.Id)
            {
                return BadRequest();
            }

            var updatedProposalLetter = await _service.UpdateProposalLetter(proposalLetter);
            if (updatedProposalLetter == null)
            {
                return NotFound();
            }
            return Ok("Proposal Letter Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProposalLetter(int id)
        {
            var result = await _service.DeleteProposalLetter(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok("Proposal Letter Deleted Successfully");
        }
    }
}
