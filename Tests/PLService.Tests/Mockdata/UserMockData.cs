using TMS.Models;

namespace PLservice.Tests.Mockdata
{
    public static class UserMockData
    {
        public static List<User> GetUsers()
        {
            return new List<User>(){
                new User{
                Id = 1,
                Name="John",
                Address= "Chennai,India",
                Pan= "ASDDF1234DS",
                Password="1234",
                RoleId= 2,
                Email="Johnmichael@gmail.com",
                FullName= "john michael",
                Disable=false,
                CreatedOn= DateTime.Now,
                CreatedBy= 5,
                UpdatedOn= DateTime.Now,
                UpdatedBy= 5
                },
                new User{
                Id = 2,
                Name="steve",
                Address= "Chennai,India",
                Pan= "ASDDF1234DS",
                Password="1234",
                RoleId= 2,
                Email="Johnmichael@gmail.com",
                FullName= "steve michael",
                Disable=false,
                CreatedOn= DateTime.Now,
                CreatedBy= 5,
                UpdatedOn= DateTime.Now,
                UpdatedBy= 5
                }
            };
        }
        public static User GetSingleUser()
        {
            return new User
            {
                Id = 1,
                Name = "John",
                Address = "Chennai,India",
                Pan = "ASDDF1234DS",
                Password = "1234",
                RoleId = 2,
                Email = "Johnmichael@gmail.com",
                FullName = "john michael",
                Disable = false,
                CreatedOn = DateTime.Now,
                CreatedBy = 5,
                UpdatedOn = DateTime.Now,
                UpdatedBy = 5
            };
        }
    }
}