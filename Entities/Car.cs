using CentRent.Models;

namespace CentRent.Entities
{
    public partial class Car
    {
        public int Id { get; set; }
        public string? Registration { get; set; }
        public int IsRented { get; set; }

        public int CarTypeId { get; set; }
        public CarType CarType {get; set;}

        // CarRequest Constructors
        public Car(CarRequest.CreateRequest car)
        {
            Registration = car.Registration;
            CarTypeId = car.CarTypeId;
        }

        // CarResponse Constructors
        public Car(CarResponse car)
        {
            Id = car.Id;
            Registration = car.Registration;
            CarTypeId = car.CarTypeId;
        }

        public Car(CarResponse car, int isRented)
        {
            Id = car.Id;
            Registration = car.Registration;
            CarTypeId = car.CarTypeId;
            IsRented = isRented;
        }

        // Car Default Constructor
        public Car() { }
    }
}
