using System;
using System.Collections.Generic;

namespace CentRent.Models
{
    public partial class User
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public long Phone { get; set; }
        public string? Dni { get; set; }
        public long CarRentedId { get; set; }
    }
}
