using System;
using System.Collections.Generic;

namespace CentRent.Models
{
    public partial class Car
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public string? Type { get; set; }
        public string? Registration { get; set; }
        public long IsRented { get; set; }
    }
}
