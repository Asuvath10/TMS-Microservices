using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIGateway.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
