using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using APIGateway.Interfaces;

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

        [HttpGet]
        public async Task<IActionResult> GetAllProposalLetters()
        {
            var proposalLetters = await _service.GetAllProposals();
            return Ok(proposalLetters);
        }
    }
}