using System;
using System.Collections.Generic;

namespace APIGateway.Models
{
    public partial class Role
    {
        public Role()
        {
            UserTables = new HashSet<UserTable>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<UserTable> UserTables { get; set; }
    }
}
