using CentRent.Entities;

namespace CentRent.Models
{
    public partial class CarResponse
    {
        public int Id { get; }
        public string? Model { get; set; }
        public string? Brand { get; set; }
        public string? Type { get; set; }
        public string? Registration { get; set; }
        public int IsRented { get; set; }
        public string? Image { get; set; }
        public int CarTypeId { get; set; }

        // CarResponse Copy Constructor
        public CarResponse(Car car)
        {
            Id = car.Id;
            IsRented = car.IsRented;
            Registration = car.Registration;
            Brand = car.CarType.Brand;
            Model = car.CarType.Model;
            Type = car.CarType.Type;
            Image = car.CarType.Image;
            CarTypeId = car.CarType.Id;
        }

        public CarResponse(Car car, int isRented)
        {
            Id = car.Id;
            IsRented = isRented;
            Registration = car.Registration;
            Brand = car.CarType.Brand;
            Model = car.CarType.Model;
            Type = car.CarType.Type;
            Image = car.CarType.Image;
            CarTypeId = car.CarType.Id;
        }
    }
}
