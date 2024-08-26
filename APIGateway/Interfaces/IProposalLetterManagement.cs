using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models;

namespace APIGateway.Interfaces
{
    public interface IProposalLetterManagement
    {
        Task<List<ProposalLetter>> GetAllProposals();
        Task<List<Plstatus>> GetAllStatuses();
        Task<Plstatus> GetPLStatusById(int id);
        Task<ProposalLetter> GetProposalLetterById(int id);
        Task<List<ProposalLetter>> GetProposalLettersByUserId(int userid);
        Task<List<ProposalLetter>> GetProposalLettersByStatusId(int statusid);
        Task<List<ProposalLetter>> GetProposalLettersByPreparerId(int preparerId);
        Task<List<ProposalLetter>> GetProposalLettersByReviewerId(int reviewerId);
        Task<List<ProposalLetter>> GetProposalLettersByApproverId(int approverId);
        Task<int> CreateProposalLetter(ProposalLetter proposalLetter);
        Task<ProposalLetter> UpdateProposalLetter(ProposalLetter proposalLetter);
        Task<bool> DeleteProposalLetter(int id);
        Task<ProposalLetter> GeneratePdf(int proposalLetterId);
        Task<ProposalLetter> AddSignatureAsync(int proposalLetterId, byte[] signature);
        Task<List<Form>> GetAllForms();
        Task<List<Form>> GetallFormsByPLId(int PLid);
        Task<Form> GetFormById(int id);
        Task<int> CreateForm(Form form);
        Task<int> UpdateForm(Form form);
        Task<bool> DeleteForm(int id);
    }
}