using PLManagement.Models;
using UserManagement.Models;

namespace PLservice.Tests.Mockdata
{
    public static class PLStatusMockData
    {
        public static List<Plstatus> GetPlstatuses()
        {
            return new List<Plstatus>(){
                new Plstatus{
                Id = 1,
                Status="New"
                },
                new Plstatus{
                Id = 2,
                Status="Preparing"
                },
                new Plstatus{
                Id = 3,
                Status="Move to Review"
                },
                new Plstatus{
                Id = 4,
                Status="Pending Approval"
                },
                new Plstatus{
                Id = 5,
                Status="Approved"
                }
            };
        }
    }
}