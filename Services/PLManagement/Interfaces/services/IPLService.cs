using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models;
namespace PLManagement.Interfaces.services
{
    public interface IPLService
    {
        Task<IEnumerable<ProposalLetter>> GetAllPLservice();
        Task<IEnumerable<ProposalLetter>> GetAllPLsByUserId(int userId);
        Task<ProposalLetter> GetPLById(int id);
        Task<int> CreateProposalLetter(ProposalLetter proposalLetter);
        Task<ProposalLetter> UpdateProposalLetter(ProposalLetter proposalLetter);
        Task<bool> DeleteProposalLetter(int id);
        Task<ProposalLetter> AddSignatureAsync(int proposalLetterId, Byte[] signature);
        Task<ProposalLetter> AddPdf(int proposalLetterId);
        Task<IEnumerable<ProposalLetter>> GetAllPLsByStatusId(int statusId);
        Task<IEnumerable<ProposalLetter>> GetAllPLsByPreparerId(int preparerId);
        Task<IEnumerable<ProposalLetter>> GetAllPLsByReviewerId(int reviewerId);
        Task<IEnumerable<ProposalLetter>> GetAllPLsByApproverId(int approverId);
    }
}