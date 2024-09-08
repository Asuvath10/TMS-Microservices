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

        [HttpGet("GetallPLsByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<ProposalLetter>>> GetAllProposalLetters(int userId)
        {
            var proposalLetters = await _service.GetAllPLsByUserId(userId);
            return Ok(proposalLetters);
        }
        [HttpGet("GetallPLsByStatusId/{statusId}")]
        public async Task<ActionResult<IEnumerable<ProposalLetter>>> GetAllPLbystatusId(int statusId)
        {
            var proposalLetters = await _service.GetAllPLsByStatusId(statusId);
            return Ok(proposalLetters);
        }
        [HttpGet("GetallPLsByReviewerId/{reviewerId}")]
        public async Task<ActionResult<IEnumerable<ProposalLetter>>> GetAllPLbyReviewerId(int reviewerId)
        {
            var proposalLetters = await _service.GetAllPLsByReviewerId(reviewerId);
            return Ok(proposalLetters);
        }
        [HttpGet("GetallPLsByPreparerId/{preparerId}")]
        public async Task<ActionResult<IEnumerable<ProposalLetter>>> GetAllPLbyPreparerId(int preparerId)
        {
            var proposalLetters = await _service.GetAllPLsByPreparerId(preparerId);
            return Ok(proposalLetters);
        }
        [HttpGet("GetallPLsByApproverId/{approverId}")]
        public async Task<ActionResult<IEnumerable<ProposalLetter>>> GetAllPLbyApproverId(int approverId)
        {
            var proposalLetters = await _service.GetAllPLsByApproverId(approverId);
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
    }
}
