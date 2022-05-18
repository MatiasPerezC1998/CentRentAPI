using CentRent.Entities;

namespace CentRent.Models
{
    public partial class CarResponse
    {
        public int Id { get; }
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public string? Type { get; set; }
        public string? Registration { get; set; }
        public int IsRented { get; set; }

        // CarResponse Copy Constructor
        public CarResponse (Car car) {
            Id = car.Id;
            Name = car.Name;
            Brand = car.Brand;
            Type = car.Type;
            IsRented = car.IsRented;
            Registration = car.Registration;
        }
    }
}
