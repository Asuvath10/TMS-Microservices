using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Interfaces;
using DocumentManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IFirebaseStorageService _storageService;
        private readonly IPDFGenerationService _pdfService;

        public DocumentController(IFirebaseStorageService storageService, IPDFGenerationService pdfService)
        {
            _storageService = storageService;
            _pdfService = pdfService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadDocument(string foldername, Byte[] file, string contentType)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");
                string fileUrl = await _storageService.UploadFileAsync(foldername, file, contentType);
                return Ok(new { Url = fileUrl });
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateDocument([FromBody] ProposalLetter proposalLetter,  string signatureUrl)
        {
            var pdfData = await _pdfService.GeneratePdf(proposalLetter, "abcd", signatureUrl);
            return File(pdfData, "application/pdf", "Document.pdf");
        }
    }
}