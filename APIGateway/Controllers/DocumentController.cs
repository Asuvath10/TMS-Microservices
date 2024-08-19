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
            var Users = await _service.DownloadFile(fileUrl);
            return Ok(Users);
        }

        // Upload file
        [HttpPut("UploadFile")]
        public async Task<IActionResult> UploadFile(string folderpath, Byte[] file, string contentType)
        {
            var url = await _service.UploadFile(folderpath, file, contentType);
            return Ok(url);
        }
        
    }
}