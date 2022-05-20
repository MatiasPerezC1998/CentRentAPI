using CentRent.Models;
using CentRent.Data;
using CentRent.Interfaces;
using CentRent.Entities;

namespace CentRent.Business;

public class CarTypeBusiness : ICarTypeBusiness
{
    public readonly ICarTypeRepository _carTypeRepository;

    public CarTypeBusiness(ICarTypeRepository carTypeRepository)
    {
        _carTypeRepository = carTypeRepository;
    }

    public IEnumerable<CarTypeResponse> GetAll()
    {
        return _carTypeRepository.GetAll();
    }

    public CarTypeResponse? Get(int id)
    {
        return _carTypeRepository.Get(id);
    }

    public CarTypeResponse Add(CarTypeRequest.CreateRequest newCar)
    {
        if (newCar.File != null)
        {
            SaveImage(newCar.File);
        }

        return _carTypeRepository.Add(newCar);
    }

    public void Delete(int id)
    {
        var carToDelete = _carTypeRepository.Get(id);

        if (carToDelete != null)
        {
            var car = new CarType(carToDelete);
            _carTypeRepository.Delete(car);
        }
    }

    public CarTypeResponse Update(CarTypeRequest.UpdateRequest car)
    {
        if (car.File != null)
        {
            SaveImage(car.File);
        }

        var getCar = _carTypeRepository.Get(car.Id);

        if (getCar != null)
        {
            var carToUpdate = new CarType()
            {
                Id = getCar.Id,
                Brand = getCar.Brand,
                Model = getCar.Model,
                Type = getCar.Type,
                Image = getCar.Image
            };

            return _carTypeRepository.Update(carToUpdate);
        }

        return null;
    }

    private void SaveImage(IFormFile file)
    {
        try
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Images", file.FileName);

            Console.WriteLine(path);

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void DeleteImage(IFormFile file)
    {
        //var filePath = Server.MapPath("~/Images/" + file.FileName);
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Images", file.FileName);

        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
    }
}