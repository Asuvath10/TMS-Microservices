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
using SixLabors.ImageSharp.Processing;

namespace DocumentManagement.Services
{
    public class PdfGenerationService : IPDFGenerationService
    {
        private readonly IFirebaseStorageService _storageService;
        private readonly IPLCallService _PLService;
        private readonly IUserCallService _userService;

        public PdfGenerationService(IFirebaseStorageService storageService, IPLCallService PLService, IUserCallService userService)
        {
            _storageService = storageService;
            _PLService = PLService;
            _userService = userService;
        }
        public async Task<byte[]> GeneratePdf(int plId)
        {
            var proposalLetter = await _PLService.GetPLbyAPIGateway(plId);
            List<Form> forms = await _PLService.GetallFormsByPLId(plId);
            var UserDetails = await _userService.GetUserById(proposalLetter.UserId);

            // Create a new document.
            var document = new DocumentModel();

            // Add user details and proposal content.
            var section = new Section(document);
            document.Sections.Add(section);

            var header = new HeaderFooter(document, HeaderFooterType.HeaderDefault);
            var headerParagraph = new Paragraph(document);
            headerParagraph.Inlines.Add(new Run(document, "Proposal Letter")
            {
                CharacterFormat = new CharacterFormat() { Bold = true }
            });
            headerParagraph.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));
            headerParagraph.Inlines.Add(new Run(document, $"UserName: {UserDetails.FullName}"));
            headerParagraph.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));
            headerParagraph.Inlines.Add(new Run(document, $"User-email: {UserDetails.Email}"));
            headerParagraph.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));
            headerParagraph.Inlines.Add(new Run(document, $"Address: {UserDetails.Address}"));
            headerParagraph.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));
            headerParagraph.Inlines.Add(new Run(document, $"Pan Number: {UserDetails.Pan}"));
            headerParagraph.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));
            headerParagraph.Inlines.Add(new Run(document, $"Assessment Year: {proposalLetter.AssessmentYear}"));
            headerParagraph.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));
            header.Blocks.Add(headerParagraph);
            section.HeadersFooters.Add(header);

            foreach (var form in forms)
            {
                var formParagraph = new Paragraph(document);
                // Create a paragraph for the form name with bold formatting.
                formParagraph.Inlines.Add(new Run(document, form.Name)
                {
                    CharacterFormat = new CharacterFormat() { Bold = true }
                });
                formParagraph.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));
                formParagraph.Inlines.Add(new Run(document, form.Content));
                section.Blocks.Add(formParagraph);
            }
            if (proposalLetter.ApproverSignUrl != null)
            {
                var signatureBytes = await _storageService.DownloadFileAsync(proposalLetter.ApproverSignUrl);
                var imageStream = new MemoryStream(signatureBytes);
                var signatureImage = new Picture(document, imageStream);

                // var image = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "sign.png");
                // var signatureBytes = await File.ReadAllBytesAsync(image);
                
                // Create a new footer.
                var footer = new HeaderFooter(document, HeaderFooterType.FooterDefault);
                var footerParagraph = new Paragraph(document);
                footerParagraph.Inlines.Add(new Run(document, "Signature: "));
                footerParagraph.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));
                footerParagraph.Inlines.Add(signatureImage);
                footer.Blocks.Add(footerParagraph);
                // Add the footer to the section.
                section.HeadersFooters.Add(footer);
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
