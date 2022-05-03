using System;
using System.Collections.Generic;

namespace CentRent.Models
{
    public partial class Log
    {
        public string Email { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
