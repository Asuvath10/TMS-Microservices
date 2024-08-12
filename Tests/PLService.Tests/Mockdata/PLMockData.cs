using System;
using PLManagement.Models;

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
                    Content="This is Proposal Letter",
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
                    Content="This is Proposal Letters",
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
                Content = "Single Proposal Letter",
                CreatedOn = DateTime.Now,
                CreatedBy = 20,
                PreparerId = 21,
                ReviewerId = 22,
                ApproverId = 23
            };
        }
    }
}