using TMS.Models;

namespace PLservice.Tests.Mockdata
{
    public static class RoleMockData
    {
        public static List<Role> GetRoles()
        {
            return new List<Role>(){
                new Role{
                Id = 1,
                RoleName="User"
                },
                new Role{
                    Id= 2,
                    RoleName="Preparer"
                },
                new Role{
                    Id= 3,
                    RoleName="Reviewer"
                },
                new Role{
                    Id= 4,
                    RoleName="Approver"
                }
            };
        }
        public static Role GetSingleRole()
        {
            return new Role
            {
                Id = 1,
                RoleName = "User"
            };
        }
    }
}