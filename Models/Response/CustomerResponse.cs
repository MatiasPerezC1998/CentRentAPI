using CentRent.Entities;

namespace CentRent.Models
{
    public partial class CustomerResponse
    {
        public int Id { get; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Dni { get; set; }
        public int Phone { get; set; }
        public int CarRentedId { get; set; }

        public CustomerResponse(Customer customer)
        {
            Id = customer.Id;
            Name = customer.Name;
            Surname = customer.Surname;
            Email = customer.Email;
            Phone = customer.Phone;
            CarRentedId = customer.CarRentedId;
            Dni = customer.Dni;
        }
    }
}
