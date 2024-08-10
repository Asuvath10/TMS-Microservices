using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using APIGateway.Interfaces;
using APIGateway.Models;

namespace APIGateway.Controllers
{
    [Route("[controller]")]
    public class ProposalLetterController : Controller
    {
        private readonly IProposalLetterManagement _service;

        public ProposalLetterController(IProposalLetterManagement service)
        {
            _service = service;
        }

        // Get: ProposalLetters
        [HttpGet]
        public async Task<IActionResult> GetAllProposalLetters()
        {
            var proposalLetters = await _service.GetAllProposals();
            return Ok(proposalLetters);
        }

        // Get: PL Statuses 
        [HttpGet("PLStatuses")]
        public async Task<IActionResult> GetAllStatuses()
        {
            var statuses = await _service.GetAllStatuses();
            return Ok(statuses);
        }

        // GET: ProposalLetter/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProposalLetterById(int id)
        {
            var proposalLetter = await _service.GetProposalLetterById(id);
            if (proposalLetter == null)
            {
                return NotFound();
            }
            return Ok(proposalLetter);
        }

        // POST: ProposalLetter
        [HttpPost]
        public async Task<IActionResult> CreateProposalLetter([FromBody] ProposalLetter proposalLetter)
        {
            if (proposalLetter == null)
            {
                return BadRequest("Proposal letter data is null.");
            }

            var createdProposalLetter = await _service.CreateProposalLetter(proposalLetter);
            return CreatedAtAction(nameof(GetProposalLetterById), new { id = createdProposalLetter.Id }, createdProposalLetter);
        }

        // PUT: ProposalLetter/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProposalLetter(int id, [FromBody] ProposalLetter proposalLetter)
        {
            if (proposalLetter == null || id != proposalLetter.Id)
            {
                return BadRequest("Invalid proposal letter data.");
            }

            var updatedProposalLetter = await _service.UpdateProposalLetter(proposalLetter);
            if (updatedProposalLetter == null)
            {
                return NotFound();
            }

            return Ok("Proposal letter updated successfully.");
        }

        // DELETE: ProposalLetter/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProposalLetter(int id)
        {
            var result = await _service.DeleteProposalLetter(id);
            if (!result)
            {
                return NotFound();
            }

            return Ok("Proposal letter deleted successfully.");
        }
    }
}