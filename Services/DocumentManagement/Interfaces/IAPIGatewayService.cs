using DocumentManagement.Models;

namespace DocumentManagement.Interfaces
{
    public interface IApiGatewayService
    {
        Task<ProposalLetter> GetPLbyAPIGateway(int plId);
    }
}