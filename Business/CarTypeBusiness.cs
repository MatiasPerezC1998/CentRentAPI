using CentRent.Models;
using CentRent.Data;
using CentRent.Interfaces;
using CentRent.Entities;

namespace CentRent.Business;

public class CarTypeBusiness : ICarTypeBusiness
{
    public readonly ICarTypeRepository _carTypeRepository;
    public readonly ICarRepository _carRepository;
    public readonly ICarBusiness _carBusiness;

    public CarTypeBusiness(ICarTypeRepository carTypeRepository, ICarRepository carRepository, ICarBusiness carBusiness)
    {
        _carTypeRepository = carTypeRepository;
        _carRepository = carRepository;
        _carBusiness = carBusiness;
    }

    public async Task<IEnumerable<CarTypeResponse>> GetAll()
    {
        var allCarTypes = await _carTypeRepository.GetAll();
        return new List<CarTypeResponse>(allCarTypes.Select(p => new CarTypeResponse(p)));
    }

    public async Task<CarTypeResponse?> Get(int id)
    {
        var car = await _carTypeRepository.Get(id);
        if (car != null)
        {
            return new CarTypeResponse(car);
        }
        return null;
    }

    public async Task<CarTypeResponse> Add(CarTypeRequest.CreateRequest newCar)
    {
        if (newCar.File != null)
        {
            SaveImage(newCar.File);
        }

        var carTypeToAdd = new CarType(newCar);
        return new CarTypeResponse(await _carTypeRepository.Add(carTypeToAdd));
    }

    public async Task Delete(int id)
    {
        var carTypeToDelete = await _carTypeRepository.Get(id);

        if (carTypeToDelete != null)
        {
            var carsToDelete = await _carRepository.GetCarTypeId(id);
            if (carsToDelete.Count > 0)
            {
                await _carBusiness.CustomerFinishRentingFromType(carsToDelete);
            }

            if (carTypeToDelete.Image != null)
            {
                DeleteImage(carTypeToDelete.Image);
            }

            await _carTypeRepository.Delete(carTypeToDelete);
        }
    }

    public async Task<CarTypeResponse> Update(CarTypeRequest.UpdateRequest carType)
    {
        if (carType.File != null)
        {
            SaveImage(carType.File);
        }

        var getCarType = await _carTypeRepository.Get(carType.Id);

        if (getCarType != null)
        {
            var carTypeToUpdate = new CarType(carType);

            return new CarTypeResponse(await _carTypeRepository.Update(carTypeToUpdate));
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

    private void DeleteImage(string file)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Images", file);

        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
    }
}