using PLManagement.Models;
using System.Collections.Generic;

namespace PLManagement.Interfaces;

public interface IPLRepository
{
    IEnumerable<ProposalLetter> GetAllPL();
}
