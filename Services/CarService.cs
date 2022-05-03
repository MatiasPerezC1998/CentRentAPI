using Microsoft.EntityFrameworkCore;

using CentRent.Models;
using CentRent.Data;

namespace CentRent.Services;

public interface ICarService {
    IEnumerable<Car> GetAll();
    Car? Get(int id);
    Car Add(Car newCar);
    void Update(Car car);
    void Delete(int id);
}

public class CarService: ICarService{

    private readonly CentRentContext _context;
    public CarService(CentRentContext context) {
        _context = context;
    }

    public IEnumerable<Car> GetAll() {
        return _context.Cars
            .AsNoTracking()
            .ToList();
    }

    public Car? Get(int id) {
        return _context.Cars
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id);

        //Cars.FirstOrDefault(p => p.Id == id);
    }

    public Car Add(Car newCar) { // Cambiamos car por newCar
        _context.Cars.Add(newCar);
        _context.SaveChanges();

        return newCar;

        // var newCars = Cars.Select(c => c.Registration).ToList();
        // if (!newCars.Contains(car.Registration) && 
        //     car.Name != "" && 
        //     car.Brand != "" &&
        //     car.Type != "" &&
        //     car.Registration != "" &&
        //     (car.IsRented == true || car.IsRented == false)) {

        //     car.Id = nextId++;
        //     Cars.Add(car);
        //     Console.WriteLine(car);

        // } else if (car.Name == "" && 
        //     car.Brand == "" &&
        //     car.Type == "" &&
        //     car.Registration == "") {

        //     Console.WriteLine("Error, hay algún campo vacío");

        // } else if (newCars.Contains(car.Registration)) {

        //     Console.WriteLine("Error, matrícula duplicada, introduzca uno nuevo con diferente matrícula");
        
        // } else {

        //     Console.WriteLine("Error genérico");
            
        // }
    }

    public void Delete(int id) {
        var carToDelete = _context.Cars.FirstOrDefault(x => x.Id == id);
        if (carToDelete is not null) {
            _context.Cars.Remove(carToDelete);
            _context.SaveChanges();
        }

        // var car = Get(id);
        // if(car is null)
        //     return;

        // Cars.Remove(car);
    }

    public void Update(Car car) {
        var index  = _context.Cars.FirstOrDefault(p => p.Id == car.Id);

        index.Name = car.Name;
        index.Brand = car.Brand;
        index.Type = car.Type;
        index.Registration = car.Registration;
        index.IsRented = car.IsRented;

        _context.SaveChanges();

        // var index = Cars.FindIndex(p => p.Id == car.Id);
        // if(index == -1)
        //     return;

        // Cars[index] = car;
    }
}