using CentRent.Entities;

namespace CentRent.Models
{
    public partial class CarTypeResponse
    {
        public int Id { get; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Type { get; set; }
        public string? Image { get; set; }

        // CarResponse Copy Constructor
        public CarTypeResponse (CarType car) {
            Id = car.Id;
            Model = car.Model;
            Brand = car.Brand;
            Type = car.Type;
            Image = car.Image;
        }
    }
}
