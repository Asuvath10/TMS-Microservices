using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace DocumentManagement.Controllers
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
        public async Task<IActionResult> UploadFile()
        {
            using (var memoryStream = new MemoryStream())
            {
                await Request.Body.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();
                // var fileBytes = MemoryStream.ToArray();
                // var signatured = await File.ReadAllBytesAsync(file);
                if (fileBytes == null || fileBytes.Length == 0)
                    return BadRequest("No file uploaded.");
                string fileUrl = await _storageService.UploadFileAsync("signatures", fileBytes, "image/png");
                return Ok(new { Url = fileUrl });
            }
        }

        [HttpGet("download")]
        public async Task<IActionResult> downloadFile(string fileUrl)
        {
            if (fileUrl == null)
                return BadRequest("No URL is detected for the file path");
            var file = await _storageService.DownloadFileAsync(fileUrl);
            return File(file, "image/png", "image.png");
        }

        [HttpGet("generate")]
        public async Task<IActionResult> GenerateDocument(int plId)
        {
            if (plId == 0)
            {
                return BadRequest("No URL is detected for the file path");
            }
            var pdfData = await _pdfService.GeneratePdf(plId);
            return File(pdfData, "application/pdf", "Document.pdf");
        }
    }
}