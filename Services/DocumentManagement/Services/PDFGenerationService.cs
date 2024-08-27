using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GemBox.Document;
using DocumentManagement.Interfaces;
using SixLabors.ImageSharp;
using Newtonsoft.Json;
using TMS.Models;

namespace DocumentManagement.Services
{
    public class PdfGenerationService : IPDFGenerationService
    {
        private readonly IFirebaseStorageService _storageService;
        private readonly IPLCallService _PLService;

        public PdfGenerationService(IFirebaseStorageService storageService, IPLCallService PLService)
        {
            _storageService = storageService;
            _PLService = PLService;
        }
        public async Task<byte[]> GeneratePdf(int plId)
        {
            var proposalLetter = await _PLService.GetPLbyAPIGateway(plId);
            List<Form> forms = await _PLService.GetallFormsByPLId(plId);

            // Create a new document.
            var document = new DocumentModel();

            // Add user details and proposal content.
            var section = new Section(document);
            document.Sections.Add(section);

            var header = new HeaderFooter(document, HeaderFooterType.HeaderDefault);
            var headerParagraph = new Paragraph(document);
            headerParagraph.Inlines.Add(new Run(document, "Proposal Letter"));
            headerParagraph.Inlines.Add(new Run(document, "User: UserName"));
            headerParagraph.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));
            headerParagraph.Inlines.Add(new Run(document, $"Assessment Year: {proposalLetter.AssessmentYear}"));

            foreach (var form in forms)
            {
                // Create a paragraph for the form name with bold formatting.
                headerParagraph.Inlines.Add(new Run(document, form.Name)
                {
                    CharacterFormat = new CharacterFormat() { Bold = true }
                });
                headerParagraph.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));
                headerParagraph.Inlines.Add(new Run(document, form.Content));
            }

            header.Blocks.Add(headerParagraph);
            section.HeadersFooters.Add(header);
            //add the approver's signature in the footer if available.
            if (!string.IsNullOrEmpty(proposalLetter.ApproverSignUrl))
            {

                // section.HeadersFooters.Add(
                // new HeaderFooter(document, HeaderFooterType.FooterDefault,
                // new Paragraph(document, "Signature: ")));

                // var signature = await _storageService.DownloadFileAsync(proposalLetter.ApproverSignUrl);
                // using (var imageStream = new MemoryStream(signature))
                // {

                //     var image = new Picture(document, imageStream);
                //     section.HeadersFooters[HeaderFooterType.FooterDefault].Blocks.Add(new Paragraph(document, image));
                // }
            }
            var image = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "sign.png");
            var signatureBytes = await File.ReadAllBytesAsync(image);
            var imageStream = new MemoryStream(signatureBytes);

            var signatureImage = new Picture(document, imageStream);

            // Create a new footer.
            var footer = new HeaderFooter(document, HeaderFooterType.FooterDefault);
            var footerParagraph = new Paragraph(document);
            footerParagraph.Inlines.Add(new Run(document, "Signature: "));
            footerParagraph.Inlines.Add(signatureImage);
            footer.Blocks.Add(footerParagraph);

            // Add the footer to the section.
            section.HeadersFooters.Add(footer);

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
