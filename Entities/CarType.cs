using CentRent.Models;

namespace CentRent.Entities
{
    public partial class CarType
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Type { get; set; }
        public string? Image { get; set; }

        public List<Car> Cars {get; set;}

        // CarRequest Constructors
        public CarType(CarTypeRequest.CreateRequest car)
        {
            Model = car.Model;
            Brand = car.Brand;
            Type = car.Type;
            Image = car?.File?.FileName;
        }

        // CarResponse Constructors
        public CarType(CarTypeResponse car)
        {
            Id = car.Id;
            Brand = car.Brand;
            Model = car.Model;
            Type = car.Type;
            Image = car.Image;
        }

        public CarType(CarTypeResponse car, int isRented)
        {
            Id = car.Id;
            Model = car.Model;
            Brand = car.Brand;
            Type = car.Type;
            Image = car.Image;
        }

        // Car Default Constructor
        public CarType() { }
    }
}
