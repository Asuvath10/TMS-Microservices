using TMS.Models;

namespace DocumentManagement.Interfaces
{
    public interface IUserCallService
    {
        Task<User> GetUserById(int id);
    }
}