using System;
using System.Collections.Generic;

namespace CentRent.Entities
{
    public partial class Car
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public string? Type { get; set; }
        public string? Registration { get; set; }
        public int IsRented { get; set; }
    }
}
