using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GemBox.Document;
using PLManagement.Interfaces;
using PLManagement.Interfaces.services;
using PLManagement.Models;


namespace PLManagement.UtilityFunctions
{
    public class PdfGenerationService : IPDFGenerationService
    {
        public PdfGenerationService()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }
        public byte[] GeneratePdf(ProposalLetter proposalLetter, string userName, string password, string signatureUrl = null)
        {
            // Create a new document.
            var document = new DocumentModel();

            // Add user details and proposal content.
            var section = new Section(document);
            document.Sections.Add(section);

            var paragraph = new Paragraph(document);
            section.Blocks.Add(paragraph);

            paragraph.Inlines.Add(new Run(document, $"User: {userName}"));
            paragraph.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));
            paragraph.Inlines.Add(new Run(document, $"Proposal Content: {proposalLetter.Content}"));
            paragraph.Inlines.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));

            // if (!string.IsNullOrEmpty(signatureUrl))
            // {
            //     paragraph.Inlines.Add(new Run(document, $"Signature: "));
            //     var image = new Picture(document, signatureUrl);
            //     image.Width = 100;
            //     image.Height = 50;
            //     paragraph.Inlines.Add(image);
            // }

            // Save document to a stream.
            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions
            {
                DocumentOpenPassword = password
            });

            return stream.ToArray();
        }
    }
}