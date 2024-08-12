using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using PLManagement.Models;
using PLManagement.Interfaces.services;
using System;
using System.Security.Cryptography.Xml;

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
        public async Task<IActionResult> Post([FromBody] ProposalLetter proposalLetter)
        {
            if (proposalLetter == null) { return BadRequest("Request is null"); }
            int CreatedProposalLetterId = await _service.CreateProposalLetter(proposalLetter);
            return Ok(CreatedProposalLetterId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProposalLetter(int id, [FromBody] ProposalLetter proposalLetter)
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
            return Ok(updatedProposalLetter);

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

        [HttpPut("{PLid}/signature")]
        public async Task<IActionResult> AddSignature(int PLid, Byte[] signature)
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

        [HttpPut("{PLid}/generate-pdfurl")]
        public async Task<IActionResult> GeneratePdf(int PLid)
        {
            try
            {
                var proposalLetter = await _service.AddPdf(PLid);
                return Ok(proposalLetter);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
