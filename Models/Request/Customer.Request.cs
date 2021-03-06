namespace CentRent.Models
{
    public partial class CustomerRequest
    {
        public partial class CreateRequest
        {
            public string? Name { get; set; }
            public string? Surname { get; set; }
            public string? Email { get; set; }
            public int Phone { get; set; }
            public string? Dni { get; set; }
            public int CarRentedId { get; set; }
        }

        public partial class UpdateRequest
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Surname { get; set; }
            public string? Email { get; set; }
            public int Phone { get; set; }
            public string? Dni { get; set; }
            public int CarRentedId { get; set; }
        }
    }
}
