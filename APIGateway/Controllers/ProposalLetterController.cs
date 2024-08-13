using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using APIGateway.Interfaces;
using APIGateway.Models;
using Microsoft.AspNetCore.Authorization;

namespace APIGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProposalLetterController : ControllerBase
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

            int createdProposalLetterId = await _service.CreateProposalLetter(proposalLetter);
            return Ok(createdProposalLetterId);
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

        // POST: ProposalLetter/{PLid}/signature
        [HttpPut("{PLid}/signature")]
        public async Task<IActionResult> AddSignature(int PLid, [FromBody] byte[] signature)
        {
            try
            {
                var proposalLetter = await _service.AddSignatureAsync(PLid, signature);
                return Ok(proposalLetter);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: ProposalLetter/{PLid}/generate-pdf
        [HttpPut("{PLid}/generate-pdfurl")]
        public async Task<IActionResult> GeneratePdf(int PLid)
        {
            try
            {
                var proposalLetter = await _service.GeneratePdf(PLid);
                return Ok(proposalLetter);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}