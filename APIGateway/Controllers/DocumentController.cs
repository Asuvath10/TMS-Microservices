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
using System.IO;

namespace APIGateway.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentManagement _service;

        public DocumentController(IDocumentManagement service)
        {
            _service = service;
        }

        // Get: downloadfile
        [HttpGet("downloadFile")]
        public async Task<IActionResult> DownloadFile(string fileUrl)
        {
            var image = await _service.DownloadFile(fileUrl);
            return File(image, "application/image", "sign.png");
        }

        // Get: generatepdf
        [HttpGet("GeneratePdf")]
        public async Task<IActionResult> GeneratePdf(int plId)
        {
            var pdfData = await _service.GeneratePDF(plId);
            return File(pdfData, "application/pdf", "Document.pdf");
        }

        // Upload file
        [HttpPut("UploadFile")]
        public async Task<IActionResult> UploadFile()
        {
            using (var memoryStream = new MemoryStream())
            {
                await Request.Body.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();
                if (fileBytes == null || fileBytes.Length == 0)
                    return BadRequest("No file uploaded.");
                var url = await _service.UploadFile(fileBytes);
                return Ok(url);
            }
        }


    }
}