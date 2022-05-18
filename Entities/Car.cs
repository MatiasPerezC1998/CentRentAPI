using CentRent.Models;

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

        // CarRequest Constructors
        public Car(CarRequest.CreateRequest car)
        {
            Name = car.Name;
            Brand = car.Brand;
            Type = car.Type;
            Registration = car.Registration;
            IsRented = car.IsRented;
        }

        // CarResponse Constructors
        public Car(CarResponse car)
        {
            Id = car.Id;
            Brand = car.Brand;
            IsRented = car.IsRented;
            Name = car.Name;
            Registration = car.Registration;
            Type = car.Type;
        }

        public Car(CarResponse car, int isRented)
        {
            Id = car.Id;
            Name = car.Name;
            Brand = car.Brand;
            Type = car.Type;
            Registration = car.Registration;
            IsRented = isRented;
        }

        // Car Default Constructor
        public Car() {}
    }
}
