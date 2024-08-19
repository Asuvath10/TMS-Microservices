using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GemBox.Document;
using DocumentManagement.Interfaces;
using DocumentManagement.Models;
using SixLabors.ImageSharp;
using Newtonsoft.Json;

namespace DocumentManagement.Services
{
    public class PdfGenerationService : IPDFGenerationService
    {
        private readonly IFirebaseStorageService _storageService;
        private readonly IApiGatewayService _apiGatewayService;
        private readonly HttpClient _httpClient;

        public PdfGenerationService(IFirebaseStorageService storageService, HttpClient httpClient, IApiGatewayService apiGatewayService)
        {
            _storageService = storageService;
            _apiGatewayService = apiGatewayService;
            _httpClient = httpClient;
        }
        public async Task<byte[]> GeneratePdf(int plId)
        {
            var proposalLetter = _apiGatewayService.GetPLbyAPIGateway(plId);

            // Create a new document.
            var document = new DocumentModel();

            // Add user details and proposal content.
            var section = new Section(document);
            document.Sections.Add(section);

            var paragraph = new Paragraph(document);
            section.Blocks.Add(paragraph);

            paragraph.Inlines.Add(new Run(document, $"User: UserName"));
            paragraph.Inlines.Add(new Run(document, $"Asssessment year: {proposalLetter.AssessmentYear}"));
            paragraph.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));
            // paragraph.Inlines.Add(new Run(document, signature, Image));

            if (!string.IsNullOrEmpty(proposalLetter.ApproverSignUrl))
            {
                paragraph.Inlines.Add(new Run(document, $"Signature: "));
                var signature = await _storageService.DownloadFileAsync(proposalLetter.ApproverSignUrl);
                using (var imagestream = new MemoryStream(signature))
                {
                    var image = new Picture(document, imagestream);
                    paragraph.Inlines.Add(image);
                }
            }

            // Save document to a stream.
            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions
            {
                DocumentOpenPassword = "password"
            });

            return stream.ToArray();
        }
    }
}
