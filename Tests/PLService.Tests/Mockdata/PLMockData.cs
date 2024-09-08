using System;
using TMS.Models;

namespace PLservice.Tests.MockData
{
    public static class PLMockData
    {
        public static List<ProposalLetter> GetProposalLetters()
        {
            return new List<ProposalLetter>(){
                new ProposalLetter{
                    Id = 1,
                    UserId=12,
                    AssessmentYear = "2023-2024",
                    PlstatusId=1,
                    ApproverSignUrl = null,
                    PdfUrl =null,
                    CreatedOn= DateTime.Now,
                    CreatedBy= 20,
                    PreparerId= 21,
                    ReviewerId = 22,
                    ApproverId = 23
                },
                new ProposalLetter{
                    Id = 2,
                    UserId=13,
                    AssessmentYear = "2024-2025",
                    PlstatusId=1,
                    ApproverSignUrl = null,
                    PdfUrl =null,
                    CreatedOn= DateTime.Now,
                    CreatedBy= 20,
                    PreparerId= 21,
                    ReviewerId = 22,
                    ApproverId = 23
                }
            };
        }

        public static ProposalLetter GetSingleProposalLetter()
        {
            return new ProposalLetter
            {
                Id = 1,
                UserId = 12,
                AssessmentYear = "2023-2024",
                PlstatusId = 1,
                Draft = false,
                ApproverSignUrl = null,
                PdfUrl = null,
                CreatedOn = DateTime.Now,
                CreatedBy = 20,
                PreparerId = 21,
                ReviewerId = 22,
                ApproverId = 23
            };
        }

        public static IEnumerable<Form> GetAllForm()
        {
            return new List<Form>(){
                new Form{
                    Id=1,
                    Name="Form 16",
                    Plid=17,
                    Content= "Form content",
                    CreatedOn=DateTime.UtcNow
                },
                new Form{
                    Id=2,
                    Name="Form 17",
                    Plid=18,
                    Content= "Form content",
                    CreatedOn=DateTime.UtcNow
                }
            };
        }
        public static List<Form> GetallFormsByPLId()
        {
            return new List<Form>(){
                new Form{
                    Id=1,
                    Name="Form 16",
                    Plid=17,
                    Content= "Form content",
                    CreatedOn=DateTime.UtcNow
                },
                new Form{
                    Id=2,
                    Name="Form 17",
                    Plid=17,
                    Content= "Form content",
                    CreatedOn=DateTime.UtcNow
                }
                // new Form{
                //     Id=3,
                //     Name="Form 18",
                //     Plid=17,
                //     Content= "Form content",
                //     CreatedOn=DateTime.UtcNow
                // }
            };
        }
        public static Form GetFormById()
        {
            return new Form
            {
                Id = 2,
                Name = "Form 17",
                Plid = 17,
                Content = "Form content",
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}