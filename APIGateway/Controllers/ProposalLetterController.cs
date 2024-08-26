using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using APIGateway.Interfaces;
using TMS.Models;
using Microsoft.AspNetCore.Authorization;

namespace APIGateway.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProposalLetterController : ControllerBase
    {
        private readonly IProposalLetterManagement _service;

        public ProposalLetterController(IProposalLetterManagement service)
        {
            _service = service;
        }

        // PL Statuses Endpoint
        [HttpGet("PLStatuses")]
        public async Task<IActionResult> GetAllStatuses()
        {
            var statuses = await _service.GetAllStatuses();
            return Ok(statuses);
        }

        [HttpGet("PLStatus/{id}")]
        public async Task<IActionResult> GetPLStatusById(int id)
        {
            var PLStatus = await _service.GetPLStatusById(id);
            if (PLStatus == null)
            {
                return NotFound();
            }
            return Ok(PLStatus);
        }

        //ProposalLetter End points
        [HttpGet]
        public async Task<IActionResult> GetAllProposalLetters()
        {
            var proposalLetters = await _service.GetAllProposals();
            return Ok(proposalLetters);
        }
        [HttpGet("GetallPLByUserId")]
        public async Task<IActionResult> GetAllProposalLettersByUserId(int userid)
        {
            var proposalLetters = await _service.GetProposalLettersByUserId(userid);
            return Ok(proposalLetters);
        }
        [HttpGet("GetallPLByStatusId/{statusid}")]
        public async Task<IActionResult> GetAllProposalLettersByStatusId(int statusid)
        {
            var proposalLetters = await _service.GetProposalLettersByStatusId(statusid);
            return Ok(proposalLetters);
        }
        [HttpGet("GetallPLByReviewerId/{reviewerId}")]
        public async Task<IActionResult> GetAllProposalLettersByReviewerId(int reviewerId)
        {
            var proposalLetters = await _service.GetProposalLettersByReviewerId(reviewerId);
            return Ok(proposalLetters);
        }
        [HttpGet("GetallPLByPreparerId/{preparerId}")]
        public async Task<IActionResult> GetAllProposalLettersByPreparerId(int preparerId)
        {
            var proposalLetters = await _service.GetProposalLettersByPreparerId(preparerId);
            return Ok(proposalLetters);
        }
        [HttpGet("GetallPLByApproverId/{approverId}")]
        public async Task<IActionResult> GetAllProposalLettersByApproverId(int approverId)
        {
            var proposalLetters = await _service.GetProposalLettersByApproverId(approverId);
            return Ok(proposalLetters);
        }

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

            return Ok();
        }

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

        // put: ProposalLetter/{PLid}/generate-pdf
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

        // Forms Endpoint
        [HttpGet("FormByPLId/{PLid}")]
        public async Task<IActionResult> GetAllFormsByPLId(int PLid)
        {
            var proposalLetters = await _service.GetallFormsByPLId(PLid);
            return Ok(proposalLetters);
        }

        [HttpGet("GetallForms")]
        public async Task<IActionResult> GetAllForms()
        {
            var statuses = await _service.GetAllForms();
            return Ok(statuses);
        }

        // GET: form/{id}
        [HttpGet("Form/{id}")]
        public async Task<IActionResult> GetFormById(int id)
        {
            var proposalLetter = await _service.GetFormById(id);
            if (proposalLetter == null)
            {
                return NotFound();
            }
            return Ok(proposalLetter);
        }

        // POST: Form
        [HttpPost("CreateForm")]
        public async Task<IActionResult> CreateForm([FromBody] Form form)
        {
            if (form == null)
            {
                return BadRequest("form data is null.");
            }

            int createdformId = await _service.CreateForm(form);
            return Ok(createdformId);
        }

        // PUT: Form
        [HttpPut("UpdateForm")]
        public async Task<IActionResult> UpdateForm([FromBody] Form form)
        {
            if (form == null)
            {
                return BadRequest("Invalid form data.");
            }

            int formId = await _service.UpdateForm(form);
            return Ok(formId);
        }

        // DELETE: Form/{id}
        [HttpDelete("DeleteForm/{id}")]
        public async Task<IActionResult> DeleteForm(int id)
        {
            var result = await _service.DeleteForm(id);
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

    }
}