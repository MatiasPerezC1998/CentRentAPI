using Microsoft.EntityFrameworkCore;
using CentRent.Models;
using CentRent.Data;

namespace CentRent.Services;

public interface IUserService {
    IEnumerable<User> GetAll();
    User? Get(int id);
    User Add(User newUser);
    void Delete(int id);
    void Update(User user);
}

public class UserService: IUserService {

    private readonly CentRentContext _context;
    public UserService(CentRentContext context) {
        _context = context;
    }

    public IEnumerable<User> GetAll() {
        return _context.Users
            .AsNoTracking()
            .ToList();
    }

    public User? Get(int id) {
        return _context.Users
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id);
        
        // Users.FirstOrDefault(p => p.Id == id);
    }

    public User Add(User newUser) { // Cambiamos user por newUser
        _context.Users.Add(newUser);
        _context.SaveChanges();

        return newUser;
    }

    public void Delete(int id) {
        var userToDelete = _context.Users.Find(id);
        if (userToDelete is not null) {
            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
        }

        // var user = Get(id);
        // if(user is null)
        //     return;

        // Users.Remove(user);
    }

    public void Update(User user) {
        var index = _context.Users.FirstOrDefault(p => p.Id == user.Id);

        index.Name = user.Name;
        index.Surname = user.Surname;
        index.Email = user.Email;
        index.Phone = user.Phone;
        index.Dni = user.Dni;

        _context.SaveChanges();

        // var index = _context.Users.Find(p => p.Id == user.Id);
        // if(index == -1)
        //     return;

        // Users[index] = user;
    }
}