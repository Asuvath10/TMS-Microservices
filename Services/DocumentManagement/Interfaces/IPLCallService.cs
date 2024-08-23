using TMS.Models;

namespace DocumentManagement.Interfaces
{
    public interface IPLCallService
    {
        Task<ProposalLetter> GetPLbyAPIGateway(int plId);
        Task<List<Form>> GetallFormsByPLId(int PLid);
    }
}