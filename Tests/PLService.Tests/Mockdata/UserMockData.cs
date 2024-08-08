using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;

namespace PLservice.Tests.Mockdata
{
    public static class UserMockData
    {
        public static List<UserTable> GetUsers()
        {
            return new List<UserTable>(){
                new UserTable{
                Id = 1,
                Name="John",
                Address= "Chennai,India",
                Pan= "ASDDF1234DS",
                Password="1234",
                RoleId= 2,
                Email="Johnmichael@gmail.com",
                FullName= "john michael",
                CreatedOn= DateTime.Now,
                CreatedBy= 5,
                UpdatedOn= DateTime.Now,
                UpdatedBy= 5
                },
                new UserTable{
                Id = 2,
                Name="steve",
                Address= "Chennai,India",
                Pan= "ASDDF1234DS",
                Password="1234",
                RoleId= 2,
                Email="Johnmichael@gmail.com",
                FullName= "steve michael",
                CreatedOn= DateTime.Now,
                CreatedBy= 5,
                UpdatedOn= DateTime.Now,
                UpdatedBy= 5
                }
            };
        }
        public static UserTable GetSingleUser()
        {
            return new UserTable
            {
                Id = 1,
                Name = "John",
                Address = "Chennai,India",
                Pan = "ASDDF1234DS",
                Password = "1234",
                RoleId = 2,
                Email = "Johnmichael@gmail.com",
                FullName = "john michael",
                CreatedOn = DateTime.Now,
                CreatedBy = 5,
                UpdatedOn = DateTime.Now,
                UpdatedBy = 5
            };
        }
    }
}