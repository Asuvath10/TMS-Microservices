using System;
using System.Collections.Generic;

namespace APIGateway.Models
{
    public partial class Register
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Pan { get; set; }
    }
}
