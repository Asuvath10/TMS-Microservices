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

        [HttpPut("upload")]
        public async Task<IActionResult> UploadFile(string foldername, Byte[] file, string contentType)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");
            string fileUrl = await _storageService.UploadFileAsync(foldername, file, contentType);
            return Ok(new { Url = fileUrl });
        }

        [HttpGet("download")]
        public async Task<IActionResult> downloadFile(string fileUrl)
        {
            if (fileUrl == null)
            return BadRequest("No URL is detected for the file path");
            var file = await _storageService.DownloadFileAsync(fileUrl);
            return Ok(file);
        }

        [HttpGet("generate")]
        public async Task<IActionResult> GenerateDocument(int plId)
        {
            var pdfData = await _pdfService.GeneratePdf(plId);
            return File(pdfData, "application/pdf", "Document.pdf");
        }
    }
}