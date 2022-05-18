using CentRent.Models;

namespace CentRent.Entities
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public int Phone { get; set; }
        public string? Dni { get; set; }
        public int CarRentedId { get; set; }

        // CustomerRequest Constructors
        public Customer(CustomerRequest.CreateRequest customer)
        {
            Name = customer.Name;
            Surname = customer.Surname;
            Email = customer.Email;
            Phone = customer.Phone;
            Dni = customer.Dni;
            CarRentedId = customer.CarRentedId;
        }

        public Customer(CustomerRequest.UpdateRequest customer)
        {
            Id = customer.Id;
            Name = customer.Name;
            Surname = customer.Surname;
            Email = customer.Email;
            Phone = customer.Phone;
            Dni = customer.Dni;
            CarRentedId = customer.CarRentedId;
        }

        public Customer(CustomerRequest.UpdateRequest customer, int carRentedId)
        {
            Id = customer.Id;
            Name = customer.Name;
            Surname = customer.Surname;
            Email = customer.Email;
            Phone = customer.Phone;
            Dni = customer.Dni;
            CarRentedId = (carRentedId != 0) ? carRentedId : 0;
        }

        // CustomerResponse Constructors
        public Customer(CustomerResponse customer)
        {
            Id = customer.Id;
            Name = customer.Name;
            Surname = customer.Surname; 
            Email = customer.Email;
            Phone = customer.Phone;
            Dni = customer.Dni;
            CarRentedId = customer.CarRentedId;
        }

        public Customer(CustomerResponse customer, int carRentedId)
        {
            Id = customer.Id;
            Name = customer.Name;
            Surname = customer.Surname;
            Email = customer.Email;
            Dni = customer.Dni;
            Phone = customer.Phone;
            CarRentedId = carRentedId;
        }

        // Customer Default Constructor
        public Customer() {}
    }
}
